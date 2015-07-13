using Facile;
using Facile.Core;
using Orders.com.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF.VM
{
    public class EntityViewModelBase<T, Tkey> : ViewModelBase where T : IDomainObject<Tkey>, new()
    {
        private IService<T, Tkey> _service;
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

        protected virtual void OnInsertSuccess(ExecutionResult<T> result)
        {
        }

        protected virtual void OnUpdateSuccess(ExecutionResult<T> result)
        {
        }

        protected virtual void OnDeleteSuccess(ExecutionResult result)
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
                    if (IsNew)
                    {
                        OnInsertSuccess(result);
                        IsNew = false;
                    }
                    else
                    {
                        OnUpdateSuccess(result);
                    }
                    if (EntitySaved != null) EntitySaved(this, EventArgs.Empty);
                    IsDirty = false;
                    IsValid = true;
                    Errors = null;
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
                OnDeleteSuccess(result);
                if (EntityDeleted != null) EntityDeleted(this, EventArgs.Empty);
            }
        }

        protected virtual ICommand<T> CreateCommand()
        {
            if (IsNew)
                return _service.InsertCommand(CurrentEntity);
            else
                return _service.UpdateCommand(CurrentEntity);
        }

        public bool CanSave()
        {
            return CreateCommand().CanExecute;
        }
    }
}
