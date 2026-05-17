namespace SqlLibrary.Schema;

public class Literal : IValue
{
    public object Value { get; }

    public Literal(object value)
    {
        Value = value;
    }
}
