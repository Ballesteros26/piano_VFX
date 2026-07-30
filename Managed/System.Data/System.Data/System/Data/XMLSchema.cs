using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000102 RID: 258
	internal class XMLSchema
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x0003E47F File Offset: 0x0003C67F
		internal static TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003E488 File Offset: 0x0003C688
		internal static void SetProperties(object instance, XmlAttributeCollection attrs)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
				{
					string localName = attrs[i].LocalName;
					string value = attrs[i].Value;
					if (!(localName == "DefaultValue") && !(localName == "RemotingFormat") && (!(localName == "Expression") || !(instance is DataColumn)))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(instance)[localName];
						if (propertyDescriptor != null)
						{
							Type propertyType = propertyDescriptor.PropertyType;
							TypeConverter converter = XMLSchema.GetConverter(propertyType);
							object obj;
							if (converter.CanConvertFrom(typeof(string)))
							{
								obj = converter.ConvertFromString(value);
							}
							else if (propertyType == typeof(Type))
							{
								obj = DataStorage.GetType(value);
							}
							else
							{
								if (!(propertyType == typeof(CultureInfo)))
								{
									throw ExceptionBuilder.CannotConvert(value, propertyType.FullName);
								}
								obj = new CultureInfo(value);
							}
							propertyDescriptor.SetValue(instance, obj);
						}
					}
				}
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003E5AD File Offset: 0x0003C7AD
		internal static bool FEqualIdentity(XmlNode node, string name, string ns)
		{
			return node != null && node.LocalName == name && node.NamespaceURI == ns;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		internal static bool GetBooleanAttribute(XmlElement element, string attrName, string attrNS, bool defVal)
		{
			string attribute = element.GetAttribute(attrName, attrNS);
			if (attribute == null || attribute.Length == 0)
			{
				return defVal;
			}
			if (attribute == "true" || attribute == "1")
			{
				return true;
			}
			if (attribute == "false" || attribute == "0")
			{
				return false;
			}
			throw ExceptionBuilder.InvalidAttributeValue(attrName, attribute);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003E638 File Offset: 0x0003C838
		internal static string GenUniqueColumnName(string proposedName, DataTable table)
		{
			if (table.Columns.IndexOf(proposedName) >= 0)
			{
				for (int i = 0; i <= table.Columns.Count; i++)
				{
					string text = proposedName + "_" + i.ToString(CultureInfo.InvariantCulture);
					if (table.Columns.IndexOf(text) < 0)
					{
						return text;
					}
				}
			}
			return proposedName;
		}
	}
}
