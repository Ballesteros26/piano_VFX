using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines the configuration file mappings for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005EE RID: 1518
	public sealed class WebConfigurationFileMap : ConfigurationFileMap
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class.</summary>
		// Token: 0x060041CF RID: 16847 RVA: 0x000ABE50 File Offset: 0x000AA050
		public WebConfigurationFileMap()
		{
			this.virtualDirectories = new VirtualDirectoryMappingCollection();
		}

		/// <summary>Gets the listed collection of virtual directories for a Web application.</summary>
		/// <returns>A collection of <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects.</returns>
		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x000ABE63 File Offset: 0x000AA063
		public VirtualDirectoryMappingCollection VirtualDirectories
		{
			get
			{
				return this.virtualDirectories;
			}
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class with the same value as the existing instance.</summary>
		/// <returns>A new instance of a <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class.</returns>
		// Token: 0x060041D1 RID: 16849 RVA: 0x000ABE6C File Offset: 0x000AA06C
		public override object Clone()
		{
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			webConfigurationFileMap.MachineConfigFilename = base.MachineConfigFilename;
			webConfigurationFileMap.virtualDirectories = new VirtualDirectoryMappingCollection();
			foreach (object obj in this.virtualDirectories)
			{
				VirtualDirectoryMapping virtualDirectoryMapping = (VirtualDirectoryMapping)obj;
				VirtualDirectoryMapping virtualDirectoryMapping2 = new VirtualDirectoryMapping(virtualDirectoryMapping.PhysicalDirectory, virtualDirectoryMapping.IsAppRoot, virtualDirectoryMapping.ConfigFileBaseName);
				webConfigurationFileMap.virtualDirectories.Add(virtualDirectoryMapping.VirtualDirectory, virtualDirectoryMapping2);
			}
			return webConfigurationFileMap;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class by using the specified machine configuration file name.</summary>
		/// <param name="machineConfigFileName">The machine configuration file name with the complete physical path (for example, c:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\machine.config).</param>
		// Token: 0x060041D2 RID: 16850 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebConfigurationFileMap(string machineConfigFileName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400234B RID: 9035
		private VirtualDirectoryMappingCollection virtualDirectories;
	}
}
