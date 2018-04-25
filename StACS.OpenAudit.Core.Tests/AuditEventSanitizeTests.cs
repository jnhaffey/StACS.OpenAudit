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
            var auditEvent = GenerateAuditEvent(true);

            // Act
            auditEvent.Sanitize();

            // Assert
            auditEvent.ApplicationName.Should().Be(null);
            auditEvent.Data.Should().Be(null);
            auditEvent.DataId.Should().Be(null);
            auditEvent.DataType.Should().Be(null);
            auditEvent.Description.Should().Be(null);
            auditEvent.EventType.Should().Be(null);
            auditEvent.IsSensitiveData.Should().Be(false);
            auditEvent.MachineName.Should().Be(null);
            auditEvent.OperationType.Should().Be(OperationType.Action);
            auditEvent.Timestamp.Should().Be(auditEvent.Timestamp);
            auditEvent.UserEmail.Should().Be(null);
            auditEvent.UserId.Should().Be(null);
            auditEvent.UserName.Should().Be(null);
        }

        [TestMethod]
        public void AuditEvent_Sanitize_Does_Not_Turn_String_Values_To_Null()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();

            // Act
            auditEvent.Sanitize();

            // Assert
            auditEvent.ApplicationName.Should().Be(Constants.PlaceHolder);
            auditEvent.Data.Should().Be(Constants.PlaceHolder);
            auditEvent.DataId.Should().Be(Constants.PlaceHolder);
            auditEvent.DataType.Should().Be(Constants.PlaceHolder);
            auditEvent.Description.Should().Be(Constants.PlaceHolder);
            auditEvent.EventType.Should().Be(Constants.PlaceHolder);
            auditEvent.IsSensitiveData.Should().Be(false);
            auditEvent.MachineName.Should().Be(Constants.PlaceHolder);
            auditEvent.OperationType.Should().Be(OperationType.Action);
            auditEvent.Timestamp.Should().Be(auditEvent.Timestamp);
            auditEvent.UserEmail.Should().Be(Constants.PlaceHolder);
            auditEvent.UserId.Should().Be(Constants.PlaceHolder);
            auditEvent.UserName.Should().Be(Constants.PlaceHolder);
        }
    }
}