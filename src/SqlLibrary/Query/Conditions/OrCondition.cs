using SqlLibrary.Schema;

namespace SqlLibrary.Query;

/// <summary>
/// Represents a logical OR operation between two conditions.
/// </summary>
public class OrCondition : ICondition
{
    public ICondition Left { get; }
    public ICondition Right { get; }

    public OrCondition(ICondition left, ICondition right)
    {
        Left = left;
        Right = right;
    }
}

