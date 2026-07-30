using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AA7 RID: 2727
	internal sealed class EventSourceActivity : IDisposable
	{
		// Token: 0x06006310 RID: 25360 RVA: 0x001429CA File Offset: 0x00140BCA
		public EventSourceActivity(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			this.eventSource = eventSource;
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x001429E7 File Offset: 0x00140BE7
		public static implicit operator EventSourceActivity(EventSource eventSource)
		{
			return new EventSourceActivity(eventSource);
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x001429EF File Offset: 0x00140BEF
		public EventSource EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x001429F7 File Offset: 0x00140BF7
		public Guid Id
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x001429FF File Offset: 0x00140BFF
		public EventSourceActivity Start<T>(string eventName, EventSourceOptions options, T data)
		{
			return this.Start<T>(eventName, ref options, ref data);
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x00142A0C File Offset: 0x00140C0C
		public EventSourceActivity Start(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x00142A34 File Offset: 0x00140C34
		public EventSourceActivity Start(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref options, ref emptyStruct);
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00142A54 File Offset: 0x00140C54
		public EventSourceActivity Start<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			return this.Start<T>(eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x00142A74 File Offset: 0x00140C74
		public void Stop<T>(T data)
		{
			this.Stop<T>(null, ref data);
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x00142A80 File Offset: 0x00140C80
		public void Stop<T>(string eventName)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Stop<EmptyStruct>(eventName, ref emptyStruct);
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x00142A9E File Offset: 0x00140C9E
		public void Stop<T>(string eventName, T data)
		{
			this.Stop<T>(eventName, ref data);
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x00142AA9 File Offset: 0x00140CA9
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(this.eventSource, eventName, ref options, ref data);
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x00142ABC File Offset: 0x00140CBC
		public void Write<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.Write<T>(this.eventSource, eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x00142AE4 File Offset: 0x00140CE4
		public void Write(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref options, ref emptyStruct);
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00142B0C File Offset: 0x00140D0C
		public void Write(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x00142B3A File Offset: 0x00140D3A
		public void Write<T>(EventSource source, string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(source, eventName, ref options, ref data);
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x00142B48 File Offset: 0x00140D48
		public void Dispose()
		{
			if (this.state == EventSourceActivity.State.Started)
			{
				EmptyStruct emptyStruct = default(EmptyStruct);
				this.Stop<EmptyStruct>(null, ref emptyStruct);
			}
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x00142B70 File Offset: 0x00140D70
		private EventSourceActivity Start<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.eventSource.IsEnabled())
			{
				return this;
			}
			EventSourceActivity eventSourceActivity = new EventSourceActivity(this.eventSource);
			if (!this.eventSource.IsEnabled(options.Level, options.Keywords))
			{
				Guid id = this.Id;
				eventSourceActivity.activityId = Guid.NewGuid();
				eventSourceActivity.startStopOptions = options;
				eventSourceActivity.eventName = eventName;
				eventSourceActivity.startStopOptions.Opcode = EventOpcode.Start;
				this.eventSource.Write<T>(eventName, ref eventSourceActivity.startStopOptions, ref eventSourceActivity.activityId, ref id, ref data);
			}
			else
			{
				eventSourceActivity.activityId = this.Id;
			}
			return eventSourceActivity;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x00142C1A File Offset: 0x00140E1A
		private void Write<T>(EventSource eventSource, string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (eventName == null)
			{
				throw new ArgumentNullException();
			}
			eventSource.Write<T>(eventName, ref options, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x00142C48 File Offset: 0x00140E48
		private void Stop<T>(string eventName, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.StartEventWasFired)
			{
				return;
			}
			this.state = EventSourceActivity.State.Stopped;
			if (eventName == null)
			{
				eventName = this.eventName;
				if (eventName.EndsWith("Start"))
				{
					eventName = eventName.Substring(0, eventName.Length - 5);
				}
				eventName += "Stop";
			}
			this.startStopOptions.Opcode = EventOpcode.Stop;
			this.eventSource.Write<T>(eventName, ref this.startStopOptions, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x00142CD3 File Offset: 0x00140ED3
		private bool StartEventWasFired
		{
			get
			{
				return this.eventName != null;
			}
		}

		// Token: 0x04003159 RID: 12633
		private readonly EventSource eventSource;

		// Token: 0x0400315A RID: 12634
		private EventSourceOptions startStopOptions;

		// Token: 0x0400315B RID: 12635
		internal Guid activityId;

		// Token: 0x0400315C RID: 12636
		private EventSourceActivity.State state;

		// Token: 0x0400315D RID: 12637
		private string eventName;

		// Token: 0x0400315E RID: 12638
		internal static Guid s_empty;

		// Token: 0x02000AA8 RID: 2728
		private enum State
		{
			// Token: 0x04003160 RID: 12640
			Started,
			// Token: 0x04003161 RID: 12641
			Stopped
		}
	}
}
