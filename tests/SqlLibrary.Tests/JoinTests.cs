using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class JoinTests
{
    [Fact]
    public void RendersInnerJoin()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");

        var eventId = new Column(events, "Id");
        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeName = new Column(eventAttendee, "Name");

        var query = new SelectQuery(
            new[] { eventId, eventAttendeeName },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                eventAttendee,
                new ComparisonCondition(
                    eventId,
                    eventAttendeeEventId,
                    ComparisonOperator.Equal
                    )
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
    public void RendersLeftOuterJoin()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");

        var eventId = new Column(events, "Id");
        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeName = new Column(eventAttendee, "Name");

        var query = new SelectQuery(
            new[] { eventId, eventAttendeeName },
            events);

        query.Join(
            new JoinClause(
                JoinType.LeftOuter,
                eventAttendee,
                new ComparisonCondition(
                    eventId,
                    eventAttendeeEventId,
                    ComparisonOperator.Equal
                    )
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id, ea.Name
            FROM Events e
            LEFT OUTER JOIN EventAttendee ea ON e.Id = ea.EventId
            """,
            sql);
    }

    [Fact]
    public void RendersRightOuterJoin()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");

        var eventId = new Column(events, "Id");
        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeName = new Column(eventAttendee, "Name");

        var query = new SelectQuery(
            new[] { eventId, eventAttendeeName },
            events);

        query.Join(
            new JoinClause(
                JoinType.RightOuter,
                eventAttendee,
                new ComparisonCondition(
                    eventId,
                    eventAttendeeEventId,
                    ComparisonOperator.Equal
                    )
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id, ea.Name
            FROM Events e
            RIGHT OUTER JOIN EventAttendee ea ON e.Id = ea.EventId
            """,
            sql);
    }

    [Fact]
    public void RendersFullOuterJoin()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");

        var eventId = new Column(events, "Id");
        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeName = new Column(eventAttendee, "Name");

        var query = new SelectQuery(
            new[] { eventId, eventAttendeeName },
            events);

        query.Join(
            new JoinClause(
                JoinType.FullOuter,
                eventAttendee,
                new ComparisonCondition(
                    eventId,
                    eventAttendeeEventId,
                    ComparisonOperator.Equal
                    )
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id, ea.Name
            FROM Events e
            FULL OUTER JOIN EventAttendee ea ON e.Id = ea.EventId
            """,
            sql);
    }

    [Fact]
    public void RendersMultipleJoins()
    {
        var events = new Table("Events", "e");
        var eventAttendee = new Table("EventAttendee", "ea");
        var attendees = new Table("Attendees", "a");

        var eventId = new Column(events, "Id");
        var eventAttendeeEventId = new Column(eventAttendee, "EventId");
        var eventAttendeeAttendeeId = new Column(eventAttendee, "AttendeeId");
        var attendeeId = new Column(attendees, "Id");

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                eventAttendee,
                new ComparisonCondition(
                    eventId,
                    eventAttendeeEventId,
                    ComparisonOperator.Equal
                )
            )
        );

        query.Join(
            new JoinClause(
                JoinType.Inner,
                attendees,
                new ComparisonCondition(
                    eventAttendeeAttendeeId,
                    attendeeId,
                    ComparisonOperator.Equal
                )
            )
        );

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.Id
            FROM Events e
            INNER JOIN EventAttendee ea ON e.Id = ea.EventId
            INNER JOIN Attendees a ON ea.AttendeeId = a.Id
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

        var eventTheme = new Column(events, "Theme");
        var attendeeTheme = new Column(attendees, "FavouriteTheme");

        var condition =
            new AndCondition(
                new ComparisonCondition(
                    eventId,
                    attendeeEventId,
                    ComparisonOperator.Equal
                    ),
                new ComparisonCondition(
                    eventTheme,
                    attendeeTheme,
                    ComparisonOperator.Equal
                    )
            );

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.Join(
            new JoinClause(
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
            INNER JOIN Attendees a ON (e.Id = a.EventId AND e.Theme = a.FavouriteTheme)
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

        var eventTheme = new Column(events, "Theme");
        var attendeeTheme = new Column(attendees, "FavouriteTheme");

        var condition =
            new OrCondition(
                new ComparisonCondition(
                    eventId,
                    attendeeEventId,
                    ComparisonOperator.Equal
                    ),
                new ComparisonCondition(
                    eventTheme,
                    attendeeTheme,
                    ComparisonOperator.Equal
                    )
            );

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.Join(
            new JoinClause(
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
            INNER JOIN Attendees a ON (e.Id = a.EventId OR e.Theme = a.FavouriteTheme)
            """,
            sql);
    }

    [Fact]
    public void RendersGreaterThanCondition()
    {
        var events = new Table("Events", "e");
        var attendees = new Table("Attendees", "a");

        var eventHighScore = new Column(events, "HighScore");
        var attendeeScore = new Column(attendees, "Score");

        var condition =
            new ComparisonCondition(
                attendeeScore,
                eventHighScore,
                ComparisonOperator.GreaterThan
            );

        var query = new SelectQuery(
            new[] { eventHighScore },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                attendees,
                condition
            )
        );

        var renderer = new SqlRenderer();

        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.HighScore
            FROM Events e
            INNER JOIN Attendees a ON a.Score > e.HighScore
            """,
            sql);
    }

    [Fact]
    public void RendersGreaterThanOrEqualsCondition()
    {
        var events = new Table("Events", "e");
        var attendees = new Table("Attendees", "a");

        var eventHighScore = new Column(events, "HighScore");
        var attendeeScore = new Column(attendees, "Score");

        var condition =
            new ComparisonCondition(
                attendeeScore,
                eventHighScore,
                ComparisonOperator.GreaterThanOrEqual
            );

        var query = new SelectQuery(
            new[] { eventHighScore },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                attendees,
                condition
            )
        );

        var renderer = new SqlRenderer();

        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.HighScore
            FROM Events e
            INNER JOIN Attendees a ON a.Score >= e.HighScore
            """,
            sql);
    }

    [Fact]
    public void RendersLessThanCondition()
    {
        var events = new Table("Events", "e");
        var attendees = new Table("Attendees", "a");

        var eventHighScore = new Column(events, "HighScore");
        var attendeeScore = new Column(attendees, "Score");

        var condition =
            new ComparisonCondition(
                attendeeScore,
                eventHighScore,
                ComparisonOperator.LessThan
            );

        var query = new SelectQuery(
            new[] { eventHighScore },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                attendees,
                condition
            )
        );

        var renderer = new SqlRenderer();

        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.HighScore
            FROM Events e
            INNER JOIN Attendees a ON a.Score < e.HighScore
            """,
            sql);
    }

    [Fact]
    public void RendersLessThanOrEqualsCondition()
    {
        var events = new Table("Events", "e");
        var attendees = new Table("Attendees", "a");

        var eventHighScore = new Column(events, "HighScore");
        var attendeeScore = new Column(attendees, "Score");

        var condition =
            new ComparisonCondition(
                attendeeScore,
                eventHighScore,
                ComparisonOperator.LessThanOrEqual
            );

        var query = new SelectQuery(
            new[] { eventHighScore },
            events);

        query.Join(
            new JoinClause(
                JoinType.Inner,
                attendees,
                condition
            )
        );

        var renderer = new SqlRenderer();

        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT e.HighScore
            FROM Events e
            INNER JOIN Attendees a ON a.Score <= e.HighScore
            """,
            sql);
    }
}
