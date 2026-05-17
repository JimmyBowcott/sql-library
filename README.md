# SqlLibrary

A strongly-typed SQL query builder which generates T-SQL from C# code.

The library focuses on:

- Strong typing instead of 'magic strings'
- Extensibility

## Features

- SELECT queries
- Table aliases
- Joins (INNER and OUTER)
- WHERE clause
- AND, OR and equality operators

## Usage

SQL statements are composed via an AST, which takes references to objects. For example:

```csharp
var events = new Table("Events", "e");
var eventAttendee = new Table("EventAttendee", "ea");
var attendee = new Table("Attendee", "a");

var eventId = new Column(events, "Id");
var eventName = new Column(events, "Name");

var eventAttendeeEventId = new Column(eventAttendee, "EventId");
var eventAttendeeAttendeeId = new Column(eventAttendee, "AttendeeId");

var attendeeId = new Column(attendee, "Id");
var attendeeName = new Column(attendee, "Name");

var query = new SelectQuery(new[] { eventId, attendeeName });

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
        attendee,
        new ComparisonCondition(
            attendeeId,
            eventAttendeeAttendeeId,
            ComparisonOperator.Equal
        )
    )
);

query.Where(
    new AndCondition(
        new ComparisonCondition(
            eventId,
            new Literal(5),
            ComparisonOperator.GreaterThan
            ),
        new ComparisonCondition(
            attendeeName,
            new Literal("Bob"),
            ComparisonOperator.Equal
            )
    )
);

var sql = new SqlRenderer().Render(query);

```

Will produce the string:

```SQL
SELECT e.Id, a.Name
FROM Events e
INNER JOIN EventAttendee ea ON e.Id = ea.EventId
INNER JOIN Attendee a ON a.Id = ea.AttendeeId
WHERE (e.Id > 5 AND a.Name = 'Bob')
```

## Requirements

- .NET 8 SDK

## Build & test

Clone the repository:

`$ git clone git@github.com:JimmyBowcott/sql-library.git`

Run tests:

`$ dotnet test`

Build:

`$ dotnet build`
