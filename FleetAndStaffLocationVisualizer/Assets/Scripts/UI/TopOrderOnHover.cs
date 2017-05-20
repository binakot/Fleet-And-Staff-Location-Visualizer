using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public sealed class TopOrderOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const int TopOrderOffset = 1000;

        public Color DefaultColor = Color.gray;
        public Color HoverColor = Color.black;

        private RectTransform rect;
        private Image backgroud;

        private void Start()
        {
            rect = GetComponent<RectTransform>();
            backgroud = GetComponent<Image>();
            backgroud.color = DefaultColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            rect.SetSiblingIndex(rect.GetSiblingIndex() + TopOrderOffset);
            backgroud.color = HoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rect.SetSiblingIndex(rect.GetSiblingIndex() - TopOrderOffset);
            backgroud.color = DefaultColor;
        }
    }
}
