using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Orders.com.WPF.VM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isDirty = false;
        private bool _isNew = false;
        private bool _isValid = true;
        private IEnumerable<ValidationResult> _errors;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

        public bool IsNew
        {
            get { return _isNew; }
            protected set
            {
                _isNew = value;
                OnPropertyChanged("IsNew");
            }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            protected set
            {
                _isDirty = value;
                OnPropertyChanged("IsDirty");
            }
        }

        public bool IsValid
        {
            get { return _isValid; }
            protected set
            {
                _isValid = value;
                OnPropertyChanged("IsValid");
            }
        }

        public IEnumerable<ValidationResult> Errors
        {
            get { return _errors; }
            protected set
            {
                _errors = value;
                OnPropertyChanged("Errors");
            }
        }
    }
}
