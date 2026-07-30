using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdated" /> event.</summary>
	// Token: 0x020003A4 RID: 932
	public class FormViewUpdatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewUpdatedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the update operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the update operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x06002530 RID: 9520 RVA: 0x00060C0E File Offset: 0x0005EE0E
		public FormViewUpdatedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepEditMode = false;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00060C32 File Offset: 0x0005EE32
		internal FormViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.oldValues = oldValues;
			this.newValues = newValues;
		}

		/// <summary>Gets the number of rows affected by the update operation.</summary>
		/// <returns>The number of rows affected by the update operation.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x00060C53 File Offset: 0x0005EE53
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> object that represents the exception that was raised during the update operation.</returns>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x00060C5B File Offset: 0x0005EE5B
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the update operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x00060C63 File Offset: 0x0005EE63
		// (set) Token: 0x06002535 RID: 9525 RVA: 0x00060C6B File Offset: 0x0005EE6B
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FormView" /> control should remain in edit mode after an update operation.</summary>
		/// <returns>true to remain in edit mode after an update operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x00060C74 File Offset: 0x0005EE74
		// (set) Token: 0x06002537 RID: 9527 RVA: 0x00060C7C File Offset: 0x0005EE7C
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

		/// <summary>Gets a dictionary that contains the original key field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original key field name/value pairs for the updated record.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x00060C85 File Offset: 0x0005EE85
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the updated record.</returns>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x00060C8D File Offset: 0x0005EE8D
		public IOrderedDictionary NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		/// <summary>Gets a dictionary that contains the original non-key field name/value pairs for the updated record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the updated record.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x00060C95 File Offset: 0x0005EE95
		public IOrderedDictionary OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x040019F3 RID: 6643
		private int rowsAffected;

		// Token: 0x040019F4 RID: 6644
		private Exception e;

		// Token: 0x040019F5 RID: 6645
		private bool exceptionHandled;

		// Token: 0x040019F6 RID: 6646
		private bool keepEditMode;

		// Token: 0x040019F7 RID: 6647
		private IOrderedDictionary keys;

		// Token: 0x040019F8 RID: 6648
		private IOrderedDictionary oldValues;

		// Token: 0x040019F9 RID: 6649
		private IOrderedDictionary newValues;
	}
}
