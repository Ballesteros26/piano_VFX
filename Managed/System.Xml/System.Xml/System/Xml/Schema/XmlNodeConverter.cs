using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x0200049B RID: 1179
	internal class XmlNodeConverter : XmlBaseConverter
	{
		// Token: 0x06002FB1 RID: 12209 RVA: 0x00112FFE File Offset: 0x001111FE
		protected XmlNodeConverter()
			: base(XmlTypeCode.Node)
		{
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x00113008 File Offset: 0x00111208
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XPathNavigatorType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return (XPathNavigator)value;
			}
			if (destinationType == XmlBaseConverter.XPathItemType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return (XPathItem)value;
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x04001EE7 RID: 7911
		public static readonly XmlValueConverter Node = new XmlNodeConverter();
	}
}
