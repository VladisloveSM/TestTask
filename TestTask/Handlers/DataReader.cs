using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestTask.Handlers;

namespace TestTask
{
    public class DataReader
    {
        public const int FILE_AMOUNT = 30;

        private readonly Dictionary<string, Tuple<int?[], int[]>> userSteps;

        public Dictionary<string, Tuple<int?[], int[]>> UserSteps { get => userSteps; }

        public DataReader()
        {
            this.userSteps = new Dictionary<string, Tuple<int?[], int[]>>();
            this.ReadAllData();
        }

        private void ReadAllData()
        {
            for (int i = 0; i < FILE_AMOUNT; i++)
            {
                var result = JsonConvert.DeserializeObject<Man[]>(System.IO.File.ReadAllText(GetPath.GetJsonPath(i + 1)));
                foreach (var person in result)
                {
                    if (this.userSteps.TryGetValue(person.User, out Tuple<int?[], int[]> steps))
                    {
                        steps.Item1[i] = person.Steps;
                        steps.Item2[i] = person.Rank;
                    }
                    else
                    {
                        int?[] arrSteps = new int?[FILE_AMOUNT];
                        int[] ranks = new int[FILE_AMOUNT];
                        arrSteps[i] = person.Steps;
                        ranks[i] = person.Rank;
                        this.userSteps.Add(person.User, new Tuple<int?[], int[]>(arrSteps, ranks));
                    }
                }
            }
        }
    }
}
