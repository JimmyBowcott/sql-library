using SqlLibrary.Query;
using SqlLibrary.Rendering;
using SqlLibrary.Schema;

namespace SqlLibrary.Tests;

public class ConditionTests
{
    [Fact]
    public void RendersInt()
    {
        var events = new Table("Events");
        var highScore = new Column(events, "HighScore");

        var condition =
            new ComparisonCondition(
                highScore,
                new Literal(10),
                ComparisonOperator.Equal
            );

        var query = new SelectQuery(
            new[] { highScore },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT HighScore
            FROM Events
            WHERE HighScore = 10
            """,
            sql);
    }

    [Fact]
    public void RendersString()
    {
        var events = new Table("Events");
        var theme = new Column(events, "Theme");

        var condition =
            new ComparisonCondition(
                theme,
                new Literal("Forest"),
                ComparisonOperator.Equal
            );

        var query = new SelectQuery(
            new[] { theme },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT Theme
            FROM Events
            WHERE Theme = 'Forest'
            """,
            sql);
    }

    [Fact]
    public void RendersBoolean()
    {
        var events = new Table("Events");
        var started = new Column(events, "Started");

        var condition =
            new ComparisonCondition(
                started,
                new Literal(true),
                ComparisonOperator.Equal
            );

        var query = new SelectQuery(
            new[] { started },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT Started
            FROM Events
            WHERE Started = 1
            """,
            sql);
    }

    [Fact]
    public void RendersGreaterThanCondition()
    {
        var events = new Table("Events");
        var highScore = new Column(events, "HighScore");

        var condition =
            new ComparisonCondition(
                highScore,
                new Literal(10),
                ComparisonOperator.GreaterThan
            );

        var query = new SelectQuery(
            new[] { highScore },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT HighScore
            FROM Events
            WHERE HighScore > 10
            """,
            sql);
    }

    [Fact]
    public void RendersGreaterThanOrEqualsCondition()
    {
        var events = new Table("Events");
        var highScore = new Column(events, "HighScore");

        var condition =
            new ComparisonCondition(
                highScore,
                new Literal(10),
                ComparisonOperator.GreaterThanOrEqual
            );

        var query = new SelectQuery(
            new[] { highScore },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT HighScore
            FROM Events
            WHERE HighScore >= 10
            """,
            sql);
    }

    [Fact]
    public void RendersLessThanCondition()
    {
        var events = new Table("Events");
        var highScore = new Column(events, "HighScore");

        var condition =
            new ComparisonCondition(
                highScore,
                new Literal(10),
                ComparisonOperator.LessThan
            );

        var query = new SelectQuery(
            new[] { highScore },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT HighScore
            FROM Events
            WHERE HighScore < 10
            """,
            sql);
    }

    [Fact]
    public void RendersLessThanOrEqualsCondition()
    {
        var events = new Table("Events");
        var highScore = new Column(events, "HighScore");

        var condition =
            new ComparisonCondition(
                highScore,
                new Literal(10),
                ComparisonOperator.LessThanOrEqual
            );

        var query = new SelectQuery(
            new[] { highScore },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT HighScore
            FROM Events
            WHERE HighScore <= 10
            """,
            sql);
    }

    [Fact]
    public void RendersAndCondition()
    {
        var events = new Table("Events");
        var eventId = new Column(events, "Id");
        var eventTheme = new Column(events, "Theme");

        var condition =
            new AndCondition(
                new ComparisonCondition(
                    eventId,
                    new Literal(5),
                    ComparisonOperator.GreaterThan
                    ),
                new ComparisonCondition(
                    eventTheme,
                    new Literal("Forest"),
                    ComparisonOperator.Equal
                    )
            );

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT Id
            FROM Events
            WHERE (Id > 5 AND Theme = 'Forest')
            """,
            sql);
    }

    [Fact]
    public void RendersOrCondition()
    {
        var events = new Table("Events");
        var eventId = new Column(events, "Id");
        var eventTheme = new Column(events, "Theme");

        var condition =
            new OrCondition(
                new ComparisonCondition(
                    eventId,
                    new Literal(5),
                    ComparisonOperator.GreaterThan
                    ),
                new ComparisonCondition(
                    eventTheme,
                    new Literal("Forest"),
                    ComparisonOperator.Equal
                    )
            );

        var query = new SelectQuery(
            new[] { eventId },
            events);

        query.Where(condition);

        var renderer = new SqlRenderer();
        var sql = renderer.Render(query);

        Assert.Equal(
            """
            SELECT Id
            FROM Events
            WHERE (Id > 5 OR Theme = 'Forest')
            """,
            sql);
    }
}

