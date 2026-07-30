using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace System.Configuration
{
	/// <summary>Contains the value of a settings property that can be loaded and stored by an instance of <see cref="T:System.Configuration.SettingsBase" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000199 RID: 409
	public class SettingsPropertyValue
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValue" /> class, based on supplied parameters.</summary>
		/// <param name="property">Specifies a <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000C2A RID: 3114 RVA: 0x0003CB7F File Offset: 0x0003AD7F
		public SettingsPropertyValue(SettingsProperty property)
		{
			this.property = property;
			this.needPropertyValue = true;
		}

		/// <summary>Gets or sets whether the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has been deserialized. </summary>
		/// <returns>true if the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has been deserialized; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0003CB95 File Offset: 0x0003AD95
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0003CB9D File Offset: 0x0003AD9D
		public bool Deserialized
		{
			get
			{
				return this.deserialized;
			}
			set
			{
				this.deserialized = value;
			}
		}

		/// <summary>Gets or sets whether the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has changed. </summary>
		/// <returns>true if the value of a <see cref="T:System.Configuration.SettingsProperty" /> object has changed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0003CBA6 File Offset: 0x0003ADA6
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0003CBAE File Offset: 0x0003ADAE
		public bool IsDirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = value;
			}
		}

		/// <summary>Gets the name of the property from the associated <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0003CBB7 File Offset: 0x0003ADB7
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.SettingsProperty" /> object that describes the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0003CBC4 File Offset: 0x0003ADC4
		public SettingsProperty Property
		{
			get
			{
				return this.property;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.SettingsProperty" /> object. When this value is set, the <see cref="P:System.Configuration.SettingsPropertyValue.IsDirty" /> property is set to true and <see cref="P:System.Configuration.SettingsPropertyValue.UsingDefaultValue" /> is set to false.When a value is first accessed from the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property, and if the value was initially stored into the <see cref="T:System.Configuration.SettingsPropertyValue" /> object as a serialized representation using the <see cref="P:System.Configuration.SettingsPropertyValue.SerializedValue" /> property, the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property will trigger deserialization of the underlying value.  As a side effect, the <see cref="P:System.Configuration.SettingsPropertyValue.Deserialized" /> property will be set to true.If this chain of events occurs in ASP.NET, and if an error occurs during the deserialization process, the error is logged using the health-monitoring feature of ASP.NET. By default, this means that deserialization errors will show up in the Application Event Log when running under ASP.NET. If this process occurs outside of ASP.NET, and if an error occurs during deserialization, the error is suppressed, and the remainder of the logic during deserialization occurs. If there is no serialized value to deserialize when the deserialization is attempted, then <see cref="T:System.Configuration.SettingsPropertyValue" /> object will instead attempt to return a default value if one was configured as defined on the associated <see cref="T:System.Configuration.SettingsProperty" /> instance. In this case, if the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property was set to either null, or to the string "[null]", then the <see cref="T:System.Configuration.SettingsPropertyValue" /> object will initialize the <see cref="P:System.Configuration.SettingsPropertyValue.PropertyValue" /> property to either null for reference types, or to the default value for the associated value type.  On the other hand, if <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property holds a valid object reference or string value (other than "[null]"), then the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property is returned instead.If there is no serialized value to deserialize when the deserialization is attempted, and no default value was specified, then an empty string will be returned for string types. For all other types, a default instance will be returned by calling <see cref="M:System.Activator.CreateInstance(System.Type)" /> — for reference types this means an attempt will be made to create an object instance using the default constructor.  If this attempt fails, then null is returned.</returns>
		/// <exception cref="T:System.ArgumentException">While attempting to use the default value from the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property, an error occurred.  Either the attempt to convert <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property to a valid type failed, or the resulting value was not compatible with the type defined by <see cref="P:System.Configuration.SettingsProperty.PropertyType" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0003CBCC File Offset: 0x0003ADCC
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0003CC56 File Offset: 0x0003AE56
		public object PropertyValue
		{
			get
			{
				if (this.needPropertyValue)
				{
					this.propertyValue = this.GetDeserializedValue(this.serializedValue);
					if (this.propertyValue == null)
					{
						this.propertyValue = this.GetDeserializedDefaultValue();
						this.defaulted = true;
					}
					this.needPropertyValue = false;
				}
				if (this.propertyValue != null && !(this.propertyValue is string) && !(this.propertyValue is DateTime) && !this.property.PropertyType.IsPrimitive)
				{
					this.dirty = true;
				}
				return this.propertyValue;
			}
			set
			{
				this.propertyValue = value;
				this.dirty = true;
				this.needPropertyValue = false;
				this.needSerializedValue = true;
				this.defaulted = false;
			}
		}

		/// <summary>Gets or sets the serialized value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>The serialized value of a <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The serialization options for the property indicated the use of a string type converter, but a type converter was not available.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0003CC7C File Offset: 0x0003AE7C
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0003CD6D File Offset: 0x0003AF6D
		public object SerializedValue
		{
			get
			{
				if (this.needSerializedValue)
				{
					this.needSerializedValue = false;
					switch (this.property.SerializeAs)
					{
					case SettingsSerializeAs.String:
						this.serializedValue = TypeDescriptor.GetConverter(this.property.PropertyType).ConvertToInvariantString(this.propertyValue);
						break;
					case SettingsSerializeAs.Xml:
						if (this.propertyValue != null)
						{
							XmlSerializer xmlSerializer = new XmlSerializer(this.propertyValue.GetType());
							StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
							xmlSerializer.Serialize(stringWriter, this.propertyValue);
							this.serializedValue = stringWriter.ToString();
						}
						else
						{
							this.serializedValue = null;
						}
						break;
					case SettingsSerializeAs.Binary:
						if (this.propertyValue != null)
						{
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							MemoryStream memoryStream = new MemoryStream();
							binaryFormatter.Serialize(memoryStream, this.propertyValue);
							this.serializedValue = memoryStream.ToArray();
						}
						else
						{
							this.serializedValue = null;
						}
						break;
					default:
						this.serializedValue = null;
						break;
					}
				}
				return this.serializedValue;
			}
			set
			{
				this.serializedValue = value;
				this.needPropertyValue = true;
			}
		}

		/// <summary>Gets a Boolean value specifying whether the value of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object is the default value as defined by the <see cref="P:System.Configuration.SettingsProperty.DefaultValue" /> property value on the associated <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>true if the value of the <see cref="T:System.Configuration.SettingsProperty" /> object is the default value; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0003CD7D File Offset: 0x0003AF7D
		public bool UsingDefaultValue
		{
			get
			{
				return this.defaulted;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0003CD85 File Offset: 0x0003AF85
		internal object Reset()
		{
			this.propertyValue = this.GetDeserializedDefaultValue();
			this.dirty = true;
			this.defaulted = true;
			this.needPropertyValue = true;
			return this.propertyValue;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		private object GetDeserializedDefaultValue()
		{
			if (this.property.DefaultValue == null)
			{
				if (this.property.PropertyType != null && this.property.PropertyType.IsValueType)
				{
					return Activator.CreateInstance(this.property.PropertyType);
				}
				return null;
			}
			else if (this.property.DefaultValue is string && ((string)this.property.DefaultValue).Length == 0)
			{
				if (this.property.PropertyType != typeof(string))
				{
					return Activator.CreateInstance(this.property.PropertyType);
				}
				return string.Empty;
			}
			else
			{
				if (this.property.DefaultValue is string && ((string)this.property.DefaultValue).Length > 0)
				{
					return this.GetDeserializedValue(this.property.DefaultValue);
				}
				if (!this.property.PropertyType.IsAssignableFrom(this.property.DefaultValue.GetType()))
				{
					return TypeDescriptor.GetConverter(this.property.PropertyType).ConvertFrom(null, CultureInfo.InvariantCulture, this.property.DefaultValue);
				}
				return this.property.DefaultValue;
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0003CEF0 File Offset: 0x0003B0F0
		private object GetDeserializedValue(object serializedValue)
		{
			if (serializedValue == null)
			{
				return null;
			}
			object obj = null;
			try
			{
				switch (this.property.SerializeAs)
				{
				case SettingsSerializeAs.String:
					if (serializedValue is string)
					{
						obj = TypeDescriptor.GetConverter(this.property.PropertyType).ConvertFromInvariantString((string)serializedValue);
					}
					break;
				case SettingsSerializeAs.Xml:
				{
					XmlSerializer xmlSerializer = new XmlSerializer(this.property.PropertyType);
					StringReader stringReader = new StringReader((string)serializedValue);
					obj = xmlSerializer.Deserialize(XmlReader.Create(stringReader));
					break;
				}
				case SettingsSerializeAs.Binary:
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream memoryStream;
					if (serializedValue is string)
					{
						memoryStream = new MemoryStream(Convert.FromBase64String((string)serializedValue));
					}
					else
					{
						memoryStream = new MemoryStream((byte[])serializedValue);
					}
					obj = binaryFormatter.Deserialize(memoryStream);
					break;
				}
				}
			}
			catch (Exception ex)
			{
				if (this.property.ThrowOnErrorDeserializing)
				{
					throw ex;
				}
			}
			return obj;
		}

		// Token: 0x04000FF1 RID: 4081
		private readonly SettingsProperty property;

		// Token: 0x04000FF2 RID: 4082
		private object propertyValue;

		// Token: 0x04000FF3 RID: 4083
		private object serializedValue;

		// Token: 0x04000FF4 RID: 4084
		private bool needSerializedValue;

		// Token: 0x04000FF5 RID: 4085
		private bool needPropertyValue;

		// Token: 0x04000FF6 RID: 4086
		private bool dirty;

		// Token: 0x04000FF7 RID: 4087
		private bool defaulted;

		// Token: 0x04000FF8 RID: 4088
		private bool deserialized;
	}
}
