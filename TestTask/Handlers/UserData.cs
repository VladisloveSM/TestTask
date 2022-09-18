using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Handlers
{
    public class UserData
    {
        private readonly TableResource[] resources;

        public TableResource[] Resources { get => resources; }

        public UserData(Dictionary<string, Tuple<int?[], int[]>> dictionary)
        {
            this.resources = new TableResource[dictionary.Count];
            int index = 0;
            foreach (var user in dictionary)
            {
                this.resources[index++] = GetElement(user.Key, user.Value);
            }
        }

        private TableResource GetElement(string name, Tuple<int?[], int[]> stepRanks)
        {
            var average = stepRanks.Item1.Where(i => i != null).Average().Value;
            var max = stepRanks.Item1.Max().Value;
            var min = stepRanks.Item1.Min().Value;
            var Steps = ToDoubleArray(stepRanks.Item1);
            var isMore = SetIsMore(max, min, average);
            return new TableResource
            {
                FIO = name,
                Average = average,
                Steps = Steps,
                BestResult = max,
                WorstResult = min,
                IsMore = isMore,
                Ranks = stepRanks.Item2
            };
        }

        private double?[] ToDoubleArray(int?[] arr)
        {
            double?[] result = new double?[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = arr[i];
            }
            return result;
        }

        private bool SetIsMore(int max, int min, double average) => (1 - average / max > 0.2 || 1 - (double)min / average > 0.2);
    }
}
