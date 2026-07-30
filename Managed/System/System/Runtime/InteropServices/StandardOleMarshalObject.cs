using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Replaces the standard common language runtime (CLR) free-threaded marshaler with the standard OLE STA marshaler.</summary>
	// Token: 0x02000360 RID: 864
	[ComVisible(true)]
	[MonoLimitation("The runtime does nothing special apart from what it already does with marshal-by-ref objects")]
	public class StandardOleMarshalObject : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.StandardOleMarshalObject" /> class. </summary>
		// Token: 0x06001AC3 RID: 6851 RVA: 0x0004E8BB File Offset: 0x0004CABB
		protected StandardOleMarshalObject()
		{
		}
	}
}
