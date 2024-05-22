namespace CircusTrein.Models;

public class IncompatibleAnimalException : Exception
{
    public IncompatibleAnimalException() {}
    public IncompatibleAnimalException(string message) : base(message) {}
    public IncompatibleAnimalException(string message, Exception inner) : base(message, inner) {}
}
public class ExceedsMaxWagonSizeException : Exception
{
    public ExceedsMaxWagonSizeException() {}
    public ExceedsMaxWagonSizeException(string message) : base(message) {}
    public ExceedsMaxWagonSizeException(string message, Exception inner) : base(message, inner) {}
}
