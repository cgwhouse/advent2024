namespace advent2024.Days;

public class Day3(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var result = 0;

        foreach (var line in InputFromFile)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (i + 4 >= line.Length)
                    break;

                if (line.Substring(i, 4) != "mul(")
                    continue;

                // See if we get several ints followed by a comma
                var first = "";
                var curr = i + 4;

                while (int.TryParse(line[curr].ToString(), out var digit))
                {
                    first += digit;
                    curr++;
                }

                // We need to now be on a comma, with ints accumulated
                if (line[curr] != ',' || !int.TryParse(first, out var firstInt))
                    continue;

                // Move past the comma and try to collect secondInt
                curr++;

                var second = "";

                while (int.TryParse(line[curr].ToString(), out var digit))
                {
                    second += digit;
                    curr++;
                }

                if (line[curr] != ')' || !int.TryParse(second, out var secondInt))
                    continue;

                result += firstInt * secondInt;
            }
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        var result = 0;
        var enabled = true;

        foreach (var line in InputFromFile)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (i + 7 >= line.Length)
                    break;

                if (line.Substring(i, 4) != "mul(")
                {
                    if (line.Substring(i, 4) == "do()" && !enabled)
                        enabled = true;
                    else if (line.Substring(i, 7) == "don't()" && enabled)
                        enabled = false;

                    continue;
                }

                // See if we get several ints followed by a comma
                var first = "";
                var curr = i + 4;

                while (int.TryParse(line[curr].ToString(), out var digit))
                {
                    first += digit;
                    curr++;
                }

                // We need to now be on a comma, with ints accumulated
                if (line[curr] != ',' || !int.TryParse(first, out var firstInt))
                    continue;

                // Move past the comma and try to collect secondInt
                curr++;

                var second = "";

                while (int.TryParse(line[curr].ToString(), out var digit))
                {
                    second += digit;
                    curr++;
                }

                if (line[curr] != ')' || !int.TryParse(second, out var secondInt))
                    continue;

                if (enabled)
                    result += firstInt * secondInt;
            }
        }

        return result.ToString();
    }
}
