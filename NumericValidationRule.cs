using System.Globalization;
using System.Windows.Controls;

namespace Data_Input_Validation
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int result))
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "aaa");
        }
    }
}
