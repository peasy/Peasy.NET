using Facile;
using Facile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.WPF
{
    public class EntityViewModelBase<T, Tkey> : ViewModelBase where T : new()
    {
        private IService<T, Tkey> _service;
        private T _current;

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

        protected virtual void OnCommandExecutionSuccess(ExecutionResult<T> result)
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
                    OnCommandExecutionSuccess(result);
                    IsNew = false;
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

        protected virtual ICommand<T> CreateCommand()
        {
            if (IsNew)
                return _service.InsertCommand(CurrentEntity);
            else
                return _service.UpdateCommand(CurrentEntity);
        }
    }
}
