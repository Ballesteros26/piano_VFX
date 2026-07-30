using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200049D RID: 1181
	internal class XmlAnyListConverter : XmlListConverter
	{
		// Token: 0x06002FCB RID: 12235 RVA: 0x00113B47 File Offset: 0x00111D47
		protected XmlAnyListConverter(XmlBaseConverter atomicConverter)
			: base(atomicConverter)
		{
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00113B50 File Offset: 0x00111D50
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
			if (!(value is IEnumerable) || value.GetType() == XmlBaseConverter.StringType || value.GetType() == XmlBaseConverter.ByteArrayType)
			{
				value = new object[] { value };
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x04001EEA RID: 7914
		public static readonly XmlValueConverter ItemList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.Item);

		// Token: 0x04001EEB RID: 7915
		public static readonly XmlValueConverter AnyAtomicList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.AnyAtomic);
	}
}
