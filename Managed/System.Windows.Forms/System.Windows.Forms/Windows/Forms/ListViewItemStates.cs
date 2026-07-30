using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000238 RID: 568
	[Flags]
	public enum ListViewItemStates
	{
		/// <summary>The item is selected.</summary>
		// Token: 0x040012D9 RID: 4825
		Selected = 1,
		/// <summary>The item is disabled.</summary>
		// Token: 0x040012DA RID: 4826
		Grayed = 2,
		/// <summary>The item is checked.</summary>
		// Token: 0x040012DB RID: 4827
		Checked = 8,
		/// <summary>The item has focus.</summary>
		// Token: 0x040012DC RID: 4828
		Focused = 16,
		/// <summary>The item is in its default state.</summary>
		// Token: 0x040012DD RID: 4829
		Default = 32,
		/// <summary>The item is currently under the mouse pointer.</summary>
		// Token: 0x040012DE RID: 4830
		Hot = 64,
		/// <summary>The item is marked.</summary>
		// Token: 0x040012DF RID: 4831
		Marked = 128,
		/// <summary>The item is in an indeterminate state.</summary>
		// Token: 0x040012E0 RID: 4832
		Indeterminate = 256,
		/// <summary>The item should indicate a keyboard shortcut.</summary>
		// Token: 0x040012E1 RID: 4833
		ShowKeyboardCues = 512
	}
}
