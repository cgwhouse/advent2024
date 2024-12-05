using System;
using System.Collections.Generic;

namespace advent2024.Days;

public class Day5(int day) : BaseDay(day)
{
    protected override string SolveFirst()
    {
        // dictionary of key, value is list of things that must come after
        // for each number in the list of pages
        // look at everything up until the number we're currently on
        // if any of those numbers leading up to current number are in the "must come after"
        // list for that number, it is not correct

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
        throw new NotImplementedException();
    }
}
