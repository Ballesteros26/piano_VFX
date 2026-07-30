using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains an event opcode that is defined in an event provider. An opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A2 RID: 930
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventOpcode
	{
		// Token: 0x06001B9C RID: 7068 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventOpcode()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name for an event opcode.</summary>
		/// <returns>Returns a string that contains the localized name for an event opcode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x000560B4 File Offset: 0x000542B4
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the non-localized name for an event opcode.</summary>
		/// <returns>Returns a string that contains the non-localized name for an event opcode.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the numeric value associated with the event opcode.</summary>
		/// <returns>Returns an integer value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00056A80 File Offset: 0x00054C80
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
