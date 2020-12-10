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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
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
            var service = new ServiceBaseMock(new PersonProxyStub());
            await service.DeleteCommand(1).ExecuteAsync();
            service.OnDeleteCommandInitializationAsyncWasInvoked.ShouldBe(true);
            service.GetValidationResultsForDeleteWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForDeleteAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForDeleteAsyncWasInvoked.ShouldBe(true);
            service.DeleteAsyncWasInvoked.ShouldBe(true);
        }
    }

    public class ServiceBaseMock : ServiceBase<Person, long>
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

        public ServiceBaseMock(IDataProxy<Person, long> dataProxy) : base(dataProxy)
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

        protected override IEnumerable<ValidationResult> GetAllErrorsForGetAll(ExecutionContext<Person> context)
        {
            GetAllErrorsForGetAllWasInvoked = true;
            return base.GetAllErrorsForGetAll(context);
        }

        protected override Task<IEnumerable<ValidationResult>> GetAllErrorsForGetAllAsync(ExecutionContext<Person> context)
        {
            GetAllErrorsForGetAllAsyncWasInvoked = true;
            return base.GetAllErrorsForGetAllAsync(context);
        }

        protected override IEnumerable<Person> GetAll(ExecutionContext<Person> context)
        {
            GetAllWasInvoked = true;
            return base.GetAll(context);
        }

        protected override Task<IEnumerable<Person>> GetAllAsync(ExecutionContext<Person> context)
        {
            GetAllAsyncWasInvoked = true;
            return base.GetAllAsync(context);
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

        protected override IEnumerable<ValidationResult> GetAllErrorsForGetByID(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForGetByIDWasInvoked = true;
            return base.GetAllErrorsForGetByID(id, context);
        }

        protected override Task<IEnumerable<ValidationResult>> GetAllErrorsForGetByIDAsync(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForGetByIDAsyncWasInvoked = true;
            return base.GetAllErrorsForGetByIDAsync(id, context);
        }

        protected override Person GetByID(long id, ExecutionContext<Person> context)
        {
            GetByIDWasInvoked = true;
            return base.GetByID(id, context);
        }

        protected override Task<Person> GetByIDAsync(long id, ExecutionContext<Person> context)
        {
            GetByIDAsyncWasInvoked = true;
            return base.GetByIDAsync(id, context);
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

        protected override IEnumerable<ValidationResult> GetAllErrorsForInsert(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForInsertWasInvoked = true;
            return base.GetAllErrorsForInsert(person, context);
        }

        protected override Task<IEnumerable<ValidationResult>> GetAllErrorsForInsertAsync(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForInsertAsyncWasInvoked = true;
            return base.GetAllErrorsForInsertAsync(person, context);
        }

        protected override Person Insert(Person person, ExecutionContext<Person> context)
        {
            InsertWasInvoked = true;
            return base.Insert(person, context);
        }

        protected override Task<Person> InsertAsync(Person person, ExecutionContext<Person> context)
        {
            InsertAsyncWasInvoked = true;
            return base.InsertAsync(person, context);
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

        protected override IEnumerable<ValidationResult> GetValidationResultsForUpdate(Person person, ExecutionContext<Person> context)
        {
            GetValidationResultsForUpdateWasInvoked = true;
            return base.GetValidationResultsForUpdate(person, context);
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

        protected override IEnumerable<ValidationResult> GetAllErrorsForUpdate(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForUpdateWasInvoked = true;
            return base.GetAllErrorsForUpdate(person, context);
        }

        protected override Task<IEnumerable<ValidationResult>> GetAllErrorsForUpdateAsync(Person person, ExecutionContext<Person> context)
        {
            GetAllErrorsForUpdateAsyncWasInvoked = true;
            return base.GetAllErrorsForUpdateAsync(person, context);
        }

        protected override Person Update(Person person, ExecutionContext<Person> context)
        {
            UpdateWasInvoked = true;
            return base.Update(person, context);
        }

        protected override Task<Person> UpdateAsync(Person person, ExecutionContext<Person> context)
        {
            UpdateAsyncWasInvoked = true;
            return base.UpdateAsync(person, context);
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

        protected override IEnumerable<ValidationResult> GetAllErrorsForDelete(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForDeleteWasInvoked = true;
            return base.GetAllErrorsForDelete(id, context);
        }

        protected override Task<IEnumerable<ValidationResult>> GetAllErrorsForDeleteAsync(long id, ExecutionContext<Person> context)
        {
            GetAllErrorsForDeleteAsyncWasInvoked = true;
            return base.GetAllErrorsForDeleteAsync(id, context);
        }

        protected override void Delete(long id, ExecutionContext<Person> context)
        {
            DeleteWasInvoked = true;
            base.Delete(id, context);
        }

        protected override Task DeleteAsync(long id, ExecutionContext<Person> context)
        {
            DeleteAsyncWasInvoked = true;
            return base.DeleteAsync(id, context);
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
