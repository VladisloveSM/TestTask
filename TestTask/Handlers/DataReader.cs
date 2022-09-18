using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestTask.Handlers;
using TestTask.Templates;

namespace TestTask
{
    public class DataReader
    {
        public const int FILE_AMOUNT = 30;

        private readonly Dictionary<string, StepsInfo> userSteps;

        public Dictionary<string, StepsInfo> UserSteps { get => userSteps; }

        public DataReader()
        {
            this.userSteps = new Dictionary<string, StepsInfo>();
            this.ReadAllData();
        }

        private void ReadAllData()
        {
            for (int i = 0; i < FILE_AMOUNT; i++)
            {
                var result = JsonConvert.DeserializeObject<Man[]>(System.IO.File.ReadAllText(GetPath.GetJsonPath(i + 1)));
                foreach (var person in result)
                {
                    if (person.Status == "Finished")
                    {
                        if (this.userSteps.TryGetValue(person.User, out StepsInfo steps))
                        {
                            steps.Steps[i] = person.Steps;
                            steps.Ranks[i] = person.Rank;
                        }
                        else
                        {
                            int?[] arrSteps = new int?[FILE_AMOUNT];
                            int[] ranks = new int[FILE_AMOUNT];
                            arrSteps[i] = person.Steps;
                            ranks[i] = person.Rank;
                            this.userSteps.Add(person.User, new StepsInfo() { Steps = arrSteps, Ranks = ranks });
                        }
                    }
                }
            }
        }
    }
}
