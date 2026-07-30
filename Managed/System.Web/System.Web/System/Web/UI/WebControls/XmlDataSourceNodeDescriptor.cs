using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000454 RID: 1108
	internal class XmlDataSourceNodeDescriptor : ICustomTypeDescriptor, IXPathNavigable
	{
		// Token: 0x0600337D RID: 13181 RVA: 0x00089F83 File Offset: 0x00088183
		public XmlDataSourceNodeDescriptor(XmlNode node)
		{
			this.node = node;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x00089F92 File Offset: 0x00088192
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x00089F9A File Offset: 0x0008819A
		public AttributeCollection GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x00089FA1 File Offset: 0x000881A1
		public string GetClassName()
		{
			return "XmlDataSourceNodeDescriptor";
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x00003BEA File Offset: 0x00001DEA
		public string GetComponentName()
		{
			return null;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x00003BEA File Offset: 0x00001DEA
		public TypeConverter GetConverter()
		{
			return null;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x00003BEA File Offset: 0x00001DEA
		public EventDescriptor GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x00003BEA File Offset: 0x00001DEA
		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x00003BEA File Offset: 0x00001DEA
		public object GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x00003BEA File Offset: 0x00001DEA
		public EventDescriptorCollection GetEvents()
		{
			return null;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x00003BEA File Offset: 0x00001DEA
		public EventDescriptorCollection GetEvents(Attribute[] arr)
		{
			return null;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x00089FA8 File Offset: 0x000881A8
		public PropertyDescriptorCollection GetProperties()
		{
			if (this.node.Attributes != null)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.node.Attributes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new XmlDataSourcePropertyDescriptor(this.node.Attributes[i].Name, this.node.IsReadOnly);
				}
				return new PropertyDescriptorCollection(array);
			}
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x0008A01B File Offset: 0x0008821B
		public PropertyDescriptorCollection GetProperties(Attribute[] arr)
		{
			return this.GetProperties();
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0008A023 File Offset: 0x00088223
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlDataSourcePropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0008A030 File Offset: 0x00088230
		public XPathNavigator CreateNavigator()
		{
			return this.node.CreateNavigator();
		}

		// Token: 0x04001CD7 RID: 7383
		private XmlNode node;
	}
}
