using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents a link between an event provider and an event log that the provider publishes events into. This object cannot be instantiated.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000394 RID: 916
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogLink
	{
		// Token: 0x06001B18 RID: 6936 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventLogLink()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name of the event log.</summary>
		/// <returns>Returns a string that contains the localized name of the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x000560B4 File Offset: 0x000542B4
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a Boolean value that determines whether the event log is imported, rather than defined in the event provider. An imported event log is defined in a different provider.</summary>
		/// <returns>Returns true if the event log is imported by the event provider, and returns false if the event log is not imported by the event provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x00056818 File Offset: 0x00054A18
		public bool IsImported
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the non-localized name of the event log associated with this object.</summary>
		/// <returns>Returns a string that contains the non-localized name of the event log associated with this object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x000560B4 File Offset: 0x000542B4
		public string LogName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
