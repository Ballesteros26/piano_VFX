using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes how an instruction alters the flow of control.</summary>
	// Token: 0x0200035C RID: 860
	[ComVisible(true)]
	[Serializable]
	public enum FlowControl
	{
		/// <summary>Branch instruction.</summary>
		// Token: 0x04001419 RID: 5145
		Branch,
		/// <summary>Break instruction.</summary>
		// Token: 0x0400141A RID: 5146
		Break,
		/// <summary>Call instruction.</summary>
		// Token: 0x0400141B RID: 5147
		Call,
		/// <summary>Conditional branch instruction.</summary>
		// Token: 0x0400141C RID: 5148
		Cond_Branch,
		/// <summary>Provides information about a subsequent instruction. For example, the Unaligned instruction of Reflection.Emit.Opcodes has FlowControl.Meta and specifies that the subsequent pointer instruction might be unaligned.</summary>
		// Token: 0x0400141D RID: 5149
		Meta,
		/// <summary>Normal flow of control.</summary>
		// Token: 0x0400141E RID: 5150
		Next,
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x0400141F RID: 5151
		[Obsolete("This API has been deprecated.")]
		Phi,
		/// <summary>Return instruction.</summary>
		// Token: 0x04001420 RID: 5152
		Return,
		/// <summary>Exception throw instruction.</summary>
		// Token: 0x04001421 RID: 5153
		Throw
	}
}
