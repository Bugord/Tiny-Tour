using UnityEngine;

namespace Core.Navigation
{
    public interface IScreenFactory
    {
        T Create<T>(Transform parentTransform) where T : BaseScreen;
    }
}