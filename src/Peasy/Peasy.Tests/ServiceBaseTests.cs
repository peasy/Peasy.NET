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
        public void Ensure_Method_Invocations_For_GetAllCommand()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.GetAllCommand().Execute();
            service.OnGetAllCommandInitializationWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForGetAllWasInvoked.ShouldBe(true);
            service.OnPerformGetAllCommandValidationWasInvoked.ShouldBe(true);
            service.OnGetAllCommandValidationSuccessWasInvoked.ShouldBe(true);
        }

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
        public void Ensure_Method_Invocations_For_GetByIDCommand()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.GetByIDCommand(1).Execute();
            service.OnGetByIDCommandInitializationWasInvoked.ShouldBe(true);
            service.OnValidateIdForGetByIDWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForGetByIDWasInvoked.ShouldBe(true);
            service.OnPerformGetByIDCommandValidationWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandValidationSuccessWasInvoked.ShouldBe(true);
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
        public void Ensure_Method_Invocations_For_InsertCommand()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.InsertCommand(new Person()).Execute();
            service.OnInsertCommandInitializationWasInvoked.ShouldBe(true);
            service.OnValidateObjectForInsertWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForInsertWasInvoked.ShouldBe(true);
            service.OnPerformInsertCommandValidationWasInvoked.ShouldBe(true);
            service.OnInsertCommandValidationSuccessWasInvoked.ShouldBe(true);
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
        public void Ensure_Method_Invocations_For_Update()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.UpdateCommand(new Person()).Execute();
            service.OnUpdateCommandInitializationWasInvoked.ShouldBe(true);
            service.OnValidateObjectForUpdateWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForUpdateWasInvoked.ShouldBe(true);
            service.OnPerformUpdateCommandValidationWasInvoked.ShouldBe(true);
            service.OnUpdateCommandValidationSuccessWasInvoked.ShouldBe(true);
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
        public void Ensure_Method_Invocations_For_Delete()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.DeleteCommand(1).Execute();
            service.OnDeleteCommandInitializationWasInvoked.ShouldBe(true);
            service.OnValidateIdForDeleteWasInvoked.ShouldBe(true);
            service.OnGetBusinessRulesForDeleteWasInvoked.ShouldBe(true);
            service.OnPerformDeleteCommandValidationWasInvoked.ShouldBe(true);
            service.OnDeleteCommandValidationSuccessWasInvoked.ShouldBe(true);
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
    }

    public class ServiceBaseStub : ServiceBase<Person, long>
    {
        #region Properties

        public bool OnGetBusinessRulesForGetAllWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForGetAllAsyncWasInvoked { get; private set; }
        public bool OnGetAllCommandValidationSuccessWasInvoked { get; private set; }
        public bool OnGetAllCommandInitializationWasInvoked { get; private set; }
        public bool OnGetAllCommandValidationSuccessAsyncWasInvoked { get; private set; }
        public bool OnPerformGetAllCommandValidationWasInvoked { get; private set; }
        public bool OnPerformGetAllCommandValidationAsyncWasInvoked { get; private set; }
        public bool OnGetByIDCommandInitializationWasInvoked { get; private set; }
        public bool OnValidateIdForGetByIDWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForGetByIDWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForGetByIDAsyncWasInvoked { get; private set; }
        public bool OnPerformGetByIDCommandValidationWasInvoked { get; private set; }
        public bool OnPerformGetByIDCommandValidationAsyncWasInvoked { get; private set; }
        public bool OnGetByIDCommandValidationSuccessWasInvoked { get; private set; }
        public bool OnGetByIDCommandValidationSuccessAsyncWasInvoked { get; private set; }
        public bool OnInsertCommandInitializationWasInvoked { get; private set; }
        public bool OnValidateObjectForInsertWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForInsertWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForInsertAsyncWasInvoked { get; private set; }
        public bool OnPerformInsertCommandValidationWasInvoked { get; private set; }
        public bool OnPerformInsertCommandValidationAsyncWasInvoked { get; private set; }
        public bool OnInsertCommandValidationSuccessWasInvoked { get; private set; }
        public bool OnInsertCommandValidationSuccessAsyncWasInvoked { get; private set; }
        public bool OnUpdateCommandInitializationWasInvoked { get; private set; }
        public bool OnValidateObjectForUpdateWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForUpdateWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForUpdateAsyncWasInvoked { get; private set; }
        public bool OnPerformUpdateCommandValidationWasInvoked { get; private set; }
        public bool OnPerformUpdateCommandValidationAsyncWasInvoked { get; private set; }
        public bool OnUpdateCommandValidationSuccessWasInvoked { get; private set; }
        public bool OnUpdateCommandValidationSuccessAsyncWasInvoked { get; private set; }
        public bool OnDeleteCommandInitializationWasInvoked { get; private set; }
        public bool OnValidateIdForDeleteWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForDeleteWasInvoked { get; private set; }
        public bool OnGetBusinessRulesForDeleteAsyncWasInvoked { get; private set; }
        public bool OnPerformDeleteCommandValidationWasInvoked { get; private set; }
        public bool OnPerformDeleteCommandValidationAsyncWasInvoked { get; private set; }
        public bool OnDeleteCommandValidationSuccessWasInvoked { get; private set; }
        public bool OnDeleteCommandValidationSuccessAsyncWasInvoked { get; private set; }
        public bool OnDeleteCommandInitializationAsyncWasInvoked { get; private set; }
        public bool OnUpdateCommandInitializationAsyncWasInvoked { get; private set; }
        public bool OnInsertCommandInitializationAsyncWasInvoked { get; private set; }
        public bool OnGetByIDCommandInitializationAsyncWasInvoked { get; private set; }
        public bool OnGetAllCommandInitializationAsyncWasInvoked { get; private set; }

        #endregion

        public ServiceBaseStub(IDataProxy<Person, long> dataProxy) : base(dataProxy)
        {
        }

        #region GetAll

        protected override void OnGetAllCommandInitialization(ExecutionContext<Person> context)
        {
            OnGetAllCommandInitializationWasInvoked = true;
            base.OnGetAllCommandInitialization(context);
        }

        protected override Task OnGetAllCommandInitializationAsync(ExecutionContext<Person> context)
        {
            OnGetAllCommandInitializationAsyncWasInvoked = true;
            return base.OnGetAllCommandInitializationAsync(context);
        }

        protected override IEnumerable<IRule> OnGetBusinessRulesForGetAll(ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForGetAllWasInvoked = true;
            return base.OnGetBusinessRulesForGetAll(context);
        }

        protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForGetAllAsync(ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForGetAllAsyncWasInvoked = true;
            return base.OnGetBusinessRulesForGetAllAsync(context);
        }

        protected override IEnumerable<ValidationResult> OnPerformGetAllCommandValidation(ExecutionContext<Person> context)
        {
            OnPerformGetAllCommandValidationWasInvoked = true;
            return base.OnPerformGetAllCommandValidation(context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnPerformGetAllCommandValidationAsync(ExecutionContext<Person> context)
        {
            OnPerformGetAllCommandValidationAsyncWasInvoked = true;
            return base.OnPerformGetAllCommandValidationAsync(context);
        }

        protected override IEnumerable<Person> OnGetAllCommandValidationSuccess(ExecutionContext<Person> context)
        {
            OnGetAllCommandValidationSuccessWasInvoked = true;
            return base.OnGetAllCommandValidationSuccess(context);
        }

        protected override Task<IEnumerable<Person>> OnGetAllCommandValidationSuccessAsync(ExecutionContext<Person> context)
        {
            OnGetAllCommandValidationSuccessAsyncWasInvoked = true;
            return base.OnGetAllCommandValidationSuccessAsync(context);
        }

        #endregion

        #region GetByID

        protected override void OnGetByIDCommandInitialization(long id, ExecutionContext<Person> context)
        {
            OnGetByIDCommandInitializationWasInvoked = true;
            base.OnGetByIDCommandInitialization(id, context);
        }

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

        protected override IEnumerable<IRule> OnGetBusinessRulesForGetByID(long id, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForGetByIDWasInvoked = true;
            return base.OnGetBusinessRulesForGetByID(id, context);
        }

        protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForGetByIDAsync(long id, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForGetByIDAsyncWasInvoked = true;
            return base.OnGetBusinessRulesForGetByIDAsync(id, context);
        }

        protected override IEnumerable<ValidationResult> OnPerformGetByIDCommandValidation(long id, ExecutionContext<Person> context)
        {
            OnPerformGetByIDCommandValidationWasInvoked = true;
            return base.OnPerformGetByIDCommandValidation(id, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnPerformGetByIDCommandValidationAsync(long id, ExecutionContext<Person> context)
        {
            OnPerformGetByIDCommandValidationAsyncWasInvoked = true;
            return base.OnPerformGetByIDCommandValidationAsync(id, context);
        }

        protected override Person OnGetByIDCommandValidationSuccess(long id, ExecutionContext<Person> context)
        {
            OnGetByIDCommandValidationSuccessWasInvoked = true;
            return base.OnGetByIDCommandValidationSuccess(id, context);
        }

        protected override Task<Person> OnGetByIDCommandValidationSuccessAsync(long id, ExecutionContext<Person> context)
        {
            OnGetByIDCommandValidationSuccessAsyncWasInvoked = true;
            return base.OnGetByIDCommandValidationSuccessAsync(id, context);
        }

        #endregion

        #region Insert

        protected override void OnInsertCommandInitialization(Person entity, ExecutionContext<Person> context)
        {
            OnInsertCommandInitializationWasInvoked = true;
            base.OnInsertCommandInitialization(entity, context);
        }

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

        protected override IEnumerable<IRule> OnGetBusinessRulesForInsert(Person person, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForInsertWasInvoked = true;
            return base.OnGetBusinessRulesForInsert(person, context);
        }

        protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForInsertAsync(Person person, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForInsertAsyncWasInvoked = true;
            return base.OnGetBusinessRulesForInsertAsync(person, context);
        }

        protected override IEnumerable<ValidationResult> OnPerformInsertCommandValidation(Person person, ExecutionContext<Person> context)
        {
            OnPerformInsertCommandValidationWasInvoked = true;
            return base.OnPerformInsertCommandValidation(person, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnPerformInsertCommandValidationAsync(Person person, ExecutionContext<Person> context)
        {
            OnPerformInsertCommandValidationAsyncWasInvoked = true;
            return base.OnPerformInsertCommandValidationAsync(person, context);
        }

        protected override Person OnInsertCommandValidationSuccess(Person person, ExecutionContext<Person> context)
        {
            OnInsertCommandValidationSuccessWasInvoked = true;
            return base.OnInsertCommandValidationSuccess(person, context);
        }

        protected override Task<Person> OnInsertCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
        {
            OnInsertCommandValidationSuccessAsyncWasInvoked = true;
            return base.OnInsertCommandValidationSuccessAsync(person, context);
        }

        #endregion

        #region Update

        protected override void OnUpdateCommandInitialization(Person entity, ExecutionContext<Person> context)
        {
            OnUpdateCommandInitializationWasInvoked = true;
            base.OnUpdateCommandInitialization(entity, context);
        }

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

        protected override IEnumerable<IRule> OnGetBusinessRulesForUpdate(Person person, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForUpdateWasInvoked = true;
            return base.OnGetBusinessRulesForUpdate(person, context);
        }

        protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForUpdateAsync(Person person, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForUpdateAsyncWasInvoked = true;
            return base.OnGetBusinessRulesForUpdateAsync(person, context);
        }

        protected override IEnumerable<ValidationResult> OnPerformUpdateCommandValidation(Person person, ExecutionContext<Person> context)
        {
            OnPerformUpdateCommandValidationWasInvoked = true;
            return base.OnPerformUpdateCommandValidation(person, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnPerformUpdateCommandValidationAsync(Person person, ExecutionContext<Person> context)
        {
            OnPerformUpdateCommandValidationAsyncWasInvoked = true;
            return base.OnPerformUpdateCommandValidationAsync(person, context);
        }

        protected override Person OnUpdateCommandValidationSuccess(Person person, ExecutionContext<Person> context)
        {
            OnUpdateCommandValidationSuccessWasInvoked = true;
            return base.OnUpdateCommandValidationSuccess(person, context);
        }

        protected override Task<Person> OnUpdateCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
        {
            OnUpdateCommandValidationSuccessAsyncWasInvoked = true;
            return base.OnUpdateCommandValidationSuccessAsync(person, context);
        }

        #endregion

        #region Delete

        protected override void OnDeleteCommandInitialization(long id, ExecutionContext<Person> context)
        {
            OnDeleteCommandInitializationWasInvoked = true;
        }

        protected override IEnumerable<ValidationResult> OnValidateIdForDelete(long id, ExecutionContext<Person> context)
        {
            OnValidateIdForDeleteWasInvoked = true;
            return base.OnValidateIdForDelete(id, context);
        }

        protected override IEnumerable<IRule> OnGetBusinessRulesForDelete(long id, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForDeleteWasInvoked = true;
            return base.OnGetBusinessRulesForDelete(id, context);
        }

        protected override Task<IEnumerable<IRule>> OnGetBusinessRulesForDeleteAsync(long id, ExecutionContext<Person> context)
        {
            OnGetBusinessRulesForDeleteAsyncWasInvoked = true;
            return base.OnGetBusinessRulesForDeleteAsync(id, context);
        }

        protected override IEnumerable<ValidationResult> OnPerformDeleteCommandValidation(long id, ExecutionContext<Person> context)
        {
            OnPerformDeleteCommandValidationWasInvoked = true;
            return base.OnPerformDeleteCommandValidation(id, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnPerformDeleteCommandValidationAsync(long id, ExecutionContext<Person> context)
        {
            OnPerformDeleteCommandValidationAsyncWasInvoked = true;
            return base.OnPerformDeleteCommandValidationAsync(id, context);
        }

        protected override void OnDeleteCommandValidationSuccess(long id, ExecutionContext<Person> context)
        {
            OnDeleteCommandValidationSuccessWasInvoked = true;
            base.OnDeleteCommandValidationSuccess(id, context);
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
        public void Delete(long id) { }

        public Task DeleteAsync(long id)
        {
            Delete(id);
            return Task.FromResult<object>(null);
        }

        public IEnumerable<Person> GetAll()
        {
            return Enumerable.Empty<Person>();
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        public Person GetByID(long id)
        {
            return new Person();
        }

        public Task<Person> GetByIDAsync(long id)
        {
            return Task.FromResult(GetByID(id));
        }

        public Person Insert(Person entity)
        {
            return new Person();
        }

        public Task<Person> InsertAsync(Person entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Person Update(Person entity)
        {
            return new Person();
        }

        public Task<Person> UpdateAsync(Person entity)
        {
            return Task.FromResult(Update(entity));
        }
    }
}
