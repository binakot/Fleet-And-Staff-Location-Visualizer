using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public sealed class TopOrderOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const int TopOrderOffset = 100;

        private RectTransform rect;
        private Image backgroud;

        private void Start()
        {
            rect = GetComponent<RectTransform>();
            backgroud = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            rect.SetSiblingIndex(rect.GetSiblingIndex() + TopOrderOffset);
            backgroud.color += new Color(0, 0, 0, 0.5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rect.SetSiblingIndex(rect.GetSiblingIndex() - TopOrderOffset);
            backgroud.color -= new Color(0, 0, 0, 0.5f);
        }
    }
}
