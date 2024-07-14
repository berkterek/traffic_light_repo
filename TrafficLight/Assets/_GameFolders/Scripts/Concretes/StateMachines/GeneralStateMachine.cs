using System.Collections.Generic;
using TrafficLight.Abstracts.States;

namespace TrafficLight.StateMachines
{
    public class GeneralStateMachine : IStateMachine
    {
        IState _currentState;
        readonly List<IUpdateState> _normalStates;
        readonly List<IUpdateState> _anyStates;

        public GeneralStateMachine()
        {
            _normalStates = new List<IUpdateState>();
            _anyStates = new List<IUpdateState>();
        }

        public void Tick()
        {
            CheckState();
            _currentState?.Tick();
        }
        
        public void SetNormalStateAndConditions(IState currentState, IState nextState, System.Func<bool> condition, bool isFirstState = false)
        {
            if (isFirstState)
            {
                SetStateAndConditionsWithFirstState(currentState);
            }

            var updateState = Create(currentState, nextState, condition);
            if (_normalStates.Contains(updateState)) return;
            _normalStates.Add(updateState);
        }

        public void SetAnyState(IState nextState, System.Func<bool> condition)
        {
            var updateState = Create(null, nextState, condition);
            if (_anyStates.Contains(updateState)) return;
            _anyStates.Add(updateState);
        }

        void SetStateAndConditionsWithFirstState(IState firsState)
        {
            UpdateCurrentState(firsState);
        }

        void UpdateCurrentState(IState state)
        {
            _currentState?.Exit();

            _currentState = state;
            
            _currentState.Enter();
        }

        void CheckState()
        {
            foreach (var state in _anyStates)
            {
                if (state.Condition.Invoke() && state.CurrentState == null)
                {
                    UpdateCurrentState(state.NextState);
                    return;    
                }
            }
            
            foreach (var state in _normalStates)
            {
                if (state.Condition.Invoke() && _currentState.Equals(state.CurrentState))
                {
                    UpdateCurrentState(state.NextState);
                    break;
                }
            }
        }

        IUpdateState Create(IState currentState, IState nextState, System.Func<bool> condition)
        {
            return new GeneralUpdateState(currentState, nextState, condition);
        }
    }
}