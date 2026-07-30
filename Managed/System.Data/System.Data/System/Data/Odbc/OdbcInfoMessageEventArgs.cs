using System;
using System.Text;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A4 RID: 676
	public sealed class OdbcInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001CAF RID: 7343 RVA: 0x0008E035 File Offset: 0x0008C235
		internal OdbcInfoMessageEventArgs(OdbcErrorCollection errors)
		{
			this._errors = errors;
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0008E044 File Offset: 0x0008C244
		public OdbcErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x0008E04C File Offset: 0x0008C24C
		public string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.Errors)
				{
					OdbcError odbcError = (OdbcError)obj;
					if (0 < stringBuilder.Length)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(odbcError.Message);
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x0008E0CC File Offset: 0x0008C2CC
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00010468 File Offset: 0x0000E668
		internal OdbcInfoMessageEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001545 RID: 5445
		private OdbcErrorCollection _errors;
	}
}
