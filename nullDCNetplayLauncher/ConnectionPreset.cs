using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace nullDCNetplayLauncher
{
    public class ConnectionPresetList
    {
        public List<ConnectionPreset> ConnectionPresets = new List<ConnectionPreset>();
    }
    public class ConnectionPreset
    {
        public string Name;
        public string IP;
        public string Port;
        public decimal Delay;
        public int Method;

        public override string ToString()
        {
            return Name;
        }

        public static ConnectionPresetList ReadPresetsFile()
        {
            string path = Launcher.GetDistributionRootDirectoryName() + "//ConnectionPresetList.xml";
            ConnectionPresetList readPresetList;
            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(ConnectionPresetList));
                FileStream fs = new FileStream(path, FileMode.Open);

                readPresetList = (ConnectionPresetList)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                ConnectionPreset defaultPreset = new ConnectionPreset
                {
                    Name = "Default",
                    IP = "127.0.0.1",
                    Port = "27886",
                    Delay = 1,
                    Method = 0
                };
                readPresetList = new ConnectionPresetList();
                readPresetList.ConnectionPresets.Add(defaultPreset);

                // generates a new file if it does not exist
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(ConnectionPresetList));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer.BaseStream, readPresetList);
                writer.Close();
            }
            return readPresetList;
        }
    }
}
