namespace SqlLibrary.Schema;

/// <summary>
/// Represents a string, boolean or integer.
/// </summary>
public class Literal : IValue
{
    public object Value { get; }

    public Literal(object value)
    {
        Value = value;
    }
}
