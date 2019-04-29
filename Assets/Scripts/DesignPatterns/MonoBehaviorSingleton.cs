using UnityEngine;

namespace DesignPatterns {
    public abstract class MonoBehaviorSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static Observable afterAwake = new Observable();

        // Gets the instance
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    throw new System.Exception( "MonoBehaviorSingleton Error - An instance of " + typeof(T) + " " +
                                                "is needed in the scene (some script is trying to use it), " +
                                                "but there is none.");
                }
                return instance;
            }
        }

        protected void Awake ()
        {
            if(instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this.gameObject);
                this.onAwake();
                afterAwake.trigger();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Friendly reminder:
        // The virtual keyword is used to modify a method, property, indexer,
        // or event declaration and allow for it to be overridden in a derived class.
        // For example, this method can be overridden by any class that inherits it:
        public virtual void onAwake() {
        }
    }
}