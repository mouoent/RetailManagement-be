﻿namespace RetailManagement_be.Shared.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
