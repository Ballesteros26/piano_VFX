using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap
{
	// Token: 0x02000010 RID: 16
	internal class MethodSignature
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003E66 File Offset: 0x00002066
		public MethodSignature(Type[] types)
		{
			this.types = types;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003E78 File Offset: 0x00002078
		public static object ReadXmlValue(SoapReader reader)
		{
			reader.XmlReader.MoveToElement();
			if (reader.XmlReader.IsEmptyElement)
			{
				reader.XmlReader.Skip();
				return new Type[0];
			}
			reader.XmlReader.ReadStartElement();
			string text = reader.XmlReader.ReadString();
			while (reader.XmlReader.NodeType != XmlNodeType.EndElement)
			{
				reader.XmlReader.Skip();
			}
			ArrayList arrayList = new ArrayList();
			foreach (string text2 in text.Split(new char[] { ' ' }))
			{
				if (text2.Length != 0)
				{
					arrayList.Add(reader.GetTypeFromQName(text2));
				}
			}
			reader.XmlReader.ReadEndElement();
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003F48 File Offset: 0x00002148
		public string GetXmlValue(SoapWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in this.types)
			{
				Element xmlElement = writer.Mapper.GetXmlElement(type);
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				string namespacePrefix = writer.GetNamespacePrefix(xmlElement);
				stringBuilder.Append(namespacePrefix).Append(':').Append(xmlElement.LocalName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000053 RID: 83
		private Type[] types;
	}
}
