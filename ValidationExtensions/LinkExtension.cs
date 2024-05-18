using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ValidationExtensions
{
    public class LinkExtensionAttribute : ValidationAttribute
    {
        private readonly string _name;
        private readonly Regex _regex;

        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }
        public string TypeMessage { get => $"{_name}: chưa đúng định dạng dữ liệu"; }

        public LinkExtensionAttribute(string name)
        {
            _name = name;
            _regex = new Regex(
                    @"^(https?|ftps?):\/\/(?:[a-zA-Z0-9]" +
                    @"(?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}" +
                    @"(?::(?:0|[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}" +
                    @"|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?" +
                    @"(?:\/(?:[-a-zA-Z0-9@%_\+.~#?&=]+\/?)*)?$",
                    RegexOptions.IgnoreCase);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as string;
            var linkValue = string.IsNullOrEmpty(field) ? null : field.Trim();

            if (linkValue == null)
            {
                return new ValidationResult(NullMessage);
            }

            _regex.Matches(linkValue);
            if (_regex.IsMatch(linkValue) == false)
            {
                return new ValidationResult(NullMessage);
            }

            return ValidationResult.Success;
        }
    }
}
