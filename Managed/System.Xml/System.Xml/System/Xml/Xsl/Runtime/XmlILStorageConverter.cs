using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000602 RID: 1538
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XmlILStorageConverter
	{
		// Token: 0x06003BEB RID: 15339 RVA: 0x0014FC04 File Offset: 0x0014DE04
		public static XmlAtomicValue StringToAtomicValue(string value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x0014FC18 File Offset: 0x0014DE18
		public static XmlAtomicValue DecimalToAtomicValue(decimal value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x0014FC31 File Offset: 0x0014DE31
		public static XmlAtomicValue Int64ToAtomicValue(long value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x0014FC45 File Offset: 0x0014DE45
		public static XmlAtomicValue Int32ToAtomicValue(int value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x0014FC59 File Offset: 0x0014DE59
		public static XmlAtomicValue BooleanToAtomicValue(bool value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x0014FC6D File Offset: 0x0014DE6D
		public static XmlAtomicValue DoubleToAtomicValue(double value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x0014FC81 File Offset: 0x0014DE81
		public static XmlAtomicValue SingleToAtomicValue(float value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, (double)value);
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0014FC96 File Offset: 0x0014DE96
		public static XmlAtomicValue DateTimeToAtomicValue(DateTime value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x0014FCAA File Offset: 0x0014DEAA
		public static XmlAtomicValue XmlQualifiedNameToAtomicValue(XmlQualifiedName value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x0014FCBE File Offset: 0x0014DEBE
		public static XmlAtomicValue TimeSpanToAtomicValue(TimeSpan value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x0014FCAA File Offset: 0x0014DEAA
		public static XmlAtomicValue BytesToAtomicValue(byte[] value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x0014FCD8 File Offset: 0x0014DED8
		public static IList<XPathItem> NavigatorsToItems(IList<XPathNavigator> listNavigators)
		{
			IList<XPathItem> list = listNavigators as IList<XPathItem>;
			if (list != null)
			{
				return list;
			}
			return new XmlQueryNodeSequence(listNavigators);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x0014FCF8 File Offset: 0x0014DEF8
		public static IList<XPathNavigator> ItemsToNavigators(IList<XPathItem> listItems)
		{
			IList<XPathNavigator> list = listItems as IList<XPathNavigator>;
			if (list != null)
			{
				return list;
			}
			XmlQueryNodeSequence xmlQueryNodeSequence = new XmlQueryNodeSequence(listItems.Count);
			for (int i = 0; i < listItems.Count; i++)
			{
				xmlQueryNodeSequence.Add((XPathNavigator)listItems[i]);
			}
			return xmlQueryNodeSequence;
		}
	}
}
