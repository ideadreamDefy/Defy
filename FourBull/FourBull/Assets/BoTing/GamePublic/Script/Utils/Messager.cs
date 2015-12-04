using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoTing.GamePublic
{
	public delegate void Callback();
	public delegate void Callback<T>(T arg1);
	public delegate void Callback<T, U>(T arg1, U arg2);
	public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);

	public class Messager
	{
		public Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();
		public List< string > permanentMessages = new List< string > ();
		
		public void MarkAsPermanent(string eventType)
		{
			//Debug.Log("NotifyCenter MarkAsPermanent \t\"" + eventType + "\"");
			permanentMessages.Add( eventType );
		}
		
		
		public void Cleanup()
		{
			//Debug.Log("NotifyCenter Cleanup. Make sure that none of necessary listeners are removed.");
			List< string > messagesToRemove = new List<string>();
			
			foreach (KeyValuePair<string, Delegate> pair in eventTable)
			{
				bool wasFound = false;
				
				foreach (string message in permanentMessages)
				{
					if (pair.Key == message) 
					{
						wasFound = true;
						break;
					}
				}
				
				if (!wasFound)messagesToRemove.Add( pair.Key );
			}
			
			foreach (string message in messagesToRemove) 
			{
				eventTable.Remove( message );
			}
		}
		
		public void PrintEventTable()
		{
			Debug.Log("\t\t\t=== MESSENGER PrintEventTable ===");
			
			foreach (KeyValuePair<string, Delegate> pair in eventTable) 
			{
				Debug.Log("\t\t\t" + pair.Key + "\t\t" + pair.Value);
			}
			
			Debug.Log("\n");
		}
		
		
		public void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
		{
			//Debug.Log("NotifyCenter OnListenerAdding \t\"" + eventType + "\"\t{" + listenerBeingAdded.Target + " -> " + listenerBeingAdded.Method + "}");
			
			if (!eventTable.ContainsKey(eventType)) 
			{
				eventTable.Add(eventType, null );
			}
			
			Delegate d = eventTable[eventType];
			if (d != null && d.GetType() != listenerBeingAdded.GetType()) {
				throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
			}
		}
		
		public bool OnListenerRemoving(string eventType, Delegate listenerBeingRemoved) 
		{
            bool canRemove = false;
            do
            {
                if (!eventTable.ContainsKey(eventType))
                {
                    Debug.LogWarningFormat("Attempting to remove listener with for event type \"{0}\" but current listener does not exist.", eventType);
                    break;
                }

                Delegate d = eventTable[eventType];
                if (d == null)
                {
                    Debug.LogWarningFormat("Attempting to remove listener with for event type \"{0}\" but current listener does not exist.", eventType);
                    break;
                }

                if (d.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", 
                        eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
                }

                canRemove = true;

            } while (false);
            return canRemove;			
		}
		
		public void OnListenerRemoved(string eventType)
		{
			if (eventTable.ContainsKey(eventType) && eventTable[eventType] == null)
			{
				eventTable.Remove(eventType);
			}
		}
		
		public void OnBroadcasting(string eventType) 
		{
			#if REQUIRE_LISTENER
			if (!eventTable.ContainsKey(eventType)) 
			{
				throw new BroadcastException(string.Format("Broadcasting message \"{0}\" but no listener found. Try marking the message with Messenger.MarkAsPermanent.", eventType));
			}
			#endif
		}
		
		public BroadcastException CreateBroadcastSignatureException(string eventType)
		{
			return new BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType));
		}
		
		public class BroadcastException : Exception 
		{
			public BroadcastException(string msg)
				: base(msg) 
			{
			}
		}

		class ListenerException : Exception
		{
			public ListenerException(string msg)
				: base(msg) 
			{
			}
		}

		//No parameters
		public void AddListener(string eventType, Callback handler) 
		{
			OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Callback)eventTable[eventType] + handler;
		}
		
		//Single parameter
		public void AddListener<T>(string eventType, Callback<T> handler)
		{
			OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Callback<T>)eventTable[eventType] + handler;
		}
		
		//Two parameters
		public void AddListener<T, U>(string eventType, Callback<T, U> handler)
		{
			OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Callback<T, U>)eventTable[eventType] + handler;
		}
		
		//Three parameters
		public void AddListener<T, U, V>(string eventType, Callback<T, U, V> handler) 
		{
			OnListenerAdding(eventType, handler);
			eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] + handler;
		}
		
		
		//No parameters
		public void RemoveListener(string eventType, Callback handler) 
		{
			bool result = OnListenerRemoving(eventType, handler);
            if(result)
            {
                eventTable[eventType] = (Callback)eventTable[eventType] - handler;
            }
			OnListenerRemoved(eventType);
		}
		
		//Single parameter
		public void RemoveListener<T>(string eventType, Callback<T> handler) 
		{
            bool result = OnListenerRemoving(eventType, handler);
            if (result)
            {
                eventTable[eventType] = (Callback<T>)eventTable[eventType] - handler;
            }			
			OnListenerRemoved(eventType);
		}
		
		//Two parameters
		public void RemoveListener<T, U>(string eventType, Callback<T, U> handler) 
		{
			bool result = OnListenerRemoving(eventType, handler);
            if (result)
            {
                eventTable[eventType] = (Callback<T, U>)eventTable[eventType] - handler;
            }
			OnListenerRemoved(eventType);
		}
		
		//Three parameters
		public void RemoveListener<T, U, V>(string eventType, Callback<T, U, V> handler)
		{
			bool result = OnListenerRemoving(eventType, handler);
            if (result)
            {
                eventTable[eventType] = (Callback<T, U, V>)eventTable[eventType] - handler;
            }
			OnListenerRemoved(eventType);
		}
		
		//No parameters
		public void Broadcast(string eventType)
		{
			//Debug.Log("NotifyCenter\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			OnBroadcasting(eventType);
			
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d)) 
			{
				Callback callback = d as Callback;
				
				if (callback != null)
				{
					callback();
				}
				else
				{
					throw CreateBroadcastSignatureException(eventType);
				}
			}
		}
		
		//Single parameter
		public void Broadcast<T>(string eventType, T arg1) 
		{
			#if LOG_ALL_MESSAGES || LOG_BROADCAST_MESSAGE
			Debug.Log("NotifyCenter\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			#endif
			OnBroadcasting(eventType);
			
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d)) 
			{
				Callback<T> callback = d as Callback<T>;
				
				if (callback != null) 
				{
					callback(arg1);
				}
				else 
				{
					throw CreateBroadcastSignatureException(eventType);
				}
			}
		}
		
		//Two parameters
		public void Broadcast<T, U>(string eventType, T arg1, U arg2) 
		{
			
			//Debug.Log("NotifyCenter\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			OnBroadcasting(eventType);
			
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d))
			{
				Callback<T, U> callback = d as Callback<T, U>;
				if (callback != null) 
				{
					callback(arg1, arg2);
				} 
				else 
				{
					throw CreateBroadcastSignatureException(eventType);
				}
			}
		}
		
		public void Broadcast<T, U, V>(string eventType, T arg1, U arg2, V arg3)
		{
			//Debug.Log("NotifyCenter\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventType + "\"");
			OnBroadcasting(eventType);
			Delegate d;
			if (eventTable.TryGetValue(eventType, out d)) 
			{
				Callback<T, U, V> callback = d as Callback<T, U, V>;
				
				if (callback != null) 
				{
					callback(arg1, arg2, arg3);
				} 
				else 
				{
					throw CreateBroadcastSignatureException(eventType);
				}
			}
		}
	}
}

