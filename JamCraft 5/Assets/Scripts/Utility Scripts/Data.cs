using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utility.Development
{
    public static class Data
    {
        #region Save
        /// <summary>
        /// Saves the object in binary format. Creates the file if it doesn't exist.
        /// </summary>
        public static void Save(string path, object data, bool overwrite = true)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            FileStream stream;
            if (overwrite)
            {
                stream = File.Open(path, FileMode.Open);
            }
            else
            {
                stream = File.Open(path, FileMode.Append);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, data);
            stream.Dispose();
        }
        #endregion

        #region Load<T>
        /// <summary>
        /// Loads the object of the given type, from the given path.
        /// </summary>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T data = (T)binaryFormatter.Deserialize(stream);
            stream.Dispose();
            return data;
        }
        #endregion
    }
}
