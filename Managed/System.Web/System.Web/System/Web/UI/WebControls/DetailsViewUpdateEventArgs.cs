using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdating" /> event.</summary>
	// Token: 0x0200038F RID: 911
	public class DetailsViewUpdateEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewUpdateEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		// Token: 0x060023C1 RID: 9153 RVA: 0x0005CF0B File Offset: 0x0005B10B
		public DetailsViewUpdateEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x0005CF1A File Offset: 0x0005B11A
		internal DetailsViewUpdateEventArgs(object argument, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		{
			this.argument = argument;
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x0005CF3F File Offset: 0x0005B13F
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the key field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to update.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x0005CF47 File Offset: 0x0005B147
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the record to update.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0005CF4F File Offset: 0x0005B14F
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the record to update.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0005CF57 File Offset: 0x0005B157
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x0400198A RID: 6538
		private object argument;

		// Token: 0x0400198B RID: 6539
		private IOrderedDictionary keys;

		// Token: 0x0400198C RID: 6540
		private IOrderedDictionary newValues;

		// Token: 0x0400198D RID: 6541
		private IOrderedDictionary oldValues;
	}
}
