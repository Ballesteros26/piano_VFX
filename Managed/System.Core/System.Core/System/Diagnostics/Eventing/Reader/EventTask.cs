using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an event task that is defined in an event provider. The task identifies a portion of an application or a component that publishes an event. A task is a 16-bit value with 16 top values reserved.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A3 RID: 931
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventTask
	{
		// Token: 0x06001BA0 RID: 7072 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventTask()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name for the event task.</summary>
		/// <returns>Returns a string that contains the localized name for the event task.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000560B4 File Offset: 0x000542B4
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the event globally unique identifier (GUID) associated with the task. </summary>
		/// <returns>Returns a GUID value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x00056A9C File Offset: 0x00054C9C
		public Guid EventGuid
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		/// <summary>Gets the non-localized name of the event task.</summary>
		/// <returns>Returns a string that contains the non-localized name of the event task.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value associated with the task.</summary>
		/// <returns>Returns an integer value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00056AB8 File Offset: 0x00054CB8
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
