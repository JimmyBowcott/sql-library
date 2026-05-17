namespace SqlLibrary.Query;

/// <summary>
/// The type of JOIN operation, e.g. "INNER JOIN" or "FULL OUTER JOIN"
/// </summary>
public enum JoinType
{
    Inner,
    LeftOuter,
    RightOuter,
    FullOuter,
}
