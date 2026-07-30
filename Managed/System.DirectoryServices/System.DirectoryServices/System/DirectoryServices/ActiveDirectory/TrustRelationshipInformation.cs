using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> class contains information for a trust relationship between a pair of <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects.</summary>
	// Token: 0x0200008B RID: 139
	public class TrustRelationshipInformation
	{
		/// <summary>Obtains the name of the source <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</summary>
		/// <returns>A string that contains the name of the source <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000208C File Offset: 0x0000028C
		public string SourceName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Obtains the name of the target <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</summary>
		/// <returns>A string that contains the name of the target <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects for this trust relationship.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000208C File Offset: 0x0000028C
		public string TargetName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Obtains the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustType" /> object of the trust relationship.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustType" /> value that represents the type of the trust relationship.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000208C File Offset: 0x0000028C
		public TrustType TrustType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Obtains the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> objects for this trust relationship relative to the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects that created the trust.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value for this trust relationship relative to the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects that created the trust.</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000208C File Offset: 0x0000028C
		public TrustDirection TrustDirection
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
