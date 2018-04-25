using System;
using FluentValidation;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Models;
using static StACS.OpenAudit.Core.Constants;

namespace StACS.OpenAudit.Core.Validation
{
    public class AuditEventValidator : AbstractValidator<AuditEvent>
    {
        public AuditEventValidator()
        {
            RuleFor(auditEvent => auditEvent.OperationType)
                .NotEqual(OperationType.Unknown)
                .WithMessage(auditEvent => ErrorMessages.OperationTypeUnknownNotSupported);

            RuleFor(auditEvent => auditEvent.EventType)
                .NotEmpty()
                .WithMessage(ErrorMessages.EventTypeIsRequired);

            RuleFor(auditEvent => auditEvent.Timestamp)
                .NotEmpty()
                .WithMessage(ErrorMessages.TimestampMustNotBeDefault);

            RuleFor(auditEvent => auditEvent.Timestamp)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(ErrorMessages.TimestampMustNotBeFuture);

            RuleFor(auditEvent => auditEvent.DataId)
                .NotEmpty()
                .When(auditEvent => !string.IsNullOrEmpty(auditEvent.DataType))
                .WithMessage(ErrorMessages.DataIdDataTypeRequiredTogether);

            RuleFor(auditEvent => auditEvent.DataType)
                .NotEmpty()
                .When(auditEvent => !string.IsNullOrEmpty(auditEvent.DataId))
                .WithMessage(ErrorMessages.DataIdDataTypeRequiredTogether);

            RuleFor(auditEvent => auditEvent.UserId)
                .NotEmpty()
                .When(auditEvent => !string.IsNullOrEmpty(auditEvent.UserName))
                .WithMessage(ErrorMessages.UserIdUserNameRequiredTogether);

            RuleFor(auditEvent => auditEvent.UserName)
                .NotEmpty()
                .When(auditEvent => !string.IsNullOrEmpty(auditEvent.UserId))
                .WithMessage(ErrorMessages.UserIdUserNameRequiredTogether);

            RuleFor(auditEvent => auditEvent.Data)
                .NotEmpty()
                .When(auditEvent => !string.IsNullOrEmpty(auditEvent.DataId)
                                    || !string.IsNullOrEmpty(auditEvent.DataType))
                .WithMessage(ErrorMessages.DataRequiredWithDataIdOrDataType);
        }
    }
}