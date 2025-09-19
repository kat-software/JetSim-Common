
using UdonSharp;
using VRC.Udon.Common.Enums;

namespace KatSoftware.JetSim.Common.Runtime
{
    public abstract class CoroutineBehaviour : UdonSharpBehaviour
    {
        private string _methodName;
        private float _waitForSeconds;
        private EventTiming _eventTiming;

        private bool _shouldBeRunning;
        private bool _isRunning;


        /// <summary>Calls the method referenced in <paramref name="methodName"/> on this instance every few seconds. The amount of seconds is determined by the value of <paramref name="waitForSeconds"/>. Will continue to do so until <see cref="StopCoroutines"/> is called.</summary>
        /// <param name="methodName">The name of the method to run. Recommended to use nameof to get the method name.</param>
        /// <param name="waitForSeconds">How long to wait before calling the method again.</param>
        /// <param name="eventTiming">Where in the frame the event should be run.</param>
        protected void StartCoroutine(string methodName, float waitForSeconds = 0f, EventTiming eventTiming = EventTiming.Update)
        {
            if (string.IsNullOrWhiteSpace(methodName)) return;

            _methodName = methodName;
            _waitForSeconds = waitForSeconds;
            _eventTiming = eventTiming;

            if (_isRunning) return;

            _shouldBeRunning = true;
            _isRunning = true;

            // One frame delay to ensure it gets called on the correct timing.
            SendCustomEventDelayedFrames(nameof(_InternalUpdateLoopDoNotCall), 1, _eventTiming);
        }

        protected void StopCoroutines() => _shouldBeRunning = false;

        /// <summary>Only public due to SendCustomEventDelayedSeconds</summary>
        public void _InternalUpdateLoopDoNotCall()
        {
            if (!_shouldBeRunning)
            {
                _isRunning = false;
                return;
            }

            SendCustomEvent(_methodName);

            SendCustomEventDelayedSeconds(nameof(_InternalUpdateLoopDoNotCall), _waitForSeconds, _eventTiming);
        }
    }
}