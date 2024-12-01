using System.IO;

namespace advent2024;

public abstract class BaseDay
{
    protected string[] InputFromFile;
    public string FirstResult => SolveFirst();
    public string SecondResult => SolveSecond();

    public BaseDay(int day) => InputFromFile = File.ReadAllLines($"Inputs/day{day}.txt");

    protected abstract string SolveFirst();
    protected abstract string SolveSecond();
}
