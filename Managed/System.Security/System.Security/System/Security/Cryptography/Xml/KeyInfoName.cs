using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents a &lt;KeyName&gt; subelement of an XMLDSIG or XML Encryption &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000065 RID: 101
	public class KeyInfoName : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class.</summary>
		// Token: 0x06000291 RID: 657 RVA: 0x00009C95 File Offset: 0x00007E95
		public KeyInfoName()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class by specifying the string identifier that is the value of the &lt;KeyName&gt; element.</summary>
		/// <param name="keyName">The string identifier that is the value of the &lt;KeyName&gt; element.</param>
		// Token: 0x06000292 RID: 658 RVA: 0x00009C9E File Offset: 0x00007E9E
		public KeyInfoName(string keyName)
		{
			this.Value = keyName;
		}

		/// <summary>Gets or sets the string identifier contained within a &lt;KeyName&gt; element.</summary>
		/// <returns>The string identifier that is the value of the &lt;KeyName&gt; element.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009CB5 File Offset: 0x00007EB5
		public string Value
		{
			get
			{
				return this._keyName;
			}
			set
			{
				this._keyName = value;
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000295 RID: 661 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009CE1 File Offset: 0x00007EE1
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyName", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.AppendChild(xmlDocument.CreateTextNode(this._keyName));
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06000297 RID: 663 RVA: 0x00009D08 File Offset: 0x00007F08
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._keyName = value.InnerText.Trim();
		}

		// Token: 0x0400016F RID: 367
		private string _keyName;
	}
}
