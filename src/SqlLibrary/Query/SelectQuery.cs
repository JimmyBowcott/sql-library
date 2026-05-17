using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class SelectQuery
{
    public IReadOnlyList<Column> Columns { get; }
    public Table From { get; }
    public List<JoinClause> Joins { get; } = new();
    public ICondition? WhereClause { get; private set; }

    public SelectQuery(IReadOnlyList<Column> columns, Table from)
    {
        Columns = columns;
        From = from;
    }

    public void Join(JoinClause join)
    {
        Joins.Add(join);
    }

    public void Where(ICondition condition)
    {
        WhereClause = condition;
    }
}
