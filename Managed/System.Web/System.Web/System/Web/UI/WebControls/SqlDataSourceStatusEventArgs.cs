using System;
using System.Data.Common;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for an event that is raised by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control after a data operation has completed.</summary>
	// Token: 0x02000314 RID: 788
	public class SqlDataSourceStatusEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> class, using the specified output parameters, return value, and number of rows affected by the database operation.</summary>
		/// <param name="command">The <see cref="T:System.Data.Common.DbCommand" /> that represents the database query, command, or stored procedure that is submitted to the database by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control. </param>
		/// <param name="affectedRows">The number of rows affected by the database operation, if applicable. </param>
		/// <param name="exception">An <see cref="T:System.Exception" /> thrown by the database operation, if applicable.</param>
		// Token: 0x06001C0D RID: 7181 RVA: 0x00046323 File Offset: 0x00044523
		public SqlDataSourceStatusEventArgs(DbCommand command, int affectedRows, Exception exception)
		{
			this._command = command;
			this._affectedRows = affectedRows;
			this._exception = exception;
		}

		/// <summary>Gets the number of rows affected by a database operation.</summary>
		/// <returns>The number of rows affected by a database operation. The default value is -1.</returns>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00046340 File Offset: 0x00044540
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		/// <summary>Gets the database command submitted to the database.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbCommand" /> object that represents the database command submitted to the database.</returns>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00046348 File Offset: 0x00044548
		public DbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		/// <summary>Gets a wrapper for any exceptions thrown by the database during a data operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that wraps any exceptions thrown by the database in its <see cref="P:System.Exception.InnerException" /> property.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00046350 File Offset: 0x00044550
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception thrown by the database has been handled.</summary>
		/// <returns>true if an exception thrown by the database has been handled and should not be thrown by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control; otherwise, false.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00046358 File Offset: 0x00044558
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00046360 File Offset: 0x00044560
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x0400176D RID: 5997
		private DbCommand _command;

		// Token: 0x0400176E RID: 5998
		private Exception _exception;

		// Token: 0x0400176F RID: 5999
		private bool _exceptionHandled;

		// Token: 0x04001770 RID: 6000
		private int _affectedRows;
	}
}
