using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class UiManager : Singleton<UiManager>
    {
        [Header("References")]
        public GameObject ObjectLabelsRoot;
        public GameObject ObjectsListRoot;

        [Header("Prefabs")]
        public GameObject ObjectLabel;
        public GameObject ObjectsListItem;

        public void AddObjectLabel(GameObject go, string text)
        {
            var labelGo = Instantiate(ObjectLabel, ObjectLabelsRoot.transform);

            var uiText = labelGo.GetComponentInChildren<Text>();
            uiText.text = text.Trim();

            var follower = labelGo.GetComponentInChildren<FollowToGameObject>();
            follower.followedGameObject = go;

            var listItemGo = Instantiate(ObjectsListItem, ObjectsListRoot.transform);
            listItemGo.GetComponentInChildren<Text>().text = text.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0]; // Take only the first row.
        }
    }
}
