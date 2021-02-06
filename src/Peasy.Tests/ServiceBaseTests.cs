using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Peasy.Extensions;

namespace Peasy.Core.Tests
{
    public class ServiceBaseTests
    {
        [Fact]
        public async Task Ensure_Method_Invocations_For_GetAllCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.GetAllCommand().ExecuteAsync();
            service.OnGetAllCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnGetAllCommandGetRulesAsyncWasInvoked.ShouldBe(true);
            service.OnGetAllCommandPerformValidationAsyncWasInvoked.ShouldBe(true);
            service.OnGetAllCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task GetAllCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var validationResult = await service.GetAllCommand().ValidateAsync();
            var executionResult = await validationResult.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_GetByIDCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.GetByIDCommand(1).ExecuteAsync();
            service.OnGetByIDCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandValidateIDWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandGetRulesAsyncWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandPerformValiationAsyncWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task GetByIDCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var validatonResult = await service.GetByIDCommand(123).ValidateAsync();
            var executionResult = await validatonResult.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_InsertCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.InsertCommand(new Person()).ExecuteAsync();
            service.OnInsertCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnInsertCommandValidateObjectWasInvoked.ShouldBe(true);
            service.OnInsertCommandGetRulesAsyncWasInvoked.ShouldBe(true);
            service.OnInsertCommandPerformValidationAsyncWasInvoked.ShouldBe(true);
            service.OnInsertCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task InsertCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var data = new Person();
            var validatonResult = await service.InsertCommand(data).ValidateAsync();
            var executionResult = await validatonResult.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_UpdateCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.UpdateCommand(new Person()).ExecuteAsync();
            service.OnUpdateCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnUpdateCommandValidateObjectWasInvoked.ShouldBe(true);
            service.OnUpdateCommandGetRulesAsyncWasInvoked.ShouldBe(true);
            service.OnUpdateCommandPerformValidationAsyncWasInvoked.ShouldBe(true);
            service.OnUpdateCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var data = new Person();
            var validatonResult = await service.UpdateCommand(data).ValidateAsync();
            var executionResult = await validatonResult.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_DeleteCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.DeleteCommand(1).ExecuteAsync();
            service.OnDeleteCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnDeleteCommandValidateIdWasInvoked.ShouldBe(true);
            service.OnDeleteCommandGetRulesAsyncWasInvoked.ShouldBe(true);
            service.OnDeleteCommandPerformValidationAsyncWasInvoked.ShouldBe(true);
            service.OnDeleteCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var validationOperation = await service.DeleteCommand(123).ValidateAsync();
            var executionResult = await validationOperation.CompleteCommandExecutionAsync();
            executionResult.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task TheseRules_Works_As_Expected()
        {
            var service = new TheseRulesStub(new PersonProxyStub());

            var insertResult = await service.InsertCommand(new Person()).ExecuteAsync();
            var updateResult = await service.UpdateCommand(new Person()).ExecuteAsync();

            insertResult.Success.ShouldBeTrue();
            updateResult.Success.ShouldBeFalse();
        }

        public class ServiceBaseStub : ServiceBase<Person, long>
        {
            #region Properties

            public bool OnGetAllCommandGetRulesAsyncWasInvoked { get; private set; }
            public bool OnGetAllCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnGetAllCommandPerformValidationAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandGetRulesAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandPerformValiationAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnInsertCommandGetRulesAsyncWasInvoked { get; private set; }
            public bool OnInsertCommandPerformValidationAsyncWasInvoked { get; private set; }
            public bool OnInsertCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnUpdateCommandGetRulesAsyncWasInvoked { get; private set; }
            public bool OnUpdateCommandPerformValidationAsyncWasInvoked { get; private set; }
            public bool OnUpdateCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnDeleteCommandGetRulesAsyncWasInvoked { get; private set; }
            public bool OnDeleteCommandPerformValidationAsyncWasInvoked { get; private set; }
            public bool OnDeleteCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnDeleteCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnUpdateCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnInsertCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnGetAllCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandValidateIDWasInvoked { get; private set; }
            public bool OnInsertCommandValidateObjectWasInvoked { get; private set; }
            public bool OnUpdateCommandValidateObjectWasInvoked { get; private set; }
            public bool OnDeleteCommandValidateIdWasInvoked { get; private set; }

            #endregion

            public ServiceBaseStub(IDataProxy<Person, long> dataProxy) : base(dataProxy)
            {
            }

            #region GetAll

            protected override Task OnGetAllCommandInitializationAsync(ExecutionContext<Person> context)
            {
                OnGetAllCommandInitializationAsyncWasInvoked = true;
                return base.OnGetAllCommandInitializationAsync(context);
            }

            protected override Task<IEnumerable<IRule>> OnGetAllCommandGetRulesAsync(ExecutionContext<Person> context)
            {
                OnGetAllCommandGetRulesAsyncWasInvoked = true;
                return base.OnGetAllCommandGetRulesAsync(context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnGetAllCommandPerformValidationAsync(ExecutionContext<Person> context)
            {
                OnGetAllCommandPerformValidationAsyncWasInvoked = true;
                return base.OnGetAllCommandPerformValidationAsync(context);
            }

            protected override Task<IEnumerable<Person>> OnGetAllCommandValidationSuccessAsync(ExecutionContext<Person> context)
            {
                OnGetAllCommandValidationSuccessAsyncWasInvoked = true;
                return base.OnGetAllCommandValidationSuccessAsync(context);
            }

            #endregion

            #region GetByID

            protected override Task OnGetByIDCommandInitializationAsync(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandInitializationAsyncWasInvoked = true;
                return base.OnGetByIDCommandInitializationAsync(id, context);
            }

            protected override IEnumerable<ValidationResult> OnGetByIDCommandValidateID(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandValidateIDWasInvoked = true;
                return base.OnGetByIDCommandValidateID(id, context);
            }

            protected override Task<IEnumerable<IRule>> OnGetByIDCommandGetRulesAsync(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandGetRulesAsyncWasInvoked = true;
                return base.OnGetByIDCommandGetRulesAsync(id, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnGetByIDCommandPerformValidationAsync(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandPerformValiationAsyncWasInvoked = true;
                return base.OnGetByIDCommandPerformValidationAsync(id, context);
            }

            protected override Task<Person> OnGetByIDCommandValidationSuccessAsync(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandValidationSuccessAsyncWasInvoked = true;
                return base.OnGetByIDCommandValidationSuccessAsync(id, context);
            }

            #endregion

            #region Insert

            protected override Task OnInsertCommandInitializationAsync(Person entity, ExecutionContext<Person> context)
            {
                OnInsertCommandInitializationAsyncWasInvoked = true;
                return base.OnInsertCommandInitializationAsync(entity, context);
            }

            protected override IEnumerable<ValidationResult> OnInsertCommandValidateObject(Person resource, ExecutionContext<Person> context)
            {
                OnInsertCommandValidateObjectWasInvoked = true;
                return base.OnInsertCommandValidateObject(resource, context);
            }

            protected override Task<IEnumerable<IRule>> OnInsertCommandGetRulesAsync(Person resource, ExecutionContext<Person> context)
            {
                OnInsertCommandGetRulesAsyncWasInvoked = true;
                return base.OnInsertCommandGetRulesAsync(resource, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnInsertCommandPerformValidationAsync(Person resource, ExecutionContext<Person> context)
            {
                OnInsertCommandPerformValidationAsyncWasInvoked = true;
                return base.OnInsertCommandPerformValidationAsync(resource, context);
            }

            protected override Task<Person> OnInsertCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
            {
                OnInsertCommandValidationSuccessAsyncWasInvoked = true;
                return base.OnInsertCommandValidationSuccessAsync(person, context);
            }

            #endregion

            #region Update

            protected override Task OnUpdateCommandInitializationAsync(Person entity, ExecutionContext<Person> context)
            {
                OnUpdateCommandInitializationAsyncWasInvoked = true;
                return base.OnUpdateCommandInitializationAsync(entity, context);
            }

            protected override IEnumerable<ValidationResult> OnUpdateCommandValidateObject(Person resource, ExecutionContext<Person> context)
            {
                OnUpdateCommandValidateObjectWasInvoked = true;
                return base.OnUpdateCommandValidateObject(resource, context);
            }

            protected override Task<IEnumerable<IRule>> OnUpdateCommandGetRulesAsync(Person resource, ExecutionContext<Person> context)
            {
                OnUpdateCommandGetRulesAsyncWasInvoked = true;
                return base.OnUpdateCommandGetRulesAsync(resource, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnUpdateCommandPerformValidationAsync(Person resource, ExecutionContext<Person> context)
            {
                OnUpdateCommandPerformValidationAsyncWasInvoked = true;
                return base.OnUpdateCommandPerformValidationAsync(resource, context);
            }

            protected override Task<Person> OnUpdateCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
            {
                OnUpdateCommandValidationSuccessAsyncWasInvoked = true;
                return base.OnUpdateCommandValidationSuccessAsync(person, context);
            }

            #endregion

            #region Delete

            protected override Task<IEnumerable<IRule>> OnDeleteCommandGetRulesAsync(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandGetRulesAsyncWasInvoked = true;
                return base.OnDeleteCommandGetRulesAsync(id, context);
            }

            protected override IEnumerable<ValidationResult> OnDeleteCommandValidateId(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandValidateIdWasInvoked = true;
                return base.OnDeleteCommandValidateId(id, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnDeleteCommandPerformValidationAsync(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandPerformValidationAsyncWasInvoked = true;
                return base.OnDeleteCommandPerformValidationAsync(id, context);
            }

            protected override Task OnDeleteCommandValidationSuccessAsync(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandValidationSuccessAsyncWasInvoked = true;
                return base.OnDeleteCommandValidationSuccessAsync(id, context);
            }

            protected override Task OnDeleteCommandInitializationAsync(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandInitializationAsyncWasInvoked = true;
                return base.OnDeleteCommandInitializationAsync(id, context);
            }

            #endregion
        }

        public class TheseRulesStub : ServiceBaseStub
        {
            public TheseRulesStub(IDataProxy<Person, long> dataProxy) : base(dataProxy)
            {
            }

            // Because we should be able to.
            protected override Task<IEnumerable<IRule>> TheseRules(params IRule[] rules)
            {
                return base.TheseRules(rules);
            }

            protected override Task<IEnumerable<IRule>> OnInsertCommandGetRulesAsync(Person resource, ExecutionContext<Person> context)
            {
                return TheseRules
                (
                    new TrueRule(),
                    new TrueRule().IfValidThenValidate(new TrueRule()),
                    new TrueRule()
                );
            }

            protected override Task<IEnumerable<IRule>> OnUpdateCommandGetRulesAsync(Person resource, ExecutionContext<Person> context)
            {
                return TheseRules
                (
                    new TrueRule(),
                    new TrueRule(),
                    new TrueRule().IfValidThenValidate(new FalseRule1())
                );
            }
        }

        public class PersonProxyStub : IDataProxy<Person, long>
        {
            public Task DeleteAsync(long id)
            {
                return Task.FromResult<object>(null);
            }

            public Task<IEnumerable<Person>> GetAllAsync()
            {
                return Task.FromResult(Enumerable.Empty<Person>());
            }

            public Task<Person> GetByIDAsync(long id)
            {
                return Task.FromResult(new Person());
            }

            public Task<Person> InsertAsync(Person entity)
            {
                return Task.FromResult(new Person());
            }

            public Task<Person> UpdateAsync(Person entity)
            {
                return Task.FromResult(new Person());
            }
        }
    }

}
