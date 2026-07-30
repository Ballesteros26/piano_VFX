using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Xml;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for a <see cref="T:System.Security.PermissionSet" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005A2 RID: 1442
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PermissionSetAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the enumeration values that specifies a security action. </param>
		// Token: 0x0600403F RID: 16447 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public PermissionSetAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a file containing the XML representation of a custom permission set to be declared.</summary>
		/// <returns>The physical path to the file containing the XML representation of the permission set.</returns>
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x000E4D6C File Offset: 0x000E2F6C
		// (set) Token: 0x06004041 RID: 16449 RVA: 0x000E4D74 File Offset: 0x000E2F74
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		/// <summary>Gets or sets the hexadecimal representation of the XML encoded permission set.</summary>
		/// <returns>The hexadecimal representation of the XML encoded permission set.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x000E4D7D File Offset: 0x000E2F7D
		// (set) Token: 0x06004043 RID: 16451 RVA: 0x000E4D85 File Offset: 0x000E2F85
		public string Hex
		{
			get
			{
				return this.hex;
			}
			set
			{
				this.hex = value;
			}
		}

		/// <summary>Gets or sets the name of the permission set.</summary>
		/// <returns>The name of an immutable <see cref="T:System.Security.NamedPermissionSet" /> (one of several permission sets that are contained in the default policy and cannot be altered).</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x000E4D8E File Offset: 0x000E2F8E
		// (set) Token: 0x06004045 RID: 16453 RVA: 0x000E4D96 File Offset: 0x000E2F96
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the file specified by <see cref="P:System.Security.Permissions.PermissionSetAttribute.File" /> is Unicode or ASCII encoded.</summary>
		/// <returns>true if the file is Unicode encoded; otherwise, false.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x000E4D9F File Offset: 0x000E2F9F
		// (set) Token: 0x06004047 RID: 16455 RVA: 0x000E4DA7 File Offset: 0x000E2FA7
		public bool UnicodeEncoded
		{
			get
			{
				return this.isUnicodeEncoded;
			}
			set
			{
				this.isUnicodeEncoded = value;
			}
		}

		/// <summary>Gets or sets the XML representation of a permission set.</summary>
		/// <returns>The XML representation of a permission set.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x000E4DB0 File Offset: 0x000E2FB0
		// (set) Token: 0x06004049 RID: 16457 RVA: 0x000E4DB8 File Offset: 0x000E2FB8
		public string XML
		{
			get
			{
				return this.xml;
			}
			set
			{
				this.xml = value;
			}
		}

		/// <summary>This method is not used.</summary>
		/// <returns>A null reference (nothing in Visual Basic) in all cases.</returns>
		// Token: 0x0600404A RID: 16458 RVA: 0x0000A42E File Offset: 0x0000862E
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000E4DC4 File Offset: 0x000E2FC4
		private PermissionSet CreateFromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			try
			{
				securityParser.LoadXml(xml);
			}
			catch (SmallXmlParserException ex)
			{
				throw new XmlSyntaxException(ex.Line, ex.ToString());
			}
			SecurityElement securityElement = securityParser.ToXml();
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				return null;
			}
			PermissionState permissionState = PermissionState.None;
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				permissionState = PermissionState.Unrestricted;
			}
			if (text.EndsWith("NamedPermissionSet"))
			{
				NamedPermissionSet namedPermissionSet = new NamedPermissionSet(securityElement.Attribute("Name"), permissionState);
				namedPermissionSet.FromXml(securityElement);
				return namedPermissionSet;
			}
			if (text.EndsWith("PermissionSet"))
			{
				PermissionSet permissionSet = new PermissionSet(permissionState);
				permissionSet.FromXml(securityElement);
				return permissionSet;
			}
			return null;
		}

		/// <summary>Creates and returns a new permission set based on this permission set attribute object.</summary>
		/// <returns>A new permission set.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600404C RID: 16460 RVA: 0x000E4E6C File Offset: 0x000E306C
		public PermissionSet CreatePermissionSet()
		{
			PermissionSet permissionSet = null;
			if (base.Unrestricted)
			{
				permissionSet = new PermissionSet(PermissionState.Unrestricted);
			}
			else
			{
				permissionSet = new PermissionSet(PermissionState.None);
				if (this.name != null)
				{
					return PolicyLevel.CreateAppDomainLevel().GetNamedPermissionSet(this.name);
				}
				if (this.file != null)
				{
					Encoding encoding = (this.isUnicodeEncoded ? Encoding.Unicode : Encoding.ASCII);
					using (StreamReader streamReader = new StreamReader(this.file, encoding))
					{
						return this.CreateFromXml(streamReader.ReadToEnd());
					}
				}
				if (this.xml != null)
				{
					permissionSet = this.CreateFromXml(this.xml);
				}
				else if (this.hex != null)
				{
					Encoding ascii = Encoding.ASCII;
					byte[] array = CryptoConvert.FromHex(this.hex);
					permissionSet = this.CreateFromXml(ascii.GetString(array, 0, array.Length));
				}
			}
			return permissionSet;
		}

		// Token: 0x04002098 RID: 8344
		private string file;

		// Token: 0x04002099 RID: 8345
		private string name;

		// Token: 0x0400209A RID: 8346
		private bool isUnicodeEncoded;

		// Token: 0x0400209B RID: 8347
		private string xml;

		// Token: 0x0400209C RID: 8348
		private string hex;
	}
}
