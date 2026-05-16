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

    [Fact]
    public void RendersJoinWithAndCondition()
    {
        var events = new Table("Events", "e");
        var attendees = new Table("Attendees", "a");

        var eventId = new Column(events, "Id");
        var attendeeEventId = new Column(attendees, "EventId");

        var ownerId = new Column(events, "OwnerId");
        var attendeeOwnerId = new Column(attendees, "OwnerId");

        var condition =
            new AndCondition(
                new EqualsCondition(eventId, attendeeEventId),
                new EqualsCondition(ownerId, attendeeOwnerId)
            );

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.AddJoin(
            new Join(
                JoinType.Inner,
                attendees,
                condition
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id
            FROM Events e
            INNER JOIN Attendees a ON (e.Id = a.EventId AND e.OwnerId = a.OwnerId)
            """,
            sql);
    }

    [Fact]
    public void RendersJoinWithOrCondition()
    {
        var events = new Table("Events", "e");
        var attendees = new Table("Attendees", "a");

        var eventId = new Column(events, "Id");
        var attendeeEventId = new Column(attendees, "EventId");

        var ownerId = new Column(events, "OwnerId");
        var attendeeOwnerId = new Column(attendees, "OwnerId");

        var condition =
            new OrCondition(
                new EqualsCondition(eventId, attendeeEventId),
                new EqualsCondition(ownerId, attendeeOwnerId)
            );

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.AddJoin(
            new Join(
                JoinType.Inner,
                attendees,
                condition
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id
            FROM Events e
            INNER JOIN Attendees a ON (e.Id = a.EventId OR e.OwnerId = a.OwnerId)
            """,
            sql);
    }
}
