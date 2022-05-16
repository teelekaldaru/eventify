using Eventify.Common.Classes.Messages.Enums;
using Eventify.Common.Classes.Messages.OperationResult;
using Eventify.Common.Classes.Utilities;
using Eventify.Common.Classes.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventify.Common.Classes.Messages.RequestResult
{
    public class RequestResult<T> : RequestResult
    {
        public T Data { get; private set; }

        private RequestResult()
        {
        }

        public static RequestResult<T> Create(IEnumerable<SimpleMessage> messages, T data = default)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = messages
            };
        }

        public static RequestResult<T> Create(ValidationMessages messages, T data = default)
        {
            return Create(messages.Select((x) => x.Message), data);
        }

        public static RequestResult<T> CreateSuccess(T data, string message = null)
        {
            return Create(SimpleMessage.Simple("Success!", message).MakeCollection(), data);
        }

        public static RequestResult<T> CreateError(IEnumerable<string> messages, T data = default)
        {
            return Create(messages.Select((message) => SimpleMessage.Error(message, null)), data);
        }

        public static RequestResult<T> CreateError(string message, T data = default)
        {
            return CreateError(message.MakeCollection(), data);
        }

        public static RequestResult<T> CreateError(string header, string description, T data = default)
        {
            return Create(SimpleMessage.Error(header, description).MakeCollection(), data);
        }

        public static RequestResult<T> CreateValidation(string header, string description, ValidationMessageSeverity severity, T data = default)
        {
            return Create(SimpleMessage.Validation(header, description, severity).MakeCollection(), data);
        }

        public static RequestResult<T> CreateValidation(ValidationMessages messages, T data = default)
        {
            return Create(messages, data);
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult<T>
            {
                Data = default,
                IsExplicitSuccess = false,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult, string headerOverride)
        {
            IEnumerable<SimpleMessage> messages = operationResult.Messages.Select((x) => SimpleMessage.CopyAndOverrideHeader(x, headerOverride));
            return new RequestResult<T>
            {
                Data = default,
                IsExplicitSuccess = false,
                Messages = messages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResults(params IOperationResult[] results)
        {
            return new RequestResult<T>
            {
                Messages = results.Where((x) => !x.Success).SelectMany((x) => x.Messages),
                Data = default,
                IsExplicitSuccess = false
            };
        }

        public static RequestResult<T> CreateFromOperationResult(IOperationResult<T> operationResult)
        {
            return new RequestResult<T>
            {
                Data = operationResult.Data,
                IsExplicitSuccess = operationResult.Success,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult<T> CreateFromOperationResult<TOp>(IOperationResult<TOp> operationResult, Func<TOp, T> mappingFunc)
        {
            return new RequestResult<T>
            {
                Data = mappingFunc(operationResult.Data),
                IsExplicitSuccess = operationResult.Success,
                Messages = operationResult.Messages
            };
        }
    }
}
