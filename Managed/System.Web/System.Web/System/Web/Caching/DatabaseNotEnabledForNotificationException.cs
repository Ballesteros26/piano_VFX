using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>The exception that is thrown when a SQL Server database is not enabled to support dependencies associated with the <see cref="T:System.Web.Caching.SqlCacheDependency" /> class. This class cannot be inherited. </summary>
	// Token: 0x0200068D RID: 1677
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class DatabaseNotEnabledForNotificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class.</summary>
		// Token: 0x06004773 RID: 18291 RVA: 0x000C8FBC File Offset: 0x000C71BC
		public DatabaseNotEnabledForNotificationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class.</summary>
		/// <param name="message">A string that describes the error. </param>
		// Token: 0x06004774 RID: 18292 RVA: 0x000C8FC4 File Offset: 0x000C71C4
		public DatabaseNotEnabledForNotificationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06004775 RID: 18293 RVA: 0x000C8FCD File Offset: 0x000C71CD
		public DatabaseNotEnabledForNotificationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
