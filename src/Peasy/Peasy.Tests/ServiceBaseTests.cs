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
            service.GetValidationResultsForGetAllWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetAllWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetAllWasInvoked.ShouldBe(true);
            service.GetAllWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_GetAllCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.GetAllCommand().ExecuteAsync();
            service.OnGetAllCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetAllWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetAllAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetAllAsyncWasInvoked.ShouldBe(true);
            service.GetAllAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Ensure_Method_Invocations_For_GetByIDCommand()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.GetByIDCommand(1).Execute();
            service.OnGetByIDCommandInitializationWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetByIDWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetByIDWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetByIDWasInvoked.ShouldBe(true);
            service.GetByIDWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_GetByIDCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.GetByIDCommand(1).ExecuteAsync();
            service.OnGetByIDCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetByIDWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetByIDAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetByIDAsyncWasInvoked.ShouldBe(true);
            service.GetByIDAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Ensure_Method_Invocations_For_InsertCommand()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.InsertCommand(new Person()).Execute();
            service.OnInsertCommandInitializationWasInvoked.ShouldBe(true);
            service.GetValidationResultsForInsertWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForInsertWasInvoked.ShouldBe(true);
            service.GetAllErrorsForInsertWasInvoked.ShouldBe(true);
            service.InsertWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_InsertCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.InsertCommand(new Person()).ExecuteAsync();
            service.OnInsertCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.GetValidationResultsForInsertWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForInsertAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForInsertAsyncWasInvoked.ShouldBe(true);
            service.InsertAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Ensure_Method_Invocations_For_Update()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.UpdateCommand(new Person()).Execute();
            service.OnUpdateCommandInitializationWasInvoked.ShouldBe(true);
            service.GetValidationResultsForUpdateWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForUpdateWasInvoked.ShouldBe(true);
            service.GetAllErrorsForUpdateWasInvoked.ShouldBe(true);
            service.UpdateWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_UpdateCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.UpdateCommand(new Person()).ExecuteAsync();
            service.OnUpdateCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.GetValidationResultsForUpdateWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForUpdateAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForUpdateAsyncWasInvoked.ShouldBe(true);
            service.UpdateAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Ensure_Method_Invocations_For_Delete()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var result = service.DeleteCommand(1).Execute();
            service.OnDeleteCommandInitializationWasInvoked.ShouldBe(true);
            service.GetValidationResultsForDeleteWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForDeleteWasInvoked.ShouldBe(true);
            service.GetAllErrorsForDeleteWasInvoked.ShouldBe(true);
            service.DeleteWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Ensure_Method_Invocations_For_DeleteCommandAsync()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            await service.DeleteCommand(1).ExecuteAsync();
            service.OnDeleteCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.GetValidationResultsForDeleteWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForDeleteAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForDeleteAsyncWasInvoked.ShouldBe(true);
            service.DeleteAsyncWasInvoked.ShouldBe(true);
        }
    }

    public class ServiceBaseStub : ServiceBase<Person, long>
    {
        #region Properties

        public bool GetValidationResultsForGetAllWasInvoked { get; private set; }
        public bool GetBusinessRulesForGetAllWasInvoked { get; private set; }
        public bool GetBusinessRulesForGetAllAsyncWasInvoked { get; private set; }
        public bool GetAllWasInvoked { get; private set; }
        public bool OnGetAllCommandInitializationWasInvoked { get; private set; }
        public bool GetAllAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForGetAllWasInvoked { get; private set; }
        public bool GetAllErrorsForGetAllAsyncWasInvoked { get; private set; }
        public bool OnGetByIDCommandInitializationWasInvoked { get; private set; }
        public bool GetValidationResultsForGetByIDWasInvoked { get; private set; }
        public bool GetBusinessRulesForGetByIDWasInvoked { get; private set; }
        public bool GetBusinessRulesForGetByIDAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForGetByIDWasInvoked { get; private set; }
        public bool GetAllErrorsForGetByIDAsyncWasInvoked { get; private set; }
        public bool GetByIDWasInvoked { get; private set; }
        public bool GetByIDAsyncWasInvoked { get; private set; }
        public bool OnInsertCommandInitializationWasInvoked { get; private set; }
        public bool GetValidationResultsForInsertWasInvoked { get; private set; }
        public bool GetBusinessRulesForInsertWasInvoked { get; private set; }
        public bool GetBusinessRulesForInsertAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForInsertWasInvoked { get; private set; }
        public bool GetAllErrorsForInsertAsyncWasInvoked { get; private set; }
        public bool InsertWasInvoked { get; private set; }
        public bool InsertAsyncWasInvoked { get; private set; }
        public bool OnUpdateCommandInitializationWasInvoked { get; private set; }
        public bool GetValidationResultsForUpdateWasInvoked { get; private set; }
        public bool GetBusinessRulesForUpdateWasInvoked { get; private set; }
        public bool GetBusinessRulesForUpdateAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForUpdateWasInvoked { get; private set; }
        public bool GetAllErrorsForUpdateAsyncWasInvoked { get; private set; }
        public bool UpdateWasInvoked { get; private set; }
        public bool UpdateAsyncWasInvoked { get; private set; }
        public bool OnDeleteCommandInitializationWasInvoked { get; private set; }
        public bool GetValidationResultsForDeleteWasInvoked { get; private set; }
        public bool GetBusinessRulesForDeleteWasInvoked { get; private set; }
        public bool GetBusinessRulesForDeleteAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForDeleteWasInvoked { get; private set; }
        public bool GetAllErrorsForDeleteAsyncWasInvoked { get; private set; }
        public bool DeleteWasInvoked { get; private set; }
        public bool DeleteAsyncWasInvoked { get; private set; }
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

        protected override IEnumerable<ValidationResult> GetValidationResultsForGetAll(ExecutionContext<Person> context)
        {
            GetValidationResultsForGetAllWasInvoked = true;
            return base.GetValidationResultsForGetAll(context);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForGetAll(ExecutionContext<Person> context)
        {
            GetBusinessRulesForGetAllWasInvoked = true;
            return base.GetBusinessRulesForGetAll(context);
        }

        protected override Task<IEnumerable<IRule>> GetBusinessRulesForGetAllAsync(ExecutionContext<Person> context)
        {
            GetBusinessRulesForGetAllAsyncWasInvoked = true;
            return base.GetBusinessRulesForGetAllAsync(context);
        }

        protected override IEnumerable<ValidationResult> OnGetAllCommandValidation(ExecutionContext<Person> context)
        {
            GetAllErrorsForGetAllWasInvoked = true;
            return base.OnGetAllCommandValidation(context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnGetAllCommandValidationAsync(ExecutionContext<Person> context)
        {
            GetAllErrorsForGetAllAsyncWasInvoked = true;
            return base.OnGetAllCommandValidationAsync(context);
        }

        protected override IEnumerable<Person> OnGetAllCommandValidationSuccess(ExecutionContext<Person> context)
        {
            GetAllWasInvoked = true;
            return base.OnGetAllCommandValidationSuccess(context);
        }

        protected override Task<IEnumerable<Person>> OnGetAllCommandValidationSuccessAsync(ExecutionContext<Person> context)
        {
            GetAllAsyncWasInvoked = true;
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

        protected override IEnumerable<ValidationResult> GetValidationResultsForGetByID(long id, ExecutionContext<Person> context)
        {
            GetValidationResultsForGetByIDWasInvoked = true;
            return base.GetValidationResultsForGetByID(id, context);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForGetByID(long id, ExecutionContext<Person> context)
        {
            GetBusinessRulesForGetByIDWasInvoked = true;
            return base.GetBusinessRulesForGetByID(id, context);
        }

        protected override Task<IEnumerable<IRule>> GetBusinessRulesForGetByIDAsync(long id, ExecutionContext<Person> context)
        {
            GetBusinessRulesForGetByIDAsyncWasInvoked = true;
            return base.GetBusinessRulesForGetByIDAsync(id, context);
        }

        protected override IEnumerable<ValidationResult> OnGetByIDCommandValidation(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForGetByIDWasInvoked = true;
            return base.OnGetByIDCommandValidation(id, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnGetByIDCommandValidationAsync(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForGetByIDAsyncWasInvoked = true;
            return base.OnGetByIDCommandValidationAsync(id, context);
        }

        protected override Person OnGetByIDCommandValidationSuccess(long id, ExecutionContext<Person> context)
        {
            GetByIDWasInvoked = true;
            return base.OnGetByIDCommandValidationSuccess(id, context);
        }

        protected override Task<Person> OnGetByIDCommandValidationSuccessAsync(long id, ExecutionContext<Person> context)
        {
            GetByIDAsyncWasInvoked = true;
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

        protected override IEnumerable<ValidationResult> GetValidationResultsForInsert(Person person, ExecutionContext<Person> context)
        {
            GetValidationResultsForInsertWasInvoked = true;
            return base.GetValidationResultsForInsert(person, context);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(Person person, ExecutionContext<Person> context)
        {
            GetBusinessRulesForInsertWasInvoked = true;
            return base.GetBusinessRulesForInsert(person, context);
        }

        protected override Task<IEnumerable<IRule>> GetBusinessRulesForInsertAsync(Person person, ExecutionContext<Person> context)
        {
            GetBusinessRulesForInsertAsyncWasInvoked = true;
            return base.GetBusinessRulesForInsertAsync(person, context);
        }

        protected override IEnumerable<ValidationResult> OnInsertCommandValidation(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForInsertWasInvoked = true;
            return base.OnInsertCommandValidation(person, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnInsertCommandValidationAsync(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForInsertAsyncWasInvoked = true;
            return base.OnInsertCommandValidationAsync(person, context);
        }

        protected override Person OnInsertCommandValidationSuccess(Person person, ExecutionContext<Person> context)
        {
            InsertWasInvoked = true;
            return base.OnInsertCommandValidationSuccess(person, context);
        }

        protected override Task<Person> OnInsertCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
        {
            InsertAsyncWasInvoked = true;
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

        protected override IEnumerable<ValidationResult> GetValidationResultsForUpdate(Person resource, ExecutionContext<Person> context)
        {
            GetValidationResultsForUpdateWasInvoked = true;
            return base.GetValidationResultsForUpdate(resource, context);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForUpdate(Person person, ExecutionContext<Person> context)
        {
            GetBusinessRulesForUpdateWasInvoked = true;
            return base.GetBusinessRulesForUpdate(person, context);
        }

        protected override Task<IEnumerable<IRule>> GetBusinessRulesForUpdateAsync(Person person, ExecutionContext<Person> context)
        {
            GetBusinessRulesForUpdateAsyncWasInvoked = true;
            return base.GetBusinessRulesForUpdateAsync(person, context);
        }

        protected override IEnumerable<ValidationResult> OnUpdateCommandValidation(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForUpdateWasInvoked = true;
            return base.OnUpdateCommandValidation(person, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnUpdateCommandValidationAsync(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForUpdateAsyncWasInvoked = true;
            return base.OnUpdateCommandValidationAsync(person, context);
        }

        protected override Person OnUpdateCommandValidationSuccess(Person person, ExecutionContext<Person> context)
        {
            UpdateWasInvoked = true;
            return base.OnUpdateCommandValidationSuccess(person, context);
        }

        protected override Task<Person> OnUpdateCommandValidationSuccessAsync(Person person, ExecutionContext<Person> context)
        {
            UpdateAsyncWasInvoked = true;
            return base.OnUpdateCommandValidationSuccessAsync(person, context);
        }

        #endregion

        #region Delete

        protected override void OnDeleteCommandInitialization(long id, ExecutionContext<Person> context)
        {
            OnDeleteCommandInitializationWasInvoked = true;
        }

        protected override IEnumerable<ValidationResult> GetValidationResultsForDelete(long id, ExecutionContext<Person> context)
        {
            GetValidationResultsForDeleteWasInvoked = true;
            return base.GetValidationResultsForDelete(id, context);
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id, ExecutionContext<Person> context)
        {
            GetBusinessRulesForDeleteWasInvoked = true;
            return base.GetBusinessRulesForDelete(id, context);
        }

        protected override Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(long id, ExecutionContext<Person> context)
        {
            GetBusinessRulesForDeleteAsyncWasInvoked = true;
            return base.GetBusinessRulesForDeleteAsync(id, context);
        }

        protected override IEnumerable<ValidationResult> OnDeleteCommandValidation(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForDeleteWasInvoked = true;
            return base.OnDeleteCommandValidation(id, context);
        }

        protected override Task<IEnumerable<ValidationResult>> OnDeleteCommandValidationAsync(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForDeleteAsyncWasInvoked = true;
            return base.OnDeleteCommandValidationAsync(id, context);
        }

        protected override void OnDeleteCommandValidationSuccess(long id, ExecutionContext<Person> context)
        {
            DeleteWasInvoked = true;
            base.OnDeleteCommandValidationSuccess(id, context);
        }

        protected override Task OnDeleteCommandValidationSuccessAsync(long id, ExecutionContext<Person> context)
        {
            DeleteAsyncWasInvoked = true;
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
