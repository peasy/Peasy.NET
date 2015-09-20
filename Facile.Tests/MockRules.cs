namespace Facile.Core.Tests
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
}
