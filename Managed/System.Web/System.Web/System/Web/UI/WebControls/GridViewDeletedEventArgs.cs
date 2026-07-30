using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleted" /> event.</summary>
	// Token: 0x020003A7 RID: 935
	public class GridViewDeletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewDeletedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x06002626 RID: 9766 RVA: 0x00064633 File Offset: 0x00062833
		public GridViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00064650 File Offset: 0x00062850
		internal GridViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the delete operation.</summary>
		/// <returns>The number of rows affected by the delete operation.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00064669 File Offset: 0x00062869
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00064671 File Offset: 0x00062871
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00064679 File Offset: 0x00062879
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x00064681 File Offset: 0x00062881
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
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x0006468A File Offset: 0x0006288A
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the deleted record.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the deleted record.</returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00064692 File Offset: 0x00062892
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001A39 RID: 6713
		private int rowsAffected;

		// Token: 0x04001A3A RID: 6714
		private Exception e;

		// Token: 0x04001A3B RID: 6715
		private bool exceptionHandled;

		// Token: 0x04001A3C RID: 6716
		private IOrderedDictionary keys;

		// Token: 0x04001A3D RID: 6717
		private IOrderedDictionary values;
	}
}
