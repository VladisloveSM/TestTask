using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestTask.Handlers;
using TestTask.Interfaces;

namespace TestTask.Export
{
    public class XmlUser : IUserSave
    {
        public void SaveUser(string path, TableResource user)
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
    }
}
