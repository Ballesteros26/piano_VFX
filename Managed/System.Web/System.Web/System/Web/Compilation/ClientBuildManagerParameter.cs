using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Contains values passed to the ASP.NET compiler during precompilation.</summary>
	// Token: 0x0200064B RID: 1611
	[Serializable]
	public class ClientBuildManagerParameter
	{
		/// <summary>Gets or sets the flags that determine precompilation behavior.</summary>
		/// <returns>The <see cref="T:System.Web.Compilation.PrecompilationFlags" /> for a client build.</returns>
		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x0600454A RID: 17738 RVA: 0x000BDBBE File Offset: 0x000BBDBE
		// (set) Token: 0x0600454B RID: 17739 RVA: 0x000BDBC6 File Offset: 0x000BBDC6
		public PrecompilationFlags PrecompilationFlags
		{
			get
			{
				return this.precompilationFlags;
			}
			set
			{
				this.precompilationFlags = value;
			}
		}

		/// <summary>Gets or sets the key container used during compilation.</summary>
		/// <returns>A <see cref="T:System.String" /> of the value for the key container.</returns>
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x0600454C RID: 17740 RVA: 0x000BDBCF File Offset: 0x000BBDCF
		// (set) Token: 0x0600454D RID: 17741 RVA: 0x000BDBD7 File Offset: 0x000BBDD7
		public string StrongNameKeyContainer
		{
			get
			{
				return this.strongNameKeyContainer;
			}
			set
			{
				this.strongNameKeyContainer = value;
			}
		}

		/// <summary>Gets or sets the key file used during compilation.</summary>
		/// <returns>A <see cref="T:System.String" /> of the value for the key file.</returns>
		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x0600454E RID: 17742 RVA: 0x000BDBE0 File Offset: 0x000BBDE0
		// (set) Token: 0x0600454F RID: 17743 RVA: 0x000BDBE8 File Offset: 0x000BBDE8
		public string StrongNameKeyFile
		{
			get
			{
				return this.strongNameKeyFile;
			}
			set
			{
				this.strongNameKeyFile = value;
			}
		}

		/// <summary>Gets or sets excluded virtual paths.</summary>
		/// <returns>The excluded virtual paths.</returns>
		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public List<string> ExcludedVirtualPaths
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x040024DB RID: 9435
		private PrecompilationFlags precompilationFlags;

		// Token: 0x040024DC RID: 9436
		private string strongNameKeyContainer;

		// Token: 0x040024DD RID: 9437
		private string strongNameKeyFile;
	}
}
