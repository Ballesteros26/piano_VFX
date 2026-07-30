using System;
using System.Security;

namespace System.Data.SqlClient
{
	/// <summary>
	///   <see cref="T:System.Data.SqlClient.SqlCredential" /> provides a more secure way to specify the password for a login attempt using SQL Server Authentication.<see cref="T:System.Data.SqlClient.SqlCredential" /> is comprised of a user id and a password that will be used for SQL Server Authentication. The password in a <see cref="T:System.Data.SqlClient.SqlCredential" /> object is of type <see cref="T:System.Security.SecureString" />.<see cref="T:System.Data.SqlClient.SqlCredential" /> cannot be inherited.Windows Authentication (Integrated Security = true) remains the most secure way to log in to a SQL Server database.</summary>
	// Token: 0x0200023A RID: 570
	[Serializable]
	public sealed class SqlCredential
	{
		/// <summary>Creates an object of type <see cref="T:System.Data.SqlClient.SqlCredential" />.</summary>
		/// <param name="userId">The user id.</param>
		/// <param name="password">The password; a <see cref="T:System.Security.SecureString" /> value marked as read-only.  Passing a read/write <see cref="T:System.Security.SecureString" /> parameter will raise an <see cref="T:System.ArgumentException" />.</param>
		// Token: 0x060019BA RID: 6586 RVA: 0x00082CD0 File Offset: 0x00080ED0
		public SqlCredential(string userId, SecureString password)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.uid = userId;
			this.pwd = password;
		}

		/// <summary>Returns the user ID component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <returns>Returns the user ID component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object..</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x00082D0D File Offset: 0x00080F0D
		public string UserId
		{
			get
			{
				return this.uid;
			}
		}

		/// <summary>Returns the password component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <returns>Returns the password component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00082D15 File Offset: 0x00080F15
		public SecureString Password
		{
			get
			{
				return this.pwd;
			}
		}

		// Token: 0x0400124C RID: 4684
		private string uid = "";

		// Token: 0x0400124D RID: 4685
		private SecureString pwd;
	}
}
