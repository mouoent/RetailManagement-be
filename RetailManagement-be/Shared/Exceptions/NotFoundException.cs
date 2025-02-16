using RetailManagement_be.Models.Entities;

namespace RetailManagement_be.Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class NotFoundException<T> : NotFoundException where T : BaseEntity
{
    public NotFoundException()
            : base($"Could not find {typeof(T).Name}") { }
}
