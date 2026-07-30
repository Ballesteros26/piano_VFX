using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x0200026E RID: 622
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class EventHandlerList : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventHandlerList" /> class. </summary>
		// Token: 0x060013F9 RID: 5113 RVA: 0x000020EB File Offset: 0x000002EB
		public EventHandlerList()
		{
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0005289B File Offset: 0x00050A9B
		internal EventHandlerList(Component parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <returns>The delegate for the specified key, or null if a delegate does not exist.</returns>
		/// <param name="key">An object to find in the list. </param>
		// Token: 0x17000422 RID: 1058
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this.parent == null || this.parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry != null)
				{
					return listEntry.handler;
				}
				return null;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry.handler = value;
					return;
				}
				this.head = new EventHandlerList.ListEntry(key, value, this.head);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to add to the list. </param>
		// Token: 0x060013FD RID: 5117 RVA: 0x00052918 File Offset: 0x00050B18
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Combine(listEntry.handler, value);
				return;
			}
			this.head = new EventHandlerList.ListEntry(key, value, this.head);
		}

		/// <summary>Adds a list of delegates to the current list.</summary>
		/// <param name="listToAddFrom">The list to add.</param>
		// Token: 0x060013FE RID: 5118 RVA: 0x00052958 File Offset: 0x00050B58
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry next = listToAddFrom.head; next != null; next = next.next)
			{
				this.AddHandler(next.key, next.handler);
			}
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x060013FF RID: 5119 RVA: 0x0005298A File Offset: 0x00050B8A
		public void Dispose()
		{
			this.head = null;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00052994 File Offset: 0x00050B94
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry next = this.head;
			while (next != null && next.key != key)
			{
				next = next.next;
			}
			return next;
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to remove from the list. </param>
		// Token: 0x06001401 RID: 5121 RVA: 0x000529C0 File Offset: 0x00050BC0
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Remove(listEntry.handler, value);
			}
		}

		// Token: 0x040012E0 RID: 4832
		private EventHandlerList.ListEntry head;

		// Token: 0x040012E1 RID: 4833
		private Component parent;

		// Token: 0x0200026F RID: 623
		private sealed class ListEntry
		{
			// Token: 0x06001402 RID: 5122 RVA: 0x000529EA File Offset: 0x00050BEA
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x040012E2 RID: 4834
			internal EventHandlerList.ListEntry next;

			// Token: 0x040012E3 RID: 4835
			internal object key;

			// Token: 0x040012E4 RID: 4836
			internal Delegate handler;
		}
	}
}
