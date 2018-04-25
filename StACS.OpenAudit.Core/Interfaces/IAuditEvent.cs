using System;
using StACS.OpenAudit.Core.Enums;

namespace StACS.OpenAudit.Core.Interfaces
{
    public interface IAuditEvent : ISanitizeAndValidate
    {
        OperationType OperationType { get; set; }
        string EventType { get; set; }
        DateTime Timestamp { get; set; }
        string Description { get; set; }
        string DataType { get; set; }
        string DataId { get; set; }
        string Data { get; set; }
        bool IsSensitiveData { get; set; }
        string ApplicationName { get; set; }
        string MachineName { get; set; }
        string UserId { get; set; }
        string UserName { get; set; }
        string UserEmail { get; set; }
    }
}