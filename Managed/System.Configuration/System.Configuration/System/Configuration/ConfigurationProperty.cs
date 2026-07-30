using System;
using System.ComponentModel;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents an attribute or a child of a configuration element. This class cannot be inherited.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class ConfigurationProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		// Token: 0x06000164 RID: 356 RVA: 0x000062F7 File Offset: 0x000044F7
		public ConfigurationProperty(string name, Type type)
			: this(name, type, ConfigurationProperty.NoDefaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		// Token: 0x06000165 RID: 357 RVA: 0x00006313 File Offset: 0x00004513
		public ConfigurationProperty(string name, Type type, object defaultValue)
			: this(name, type, defaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), ConfigurationPropertyOptions.None, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values.</param>
		// Token: 0x06000166 RID: 358 RVA: 0x0000632B File Offset: 0x0000452B
		public ConfigurationProperty(string name, Type type, object defaultValue, ConfigurationPropertyOptions options)
			: this(name, type, defaultValue, TypeDescriptor.GetConverter(type), new DefaultValidator(), options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity.</param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values. </param>
		// Token: 0x06000167 RID: 359 RVA: 0x00006344 File Offset: 0x00004544
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options)
			: this(name, type, defaultValue, typeConverter, validator, options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationProperty" /> class. </summary>
		/// <param name="name">The name of the configuration entity. </param>
		/// <param name="type">The type of the configuration entity. </param>
		/// <param name="defaultValue">The default value of the configuration entity. </param>
		/// <param name="typeConverter">The type of the converter to apply.</param>
		/// <param name="validator">The validator to use. </param>
		/// <param name="options">One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values. </param>
		/// <param name="description">The description of the configuration entity. </param>
		// Token: 0x06000168 RID: 360 RVA: 0x00006358 File Offset: 0x00004558
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options, string description)
		{
			this.name = name;
			this.converter = ((typeConverter != null) ? typeConverter : TypeDescriptor.GetConverter(type));
			if (defaultValue != null)
			{
				if (defaultValue == ConfigurationProperty.NoDefaultValue)
				{
					TypeCode typeCode = Type.GetTypeCode(type);
					if (typeCode != TypeCode.Object)
					{
						if (typeCode != TypeCode.String)
						{
							defaultValue = Activator.CreateInstance(type);
						}
						else
						{
							defaultValue = string.Empty;
						}
					}
					else
					{
						defaultValue = null;
					}
				}
				else if (!type.IsAssignableFrom(defaultValue.GetType()))
				{
					if (!this.converter.CanConvertFrom(defaultValue.GetType()))
					{
						throw new ConfigurationErrorsException(string.Format("The default value for property '{0}' has a different type than the one of the property itself: expected {1} but was {2}", name, type, defaultValue.GetType()));
					}
					defaultValue = this.converter.ConvertFrom(defaultValue);
				}
			}
			this.default_value = defaultValue;
			this.flags = options;
			this.type = type;
			this.validation = ((validator != null) ? validator : new DefaultValidator());
			this.description = description;
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.TypeConverter" /> used to convert this <see cref="T:System.Configuration.ConfigurationProperty" /> into an XML representation for writing to the configuration file.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> used to convert this <see cref="T:System.Configuration.ConfigurationProperty" /> into an XML representation for writing to the configuration file.</returns>
		/// <exception cref="T:System.Exception">This <see cref="T:System.Configuration.ConfigurationProperty" /> cannot be converted. </exception>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006433 File Offset: 0x00004633
		public TypeConverter Converter
		{
			get
			{
				return this.converter;
			}
		}

		/// <summary>Gets the default value for this <see cref="T:System.Configuration.ConfigurationProperty" /> property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to the type specified by the <see cref="P:System.Configuration.ConfigurationProperty.Type" /> property.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000643B File Offset: 0x0000463B
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is the key for the containing <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>true if this <see cref="T:System.Configuration.ConfigurationProperty" /> object is the key for the containing element; otherwise, false. The default is false.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006443 File Offset: 0x00004643
		public bool IsKey
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsKey) > ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Configuration.ConfigurationProperty" /> is required.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationProperty" /> is required; otherwise, false. The default is false.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006450 File Offset: 0x00004650
		public bool IsRequired
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsRequired) > ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets a value that indicates whether the property is the default collection of an element. </summary>
		/// <returns>true if the property is the default collection of an element; otherwise, false.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000645D File Offset: 0x0000465D
		public bool IsDefaultCollection
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsDefaultCollection) > ConfigurationPropertyOptions.None;
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000646A File Offset: 0x0000466A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the description associated with the <see cref="T:System.Configuration.ConfigurationProperty" />.</summary>
		/// <returns>A string value that describes the property.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006472 File Offset: 0x00004672
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000647A File Offset: 0x0000467A
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationValidatorAttribute" />, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator, which is used to validate this <see cref="T:System.Configuration.ConfigurationProperty" />.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006482 File Offset: 0x00004682
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.validation;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000648A File Offset: 0x0000468A
		internal object ConvertFromString(string value)
		{
			if (this.converter != null)
			{
				return this.converter.ConvertFromInvariantString(value);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000064A6 File Offset: 0x000046A6
		internal string ConvertToString(object value)
		{
			if (this.converter != null)
			{
				return this.converter.ConvertToInvariantString(value);
			}
			throw new NotImplementedException();
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000064C2 File Offset: 0x000046C2
		internal bool IsElement
		{
			get
			{
				return typeof(ConfigurationElement).IsAssignableFrom(this.type);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000064D9 File Offset: 0x000046D9
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000064E1 File Offset: 0x000046E1
		internal ConfigurationCollectionAttribute CollectionAttribute
		{
			get
			{
				return this.collectionAttribute;
			}
			set
			{
				this.collectionAttribute = value;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000064EA File Offset: 0x000046EA
		internal void Validate(object value)
		{
			if (this.validation != null)
			{
				this.validation.Validate(value);
			}
		}

		/// <summary>Indicates whether the assembly name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		/// <returns>true if the property requires assembly name transformation; otherwise, false.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000650C File Offset: 0x0000470C
		public bool IsAssemblyStringTransformationRequired
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Indicates whether the type name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		/// <returns>true if the property requires type-name transformation; otherwise, false.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00006528 File Offset: 0x00004728
		public bool IsTypeStringTransformationRequired
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Indicates whether the configuration property's parent configuration section is queried at serialization time to determine whether the configuration property should be serialized into XML.</summary>
		/// <returns>true if the parent configuration section should be queried; otherwise, false.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006544 File Offset: 0x00004744
		public bool IsVersionCheckRequired
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x0400009E RID: 158
		internal static readonly object NoDefaultValue = new object();

		// Token: 0x0400009F RID: 159
		private string name;

		// Token: 0x040000A0 RID: 160
		private Type type;

		// Token: 0x040000A1 RID: 161
		private object default_value;

		// Token: 0x040000A2 RID: 162
		private TypeConverter converter;

		// Token: 0x040000A3 RID: 163
		private ConfigurationValidatorBase validation;

		// Token: 0x040000A4 RID: 164
		private ConfigurationPropertyOptions flags;

		// Token: 0x040000A5 RID: 165
		private string description;

		// Token: 0x040000A6 RID: 166
		private ConfigurationCollectionAttribute collectionAttribute;
	}
}
