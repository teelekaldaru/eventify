﻿using Eventify.Common.Classes.Messages.Enums;

namespace Eventify.Common.Classes.Validations
{
    public class ValidationCheck
    {
        public bool IsValid { get; private set; }

        public ValidationMessage Message { get; private set; }

        private ValidationCheck()
        {
        }

        public static ValidationCheck Create(bool isValid, string header, string description, ValidationMessageSeverity severity)
        {
            return new ValidationCheck
            {
                IsValid = isValid,
                Message = new ValidationMessage(header, description, severity)
            };
        }
    }
}
