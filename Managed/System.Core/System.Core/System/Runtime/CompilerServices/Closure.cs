using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the runtime state of a dynamically generated method.</summary>
	// Token: 0x020002F9 RID: 761
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class Closure
	{
		/// <summary>Creates an object to hold state of a dynamically generated method.</summary>
		/// <param name="constants">The constant values that are used by the method.</param>
		/// <param name="locals">The hoisted local variables from the parent context.</param>
		// Token: 0x0600172D RID: 5933 RVA: 0x0004C333 File Offset: 0x0004A533
		public Closure(object[] constants, object[] locals)
		{
			this.Constants = constants;
			this.Locals = locals;
		}

		/// <summary>Represents the non-trivial constants and locally executable expressions that are referenced by a dynamically generated method.</summary>
		// Token: 0x04000AC1 RID: 2753
		public readonly object[] Constants;

		/// <summary>Represents the hoisted local variables from the parent context.</summary>
		// Token: 0x04000AC2 RID: 2754
		public readonly object[] Locals;
	}
}
