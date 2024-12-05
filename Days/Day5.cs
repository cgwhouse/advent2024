using System;
using System.Collections.Generic;
using System.Linq;

namespace advent2024.Days;

public class Day5(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        var rulesDict = new Dictionary<int, List<int>>();
        var validUpdates = new List<List<int>>();

        foreach (var line in InputFromFile)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Contains('|'))
            {
                var rule = line.Split('|');
                var first = int.Parse(rule[0]);
                var second = int.Parse(rule[1]);

                if (rulesDict.ContainsKey(first))
                    rulesDict[first].Add(second);
                else
                    rulesDict[first] = [second];

                continue;
            }

            var pageUpdate = line.Split(',');

            // Add to this as we check each thing in the update
            var processedUpdate = new List<int>();
            var valid = true;

            foreach (var pu in pageUpdate)
            {
                var pageNum = int.Parse(pu);

                if (!rulesDict.ContainsKey(pageNum))
                {
                    processedUpdate.Add(pageNum);
                    continue;
                }

                var rules = rulesDict[pageNum];

                foreach (var rule in rules)
                {
                    if (processedUpdate.Contains(rule))
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                    break;

                processedUpdate.Add(pageNum);
            }

            if (valid)
                validUpdates.Add(processedUpdate);
        }

        var result = 0;

        foreach (var update in validUpdates)
            result += update[update.Count / 2];

        return result.ToString();
    }

    protected override string SolveSecond()
    {
        var rulesDict = new Dictionary<int, List<int>>();
        var invalidUpdates = new List<List<int>>();

        foreach (var line in InputFromFile)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Contains('|'))
            {
                var rule = line.Split('|');
                var first = int.Parse(rule[0]);
                var second = int.Parse(rule[1]);

                if (rulesDict.ContainsKey(first))
                    rulesDict[first].Add(second);
                else
                    rulesDict[first] = [second];

                continue;
            }

            var pageUpdate = line.Split(',');

            var processedUpdate = new List<int>();
            var valid = true;

            foreach (var pu in pageUpdate)
            {
                var pageNum = int.Parse(pu);

                if (!rulesDict.ContainsKey(pageNum))
                {
                    processedUpdate.Add(pageNum);
                    continue;
                }

                var rules = rulesDict[pageNum];

                foreach (var rule in rules)
                {
                    if (processedUpdate.Contains(rule))
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                    break;

                processedUpdate.Add(pageNum);
            }

            if (!valid)
                invalidUpdates.Add(processedUpdate);
        }

        var validUpdates = new List<List<int>>();

        foreach (var invalidUpdate in invalidUpdates)
        {
            var validUpdate = new List<int>();

            foreach (var pageNum in invalidUpdate)
            {
                // No rules to follow, or this is the first one
                if (!rulesDict.ContainsKey(pageNum) || validUpdate.Count == 0)
                {
                    validUpdate.Add(pageNum);
                    continue;
                }

                // Get the rules for our current pageNum we're trying to add
                var rules = rulesDict[pageNum];

                // We will look for its location in the list we're currently building
                var currentDesiredIndex = 0;
                var locationFound = false;

                while (!locationFound)
                {
                    // We may have already hit the end
                    if (currentDesiredIndex == validUpdate.Count)
                    {
                        locationFound = true;
                        validUpdate.Add(pageNum);
                    }

                    var test = new List<int>();
                    test.AddRange(validUpdate);
                    test.Insert(currentDesiredIndex, pageNum);

                    var slice = test.Slice(
                        currentDesiredIndex + 1,
                        test.Count - currentDesiredIndex - 1
                    );

                    if (!slice.Any(x => rules.Contains(x)))
                    {
                        locationFound = true;
                        validUpdate = test;
                    }
                    else
                    {
                        currentDesiredIndex++;
                    }
                }
            }

            // TODO: reorder the invalid update

            validUpdates.Add(validUpdate.ToList());
        }

        var result = 0;

        foreach (var update in validUpdates)
            result += update[update.Count / 2];

        return result.ToString();
    }
}
