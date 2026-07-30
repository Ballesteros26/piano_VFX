using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the metadata (properties and settings) for an event that is defined in an event provider. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A1 RID: 929
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventMetadata
	{
		// Token: 0x06001B92 RID: 7058 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventMetadata()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the description template associated with the event using the current thread locale for the description language.</summary>
		/// <returns>Returns a string that contains the description template associated with the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Description
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the identifier of the event that is defined in the event provider.</summary>
		/// <returns>Returns a long value that is the event identifier.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00056A48 File Offset: 0x00054C48
		public long Id
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the keywords associated with the event that is defined in the even provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0005672F File Offset: 0x0005492F
		public IEnumerable<EventKeyword> Keywords
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the level associated with the event that is defined in the event provider. The level defines the severity of the event.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000560B4 File Offset: 0x000542B4
		public EventLevel Level
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a link to the event log that receives this event when the provider publishes this event.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventLogLink" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000560B4 File Offset: 0x000542B4
		public EventLogLink LogLink
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the opcode associated with this event that is defined by an event provider. The opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000560B4 File Offset: 0x000542B4
		public EventOpcode Opcode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the task associated with the event. A task identifies a portion of an application or a component that publishes an event. </summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventTask" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000560B4 File Offset: 0x000542B4
		public EventTask Task
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the template string for the event. Templates are used to describe data that is used by a provider when an event is published. Templates optionally specify XML that provides the structure of an event. The XML allows values that the event publisher provides to be inserted during the rendering of an event.</summary>
		/// <returns>Returns a string that contains the template for the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Template
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the version of the event that qualifies the event identifier.</summary>
		/// <returns>Returns a byte value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00056A64 File Offset: 0x00054C64
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
