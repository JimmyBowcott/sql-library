using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class SelectQueryTests
{
    [Fact]
    public void RendersSelectQuery()
    {
        var eventsTable = new Table("Events");
        var idColumn = new Column(
            eventsTable,
            "Id");

        var query = new SelectQuery(
            new[] { idColumn },
            eventsTable);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT Id
            FROM Events
            """,
            sql);
    }

    [Fact]
    public void RendersMultipleColumns()
    {
        var eventsTable = new Table("Events");
        var idColumn = new Column(eventsTable, "Id");
        var nameColumn = new Column(eventsTable, "Name");

        var query = new SelectQuery(
            new[] { idColumn, nameColumn },
            eventsTable);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT Id, Name
            FROM Events
            """,
            sql);
    }

    [Fact]
    public void RendersTableAlias()
    {
        var eventsTable = new Table("Events", "e");
        var idColumn = new Column(eventsTable, "Id");

        var query = new SelectQuery(
            new[] { idColumn },
            eventsTable);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id
            FROM Events e
            """,
            sql);
    }
}
