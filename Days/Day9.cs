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

            // We have a free space, pull something off the right
            compactedDiskMap.Add(diskMapExpanded[currentRight]);

            // Find the next thing on the right we can swap into free space
            currentRight--;
            while (diskMapExpanded[currentRight] == ".")
                currentRight--;
        }

        long result = 0;

        for (int i = 0; i < compactedDiskMap.Count; i++)
            result += i * long.Parse(compactedDiskMap[i].ToString());

        return result.ToString();
    }

    protected override string SolveSecond()
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

        // Start at highest ID and try to move each one
        currentID--;
        while (currentID > -1)
        {
            var startIndexOfBlock = diskMapExpanded.IndexOf(currentID.ToString());

            if (startIndexOfBlock == 0)
            {
                currentID--;
                continue;
            }

            // Figure out how long this ID's block is
            var lengthOfBlockToMove = 0;
            var currentIndex = startIndexOfBlock;
            while (diskMapExpanded[currentIndex] == currentID.ToString())
            {
                lengthOfBlockToMove++;
                currentIndex++;

                if (currentIndex == diskMapExpanded.Count)
                    break;
            }

            var currentFreeSpace = new List<string>();
            var currentBlockIsFile = true;

            // Starting from the beginning of diskMapExpanded, look for free space to the left that can fit it
            // if we pass it, we can't move it
            for (int i = 0; i < startIndexOfBlock + 1; i++)
            {
                // We think we're processing a file block but just found free space
                if (currentBlockIsFile)
                {
                    if (diskMapExpanded[i] == ".")
                    {
                        // We have been processing a file block, but have now found free space,
                        // flip flag and restart currentFreeSpace
                        currentBlockIsFile = false;

                        currentFreeSpace.Clear();
                        currentFreeSpace.Add(".");
                    }

                    continue;
                }

                // Current block is free space, if this is more free space add to current
                if (diskMapExpanded[i] == ".")
                {
                    currentFreeSpace.Add(diskMapExpanded[i]);
                    continue;
                }

                // We have been processing a free space block, but have now found
                // a file again, check if currentFreeSpace is big enough for the move,
                // continue if not
                if (!(currentFreeSpace.Count >= lengthOfBlockToMove))
                {
                    currentBlockIsFile = true;

                    currentFreeSpace.Clear();
                    currentFreeSpace.Add(diskMapExpanded[i]);
                    continue;
                }

                // We have found a free space block that has enough room, do replacement
                // Populate free space with file blocks
                var replacementIndex = i - currentFreeSpace.Count;
                for (int j = 0; j < lengthOfBlockToMove; j++)
                {
                    diskMapExpanded[replacementIndex] = currentID.ToString();
                    replacementIndex++;
                }

                // Wipe out old file block location
                for (int j = 0; j < lengthOfBlockToMove; j++)
                {
                    diskMapExpanded[startIndexOfBlock] = ".";
                    startIndexOfBlock++;
                }

                // Exit, we moved this one successfully
                break;
            }

            currentID--;
        }

        long result = 0;

        for (int i = 0; i < diskMapExpanded.Count; i++)
        {
            if (diskMapExpanded[i] == ".")
                continue;

            result += i * long.Parse(diskMapExpanded[i]);
        }

        return result.ToString();
    }
}
