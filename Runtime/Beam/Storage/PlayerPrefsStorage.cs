using UnityEngine;

namespace Beam.Storage
{
    internal class PlayerPrefsStorage: IStorage
    {
        public string Get(string key)
        {
            var value = PlayerPrefs.GetString(key);
            return string.IsNullOrEmpty(value) ? null : value;
        }

        public void Set(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
    }
}