using System.Collections.Generic;

namespace Eventify.Common.Utils.Messages.OperationResult
{
    public interface IOperationResult
    {
        bool Success { get; }

        bool HasOnlyWarnings { get; }

        IEnumerable<SimpleMessage> Messages { get; }
    }

    public interface IOperationResult<out T> : IOperationResult
    {
        T Data { get; }
    }
}
