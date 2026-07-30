using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents an XML document that specifies a list of file system directory paths that should not be searched for references to add to a Web services discovery document.</summary>
	// Token: 0x020000B0 RID: 176
	[XmlRoot("dynamicDiscovery", Namespace = "urn:schemas-dynamicdiscovery:disco.2000-03-17")]
	public sealed class DynamicDiscoveryDocument
	{
		/// <summary>Gets or sets the file-system directory paths that should not be searched for references to add to a discovery document.</summary>
		/// <returns>An array of <see cref="T:System.Web.Services.Discovery.ExcludePathInfo" /> objects.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000159B0 File Offset: 0x00013BB0
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000159B8 File Offset: 0x00013BB8
		[XmlElement("exclude", typeof(ExcludePathInfo))]
		public ExcludePathInfo[] ExcludePaths
		{
			get
			{
				return this.excludePaths;
			}
			set
			{
				if (value == null)
				{
					value = new ExcludePathInfo[0];
				}
				this.excludePaths = value;
			}
		}

		/// <summary>Serializes a <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> instance into an XML document specified as an output stream.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object to which the XML dynamic discovery document is serialized.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004A2 RID: 1186 RVA: 0x000159CC File Offset: 0x00013BCC
		public void Write(Stream stream)
		{
			new XmlSerializer(typeof(DynamicDiscoveryDocument)).Serialize(new StreamWriter(stream, new UTF8Encoding(false)), this);
		}

		/// <summary>Deserializes an XML document into a <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> instance.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> that was loaded.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object from which the XML dynamic discovery document is deserialized.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004A3 RID: 1187 RVA: 0x000159EF File Offset: 0x00013BEF
		public static DynamicDiscoveryDocument Load(Stream stream)
		{
			return (DynamicDiscoveryDocument)new XmlSerializer(typeof(DynamicDiscoveryDocument)).Deserialize(stream);
		}

		// Token: 0x04000350 RID: 848
		private ExcludePathInfo[] excludePaths = new ExcludePathInfo[0];

		/// <summary>Contains the dynamic discovery document namespace "urn:schemas-dynamicdiscovery:disco.2000-03-17". This field is constant.</summary>
		// Token: 0x04000351 RID: 849
		public const string Namespace = "urn:schemas-dynamicdiscovery:disco.2000-03-17";
	}
}
