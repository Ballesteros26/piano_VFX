using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Contains forest trust account information about a top-level domain in a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</summary>
	// Token: 0x02000086 RID: 134
	public class TopLevelName
	{
		/// <summary>Gets the name of a top-level domain in a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelName" /> object.</returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the status of a top-level domain in a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameStatus" /> value that contains the status of the <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelName" /> object.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameStatus" /> values.</exception>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000208C File Offset: 0x0000028C
		public TopLevelNameStatus Status
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
