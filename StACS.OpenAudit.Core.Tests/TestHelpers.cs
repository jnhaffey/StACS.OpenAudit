using System;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Models;

namespace StACS.OpenAudit.Core.Tests
{
    public static class TestHelpers
    {
        public static AuditEvent GenerateAuditEvent(bool emptyStrings = false)
        {
            return new AuditEvent
            {
                ApplicationName = emptyStrings ? string.Empty : Constants.PlaceHolder,
                Data = emptyStrings ? string.Empty : Constants.PlaceHolder,
                DataId = emptyStrings ? string.Empty : Constants.PlaceHolder,
                DataType = emptyStrings ? string.Empty : Constants.PlaceHolder,
                Description = emptyStrings ? string.Empty : Constants.PlaceHolder,
                EventType = emptyStrings ? string.Empty : Constants.PlaceHolder,
                IsSensitiveData = false,
                MachineName = emptyStrings ? string.Empty : Constants.PlaceHolder,
                OperationType = OperationType.Action,
                Timestamp = DateTime.UtcNow.AddMinutes(-1),
                UserEmail = emptyStrings ? string.Empty : Constants.PlaceHolder,
                UserId = emptyStrings ? string.Empty : Constants.PlaceHolder,
                UserName = emptyStrings ? string.Empty : Constants.PlaceHolder
            };
        }
    }
}