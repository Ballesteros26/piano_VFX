using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleted" /> event.</summary>
	// Token: 0x0200039E RID: 926
	public class FormViewDeletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewDeletedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x0600250C RID: 9484 RVA: 0x000609D5 File Offset: 0x0005EBD5
		public FormViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000609EB File Offset: 0x0005EBEB
		internal FormViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the delete operation.</summary>
		/// <returns>The number of rows affected by the delete operation.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x00060A04 File Offset: 0x0005EC04
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x00060A0C File Offset: 0x0005EC0C
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00060A14 File Offset: 0x0005EC14
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x00060A1C File Offset: 0x0005EC1C
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

		/// <summary>Gets an ordered dictionary of key field name/value pairs for the deleted record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00060A25 File Offset: 0x0005EC25
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the deleted record.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x00060A2D File Offset: 0x0005EC2D
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040019DF RID: 6623
		private int rowsAffected;

		// Token: 0x040019E0 RID: 6624
		private Exception e;

		// Token: 0x040019E1 RID: 6625
		private bool exceptionHandled;

		// Token: 0x040019E2 RID: 6626
		private IOrderedDictionary keys;

		// Token: 0x040019E3 RID: 6627
		private IOrderedDictionary values;
	}
}
