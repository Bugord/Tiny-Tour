using UnityEngine;

namespace Core.Navigation
{
    public abstract class BasePopup : BaseNavigationElement
    {
        [field: SerializeField]
        public int Priority { get; private set; }
    }
}