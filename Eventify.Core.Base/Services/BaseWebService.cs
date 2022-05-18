using System;
using Eventify.Common.Utils.Exceptions;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages.RequestResult;
using Eventify.Common.Utils.Utilities;

namespace Eventify.Core.Base.Services
{
    public abstract class BaseWebService
    {
        protected readonly ILogger _logger;

        protected BaseWebService(ILogger logger)
        {
            ServiceActivator.GetScope();
            _logger = logger;
        }

        protected RequestResult<T> HandleException<T>(Exception exception)
        {
            _logger.Error("BaseWebService", exception.LogException());
            if (exception is SimpleException se)
            {
                return RequestResult<T>.Create(se.Messages);
            }

            return RequestResult<T>.CreateError(exception.LogException());
        }

        protected RequestResult HandleException(Exception exception)
        {
            _logger.Error("BaseWebService", exception.LogException());

            if (exception is SimpleException se)
            {
                return RequestResult.Create(se.Messages);
            }

            return RequestResult.CreateError(exception.LogException());
        }
    }
}
