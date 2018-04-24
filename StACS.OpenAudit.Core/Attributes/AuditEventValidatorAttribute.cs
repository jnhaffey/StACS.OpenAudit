using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using LiteGuard;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Interfaces;
using StACS.OpenAudit.Core.Models;

namespace StACS.OpenAudit.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditEventValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Guard.AgainstNullArgument(nameof(value), value);
            Guard.AgainstNullArgument(nameof(validationContext), validationContext);

            if (!(value is IAuditEvent auditEvent))
            {
                var message = string.Format(CultureInfo.InvariantCulture,
                    $"AuditEventValidatorAttribute does not support {value.GetType()}");
                return new ValidationResult(message);
            }

            if (auditEvent.OperationType == OperationType.Unknown)
            {
                validationContext.Items.Add(nameof(AuditEvent.OperationType),
                    $"{auditEvent.OperationType} is not a supported OperationType.");
            }


            if (!validationContext.Items.Any()) return ValidationResult.Success;
            
            var memberNames = validationContext.Items.Select(i => i.Key.ToString()).ToArray();
            return new ValidationResult("AuditEvent is not valid. See ValidationResult for details.", memberNames);
        }
    }
}