using Newtonsoft.Json;
using System.IO;
using TestTask.Handlers;
using TestTask.Interfaces;

namespace TestTask.Export
{
    public class JsonUser : IUserSave
    {
        public void SaveUser(string path, TableResource user)
        {
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter js = new JsonTextWriter(sw))
            {
                js.Formatting = Newtonsoft.Json.Formatting.Indented;
                js.WriteStartObject();

                js.WritePropertyName("Имя");
                js.WriteValue(user.FIO);

                js.WritePropertyName("Среднее");
                js.WriteValue(user.Average);

                js.WritePropertyName("Лучшее");
                js.WriteValue(user.BestResult);

                js.WritePropertyName("Худшее");
                js.WriteValue(user.WorstResult);

                js.WritePropertyName("Дни");
                js.WriteStartArray();

                for (int i = 0; i < user.Steps.Length; i++)
                {
                    if (user.Steps[i].HasValue)
                    {
                        js.WriteStartObject();

                        js.WritePropertyName("Номер");
                        js.WriteValue(i + 1);

                        js.WritePropertyName("Шаги");
                        js.WriteValue((int)user.Steps[i]);

                        js.WritePropertyName("Ранг");
                        js.WriteValue(user.Ranks[i]);

                        js.WritePropertyName("Статус");
                        js.WriteValue("Finished");

                        js.WriteEndObject();
                    }
                }

                js.WriteEndArray();

                js.WriteEndObject();
            }
        }
    }
}
