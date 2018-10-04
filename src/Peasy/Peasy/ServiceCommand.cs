using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Peasy
{
    public class ServiceCommand : Command
    {
        private Action _initializationMethod;
        private Func<Task> _initializationAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;
        private Action _executeMethod;
        private Func<Task> _executeAsyncMethod;

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Action executeMethod,
                              Func<Task> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Action executeMethod,
                              Func<Task> executeAsyncMethod)
            : this(initializationMethod: initializationMethod,
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Action initializationMethod,
                              Action executeMethod)
            : this(initializationMethod: initializationMethod,
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: () => Task.FromResult<object>(null))
        {
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod,
                              Func<Task> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: () => {},
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Action executeMethod,
                              Func<Task> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: getErrorsMethod,
                   getErrorsAsyncMethod: getErrorsAsyncMethod,
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Action executeMethod, Func<Task> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Action executeMethod, Func<IEnumerable<ValidationResult>> getErrorsMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: getErrorsMethod,
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: () => Task.FromResult<object>(null))
        {
        }

        public ServiceCommand(Func<Task> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: getErrorsAsyncMethod,
                   executeMethod: () => {},
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Action executeMethod,
                              Func<Task> executeAsyncMethod,
                              Func<IEnumerable<IRule>> getBusinessRulesMethod,
                              Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => getBusinessRulesMethod().GetValidationResults(),
                   getErrorsAsyncMethod: async () =>
                   {
                      var rules = await getBusinessRulesAsyncMethod();
                      return await rules.GetValidationResultsAsync();
                   },
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Action executeMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => getBusinessRulesMethod().GetValidationResults(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: () => Task.FromResult<object>(null))
        {
        }

        public ServiceCommand(Func<Task> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: () => {},
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<Task> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: async () =>
                   {
                      var rules = await getBusinessRulesAsyncMethod();
                      return await rules.GetValidationResultsAsync();
                   },
                   executeMethod: () => {},
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: async () =>
                   {
                       var rules = await getBusinessRulesAsyncMethod();
                       return await rules.GetValidationResultsAsync();
                   },
                   executeMethod: () => {},
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: getErrorsAsyncMethod,
                   executeMethod: () => {},
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        protected override void OnInitialization()
        {
            _initializationMethod();
        }

        protected override void OnExecute()
        {
            _executeMethod();
        }

        protected override async Task OnInitializationAsync()
        {
            await _initializationAsyncMethod();
        }

        protected async override Task OnExecuteAsync()
        {
            await _executeAsyncMethod();
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return _getErrorsMethod();
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return await _getErrorsAsyncMethod();
        }
    }

    public class ServiceCommand<T> : Command<T>
    {
        private Action _initializationMethod;
        private Func<Task> _initializationAsyncMethod;
        private Func<IEnumerable<ValidationResult>> _getErrorsMethod;
        private Func<Task<IEnumerable<ValidationResult>>> _getErrorsAsyncMethod;
        private Func<T> _executeMethod;
        private Func<Task<T>> _executeAsyncMethod;

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Func<T> executeMethod,
                              Func<Task<T>> executeAsyncMethod)
        {
            _initializationMethod = initializationMethod;
            _initializationAsyncMethod = initializationAsyncMethod;
            _executeMethod = executeMethod;
            _executeAsyncMethod = executeAsyncMethod;
            _getErrorsMethod = getErrorsMethod;
            _getErrorsAsyncMethod = getErrorsAsyncMethod;
        }

        public ServiceCommand(Action initializationMethod,
                              Func<Task> initializationAsyncMethod,
                              Func<T> executeMethod,
                              Func<Task<T>> executeAsyncMethod)
            : this(initializationMethod: initializationMethod,
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Action initializationMethod,
                              Func<T> executeMethod)
            : this(initializationMethod: initializationMethod,
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: () => Task.FromResult(default(T)))
        {
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod,
                              Func<Task<T>> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: () => default(T),
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<IEnumerable<ValidationResult>> getErrorsMethod,
                              Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod,
                              Func<T> executeMethod,
                              Func<Task<T>> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: getErrorsMethod,
                   getErrorsAsyncMethod: getErrorsAsyncMethod,
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<T> executeMethod, Func<Task<T>> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<T> executeMethod, Func<IEnumerable<ValidationResult>> getErrorsMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: getErrorsMethod,
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: () => Task.FromResult(default(T)))
        {
        }

        public ServiceCommand(Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: getErrorsAsyncMethod,
                   executeMethod: () => default(T),
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<T> executeMethod,
                              Func<Task<T>> executeAsyncMethod,
                              Func<IEnumerable<IRule>> getBusinessRulesMethod,
                              Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => getBusinessRulesMethod().GetValidationResults(),
                   getErrorsAsyncMethod: async () =>
                   {
                      var rules = await getBusinessRulesAsyncMethod();
                      return await rules.GetValidationResultsAsync();
                   },
                   executeMethod: executeMethod,
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<T> executeMethod, Func<IEnumerable<IRule>> getBusinessRulesMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => getBusinessRulesMethod().GetValidationResults(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: executeMethod,
                   executeAsyncMethod: () => Task.FromResult(default(T)))
        {
        }

        public ServiceCommand(Func<Task<T>> executeAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: () => Task.FromResult(Enumerable.Empty<ValidationResult>()),
                   executeMethod: () => default(T),
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: () => Task.FromResult<object>(null),
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: async () =>
                   {
                      var rules = await getBusinessRulesAsyncMethod();
                      return await rules.GetValidationResultsAsync();
                   },
                   executeMethod: () => default(T),
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<IRule>>> getBusinessRulesAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: async () =>
                   {
                       var rules = await getBusinessRulesAsyncMethod();
                       return await rules.GetValidationResultsAsync();
                   },
                   executeMethod: () => default(T),
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        public ServiceCommand(Func<Task> initializationAsyncMethod, Func<Task<T>> executeAsyncMethod, Func<Task<IEnumerable<ValidationResult>>> getErrorsAsyncMethod)
            : this(initializationMethod: () => {},
                   initializationAsyncMethod: initializationAsyncMethod,
                   getErrorsMethod: () => Enumerable.Empty<ValidationResult>(),
                   getErrorsAsyncMethod: getErrorsAsyncMethod,
                   executeMethod: () => default(T),
                   executeAsyncMethod: executeAsyncMethod)
        {
        }

        protected override void OnInitialization()
        {
            _initializationMethod();
        }

        protected override T OnExecute()
        {
            var result = _executeMethod();
            return result;
        }

        protected override async Task OnInitializationAsync()
        {
            await _initializationAsyncMethod();
        }

        protected async override Task<T> OnExecuteAsync()
        {
            return await _executeAsyncMethod();
        }

        public override IEnumerable<ValidationResult> GetErrors()
        {
            return _getErrorsMethod();
        }

        public override async Task<IEnumerable<ValidationResult>> GetErrorsAsync()
        {
            return await _getErrorsAsyncMethod();
        }
    }
}