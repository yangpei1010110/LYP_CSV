// simple two function


// split the csv string line but not keep the quotation marks
// For example, "a","b","c" will be parsed as a, b, c
// The quotation marks can be represented by "" such as "a""b","c" will be parsed as a"b, c

public static IReadOnlyList<string> ParseIgnoreQuotation(string csv, char key = ',')


// split the csv string line but keep the quotation marks
// For example, "a","b","c" will be parsed as "a", "b", "c"
// The quotation marks can be represented by "" such as "a""b","c" will be parsed as "a"b", "c"

public static IReadOnlyList<string> Parse(string csv, char key = ',')
