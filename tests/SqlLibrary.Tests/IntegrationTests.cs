using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class IntegrationTests
{
    [Fact]
    public void RendersComplexQuery()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");
        var attendees = new Table("Attendees", "a");

        var eventId = new Column(events, "Id");
        var eventName = new Column(events, "Name");
        var started = new Column(events, "Started");

        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeAttendeeId = new Column(eventAttendee, "AttendeeId");

        var attendeeId = new Column(attendees, "Id");
        var attendeeName = new Column(attendees, "Name");
        var attendeeHighScore = new Column(attendees, "HighScore");

        var query = new SelectQuery(
            new[] { eventId, eventName, attendeeName },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                eventAttendee,
                new AndCondition(
                    new ComparisonCondition(
                        eventId,
                        eventAttendeeEventId,
                        ComparisonOperator.Equal
                    ),
                    new ComparisonCondition(
                        started,
                        new Literal(1),
                        ComparisonOperator.Equal
                    )
                )
            )
        );

        query.Join(
            new JoinClause(
                JoinType.LeftOuter,
                attendees,
                new ComparisonCondition(
                    eventAttendeeAttendeeId,
                    attendeeId,
                    ComparisonOperator.Equal
                )
            )
        );

        query.Where(
            new OrCondition(
                new ComparisonCondition(
                    attendeeName,
                    new Literal("Bob"),
                    ComparisonOperator.Equal
                ),
                new ComparisonCondition(
                    attendeeHighScore,
                    new Literal(10),
                    ComparisonOperator.GreaterThan
                )
            )
        );

        var renderer = new SqlRenderer();

        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id, e.Name, a.Name
            FROM Events e
            INNER JOIN EventAttendee ea ON (e.Id = ea.EventId AND e.Started = 1)
            LEFT OUTER JOIN Attendees a ON ea.AttendeeId = a.Id
            WHERE (a.Name = 'Bob' OR a.HighScore > 10)
            """,
            sql);
    }  
}
