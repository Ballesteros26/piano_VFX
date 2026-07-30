using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039A RID: 922
	internal class SmiEventSink_Default : SmiEventSink
	{
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000BFD12 File Offset: 0x000BDF12
		internal bool HasMessages
		{
			get
			{
				return this._errors != null || this._warnings != null;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x00004526 File Offset: 0x00002726
		internal virtual string ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000BFD28 File Offset: 0x000BDF28
		protected virtual void DispatchMessages()
		{
			SqlException ex = this.ProcessMessages(true);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000BFD44 File Offset: 0x000BDF44
		protected SqlException ProcessMessages(bool ignoreWarnings)
		{
			SqlException ex = null;
			SqlErrorCollection sqlErrorCollection = null;
			if (this._errors != null)
			{
				if (this._warnings != null)
				{
					foreach (object obj in this._warnings)
					{
						SqlError sqlError = (SqlError)obj;
						this._errors.Add(sqlError);
					}
				}
				sqlErrorCollection = this._errors;
				this._errors = null;
				this._warnings = null;
			}
			else
			{
				if (!ignoreWarnings)
				{
					sqlErrorCollection = this._warnings;
				}
				this._warnings = null;
			}
			if (sqlErrorCollection != null)
			{
				ex = SqlException.CreateException(sqlErrorCollection, this.ServerVersion);
			}
			return ex;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000BFDF4 File Offset: 0x000BDFF4
		internal void ProcessMessagesAndThrow()
		{
			if (this.HasMessages)
			{
				this.DispatchMessages();
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000BFE04 File Offset: 0x000BE004
		internal SmiEventSink_Default()
		{
		}

		// Token: 0x04001A59 RID: 6745
		private SqlErrorCollection _errors;

		// Token: 0x04001A5A RID: 6746
		private SqlErrorCollection _warnings;
	}
}
