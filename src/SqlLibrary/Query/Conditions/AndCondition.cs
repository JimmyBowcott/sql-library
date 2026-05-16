using SqlLibrary.Schema;

namespace SqlLibrary.Query;

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

