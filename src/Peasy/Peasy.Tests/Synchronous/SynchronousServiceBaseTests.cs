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
            service.OnGetAllCommandGetRulesWasInvoked.ShouldBe(true);
            service.OnGetAllCommandPerformValidationWasInvoked.ShouldBe(true);
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
            service.OnGetByIDCommandValidateIDWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandGetRulesWasInvoked.ShouldBe(true);
            service.OnGetByIDCommandPerformValidationWasInvoked.ShouldBe(true);
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
            service.OnInsertCommandValidateObjectWasInvoked.ShouldBe(true);
            service.OnInsertCommandGetRulesWasInvoked.ShouldBe(true);
            service.OnInsertCommandPerformValidationWasInvoked.ShouldBe(true);
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
            service.OnUpdateCommandValidateObjectWasInvoked.ShouldBe(true);
            service.OnUpdateCommandGetRulesWasInvoked.ShouldBe(true);
            service.OnUpdateCommandPerformValidationWasInvoked.ShouldBe(true);
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
            service.OnDeleteCommandValidateIdWasInvoked.ShouldBe(true);
            service.OnDeleteCommandGetRulesWasInvoked.ShouldBe(true);
            service.OnDeleteCommandPerformValidationWasInvoked.ShouldBe(true);
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

            public bool OnGetAllCommandGetRulesWasInvoked { get; private set; }
            public bool OnGetAllCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnGetAllCommandInitializationWasInvoked { get; private set; }
            public bool OnGetAllCommandPerformValidationWasInvoked { get; private set; }
            public bool OnGetByIDCommandInitializationWasInvoked { get; private set; }
            public bool OnGetByIDCommandValidateIDWasInvoked { get; private set; }
            public bool OnGetByIDCommandGetRulesWasInvoked { get; private set; }
            public bool OnGetByIDCommandPerformValidationWasInvoked { get; private set; }
            public bool OnGetByIDCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnInsertCommandInitializationWasInvoked { get; private set; }
            public bool OnInsertCommandValidateObjectWasInvoked { get; private set; }
            public bool OnInsertCommandGetRulesWasInvoked { get; private set; }
            public bool OnInsertCommandPerformValidationWasInvoked { get; private set; }
            public bool OnInsertCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnUpdateCommandInitializationWasInvoked { get; private set; }
            public bool OnUpdateCommandValidateObjectWasInvoked { get; private set; }
            public bool OnUpdateCommandGetRulesWasInvoked { get; private set; }
            public bool OnUpdateCommandPerformValidationWasInvoked { get; private set; }
            public bool OnUpdateCommandValidationSuccessWasInvoked { get; private set; }
            public bool OnDeleteCommandInitializationWasInvoked { get; private set; }
            public bool OnDeleteCommandValidateIdWasInvoked { get; private set; }
            public bool OnDeleteCommandGetRulesWasInvoked { get; private set; }
            public bool OnDeleteCommandPerformValidationWasInvoked { get; private set; }
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

            protected override IEnumerable<ISynchronousRule> OnGetAllCommandGetRules(ExecutionContext<Person> context)
            {
                OnGetAllCommandGetRulesWasInvoked = true;
                return base.OnGetAllCommandGetRules(context);
            }

            protected override IEnumerable<ValidationResult> OnGetAllCommandPerformValidation(ExecutionContext<Person> context)
            {
                OnGetAllCommandPerformValidationWasInvoked = true;
                return base.OnGetAllCommandPerformValidation(context);
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

            protected override IEnumerable<ValidationResult> OnGetByIDCommandValidateID(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandValidateIDWasInvoked = true;
                return base.OnGetByIDCommandValidateID(id, context);
            }

            protected override IEnumerable<ISynchronousRule> OnGetByIDCommandGetRules(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandGetRulesWasInvoked = true;
                return base.OnGetByIDCommandGetRules(id, context);
            }

            protected override IEnumerable<ValidationResult> OnGetByIDCommandPerformValidation(long id, ExecutionContext<Person> context)
            {
                OnGetByIDCommandPerformValidationWasInvoked = true;
                return base.OnGetByIDCommandPerformValidation(id, context);
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

            protected override IEnumerable<ValidationResult> OnInsertCommandValidateObject(Person resource, ExecutionContext<Person> context)
            {
                OnInsertCommandValidateObjectWasInvoked = true;
                return base.OnInsertCommandValidateObject(resource, context);
            }

            protected override IEnumerable<ISynchronousRule> OnInsertCommandGetRules(Person resource, ExecutionContext<Person> context)
            {
                OnInsertCommandGetRulesWasInvoked = true;
                return base.OnInsertCommandGetRules(resource, context);
            }

            protected override IEnumerable<ValidationResult> OnInsertCommandPerformValidation(Person resource, ExecutionContext<Person> context)
            {
                OnInsertCommandPerformValidationWasInvoked = true;
                return base.OnInsertCommandPerformValidation(resource, context);
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

            protected override IEnumerable<ValidationResult> OnUpdateCommandValidateObject(Person resource, ExecutionContext<Person> context)
            {
                OnUpdateCommandValidateObjectWasInvoked = true;
                return base.OnUpdateCommandValidateObject(resource, context);
            }

            protected override IEnumerable<ISynchronousRule> OnUpdateCommandGetRules(Person resource, ExecutionContext<Person> context)
            {
                OnUpdateCommandGetRulesWasInvoked = true;
                return base.OnUpdateCommandGetRules(resource, context);
            }

            protected override IEnumerable<ValidationResult> OnUpdateCommandPerformValidation(Person resource, ExecutionContext<Person> context)
            {
                OnUpdateCommandPerformValidationWasInvoked = true;
                return base.OnUpdateCommandPerformValidation(resource, context);
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

            protected override IEnumerable<ValidationResult> OnDeleteCommandValidateId(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandValidateIdWasInvoked = true;
                return base.OnDeleteCommandValidateId(id, context);
            }

            protected override IEnumerable<ISynchronousRule> OnDeleteCommandGetRules(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandGetRulesWasInvoked = true;
                return base.OnDeleteCommandGetRules(id, context);
            }

            protected override IEnumerable<ValidationResult> OnDeleteCommandPerformValidation(long id, ExecutionContext<Person> context)
            {
                OnDeleteCommandPerformValidationWasInvoked = true;
                return base.OnDeleteCommandPerformValidation(id, context);
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
