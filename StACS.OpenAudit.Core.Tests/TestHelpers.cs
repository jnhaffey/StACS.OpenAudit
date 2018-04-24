using System;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Models;

namespace StACS.OpenAudit.Core.Tests
{
    public static class TestHelpers
    {
        public const string PlaceHolder = "PLACEHOLDER";

        public static AuditEvent GenerateAuditEvent(string stringValues = "")
        {
            return new AuditEvent
            {
                ApplicationName = stringValues,
                Data = stringValues,
                DataId = stringValues,
                DataType = stringValues,
                Description = stringValues,
                EventType = stringValues,
                IsEncrypted = false,
                MachineName = stringValues,
                OperationType = OperationType.Unknown,
                Timestamp = DateTime.MaxValue,
                UserEmail = stringValues,
                UserId = stringValues,
                UserName = stringValues
            };
        }
    }
}