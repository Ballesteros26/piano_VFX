using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies identifiers to indicate the return value of a dialog box.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000149 RID: 329
	[ComVisible(true)]
	public enum DialogResult
	{
		/// <summary>Nothing is returned from the dialog box. This means that the modal dialog continues running.</summary>
		// Token: 0x04000C99 RID: 3225
		None,
		/// <summary>The dialog box return value is OK (usually sent from a button labeled OK).</summary>
		// Token: 0x04000C9A RID: 3226
		OK,
		/// <summary>The dialog box return value is Cancel (usually sent from a button labeled Cancel).</summary>
		// Token: 0x04000C9B RID: 3227
		Cancel,
		/// <summary>The dialog box return value is Abort (usually sent from a button labeled Abort).</summary>
		// Token: 0x04000C9C RID: 3228
		Abort,
		/// <summary>The dialog box return value is Retry (usually sent from a button labeled Retry).</summary>
		// Token: 0x04000C9D RID: 3229
		Retry,
		/// <summary>The dialog box return value is Ignore (usually sent from a button labeled Ignore).</summary>
		// Token: 0x04000C9E RID: 3230
		Ignore,
		/// <summary>The dialog box return value is Yes (usually sent from a button labeled Yes).</summary>
		// Token: 0x04000C9F RID: 3231
		Yes,
		/// <summary>The dialog box return value is No (usually sent from a button labeled No).</summary>
		// Token: 0x04000CA0 RID: 3232
		No
	}
}
