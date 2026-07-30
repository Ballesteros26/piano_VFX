using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserted" /> event.</summary>
	// Token: 0x0200038B RID: 907
	public class DetailsViewInsertedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewInsertedEventArgs" /> class.</summary>
		/// <param name="affectedRows">The number of rows affected by the insert operation.</param>
		/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the insert operation was performed. If no exception was raised, use null for this parameter.</param>
		// Token: 0x060023A7 RID: 9127 RVA: 0x0005CDB9 File Offset: 0x0005AFB9
		public DetailsViewInsertedEventArgs(int affectedRows, Exception e)
		{
			this.rowsAffected = affectedRows;
			this.e = e;
			this.exceptionHandled = false;
			this.keepInsertedMode = false;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0005CDDD File Offset: 0x0005AFDD
		internal DetailsViewInsertedEventArgs(int affectedRows, Exception e, IOrderedDictionary values)
			: this(affectedRows, e)
		{
			this.values = values;
		}

		/// <summary>Gets the number of rows affected by the insert operation.</summary>
		/// <returns>The number of rows affected by the insert operation.</returns>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x0005CDEE File Offset: 0x0005AFEE
		public int AffectedRows
		{
			get
			{
				return this.rowsAffected;
			}
		}

		/// <summary>Gets the exception (if any) that was raised during the insert operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the insert operation.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0005CDF6 File Offset: 0x0005AFF6
		public Exception Exception
		{
			get
			{
				return this.e;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was raised during the insert operation was handled in the event handler.</summary>
		/// <returns>true if the exception was handled in the event handler; otherwise, false. The default is false.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x0005CDFE File Offset: 0x0005AFFE
		// (set) Token: 0x060023AC RID: 9132 RVA: 0x0005CE06 File Offset: 0x0005B006
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control should remain in insert mode after an insert operation.</summary>
		/// <returns>true to remain in insert mode after an insert operation; otherwise, false. The default is false.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x0005CE0F File Offset: 0x0005B00F
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x0005CE17 File Offset: 0x0005B017
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
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the inserted record.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x0005CE20 File Offset: 0x0005B020
		public IOrderedDictionary Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04001980 RID: 6528
		private int rowsAffected;

		// Token: 0x04001981 RID: 6529
		private Exception e;

		// Token: 0x04001982 RID: 6530
		private bool exceptionHandled;

		// Token: 0x04001983 RID: 6531
		private bool keepInsertedMode;

		// Token: 0x04001984 RID: 6532
		private IOrderedDictionary values;
	}
}
