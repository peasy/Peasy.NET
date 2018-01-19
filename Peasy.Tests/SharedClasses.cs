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
            ErrorMessages[string.Empty] = "FalseRule1 failed validation";
        }
    }

    public class FalseRule2 : RuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessages[string.Empty] = "FalseRule2 failed validation";
        }
    }

    public class FalseRule3 : RuleBase
    {
        protected override void OnValidate()
        {
            IsValid = false;
            ErrorMessages[string.Empty] = "FalseRule3 failed validation";
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
