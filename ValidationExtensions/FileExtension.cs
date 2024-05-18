using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ValidationExtensions
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string _name;
        private readonly string[] _extensions;
        private readonly int _maxSize;

        public string NullMessage { get => $"{_name}: chưa có dữ liệu"; }
        public string TypeMessage { get => $"{_name}: dữ liệu chỉ được hỗ trợ ở định dạng file {string.Join(", ", _extensions)}"; }
        public string SizeMessage { get => $"{_name}: kích thước tối đa cho phép là {_maxSize} byte"; }

        public FileExtensionAttribute(string name, string[] extension, int maxSize = 2 * 1024 * 1024)
        {
            _name = name;
            _extensions = extension;
            _maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = value as IFormFile;

            if (field == null)
            {
                return new ValidationResult(NullMessage);
            }

            if (field.Length > _maxSize)
            {
                return new ValidationResult(SizeMessage);
            }

            var extension = Path.GetExtension(field.FileName);
            if (!_extensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult(TypeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
