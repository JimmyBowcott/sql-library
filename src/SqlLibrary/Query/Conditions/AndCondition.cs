using SqlLibrary.Schema;

namespace SqlLibrary.Query;

/// <summary>
/// Represents a logical AND operation between two conditions.
/// </summary>
public class AndCondition : ICondition
{
    public ICondition Left { get; }
    public ICondition Right { get; }

    public AndCondition(ICondition left, ICondition right)
    {
        Left = left;
        Right = right;
    }
}

