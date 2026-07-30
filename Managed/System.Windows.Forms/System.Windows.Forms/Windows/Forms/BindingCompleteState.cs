using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates the result of a completed binding operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005B RID: 91
	public enum BindingCompleteState
	{
		/// <summary>An indication that the binding operation completed successfully.</summary>
		// Token: 0x0400062C RID: 1580
		Success,
		/// <summary>An indication that the binding operation failed with a data error.</summary>
		// Token: 0x0400062D RID: 1581
		DataError,
		/// <summary>An indication that the binding operation failed with an exception.</summary>
		// Token: 0x0400062E RID: 1582
		Exception
	}
}
