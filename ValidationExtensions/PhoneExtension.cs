using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ValidationExtensions
{
    public class PhoneExtensionAttribute : ValidationAttribute
    {
        private readonly string _name;
        private const string phoneType = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public string TypeMessage { get => $"{_name}: chưa đúng định dạng dữ liệu"; }
        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }

        public PhoneExtensionAttribute(string name)
        {
            _name = name;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as string;
            var phoneValue = string.IsNullOrEmpty(field) ? null : field.Trim();

            if (phoneValue == null)
            {
                return new ValidationResult(NullMessage);
            }

            if (!Regex.IsMatch(phoneValue, phoneType))
            {
                return new ValidationResult(TypeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
