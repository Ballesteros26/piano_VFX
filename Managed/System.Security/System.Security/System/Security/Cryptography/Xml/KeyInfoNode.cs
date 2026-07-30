using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Handles <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> subelements that do not have specific implementations or handlers registered on the machine.</summary>
	// Token: 0x02000066 RID: 102
	public class KeyInfoNode : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> class.</summary>
		// Token: 0x06000298 RID: 664 RVA: 0x00009C09 File Offset: 0x00007E09
		public KeyInfoNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> class with content taken from the specified <see cref="T:System.Xml.XmlElement" />.</summary>
		/// <param name="node">An XML element from which to take the content used to create the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />. </param>
		// Token: 0x06000299 RID: 665 RVA: 0x00009D36 File Offset: 0x00007F36
		public KeyInfoNode(XmlElement node)
		{
			this._node = node;
		}

		/// <summary>Gets or sets the XML content of the current <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</summary>
		/// <returns>The XML content of the current <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009D45 File Offset: 0x00007F45
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00009D4D File Offset: 0x00007F4D
		public XmlElement Value
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600029C RID: 668 RVA: 0x00009D58 File Offset: 0x00007F58
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009D79 File Offset: 0x00007F79
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			return xmlDocument.ImportNode(this._node, true) as XmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" /> to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoNode" />. </param>
		// Token: 0x0600029E RID: 670 RVA: 0x00009D4D File Offset: 0x00007F4D
		public override void LoadXml(XmlElement value)
		{
			this._node = value;
		}

		// Token: 0x04000170 RID: 368
		private XmlElement _node;
	}
}
