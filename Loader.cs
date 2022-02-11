using UnityEngine;

namespace EFTDLL
{
    public class Loader
    {
        public static GameObject loadObject;

        public static void Load()
        {
            loadObject = new GameObject();
            loadObject.AddComponent<Menu>();
            Object.DontDestroyOnLoad(loadObject);
        }

        public static void Unload()
        {
            _Unload();
        }

        public static void _Unload()
        {
            Object.Destroy(loadObject);
        }
    }
}
