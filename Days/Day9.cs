using System;
using System.Collections.Generic;
using System.Linq;

namespace advent2024.Days;

public class Day9(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var diskMap = InputFromFile.First() ?? throw new Exception();

        // First create the expanded version of the disk map with '.' for free space
        var diskMapExpanded = new List<string>();
        var currentID = 0;
        var isFile = true;

        for (int i = 0; i < diskMap.Length; i++)
        {
            var sizeOfThing = int.Parse(diskMap[i].ToString());

            for (int j = 0; j < sizeOfThing; j++)
            {
                if (isFile)
                    diskMapExpanded.Add(currentID.ToString());
                else
                    diskMapExpanded.Add(".");
            }

            if (isFile)
                currentID++;

            isFile = !isFile;
        }

        // As we encounter free space while moving left to right,
        // keep track of where we are as we're pulling values off of the right side
        var currentRight = diskMapExpanded.Count - 1;
        while (diskMapExpanded[currentRight] == ".")
            currentRight--;

        var compactedDiskMap = new List<string>();

        for (int i = 0; i < diskMapExpanded.Count; i++)
        {
            if (i > currentRight)
                break;

            if (diskMapExpanded[i] != ".")
            {
                compactedDiskMap.Add(diskMapExpanded[i]);
                continue;
            }

            // We have a free space, pull something off the right, then add to indices
            compactedDiskMap.Add(diskMapExpanded[currentRight]);

            // Find the next thing on the right we can swap into free space
            currentRight--;
            while (diskMapExpanded[currentRight] == ".")
                currentRight--;
        }

        long result = 0;
        long curr = 0;

        foreach (var s in compactedDiskMap)
        {
            if (s == ".")
                continue;

            result += curr * long.Parse(s.ToString());

            curr++;
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
