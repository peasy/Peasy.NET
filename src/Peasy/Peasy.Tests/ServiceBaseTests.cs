using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xunit;

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
            service.OnGetBusinessRulesForGetAllAsyncWasInvoked.ShouldBe(true);
            service.OnPerformGetAllCommandValidationAsyncWasInvoked.ShouldBe(true);
            service.OnGetAllCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_GetAllCommandAsync_I()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var command = service.GetAllCommand() as ISupportValidation<ExecutionResult<IEnumerable<Person>>>;
            var validationResult = command.ValidateAsync();
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_GetByIDCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.GetByIDCommand(1).ExecuteAsync();
            service.OnGetByIDCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnValidateIdForGetByIDWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForGetByIDAsyncWasInvoked.ShouldBe(true);
            service.OnPerformGetByIDCommandValidationAsyncWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_InsertCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.InsertCommand(new Person()).ExecuteAsync();
            service.OnInsertCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnValidateObjectForInsertWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForInsertAsyncWasInvoked.ShouldBe(true);
            service.OnPerformInsertCommandValidationAsyncWasInvoked.ShouldBe(true);
            service.OnInsertCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_UpdateCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.UpdateCommand(new Person()).ExecuteAsync();
            service.OnUpdateCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnValidateObjectForUpdateWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForUpdateAsyncWasInvoked.ShouldBe(true);
            service.OnPerformUpdateCommandValidationAsyncWasInvoked.ShouldBe(true);
            service.OnUpdateCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_DeleteCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.DeleteCommand(1).ExecuteAsync();
            service.OnDeleteCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.OnValidateIdForDeleteWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForDeleteAsyncWasInvoked.ShouldBe(true);
            service.OnPerformDeleteCommandValidationAsyncWasInvoked.ShouldBe(true);
            service.OnDeleteCommandValidationSuccessAsyncWasInvoked.ShouldBe(true);
        }
        public class ServiceBaseStub : ServiceBase<Person, long>
        {
            #region Properties

            public bool OnGetBusinessRulesForGetAllAsyncWasInvoked { get; private set; }
            public bool OnGetAllCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnPerformGetAllCommandValidationAsyncWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForGetByIDAsyncWasInvoked { get; private set; }
            public bool OnPerformGetByIDCommandValidationAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForInsertAsyncWasInvoked { get; private set; }
            public bool OnPerformInsertCommandValidationAsyncWasInvoked { get; private set; }
            public bool OnInsertCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForUpdateAsyncWasInvoked { get; private set; }
            public bool OnPerformUpdateCommandValidationAsyncWasInvoked { get; private set; }
            public bool OnUpdateCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForDeleteAsyncWasInvoked { get; private set; }
            public bool OnPerformDeleteCommandValidationAsyncWasInvoked { get; private set; }
            public bool OnDeleteCommandValidationSuccessAsyncWasInvoked { get; private set; }
            public bool OnDeleteCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnUpdateCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnInsertCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnGetByIDCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnGetAllCommandInitializationAsyncWasInvoked { get; private set; }
            public bool OnValidateIdForGetByIDWasInvoked { get; private set; }
            public bool OnValidateObjectForInsertWasInvoked { get; private set; }
            public bool OnValidateObjectForUpdateWasInvoked { get; private set; }
            public bool OnValidateIdForDeleteWasInvoked { get; private set; }

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

            protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForGetAllAsync(ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForGetAllAsyncWasInvoked = true;
                return base.OnGetBusinessRulesForGetAllAsync(context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnPerformGetAllCommandValidationAsync(ExecutionContext<Person> context)
            {
                OnPerformGetAllCommandValidationAsyncWasInvoked = true;
                return base.OnPerformGetAllCommandValidationAsync(context);
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

            protected override IEnumerable<ValidationResult> OnValidateIdForGetByID(long id, ExecutionContext<Person> context)
            {
                OnValidateIdForGetByIDWasInvoked = true;
                return base.OnValidateIdForGetByID(id, context);
            }

            protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForGetByIDAsync(long id, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForGetByIDAsyncWasInvoked = true;
                return base.OnGetBusinessRulesForGetByIDAsync(id, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnPerformGetByIDCommandValidationAsync(long id, ExecutionContext<Person> context)
            {
                OnPerformGetByIDCommandValidationAsyncWasInvoked = true;
                return base.OnPerformGetByIDCommandValidationAsync(id, context);
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
            protected override IEnumerable<ValidationResult> OnValidateObjectForInsert(Person resource, ExecutionContext<Person> context)
            {
                OnValidateObjectForInsertWasInvoked = true;
                return base.OnValidateObjectForInsert(resource, context);
            }

            protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForInsertAsync(Person person, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForInsertAsyncWasInvoked = true;
                return base.OnGetBusinessRulesForInsertAsync(person, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnPerformInsertCommandValidationAsync(Person person, ExecutionContext<Person> context)
            {
                OnPerformInsertCommandValidationAsyncWasInvoked = true;
                return base.OnPerformInsertCommandValidationAsync(person, context);
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

            protected override IEnumerable<ValidationResult> OnValidateObjectForUpdate(Person resource, ExecutionContext<Person> context)
            {
                OnValidateObjectForUpdateWasInvoked = true;
                return base.OnValidateObjectForUpdate(resource, context);
            }

            protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForUpdateAsync(Person person, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForUpdateAsyncWasInvoked = true;
                return base.OnGetBusinessRulesForUpdateAsync(person, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnPerformUpdateCommandValidationAsync(Person person, ExecutionContext<Person> context)
            {
                OnPerformUpdateCommandValidationAsyncWasInvoked = true;
                return base.OnPerformUpdateCommandValidationAsync(person, context);
            }

            protected override Task<Person> OnUpdateCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
            {
                OnUpdateCommandValidationSuccessAsyncWasInvoked = true;
                return base.OnUpdateCommandValidationSuccessAsync(person, context);
            }

            #endregion

            #region Delete

            protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForDeleteAsync(long id, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForDeleteAsyncWasInvoked = true;
                return base.OnGetBusinessRulesForDeleteAsync(id, context);
            }
            protected override IEnumerable<ValidationResult> OnValidateIdForDelete(long id, ExecutionContext<Person> context)
            {
                OnValidateIdForDeleteWasInvoked = true;
                return base.OnValidateIdForDelete(id, context);
            }

            protected override Task<IEnumerable<ValidationResult>> OnPerformDeleteCommandValidationAsync(long id, ExecutionContext<Person> context)
            {
                OnPerformDeleteCommandValidationAsyncWasInvoked = true;
                return base.OnPerformDeleteCommandValidationAsync(id, context);
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
