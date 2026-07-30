using System;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	/// <summary>Represents the Properties tab on a <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	// Token: 0x0200001D RID: 29
	public class PropertiesTab : PropertyTab
	{
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties.</returns>
		/// <param name="component">The component to retrieve properties from. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
		// Token: 0x060000EE RID: 238 RVA: 0x000048EC File Offset: 0x00002AEC
		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return this.GetProperties(null, component, attributes);
		}

		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties matching the specified context and attributes.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context to retrieve properties from. </param>
		/// <param name="component">The component to retrieve properties from. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the properties to retrieve. </param>
		// Token: 0x060000EF RID: 239 RVA: 0x000048F8 File Offset: 0x00002AF8
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			if (component == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			if (attributes == null)
			{
				attributes = new Attribute[] { BrowsableAttribute.Yes };
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			TypeConverter converter = TypeDescriptor.GetConverter(component);
			if (converter != null && converter.GetPropertiesSupported())
			{
				propertyDescriptorCollection = converter.GetProperties(context, component, attributes);
			}
			if (propertyDescriptorCollection == null)
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(component, attributes);
			}
			return propertyDescriptorCollection;
		}

		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property.</returns>
		/// <param name="obj"></param>
		// Token: 0x060000F0 RID: 240 RVA: 0x0000495C File Offset: 0x00002B5C
		public override PropertyDescriptor GetDefaultProperty(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return TypeDescriptor.GetDefaultProperty(obj);
		}

		/// <summary>Gets the Help keyword that is to be associated with this tab.</summary>
		/// <returns>The string "vs.properties".</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000496C File Offset: 0x00002B6C
		public override string HelpKeyword
		{
			get
			{
				return "vs.properties";
			}
		}

		/// <summary>Gets the name of the Properties tab.</summary>
		/// <returns>The string "Properties".</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004974 File Offset: 0x00002B74
		public override string TabName
		{
			get
			{
				return "Properties";
			}
		}
	}
}
