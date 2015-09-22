using Facile.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.WPF.VM
{
    public class EntityViewModelBase<T, Tkey> : ViewModelBase where T : IDomainObject<Tkey>, new()
    {
        protected IService<T, Tkey> _service;
        private T _current;

        public EventHandler EntitySaved { get; set; }
        public EventHandler EntityDeleted { get; set; }

        public EntityViewModelBase(IService<T, Tkey> service)
        {
            _service = service;
            CurrentEntity = new T();
            IsNew = true;
        }

        public EntityViewModelBase(T entity, IService<T, Tkey> service)
        {
            _service = service;
            CurrentEntity = entity;
            IsDirty = false;
            IsNew = false;
        }

        public T CurrentEntity
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged("CurrentEntity");
            }
        }

        protected virtual void OnInsertSuccess(T result)
        {
        }

        protected virtual void OnUpdateSuccess(T result)
        {
        }

        protected virtual void OnDeleteSuccess()
        {
        }
        
        public virtual async Task SaveAsync()
        {
            if (IsDirty)
            {
                ExecutionResult<T> result = await CreateCommand().ExecuteAsync();

                if (result.Success)
                {
                    CurrentEntity = result.Value;
                    IsDirty = false;
                    IsValid = true;
                    Errors = Enumerable.Empty<ValidationResult>();
                    if (IsNew)
                    {
                        IsNew = false;
                        OnInsertSuccess(result.Value);
                    }
                    else
                    {
                        OnUpdateSuccess(result.Value);
                    }
                    if (EntitySaved != null) EntitySaved(this, EventArgs.Empty);
                }
                else
                {
                    IsValid = false;
                    Errors = result.Errors.ToArray();
                }
            }
        }

        public virtual async Task DeleteAsync()
        {
            if (!IsNew)
            {
                var result = await _service.DeleteCommand(CurrentEntity.ID).ExecuteAsync();
                if (result.Success)
                {
                    OnDeleteSuccess();
                    if (EntityDeleted != null) EntityDeleted(this, EventArgs.Empty);
                }
                else
                {
                    IsValid = false;
                    Errors = result.Errors.ToArray();
                }               
            }
        }

        protected virtual ICommand<T> CreateCommand()
        {
            if (IsNew)
                return _service.InsertCommand(CurrentEntity);
            else
                return _service.UpdateCommand(CurrentEntity);
        }

        public virtual bool CanSave
        {
            get { return !CreateCommand().GetErrors().Any(); }
        }

    }
}
