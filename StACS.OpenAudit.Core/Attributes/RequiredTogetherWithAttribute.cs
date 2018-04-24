using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using LiteGuard;
using StACS.OpenAudit.Core.Extensions;

namespace StACS.OpenAudit.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredTogetherWithAttribute : ValidationAttribute
    {
        public RequiredTogetherWithAttribute(string otherFieldName)
        {
            Guard.AgainstNullArgument(nameof(otherFieldName), otherFieldName);
            OtherFieldName = otherFieldName;
        }

        public string OtherFieldName { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Guard.AgainstNullArgument(nameof(value), value);
            Guard.AgainstNullArgument(nameof(validationContext), validationContext);

            var validationTarget = validationContext.ObjectInstance;

            var objectType = validationTarget.GetType();
            var otherValueProperty = objectType.GetProperty(OtherFieldName);
            var otherValueType = otherValueProperty?.PropertyType;

            var otherValue = otherValueProperty?.GetValue(null, null);

            var otherValueIsSet = otherValue != null && otherValue != otherValueType.GetDefaultValue();

            var valueIsSet = value != null;
            var field = validationContext.MemberName;

            if ((!valueIsSet || otherValueIsSet) && (valueIsSet || !otherValueIsSet)) return ValidationResult.Success;

            var message = string.Format(CultureInfo.InvariantCulture,
                $"{field} is required if {OtherFieldName} is set.");
            return new ValidationResult(message);
        }
    }
}