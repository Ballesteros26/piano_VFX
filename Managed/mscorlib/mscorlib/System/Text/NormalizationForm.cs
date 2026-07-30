using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	/// <summary>Defines the type of normalization to perform.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029A RID: 666
	[ComVisible(true)]
	public enum NormalizationForm
	{
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x040010B4 RID: 4276
		FormC = 1,
		/// <summary>Indicates that a Unicode string is normalized using full canonical decomposition.</summary>
		// Token: 0x040010B5 RID: 4277
		FormD,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition, followed by the replacement of sequences with their primary composites, if possible.</summary>
		// Token: 0x040010B6 RID: 4278
		FormKC = 5,
		/// <summary>Indicates that a Unicode string is normalized using full compatibility decomposition.</summary>
		// Token: 0x040010B7 RID: 4279
		FormKD
	}
}
