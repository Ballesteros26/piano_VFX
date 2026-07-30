using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000459 RID: 1113
	internal class XmlHierarchyData : IHierarchyData, ICustomTypeDescriptor
	{
		// Token: 0x0600339F RID: 13215 RVA: 0x0008A324 File Offset: 0x00088524
		internal XmlHierarchyData(XmlNode item)
		{
			this.item = item;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0008A333 File Offset: 0x00088533
		public override string ToString()
		{
			return this.item.Name;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x00089F9A File Offset: 0x0008819A
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x0008A340 File Offset: 0x00088540
		string ICustomTypeDescriptor.GetClassName()
		{
			return "XmlHierarchyData";
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00003BEA File Offset: 0x00001DEA
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00003BEA File Offset: 0x00001DEA
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x00003BEA File Offset: 0x00001DEA
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x0008A347 File Offset: 0x00088547
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##Name##");
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x00003BEA File Offset: 0x00001DEA
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x00003BEA File Offset: 0x00001DEA
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return null;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x00003BEA File Offset: 0x00001DEA
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
		{
			return null;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x0008A359 File Offset: 0x00088559
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x0008A364 File Offset: 0x00088564
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrFilter)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##Name##"));
			arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##Value##"));
			arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##InnerText##"));
			if (this.item.Attributes != null)
			{
				foreach (object obj in this.item.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, xmlAttribute.Name));
				}
			}
			return new PropertyDescriptorCollection((PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor)));
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x0008A448 File Offset: 0x00088648
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlHierarchyData.XmlHierarchyDataPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x0008A455 File Offset: 0x00088655
		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return new XmlHierarchicalEnumerable(this.item.ChildNodes);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x0008A467 File Offset: 0x00088667
		IHierarchyData IHierarchyData.GetParent()
		{
			return new XmlHierarchyData(this.item.ParentNode);
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x0008A479 File Offset: 0x00088679
		bool IHierarchyData.HasChildren
		{
			get
			{
				return this.item.HasChildNodes;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x0008A486 File Offset: 0x00088686
		object IHierarchyData.Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x0008A490 File Offset: 0x00088690
		string IHierarchyData.Path
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				XmlNode parentNode = this.item;
				do
				{
					int num = 1;
					XmlNode xmlNode = parentNode.PreviousSibling;
					while (xmlNode != null)
					{
						xmlNode = xmlNode.PreviousSibling;
						num++;
					}
					stringBuilder.Insert(0, "/*[position()=" + num + "]");
					parentNode = parentNode.ParentNode;
				}
				while (parentNode != null && !(parentNode is XmlDocument));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x0008A333 File Offset: 0x00088533
		string IHierarchyData.Type
		{
			get
			{
				return this.item.Name;
			}
		}

		// Token: 0x04001CDD RID: 7389
		private XmlNode item;

		// Token: 0x0200045A RID: 1114
		private class XmlHierarchyDataPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x060033B3 RID: 13235 RVA: 0x0008A4F8 File Offset: 0x000886F8
			public XmlHierarchyDataPropertyDescriptor(XmlNode xmlNode, string name)
				: base(name, null)
			{
				this.xmlNode = xmlNode;
				this.name = name;
			}

			// Token: 0x060033B4 RID: 13236 RVA: 0x00008A69 File Offset: 0x00006C69
			public override bool CanResetValue(object o)
			{
				return false;
			}

			// Token: 0x060033B5 RID: 13237 RVA: 0x0000393A File Offset: 0x00001B3A
			public override void ResetValue(object o)
			{
			}

			// Token: 0x060033B6 RID: 13238 RVA: 0x0008A510 File Offset: 0x00088710
			public override object GetValue(object o)
			{
				if (o is XmlHierarchyData)
				{
					string text = this.name;
					if (text == "##Name##")
					{
						return this.xmlNode.Name;
					}
					if (text == "##Value##")
					{
						return this.xmlNode.Value;
					}
					if (text == "##InnerText##")
					{
						return this.xmlNode.InnerText;
					}
					if (text == null)
					{
						return string.Empty;
					}
					if (this.xmlNode.Attributes != null)
					{
						XmlAttribute xmlAttribute = this.xmlNode.Attributes[this.name];
						if (xmlAttribute != null)
						{
							return xmlAttribute.Value;
						}
					}
				}
				return string.Empty;
			}

			// Token: 0x060033B7 RID: 13239 RVA: 0x0008A5B8 File Offset: 0x000887B8
			public override void SetValue(object o, object value)
			{
				if (o is XmlHierarchyData)
				{
					string text = this.name;
					if (!(text == "##Name##"))
					{
						if (text == "##Value##")
						{
							this.xmlNode.Value = value.ToString();
							return;
						}
						if (text == "##InnerText##")
						{
							this.xmlNode.InnerText = value.ToString();
							return;
						}
						if (text != null && this.xmlNode.Attributes != null)
						{
							XmlAttribute xmlAttribute = this.xmlNode.Attributes[this.name];
							if (xmlAttribute != null)
							{
								xmlAttribute.Value = value.ToString();
							}
						}
					}
				}
			}

			// Token: 0x060033B8 RID: 13240 RVA: 0x0008A0E6 File Offset: 0x000882E6
			public override bool ShouldSerializeValue(object o)
			{
				return o is XmlNode;
			}

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x060033B9 RID: 13241 RVA: 0x0008A65A File Offset: 0x0008885A
			public override Type ComponentType
			{
				get
				{
					return typeof(XmlHierarchyData);
				}
			}

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x060033BA RID: 13242 RVA: 0x0008A666 File Offset: 0x00088866
			public override bool IsReadOnly
			{
				get
				{
					return this.xmlNode.IsReadOnly;
				}
			}

			// Token: 0x1700104F RID: 4175
			// (get) Token: 0x060033BB RID: 13243 RVA: 0x000363B2 File Offset: 0x000345B2
			public override Type PropertyType
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x04001CDE RID: 7390
			private string name;

			// Token: 0x04001CDF RID: 7391
			private XmlNode xmlNode;
		}
	}
}
