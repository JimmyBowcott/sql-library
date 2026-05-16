using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class SelectQuery
{
    public IReadOnlyList<Column> Columns { get; }
    public Table From { get; }
    public List<Join> Joins { get; } = new();

    public SelectQuery(IReadOnlyList<Column> columns, Table from)
    {
        Columns = columns;
        From = from;
    }

    public void AddJoin(Join join)
    {
        Joins.Add(join);
    }
}
