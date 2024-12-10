using System.IO;

namespace advent2024.Days;

public abstract class BaseDay(int day, bool useSampleInput = false)
{
    protected string[] InputFromFile = useSampleInput
        ? File.ReadAllLines($"Inputs/sample.txt")
        : File.ReadAllLines($"Inputs/day{day}.txt");

    public string FirstResult => SolveFirst();
    public string SecondResult => SolveSecond();

    protected abstract string SolveFirst();
    protected abstract string SolveSecond();
}
