using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents a keyword for an event. Keywords are defined in an event provider and are used to group the event with other similar events (based on the usage of the events).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000388 RID: 904
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventKeyword
	{
		// Token: 0x06001AD0 RID: 6864 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventKeyword()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name of the keyword.</summary>
		/// <returns>Returns a string that contains a localized name for this keyword.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x000560B4 File Offset: 0x000542B4
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the non-localized name of the keyword.</summary>
		/// <returns>Returns a string that contains the non-localized name of this keyword.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value associated with the keyword.</summary>
		/// <returns>Returns a long value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0005658C File Offset: 0x0005478C
		public long Value
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}
	}
}
