using System.ComponentModel.DataAnnotations;

namespace ValidationExtensions
{
    public class EnumExtensionAttribute<T> : ValidationAttribute where T : struct, Enum
    {
        private readonly string _name;

        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }
        public string TypeMessage { get => $"{_name}: chưa đúng định dạng dữ liệu"; }

        public EnumExtensionAttribute(string name)
        {
            _name = name;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as Enum;

            if (field == null)
            {
                return new ValidationResult(NullMessage);
            }

            if (Enum.IsDefined(typeof(T), field) == false)
            {
                return new ValidationResult(TypeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
