using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class UiManager : Singleton<UiManager>
    {
        public GameObject ObjectLabel;

        public void AddObjectLabel(GameObject go, string text)
        {
            var labelGo = Instantiate(ObjectLabel, this.transform);

            var uiText = labelGo.GetComponentInChildren<Text>();
            uiText.text = text.Trim();

            var follower = labelGo.GetComponentInChildren<FollowToGameObject>();
            follower.followedGameObject = go;
        }
    }
}
