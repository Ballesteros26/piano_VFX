using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing
{
	/// <summary>Contains the metadata that defines an event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000383 RID: 899
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct EventDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.EventDescriptor" /> class.</summary>
		/// <param name="id">The event identifier.</param>
		/// <param name="version">Version of the event. The version indicates a revision to the event definition. You can use this member and the Id member to identify a unique event.</param>
		/// <param name="channel">Defines a potential target for the event.</param>
		/// <param name="level">Specifies the level of detail included in the event.</param>
		/// <param name="opcode">Operation being performed at the time the event is written.</param>
		/// <param name="task">Identifies a logical component of the application that is writing the event.</param>
		/// <param name="keywords">Bit mask that specifies the event category. The keyword can contain one or more provider-defined keywords, standard keywords, or both.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AAE RID: 6830 RVA: 0x0000220F File Offset: 0x0000040F
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the channel value from the event descriptor.</summary>
		/// <returns>The channel that defines a potential target for the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00056394 File Offset: 0x00054594
		public byte Channel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the event identifier value from the event descriptor.</summary>
		/// <returns>The event identifier.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x000563B0 File Offset: 0x000545B0
		public int EventId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the keyword value from the event descriptor.</summary>
		/// <returns>The keyword, which is a bit mask, that specifies the event category.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x000563CC File Offset: 0x000545CC
		public long Keywords
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Retrieves the level value from the event descriptor.</summary>
		/// <returns>The level of detail included in the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x000563E8 File Offset: 0x000545E8
		public byte Level
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the operation code value from the event descriptor.</summary>
		/// <returns>The operation being performed at the time the event is written.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00056404 File Offset: 0x00054604
		public byte Opcode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the task value from the event descriptor.</summary>
		/// <returns>The task that identifies the logical component of the application that is writing the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00056420 File Offset: 0x00054620
		public int Task
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Retrieves the version value from the event descriptor.</summary>
		/// <returns>The version of the event. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0005643C File Offset: 0x0005463C
		public byte Version
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
