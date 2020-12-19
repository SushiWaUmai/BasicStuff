using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Basics
{
    public static class BasicSaveSystem
    {
        private static readonly string SaveDirectoryPath = Application.persistentDataPath + "/Saves/";

        /// <summary>
        /// Saves the object as a binary file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="save"></param>
        /// <param name="name"></param>
        public static void SaveInBinary<T>(T save, string name) where T : class
        {
            SaveInBinary(save, name, string.Empty);
        }

        /// <summary>
        /// Saves a object as a binary file under a path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="save"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
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
            FileStream fs = new FileStream(SaveDirectoryPath + path + name, FileMode.Create);
            formatter.Serialize(fs, save);
            fs.Close();
        }

        /// <summary>
        /// Loads a binary file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T LoadBinary<T>(string name) where T : class
        {
            return LoadBinary<T>(name, string.Empty);
        }

        /// <summary>
        /// loads binary file and returns true if file exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool LoadBinary<T>(string name, out T result) where T : class
        {
            result = LoadBinary<T>(name, string.Empty);
            return result != null;
        }

        /// <summary>
        /// Loads binary file from a certain path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadBinary<T>(string name, string path) where T : class
        {
            if (File.Exists(SaveDirectoryPath + path + name))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream fs = new FileStream(SaveDirectoryPath + path + name, FileMode.Open);
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

        /// <summary>
        /// Saves an object in Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="save"></param>
        /// <param name="name"></param>
        public static void SaveInJson<T>(T save, string name) where T : class
        {
            SaveInJson(save, name, string.Empty);
        }

        /// <summary>
        /// Saves an object in Json under a certain path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="save"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
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

        /// <summary>
        /// Loads a json file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T LoadJson<T>(string name) where T : class
        {
            return LoadJson<T>(name, string.Empty);
        }

        /// <summary>
        /// Loads a Json file and returns false if file does not exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool LoadJson<T>(string name, out T result) where T : class
        {
            result = LoadJson<T>(name, string.Empty);
            return result != null;
        }

        /// <summary>
        /// Loads a Json file under a certain path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes a Json File
        /// </summary>
        /// <param name="name"></param>
        public static void DeleteJsonFile(string name)
        {
            DeleteJsonFile(name, string.Empty);
        }

        /// <summary>
        /// Deletes a Json File
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
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

        /// <summary>
        /// Deletes a Binary File
        /// </summary>
        /// <param name="name"></param>
        public static void DeleteBinaryFile(string name)
        {
            DeleteBinaryFile(name, string.Empty);
        }

        /// <summary>
        /// Deletes a Binary File under a certain path
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        public static void DeleteBinaryFile(string name, string path)
        {
            if (File.Exists(SaveDirectoryPath + path + name))
            {
                File.Delete(SaveDirectoryPath + path + name);
            }
            else
            {
                Debug.Log("No File found");
            }
        }

        /// <summary>
        /// Deletes a Directory
        /// </summary>
        /// <param name="path"></param>
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

