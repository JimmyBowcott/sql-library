using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class Join
{
    public JoinType Type { get; }
    public Table Table { get; }
    public ICondition Condition { get; }

    public Join(JoinType type, Table table, ICondition condition)
    {
        Type = type;
        Table = table;
        Condition = condition;
    }
}

