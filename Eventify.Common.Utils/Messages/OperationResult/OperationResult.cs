using System;
using System.Collections.Generic;
using System.Linq;
using Eventify.Common.Utils.Exceptions;
using Eventify.Common.Utils.Messages.Enums;
using Eventify.Common.Utils.Utilities;
using Eventify.Common.Utils.Validations;

namespace Eventify.Common.Utils.Messages.OperationResult
{
    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        public T Data { get; set; }

        public static OperationResult<T> CreateFromOperationResult(IOperationResult result, T data)
        {
            var operationResult = result as OperationResult;
            return new OperationResult<T>
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Data = data,
                Success = result.Success
            };
        }

        public new static OperationResult<T> CreateErrorFromOperationResult(IOperationResult result)
        {
            var operationResult = result as OperationResult;
            return new OperationResult<T>
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Data = default!,
                Success = false
            };
        }

        public new static OperationResult<T> CreateError(IEnumerable<SimpleMessage> messages)
        {
            return new OperationResult<T>
            {
                Messages = messages,
                Data = default!
            };
        }

        public static OperationResult<T> CreateErrorFromOperationResults(params IOperationResult[] results)
        {
            return new OperationResult<T>
            {
                Messages = results.Where((x) => !x.Success).SelectMany((x) => x.Messages),
                Data = default!,
                Success = false
            };
        }

        public static OperationResult<T> CreateError(Exception e, T data = default!)
        {
	        var messages = e is SimpleException ex ? ex.Messages : SimpleMessage.Error(e.Message, e.LogInnerExceptions()).MakeCollection();
            return new OperationResult<T>
            {
                Messages = messages,
                Data = data
            };
        }

        public static OperationResult<T> CreateError(Exception e, string header)
        {
            return new OperationResult<T>
            {
                Messages = SimpleMessage.Error(header, e.Message).MakeCollection(),
                Data = default!
            };
        }

        public static OperationResult<T> CreateError(IEnumerable<string> messages, T data = default!)
        {
            return new OperationResult<T>
            {
                Messages = messages.Select((x) => SimpleMessage.Error(x, null)),
                Data = data
            };
        }

        public static OperationResult<T> CreateError(string message, T data = default!)
        {
            return new OperationResult<T>
            {
                Messages = SimpleMessage.Error(message, null).MakeCollection(),
                Data = data
            };
        }

        public static OperationResult<T> CreateError(string header, string message, T data = default!)
        {
            return new OperationResult<T>
            {
                Messages = SimpleMessage.Error(header, message).MakeCollection(),
                Data = data
            };
        }

        public static OperationResult<T> CreateSuccess(T data, string message = null)
        {
            return new OperationResult<T>
            {
                Data = data,
                Messages = SimpleMessage.Simple("Success!", message).MakeCollection(),
                Success = true
            };
        }

        public static OperationResult<T> CreateSuccess(T data, IEnumerable<SimpleMessage> messages)
        {
            return new OperationResult<T>
            {
                Data = data,
                Messages = messages,
                Success = true
            };
        }

        public static OperationResult<T> CreateValidationErrors(ValidationMessages messages, T data = default!)
        {
            return new OperationResult<T>
            {
                Data = data,
                Messages = messages.Select((x) => x.Message)
            };
        }

        public static OperationResult<T> CreateValidationErrors(string header, string description, ValidationMessageSeverity severity, T data = default!)
        {
            return new OperationResult<T>
            {
                Data = data,
                Messages = SimpleMessage.Validation(header, description, severity).MakeCollection()
            };
        }
    }
}
