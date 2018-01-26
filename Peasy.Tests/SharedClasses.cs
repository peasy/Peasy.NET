using System.ComponentModel.DataAnnotations;

namespace Peasy.Core.Tests
{
    public class TrueRule : RuleBase
    {
    }

    public class FalseRule1 : RuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = "FalseRule1 failed validation";
        }
    }

    public class FalseRuleWithAssociation : RuleBase
    {
        public FalseRuleWithAssociation(string association)
        {
            Association = association;
        }

        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = "FalseRule1 failed validation";
        }
    }

    public class FalseRule2 : RuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessage = "FalseRule2 failed validation";
        }
    }

    public class FalseRule3 : RuleBase
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
    }
}
