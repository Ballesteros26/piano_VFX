using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Specifies the attributes of a property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011E RID: 286
	[Flags]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("PropertyAttributes has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
	public enum PropertyAttributes
	{
		/// <summary>The property is not supported by the provider.</summary>
		// Token: 0x040009FB RID: 2555
		NotSupported = 0,
		/// <summary>The user must specify a value for this property before the data source is initialized.</summary>
		// Token: 0x040009FC RID: 2556
		Required = 1,
		/// <summary>The user does not need to specify a value for this property before the data source is initialized.</summary>
		// Token: 0x040009FD RID: 2557
		Optional = 2,
		/// <summary>The user can read the property.</summary>
		// Token: 0x040009FE RID: 2558
		Read = 512,
		/// <summary>The user can write to the property.</summary>
		// Token: 0x040009FF RID: 2559
		Write = 1024
	}
}
