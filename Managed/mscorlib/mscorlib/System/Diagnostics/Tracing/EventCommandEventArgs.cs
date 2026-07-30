using System;
using System.Collections.Generic;
using Unity;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the arguments for the <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> callback.</summary>
	// Token: 0x02000B02 RID: 2818
	public class EventCommandEventArgs : EventArgs
	{
		/// <summary>Gets the command for the callback.</summary>
		/// <returns>The callback command.</returns>
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x0600655B RID: 25947 RVA: 0x0014D1BA File Offset: 0x0014B3BA
		// (set) Token: 0x0600655C RID: 25948 RVA: 0x0014D1C2 File Offset: 0x0014B3C2
		public EventCommand Command { get; internal set; }

		/// <summary>Gets the array of arguments for the callback.</summary>
		/// <returns>An array of callback arguments.</returns>
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x0600655D RID: 25949 RVA: 0x0014D1CB File Offset: 0x0014B3CB
		// (set) Token: 0x0600655E RID: 25950 RVA: 0x0014D1D3 File Offset: 0x0014B3D3
		public IDictionary<string, string> Arguments { get; internal set; }

		/// <summary>Enables the event that has the specified identifier.</summary>
		/// <returns>true if <paramref name="eventId" /> is in range; otherwise, false.</returns>
		/// <param name="eventId">The identifier of the event to enable.</param>
		// Token: 0x0600655F RID: 25951 RVA: 0x0014D1DC File Offset: 0x0014B3DC
		public bool EnableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
		}

		/// <summary>Disables the event that have the specified identifier.</summary>
		/// <returns>true if <paramref name="eventId" /> is in range; otherwise, false.</returns>
		/// <param name="eventId">The identifier of the event to disable.</param>
		// Token: 0x06006560 RID: 25952 RVA: 0x0014D20B File Offset: 0x0014B40B
		public bool DisableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x0014D23C File Offset: 0x0014B43C
		internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.Command = command;
			this.Arguments = arguments;
			this.eventSource = eventSource;
			this.listener = listener;
			this.perEventSourceSessionId = perEventSourceSessionId;
			this.etwSessionId = etwSessionId;
			this.enable = enable;
			this.level = level;
			this.matchAnyKeyword = matchAnyKeyword;
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal EventCommandEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400326C RID: 12908
		internal EventSource eventSource;

		// Token: 0x0400326D RID: 12909
		internal EventDispatcher dispatcher;

		// Token: 0x0400326E RID: 12910
		internal EventListener listener;

		// Token: 0x0400326F RID: 12911
		internal int perEventSourceSessionId;

		// Token: 0x04003270 RID: 12912
		internal int etwSessionId;

		// Token: 0x04003271 RID: 12913
		internal bool enable;

		// Token: 0x04003272 RID: 12914
		internal EventLevel level;

		// Token: 0x04003273 RID: 12915
		internal EventKeywords matchAnyKeyword;

		// Token: 0x04003274 RID: 12916
		internal EventCommandEventArgs nextCommand;
	}
}
