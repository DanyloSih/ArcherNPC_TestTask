using System.Linq;
using ArcherNPC_TestTask.StateMachineBasics;
using UnityEngine;

namespace ArcherNPC_TestTask.Detection
{
    public class ObjectsDetectionStateChangedTransition<TDetectionObject> : ITransition
        where TDetectionObject : MonoBehaviour
    {
        private readonly IState _nextState;
        private ObjectsDetector _detector;
        private readonly bool[] _transitionTriggerValues;
        private bool? _isDetected = null;   

        public ObjectsDetectionStateChangedTransition(
            IState nextState,
            ObjectsDetector detector,
            params bool[] transitionTriggerValues)
        {
            _nextState = nextState;
            _detector = detector;
            _transitionTriggerValues = transitionTriggerValues;
            _isDetected = _detector.TryDetectObjects<TDetectionObject>(out _);
        }

        public IState GetTransitedState()
        {
            bool isDetected = _detector.TryDetectObjects<TDetectionObject>(out _);
            var result = isDetected != _isDetected && _transitionTriggerValues.Contains(isDetected)
                ? _nextState
                : null;

            _isDetected = isDetected;
            return result;
        }

        public void Enter()
            => _isDetected = null;
    }
}