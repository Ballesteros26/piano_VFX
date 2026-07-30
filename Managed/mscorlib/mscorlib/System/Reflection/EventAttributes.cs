using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the attributes of an event.</summary>
	// Token: 0x020002DC RID: 732
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum EventAttributes
	{
		/// <summary>Specifies that the event has no attributes.</summary>
		// Token: 0x040011A3 RID: 4515
		None = 0,
		/// <summary>Specifies that the event is special in a way described by the name.</summary>
		// Token: 0x040011A4 RID: 4516
		SpecialName = 512,
		/// <summary>Specifies a reserved flag for common language runtime use only.</summary>
		// Token: 0x040011A5 RID: 4517
		ReservedMask = 1024,
		/// <summary>Specifies that the common language runtime should check name encoding.</summary>
		// Token: 0x040011A6 RID: 4518
		RTSpecialName = 1024
	}
}
