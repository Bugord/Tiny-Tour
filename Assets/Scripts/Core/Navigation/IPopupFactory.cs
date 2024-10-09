using UnityEngine;

namespace Core.Navigation
{
    public interface IPopupFactory
    {
        T Create<T>(Transform parentTransform) where T : BasePopup;
    }
}