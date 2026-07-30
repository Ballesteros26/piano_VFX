using System;
using System.Data.SqlClient;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines a class for SQL execution exceptions in the <see cref="N:System.Web.Management" /> namespace.</summary>
	// Token: 0x0200074B RID: 1867
	[Serializable]
	public sealed class SqlExecutionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class using default values.</summary>
		// Token: 0x06004CB4 RID: 19636 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SqlExecutionException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class using the passed message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06004CB5 RID: 19637 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SqlExecutionException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class using the passed message and exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> encountered.</param>
		// Token: 0x06004CB6 RID: 19638 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SqlExecutionException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.SqlExecutionException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="server">The SQL Server instance against which the SQL commands are run.</param>
		/// <param name="database">The database against which the SQL commands are run.</param>
		/// <param name="sqlFile">The name of the installation file containing the SQL commands being run.</param>
		/// <param name="commands">The SQL commands being run when the exception occurred.</param>
		/// <param name="sqlException">The <see cref="T:System.Data.SqlClient.SqlException" /> encountered when processing the SQL commands.</param>
		// Token: 0x06004CB7 RID: 19639 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public SqlExecutionException(string message, string server, string database, string sqlFile, string commands, SqlException sqlException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the SQL commands being run when the exception occurred.</summary>
		/// <returns>The SQL commands being run when the exception occurred.</returns>
		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Commands
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the database being accessed when the exception occurred.</summary>
		/// <returns>The name of the database being accessed when the exception occurred.</returns>
		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Database
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the exception encountered when processing the SQL commands.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlException" /> encountered when processing the SQL commands.</returns>
		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public SqlException Exception
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the SQL Server instance being accessed when the exception occurred.</summary>
		/// <returns>The name of the SQL Server instance being accessed when the exception occurred.</returns>
		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Server
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the path and name of the file containing the SQL commands being run when the exception occurred.</summary>
		/// <returns>The path and name of the file that contains the SQL commands being run when the exception occurred.</returns>
		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string SqlFile
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
