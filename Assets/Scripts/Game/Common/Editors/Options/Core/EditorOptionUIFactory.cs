﻿using System;
using System.Collections.Generic;
using Core.Logging;
using Game.Common.UI.Editing.EditorOption;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Common.Editors.Options.Core
{
    public class EditorOptionUIFactory : IEditorOptionUIFactory
    {
        private readonly DiContainer diContainer;
        private readonly EditorOptionUI editorOptionUIPrefab;

        private Dictionary<Type, EditorOptionUI> editorOptionsUIPrefabs;

        public EditorOptionUIFactory(DiContainer diContainer, EditorOptionUI editorOptionUIPrefab)
        {
            this.diContainer = diContainer;
            this.editorOptionUIPrefab = editorOptionUIPrefab;
        }

        public EditorOptionUI Create(Transform rootTransform, ToggleGroup toggleGroup)
        {
            var editorOptionUI = diContainer.InstantiatePrefabForComponent<EditorOptionUI>(editorOptionUIPrefab, rootTransform);
            editorOptionUI.SetToggleGroup(toggleGroup);
            return editorOptionUI;
        }
    }
}