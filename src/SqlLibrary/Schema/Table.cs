namespace SqlLibrary.Schema;

public class Table
{
    public string Name { get; }
    public string? Alias { get; }

    public Table(string name)
    {
        Name = name;
    }

    public Table(string name, string alias)
    {
        Name = name;
        Alias = alias;
    }
}
