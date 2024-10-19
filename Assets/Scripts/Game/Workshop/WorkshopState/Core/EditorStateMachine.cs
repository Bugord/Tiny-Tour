using System;
using System.Collections.Generic;
using LevelEditing.EditorState.States;
using UnityEngine;

namespace LevelEditing.EditorState.Core
{
    public class EditorStateMachine
    {
        private readonly IEditorStateFactory editorStateFactory;

        private BaseEditorState currentState;

        public EditorStateMachine(IEditorStateFactory editorStateFactory)
        {
            this.editorStateFactory = editorStateFactory;
        }

        public void ChangeState<T>() where T : BaseEditorState
        {
            currentState?.OnExit();

            var newState = editorStateFactory.Create<T>(this);
            currentState = newState;
            currentState?.OnEnter();
        }
    }
}