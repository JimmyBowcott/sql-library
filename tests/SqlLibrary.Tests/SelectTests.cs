using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class SelectTests
{
    [Fact]
    public void RendersSelectQuery()
    {
        var eventsTable = new Table("Events");
        var eventId = new Column(
            eventsTable,
            "Id");

        var query = new SelectQuery(
            new[] { eventId },
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
        var eventId = new Column(eventsTable, "Id");
        var eventName = new Column(eventsTable, "Name");

        var query = new SelectQuery(
            new[] { eventId, eventName },
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
        var eventId = new Column(eventsTable, "Id");

        var query = new SelectQuery(
            new[] { eventId },
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
