using System;

namespace System.Configuration
{
	/// <summary>Used internally as the class that represents metadata about an individual configuration property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000195 RID: 405
	public class SettingsProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class, based on the supplied parameter.</summary>
		/// <param name="propertyToCopy">Specifies a copy of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000BFC RID: 3068 RVA: 0x0003C8EC File Offset: 0x0003AAEC
		public SettingsProperty(SettingsProperty propertyToCopy)
			: this(propertyToCopy.Name, propertyToCopy.PropertyType, propertyToCopy.Provider, propertyToCopy.IsReadOnly, propertyToCopy.DefaultValue, propertyToCopy.SerializeAs, new SettingsAttributeDictionary(propertyToCopy.Attributes), propertyToCopy.ThrowOnErrorDeserializing, propertyToCopy.ThrowOnErrorSerializing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class. based on the supplied parameter.</summary>
		/// <param name="name">Specifies the name of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000BFD RID: 3069 RVA: 0x0003C93C File Offset: 0x0003AB3C
		public SettingsProperty(string name)
			: this(name, null, null, false, null, SettingsSerializeAs.String, new SettingsAttributeDictionary(), false, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class based on the supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="propertyType">The type of <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object to use for persistence.</param>
		/// <param name="isReadOnly">A <see cref="T:System.Boolean" /> value specifying whether the <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</param>
		/// <param name="defaultValue">The default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used to set the serialization scheme for storing application settings.</param>
		/// <param name="attributes">A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</param>
		/// <param name="throwOnErrorDeserializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</param>
		/// <param name="throwOnErrorSerializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully serialized.</param>
		// Token: 0x06000BFE RID: 3070 RVA: 0x0003C95C File Offset: 0x0003AB5C
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this.name = name;
			this.propertyType = propertyType;
			this.provider = provider;
			this.isReadOnly = isReadOnly;
			this.defaultValue = defaultValue;
			this.serializeAs = serializeAs;
			this.attributes = attributes;
			this.throwOnErrorDeserializing = throwOnErrorDeserializing;
			this.throwOnErrorSerializing = throwOnErrorSerializing;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object containing the attributes of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0003C9B4 File Offset: 0x0003ABB4
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>An object containing the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x0003C9C4 File Offset: 0x0003ABC4
		public virtual object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether a <see cref="T:System.Configuration.SettingsProperty" /> object is read-only. </summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingsProperty" /> is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0003C9CD File Offset: 0x0003ABCD
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0003C9D5 File Offset: 0x0003ABD5
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0003C9DE File Offset: 0x0003ABDE
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x0003C9E6 File Offset: 0x0003ABE6
		public virtual string Name
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

		/// <summary>Gets or sets the type for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The type for the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0003C9EF File Offset: 0x0003ABEF
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0003C9F7 File Offset: 0x0003ABF7
		public virtual Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
			set
			{
				this.propertyType = value;
			}
		}

		/// <summary>Gets or sets the provider for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProvider" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0003CA00 File Offset: 0x0003AC00
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0003CA08 File Offset: 0x0003AC08
		public virtual SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Configuration.SettingsSerializeAs" /> object for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003CA11 File Offset: 0x0003AC11
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0003CA19 File Offset: 0x0003AC19
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
			set
			{
				this.serializeAs = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully deserialized; otherwise, false.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0003CA22 File Offset: 0x0003AC22
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0003CA2A File Offset: 0x0003AC2A
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this.throwOnErrorDeserializing;
			}
			set
			{
				this.throwOnErrorDeserializing = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully serialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully serialized; otherwise, false.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0003CA33 File Offset: 0x0003AC33
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0003CA3B File Offset: 0x0003AC3B
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this.throwOnErrorSerializing;
			}
			set
			{
				this.throwOnErrorSerializing = value;
			}
		}

		// Token: 0x04000FE6 RID: 4070
		private string name;

		// Token: 0x04000FE7 RID: 4071
		private Type propertyType;

		// Token: 0x04000FE8 RID: 4072
		private SettingsProvider provider;

		// Token: 0x04000FE9 RID: 4073
		private bool isReadOnly;

		// Token: 0x04000FEA RID: 4074
		private object defaultValue;

		// Token: 0x04000FEB RID: 4075
		private SettingsSerializeAs serializeAs;

		// Token: 0x04000FEC RID: 4076
		private SettingsAttributeDictionary attributes;

		// Token: 0x04000FED RID: 4077
		private bool throwOnErrorDeserializing;

		// Token: 0x04000FEE RID: 4078
		private bool throwOnErrorSerializing;
	}
}
