using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction of the binding operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000059 RID: 89
	public enum BindingCompleteContext
	{
		/// <summary>An indication that the control property value is being updated from the data source.</summary>
		// Token: 0x04000624 RID: 1572
		ControlUpdate,
		/// <summary>An indication that the data source value is being updated from the control property.</summary>
		// Token: 0x04000625 RID: 1573
		DataSourceUpdate
	}
}
