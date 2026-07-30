using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a discovery document. This class cannot be inherited.</summary>
	// Token: 0x020000A3 RID: 163
	[XmlRoot("discovery", Namespace = "http://schemas.xmlsoap.org/disco/")]
	public sealed class DiscoveryDocument
	{
		/// <summary>A list of references contained within the discovery document.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> containing the references within the discovery document.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00013AA4 File Offset: 0x00011CA4
		[XmlIgnore]
		public IList References
		{
			get
			{
				return this.references;
			}
		}

		/// <summary>Reads and returns a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> from the passed <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of a discovery document from the passed <see cref="T:System.IO.Stream" />.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000439 RID: 1081 RVA: 0x00013AAC File Offset: 0x00011CAC
		public static DiscoveryDocument Read(Stream stream)
		{
			return DiscoveryDocument.Read(new XmlTextReader(stream)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			});
		}

		/// <summary>Reads and returns a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> from the passed <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of a discovery document from the passed <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043A RID: 1082 RVA: 0x00013ACE File Offset: 0x00011CCE
		public static DiscoveryDocument Read(TextReader reader)
		{
			return DiscoveryDocument.Read(new XmlTextReader(reader)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			});
		}

		/// <summary>Reads and returns a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> from the passed <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of a discovery document from the passed <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> from which to read the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043B RID: 1083 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public static DiscoveryDocument Read(XmlReader xmlReader)
		{
			return (DiscoveryDocument)WebServicesSection.Current.DiscoveryDocumentSerializer.Deserialize(xmlReader);
		}

		/// <summary>Returns a value indicating whether the passed <see cref="T:System.Xml.XmlReader" /> can be deserialized into a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />.</summary>
		/// <returns>true if <see cref="T:System.Xml.XmlReader" /> can be deserialized into a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />; otherwise, false.</returns>
		/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that is tested whether it can be deserialized into a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043C RID: 1084 RVA: 0x00013B07 File Offset: 0x00011D07
		public static bool CanRead(XmlReader xmlReader)
		{
			return WebServicesSection.Current.DiscoveryDocumentSerializer.CanDeserialize(xmlReader);
		}

		/// <summary>Writes this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> into which this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043D RID: 1085 RVA: 0x00013B1C File Offset: 0x00011D1C
		public void Write(TextWriter writer)
		{
			this.Write(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			});
		}

		/// <summary>Writes this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043E RID: 1086 RVA: 0x00013B48 File Offset: 0x00011D48
		public void Write(Stream stream)
		{
			TextWriter textWriter = new StreamWriter(stream, new UTF8Encoding(false));
			this.Write(textWriter);
		}

		/// <summary>Writes this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> into which this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043F RID: 1087 RVA: 0x00013B6C File Offset: 0x00011D6C
		public void Write(XmlWriter writer)
		{
			XmlSerializer discoveryDocumentSerializer = WebServicesSection.Current.DiscoveryDocumentSerializer;
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			discoveryDocumentSerializer.Serialize(writer, this, xmlSerializerNamespaces);
		}

		/// <summary>Namespace of the discovery XML element of a discovery document.</summary>
		// Token: 0x0400032F RID: 815
		public const string Namespace = "http://schemas.xmlsoap.org/disco/";

		// Token: 0x04000330 RID: 816
		private ArrayList references = new ArrayList();
	}
}
