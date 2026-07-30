using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an event level that is defined in an event provider. The level signifies the severity of the event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000389 RID: 905
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLevel
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventLevel()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name for the event level. The name describes what severity level of events this level is used for.</summary>
		/// <returns>Returns a string that contains the localized name for the event level.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000560B4 File Offset: 0x000542B4
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the non-localized name of the event level.</summary>
		/// <returns>Returns a string that contains the non-localized name of the event level.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value of the event level.</summary>
		/// <returns>Returns an integer value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x000565A8 File Offset: 0x000547A8
		public int Value
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
