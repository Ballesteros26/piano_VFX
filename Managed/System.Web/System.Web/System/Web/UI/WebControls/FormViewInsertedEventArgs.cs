using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserted" /> event.</summary>
	// Token: 0x020003A0 RID: 928
	public class FormViewInsertedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewInsertedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the insert operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the insert operation was performed. If no exception was raised, use null for this parameter.</param>
		// Token: 0x06002518 RID: 9496 RVA: 0x00060A6A File Offset: 0x0005EC6A
		public FormViewInsertedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00060A80 File Offset: 0x0005EC80
		internal FormViewInsertedEventArgs(int affectedRows, Exception e, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the insert operation.</summary>
		/// <returns>The number of rows affected by the insert operation.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00060A91 File Offset: 0x0005EC91
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the insert operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the insert operation.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x00060A99 File Offset: 0x0005EC99
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the insert operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00060AA1 File Offset: 0x0005ECA1
		// (set) Token: 0x0600251D RID: 9501 RVA: 0x00060AA9 File Offset: 0x0005ECA9
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FormView" /> control should remain in insert mode after an insert operation.</summary>
		/// <returns>true to remain in insert mode after an insert operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00060AB2 File Offset: 0x0005ECB2
		// (set) Token: 0x0600251F RID: 9503 RVA: 0x00060ABA File Offset: 0x0005ECBA
		public bool KeepInInsertMode
		{
			get
			{
				return this.keepInsertedMode;
			}
			set
			{
				this.keepInsertedMode = value;
			}
		}

		/// <summary>Gets a dictionary that contains the field name/value pairs for the inserted record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains a dictionary of key field name/value pairs for the inserted record.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x00060AC3 File Offset: 0x0005ECC3
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019E6 RID: 6630
		private int rowsAffected;

		// Token: 0x040019E7 RID: 6631
		private Exception e;

		// Token: 0x040019E8 RID: 6632
		private bool exceptionHandled;

		// Token: 0x040019E9 RID: 6633
		private bool keepInsertedMode;

		// Token: 0x040019EA RID: 6634
		private IOrderedDictionary values;
	}
}
