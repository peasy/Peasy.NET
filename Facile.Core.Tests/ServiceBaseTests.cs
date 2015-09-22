using Facile.Core;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facile.Core.Tests
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
    }

    public class ServiceBaseMock : ServiceBase<Person, long>
    {
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
