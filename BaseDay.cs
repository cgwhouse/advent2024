using System.IO;

namespace advent2024;

public abstract class BaseDay(int day)
{
    protected string[] InputFromFile = File.ReadAllLines($"Inputs/day{day}.txt");
    public string FirstResult => SolveFirst();
    public string SecondResult => SolveSecond();

    protected abstract string SolveFirst();
    protected abstract string SolveSecond();
}
