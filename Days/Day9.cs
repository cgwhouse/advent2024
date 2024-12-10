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
        var diskMapExpanded = "";
        var currentID = 0;
        var isFile = true;

        for (int i = 0; i < diskMap.Length; i++)
        {
            var sizeOfThing = int.Parse(diskMap[i].ToString());

            for (int j = 0; j < sizeOfThing; j++)
            {
                if (isFile)
                    diskMapExpanded += currentID.ToString();
                else
                    diskMapExpanded += '.';
            }

            if (isFile)
                currentID++;

            isFile = !isFile;
        }

        // As we encounter free space while moving left to right,
        // keep track of where we are as we're pulling values off of the right side
        var currentRight = diskMapExpanded.Length - 1;
        while (diskMapExpanded[currentRight] == '.')
            currentRight--;

        var compactedDiskMap = "";
        var indicesAlreadyCompacted = new HashSet<int>();

        for (int i = 0; i < diskMapExpanded.Length; i++)
        {
            if (i > currentRight || indicesAlreadyCompacted.Contains(i))
            {
                compactedDiskMap += '.';
                continue;
            }

            if (diskMapExpanded[i] != '.')
            {
                compactedDiskMap += diskMapExpanded[i];
                continue;
            }

            // We have a free space, pull something off the right, then add to indices
            compactedDiskMap += diskMapExpanded[currentRight];
            indicesAlreadyCompacted.Add(currentRight);

            // Find the next thing on the right we can swap into free space
            currentRight--;
            while (diskMapExpanded[currentRight] == '.')
                currentRight--;
        }

        //Console.WriteLine(compactedDiskMap);

        long result = 0;

        for (int i = 0; i < compactedDiskMap.Length; i++)
        {
            if (compactedDiskMap[i] == '.')
                continue;

            result += int.Parse(compactedDiskMap[i].ToString()) * i;
        }

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        throw new NotImplementedException();
    }
}
