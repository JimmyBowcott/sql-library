using SqlLibrary.Schema;

namespace SqlLibrary.Query;

public class SelectQuery
{
    public IReadOnlyList<Column> Columns { get; }
    public Table From { get; }

    public SelectQuery(IReadOnlyList<Column> columns, Table from)
    {
        Columns = columns;
        From = from;
    }
}
