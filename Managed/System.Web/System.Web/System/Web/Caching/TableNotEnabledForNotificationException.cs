using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Web.Caching.SqlCacheDependency" /> class is used against a database table that is not enabled for change notifications.</summary>
	// Token: 0x02000698 RID: 1688
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class TableNotEnabledForNotificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.TableNotEnabledForNotificationException" /> class.</summary>
		// Token: 0x060047B7 RID: 18359 RVA: 0x000C8FBC File Offset: 0x000C71BC
		public TableNotEnabledForNotificationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.TableNotEnabledForNotificationException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060047B8 RID: 18360 RVA: 0x000C8FC4 File Offset: 0x000C71C4
		public TableNotEnabledForNotificationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.TableNotEnabledForNotificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060047B9 RID: 18361 RVA: 0x000C8FCD File Offset: 0x000C71CD
		public TableNotEnabledForNotificationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
