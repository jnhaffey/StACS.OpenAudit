using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StACS.OpenAudit.Core.Enums;
using static StACS.OpenAudit.Core.Tests.TestHelpers;

namespace StACS.OpenAudit.Core.Tests
{
    [TestClass]
    public class AuditEventSanitizeTests
    {
        [TestMethod]
        public void AuditEvent_Sanitize_Turns_WhiteSpace_To_Null()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();

            // Act
            auditEvent.Sanitize();

            // Assert
            auditEvent.ApplicationName.Should().Be(null);
            auditEvent.Data.Should().Be(null);
            auditEvent.DataId.Should().Be(null);
            auditEvent.DataType.Should().Be(null);
            auditEvent.Description.Should().Be(null);
            auditEvent.EventType.Should().Be(null);
            auditEvent.IsEncrypted.Should().Be(false);
            auditEvent.MachineName.Should().Be(null);
            auditEvent.OperationType.Should().Be(OperationType.Unknown);
            auditEvent.Timestamp.Should().Be(DateTime.MaxValue);
            auditEvent.UserEmail.Should().Be(null);
            auditEvent.UserId.Should().Be(null);
            auditEvent.UserName.Should().Be(null);
        }

        [TestMethod]
        public void AuditEvent_Sanitize_Does_Not_Turn_String_Values_To_Null()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent(PlaceHolder);

            // Act
            auditEvent.Sanitize();

            // Assert
            auditEvent.ApplicationName.Should().Be(PlaceHolder);
            auditEvent.Data.Should().Be(PlaceHolder);
            auditEvent.DataId.Should().Be(PlaceHolder);
            auditEvent.DataType.Should().Be(PlaceHolder);
            auditEvent.Description.Should().Be(PlaceHolder);
            auditEvent.EventType.Should().Be(PlaceHolder);
            auditEvent.IsEncrypted.Should().Be(false);
            auditEvent.MachineName.Should().Be(PlaceHolder);
            auditEvent.OperationType.Should().Be(OperationType.Unknown);
            auditEvent.Timestamp.Should().Be(DateTime.MaxValue);
            auditEvent.UserEmail.Should().Be(PlaceHolder);
            auditEvent.UserId.Should().Be(PlaceHolder);
            auditEvent.UserName.Should().Be(PlaceHolder);
        }
    }
}