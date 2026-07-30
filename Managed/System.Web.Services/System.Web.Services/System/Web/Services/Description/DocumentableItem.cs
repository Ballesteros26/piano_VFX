using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents the abstract base class from which several classes in the <see cref="N:System.Web.Services.Description" /> namespace are derived.</summary>
	// Token: 0x020000E9 RID: 233
	public abstract class DocumentableItem
	{
		/// <summary>Gets or sets the text documentation for the instance of the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</summary>
		/// <returns>A string that represents the documentation for the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001C29C File Offset: 0x0001A49C
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		[XmlIgnore]
		public string Documentation
		{
			get
			{
				if (this.documentation != null)
				{
					return this.documentation;
				}
				if (this.documentationElement == null)
				{
					return string.Empty;
				}
				return this.documentationElement.InnerXml;
			}
			set
			{
				this.documentation = value;
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.WriteElementString("wsdl", "documentation", "http://schemas.xmlsoap.org/wsdl/", value);
				this.Parent.LoadXml(stringWriter.ToString());
				this.documentationElement = this.parent.DocumentElement;
				xmlTextWriter.Close();
			}
		}

		/// <summary>Gets or sets the documentation element for the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlElement" /> that represents the documentation for the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001C32A File Offset: 0x0001A52A
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001C332 File Offset: 0x0001A532
		[ComVisible(false)]
		[XmlAnyElement("documentation", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
		public XmlElement DocumentationElement
		{
			get
			{
				return this.documentationElement;
			}
			set
			{
				this.documentationElement = value;
				this.documentation = null;
			}
		}

		/// <summary>Gets or sets an array of type <see cref="T:System.Xml.XmlAttribute" /> that represents attribute extensions of WSDL to comply with Web Services Interoperability (WS-I) Basic Profile 1.1.</summary>
		/// <returns>An array of type <see cref="T:System.Xml.XmlAttribute" /> that represents attribute extensions of WSDL to comply with Web Services Interoperability (WS-I) Basic Profile 1.1.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001C342 File Offset: 0x0001A542
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001C34A File Offset: 0x0001A54A
		[XmlAnyAttribute]
		public XmlAttribute[] ExtensibleAttributes
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		/// <summary>Gets or sets the dictionary of namespace prefixes and namespaces used to preserve namespace prefixes and namespaces when a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object is constructed.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> dictionary containing prefix/namespace pairs.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001C353 File Offset: 0x0001A553
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001C36E File Offset: 0x0001A56E
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlSerializerNamespaces();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.DocumentableItem" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.DocumentableItem" />.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000652 RID: 1618
		[XmlIgnore]
		public abstract ServiceDescriptionFormatExtensionCollection Extensions { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001C377 File Offset: 0x0001A577
		internal XmlDocument Parent
		{
			get
			{
				if (this.parent == null)
				{
					this.parent = new XmlDocument();
				}
				return this.parent;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001C394 File Offset: 0x0001A594
		internal XmlElement GetDocumentationElement()
		{
			if (this.documentationElement == null)
			{
				this.documentationElement = this.Parent.CreateElement("wsdl", "documentation", "http://schemas.xmlsoap.org/wsdl/");
				this.Parent.InsertBefore(this.documentationElement, null);
			}
			return this.documentationElement;
		}

		// Token: 0x040003E3 RID: 995
		private XmlDocument parent;

		// Token: 0x040003E4 RID: 996
		private string documentation;

		// Token: 0x040003E5 RID: 997
		private XmlElement documentationElement;

		// Token: 0x040003E6 RID: 998
		private XmlAttribute[] anyAttribute;

		// Token: 0x040003E7 RID: 999
		private XmlSerializerNamespaces namespaces;
	}
}
