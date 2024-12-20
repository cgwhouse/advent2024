﻿using System;
using advent2024.Days;

namespace advent2024;

public class Program
{
    public static bool UseSampleInput = false;

    public static void Main(string[] args)
    {
        if (!int.TryParse(args[0], out var day) || !int.TryParse(args[1], out var part))
        {
            Console.WriteLine("Arguments must be ints");
            return;
        }

        var dayType =
            Type.GetType($"advent2024.Days.Day{day}")
            ?? throw new Exception($"Unable to find implementation for day {day}");

        // Check whether to use sample input instead of real one
        if (args.Length > 2 && args[2] == "true")
            UseSampleInput = true;

        var dayInstance =
            (BaseDay?)Activator.CreateInstance(dayType, day)
            ?? throw new Exception($"Unable to calculate solution for day {day}");

        string result;

        result = part switch
        {
            1 => dayInstance.FirstResult,
            2 => dayInstance.SecondResult,
            _ => throw new Exception(
                "Provide a 1 or 2 for the second argument (part 1 or 2 of the solution)"
            ),
        };

        Console.WriteLine($"Result: {result}");
    }
}
