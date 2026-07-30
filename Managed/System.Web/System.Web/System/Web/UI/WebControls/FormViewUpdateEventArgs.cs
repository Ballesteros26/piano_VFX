using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdating" /> event.</summary>
	// Token: 0x020003A3 RID: 931
	public class FormViewUpdateEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs" /> class.</summary>
		/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		// Token: 0x0600252A RID: 9514 RVA: 0x00060BC0 File Offset: 0x0005EDC0
		public FormViewUpdateEventArgs(object commandArgument)
		{
			this.argument = commandArgument;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00060BCF File Offset: 0x0005EDCF
		internal FormViewUpdateEventArgs(object argument, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(argument)
		{
			this.keys = keys;
			this.oldValues = oldValues;
			this.newValues = newValues;
		}

		/// <summary>Gets the command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x00060BEE File Offset: 0x0005EDEE
		public object CommandArgument
		{
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets a dictionary that contains the original key field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original key field name/value pairs for the record to update.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x00060BF6 File Offset: 0x0005EDF6
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the record to update.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x00060BFE File Offset: 0x0005EDFE
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original non-key field name/value pairs for the record to update.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original non-key field name/value pairs for the record to update.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x00060C06 File Offset: 0x0005EE06
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x040019EF RID: 6639
		private object argument;

		// Token: 0x040019F0 RID: 6640
		private IOrderedDictionary keys;

		// Token: 0x040019F1 RID: 6641
		private IOrderedDictionary oldValues;

		// Token: 0x040019F2 RID: 6642
		private IOrderedDictionary newValues;
	}
}
