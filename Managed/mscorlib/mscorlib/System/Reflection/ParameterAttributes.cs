using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a parameter. These are defined in CorHdr.h.</summary>
	// Token: 0x020002F9 RID: 761
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum ParameterAttributes
	{
		/// <summary>Specifies that there is no parameter attribute.</summary>
		// Token: 0x04001292 RID: 4754
		None = 0,
		/// <summary>Specifies that the parameter is an input parameter.</summary>
		// Token: 0x04001293 RID: 4755
		In = 1,
		/// <summary>Specifies that the parameter is an output parameter.</summary>
		// Token: 0x04001294 RID: 4756
		Out = 2,
		/// <summary>Specifies that the parameter is a locale identifier (lcid).</summary>
		// Token: 0x04001295 RID: 4757
		Lcid = 4,
		/// <summary>Specifies that the parameter is a return value.</summary>
		// Token: 0x04001296 RID: 4758
		Retval = 8,
		/// <summary>Specifies that the parameter is optional.</summary>
		// Token: 0x04001297 RID: 4759
		Optional = 16,
		/// <summary>Specifies that the parameter is reserved.</summary>
		// Token: 0x04001298 RID: 4760
		ReservedMask = 61440,
		/// <summary>Specifies that the parameter has a default value.</summary>
		// Token: 0x04001299 RID: 4761
		HasDefault = 4096,
		/// <summary>Specifies that the parameter has field marshaling information.</summary>
		// Token: 0x0400129A RID: 4762
		HasFieldMarshal = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x0400129B RID: 4763
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x0400129C RID: 4764
		Reserved4 = 32768
	}
}
