using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    #region SINGLETON
    public static EventQueueManager instance;

    private void Awake() {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion
    
    public Queue<ICommand> eventQueue => _eventQueue;
    private Queue<ICommand> _eventQueue = new Queue<ICommand>();

    public void AddCommand(ICommand command) => _eventQueue.Enqueue(command);

    private void Update() {
        while (_eventQueue.Count > 0) {
            _eventQueue.Dequeue().Do();
        } 
        
    }
}
