using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserting" /> event.</summary>
	// Token: 0x0200038A RID: 906
	public class DetailsViewInsertEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewInsertEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		// Token: 0x060023A3 RID: 9123 RVA: 0x0005CD84 File Offset: 0x0005AF84
		public DetailsViewInsertEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0005CD93 File Offset: 0x0005AF93
		internal DetailsViewInsertEventArgs(object argument, IOrderedDictionary values)
		{
			this.argument = argument;
			this.values = values;
		}

		/// <summary>Gets the command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0005CDA9 File Offset: 0x0005AFA9
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the field name/value pairs for the record to insert.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to insert.</returns>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0005CDB1 File Offset: 0x0005AFB1
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x0400197E RID: 6526
		private object argument;

		// Token: 0x0400197F RID: 6527
		private IOrderedDictionary values;
	}
}
