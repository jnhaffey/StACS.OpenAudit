using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Models;
using static StACS.OpenAudit.Core.Tests.TestHelpers;

namespace StACS.OpenAudit.Core.Tests
{
    [TestClass]
    public class AuditEventValidateTests
    {
        [TestMethod]
        public void AuditEvent_Validate_OperationType_Set_Unknown_Throws_Validation_Exception()
        {
            // Arrange
            var auditEvent = GenerateAuditEvent();

            // Act
            Action act = () => auditEvent.Validate();

            // Assert
            act.Should().Throw<ValidationException>()
                .Where(e => e.ValidationResult.MemberNames.Contains(nameof(AuditEvent.OperationType)));
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
                Action act = () => auditEvent.Validate();

                // Assert
                act.Should().NotThrow<ValidationException>();
            }
        }
    }
}