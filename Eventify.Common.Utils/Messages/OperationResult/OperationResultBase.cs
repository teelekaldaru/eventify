using System;
using System.Collections.Generic;
using System.Linq;
using Eventify.Common.Utils.Exceptions;
using Eventify.Common.Utils.Messages.Enums;
using Eventify.Common.Utils.Utilities;
using Eventify.Common.Utils.Validations;

namespace Eventify.Common.Utils.Messages.OperationResult
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
	        var messages = e is SimpleException ex ? ex.Messages : SimpleMessage.Error(e.Message, e.LogInnerExceptions()).MakeCollection();
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
            var operationResult = result as OperationResult;
            return new OperationResult
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Success = false
            };
        }
    }
}
