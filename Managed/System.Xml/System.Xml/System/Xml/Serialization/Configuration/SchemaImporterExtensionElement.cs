using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the configuration for the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000378 RID: 888
	public sealed class SchemaImporterExtensionElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> class.</summary>
		// Token: 0x0600241F RID: 9247 RVA: 0x000DCE0C File Offset: 0x000DB00C
		public SchemaImporterExtensionElement()
		{
			this.properties.Add(this.name);
			this.properties.Add(this.type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> class and specifies the name and type of the extension.</summary>
		/// <param name="name">The name of the new extension. The name must be unique.</param>
		/// <param name="type">The type of the new extension, specified as a string.</param>
		// Token: 0x06002420 RID: 9248 RVA: 0x000DCE8A File Offset: 0x000DB08A
		public SchemaImporterExtensionElement(string name, string type)
			: this()
		{
			this.Name = name;
			base[this.type] = new SchemaImporterExtensionElement.TypeAndName(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> class using the specified name and type.</summary>
		/// <param name="name">The name of the new extension. The name must be unique.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the new extension.</param>
		// Token: 0x06002421 RID: 9249 RVA: 0x000DCEAB File Offset: 0x000DB0AB
		public SchemaImporterExtensionElement(string name, Type type)
			: this()
		{
			this.Name = name;
			this.Type = type;
		}

		/// <summary>Gets or sets the name of the extension.</summary>
		/// <returns>The name of the extension.</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000DCEC1 File Offset: 0x000DB0C1
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x000DCED4 File Offset: 0x000DB0D4
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[this.name];
			}
			set
			{
				base[this.name] = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x000DCEE3 File Offset: 0x000DB0E3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the type of the extension.</summary>
		/// <returns>A type of the extension.</returns>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x000DCEEB File Offset: 0x000DB0EB
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x000DCF03 File Offset: 0x000DB103
		[TypeConverter(typeof(SchemaImporterExtensionElement.TypeTypeConverter))]
		[ConfigurationProperty("type", IsRequired = true, IsKey = false)]
		public Type Type
		{
			get
			{
				return ((SchemaImporterExtensionElement.TypeAndName)base[this.type]).type;
			}
			set
			{
				base[this.type] = new SchemaImporterExtensionElement.TypeAndName(value);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000DCF17 File Offset: 0x000DB117
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x040018B2 RID: 6322
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040018B3 RID: 6323
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x040018B4 RID: 6324
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(Type), null, new SchemaImporterExtensionElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x02000379 RID: 889
		private class TypeAndName
		{
			// Token: 0x06002428 RID: 9256 RVA: 0x000DCF1F File Offset: 0x000DB11F
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x06002429 RID: 9257 RVA: 0x000DCF3C File Offset: 0x000DB13C
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x0600242A RID: 9258 RVA: 0x000DCF4B File Offset: 0x000DB14B
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x0600242B RID: 9259 RVA: 0x000DCF58 File Offset: 0x000DB158
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((SchemaImporterExtensionElement.TypeAndName)comparand).type);
			}

			// Token: 0x040018B5 RID: 6325
			public readonly Type type;

			// Token: 0x040018B6 RID: 6326
			public readonly string name;
		}

		// Token: 0x0200037A RID: 890
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x0600242C RID: 9260 RVA: 0x000DCF70 File Offset: 0x000DB170
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600242D RID: 9261 RVA: 0x000DCF8E File Offset: 0x000DB18E
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new SchemaImporterExtensionElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x0600242E RID: 9262 RVA: 0x000DCFB0 File Offset: 0x000DB1B0
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (!(destinationType == typeof(string)))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				SchemaImporterExtensionElement.TypeAndName typeAndName = (SchemaImporterExtensionElement.TypeAndName)value;
				if (typeAndName.name != null)
				{
					return typeAndName.name;
				}
				return typeAndName.type.AssemblyQualifiedName;
			}
		}
	}
}
