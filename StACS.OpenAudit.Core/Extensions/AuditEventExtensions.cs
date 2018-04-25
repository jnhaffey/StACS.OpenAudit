using System;
using System.Diagnostics.Contracts;
using LiteGuard;
using Newtonsoft.Json;
using StACS.OpenAudit.Core.Models;

namespace StACS.OpenAudit.Core.Extensions
{
    public static class AuditEventExtensions
    {
        public static AuditEvent WithData<T>(this AuditEvent source, T data, Func<T, string> dataIdAccessor)
            where T : class
        {
            Contract.Requires(source != null);
            Contract.Requires(data != null);
            Contract.Requires(dataIdAccessor != null);
            Guard.AgainstNullArgument(nameof(source), source);
            Guard.AgainstNullArgument(nameof(data), data);
            Guard.AgainstNullArgument(nameof(dataIdAccessor), dataIdAccessor);
            Contract.EndContractBlock();

            source.Data = JsonConvert.SerializeObject(data);
            source.DataType = typeof(T).FullName;
            source.DataId = dataIdAccessor(data);

            return source;
        }

        public static AuditEvent WithSensitiveData<T>(this AuditEvent source, T data, Func<T, string> dataIdAccessor)
            where T : class
        {
            source.WithData(data, dataIdAccessor);
            // TODO : Encrypt Sensitive Data
            source.IsSensitiveData = true;

            return source;
        }

        public static AuditEvent WithNoData(this AuditEvent source)
        {
            Contract.Requires(source != null);
            Guard.AgainstNullArgument(nameof(source), source);
            Contract.EndContractBlock();

            source.Data = null;
            source.DataType = null;
            source.DataId = null;

            return source;
        }

        public static AuditEvent AsEvent(this AuditEvent source, string eventName)
        {
            Contract.Requires(source != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(eventName));
            Guard.AgainstNullArgument(nameof(source), source);
            Guard.AgainstNullArgument(nameof(eventName), eventName);
            Contract.EndContractBlock();

            source.EventType = eventName;

            return source;
        }

        public static AuditEvent WithDescription(this AuditEvent source, string description)
        {
            Contract.Requires(source != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(description));
            Guard.AgainstNullArgument(nameof(source), source);
            Guard.AgainstNullArgument(nameof(description), description);
            Contract.EndContractBlock();

            source.Description = description;

            return source;
        }
    }
}