using Eventify.Common.Classes.Exceptions;
using Eventify.Common.Classes.Messages.Enums;
using Eventify.Common.Classes.Utilities;
using Eventify.Common.Classes.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventify.Common.Classes.Messages.OperationResult
{
    public class OperationResult : IOperationResult
    {
        public bool Success { get; set; }

        public bool HasOnlyWarnings
        {
            get
            {
                if (Messages.Any())
                {
                    return Messages.All((x) => x.IsWarning);
                }

                return false;
            }
        }

        public IEnumerable<SimpleMessage> Messages { get; set; }

        public static IOperationResult CreateSuccess()
        {
            return new OperationResult
            {
                Success = true,
                Messages = Enumerable.Empty<SimpleMessage>()
            };
        }

        public static IOperationResult CreateSuccess(IEnumerable<SimpleMessage> messages)
        {
            return new OperationResult
            {
                Success = true,
                Messages = messages
            };
        }

        public static IOperationResult CreateError(Exception e)
        {
            SimpleException ex = e as SimpleException;
            IEnumerable<SimpleMessage> messages = ex != null ? ex.Messages : SimpleMessage.Error(e.Message, e.LogInnerExceptions()).MakeCollection();
            return new OperationResult
            {
                Messages = messages
            };
        }

        public static IOperationResult CreateError(IEnumerable<string> messages)
        {
            return new OperationResult
            {
                Messages = messages.Select((x) => SimpleMessage.Error(x, null))
            };
        }

        public static OperationResult CreateError(IEnumerable<SimpleMessage> messages)
        {
            return new OperationResult
            {
                Messages = messages
            };
        }

        public static IOperationResult CreateError(string message)
        {
            return new OperationResult
            {
                Messages = SimpleMessage.Error(message, null).MakeCollection()
            };
        }

        public static IOperationResult CreateError(string header, string description)
        {
            return new OperationResult
            {
                Messages = SimpleMessage.Error(header, description).MakeCollection()
            };
        }

        public static IOperationResult CreateValidationErrors(string header, string description, ValidationMessageSeverity severity)
        {
            return new OperationResult
            {
                Messages = SimpleMessage.Validation(header, description, severity).MakeCollection()
            };
        }

        public static IOperationResult CreateValidationErrors(ValidationMessages messages)
        {
            return new OperationResult
            {
                Messages = messages.Select((x) => x.Message)
            };
        }

        public static OperationResult CreateErrorFromOperationResult(IOperationResult result)
        {
            OperationResult operationResult = result as OperationResult;
            return new OperationResult
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Success = false
            };
        }
    }
}
