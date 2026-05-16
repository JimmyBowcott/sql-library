using SqlLibrary.Query;

namespace SqlLibrary.Rendering;

public class SqlRenderer
{
    public string Render(SelectQuery query)
    {
        var columns = string.Join(", ", query.Columns.Select(c => c.Name));

        return
            $"""
            SELECT {columns}
            FROM {query.From.Name}
            """;
    }
}
