using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserting" /> event.</summary>
	// Token: 0x0200039F RID: 927
	public class FormViewInsertEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		// Token: 0x06002514 RID: 9492 RVA: 0x00060A35 File Offset: 0x0005EC35
		public FormViewInsertEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00060A44 File Offset: 0x0005EC44
		internal FormViewInsertEventArgs(object argument, IOrderedDictionary values)
		{
			this.values = values;
			this.argument = argument;
		}

		/// <summary>Gets the command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00060A5A File Offset: 0x0005EC5A
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the field name/value pairs for the record to insert.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to insert.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x00060A62 File Offset: 0x0005EC62
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019E4 RID: 6628
		private object argument;

		// Token: 0x040019E5 RID: 6629
		private IOrderedDictionary values;
	}
}
