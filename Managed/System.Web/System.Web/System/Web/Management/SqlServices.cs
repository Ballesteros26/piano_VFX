using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.Management
{
	/// <summary>Supports installing and removing the SQL Server database elements of ASP.NET features.</summary>
	// Token: 0x0200074C RID: 1868
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.High)]
	public static class SqlServices
	{
		/// <summary>Generates the SQL scripts for the selected features. The scripts will reference the database indicated by the database parameter.</summary>
		/// <returns>Returns the generated script code.</returns>
		/// <param name="install">true to generate a script that installs the specified features; false to generate a script that removes the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the specific features for which to generate scripts.</param>
		/// <param name="database">The name of the SQL Server database to use in the generated scripts.</param>
		// Token: 0x06004CBD RID: 19645 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string GenerateApplicationServicesScripts(bool install, SqlFeatures features, string database)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Generates the SQL script for installing or removing session state. The script will reference the database indicated by the database parameter.</summary>
		/// <returns>Returns the generated script code.</returns>
		/// <param name="install">true to generate a script that installs the specified features; false to generate a script that removes the features.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state for which to generate scripts.</param>
		/// <param name="customDatabase">The name of the SQL Server database to use in the generated scripts.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		// Token: 0x06004CBE RID: 19646 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static string GenerateSessionStateScripts(bool install, SessionStateType type, string customDatabase)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Installs components for selected ASP.NET features on a SQL Server database.</summary>
		/// <param name="server">The database server on which to install the features.</param>
		/// <param name="user">The user name to use when connecting to the database.</param>
		/// <param name="password">The password to use when connecting to the database.</param>
		/// <param name="database">The database on which to install the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the features to install.</param>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="features" /> value contains one or more invalid flags.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CBF RID: 19647 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void Install(string server, string user, string password, string database, SqlFeatures features)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Installs components for selected ASP.NET features on a SQL Server database.</summary>
		/// <param name="server">The database server on which to install the features.</param>
		/// <param name="database">The database on which to install the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the features to install.</param>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="features" /> value contains one or more invalid flags.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC0 RID: 19648 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void Install(string server, string database, SqlFeatures features)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Installs components for selected ASP.NET services on a SQL Server database.</summary>
		/// <param name="database">The database on which to install the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the features to install.</param>
		/// <param name="connectionString">The connection string to use. The connection string is only used to establish a connection to the database server.  Specifying a database in the connection string has no effect.</param>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="features" /> value contains one or more invalid flags.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC1 RID: 19649 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void Install(string database, SqlFeatures features, string connectionString)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Installs components for ASP.NET session state on a SQL Server database.</summary>
		/// <param name="server">The SQL Server instance on which to install the session-state components.</param>
		/// <param name="user">The user name to use when connecting to the database.</param>
		/// <param name="password">The password to use when connecting to the database.</param>
		/// <param name="customDatabase">The database on which to install the session-state components.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state to install.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC2 RID: 19650 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void InstallSessionState(string server, string user, string password, string customDatabase, SessionStateType type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Installs components for ASP.NET session state on a SQL Server database.</summary>
		/// <param name="server">The SQL Server instance on which to install the session-state components.</param>
		/// <param name="customDatabase">The database on which to install the session-state components.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state to install.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC3 RID: 19651 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void InstallSessionState(string server, string customDatabase, SessionStateType type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Installs components for ASP.NET session state on a SQL Server database.</summary>
		/// <param name="customDatabase">The database on which to install the session-state components.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state to install.</param>
		/// <param name="connectionString">The connection string to use. The connection string is only used to establish a connection to the database server.  Specifying a database in the connection string has no effect.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC4 RID: 19652 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void InstallSessionState(string customDatabase, SessionStateType type, string connectionString)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes components for selected ASP.NET features from a SQL Server database.</summary>
		/// <param name="server">The SQL Server instance from which to remove the features.</param>
		/// <param name="user">The user name to use when connecting to the database.</param>
		/// <param name="password">The password to use when connecting to the database.</param>
		/// <param name="database">The database from which to remove the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the features to remove.</param>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="features" /> value contained one or more invalid flags.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation, an attempt was made to uninstall from a nonexistent database, or one or more tables for the features in the specified database contained data.</exception>
		// Token: 0x06004CC5 RID: 19653 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void Uninstall(string server, string user, string password, string database, SqlFeatures features)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes components for selected ASP.NET features from a SQL Server database.</summary>
		/// <param name="server">The SQL Server instance from which to remove the features.</param>
		/// <param name="database">The database from which to remove the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the features to remove.</param>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="features" /> value contained one or more invalid flags.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation, an attempt was made to uninstall from a nonexistent database, or one or more tables for the features in the specified database contained data.</exception>
		// Token: 0x06004CC6 RID: 19654 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void Uninstall(string server, string database, SqlFeatures features)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes components for selected ASP.NET features from a SQL Server database.</summary>
		/// <param name="database">The database from which to remove the features.</param>
		/// <param name="features">A bitwise combination of the <see cref="T:System.Web.Management.SqlFeatures" /> values, specifying the features to remove.</param>
		/// <param name="connectionString">The connection string to use. The connection string is only used to establish a connection to the database server. Specifying a database in the connection string has no effect.</param>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="features" /> value contained one or more invalid flags.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation, an attempt was made to uninstall from a nonexistent database, or one or more tables for the features in the specified database contained data.</exception>
		// Token: 0x06004CC7 RID: 19655 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void Uninstall(string database, SqlFeatures features, string connectionString)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes components for ASP.NET session state from a SQL Server database.</summary>
		/// <param name="server">The SQL Server instance from which to remove the session-state components.</param>
		/// <param name="user">The user name to use when connecting to the database.</param>
		/// <param name="password">The password to use when connecting to the database.</param>
		/// <param name="customDatabase">The database from which to remove the session-state components.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state to remove.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC8 RID: 19656 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void UninstallSessionState(string server, string user, string password, string customDatabase, SessionStateType type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes components for ASP.NET session state from a SQL Server database.</summary>
		/// <param name="server">The SQL Server instance from which to remove the session-state components.</param>
		/// <param name="customDatabase">The database from which to remove the session-state components.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state to remove.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CC9 RID: 19657 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void UninstallSessionState(string server, string customDatabase, SessionStateType type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes components for ASP.NET session state from a SQL Server database.</summary>
		/// <param name="customDatabase">The database from which to remove the session-state components.</param>
		/// <param name="type">One of the <see cref="T:System.Web.Management.SessionStateType" /> values, specifying the type of session state to remove.</param>
		/// <param name="connectionString">The connection string to use.  The connection string is only used to establish a connection to the database server.  Specifying a database in the connection string has no effect.</param>
		/// <exception cref="T:System.ArgumentException">The type is Custom and the <paramref name="customDatabase" /> value is not supplied, or the type is either Temporary or Persisted and the <paramref name="customDatabase" /> value is not null.</exception>
		/// <exception cref="T:System.Web.HttpException">Unable to connect to the specified database server.</exception>
		/// <exception cref="T:System.Web.Management.SqlExecutionException">An exception occurred while processing the SQL statements required for the operation.</exception>
		// Token: 0x06004CCA RID: 19658 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void UninstallSessionState(string customDatabase, SessionStateType type, string connectionString)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
