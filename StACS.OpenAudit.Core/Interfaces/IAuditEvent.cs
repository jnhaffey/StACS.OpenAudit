using System;
using StACS.OpenAudit.Core.Enums;

namespace StACS.OpenAudit.Core.Interfaces
{
    public interface IAuditEvent : ISanitizeAndValidate
    {
        OperationType OperationType { get; }
        string EventType { get; }
        DateTime Timestamp { get; }
        string Description { get; }
        string TargetId { get; }
        string TargetType { get; }
        string DataType { get; }
        string DataId { get; }
        string Data { get; }
        bool IsSensitiveData { get; }
        string ApplicationName { get; }
        string MachineName { get; }
        string UserId { get; }
        string UserName { get; }
        string UserEmail { get; }
    }
}