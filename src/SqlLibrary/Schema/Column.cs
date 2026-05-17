namespace SqlLibrary.Schema;

/// <summary>
/// Representation of a column belonging to a Table.
/// </summary>
public class Column : IValue
{
    public Table Table { get; }
    public string Name { get; }

    public Column(Table table, string name)
    {
        Table = table;
        Name = name;
    }
}
