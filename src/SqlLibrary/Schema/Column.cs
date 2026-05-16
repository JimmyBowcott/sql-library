namespace SqlLibrary.Schema;

public class Column
{
    public Table Table { get; }
    public string Name { get; }

    public Column(Table table, string name)
    {
        Table = table;
        Name = name;
    }
}
