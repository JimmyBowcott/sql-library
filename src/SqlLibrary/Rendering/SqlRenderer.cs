using SqlLibrary.Query;
using SqlLibrary.Schema;

namespace SqlLibrary.Rendering;

public class SqlRenderer
{
    public string Render(SelectQuery query)
    {
        var columns = string.Join(", ", query.Columns.Select(RenderColumn));

        return
            $"""
            SELECT {columns}
            FROM {RenderTable(query.From)}
            """;
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
}
