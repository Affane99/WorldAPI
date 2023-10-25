using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace World.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message):base(message) { }

        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.Errors.Select(v=> $"•{v.ErrorMessage}").ToList();
        }

        public List<string> ValidationErrors { get; set; }
    }
}
