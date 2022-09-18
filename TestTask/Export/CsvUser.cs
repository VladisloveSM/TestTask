using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Handlers;
using TestTask.Interfaces;

namespace TestTask.Export
{
    public class CsvUser : IUserSave
    {
        public void SaveUser(string path, TableResource user)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("ФИО,Среднее кол-во шагов,Худший результат,Лучший результат");
                sw.WriteLine($"{user.FIO},{user.Average.ToString(CultureInfo.GetCultureInfo("en-US"))},{user.WorstResult},{user.BestResult}");
                sw.WriteLine($"День,Шаги,Ранг,Статус");
                for (int i = 0; i < user.Steps.Length; i++)
                {
                    if (user.Steps[i].HasValue)
                    {
                        sw.WriteLine($"{i + 1},{user.Steps[i]},{user.Ranks[i]},Finished");
                    }
                }
            }
        }
    }
}
