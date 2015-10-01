using Peasy.Core;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Peasy.Core.Tests
{
    [TestClass]
    public class ServiceBaseTests
    {
        [TestMethod]
        public void EnsureMethodInvocationsForGetAllCommand()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            var result = service.GetAllCommand().Execute();
            service.OnBeforeGetAllCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetAllWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetAllWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetAllWasInvoked.ShouldBe(true);
            service.GetAllWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task EnsureMethodInvocationsForGetAllCommandAsync()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            await service.GetAllCommand().ExecuteAsync();
            service.OnBeforeGetAllCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetAllWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetAllAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetAllAsyncWasInvoked.ShouldBe(true);
            service.GetAllAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void EnsureMethodInvocationsForGetByIDCommand()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            var result = service.GetByIDCommand(1).Execute();
            service.OnBeforeGetByIDCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetByIDWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetByIDWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetByIDWasInvoked.ShouldBe(true);
            service.GetByIDWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task EnsureMethodInvocationsForGetByIDCommandAsync()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            await service.GetByIDCommand(1).ExecuteAsync();
            service.OnBeforeGetByIDCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForGetByIDWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForGetByIDAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForGetByIDAsyncWasInvoked.ShouldBe(true);
            service.GetByIDAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void EnsureMethodInvocationsForInsertCommand()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            var result = service.InsertCommand(new Person()).Execute();
            service.OnBeforeInsertCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForInsertWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForInsertWasInvoked.ShouldBe(true);
            service.GetAllErrorsForInsertWasInvoked.ShouldBe(true);
            service.InsertWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task EnsureMethodInvocationsForInsertCommandAsync()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            await service.InsertCommand(new Person()).ExecuteAsync();
            service.OnBeforeInsertCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForInsertWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForInsertAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForInsertAsyncWasInvoked.ShouldBe(true);
            service.InsertAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void EnsureMethodInvocationsForUpdate()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            var result = service.UpdateCommand(new Person()).Execute();
            service.OnBeforeUpdateCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForUpdateWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForUpdateWasInvoked.ShouldBe(true);
            service.GetAllErrorsForUpdateWasInvoked.ShouldBe(true);
            service.UpdateWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task EnsureMethodInvocationsForUpdateCommandAsync()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            await service.UpdateCommand(new Person()).ExecuteAsync();
            service.OnBeforeUpdateCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForUpdateWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForUpdateAsyncWasInvoked.ShouldBe(true);
            service.GetAllErrorsForUpdateAsyncWasInvoked.ShouldBe(true);
            service.UpdateAsyncWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public void EnsureMethodInvocationsForDelete()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            var result = service.DeleteCommand(1).Execute();
            service.OnBeforeDeleteCommandExecutedWasInvoked.ShouldBe(true);
            service.GetValidationResultsForDeleteWasInvoked.ShouldBe(true);
            service.GetBusinessRulesForDeleteWasInvoked.ShouldBe(true);
            service.GetAllErrorsForDeleteWasInvoked.ShouldBe(true);
            service.DeleteWasInvoked.ShouldBe(true);
        }

        [TestMethod]
        public async Task EnsureMethodInvocationsForDeleteCommandAsync()
        {
            var service = new ServiceBaseMock(new PersonProxyStub());
            await service.DeleteCommand(1).ExecuteAsync();
            service.OnBeforeDeleteCommandExecutedWasInvoked.ShouldBe(true);
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
        public bool OnBeforeGetAllCommandExecutedWasInvoked { get; private set; }
        public bool GetAllAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForGetAllWasInvoked { get; private set; }
        public bool GetAllErrorsForGetAllAsyncWasInvoked { get; private set; }
        public bool OnBeforeGetByIDCommandExecutedWasInvoked { get; private set; }
        public bool GetValidationResultsForGetByIDWasInvoked { get; private set; }
        public bool GetBusinessRulesForGetByIDWasInvoked { get; private set; }
        public bool GetBusinessRulesForGetByIDAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForGetByIDWasInvoked { get; private set; }
        public bool GetAllErrorsForGetByIDAsyncWasInvoked { get; private set; }
        public bool GetByIDWasInvoked { get; private set; }
        public bool GetByIDAsyncWasInvoked { get; private set; }
        public bool OnBeforeInsertCommandExecutedWasInvoked { get; private set; }
        public bool GetValidationResultsForInsertWasInvoked { get; private set; }
        public bool GetBusinessRulesForInsertWasInvoked { get; private set; }
        public bool GetBusinessRulesForInsertAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForInsertWasInvoked { get; private set; }
        public bool GetAllErrorsForInsertAsyncWasInvoked { get; private set; }
        public bool InsertWasInvoked { get; private set; }
        public bool InsertAsyncWasInvoked { get; private set; }
        public bool OnBeforeUpdateCommandExecutedWasInvoked { get; private set; }
        public bool GetValidationResultsForUpdateWasInvoked { get; private set; }
        public bool GetBusinessRulesForUpdateWasInvoked { get; private set; }
        public bool GetBusinessRulesForUpdateAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForUpdateWasInvoked { get; private set; }
        public bool GetAllErrorsForUpdateAsyncWasInvoked { get; private set; }
        public bool UpdateWasInvoked { get; private set; }
        public bool UpdateAsyncWasInvoked { get; private set; }
        public bool OnBeforeDeleteCommandExecutedWasInvoked { get; private set; }
        public bool GetValidationResultsForDeleteWasInvoked { get; private set; }
        public bool GetBusinessRulesForDeleteWasInvoked { get; private set; }
        public bool GetBusinessRulesForDeleteAsyncWasInvoked { get; private set; }
        public bool GetAllErrorsForDeleteWasInvoked { get; private set; }
        public bool GetAllErrorsForDeleteAsyncWasInvoked { get; private set; }
        public bool DeleteWasInvoked { get; private set; }
        public bool DeleteAsyncWasInvoked { get; private set; }

        #endregion

        public ServiceBaseMock(IDataProxy<Person, long> dataProxy) : base(dataProxy)
        {
        }

        #region GetAll

        protected override void OnBeforeGetAllCommandExecuted(ExecutionContext<Person> context)
        {
            OnBeforeGetAllCommandExecutedWasInvoked = true;
            base.OnBeforeGetAllCommandExecuted(context);
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

        protected override void OnBeforeGetByIDCommandExecuted(long id, ExecutionContext<Person> context)
        {
            OnBeforeGetByIDCommandExecutedWasInvoked = true;
            base.OnBeforeGetByIDCommandExecuted(id, context);
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

        protected override void OnBeforeInsertCommandExecuted(Person person, ExecutionContext<Person> context)
        {
            OnBeforeInsertCommandExecutedWasInvoked = true;
            base.OnBeforeInsertCommandExecuted(person, context);
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

        protected override void OnBeforeUpdateCommandExecuted(Person person, ExecutionContext<Person> context)
        {
            OnBeforeUpdateCommandExecutedWasInvoked = true;
            base.OnBeforeUpdateCommandExecuted(person, context);
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

        protected override void OnBeforeDeleteCommandExecuted(long id, ExecutionContext<Person> context)
        {
            OnBeforeDeleteCommandExecutedWasInvoked = true;
            base.OnBeforeDeleteCommandExecuted(id, context);
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

        #endregion
    }

    public class PersonProxyStub : IDataProxy<Person, long>
    {
        public void Delete(long id) { }

        public Task DeleteAsync(long id)
        {
            return Task.Run(() => Delete(id));
        }

        public IEnumerable<Person> GetAll()
        {
            return Enumerable.Empty<Person>();
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Person GetByID(long id)
        {
            return new Person();
        }

        public Task<Person> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Person Insert(Person entity)
        {
            return new Person();
        }

        public Task<Person> InsertAsync(Person entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Person Update(Person entity)
        {
            return new Person();
        }

        public Task<Person> UpdateAsync(Person entity)
        {
            return Task.Run(() => Update(entity));
        }
    }
}
