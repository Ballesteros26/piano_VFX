using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;
using Unity;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides data for the <see cref="M:System.Diagnostics.Tracing.EventListener.OnEventWritten(System.Diagnostics.Tracing.EventWrittenEventArgs)" /> callback.</summary>
	// Token: 0x02000B04 RID: 2820
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x0014D2AD File Offset: 0x0014B4AD
		// (set) Token: 0x06006567 RID: 25959 RVA: 0x0014D2E4 File Offset: 0x0014B4E4
		public string EventName
		{
			get
			{
				if (this.m_eventName != null || this.EventId < 0)
				{
					return this.m_eventName;
				}
				return this.m_eventSource.m_eventData[this.EventId].Name;
			}
			internal set
			{
				this.m_eventName = value;
			}
		}

		/// <summary>Gets the event identifier.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06006568 RID: 25960 RVA: 0x0014D2ED File Offset: 0x0014B4ED
		// (set) Token: 0x06006569 RID: 25961 RVA: 0x0014D2F5 File Offset: 0x0014B4F5
		public int EventId { get; internal set; }

		/// <summary>Gets the activity ID on the thread that the event was written to. </summary>
		/// <returns>The activity ID on the thread that the event was written to. </returns>
		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x0600656A RID: 25962 RVA: 0x0014D2FE File Offset: 0x0014B4FE
		public Guid ActivityId
		{
			[SecurityCritical]
			get
			{
				return EventSource.CurrentThreadActivityId;
			}
		}

		/// <summary>Gets the identifier of an activity that is related to the activity represented by the current instance. </summary>
		/// <returns>The identifier of the related activity, or <see cref="F:System.Guid.Empty" /> if there is no related activity.</returns>
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x0014D305 File Offset: 0x0014B505
		// (set) Token: 0x0600656C RID: 25964 RVA: 0x0014D30D File Offset: 0x0014B50D
		public Guid RelatedActivityId
		{
			[SecurityCritical]
			get;
			internal set; }

		/// <summary>Gets the payload for the event.</summary>
		/// <returns>The payload for the event.</returns>
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600656D RID: 25965 RVA: 0x0014D316 File Offset: 0x0014B516
		// (set) Token: 0x0600656E RID: 25966 RVA: 0x0014D31E File Offset: 0x0014B51E
		public ReadOnlyCollection<object> Payload { get; internal set; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x0014D328 File Offset: 0x0014B528
		// (set) Token: 0x06006570 RID: 25968 RVA: 0x0014D391 File Offset: 0x0014B591
		public ReadOnlyCollection<string> PayloadNames
		{
			get
			{
				if (this.m_payloadNames == null)
				{
					List<string> list = new List<string>();
					foreach (ParameterInfo parameterInfo in this.m_eventSource.m_eventData[this.EventId].Parameters)
					{
						list.Add(parameterInfo.Name);
					}
					this.m_payloadNames = new ReadOnlyCollection<string>(list);
				}
				return this.m_payloadNames;
			}
			internal set
			{
				this.m_payloadNames = value;
			}
		}

		/// <summary>Gets the event source object.</summary>
		/// <returns>The event source object.</returns>
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x0014D39A File Offset: 0x0014B59A
		public EventSource EventSource
		{
			get
			{
				return this.m_eventSource;
			}
		}

		/// <summary>Gets the keywords for the event.</summary>
		/// <returns>The keywords for the event.</returns>
		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x0014D3A2 File Offset: 0x0014B5A2
		public EventKeywords Keywords
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_keywords;
				}
				return (EventKeywords)this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
			}
		}

		/// <summary>Gets the operation code for the event.</summary>
		/// <returns>The operation code for the event.</returns>
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06006573 RID: 25971 RVA: 0x0014D3D6 File Offset: 0x0014B5D6
		public EventOpcode Opcode
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_opcode;
				}
				return (EventOpcode)this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
			}
		}

		/// <summary>Gets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x0014D40A File Offset: 0x0014B60A
		public EventTask Task
		{
			get
			{
				if (this.EventId < 0)
				{
					return EventTask.None;
				}
				return (EventTask)this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x0014D439 File Offset: 0x0014B639
		public EventTags Tags
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_tags;
				}
				return this.m_eventSource.m_eventData[this.EventId].Tags;
			}
		}

		/// <summary>Gets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06006576 RID: 25974 RVA: 0x0014D468 File Offset: 0x0014B668
		// (set) Token: 0x06006577 RID: 25975 RVA: 0x0014D497 File Offset: 0x0014B697
		public string Message
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_message;
				}
				return this.m_eventSource.m_eventData[this.EventId].Message;
			}
			internal set
			{
				this.m_message = value;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06006578 RID: 25976 RVA: 0x0014D4A0 File Offset: 0x0014B6A0
		public EventChannel Channel
		{
			get
			{
				if (this.EventId < 0)
				{
					return EventChannel.None;
				}
				return (EventChannel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
			}
		}

		/// <summary>Gets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x0014D4CF File Offset: 0x0014B6CF
		public byte Version
		{
			get
			{
				if (this.EventId < 0)
				{
					return 0;
				}
				return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
			}
		}

		/// <summary>Gets the level of the event.</summary>
		/// <returns>The level of the event.</returns>
		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x0600657A RID: 25978 RVA: 0x0014D4FE File Offset: 0x0014B6FE
		public EventLevel Level
		{
			get
			{
				if (this.EventId < 0)
				{
					return EventLevel.LogAlways;
				}
				return (EventLevel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
			}
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0014D52D File Offset: 0x0014B72D
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal EventWrittenEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04003279 RID: 12921
		private string m_message;

		// Token: 0x0400327A RID: 12922
		private string m_eventName;

		// Token: 0x0400327B RID: 12923
		private EventSource m_eventSource;

		// Token: 0x0400327C RID: 12924
		private ReadOnlyCollection<string> m_payloadNames;

		// Token: 0x0400327D RID: 12925
		internal EventTags m_tags;

		// Token: 0x0400327E RID: 12926
		internal EventOpcode m_opcode;

		// Token: 0x0400327F RID: 12927
		internal EventKeywords m_keywords;
	}
}
