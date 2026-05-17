using SqlLibrary.Query;
using SqlLibrary.Schema;

namespace SqlLibrary.Rendering;

public class SqlRenderer
{
    public string Render(SelectQuery query)
    {
        var columns = string.Join(", ", query.Columns.Select(RenderColumn));

        var sections = new List<string>
        {
            $"SELECT {columns}",
            $"FROM {RenderTable(query.From)}",
        };

        if (query.Joins.Any())
            sections.Add(string.Join("\n", query.Joins.Select(RenderJoin)));

        if (query.WhereClause != null)
            sections.Add(RenderWhere(query.WhereClause));

        return string.Join("\n", sections);
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

    private string RenderJoin(JoinClause join)
    {
        return $"{RenderJoinType(join.Type)} {RenderTable(join.Table)} ON {RenderCondition(join.Condition)}";
    }

    private string RenderWhere(ICondition condition)
    {
        return $"WHERE {RenderCondition(condition)}";
    }

    private string RenderJoinType(JoinType joinType)
    {
        return joinType switch
        {
            JoinType.Inner => "INNER JOIN",
            JoinType.LeftOuter => "LEFT OUTER JOIN",
            JoinType.RightOuter => "RIGHT OUTER JOIN",
            JoinType.FullOuter => "FULL OUTER JOIN",
            _ => throw new NotSupportedException()
        };
    }

    private string RenderCondition(ICondition condition)
    {
        return condition switch
        {
            ComparisonCondition eq => RenderComparison(eq),
            AndCondition eq => RenderAnd(eq),
            OrCondition eq => RenderOr(eq),
            _ => throw new NotSupportedException()
        };
    }

    private string RenderComparison(ComparisonCondition condition)
    {
        string op = condition.Op switch
        {
            ComparisonOperator.Equal => "=",
            ComparisonOperator.GreaterThan => ">",
            ComparisonOperator.GreaterThanOrEqual => ">=",
            ComparisonOperator.LessThan => "<",
            ComparisonOperator.LessThanOrEqual => "<=",
            _ => throw new NotSupportedException()
        };

        return $"{RenderColumn(condition.Left)} {op} {RenderColumn(condition.Right)}";
    }

    private string RenderAnd(AndCondition condition)
    {
        var left = RenderCondition(condition.Left);
        var right = RenderCondition(condition.Right);

        return $"({left} AND {right})";
    }

    private string RenderOr(OrCondition condition)
    {
        var left = RenderCondition(condition.Left);
        var right = RenderCondition(condition.Right);

        return $"({left} OR {right})";
    }
}
