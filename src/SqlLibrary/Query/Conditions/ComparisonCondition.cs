using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class ComparisonCondition : ICondition
{
    public Column Left { get; }
    public Column Right { get; }
    public ComparisonOperator Op { get; }

    public ComparisonCondition(Column left, Column right, ComparisonOperator op)
    {
        Left = left;
        Right = right;
        Op = op;
    }
}
