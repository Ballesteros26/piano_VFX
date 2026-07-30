using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200083A RID: 2106
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x060053A8 RID: 21416 RVA: 0x00125C88 File Offset: 0x00123E88
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x00125CA5 File Offset: 0x00123EA5
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x04002B80 RID: 11136
		private readonly string[] _transformNames;
	}
}
