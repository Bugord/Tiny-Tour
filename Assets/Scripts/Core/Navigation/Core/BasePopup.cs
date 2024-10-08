using UnityEngine;

namespace Core.Navigation.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        [field: SerializeField]
        public int Priority { get; private set; }
        
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual void SetInactive()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetActive()
        {
            gameObject.SetActive(true);
        }
    }
}