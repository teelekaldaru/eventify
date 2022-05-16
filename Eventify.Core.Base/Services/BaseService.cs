using Eventify.Common.Classes.Exceptions;
using Eventify.Common.Classes.Logger;
using Eventify.Common.Classes.Messages.OperationResult;
using System;

namespace Eventify.Core.Base.Services
{
    public abstract class BaseService
    {
        protected readonly ILogger _logger;

        protected BaseService(ILogger logger)
        {
            _logger = logger;
        }

        protected IOperationResult<T> HandleException<T>(Exception e)
        {
            _logger.Error("BaseService", e.LogException(), e.StackTrace);

            if (e is SimpleException se)
            {
                return OperationResult<T>.CreateError(se.Messages);
            }

            return OperationResult<T>.CreateError(e.LogException());
        }

        protected IOperationResult HandleException(Exception e)
        {
            _logger.Error("BaseService", e.LogException(), e.StackTrace);

            if (e is SimpleException se)
            {
                return OperationResult.CreateError(se.Messages);
            }

            return OperationResult.CreateError(e.LogException());
        }
    }
}
