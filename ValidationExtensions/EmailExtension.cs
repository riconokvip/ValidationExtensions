using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace ValidationExtensions
{
    public class EmailExtensionAttribute : ValidationAttribute
    {
        private readonly string _name;

        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }
        public string TypeMessage { get => $"{_name}: chưa đúng định dạng dữ liệu hoặc không tồn tại"; }

        public EmailExtensionAttribute(string name)
        {
            _name = name;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as string;
            var emailValue = string.IsNullOrEmpty(field) ? null : field.Trim();

            if (emailValue == null)
            {
                return new ValidationResult(NullMessage);
            }

            try
            {
                var emailAddress = new MailAddress(emailValue);
            }
            catch
            {
                return new ValidationResult(TypeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
