using Eventify.Common.Classes.Exceptions;
using Eventify.Common.Classes.Logger;
using Eventify.Common.Classes.Messages.RequestResult;
using Eventify.Common.Classes.Utilities;
using System;

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
