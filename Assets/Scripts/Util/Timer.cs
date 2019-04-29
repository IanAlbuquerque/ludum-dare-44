using System;
using DesignPatterns;

namespace Util {
    public class Timer
    {
        private Observable onExpire = new Observable();

        float durationsInSeconds = 10.0f;

        private bool isRunning = false;

        private float currentTimeElapsedInSeconds = 0.0f;

        private bool isPeriodic = false;

        public Timer(float durationsInSeconds, bool isPeriodic = false) {
            this.durationsInSeconds = durationsInSeconds;
            this.isRunning = false;
            this.isPeriodic = isPeriodic;
            this.currentTimeElapsedInSeconds = 0.0f;
        }

        public void subscribe(Action callback) {
            this.onExpire.subscribe(callback);
        }

        public void unsubscribe(Action callback) {
            this.onExpire.unsubscribe(callback);
        }

        public void setDuration(float durationsInSeconds) {
            this.durationsInSeconds = durationsInSeconds;
        }

        public void setPeriodic(bool isPeriodic) {
            this.isPeriodic = isPeriodic;
        }

        public void tick(float deltaTimeInSeconds) {
            if(this.isRunning) {
                this.currentTimeElapsedInSeconds += deltaTimeInSeconds;
                if(this.currentTimeElapsedInSeconds >= this.durationsInSeconds) {
                    do {
                        this.currentTimeElapsedInSeconds = this.currentTimeElapsedInSeconds - this.durationsInSeconds;
                        if(!this.isPeriodic) {
                            this.stop();
                        }
                        this.onExpire.trigger();
                    } while(this.currentTimeElapsedInSeconds >= this.durationsInSeconds);
                }
            }
        }

        public void start() {
            this.isRunning = true;
            this.currentTimeElapsedInSeconds = 0.0f;
        }

        public void stop() {
            this.isRunning = false;
        }
    }
}
