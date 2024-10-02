using System;
using System.Collections.Generic;
using LevelEditor.EditorState.States;
using UnityEngine;

namespace LevelEditor.EditorState.Core
{
    public class EditorStateMachine
    {
        private readonly IEditorStateFactory editorStateFactory;
        private readonly Dictionary<Type, BaseEditorState> states;

        private BaseEditorState currentState;

        public EditorStateMachine(IEditorStateFactory editorStateFactory)
        {
            this.editorStateFactory = editorStateFactory;
            states = new Dictionary<Type, BaseEditorState>();
            
            RegisterState<LoadEditorState>();
        }

        private void RegisterState<T>() where T : BaseEditorState
        {
            if (states.ContainsKey(typeof(T))) {
                Debug.LogWarning($"Editor state {typeof(T)} was already registered");
                return;
            }

            var state = editorStateFactory.Create<T>(this);
            states.Add(typeof(T), state);
        }

        public void ChangeState<T>() where T : BaseEditorState
        {
            if (!states.ContainsKey(typeof(T))) {
                Debug.LogError($"State {typeof(T)} is not registered");
                return;
            }

            currentState?.OnExit();
            currentState = states[typeof(T)];
            currentState?.OnEnter();
        }
    }
}