using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util {
    public class TimerMonoBehavior : MonoBehaviour
    {
        private Timer timer;
        
        // Start is called before the first frame update
        void Awake()
        {
            this.timer = new Timer(10.0f);
        }

        public void init(float durationInSeconds, bool isPeriodic) {
            this.timer.setDuration(durationInSeconds);
            this.timer.setPeriodic(isPeriodic);
        }

        public void start() {
            this.timer.start();
        }

        public void stop() {
            this.timer.stop();
        }

        public void subscribe(System.Action action) {
            this.timer.subscribe(action);
        }

        public TimerMonoBehavior initSubscribeAndStart(float durationInSeconds, bool isPeriodic, System.Action action) {
            this.init(durationInSeconds, isPeriodic);
            this.subscribe(action);
            this.start();
            return this;
        }

        public void unsubscribe(System.Action action) {
            this.timer.unsubscribe(action);
        }

        void Update()
        {
            this.timer.tick(Time.deltaTime);
        }
    }
}