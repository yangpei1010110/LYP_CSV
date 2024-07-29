simple two function

/// <summary>
///     split the csv string line but not keep the quotation marks
///     For example, "a","b","c" will be parsed as a, b, c
///     The quotation marks can be represented by "" such as "a""b","c" will be parsed as a"b, c
/// </summary>
public static IReadOnlyList<string> ParseIgnoreQuotation(string csv, char key = ',')

/// <summary>
///     split the csv string line but keep the quotation marks
///     For example, "a","b","c" will be parsed as "a", "b", "c"
///     The quotation marks can be represented by "" such as "a""b","c" will be parsed as "a"b", "c"
/// </summary>
public static IReadOnlyList<string> Parse(string csv, char key = ',')
