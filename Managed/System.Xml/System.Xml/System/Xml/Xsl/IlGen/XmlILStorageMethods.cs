using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000656 RID: 1622
	internal class XmlILStorageMethods
	{
		// Token: 0x06004137 RID: 16695 RVA: 0x0015BDF8 File Offset: 0x00159FF8
		public XmlILStorageMethods(Type storageType)
		{
			if (storageType == typeof(int) || storageType == typeof(long) || storageType == typeof(decimal) || storageType == typeof(double))
			{
				Type type = Type.GetType("System.Xml.Xsl.Runtime." + storageType.Name + "Aggregator");
				this.AggAvg = XmlILMethods.GetMethod(type, "Average");
				this.AggAvgResult = XmlILMethods.GetMethod(type, "get_AverageResult");
				this.AggCreate = XmlILMethods.GetMethod(type, "Create");
				this.AggIsEmpty = XmlILMethods.GetMethod(type, "get_IsEmpty");
				this.AggMax = XmlILMethods.GetMethod(type, "Maximum");
				this.AggMaxResult = XmlILMethods.GetMethod(type, "get_MaximumResult");
				this.AggMin = XmlILMethods.GetMethod(type, "Minimum");
				this.AggMinResult = XmlILMethods.GetMethod(type, "get_MinimumResult");
				this.AggSum = XmlILMethods.GetMethod(type, "Sum");
				this.AggSumResult = XmlILMethods.GetMethod(type, "get_SumResult");
			}
			if (storageType == typeof(XPathNavigator))
			{
				this.SeqType = typeof(XmlQueryNodeSequence);
				this.SeqAdd = XmlILMethods.GetMethod(this.SeqType, "AddClone");
			}
			else if (storageType == typeof(XPathItem))
			{
				this.SeqType = typeof(XmlQueryItemSequence);
				this.SeqAdd = XmlILMethods.GetMethod(this.SeqType, "AddClone");
			}
			else
			{
				this.SeqType = typeof(XmlQuerySequence<>).MakeGenericType(new Type[] { storageType });
				this.SeqAdd = XmlILMethods.GetMethod(this.SeqType, "Add");
			}
			this.SeqEmpty = this.SeqType.GetField("Empty");
			this.SeqReuse = XmlILMethods.GetMethod(this.SeqType, "CreateOrReuse", new Type[] { this.SeqType });
			this.SeqReuseSgl = XmlILMethods.GetMethod(this.SeqType, "CreateOrReuse", new Type[] { this.SeqType, storageType });
			this.SeqSortByKeys = XmlILMethods.GetMethod(this.SeqType, "SortByKeys");
			this.IListType = typeof(IList<>).MakeGenericType(new Type[] { storageType });
			this.IListItem = XmlILMethods.GetMethod(this.IListType, "get_Item");
			this.IListCount = XmlILMethods.GetMethod(typeof(ICollection<>).MakeGenericType(new Type[] { storageType }), "get_Count");
			if (storageType == typeof(string))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_Value");
			}
			else if (storageType == typeof(int))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsInt");
			}
			else if (storageType == typeof(long))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsLong");
			}
			else if (storageType == typeof(DateTime))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsDateTime");
			}
			else if (storageType == typeof(double))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsDouble");
			}
			else if (storageType == typeof(bool))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsBoolean");
			}
			if (storageType == typeof(byte[]))
			{
				this.ToAtomicValue = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), "BytesToAtomicValue");
				return;
			}
			if (storageType != typeof(XPathItem) && storageType != typeof(XPathNavigator))
			{
				this.ToAtomicValue = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), storageType.Name + "ToAtomicValue");
			}
		}

		// Token: 0x040028F9 RID: 10489
		public MethodInfo AggAvg;

		// Token: 0x040028FA RID: 10490
		public MethodInfo AggAvgResult;

		// Token: 0x040028FB RID: 10491
		public MethodInfo AggCreate;

		// Token: 0x040028FC RID: 10492
		public MethodInfo AggIsEmpty;

		// Token: 0x040028FD RID: 10493
		public MethodInfo AggMax;

		// Token: 0x040028FE RID: 10494
		public MethodInfo AggMaxResult;

		// Token: 0x040028FF RID: 10495
		public MethodInfo AggMin;

		// Token: 0x04002900 RID: 10496
		public MethodInfo AggMinResult;

		// Token: 0x04002901 RID: 10497
		public MethodInfo AggSum;

		// Token: 0x04002902 RID: 10498
		public MethodInfo AggSumResult;

		// Token: 0x04002903 RID: 10499
		public Type SeqType;

		// Token: 0x04002904 RID: 10500
		public FieldInfo SeqEmpty;

		// Token: 0x04002905 RID: 10501
		public MethodInfo SeqReuse;

		// Token: 0x04002906 RID: 10502
		public MethodInfo SeqReuseSgl;

		// Token: 0x04002907 RID: 10503
		public MethodInfo SeqAdd;

		// Token: 0x04002908 RID: 10504
		public MethodInfo SeqSortByKeys;

		// Token: 0x04002909 RID: 10505
		public Type IListType;

		// Token: 0x0400290A RID: 10506
		public MethodInfo IListCount;

		// Token: 0x0400290B RID: 10507
		public MethodInfo IListItem;

		// Token: 0x0400290C RID: 10508
		public MethodInfo ValueAs;

		// Token: 0x0400290D RID: 10509
		public MethodInfo ToAtomicValue;
	}
}
