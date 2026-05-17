using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class WhereTests
{
    [Fact]
    public void RendersWhereClause()
    {
        var events = new Table("Events", "e");

        var id = new Column(events, "Id");
        var ownerId = new Column(events, "OwnerId");

        var query = new SelectQuery(
            new[] { id },
            events);

        query.Where(
            new ComparisonCondition(
                id,
                ownerId,
                ComparisonOperator.Equal
            )
        );

        var renderer = new SqlRenderer();

        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id
            FROM Events e
            WHERE e.Id = e.OwnerId
            """,
            sql);
    }
}

