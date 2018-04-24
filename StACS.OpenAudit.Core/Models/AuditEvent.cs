using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using StACS.OpenAudit.Core.Attributes;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Extensions;
using StACS.OpenAudit.Core.Interfaces;

namespace StACS.OpenAudit.Core.Models
{
    [AuditEventValidator]
    public class AuditEvent : IAuditEvent
    {
        /// <summary>
        ///     Operation type performed.
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        ///     Event being performed.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        ///     Timestamp of event.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        ///     Description of event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Type of data with respect to event.
        /// </summary>
        [RequiredTogetherWith("DataId")]
        public string DataType { get; set; }

        /// <summary>
        ///     Id of data with respect to event.
        /// </summary>
        [RequiredTogetherWith("DataType")]
        public string DataId { get; set; }

        /// <summary>
        ///     Actual data with respect to event.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     Flag indicating if data is encrypted.
        /// </summary>
        public bool IsEncrypted { get; set; }

        /// <summary>
        ///     Application name that executed event.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        ///     Machine name that executed event.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     Id of user that executed event.
        /// </summary>
        [RequiredTogetherWith("UserName")]
        public string UserId { get; set; }

        /// <summary>
        ///     User name that executed event.
        /// </summary>
        [RequiredTogetherWith("UserId")]
        public string UserName { get; set; }

        /// <summary>
        ///     Email of user that executed event.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        ///     Sanitized all properties of type string from whitespace to null
        /// </summary>
        public void Sanitize()
        {
            var publicProperties = typeof(AuditEvent)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string));

            foreach (var publicProperty in publicProperties)
            {
                var sanitizedValue = publicProperty.GetValue(this, null).ToString().SanitizeWhitespaceAsNull();
                publicProperty.SetValue(this, sanitizedValue);
            }
        }

        /// <summary>
        ///     Validates all properties
        /// </summary>
        public void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this));
        }

        /// <summary>
        ///     Executes Sanitizer and Validation
        /// </summary>
        public void SanitizeAndValidate()
        {
            Sanitize();
            Validate();
        }
    }
}