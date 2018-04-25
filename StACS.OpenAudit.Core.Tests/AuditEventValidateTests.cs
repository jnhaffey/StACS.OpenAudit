using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StACS.OpenAudit.Core.Enums;
using static StACS.OpenAudit.Core.Tests.TestHelpers;

namespace StACS.OpenAudit.Core.Tests
{
    [TestClass]
    public class AuditEventValidateTests
    {
        [TestMethod]
        public void AuditEvent_Validate_OperationType_Set_Unknown_Is_Invalid_With_Error()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();
            auditEvent.OperationType = OperationType.Unknown;

            // Act
            var result = auditEvent.Validate();

            // Assert
            result.IsValid.Should().Be(false);
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be(Constants.ErrorMessages.OperationTypeUnknownNotSupported);
        }

        [TestMethod]
        public void AuditEvent_Validate_OperationType_Not_Set_Unspecificed_Does_Not_Throw_Validation_Exception()
        {
            // Arrange
            var operationTypes = ((OperationType[]) Enum.GetValues(typeof(OperationType)))
                .Where(o => o != OperationType.Unknown);
            var auditEvent = GenerateAuditEvent();

            foreach (var operationType in operationTypes)
            {
                auditEvent.OperationType = operationType;

                // Act
                var result = auditEvent.Validate();

                // Assert
                result.IsValid.Should().Be(true);
                result.Errors.Should().HaveCount(0);
            }
        }

        [TestMethod]
        public void AuditEvent_Validate_Empty_EventType_Is_Invalid_With_Error()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();
            auditEvent.EventType = string.Empty;

            // Act
            var result = auditEvent.Validate();

            // Assert
            result.IsValid.Should().Be(false);
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be(Constants.ErrorMessages.EventTypeIsRequired);
        }

        [TestMethod]
        public void AuditEvent_Validate_Null_EventType_Is_Invalid_With_Error()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();
            auditEvent.EventType = null;

            // Act
            var result = auditEvent.Validate();

            // Assert
            result.IsValid.Should().Be(false);
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be(Constants.ErrorMessages.EventTypeIsRequired);
        }

        [TestMethod]
        public void AuditEvent_Validate_Default_Timestamp_Is_Invalid_With_Error()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();
            auditEvent.Timestamp = default(DateTime);

            // Act
            var result = auditEvent.Validate();

            // Assert
            result.IsValid.Should().Be(false);
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be(Constants.ErrorMessages.TimestampMustNotBeDefault);
        }

        [TestMethod]
        public void AuditEvent_Validate_Future_Timestamp_Is_Invalid_With_Error()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();
            auditEvent.Timestamp = DateTime.MaxValue;

            // Act
            var result = auditEvent.Validate();

            // Assert
            result.IsValid.Should().Be(false);
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorMessage.Should().Be(Constants.ErrorMessages.TimestampMustNotBeFuture);
        }
    }
}