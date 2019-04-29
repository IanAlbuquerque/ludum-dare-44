using System.Collections.Generic;
using System;

namespace DesignPatterns {
    public class Observable
    {
        List<Action> listOfFunctionsSubscribed = new List<Action>();

        public void trigger() {
            foreach (Action func in this.listOfFunctionsSubscribed)
            {
                func();
            }
        }

        public void subscribe(Action func)
        {
            this.listOfFunctionsSubscribed.Add(func);
        }

        public void unsubscribe(Action func)
        {
            this.listOfFunctionsSubscribed.Remove(func);
        }

        public void clear() {
            this.listOfFunctionsSubscribed.Clear();
        }

        public Action triggerAsAction() {
            return () => {this.trigger();};
        }
    }

    public class Observable<T>
    {
        List<Action<T>> listOfFunctionsSubscribed = new List<Action<T>>();

        public void trigger(T value)
        {
            foreach (Action<T> func in this.listOfFunctionsSubscribed)
            {
                func(value);
            }
        }

        public void subscribe(Action<T> func)
        {
            this.listOfFunctionsSubscribed.Add(func);
        }

        public void unsubscribe(Action<T> func)
        {
            this.listOfFunctionsSubscribed.Remove(func);
        }

        public void clear() {
            this.listOfFunctionsSubscribed.Clear();
        }

        public Action<T> triggerAsAction() {
            return (T value) => {this.trigger(value);};
        }
    }
}
