using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace nullDCNetplayLauncher
{
    public class GamePadMappingList
    {
        public List<GamePadMapping> GamePadMappings = new List<GamePadMapping>();
    }
    public class GamePadMapping
    {
        public string Name;

        public string Y { get; set; } = "";
        public string A { get; set; } = "";
        public string Back { get; set; } = "";
        public string X { get; set; } = "";
        public string B { get; set; } = "";
        public string Start { get; set; } = "";
        public string LeftStick { get; set; } = "";
        public string RightStick { get; set; } = "";
        public string LeftShoulder { get; set; } = "";
        public string RightShoulder { get; set; } = "";
        public string LeftTrigger { get; set; } = "";
        public string RightTrigger { get; set; } = "";
        public string IsUp { get; set; } = "";
        public string IsDown { get; set; } = "";
        public string IsLeft { get; set; } = "";
        public string IsRight { get; set; } = "";
        public string BigButton { get; set; } = "";

        public Boolean Default { get; set; } = false;

        public object this[string propertyName]
        {
            get
            {
                if (propertyName.Equals("Left"))
                    propertyName = "LeftTrigger";
                else if(propertyName.Equals("Right"))
                    propertyName = "RightTrigger";
                System.Diagnostics.Debug.WriteLine(propertyName);
                PropertyInfo property = GetType().GetProperty(propertyName);
                return property.GetValue(this, null);
            }
            set
            {
                if (propertyName.Equals("Left"))
                    propertyName = "LeftTrigger";
                else if (propertyName.Equals("Right"))
                    propertyName = "RightTrigger";
                System.Diagnostics.Debug.WriteLine(propertyName);
                PropertyInfo property = GetType().GetProperty(propertyName);
                property.SetValue(this, value, null);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public static GamePadMappingList ReadMappingsFile()
        {
            string path = Launcher.rootDir + "GamePadMappingList.xml";
            GamePadMappingList readMappingList;
            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(GamePadMappingList));
                FileStream fs = new FileStream(path, FileMode.Open);

                readMappingList = (GamePadMappingList)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                GamePadMapping defaultMapping = new GamePadMapping
                {
                    Name = "Default",
                    IsUp = "Up",
                    IsDown = "Down",
                    IsLeft = "Left",
                    IsRight = "Right",
                    A = "1",
                    Y = "2",
                    Back = "3",
                    B = "4",
                    X = "5",
                    Start = "6",
                    LeftStick = "Coin",
                    RightStick = "Start",
                    LeftShoulder = "",
                    RightShoulder = "",
                    LeftTrigger = "",
                    RightTrigger = "",
                    BigButton = "",
                };
                readMappingList = new GamePadMappingList();
                readMappingList.GamePadMappings.Add(defaultMapping);

                // generates a new file if it does not exist
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(GamePadMappingList));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer.BaseStream, readMappingList);
                writer.Close();
            }
            return readMappingList;
        }
    }
}
