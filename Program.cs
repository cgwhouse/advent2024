using System;

if (!int.TryParse(args[0], out var day) || !int.TryParse(args[1], out var part))
{
    Console.WriteLine("Arguments must be ints");
    return;
}

var dayType =
    Type.GetType($"advent2022.Day{day}")
    ?? throw new Exception($"Unable to find implementation for day {day}");

var dayInstance =
    (BaseDay?)Activator.CreateInstance(dayType)
    ?? throw new Exception($"Unable to calculate solution for day {day}");

string result;
switch (part)
{
    case 1:
        result = dayInstance.FirstResult;
        break;
    case 2:
        result = dayInstance.SecondResult;
        break;
    default:
        throw new Exception(
            "Provide a 1 or 2 for the second argument (part 1 or 2 of the solution)"
        );
}

Console.WriteLine($"Result: {result}");
