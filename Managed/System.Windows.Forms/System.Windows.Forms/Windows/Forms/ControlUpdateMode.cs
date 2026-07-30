using System;

namespace System.Windows.Forms
{
	/// <summary>Determines when changes to a data source value get propagated to the corresponding data-bound control property.</summary>
	// Token: 0x020000B0 RID: 176
	public enum ControlUpdateMode
	{
		/// <summary>The bound control is updated when the data source value changes, or the data source position changes.</summary>
		// Token: 0x04000852 RID: 2130
		OnPropertyChanged,
		/// <summary>The bound control is never updated when a data source value changes. The data source is write-only.</summary>
		// Token: 0x04000853 RID: 2131
		Never
	}
}
