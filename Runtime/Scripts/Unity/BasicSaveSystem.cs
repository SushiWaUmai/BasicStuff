using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Basics
{
    public static class BasicSaveSystem
    {
        private static readonly string SaveDirectoryPath = Application.persistentDataPath + "/Saves/";
        private const string FileSuffix = ".noki";

        public static void SaveInBinary<T>(T save, string name) where T : class
        {
            SaveInBinary(save, name, string.Empty);
        }

        public static void SaveInBinary<T>(T save, string name, string path) where T : class
        {
            if (save == null)
            {
                Debug.Log("Data is null");
                return;
            }
            if (!Directory.Exists(SaveDirectoryPath + path))
            {
                Directory.CreateDirectory(SaveDirectoryPath + path);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(SaveDirectoryPath + path + name + FileSuffix, FileMode.Create);
            formatter.Serialize(fs, save);
            fs.Close();
        }

        public static T LoadBinary<T>(string name) where T : class
        {
            return LoadBinary<T>(name, string.Empty);
        }

        public static bool LoadBinary<T>(string name, out T result) where T : class
        {
            result = LoadBinary<T>(name, string.Empty);
            return result != null;
        }

        public static T LoadBinary<T>(string name, string path) where T : class
        {
            if (File.Exists(SaveDirectoryPath + path + name + FileSuffix))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream fs = new FileStream(SaveDirectoryPath + path + name + FileSuffix, FileMode.Open);
                    T savedata = formatter.Deserialize(fs) as T;
                    fs.Close();
                    return savedata;
                }
                catch
                {
                    Debug.Log("No SaveData found");
                    return null;
                }
            }
            Debug.Log("No File found");
            return null;
        }


        public static void SaveInJson<T>(T save, string name) where T : class
        {
            SaveInJson(save, name, string.Empty);
        }

        public static void SaveInJson<T>(T save, string name, string path) where T : class
        {
            if (save == null)
            {
                Debug.Log("Data is null");
                return;
            }
            if (!Directory.Exists(SaveDirectoryPath + path))
            {
                Directory.CreateDirectory(SaveDirectoryPath + path);
            }
            File.WriteAllText(SaveDirectoryPath + path + name + ".txt", JsonUtility.ToJson(save));
        }

        public static T LoadJson<T>(string name) where T : class
        {
            return LoadJson<T>(name, string.Empty);
        }

        public static bool LoadJson<T>(string name, out T result) where T : class
        {
            result = LoadJson<T>(name, string.Empty);
            return result != null;
        }

        public static T LoadJson<T>(string name, string path) where T : class
        {
            if (File.Exists(SaveDirectoryPath + path + name + ".txt"))
            {
                T savedata = JsonUtility.FromJson<T>(File.ReadAllText(SaveDirectoryPath + path + name + ".txt"));
                if (savedata != null)
                {
                    return savedata;
                }
                else
                {
                    Debug.Log("No SaveData found");
                    return null;
                }
            }
            Debug.Log("No File found");
            return null;
        }


        public static void DeleteJsonFile(string name)
        {
            DeleteJsonFile(name, string.Empty);
        }

        public static void DeleteJsonFile(string name, string path)
        {
            if (File.Exists(SaveDirectoryPath + path + name + ".txt"))
            {
                File.Delete(SaveDirectoryPath + path + name + ".txt");
            }
            else
            {
                Debug.Log("No File found");
            }
        }


        public static void DeleteBinaryFile(string name)
        {
            DeleteBinaryFile(name, string.Empty);
        }

        public static void DeleteBinaryFile(string name, string path)
        {
            if (File.Exists(SaveDirectoryPath + path + name + FileSuffix))
            {
                File.Delete(SaveDirectoryPath + path + name + FileSuffix);
            }
            else
            {
                Debug.Log("No File found");
            }
        }

        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(SaveDirectoryPath + path))
            {
                Directory.Delete(SaveDirectoryPath + path, true);
            }
            else
            {
                Debug.Log("No Directory found");
            }
        }
    }
}

