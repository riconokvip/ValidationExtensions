using System.ComponentModel.DataAnnotations;

namespace ValidationExtensions
{
    /// <summary>
    /// Validation string
    /// </summary>
    public class StringExtensionAttribute : ValidationAttribute
    {
        private readonly string _name;
        private readonly int _maxSize;
        private readonly int _minSize;
        private readonly bool _isLimit;

        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }
        public string SizeMessage
        {
            get => _isLimit ?
                $"{_name}: độ dài tối thiểu cho phép là {_minSize} kí tự và tối đa là {_maxSize} kí tự"
                : $"{_name}: độ dài tối thiểu cho phép là {_minSize} kí tự";
        }

        public StringExtensionAttribute(string name, int minSize = 6, int maxSize = 20, bool isLimit = true)
        {
            _name = name;
            _minSize = minSize;
            _maxSize = maxSize;
            _isLimit = isLimit;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as string;
            var stringValue = string.IsNullOrEmpty(field) ? null : field.Trim();

            if (stringValue == null)
            {
                return new ValidationResult(NullMessage);
            }

            if (_isLimit && (stringValue.Length > _maxSize || stringValue.Length < _minSize))
            {
                return new ValidationResult(SizeMessage);
            }

            if (stringValue.Length < _minSize)
            {
                return new ValidationResult(SizeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
