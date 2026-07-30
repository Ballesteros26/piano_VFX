using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Stores mappings between delegates and event tokens, to support the implementation of a Windows Runtime event in managed code.</summary>
	/// <typeparam name="T">The type of the event handler delegate for a particular event. </typeparam>
	// Token: 0x0200095F RID: 2399
	public sealed class EventRegistrationTokenTable<T> where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1" /> class. </summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="T" /> is not a delegate type. </exception>
		// Token: 0x0600593A RID: 22842 RVA: 0x0012AC4C File Offset: 0x00128E4C
		public EventRegistrationTokenTable()
		{
			if (!typeof(Delegate).IsAssignableFrom(typeof(T)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Type '{0}' is not a delegate type.  EventTokenTable may only be used with delegate types.", new object[] { typeof(T) }));
			}
		}

		/// <summary>Gets or sets a delegate of type <paramref name="T" /> whose invocation list includes all the event handler delegates that have been added, and that have not yet been removed. Invoking this delegate invokes all the event handlers. </summary>
		/// <returns>A delegate of type <paramref name="T" /> that represents all the event handler delegates that are currently registered for an event. </returns>
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x0012ACA8 File Offset: 0x00128EA8
		// (set) Token: 0x0600593C RID: 22844 RVA: 0x0012ACB4 File Offset: 0x00128EB4
		public T InvocationList
		{
			get
			{
				return this.m_invokeList;
			}
			set
			{
				Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
				lock (tokens)
				{
					this.m_tokens.Clear();
					this.m_invokeList = default(T);
					if (value != null)
					{
						this.AddEventHandlerNoLock(value);
					}
				}
			}
		}

		/// <summary>Adds the specified event handler to the table and to the invocation list, and returns a token that can be used to remove the event handler. </summary>
		/// <returns>A token that can be used to remove the event handler from the table and the invocation list. </returns>
		/// <param name="handler">The event handler to add. </param>
		// Token: 0x0600593D RID: 22845 RVA: 0x0012AD1C File Offset: 0x00128F1C
		public EventRegistrationToken AddEventHandler(T handler)
		{
			if (handler == null)
			{
				return new EventRegistrationToken(0UL);
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			EventRegistrationToken eventRegistrationToken;
			lock (tokens)
			{
				eventRegistrationToken = this.AddEventHandlerNoLock(handler);
			}
			return eventRegistrationToken;
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x0012AD70 File Offset: 0x00128F70
		private EventRegistrationToken AddEventHandlerNoLock(T handler)
		{
			EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
			while (this.m_tokens.ContainsKey(preferredToken))
			{
				preferredToken = new EventRegistrationToken(preferredToken.Value + 1UL);
			}
			this.m_tokens[preferredToken] = handler;
			Delegate @delegate = (Delegate)((object)this.m_invokeList);
			@delegate = Delegate.Combine(@delegate, (Delegate)((object)handler));
			this.m_invokeList = (T)((object)@delegate);
			return preferredToken;
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0012ADE8 File Offset: 0x00128FE8
		[FriendAccessAllowed]
		internal T ExtractHandler(EventRegistrationToken token)
		{
			T t = default(T);
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				if (this.m_tokens.TryGetValue(token, out t))
				{
					this.RemoveEventHandlerNoLock(token);
				}
			}
			return t;
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x0012AE44 File Offset: 0x00129044
		private static EventRegistrationToken GetPreferredToken(T handler)
		{
			Delegate[] invocationList = ((Delegate)((object)handler)).GetInvocationList();
			uint num;
			if (invocationList.Length == 1)
			{
				num = (uint)invocationList[0].Method.GetHashCode();
			}
			else
			{
				num = (uint)handler.GetHashCode();
			}
			return new EventRegistrationToken(((ulong)typeof(T).MetadataToken << 32) | (ulong)num);
		}

		/// <summary>Removes the event handler that is associated with the specified token from the table and the invocation list. </summary>
		/// <param name="token">The token that was returned when the event handler was added. </param>
		// Token: 0x06005941 RID: 22849 RVA: 0x0012AEA4 File Offset: 0x001290A4
		public void RemoveEventHandler(EventRegistrationToken token)
		{
			if (token.Value == 0UL)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				this.RemoveEventHandlerNoLock(token);
			}
		}

		/// <summary>Removes the specified event handler delegate from the table and the invocation list. </summary>
		/// <param name="handler">The event handler to remove. </param>
		// Token: 0x06005942 RID: 22850 RVA: 0x0012AEF0 File Offset: 0x001290F0
		public void RemoveEventHandler(T handler)
		{
			if (handler == null)
			{
				return;
			}
			Dictionary<EventRegistrationToken, T> tokens = this.m_tokens;
			lock (tokens)
			{
				EventRegistrationToken preferredToken = EventRegistrationTokenTable<T>.GetPreferredToken(handler);
				T t;
				if (this.m_tokens.TryGetValue(preferredToken, out t) && t == handler)
				{
					this.RemoveEventHandlerNoLock(preferredToken);
				}
				else
				{
					foreach (KeyValuePair<EventRegistrationToken, T> keyValuePair in this.m_tokens)
					{
						if (keyValuePair.Value == (T)((object)handler))
						{
							this.RemoveEventHandlerNoLock(keyValuePair.Key);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x0012AFCC File Offset: 0x001291CC
		private void RemoveEventHandlerNoLock(EventRegistrationToken token)
		{
			T t;
			if (this.m_tokens.TryGetValue(token, out t))
			{
				this.m_tokens.Remove(token);
				Delegate @delegate = (Delegate)((object)this.m_invokeList);
				@delegate = Delegate.Remove(@delegate, (Delegate)((object)t));
				this.m_invokeList = (T)((object)@delegate);
			}
		}

		/// <summary>Returns the specified event registration token table, if it is not null; otherwise, returns a new event registration token table. </summary>
		/// <returns>The event registration token table that is specified by <paramref name="refEventTable" />, if it is not null; otherwise, a new event registration token table. </returns>
		/// <param name="refEventTable">An event registration token table, passed by reference. </param>
		// Token: 0x06005944 RID: 22852 RVA: 0x0012B029 File Offset: 0x00129229
		public static EventRegistrationTokenTable<T> GetOrCreateEventRegistrationTokenTable(ref EventRegistrationTokenTable<T> refEventTable)
		{
			if (refEventTable == null)
			{
				Interlocked.CompareExchange<EventRegistrationTokenTable<T>>(ref refEventTable, new EventRegistrationTokenTable<T>(), null);
			}
			return refEventTable;
		}

		// Token: 0x04002E0B RID: 11787
		private Dictionary<EventRegistrationToken, T> m_tokens = new Dictionary<EventRegistrationToken, T>();

		// Token: 0x04002E0C RID: 11788
		private volatile T m_invokeList;
	}
}
