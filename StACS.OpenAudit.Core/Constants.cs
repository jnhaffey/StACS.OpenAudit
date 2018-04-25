using StACS.OpenAudit.Core.Enums;

namespace StACS.OpenAudit.Core
{
    public static class Constants
    {
        public const string PlaceHolder = "|PLACEHOLDER|";

        public static class ErrorMessages
        {
            public const string DataIdDataTypeRequiredTogether = "DataId and DataType are required together.";

            public const string DataRequiredWithDataIdOrDataType =
                "Data is required if DataType / DataId are specified.";

            public const string UserIdUserNameRequiredTogether = "UserId and UserName are required together.";

            public const string EventTypeIsRequired = "EventType is required.";

            public const string TimestampMustNotBeDefault = "Timestamp must not be default value.";

            public const string TimestampMustNotBeFuture = "Timestamp must not be in the future.";

            public static readonly string OperationTypeUnknownNotSupported =
                $"{OperationType.Unknown} is not a supported OperationType.";
        }
    }
}