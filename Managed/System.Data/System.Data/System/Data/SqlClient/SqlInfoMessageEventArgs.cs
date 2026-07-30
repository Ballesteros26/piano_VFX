using System;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C2 RID: 450
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x00069FBC File Offset: 0x000681BC
		internal SqlInfoMessageEventArgs(SqlException exception)
		{
			this._exception = exception;
		}

		/// <summary>Gets the collection of warnings sent from the server.</summary>
		/// <returns>The collection of warnings sent from the server.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00069FCB File Offset: 0x000681CB
		public SqlErrorCollection Errors
		{
			get
			{
				return this._exception.Errors;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00069FD8 File Offset: 0x000681D8
		private bool ShouldSerializeErrors()
		{
			return this._exception != null && 0 < this._exception.Errors.Count;
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00069FF7 File Offset: 0x000681F7
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0006A004 File Offset: 0x00068204
		public string Source
		{
			get
			{
				return this._exception.Source;
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014F0 RID: 5360 RVA: 0x0006A011 File Offset: 0x00068211
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00010468 File Offset: 0x0000E668
		internal SqlInfoMessageEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000E2B RID: 3627
		private SqlException _exception;
	}
}
