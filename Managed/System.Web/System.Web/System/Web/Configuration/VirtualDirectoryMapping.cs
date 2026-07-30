using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies a custom virtual-directory hierarchy for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005EC RID: 1516
	public sealed class VirtualDirectoryMapping
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> class based on supplied parameters.</summary>
		/// <param name="physicalDirectory">A string value that specifies the absolute path to a physical directory.</param>
		/// <param name="isAppRoot">A Boolean value that indicates whether the virtual directory is the application root of the Web application.</param>
		// Token: 0x060041B9 RID: 16825 RVA: 0x000ABDA3 File Offset: 0x000A9FA3
		public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot)
		{
			this.physicalDirectory = physicalDirectory;
			this.isAppRoot = isAppRoot;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> class based on supplied parameters.</summary>
		/// <param name="physicalDirectory">A string value that specifies the absolute path to a physical directory.</param>
		/// <param name="isAppRoot">A Boolean value that indicates whether the virtual directory is the application root of the Web application.</param>
		/// <param name="configFileBaseName">The name of the configuration file.</param>
		// Token: 0x060041BA RID: 16826 RVA: 0x000ABDB9 File Offset: 0x000A9FB9
		public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot, string configFileBaseName)
		{
			this.physicalDirectory = physicalDirectory;
			this.isAppRoot = isAppRoot;
			this.configFileBaseName = configFileBaseName;
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000ABDD6 File Offset: 0x000A9FD6
		internal void SetVirtualDirectory(string dir)
		{
			this.virtualDirectory = dir;
		}

		/// <summary>Gets or sets the name of the configuration file.</summary>
		/// <returns>A value that indicates the name of the configuration file.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is null or an empty string ("").</exception>
		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x000ABDDF File Offset: 0x000A9FDF
		// (set) Token: 0x060041BD RID: 16829 RVA: 0x000ABDE7 File Offset: 0x000A9FE7
		[global::System.MonoTODO("Do something with this")]
		public string ConfigFileBaseName
		{
			get
			{
				return this.configFileBaseName;
			}
			set
			{
				this.configFileBaseName = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the virtual directory should be treated as the application root.</summary>
		/// <returns>A value that indicates whether the virtual directory should be treated as the application root.</returns>
		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000ABDF0 File Offset: 0x000A9FF0
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x000ABDF8 File Offset: 0x000A9FF8
		[global::System.MonoTODO("Do something with this")]
		public bool IsAppRoot
		{
			get
			{
				return this.isAppRoot;
			}
			set
			{
				this.isAppRoot = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the full server path of a Web application.</summary>
		/// <returns>A value that indicates the full server path of a Web application.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is invalid or fails internal security validation.</exception>
		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x000ABE01 File Offset: 0x000AA001
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x000ABE09 File Offset: 0x000AA009
		public string PhysicalDirectory
		{
			get
			{
				return this.physicalDirectory;
			}
			set
			{
				this.physicalDirectory = value;
			}
		}

		/// <summary>Gets a value that specifies the virtual directory relative to the root of the Web server.</summary>
		/// <returns>A value that indicates the relative Web-application directory.</returns>
		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x000ABE12 File Offset: 0x000AA012
		public string VirtualDirectory
		{
			get
			{
				return this.virtualDirectory;
			}
		}

		// Token: 0x04002347 RID: 9031
		private string physicalDirectory;

		// Token: 0x04002348 RID: 9032
		private bool isAppRoot;

		// Token: 0x04002349 RID: 9033
		private string configFileBaseName;

		// Token: 0x0400234A RID: 9034
		private string virtualDirectory;
	}
}
