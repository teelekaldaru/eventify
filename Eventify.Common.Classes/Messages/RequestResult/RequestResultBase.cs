using Eventify.Common.Classes.Exceptions;
using Eventify.Common.Classes.Messages.Enums;
using Eventify.Common.Classes.Messages.OperationResult;
using Eventify.Common.Classes.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventify.Common.Classes.Messages.RequestResult
{
    public class RequestResult
    {
        protected bool? IsExplicitSuccess;

        public bool Success => IsExplicitSuccess ?? Messages?.All((x) => x.IsSimpleMessage) ?? true;

        public IEnumerable<SimpleMessage> Messages { get; protected set; }

        public bool SuccessOrOnlyWarnings => IsExplicitSuccess ?? Messages?.All((x) => x.IsWarningOrSimpleMessage) ?? true;

        public static RequestResult CreateFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult
            {
                IsExplicitSuccess = operationResult.Success,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult CreateError(Exception e)
        {
            SimpleException ex = e as SimpleException;
            IEnumerable<SimpleMessage> messages = ex != null ? ex.Messages : SimpleMessage.Error(e.Message, e.LogException()).MakeCollection();
            return new RequestResult
            {
                IsExplicitSuccess = false,
                Messages = messages
            };
        }

        public static RequestResult Create(IEnumerable<SimpleMessage> messages)
        {
            return new RequestResult
            {
                Messages = messages
            };
        }

        public static RequestResult CreateError(IEnumerable<string> messages)
        {
            return Create(messages.Select((message) => SimpleMessage.Error(message, null)));
        }

        public static RequestResult CreateError(string header, string description = null)
        {
            return Create(SimpleMessage.Error(header, description).MakeCollection());
        }

        public static RequestResult Create(SimpleMessage message)
        {
            return Create(message.MakeCollection());
        }

        public static RequestResult CreateValidation(string header, string description, ValidationMessageSeverity severity)
        {
            return Create(SimpleMessage.Validation(header, description, severity));
        }

        public static RequestResult CreateSuccess(string header = null, string description = null)
        {
            return new RequestResult
            {
                Messages = SimpleMessage.Simple(string.IsNullOrWhiteSpace(header) ? "Success!" : header, description).MakeCollection()
            };
        }
    }
}
