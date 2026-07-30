using System;

namespace System.Xml.Schema
{
	// Token: 0x02000496 RID: 1174
	internal class XmlDateTimeConverter : XmlBaseConverter
	{
		// Token: 0x06002F5B RID: 12123 RVA: 0x0010F8F4 File Offset: 0x0010DAF4
		protected XmlDateTimeConverter(XmlSchemaType schemaType)
			: base(schemaType)
		{
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x00110B38 File Offset: 0x0010ED38
		public static XmlValueConverter Create(XmlSchemaType schemaType)
		{
			return new XmlDateTimeConverter(schemaType);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0000206B File Offset: 0x0000026B
		public override DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x00110B40 File Offset: 0x0010ED40
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToDateTime(value);
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x00110B48 File Offset: 0x0010ED48
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.StringToTime(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.StringToDate(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.StringToGYearMonth(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.StringToGYear(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.StringToGMonthDay(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.StringToGDay(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.StringToGMonth(value);
			default:
				return XmlBaseConverter.StringToDateTime(value);
			}
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x00110BC8 File Offset: 0x0010EDC8
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return (DateTime)value;
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTime((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDateTime((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTime)this.ChangeListType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x00110C5D File Offset: 0x0010EE5D
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return new DateTimeOffset(value);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0000206B File Offset: 0x0000026B
		public override DateTimeOffset ToDateTimeOffset(DateTimeOffset value)
		{
			return value;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x00110C68 File Offset: 0x0010EE68
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.StringToTimeOffset(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.StringToDateOffset(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.StringToGYearMonthOffset(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.StringToGYearOffset(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.StringToGMonthDayOffset(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.StringToGDayOffset(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.StringToGMonthOffset(value);
			default:
				return XmlBaseConverter.StringToDateTimeOffset(value);
			}
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x00110CE8 File Offset: 0x0010EEE8
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTimeOffset((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return (DateTimeOffset)value;
			}
			if (type == XmlBaseConverter.StringType)
			{
				return this.ToDateTimeOffset((string)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAsDateTime;
			}
			return (DateTimeOffset)this.ChangeListType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x00110D84 File Offset: 0x0010EF84
		public override string ToString(DateTime value)
		{
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.TimeToString(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.DateToString(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.GYearMonthToString(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.GYearToString(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.GMonthDayToString(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.GDayToString(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.GMonthToString(value);
			default:
				return XmlBaseConverter.DateTimeToString(value);
			}
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x00110DF8 File Offset: 0x0010EFF8
		public override string ToString(DateTimeOffset value)
		{
			switch (base.TypeCode)
			{
			case XmlTypeCode.Time:
				return XmlBaseConverter.TimeOffsetToString(value);
			case XmlTypeCode.Date:
				return XmlBaseConverter.DateOffsetToString(value);
			case XmlTypeCode.GYearMonth:
				return XmlBaseConverter.GYearMonthOffsetToString(value);
			case XmlTypeCode.GYear:
				return XmlBaseConverter.GYearOffsetToString(value);
			case XmlTypeCode.GMonthDay:
				return XmlBaseConverter.GMonthDayOffsetToString(value);
			case XmlTypeCode.GDay:
				return XmlBaseConverter.GDayOffsetToString(value);
			case XmlTypeCode.GMonth:
				return XmlBaseConverter.GMonthOffsetToString(value);
			default:
				return XmlBaseConverter.DateTimeOffsetToString(value);
			}
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x0010FBEF File Offset: 0x0010DDEF
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x00110E6C File Offset: 0x0010F06C
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.DateTimeType)
			{
				return this.ToString((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToString((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).Value;
			}
			return (string)this.ChangeListType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x00110F04 File Offset: 0x0010F104
		public override object ChangeType(DateTime value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00110FC0 File Offset: 0x0010F1C0
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, null);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x00111088 File Offset: 0x0010F288
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0011114C File Offset: 0x0010F34C
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
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return this.ToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return this.ToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.DateTimeType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTime)value);
				}
				if (type == XmlBaseConverter.DateTimeOffsetType)
				{
					return new XmlAtomicValue(base.SchemaType, (DateTimeOffset)value);
				}
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}
	}
}
