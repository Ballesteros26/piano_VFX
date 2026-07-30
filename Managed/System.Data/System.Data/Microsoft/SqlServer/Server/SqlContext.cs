using System;
using System.Security.Principal;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Represents an abstraction of the caller's context, which provides access to the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" />, <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" />, and <see cref="T:System.Security.Principal.WindowsIdentity" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003C3 RID: 963
	public sealed class SqlContext
	{
		/// <summary>Specifies whether the calling code is running within SQL Server, and if the context connection can be accessed.</summary>
		/// <returns>True if the context connection is available and the other <see cref="T:Microsoft.SqlServer.Server.SqlContext" /> members can be accessed.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000061D5 File Offset: 0x000043D5
		public static bool IsAvailable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the pipe object that allows the caller to send result sets, messages, and the results of executing commands back to the client.</summary>
		/// <returns>An instance of <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> if a pipe is available, or null if called in a context where pipe is not available (for example, in a user-defined function).</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x00004526 File Offset: 0x00002726
		public static SqlPipe Pipe
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the trigger context used to provide the caller with information about what caused the trigger to fire, and a map of the columns that were updated.</summary>
		/// <returns>An instance of <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> if a trigger context is available, or null if called outside of a trigger invocation.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x00004526 File Offset: 0x00002726
		public static SqlTriggerContext TriggerContext
		{
			get
			{
				return null;
			}
		}

		/// <summary>The Microsoft Windows identity of the caller.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> instance representing the Windows identity of the caller, or null if the client was authenticated using SQL Server Authentication. </returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x00004526 File Offset: 0x00002726
		public static WindowsIdentity WindowsIdentity
		{
			get
			{
				return null;
			}
		}
	}
}
