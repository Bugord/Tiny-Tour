﻿using LevelEditing.EditorState.States;

namespace Game.Workshop.WorkshopState.Core
{
    public class WorkshopStateMachine
    {
        private readonly IWorkshopStateFactory workshopStateFactory;

        private BaseEditorState currentState;

        public WorkshopStateMachine(IWorkshopStateFactory workshopStateFactory)
        {
            this.workshopStateFactory = workshopStateFactory;
        }

        public void ChangeState<T>() where T : BaseEditorState
        {
            currentState?.OnExit();

            var newState = workshopStateFactory.Create<T>(this);
            currentState = newState;
            currentState?.OnEnter();
        }
    }
}