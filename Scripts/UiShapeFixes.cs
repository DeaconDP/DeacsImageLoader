
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace ArchiTech.ProTV
{
    public class UiShapeFixes : UdonSharpBehaviour
    {
        void Start()
        {
            var uiShapes = GetComponentsInChildren(typeof(VRC_UiShape), true);
            foreach (Component uiShape in uiShapes)
            {
                var box = uiShape.GetComponent<BoxCollider>();
                if (box != null)
                {
                    var rect = (RectTransform)uiShape.transform;
                    var sizeDelta = rect.sizeDelta;
                    var pivot = rect.pivot;
                    box.center = new Vector3((-pivot.x + 0.5f) * sizeDelta.x, (-pivot.y + 0.5f) * sizeDelta.y, 0);
                    box.size = new Vector3(sizeDelta.x, sizeDelta.y, 0);
                }
            }
        }
    }
}
