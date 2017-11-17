using Assets.Scripts.Utils;
using Mapbox.Unity.Map;

namespace Assets.Scripts.Helpers
{
    public sealed class SceneManager : Singleton<SceneManager>
    {
        public BasicMap Map;

        private SceneManager() { }

        private void Awake()
        {
            Map = GetComponent<BasicMap>();
        }
    }
}
