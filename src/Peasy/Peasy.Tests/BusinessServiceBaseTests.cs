using Peasy.Exception;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Peasy.Tests
{
    public class BusinessServiceBaseTests
    {
        [Fact]
        public void DeleteCommand_Synchronous_Should_ReturnValidation_Result()
        {
            var service = new BusinessServiceBaseMock(new PersonProxyStub());
            var result = service.DeleteCommand(0).Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public async Task DeleteCommand_Asynchronous_Should_Return_Validation_Result()
        {
            var service = new BusinessServiceBaseMock(new PersonProxyStub());
            var result = await service.DeleteCommand(0).ExecuteAsync();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public void GetByIDCommand_Synchronous_Should_Return_Validation_Result()
        {
            var service = new BusinessServiceBaseMock(new PersonProxyStub());
            var result = service.GetByIDCommand(0).Execute();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public async Task GetByIDCommand_Asynchronous_Should_Return_Validation_Result()
        {
            var service = new BusinessServiceBaseMock(new PersonProxyStub());
            var result = await service.GetByIDCommand(0).ExecuteAsync();
            result.Errors.Count().ShouldBe(1);
        }

        [Fact]
        public void Non_Latency_Prone_Should_Not_Invoke_DataProxy_GetByID_On_Update()
        {
            var proxy = new PersonProxyStub(isLatencyProne: false);
            var service = new BusinessServiceBaseMock(proxy);
            service.UpdateCommand(new Person() { ID = 1, Version = "1" }).Execute();
            proxy.GetByIDWasInvoked.ShouldBe(true);
        }

        [Fact]
        public void Latency_Prone_Should_Not_Invoke_DataProxy_GetByID_On_Update()
        {
            var proxy = new PersonProxyStub(isLatencyProne: true);
            var service = new BusinessServiceBaseMock(proxy);
            service.UpdateCommand(new Person()).Execute();
            proxy.GetByIDWasInvoked.ShouldBe(false);
        }

        [Fact]
        public void UpdateCommand_Throws_DomainNotFoundException()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);

            Should.Throw<DomainObjectNotFoundException>(() => service.UpdateCommand(new Person()).Execute());
        }

        [Fact]
        public void UpdateCommand_Throws_ConcurrencyException()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);

            Should.Throw<ConcurrencyException>(() => service.UpdateCommand(new Person() { ID = 1, Version = "2" }).Execute());
        }

        [Fact]
        public void UpdateCommand_Reverts_Non_Editable_Values()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);
            var result = service.UpdateCommand(new Person() { ID = 1, Name = "Frank Zappa", Version = "1" }).Execute();
            result.Value.Name.ShouldBe("George Harrison");
        }

        [Fact]
        public void UpdateCommand_Reverts_PeasyForeignKey_Values()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);
            var result = service.UpdateCommand(new Person() { ID = 1, ForeignKeyID = 0, Version = "1" }).Execute();
            result.Value.ForeignKeyID.ShouldBe(null);
        }

        [Fact]
        public void DataProxy_Update_Should_Be_Invoked()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);
            var result = service.UpdateCommand(new Person() { ID = 1, ForeignKeyID = 0, Version = "1" }).Execute();
            proxy.UpdateWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Non_Latency_Prone_Should_Not_Invoke_DataProxy_GetByID_On_UpdateAsync()
        {
            var proxy = new PersonProxyStub(isLatencyProne: false);
            var service = new BusinessServiceBaseMock(proxy);
            await service.UpdateCommand(new Person() { ID = 1, Version = "1" }).ExecuteAsync();
            proxy.GetByIDAsyncWasInvoked.ShouldBe(true);
        }

        [Fact]
        public async Task Latency_Prone_Should_Not_Invoke_DataProxy_GetByID_On_UpdateAsync()
        {
            var proxy = new PersonProxyStub(isLatencyProne: true);
            var service = new BusinessServiceBaseMock(proxy);
            await service.UpdateCommand(new Person()).ExecuteAsync();
            proxy.GetByIDAsyncWasInvoked.ShouldBe(false);
        }

        [Fact]
        public async Task UpdateCommandAsync_Throws_DomainNotFoundException()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);

            await Should.ThrowAsync<DomainObjectNotFoundException>(() => service.UpdateCommand(new Person()).ExecuteAsync());
        }

        [Fact]
        public async Task UpdateCommandAsync_Throws_ConcurrencyException()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);

            await Should.ThrowAsync<ConcurrencyException>(() => service.UpdateCommand(new Person() { ID = 1, Version = "2" }).ExecuteAsync());
        }

        [Fact]
        public async Task UpdateCommandAsync_Reverts_NonEditable_Values()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);
            var result = await service.UpdateCommand(new Person() { ID = 1, Name = "Frank Zappa", Version = "1" }).ExecuteAsync();
            result.Value.Name.ShouldBe("George Harrison");
        }

        [Fact]
        public async Task UpdateCommandAsync_Reverts_PeasyForeignKey_Values()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);
            var result = await service.UpdateCommand(new Person() { ID = 1, ForeignKeyID = 0, Version = "1" }).ExecuteAsync();
            result.Value.ForeignKeyID.ShouldBe(null);
        }

        [Fact]
        public async Task DataProxy_UpdateAsync_Should_Be_Invoked()
        {
            var proxy = new PersonProxyStub();
            var service = new BusinessServiceBaseMock(proxy);
            var result = await service.UpdateCommand(new Person() { ID = 1, ForeignKeyID = 0, Version = "1" }).ExecuteAsync();
            proxy.UpdateAsyncWasInvoked.ShouldBe(true);
        }
    }

    public class BusinessServiceBaseMock : BusinessServiceBase<Person, long>
    {
        public BusinessServiceBaseMock(IServiceDataProxy<Person, long> dataProxy) : base(dataProxy)
        {
        }
    }

    public class PersonProxyStub : IServiceDataProxy<Person, long>
    {
        public bool GetByIDWasInvoked;
        public bool GetByIDAsyncWasInvoked;
        public bool UpdateWasInvoked;
        public bool UpdateAsyncWasInvoked;

        public PersonProxyStub(bool isLatencyProne = false, bool supportTransactions = true)
        {
            IsLatencyProne = isLatencyProne;
            SupportsTransactions = supportTransactions;
        }

        public bool IsLatencyProne
        {
            get; private set;
        }

        public bool SupportsTransactions
        {
            get; private set;
        }

        public void Delete(long id) { }

        public async Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Person GetByID(long id)
        {
            GetByIDWasInvoked = true;
            if (id == 1)
                return new Person() { ID = 1, Name = "George Harrison", Version = "1" };

            return null;
        }

        public async Task<Person> GetByIDAsync(long id)
        {
            GetByIDAsyncWasInvoked = true;
            if (id == 1)
                return new Person() { ID = 1, Name = "George Harrison", Version = "1" };

            return null;
        }

        public Person Insert(Person entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Person> InsertAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Person Update(Person entity)
        {
            UpdateWasInvoked = true;
            return new Person() { Name = entity.Name };
        }

        public async Task<Person> UpdateAsync(Person entity)
        {
            UpdateAsyncWasInvoked = true;
            return new Person() { Name = entity.Name };
        }
    }

    public class Person : IDomainObject<long>, IVersionContainer
    {
        public long ID { get; set; }

        public string Version { get; set; }

        [Editable(false)]
        public string Name { get; set; }

        [PeasyForeignKey]
        public int? ForeignKeyID { get; set; }
    }
}
