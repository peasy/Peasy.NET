using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Peasy.Synchronous;
using Peasy.Extensions;

namespace Peasy.Core.Tests
{
    public class SynchronousServiceBaseTests
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
        public void GetAllCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var validationResult = service.GetAllCommand().Validate();
            var executionResult = validationResult.CompleteCommandExecution();
            executionResult.Success.ShouldBeTrue();
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
        public void GetByIDCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var validatonResult = service.GetByIDCommand(123).Validate();
            var executionResult = validatonResult.CompleteCommandExecution();
            executionResult.Success.ShouldBeTrue();
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
        public void InsertCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var data = new Person();
            var validatonResult = service.InsertCommand(data).Validate();
            var executionResult = validatonResult.CompleteCommandExecution();
            executionResult.Success.ShouldBeTrue();
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
        public void UpdateCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var data = new Person();
            var validatonResult = service.UpdateCommand(data).Validate();
            var executionResult = validatonResult.CompleteCommandExecution();
            executionResult.Success.ShouldBeTrue();
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
        public void DeleteCommand_Properly_Supports_ISupportValidation()
        {
            var service = new ServiceBaseStub(new PersonProxyStub());
            var validatonResult = service.DeleteCommand(123).Validate();
            var executionResult = validatonResult.CompleteCommandExecution();
            executionResult.Success.ShouldBeTrue();
        }

        public class ServiceBaseStub : SynchronousServiceBase<Person, long>
        {
            #region Properties

            public bool OnGetBusinessRulesForGetAllWasInvoked { get; private set; }
            public bool OnGetAllCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnGetAllCommandInitializationWasInvoked { get; private set; }
            public bool OnPerformGetAllCommandValidationWasInvoked { get; private set; }
            public bool OnGetByIDCommandInitializationWasInvoked { get; private set; }
            public bool OnValidateIdForGetByIDWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForGetByIDWasInvoked { get; private set; }
            public bool OnPerformGetByIDCommandValidationWasInvoked { get; private set; }
            public bool OnGetByIDCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnInsertCommandInitializationWasInvoked { get; private set; }
            public bool OnValidateObjectForInsertWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForInsertWasInvoked { get; private set; }
            public bool OnPerformInsertCommandValidationWasInvoked { get; private set; }
            public bool OnInsertCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnUpdateCommandInitializationWasInvoked { get; private set; }
            public bool OnValidateObjectForUpdateWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForUpdateWasInvoked { get; private set; }
            public bool OnPerformUpdateCommandValidationWasInvoked { get; private set; }
            public bool OnUpdateCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnDeleteCommandInitializationWasInvoked { get; private set; }
            public bool OnValidateIdForDeleteWasInvoked { get; private set; }
            public bool OnGetBusinessRulesForDeleteWasInvoked { get; private set; }
            public bool OnPerformDeleteCommandValidationWasInvoked { get; private set; }
            public bool OnDeleteCommandValidationSuccessWasInvoked { get; private set; }

            #endregion

            public ServiceBaseStub(ISynchronousDataProxy<Person, long> dataProxy) : base(dataProxy)
            {
            }

            #region GetAll

            protected override void OnGetAllCommandInitialization(ExecutionContext<Person> context)
            {
                OnGetAllCommandInitializationWasInvoked = true;
                base.OnGetAllCommandInitialization(context);
            }
            protected override IEnumerable<ISynchronousRule> OnGetBusinessRulesForGetAll(ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForGetAllWasInvoked = true;
                return base.OnGetBusinessRulesForGetAll(context);
            }

            protected override IEnumerable<ValidationResult> OnPerformGetAllCommandValidation(ExecutionContext<Person> context)
            {
                OnPerformGetAllCommandValidationWasInvoked = true;
                return base.OnPerformGetAllCommandValidation(context);
            }

            protected override IEnumerable<Person> OnGetAllCommandValidationSuccess(ExecutionContext<Person> context)
            {
                OnGetAllCommandValidationSuccessWasInvoked = true;
                return base.OnGetAllCommandValidationSuccess(context);
            }

            #endregion

            #region GetByID

            protected override void OnGetByIDCommandInitialization(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandInitializationWasInvoked = true;
                base.OnGetByIDCommandInitialization(id, context);
            }

            protected override IEnumerable<ValidationResult> OnValidateIdForGetByID(long id, ExecutionContext<Person> context)
            {
                OnValidateIdForGetByIDWasInvoked = true;
                return base.OnValidateIdForGetByID(id, context);
            }

            protected override IEnumerable<ISynchronousRule> OnGetBusinessRulesForGetByID(long id, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForGetByIDWasInvoked = true;
                return base.OnGetBusinessRulesForGetByID(id, context);
            }

            protected override IEnumerable<ValidationResult> OnPerformGetByIDCommandValidation(long id, ExecutionContext<Person> context)
            {
                OnPerformGetByIDCommandValidationWasInvoked = true;
                return base.OnPerformGetByIDCommandValidation(id, context);
            }

            protected override Person OnGetByIDCommandValidationSuccess(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandValidationSuccessWasInvoked = true;
                return base.OnGetByIDCommandValidationSuccess(id, context);
            }

            #endregion

            #region Insert

            protected override void OnInsertCommandInitialization(Person entity, ExecutionContext<Person> context)
            {
                OnInsertCommandInitializationWasInvoked = true;
                base.OnInsertCommandInitialization(entity, context);
            }

            protected override IEnumerable<ValidationResult> OnValidateObjectForInsert(Person resource, ExecutionContext<Person> context)
            {
                OnValidateObjectForInsertWasInvoked = true;
                return base.OnValidateObjectForInsert(resource, context);
            }

            protected override IEnumerable<ISynchronousRule> OnGetBusinessRulesForInsert(Person resource, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForInsertWasInvoked = true;
                return base.OnGetBusinessRulesForInsert(resource, context);
            }

            protected override IEnumerable<ValidationResult> OnPerformInsertCommandValidation(Person person, ExecutionContext<Person> context)
            {
                OnPerformInsertCommandValidationWasInvoked = true;
                return base.OnPerformInsertCommandValidation(person, context);
            }

            protected override Person OnInsertCommandValidationSuccess(Person person, ExecutionContext<Person> context)
            {
                OnInsertCommandValidationSuccessWasInvoked = true;
                return base.OnInsertCommandValidationSuccess(person, context);
            }

            #endregion

            #region Update

            protected override void OnUpdateCommandInitialization(Person entity, ExecutionContext<Person> context)
            {
                OnUpdateCommandInitializationWasInvoked = true;
                base.OnUpdateCommandInitialization(entity, context);
            }

            protected override IEnumerable<ValidationResult> OnValidateObjectForUpdate(Person resource, ExecutionContext<Person> context)
            {
                OnValidateObjectForUpdateWasInvoked = true;
                return base.OnValidateObjectForUpdate(resource, context);
            }

            protected override IEnumerable<ISynchronousRule> OnGetBusinessRulesForUpdate(Person resource, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForUpdateWasInvoked = true;
                return base.OnGetBusinessRulesForUpdate(resource, context);
            }

            protected override IEnumerable<ValidationResult> OnPerformUpdateCommandValidation(Person person, ExecutionContext<Person> context)
            {
                OnPerformUpdateCommandValidationWasInvoked = true;
                return base.OnPerformUpdateCommandValidation(person, context);
            }

            protected override Person OnUpdateCommandValidationSuccess(Person person, ExecutionContext<Person> context)
            {
                OnUpdateCommandValidationSuccessWasInvoked = true;
                return base.OnUpdateCommandValidationSuccess(person, context);
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

            protected override IEnumerable<ISynchronousRule> OnGetBusinessRulesForDelete(long id, ExecutionContext<Person> context)
            {
                OnGetBusinessRulesForDeleteWasInvoked = true;
                return base.OnGetBusinessRulesForDelete(id, context);
            }

            protected override IEnumerable<ValidationResult> OnPerformDeleteCommandValidation(long id, ExecutionContext<Person> context)
            {
                OnPerformDeleteCommandValidationWasInvoked = true;
                return base.OnPerformDeleteCommandValidation(id, context);
            }

            protected override void OnDeleteCommandValidationSuccess(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandValidationSuccessWasInvoked = true;
                base.OnDeleteCommandValidationSuccess(id, context);
            }

            #endregion
        }

        public class PersonProxyStub : ISynchronousDataProxy<Person, long>
        {
            public void Delete(long id) { }

            public IEnumerable<Person> GetAll()
            {
                return Enumerable.Empty<Person>();
            }

            public Person GetByID(long id)
            {
                return new Person();
            }

            public Person Insert(Person entity)
            {
                return new Person();
            }

            public Person Update(Person entity)
            {
                return new Person();
            }
        }
    }

}
