namespace Application.Exceptions
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.Results;

    public class ValidationException : ApplicationException
    {
        public ValidationException(ValidationResult validationResult)
        {
            this.ValidationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                this.ValidationErrors.Add(validationError.ErrorMessage);
            }
        }

        public List<string> ValidationErrors { get; set; }
    }
}
