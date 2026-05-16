using SqlLibrary.Schema;

namespace SqlLibrary.Query;

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

