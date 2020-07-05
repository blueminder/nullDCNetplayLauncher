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

        public string Button11 { get; set; } = "";
        public string Button12 { get; set; } = "";
        public string Button13 { get; set; } = "";
        public string Button14 { get; set; } = "";
        public string Button15 { get; set; } = "";


        public Boolean Default { get; set; } = false;

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> rDict = new Dictionary<string, string>();
            rDict.Add("Y", Y);
            rDict.Add("A", A);
            rDict.Add("Back", Back);
            rDict.Add("X", X);
            rDict.Add("B", B);
            rDict.Add("Start", Start);
            rDict.Add("LeftStick", LeftStick);
            rDict.Add("RightStick", RightStick);
            rDict.Add("LeftShoulder", LeftShoulder);
            rDict.Add("RightShoulder", RightShoulder);
            rDict.Add("LeftTrigger", LeftTrigger);
            rDict.Add("RightTrigger", RightTrigger);
            rDict.Add("BigButton", BigButton);
            rDict.Add("Up", IsUp);
            rDict.Add("Down", IsDown);
            rDict.Add("Left", IsLeft);
            rDict.Add("Right", IsRight);
            rDict.Add("Button11", Button11);
            rDict.Add("Button12", Button12);
            rDict.Add("Button13", Button13);
            rDict.Add("Button14", Button14);
            rDict.Add("Button15", Button15);

            return rDict;
        }

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

            Launcher.RestoreGamePadMappings();

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
                    Button11 = "Coin",
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
