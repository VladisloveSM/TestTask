using Newtonsoft.Json;

namespace TestTask.Handlers
{
    public class TableResource
    {
        [JsonProperty("Имя")]
        public string FIO { get; set; }

        [JsonProperty("Среднее")]
        public double Average { get; set; }

        [JsonProperty("Лучший")]
        public int BestResult { get; set; }

        [JsonProperty("Худший")]
        public int WorstResult { get; set; }

        [JsonProperty("Шаги")]
        public double?[] Steps { get; set; }

        [JsonProperty("Ранги")]
        public int[] Ranks { get; set; }

        [JsonIgnore]
        public bool IsMore { get; set; }
    }
}
