using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleted" /> event.</summary>
	// Token: 0x02000389 RID: 905
	public class DetailsViewDeletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewDeletedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use null for this parameter.</param>
		// Token: 0x0600239B RID: 9115 RVA: 0x0005CD1D File Offset: 0x0005AF1D
		public DetailsViewDeletedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0005CD3A File Offset: 0x0005AF3A
		internal DetailsViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.keys = keys;
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the delete operation.</summary>
		/// <returns>The number of rows affected by the delete operation.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x0005CD53 File Offset: 0x0005AF53
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x0005CD5B File Offset: 0x0005AF5B
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x0005CD63 File Offset: 0x0005AF63
		// (set) Token: 0x060023A0 RID: 9120 RVA: 0x0005CD6B File Offset: 0x0005AF6B
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

		/// <summary>Gets an ordered dictionary of key field name/value pairs that contains the names and values of the key fields of the deleted items.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs used to match the item to delete.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x0005CD74 File Offset: 0x0005AF74
		public IOrderedDictionary Keys
		{
			get
			{
				return this.keys;
			}
		}

		/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the item to delete.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0005CD7C File Offset: 0x0005AF7C
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001979 RID: 6521
		private int rowsAffected;

		// Token: 0x0400197A RID: 6522
		private Exception e;

		// Token: 0x0400197B RID: 6523
		private bool exceptionHandled;

		// Token: 0x0400197C RID: 6524
		private IOrderedDictionary keys;

		// Token: 0x0400197D RID: 6525
		private IOrderedDictionary values;
	}
}
