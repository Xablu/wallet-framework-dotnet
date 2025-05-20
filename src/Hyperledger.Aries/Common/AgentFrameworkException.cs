using System;
using Hyperledger.Aries.Features.Handshakes.Common;
using Hyperledger.Aries.Storage;

namespace Hyperledger.Aries
{
    /// <summary>
    /// Agent Framework exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AriesFrameworkException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public ErrorCode ErrorCode { get; }
   
        /// <summary>
        /// Gets the message context record.
        /// May be <code>null</code>.
        /// </summary>
        /// <value>
        /// The message context record. 
        /// </value>
        public RecordBase ContextRecord { get; }

        /// <summary>
        /// Gets the context Record ID.
        /// </summary>
        /// <value>
        /// The context record ID. 
        /// </value>
        public string ContextRecordId { get; }
        
        /// <summary>
        /// Gets the connection record.
        /// May be <code>null</code>.
        /// </summary>
        /// <value>
        /// The connection record. 
        /// </value>
        public ConnectionRecord ConnectionRecord { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        public AriesFrameworkException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AriesFrameworkException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AriesFrameworkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected AriesFrameworkException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public AriesFrameworkException(ErrorCode errorCode) : this(errorCode,
            $"Framework error occured. Code: {errorCode}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public AriesFrameworkException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public AriesFrameworkException(ErrorCode errorCode, string message, Exception innerException) : base(message,
            innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="messages">The message to concatenate together.</param>
        public AriesFrameworkException(ErrorCode errorCode, string[] messages) : base(string.Join("\n", messages))
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="contextRecord"></param>
        /// <param name="connectionRecord"></param>
        public AriesFrameworkException(ErrorCode errorCode, string message, RecordBase contextRecord, ConnectionRecord connectionRecord) :
            base(message)
        {
            ErrorCode = errorCode;
            ContextRecord = contextRecord;
            ConnectionRecord = connectionRecord;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AriesFrameworkException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="contextRecordId">The record ID</param>
        public AriesFrameworkException(ErrorCode errorCode, string message, string contextRecordId) :
            base(message)
        {
            ErrorCode = errorCode;
            ContextRecordId = contextRecordId;
        }
    }
}
