using UnityEngine;

namespace ArcherNPC_TestTask.StateMachineBasics
{
    public class DebugState : State
    {
        private string _debugText;

        public DebugState(string debugText = "")
        {
            _debugText = debugText;
        }

        protected override void OnEnter()
        {
            Debug.Log("On Enter: " + _debugText);
        }

        protected override void OnStateUpdated()
        {
            Debug.Log("On State Updated: " + _debugText);
        }
    }
}
