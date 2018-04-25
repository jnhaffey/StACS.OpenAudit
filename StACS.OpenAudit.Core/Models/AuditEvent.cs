using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using FluentValidation.Results;
using LiteGuard;
using Newtonsoft.Json;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Extensions;
using StACS.OpenAudit.Core.Interfaces;
using StACS.OpenAudit.Core.Validation;

namespace StACS.OpenAudit.Core.Models
{
    public class AuditEvent : IAuditEvent
    {
        private static readonly Func<DateTime> TimeProvider = () => DateTime.UtcNow;
        private readonly AuditEventValidator _validator;

        /// <summary>
        ///     Initialize instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        private AuditEvent(OperationType operationType, Type targetType, string targetId)
            : this()
        {
            Contract.Requires(targetType != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(targetId));
            Guard.AgainstNullArgument(nameof(targetType), targetType);
            Guard.AgainstNullArgument(nameof(targetId), targetId);
            Contract.EndContractBlock();

            OperationType = operationType;
            TargetId = targetId;
            TargetType = targetType.Name;
            Timestamp = TimeProvider();
        }

        [JsonConstructor]
        private AuditEvent()
        {
            _validator = new AuditEventValidator();
        }

        /// <summary>
        ///     Operation type performed.
        /// </summary>
        public OperationType OperationType { get; }

        /// <summary>
        ///     Event being performed.
        /// </summary>
        public string EventType { get; internal set; }

        /// <summary>
        ///     Timestamp of event.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        ///     Description of event.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        ///     Target id of the activity, for example, case ref no
        /// </summary>
        public string TargetId { get; }

        /// <summary>
        ///     Target type
        /// </summary>
        public string TargetType { get; }

        /// <summary>
        ///     Type of data with respect to event.
        /// </summary>
        public string DataType { get; internal set; }

        /// <summary>
        ///     Id of data with respect to event.
        /// </summary>
        public string DataId { get; internal set; }

        /// <summary>
        ///     Actual data with respect to event.
        /// </summary>
        public string Data { get; internal set; }

        /// <summary>
        ///     Flag indicating if data is sensitive.
        /// </summary>
        public bool IsSensitiveData { get; internal set; }

        /// <summary>
        ///     Application name that executed event.
        /// </summary>
        public string ApplicationName { get; internal set; }

        /// <summary>
        ///     Machine name that executed event.
        /// </summary>
        public string MachineName { get; internal set; }

        /// <summary>
        ///     Id of user that executed event.
        /// </summary>
        public string UserId { get; internal set; }

        /// <summary>
        ///     User name that executed event.
        /// </summary>
        public string UserName { get; internal set; }

        /// <summary>
        ///     Email of user that executed event.
        /// </summary>
        public string UserEmail { get; internal set; }

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
        public ValidationResult Validate()
        {
            return _validator.Validate(this);
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static AuditEvent AsViewOf(Type targetType, string targetId)
        {
            Contract.Requires(targetType != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(targetId));
            Guard.AgainstNullArgument(nameof(targetType), targetType);
            Guard.AgainstNullArgument(nameof(targetId), targetId);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.View, targetType, targetId);
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetIdAccessor"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AuditEvent AsViewOf<T>(T target, Func<T, string> targetIdAccessor)
            where T : class
        {
            Contract.Requires(target != null);
            Contract.Requires(targetIdAccessor != null);
            Guard.AgainstNullArgument(nameof(target), target);
            Guard.AgainstNullArgument(nameof(targetIdAccessor), targetIdAccessor);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.View, typeof(T), targetIdAccessor(target));
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static AuditEvent AsChangeTo(Type targetType, string targetId)
        {
            Contract.Requires(targetType != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(targetId));
            Guard.AgainstNullArgument(nameof(targetType), targetType);
            Guard.AgainstNullArgument(nameof(targetId), targetId);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.Change, targetType, targetId);
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetIdAccessor"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AuditEvent AsChangeTo<T>(T target, Func<T, string> targetIdAccessor)
            where T : class
        {
            Contract.Requires(target != null);
            Contract.Requires(targetIdAccessor != null);
            Guard.AgainstNullArgument(nameof(target), target);
            Guard.AgainstNullArgument(nameof(targetIdAccessor), targetIdAccessor);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.Change, typeof(T), targetIdAccessor(target));
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static AuditEvent AsActionOn(Type targetType, string targetId)
        {
            Contract.Requires(targetType != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(targetId));
            Guard.AgainstNullArgument(nameof(targetType), targetType);
            Guard.AgainstNullArgument(nameof(targetId), targetId);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.Action, targetType, targetId);
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetIdAccessor"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AuditEvent AsActionOn<T>(T target, Func<T, string> targetIdAccessor)
            where T : class
        {
            Contract.Requires(target != null);
            Contract.Requires(targetIdAccessor != null);
            Guard.AgainstNullArgument(nameof(target), target);
            Guard.AgainstNullArgument(nameof(targetIdAccessor), targetIdAccessor);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.Action, typeof(T), targetIdAccessor(target));
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static AuditEvent AsStatementAbout(Type targetType, string targetId)
        {
            Contract.Requires(targetType != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(targetId));
            Guard.AgainstNullArgument(nameof(targetType), targetType);
            Guard.AgainstNullArgument(nameof(targetId), targetId);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.Statement, targetType, targetId);
        }

        /// <summary>
        ///     Create instance of <see cref="AuditEvent" /> class.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="targetIdAccessor"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AuditEvent AsStatementAbout<T>(T target, Func<T, string> targetIdAccessor)
            where T : class
        {
            Contract.Requires(target != null);
            Contract.Requires(targetIdAccessor != null);
            Guard.AgainstNullArgument(nameof(target), target);
            Guard.AgainstNullArgument(nameof(targetIdAccessor), targetIdAccessor);
            Contract.EndContractBlock();

            return new AuditEvent(OperationType.Statement, typeof(T), targetIdAccessor(target));
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