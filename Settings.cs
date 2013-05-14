using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CodeBackupUtility
{
    public class Settings
    {
        public string FolderSource { get; set; }
        public string FolderDestination { get; set; }
        public DateTime QualificationDateTime { get; set; }
        public string Label { get; set; }
        public string IgnoreListValues { get; set; }
        public bool IgnoreGit { get; set; }
        public bool IgnoreBin { get; set; }
        public bool IgnoreObj { get; set; }

        //each setting is an individual file

        public Settings()
        {
         
        }

        public void Load(string fileName)
        {
            try
            {
                XDocument doc = XDocument.Load(fileName);
                DateTime qd;
                
                var savedSetting = from s in doc.Descendants("Setting")
                                   select s;

                foreach (var item in savedSetting)
                {

                    {
                        FolderDestination = item.Element("FolderDestination").Value;
                        FolderSource = item.Element("FolderSource").Value;
                        Label = item.Element("Label").Value;
                        IgnoreListValues = item.Element("IgnoreListValues").Value;
                        DateTime.TryParse(item.Element("QualificationDateTime").Value, out qd);
                        QualificationDateTime = qd;
                        IgnoreObj = bool.Parse(item.Element("IgnoreObj").Value);
                        IgnoreBin = bool.Parse(item.Element("IgnoreBin").Value);
                        IgnoreGit = bool.Parse(item.Element("IgnoreGit").Value);
                    };
                }
            }
            catch (Exception e)
            {
                
            }
        }

        public void Save(string fileName)
        {
            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Setting");
                writer.WriteElementString("FolderSource", FolderSource);
                writer.WriteElementString("FolderDestination", FolderDestination);
                writer.WriteElementString("QualificationDateTime", QualificationDateTime.ToString());
                writer.WriteElementString("Label", Label);
                writer.WriteElementString("IgnoreListValues", IgnoreListValues);
                writer.WriteElementString("IgnoreGit", IgnoreGit.ToString());
                writer.WriteElementString("IgnoreBin", IgnoreBin.ToString());
                writer.WriteElementString("IgnoreObj", IgnoreObj.ToString());
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }

    }
}
