using Newtonsoft.Json;
using System.IO;
using System.Xml;
using TestTask.Handlers;

namespace TestTask.Export
{
    public static class SaveFile
    {
        public static void SaveUserInfo(string path, string format, TableResource user)
        {
            switch (format)
            {
                case "csv":
                    WriteToCsv(path, user);
                    break;
                case "xml":
                    WriteToXml(path, user);
                    break;
                case "json":
                    WriteToJson(path, user);
                    break;
            }
        }

        private static void WriteToCsv(string path, TableResource user)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine($"ФИО: {user.FIO}\nСреднее кол-во шагов: {user.Average}\nХудший результат: {user.WorstResult}\nЛучший реультат: {user.BestResult}.");
                for (int i = 0; i < user.Steps.Length; i++)
                {
                    if (user.Steps[i].HasValue) 
                    {
                        sw.WriteLine($"День {i + 1}: Шаги: {user.Steps[i]}, Ранг: {user.Ranks[i]}, Статус: Finished");
                    }
                }
            }
        }

        private static void WriteToXml(string path, TableResource user)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter xmlWrite = XmlWriter.Create(path, settings))
            {
                xmlWrite.WriteStartDocument();
                xmlWrite.WriteStartElement("Пользователь");

                xmlWrite.WriteStartElement("Имя");
                xmlWrite.WriteString(user.FIO);
                xmlWrite.WriteEndElement();

                xmlWrite.WriteStartElement("Среднее");
                xmlWrite.WriteString(user.Average.ToString());
                xmlWrite.WriteEndElement();

                xmlWrite.WriteStartElement("Худший");
                xmlWrite.WriteString(user.WorstResult.ToString());
                xmlWrite.WriteEndElement();

                xmlWrite.WriteStartElement("Лучший");
                xmlWrite.WriteString(user.BestResult.ToString());
                xmlWrite.WriteEndElement();

                for (int i = 0; i < user.Steps.Length; i++)
                {
                    if (user.Steps[i].HasValue)
                    {
                        xmlWrite.WriteStartElement("День");
                        xmlWrite.WriteAttributeString("Номер", (i + 1).ToString());

                        xmlWrite.WriteStartElement("Шаги");
                        xmlWrite.WriteString(user.Steps[i].ToString());
                        xmlWrite.WriteEndElement();

                        xmlWrite.WriteStartElement("Ранг");
                        xmlWrite.WriteString(user.Ranks[i].ToString());
                        xmlWrite.WriteEndElement();

                        xmlWrite.WriteStartElement("Статус");
                        xmlWrite.WriteString("Finished");
                        xmlWrite.WriteEndElement();

                        xmlWrite.WriteEndElement();
                    }
                }

                xmlWrite.WriteEndElement();
                xmlWrite.WriteEndDocument();
            }
        }

        private static void WriteToJson(string path, TableResource user)
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
