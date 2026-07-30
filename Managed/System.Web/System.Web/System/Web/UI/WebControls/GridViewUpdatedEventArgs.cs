using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdated" /> event.</summary>
	// Token: 0x020003AB RID: 939
	public class GridViewUpdatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewUpdatedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows that were affected by the update operation.</param>
		/// <param name="e">The exception that was raised when the update operation was performed. If no exception was raised, use null for this parameter.</param>
		// Token: 0x0600264A RID: 9802 RVA: 0x00064800 File Offset: 0x00062A00
		public GridViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepEditMode = false;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00064824 File Offset: 0x00062A24
		internal GridViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
		}

		/// <summary>Gets the number of rows that were affected by the update operation.</summary>
		/// <returns>The number of rows that were affected by the update operation.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x00064845 File Offset: 0x00062A45
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
		/// <returns>The exception that was raised during the update operation. If no exceptions were raised, this property returns null.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0006484D File Offset: 0x00062A4D
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value that indicates whether an exception that was raised during the update operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00064855 File Offset: 0x00062A55
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x0006485D File Offset: 0x00062A5D
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.GridView" /> control should remain in edit mode after an update operation.</summary>
		/// <returns>true if the control will remain in edit mode after an update operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x00064866 File Offset: 0x00062A66
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x0006486E File Offset: 0x00062A6E
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
		/// <returns>A dictionary of key field name/value pairs for the updated record.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x00064877 File Offset: 0x00062A77
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
		/// <returns>A dictionary of the new field name/value pairs for the updated record.</returns>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x0006487F File Offset: 0x00062A7F
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original field name/value pairs for the updated record.</summary>
		/// <returns>A dictionary of the original field name/value pairs for the updated record.</returns>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00064887 File Offset: 0x00062A87
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x04001A48 RID: 6728
		private int rowsAffected;

		// Token: 0x04001A49 RID: 6729
		private Exception e;

		// Token: 0x04001A4A RID: 6730
		private bool exceptionHandled;

		// Token: 0x04001A4B RID: 6731
		private bool keepEditMode;

		// Token: 0x04001A4C RID: 6732
		private IOrderedDictionary keys;

		// Token: 0x04001A4D RID: 6733
		private IOrderedDictionary newValues;

		// Token: 0x04001A4E RID: 6734
		private IOrderedDictionary oldValues;
	}
}
