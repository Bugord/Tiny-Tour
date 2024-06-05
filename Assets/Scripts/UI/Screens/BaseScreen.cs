﻿using UnityEngine;

namespace UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        protected NavigationSystem NavigationSystem => NavigationSystem.Instance;

        public virtual void Close()
        {
            NavigationSystem.PopScreen(this);
        }

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