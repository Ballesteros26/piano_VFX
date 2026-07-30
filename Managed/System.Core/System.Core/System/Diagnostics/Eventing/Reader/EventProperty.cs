using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the value of an event property that is specified by the event provider when the event is published.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039C RID: 924
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventProperty
	{
		// Token: 0x06001B5F RID: 7007 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventProperty()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the value of the event property that is specified by the event provider when the event is published.</summary>
		/// <returns>Returns an object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000560B4 File Offset: 0x000542B4
		public object Value
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
