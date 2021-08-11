using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Peasy.Attributes;
using Peasy.Synchronous;

namespace Peasy.Core.Tests
{
    public class PersonCountRule : RuleBase
    {
        private IEnumerable<Person> _people;
        private int _mustHaveCount;

        public PersonCountRule(IEnumerable<Person> people, int mustHaveCount)
        {
            _people = people;
            _mustHaveCount = mustHaveCount;
        }

        protected override Task OnValidateAsync()
        {
            return base.If(() => _people.Count() != _mustHaveCount)
                .ThenInvalidateWith("Invalid person count");
        }
    }

    public class TrueRule : RuleBase
    {
        public TrueRule()
        {
        }

        public TrueRule(string association)
        {
            Association = association;
        }

        protected override Task OnValidateAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class FalseRule1 : RuleBase
    {
        protected override Task OnValidateAsync()
        {
            IsValid = false;
            ErrorMessage = "FalseRule1 failed validation";
            return Task.CompletedTask;
        }
    }

    public class FalseRuleWithAssociation : RuleBase
    {
        public FalseRuleWithAssociation(string association)
        {
            Association = association;
        }

        protected override Task OnValidateAsync()
        {
            IsValid = false;
            ErrorMessage = $"{Association} failed validation";
            return Task.CompletedTask;
        }
    }

    public class FalseRule2 : RuleBase
    {
        protected override Task OnValidateAsync()
        {
            IsValid = false;
            ErrorMessage = "FalseRule2 failed validation";
            return Task.CompletedTask;
        }
    }

    public class FalseRule3 : RuleBase
    {
        protected override Task OnValidateAsync()
        {
            IsValid = false;
            ErrorMessage = "FalseRule3 failed validation";
            return Task.CompletedTask;
        }
    }

    public class SynchronousTrueRule : SynchronousRuleBase
    {
        public SynchronousTrueRule()
        {
        }

        public SynchronousTrueRule(string association)
        {
            Association = association;
        }
    }

    public class SynchronousFalseRule1 : SynchronousRuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = "FalseRule1 failed validation";
        }
    }

    public class SynchronousFalseRuleWithAssociation : SynchronousRuleBase
    {
        public SynchronousFalseRuleWithAssociation(string association)
        {
            Association = association;
        }

        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = $"{Association} failed validation";
        }
    }

    public class SynchronousFalseRule2 : SynchronousRuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = "FalseRule2 failed validation";
        }
    }

    public class SynchronousFalseRule3 : SynchronousRuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = "FalseRule3 failed validation";
        }
    }

    public class TrueRuleUsingIfMethod : RuleBase
    {
        protected override Task OnValidateAsync()
        {
            var name = "Jimi Hendrix";
            return If(() => name == "James Hendrix")
                .ThenInvalidateWith("Name cannot be James Hendrix");
        }
    }

    public class FalseRuleUsingIfMethod : RuleBase
    {
        protected override async Task OnValidateAsync()
        {
            var name = "James Hendrix";
            await If(() => name == "James Hendrix")
                .ThenInvalidateWith("Name cannot be James Hendrix");
        }
    }

    public class TrueRuleUsingIfNotMethod : RuleBase
    {
        protected override Task OnValidateAsync()
        {
            var age = 18;
            return IfNot(() => age <= 21)
                .ThenInvalidateWith("Not young enough");
        }
    }

    public class FalseRuleUsingIfNotMethod : RuleBase
    {
        protected override async Task OnValidateAsync()
        {
            var age = 18;
            await IfNot(() => age >= 21)
                .ThenInvalidateWith("Not old enough");
        }
    }

    public class Person : IDomainObject<long>
    {
        [Range(0, 50)]
        public long ID { get; set; }
        [MaxLength(15)]
        public string First { get; set; }
        [MaxLength(30)]
        public string Last { get; set; }
        public string Version { get; set; }

        [Editable(false)]
        public string Name { get; set; }

        [PeasyForeignKey]
        public int? ForeignKeyID { get; set; }
    }

    public interface IDoThings
    {
        void Log(string message);
        void DoSomething();
        string GetValue();
    }
}
