using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies when a data source is updated when changes occur in the bound control.</summary>
	// Token: 0x02000140 RID: 320
	public enum DataSourceUpdateMode
	{
		/// <summary>Data source is updated when the control property is validated, </summary>
		// Token: 0x04000C42 RID: 3138
		OnValidation,
		/// <summary>Data source is updated whenever the value of the control property changes. </summary>
		// Token: 0x04000C43 RID: 3139
		OnPropertyChanged,
		/// <summary>Data source is never updated and values entered into the control are not parsed, validated or re-formatted.</summary>
		// Token: 0x04000C44 RID: 3140
		Never
	}
}
