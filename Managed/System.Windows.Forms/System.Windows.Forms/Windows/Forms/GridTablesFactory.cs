using System;

namespace System.Windows.Forms
{
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A9 RID: 425
	public sealed class GridTablesFactory
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x0006B768 File Offset: 0x00069968
		internal GridTablesFactory()
		{
		}

		/// <summary>Returns the specified <see cref="P:System.Windows.Forms.DataGridColumnStyle.DataGridTableStyle" /> in a one-element array.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects.</returns>
		/// <param name="gridTable">A <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" />.</param>
		/// <param name="dataMember">A <see cref="T:System.String" />.</param>
		/// <param name="bindingManager">A <see cref="T:System.Windows.Forms.BindingContext" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BBC RID: 7100 RVA: 0x0006B770 File Offset: 0x00069970
		public static DataGridTableStyle[] CreateGridTables(DataGridTableStyle gridTable, object dataSource, string dataMember, BindingContext bindingManager)
		{
			throw new NotImplementedException();
		}
	}
}
