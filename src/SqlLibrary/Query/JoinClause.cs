using SqlLibrary.Schema;

namespace SqlLibrary.Query;

/// <summary>
/// Represents a JOIN operation with a target table and condition.
/// </summary>
public class JoinClause
{
    public JoinType Type { get; }
    public Table Table { get; }
    public ICondition Condition { get; }

    public JoinClause(JoinType type, Table table, ICondition condition)
    {
        Type = type;
        Table = table;
        Condition = condition;
    }
}

