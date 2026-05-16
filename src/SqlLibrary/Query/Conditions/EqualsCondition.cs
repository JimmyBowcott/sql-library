using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class EqualsCondition : ICondition
{
    public Column Left { get; }
    public Column Right { get; }

    public EqualsCondition(Column left, Column right)
    {
        Left = left;
        Right = right;
    }
}
