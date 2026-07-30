using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdated" /> event.</summary>
	// Token: 0x02000390 RID: 912
	public class DetailsViewUpdatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewUpdatedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the update operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the update operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x060023C7 RID: 9159 RVA: 0x0005CF5F File Offset: 0x0005B15F
		public DetailsViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepEditMode = false;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0005CF83 File Offset: 0x0005B183
		internal DetailsViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the number of rows affected by the update operation.</summary>
		/// <returns>The number of rows affected by the update operation.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x0005CFA4 File Offset: 0x0005B1A4
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the update operation.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x0005CFAC File Offset: 0x0005B1AC
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the update operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x0005CFB4 File Offset: 0x0005B1B4
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x0005CFBC File Offset: 0x0005B1BC
		public bool ExceptionHandled
		{
			get
			{
				return this.exceptionHandled;
			}
			set
			{
				this.exceptionHandled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control should remain in edit mode after an update operation.</summary>
		/// <returns>true to remain in edit mode after an update operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x0005CFC5 File Offset: 0x0005B1C5
		// (set) Token: 0x060023CE RID: 9166 RVA: 0x0005CFCD File Offset: 0x0005B1CD
		public bool KeepInEditMode
		{
			get
			{
				return this.keepEditMode;
			}
			set
			{
				this.keepEditMode = value;
			}
		}

		/// <summary>Gets a dictionary that contains the key field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the updated record.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0005CFD6 File Offset: 0x0005B1D6
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the updated record.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0005CFDE File Offset: 0x0005B1DE
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the updated record.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x0005CFE6 File Offset: 0x0005B1E6
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x0400198E RID: 6542
		private int rowsAffected;

		// Token: 0x0400198F RID: 6543
		private Exception e;

		// Token: 0x04001990 RID: 6544
		private bool exceptionHandled;

		// Token: 0x04001991 RID: 6545
		private bool keepEditMode;

		// Token: 0x04001992 RID: 6546
		private IOrderedDictionary keys;

		// Token: 0x04001993 RID: 6547
		private IOrderedDictionary newValues;

		// Token: 0x04001994 RID: 6548
		private IOrderedDictionary oldValues;
	}
}
