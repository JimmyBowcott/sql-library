using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class JoinTests
{
    [Fact]
    public void RendersSingleInnerJoin()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");

        var eventId = new Column(events, "Id");
        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeName = new Column(eventAttendee, "Name");

        var query = new SelectQuery(
            new[] { eventId, eventAttendeeName },
            events);

        query.AddJoin(
            new Join(
                JoinType.Inner,
                eventAttendee,
                new EqualsCondition(eventId, eventAttendeeEventId)
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id, ea.Name
            FROM Events e
            INNER JOIN EventAttendee ea ON e.Id = ea.EventId
            """,
            sql);
    }
}
