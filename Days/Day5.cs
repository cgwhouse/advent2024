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

                if (rulesDict.TryGetValue(first, out var rules))
                    rules.Add(second);
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
                    if (processedUpdate.Contains(rule))
                        valid = false;

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
                if (!rulesDict.ContainsKey(pageNum) || validUpdate.Count == 0)
                {
                    validUpdate.Add(pageNum);
                    continue;
                }

                var currentDesiredIndex = 0;

                while (true)
                {
                    if (currentDesiredIndex == invalidUpdate.Count)
                    {
                        validUpdate.Add(pageNum);
                        break;
                    }

                    var testAtCurrIndex = new List<int>();
                    testAtCurrIndex.AddRange(validUpdate);
                    testAtCurrIndex.Insert(currentDesiredIndex, pageNum);

                    var seen = new List<int>();
                    var valid = true;

                    foreach (var test in testAtCurrIndex)
                    {
                        if (!rulesDict.ContainsKey(test))
                        {
                            seen.Add(test);
                            continue;
                        }

                        var rules = rulesDict[test];

                        if (seen.Any(rules.Contains))
                        {
                            valid = false;
                            break;
                        }

                        seen.Add(test);
                    }

                    if (valid)
                    {
                        validUpdate.Insert(currentDesiredIndex, pageNum);
                        break;
                    }
                    else
                        currentDesiredIndex++;
                }
            }

            validUpdates.Add([.. validUpdate]);
        }

        var result = 0;

        foreach (var update in validUpdates)
            result += update[update.Count / 2];

        return result.ToString();
    }
}
