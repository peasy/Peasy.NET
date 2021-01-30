using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Peasy.Attributes;
using Peasy.Synchronous;

namespace Peasy.Core.Tests
{
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
