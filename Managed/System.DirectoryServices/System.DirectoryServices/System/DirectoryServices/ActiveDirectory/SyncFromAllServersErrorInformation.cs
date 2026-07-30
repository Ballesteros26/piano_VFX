using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Contains information about a <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</summary>
	// Token: 0x02000081 RID: 129
	public class SyncFromAllServersErrorInformation
	{
		/// <summary>Gets the category of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersErrorCategory" /> enumeration value that indicates the category of the error.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000208C File Offset: 0x0000028C
		public SyncFromAllServersErrorCategory ErrorCategory
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the error code for the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</summary>
		/// <returns>A value that identifies the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception error.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000208C File Offset: 0x0000028C
		public int ErrorCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the message that describes the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</summary>
		/// <returns>A message that describes the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000208C File Offset: 0x0000028C
		public string ErrorMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the target server for which the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception occurred.</summary>
		/// <returns>The name of the target server that is involved in the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000208C File Offset: 0x0000028C
		public string TargetServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the source server for which the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception occurred.</summary>
		/// <returns>The name of the source server that is involved in the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000208C File Offset: 0x0000028C
		public string SourceServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
