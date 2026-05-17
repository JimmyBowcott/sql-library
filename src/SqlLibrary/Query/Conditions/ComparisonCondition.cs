using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class ComparisonCondition : ICondition
{
    public Column Left { get; }
    public IValue Right { get; }
    public ComparisonOperator Op { get; }

    public ComparisonCondition(Column left, IValue right, ComparisonOperator op)
    {
        Left = left;
        Right = right;
        Op = op;
    }
}
