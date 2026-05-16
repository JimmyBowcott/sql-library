using SqlLibrary.Query;
using SqlLibrary.Schema;

namespace SqlLibrary.Rendering;

public class SqlRenderer
{
    public string Render(SelectQuery query)
    {
        var columns = string.Join(", ", query.Columns.Select(RenderColumn));
        var joins = string.Join(", ", query.Joins.Select(RenderJoin));

        return
            $"""
            SELECT {columns}
            FROM {RenderTable(query.From)}
            {joins}
            """.Trim();
    }

    private string RenderColumn(Column column)
    {
        if (!string.IsNullOrEmpty(column.Table.Alias))
            return $"{column.Table.Alias}.{column.Name}";

        return column.Name;
    }

    private string RenderTable(Table table)
    {
        if (!string.IsNullOrEmpty(table.Alias))
            return $"{table.Name} {table.Alias}";

        return table.Name;
    }

    private string RenderJoin(Join join)
    {
        return $"{RenderJoinType(join.Type)} {RenderTable(join.Table)} ON {RenderCondition(join.Condition)}";
    }

    private string RenderCondition(ICondition condition)
    {
        return condition switch
        {
            EqualsCondition eq => RenderEquals(eq),
            _ => throw new NotSupportedException()
        };
    }

    private string RenderJoinType(JoinType joinType)
    {
        return joinType switch
        {
            JoinType.Inner => "INNER JOIN",
            _ => throw new NotSupportedException()
        };
    }

    private string RenderEquals(EqualsCondition condition)
    {
        return $"{RenderColumn(condition.Left)} = {RenderColumn(condition.Right)}";
    }
}
