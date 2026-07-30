using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Contains a single configuration tag remapping statement. This class cannot be inherited.</summary>
	// Token: 0x020005DF RID: 1503
	public sealed class TagMapInfo : ConfigurationElement
	{
		// Token: 0x0600411F RID: 16671 RVA: 0x000AAF00 File Offset: 0x000A9100
		static TagMapInfo()
		{
			TagMapInfo.properties.Add(TagMapInfo.mappedTagTypeProp);
			TagMapInfo.properties.Add(TagMapInfo.tagTypeProp);
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0009F629 File Offset: 0x0009D829
		internal TagMapInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagMapInfo" /> class based on the passed property values.</summary>
		/// <param name="tagTypeName">The fully qualified name of the type for the tag that is being remapped.</param>
		/// <param name="mappedTagTypeName">The name of the type to which the tag is remapped, along with the supporting details.</param>
		// Token: 0x06004121 RID: 16673 RVA: 0x000AAF97 File Offset: 0x000A9197
		public TagMapInfo(string tagTypeName, string mappedTagTypeName)
		{
			this.TagType = tagTypeName;
			this.MappedTagType = mappedTagTypeName;
		}

		/// <summary>Compares this instance to another object.</summary>
		/// <returns>true if the objects are identical; otherwise, false.</returns>
		/// <param name="o">Object to compare.</param>
		// Token: 0x06004122 RID: 16674 RVA: 0x000AAFB0 File Offset: 0x000A91B0
		public override bool Equals(object o)
		{
			TagMapInfo tagMapInfo = o as TagMapInfo;
			return tagMapInfo != null && this.MappedTagType == tagMapInfo.MappedTagType && this.TagType == tagMapInfo.TagType;
		}

		/// <summary>Returns a hash value for the current instance.</summary>
		/// <returns>A hash value for the current instance.</returns>
		// Token: 0x06004123 RID: 16675 RVA: 0x000AAFEF File Offset: 0x000A91EF
		public override int GetHashCode()
		{
			return this.MappedTagType.GetHashCode() + this.TagType.GetHashCode();
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000AB008 File Offset: 0x000A9208
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey);
		}

		/// <summary>Gets or sets the name of the type to which the tag is remapped.</summary>
		/// <returns>The name of the type to which the tag is remapped. The default is an empty string.</returns>
		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000AB012 File Offset: 0x000A9212
		// (set) Token: 0x06004126 RID: 16678 RVA: 0x000AB024 File Offset: 0x000A9224
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("mappedTagType")]
		public string MappedTagType
		{
			get
			{
				return (string)base[TagMapInfo.mappedTagTypeProp];
			}
			set
			{
				base[TagMapInfo.mappedTagTypeProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the original type for the tag that is being remapped.</summary>
		/// <returns>The name of the original type for the tag that is being remapped. </returns>
		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x000AB032 File Offset: 0x000A9232
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x000AB044 File Offset: 0x000A9244
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("tagType", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string TagType
		{
			get
			{
				return (string)base[TagMapInfo.tagTypeProp];
			}
			set
			{
				base[TagMapInfo.tagTypeProp] = value;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x000AB052 File Offset: 0x000A9252
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagMapInfo.properties;
			}
		}

		// Token: 0x0400231F RID: 8991
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002320 RID: 8992
		private static ConfigurationProperty mappedTagTypeProp = new ConfigurationProperty("mappedTagType", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002321 RID: 8993
		private static ConfigurationProperty tagTypeProp = new ConfigurationProperty("tagType", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
