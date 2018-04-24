using System;
using StACS.OpenAudit.Core.Enums;

namespace StACS.OpenAudit.Core.Interfaces
{
    public interface IAuditEvent
    {
        /// <summary>
        ///     Operation type performed.
        /// </summary>
        OperationType OperationType { get; set; }

        /// <summary>
        ///     Event being performed.
        /// </summary>
        string EventType { get; set; }

        /// <summary>
        ///     Timestamp of event.
        /// </summary>
        DateTime Timestamp { get; set; }

        /// <summary>
        ///     Description of event.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Type of data with respect to event.
        /// </summary>
        string DataType { get; set; }

        /// <summary>
        ///     Id of data with respect to event.
        /// </summary>
        string DataId { get; set; }

        /// <summary>
        ///     Actual data with respect to event.
        /// </summary>
        string Data { get; set; }

        /// <summary>
        ///     Flag indicating if data is encrypted.
        /// </summary>
        bool IsEncrypted { get; set; }

        /// <summary>
        ///     Application name that executed event.
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        ///     Machine name that executed event.
        /// </summary>
        string MachineName { get; set; }

        /// <summary>
        ///     Id of user that executed event.
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        ///     User name that executed event.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        ///     Email of user that executed event.
        /// </summary>
        string UserEmail { get; set; }
    }
}