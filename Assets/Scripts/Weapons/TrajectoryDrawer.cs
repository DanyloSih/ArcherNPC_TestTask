﻿using System;
using System.Collections;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class TrajectoryDrawer : MonoBehaviour
    {
        [SerializeField] private int _segmentsCount = 30;
        [SerializeField] private LineRenderer _lineRenderer;

        private ITrajectoryFunction _trajectoryFunction;
        private float _segmentTime;
        private Vector3[] _points;
        private Coroutine _drawCoroutine;

        public void StartDrawing(ITrajectoryFunction trajectoryFunction, Vector2 target, Action updateTrajectoryCallback = null)
        {
            var origin = trajectoryFunction.GetTrajectoryInfoByFunction(0).TrajectoryPoint;
            var direction = target - origin;
            _trajectoryFunction = trajectoryFunction;
            _segmentTime = direction.x / _segmentsCount;
            _points = new Vector3[_segmentsCount];
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = _segmentsCount;

            _drawCoroutine = StartCoroutine(TrajectoryDrawingProcess(updateTrajectoryCallback));
        }

        public void StopDrawing()
        {
            _lineRenderer.enabled = false;
            StopCoroutine(_drawCoroutine);
        }

        private IEnumerator TrajectoryDrawingProcess(Action updateTrajectoryCallback)
        {
            while (true)
            {
                updateTrajectoryCallback?.Invoke();
                _points[0] = _trajectoryFunction
                    .GetTrajectoryInfoByFunction(0).TrajectoryPoint;

                for (int i = 1; i < _segmentsCount; i++)
                {
                    _points[i] = _trajectoryFunction
                        .GetTrajectoryInfoByFunction(i * _segmentTime).TrajectoryPoint;
                }
                _lineRenderer.SetPositions(_points);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
