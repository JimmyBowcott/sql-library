using SqlLibrary.Schema;

namespace SqlLibrary.Query;

/// <summary>
/// Represents a comparison between two values (e.g. a column reference and a string literal)
/// </summary>
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
