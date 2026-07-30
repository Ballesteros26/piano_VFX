using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Wraps the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> class, it to be placed as a subelement of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
	// Token: 0x02000064 RID: 100
	public class KeyInfoEncryptedKey : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class. </summary>
		// Token: 0x0600028A RID: 650 RVA: 0x00009C09 File Offset: 0x00007E09
		public KeyInfoEncryptedKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class using an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object.</summary>
		/// <param name="encryptedKey">An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" />  object that encapsulates an encrypted key.</param>
		// Token: 0x0600028B RID: 651 RVA: 0x00009C11 File Offset: 0x00007E11
		public KeyInfoEncryptedKey(EncryptedKey encryptedKey)
		{
			this._encryptedKey = encryptedKey;
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.KeyInfoEncryptedKey.EncryptedKey" /> property is null.</exception>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009C20 File Offset: 0x00007E20
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00009C28 File Offset: 0x00007E28
		public EncryptedKey EncryptedKey
		{
			get
			{
				return this._encryptedKey;
			}
			set
			{
				this._encryptedKey = value;
			}
		}

		/// <summary>Returns an XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</summary>
		/// <returns>An XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encrypted key is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600028E RID: 654 RVA: 0x00009C31 File Offset: 0x00007E31
		public override XmlElement GetXml()
		{
			if (this._encryptedKey == null)
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfoEncryptedKey");
			}
			return this._encryptedKey.GetXml();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009C56 File Offset: 0x00007E56
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			if (this._encryptedKey == null)
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfoEncryptedKey");
			}
			return this._encryptedKey.GetXml(xmlDocument);
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</param>
		// Token: 0x06000290 RID: 656 RVA: 0x00009C7C File Offset: 0x00007E7C
		public override void LoadXml(XmlElement value)
		{
			this._encryptedKey = new EncryptedKey();
			this._encryptedKey.LoadXml(value);
		}

		// Token: 0x0400016E RID: 366
		private EncryptedKey _encryptedKey;
	}
}
