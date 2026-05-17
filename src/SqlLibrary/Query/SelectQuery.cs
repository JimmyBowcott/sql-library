using SqlLibrary.Schema;

namespace SqlLibrary.Query;

/// <summary>
/// Represents a SELECT query, supporting joins and conditions.
/// </summary>
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

    /// <summary>
    /// Perform a JOIN operation with a table.
    /// </summary>
    public void Join(JoinClause join)
    {
        Joins.Add(join);
    }

    /// <summary>
    /// Apply a WHERE clause to the statement.
    /// </summary>
    public void Where(ICondition condition)
    {
        WhereClause = condition;
    }
}
