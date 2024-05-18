using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ValidationExtensions
{
    public class ScriptExtensionAttribute : ValidationAttribute
    {
        private readonly string _name;

        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }
        public string TypeMessage { get => $"{_name}: chưa đúng định dạng dữ liệu"; }

        public ScriptExtensionAttribute(string name)
        {
            _name = name;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as string;
            var scriptValue = string.IsNullOrEmpty(field) ? null : field.Trim();

            if (scriptValue == null)
            {
                return new ValidationResult(NullMessage);
            }

            if (scriptValue == HttpUtility.UrlEncode(scriptValue))
            {
                return new ValidationResult(TypeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
