using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Util;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines the configuration settings that control the key generation and algorithms that are used in encryption, decryption, and message authentication code (MAC) operations in Windows Forms authentication, view-state validation, and session-state application isolation. This class cannot be inherited.</summary>
	// Token: 0x020005B9 RID: 1465
	public sealed class MachineKeySection : ConfigurationSection
	{
		// Token: 0x06003EC0 RID: 16064 RVA: 0x000A6384 File Offset: 0x000A4584
		static MachineKeySection()
		{
			MachineKeySection.properties.Add(MachineKeySection.decryptionProp);
			MachineKeySection.properties.Add(MachineKeySection.decryptionKeyProp);
			MachineKeySection.properties.Add(MachineKeySection.validationProp);
			MachineKeySection.properties.Add(MachineKeySection.validationKeyProp);
			MachineKeySection.Config.AutoGenerate(MachineKeyRegistryStorage.KeyType.Encryption);
			MachineKeySection.Config.AutoGenerate(MachineKeyRegistryStorage.KeyType.Validation);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.MachineKeySection" /> class by using default settings.</summary>
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000A649D File Offset: 0x000A469D
		public MachineKeySection()
		{
			this.validation = (MachineKeyValidation)MachineKeySection.converter.ConvertFrom(null, null, this.ValidationAlgorithm);
		}

		/// <summary>Gets or sets a value that specifies whether upgraded encryption methods for view state that were introduced after the .NET Framework version 2.0 Service Pack 1 release are used.</summary>
		/// <returns>A value that indicates whether encryption methods that were introduced after the .NET Framework 2.0 SP1 release are used. </returns>
		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000A64C2 File Offset: 0x000A46C2
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x000A64CA File Offset: 0x000A46CA
		[global::System.MonoTODO]
		public MachineKeyCompatibilityMode CompatibilityMode { get; set; }

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000A64D3 File Offset: 0x000A46D3
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			this.decryption_key = null;
			this.validation_key = null;
			this.decryption_template = null;
			this.validation_template = null;
		}

		/// <summary>Specifies the encryption algorithm that is used for encrypting and decrypting forms authentication data. </summary>
		/// <returns>A value that indicates the algorithm that is used to encrypt and decrypt forms authentication data. (For information about how to specify the algorithm that is used when view state is encrypted, see the <see cref="P:System.Web.Configuration.MachineKeySection.Validation" /> property.) Auto is the default value.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is not one of the decryption values.</exception>
		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000A64F8 File Offset: 0x000A46F8
		// (set) Token: 0x06003EC6 RID: 16070 RVA: 0x000A650A File Offset: 0x000A470A
		[ConfigurationProperty("decryption", DefaultValue = "Auto")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		public string Decryption
		{
			get
			{
				return (string)base[MachineKeySection.decryptionProp];
			}
			set
			{
				this.decryption_template = MachineKeySectionUtils.GetDecryptionAlgorithm(value);
				base[MachineKeySection.decryptionProp] = value;
			}
		}

		/// <summary>Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated. </summary>
		/// <returns>A key value, or a value that indicates how the key is generated. The default is "AutoGenerate,IsolateApps".</returns>
		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x000A6524 File Offset: 0x000A4724
		// (set) Token: 0x06003EC8 RID: 16072 RVA: 0x000A6536 File Offset: 0x000A4736
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("decryptionKey", DefaultValue = "AutoGenerate,IsolateApps")]
		public string DecryptionKey
		{
			get
			{
				return (string)base[MachineKeySection.decryptionKeyProp];
			}
			set
			{
				base[MachineKeySection.decryptionKeyProp] = value;
				this.SetDecryptionKey(value);
			}
		}

		/// <summary>Specifies the hashing algorithm that is used for validating forms authentication and view state data. </summary>
		/// <returns>A value that indicates the hashing algorithm that is used to validate forms authentication and view state data.</returns>
		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000A654B File Offset: 0x000A474B
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000A6554 File Offset: 0x000A4754
		public MachineKeyValidation Validation
		{
			get
			{
				return this.validation;
			}
			set
			{
				if (value == MachineKeyValidation.Custom)
				{
					throw new ArgumentException();
				}
				string text = value.ToString();
				this.ValidationAlgorithm = ((text == "TripleDES") ? "3DES" : text);
			}
		}

		/// <summary>Gets or sets the name of a custom algorithm that is used to validate forms authentication and view state data.</summary>
		/// <returns>A string that contains the name of a predefined algorithm or the name of a custom algorithm. </returns>
		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000A6594 File Offset: 0x000A4794
		// (set) Token: 0x06003ECC RID: 16076 RVA: 0x000A65A6 File Offset: 0x000A47A6
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("validation", DefaultValue = "HMACSHA256")]
		public string ValidationAlgorithm
		{
			get
			{
				return (string)base[MachineKeySection.validationProp];
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (value.StartsWith("alg:"))
				{
					this.validation = MachineKeyValidation.Custom;
				}
				else
				{
					this.validation = (MachineKeyValidation)MachineKeySection.converter.ConvertFrom(null, null, value);
				}
				base[MachineKeySection.validationProp] = value;
			}
		}

		/// <summary>Gets or sets the key that is used to validate forms authentication and view state data, or the process by which the key is generated. </summary>
		/// <returns>A key value, or a value that indicates how the key is generated. The default is "AutoGenerate,IsolateApps".</returns>
		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000A65E6 File Offset: 0x000A47E6
		// (set) Token: 0x06003ECE RID: 16078 RVA: 0x000A65F8 File Offset: 0x000A47F8
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("validationKey", DefaultValue = "AutoGenerate,IsolateApps")]
		public string ValidationKey
		{
			get
			{
				return (string)base[MachineKeySection.validationKeyProp];
			}
			set
			{
				base[MachineKeySection.validationKeyProp] = value;
				this.SetValidationKey(value);
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000A660D File Offset: 0x000A480D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MachineKeySection.properties;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000A6614 File Offset: 0x000A4814
		internal static MachineKeySection Config
		{
			get
			{
				return WebConfigurationManager.GetSection("system.web/machineKey") as MachineKeySection;
			}
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000A6625 File Offset: 0x000A4825
		internal SymmetricAlgorithm GetDecryptionAlgorithm()
		{
			return MachineKeySectionUtils.GetDecryptionAlgorithm(this.Decryption);
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000A6632 File Offset: 0x000A4832
		private SymmetricAlgorithm DecryptionTemplate
		{
			get
			{
				if (this.decryption_template == null)
				{
					this.decryption_template = this.GetDecryptionAlgorithm();
				}
				return this.decryption_template;
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000A664E File Offset: 0x000A484E
		internal byte[] GetDecryptionKey()
		{
			if (this.decryption_key == null)
			{
				this.SetDecryptionKey(this.DecryptionKey);
			}
			return this.decryption_key;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000A666C File Offset: 0x000A486C
		private void SetDecryptionKey(string key)
		{
			if (key == null || key.StartsWith("AutoGenerate"))
			{
				this.decryption_key = this.AutoGenerate(MachineKeyRegistryStorage.KeyType.Encryption);
				return;
			}
			try
			{
				this.decryption_key = MachineKeySectionUtils.GetBytes(key, key.Length);
				this.DecryptionTemplate.Key = this.decryption_key;
			}
			catch
			{
				this.decryption_key = null;
				throw new ArgumentException("Invalid key length");
			}
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000A66E0 File Offset: 0x000A48E0
		internal KeyedHashAlgorithm GetValidationAlgorithm()
		{
			return MachineKeySectionUtils.GetValidationAlgorithm(this);
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x000A66E8 File Offset: 0x000A48E8
		private KeyedHashAlgorithm ValidationTemplate
		{
			get
			{
				if (this.validation_template == null)
				{
					this.validation_template = this.GetValidationAlgorithm();
				}
				return this.validation_template;
			}
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000A6704 File Offset: 0x000A4904
		internal byte[] GetValidationKey()
		{
			if (this.validation_key == null)
			{
				this.SetValidationKey(this.ValidationKey);
			}
			return this.validation_key;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000A6720 File Offset: 0x000A4920
		private void SetValidationKey(string key)
		{
			if (key == null || key.StartsWith("AutoGenerate"))
			{
				this.validation_key = this.AutoGenerate(MachineKeyRegistryStorage.KeyType.Validation);
				return;
			}
			try
			{
				this.validation_key = MachineKeySectionUtils.GetBytes(key, key.Length);
				this.ValidationTemplate.Key = this.validation_key;
			}
			catch (CryptographicException)
			{
				try
				{
					byte[] array = new byte[this.ValidationTemplate.Key.Length];
					Array.Copy(this.validation_key, 0, array, 0, this.validation_key.Length);
					this.ValidationTemplate.Key = array;
					this.validation_key = array;
				}
				catch
				{
					this.validation_key = null;
					throw new ArgumentException("Invalid key length");
				}
			}
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000A67E4 File Offset: 0x000A49E4
		private byte[] AutoGenerate(MachineKeyRegistryStorage.KeyType type)
		{
			byte[] array = null;
			try
			{
				array = MachineKeyRegistryStorage.Retrieve(type);
				if (type == MachineKeyRegistryStorage.KeyType.Encryption)
				{
					this.DecryptionTemplate.Key = array;
				}
				else if (type == MachineKeyRegistryStorage.KeyType.Validation)
				{
					this.ValidationTemplate.Key = array;
				}
			}
			catch (Exception)
			{
				array = null;
			}
			if (array == null)
			{
				if (type == MachineKeyRegistryStorage.KeyType.Encryption)
				{
					array = this.DecryptionTemplate.Key;
				}
				else if (type == MachineKeyRegistryStorage.KeyType.Validation)
				{
					array = this.ValidationTemplate.Key;
				}
				MachineKeyRegistryStorage.Store(array, type);
			}
			return array;
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ApplicationName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the data protector type. The default is <see cref="F:System.String.Empty" />.</summary>
		/// <returns>The name of the data protector type.</returns>
		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string DataProtectorType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002248 RID: 8776
		private static ConfigurationProperty decryptionProp = new ConfigurationProperty("decryption", typeof(string), "Auto", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002249 RID: 8777
		private static ConfigurationProperty decryptionKeyProp = new ConfigurationProperty("decryptionKey", typeof(string), "AutoGenerate,IsolateApps", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400224A RID: 8778
		private static ConfigurationProperty validationProp = new ConfigurationProperty("validation", typeof(string), "HMACSHA256", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400224B RID: 8779
		private static ConfigurationProperty validationKeyProp = new ConfigurationProperty("validationKey", typeof(string), "AutoGenerate,IsolateApps", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400224C RID: 8780
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400224D RID: 8781
		private static MachineKeyValidationConverter converter = new MachineKeyValidationConverter();

		// Token: 0x0400224E RID: 8782
		private MachineKeyValidation validation;

		// Token: 0x04002250 RID: 8784
		private byte[] decryption_key;

		// Token: 0x04002251 RID: 8785
		private byte[] validation_key;

		// Token: 0x04002252 RID: 8786
		private SymmetricAlgorithm decryption_template;

		// Token: 0x04002253 RID: 8787
		private KeyedHashAlgorithm validation_template;
	}
}
