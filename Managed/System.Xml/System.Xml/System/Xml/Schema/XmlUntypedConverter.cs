using System;

namespace System.Xml.Schema
{
	// Token: 0x0200049A RID: 1178
	internal class XmlUntypedConverter : XmlListConverter
	{
		// Token: 0x06002F86 RID: 12166 RVA: 0x00111F02 File Offset: 0x00110102
		protected XmlUntypedConverter()
			: base(DatatypeImplementation.UntypedAtomicType)
		{
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x00111F0F File Offset: 0x0011010F
		protected XmlUntypedConverter(XmlUntypedConverter atomicConverter, bool allowListToList)
			: base(atomicConverter, allowListToList ? XmlBaseConverter.StringArrayType : XmlBaseConverter.StringType)
		{
			this.allowListToList = allowListToList;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x001112F8 File Offset: 0x0010F4F8
		public override bool ToBoolean(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x00111F2E File Offset: 0x0011012E
		public override bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			return (bool)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x00111F6E File Offset: 0x0011016E
		public override DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTime(value);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x00111F84 File Offset: 0x00110184
		public override DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime((string)value);
			}
			return (DateTime)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x00111FC4 File Offset: 0x001101C4
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x00111FDA File Offset: 0x001101DA
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset((string)value);
			}
			return (DateTimeOffset)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0011201A File Offset: 0x0011021A
		public override decimal ToDecimal(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDecimal(value);
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x00112030 File Offset: 0x00110230
		public override decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDecimal((string)value);
			}
			return (decimal)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00112070 File Offset: 0x00110270
		public override double ToDouble(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToDouble(value);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x00112086 File Offset: 0x00110286
		public override double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDouble((string)value);
			}
			return (double)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x001120C6 File Offset: 0x001102C6
		public override int ToInt32(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x001120DC File Offset: 0x001102DC
		public override int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt32((string)value);
			}
			return (int)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0011211C File Offset: 0x0011031C
		public override long ToInt64(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToInt64(value);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x00112132 File Offset: 0x00110332
		public override long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt64((string)value);
			}
			return (long)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x00112172 File Offset: 0x00110372
		public override float ToSingle(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return XmlConvert.ToSingle(value);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x00112188 File Offset: 0x00110388
		public override float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.GetType() == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToSingle((string)value);
			}
			return (float)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x0011138A File Offset: 0x0010F58A
		public override string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x001121C8 File Offset: 0x001103C8
		public override string ToString(DateTime value)
		{
			return XmlBaseConverter.DateTimeToString(value);
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x001121D0 File Offset: 0x001103D0
		public override string ToString(DateTimeOffset value)
		{
			return XmlBaseConverter.DateTimeOffsetToString(value);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x001121D8 File Offset: 0x001103D8
		public override string ToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x001121E0 File Offset: 0x001103E0
		public override string ToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0010FBDF File Offset: 0x0010DDDF
		public override string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x0010FBE7 File Offset: 0x0010DDE7
		public override string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x001121E9 File Offset: 0x001103E9
		public override string ToString(float value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0010FBEF File Offset: 0x0010DDEF
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x001121F4 File Offset: 0x001103F4
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToString((bool)value);
			}
			if (type == XmlBaseConverter.ByteType)
			{
				return XmlConvert.ToString((byte)value);
			}
			if (type == XmlBaseConverter.ByteArrayType)
			{
				return XmlBaseConverter.Base64BinaryToString((byte[])value);
			}
			if (type == XmlBaseConverter.DateTimeType)
			{
				return XmlBaseConverter.DateTimeToString((DateTime)value);
			}
			if (type == XmlBaseConverter.DateTimeOffsetType)
			{
				return XmlBaseConverter.DateTimeOffsetToString((DateTimeOffset)value);
			}
			if (type == XmlBaseConverter.DecimalType)
			{
				return XmlConvert.ToString((decimal)value);
			}
			if (type == XmlBaseConverter.DoubleType)
			{
				return XmlConvert.ToString((double)value);
			}
			if (type == XmlBaseConverter.Int16Type)
			{
				return XmlConvert.ToString((short)value);
			}
			if (type == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToString((int)value);
			}
			if (type == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToString((long)value);
			}
			if (type == XmlBaseConverter.SByteType)
			{
				return XmlConvert.ToString((sbyte)value);
			}
			if (type == XmlBaseConverter.SingleType)
			{
				return XmlConvert.ToString((float)value);
			}
			if (type == XmlBaseConverter.StringType)
			{
				return (string)value;
			}
			if (type == XmlBaseConverter.TimeSpanType)
			{
				return XmlBaseConverter.DurationToString((TimeSpan)value);
			}
			if (type == XmlBaseConverter.UInt16Type)
			{
				return XmlConvert.ToString((ushort)value);
			}
			if (type == XmlBaseConverter.UInt32Type)
			{
				return XmlConvert.ToString((uint)value);
			}
			if (type == XmlBaseConverter.UInt64Type)
			{
				return XmlConvert.ToString((ulong)value);
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.UriType))
			{
				return XmlBaseConverter.AnyUriToString((Uri)value);
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return (string)((XmlAtomicValue)value).ValueAs(XmlBaseConverter.StringType, nsResolver);
			}
			if (XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XmlQualifiedNameType))
			{
				return XmlBaseConverter.QNameToString((XmlQualifiedName)value, nsResolver);
			}
			return (string)this.ChangeTypeWildcardDestination(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x00112424 File Offset: 0x00110624
		public override object ChangeType(bool value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x0011247C File Offset: 0x0011067C
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DateTimeToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x001124D4 File Offset: 0x001106D4
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
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DateTimeOffsetToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0011252C File Offset: 0x0011072C
		public override object ChangeType(decimal value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00112584 File Offset: 0x00110784
		public override object ChangeType(double value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x001125E0 File Offset: 0x001107E0
		public override object ChangeType(int value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x00112638 File Offset: 0x00110838
		public override object ChangeType(long value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00112690 File Offset: 0x00110890
		public override object ChangeType(float value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToString(value);
			}
			return this.ChangeTypeWildcardSource(value, destinationType, null);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x001126EC File Offset: 0x001108EC
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
			if (destinationType == XmlBaseConverter.BooleanType)
			{
				return XmlConvert.ToBoolean(value);
			}
			if (destinationType == XmlBaseConverter.ByteType)
			{
				return XmlBaseConverter.Int32ToByte(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.ByteArrayType)
			{
				return XmlBaseConverter.StringToBase64Binary(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime(value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset(value);
			}
			if (destinationType == XmlBaseConverter.DecimalType)
			{
				return XmlConvert.ToDecimal(value);
			}
			if (destinationType == XmlBaseConverter.DoubleType)
			{
				return XmlConvert.ToDouble(value);
			}
			if (destinationType == XmlBaseConverter.Int16Type)
			{
				return XmlBaseConverter.Int32ToInt16(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.Int32Type)
			{
				return XmlConvert.ToInt32(value);
			}
			if (destinationType == XmlBaseConverter.Int64Type)
			{
				return XmlConvert.ToInt64(value);
			}
			if (destinationType == XmlBaseConverter.SByteType)
			{
				return XmlBaseConverter.Int32ToSByte(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.SingleType)
			{
				return XmlConvert.ToSingle(value);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType)
			{
				return XmlBaseConverter.StringToDuration(value);
			}
			if (destinationType == XmlBaseConverter.UInt16Type)
			{
				return XmlBaseConverter.Int32ToUInt16(XmlConvert.ToInt32(value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type)
			{
				return XmlBaseConverter.Int64ToUInt32(XmlConvert.ToInt64(value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type)
			{
				return XmlBaseConverter.DecimalToUInt64(XmlConvert.ToDecimal(value));
			}
			if (destinationType == XmlBaseConverter.UriType)
			{
				return XmlConvert.ToUri(value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType)
			{
				return XmlBaseConverter.StringToQName(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, value);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return value;
			}
			return this.ChangeTypeWildcardSource(value, destinationType, nsResolver);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00112950 File Offset: 0x00110B50
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
			if (destinationType == XmlBaseConverter.BooleanType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToBoolean((string)value);
			}
			if (destinationType == XmlBaseConverter.ByteType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToByte(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.ByteArrayType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToBase64Binary((string)value);
			}
			if (destinationType == XmlBaseConverter.DateTimeType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTime((string)value);
			}
			if (destinationType == XmlBaseConverter.DateTimeOffsetType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.UntypedAtomicToDateTimeOffset((string)value);
			}
			if (destinationType == XmlBaseConverter.DecimalType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDecimal((string)value);
			}
			if (destinationType == XmlBaseConverter.DoubleType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToDouble((string)value);
			}
			if (destinationType == XmlBaseConverter.Int16Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToInt16(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.Int32Type && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt32((string)value);
			}
			if (destinationType == XmlBaseConverter.Int64Type && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToInt64((string)value);
			}
			if (destinationType == XmlBaseConverter.SByteType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToSByte(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.SingleType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToSingle((string)value);
			}
			if (destinationType == XmlBaseConverter.TimeSpanType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToDuration((string)value);
			}
			if (destinationType == XmlBaseConverter.UInt16Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int32ToUInt16(XmlConvert.ToInt32((string)value));
			}
			if (destinationType == XmlBaseConverter.UInt32Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.Int64ToUInt32(XmlConvert.ToInt64((string)value));
			}
			if (destinationType == XmlBaseConverter.UInt64Type && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.DecimalToUInt64(XmlConvert.ToDecimal((string)value));
			}
			if (destinationType == XmlBaseConverter.UriType && type == XmlBaseConverter.StringType)
			{
				return XmlConvert.ToUri((string)value);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.XmlQualifiedNameType && type == XmlBaseConverter.StringType)
			{
				return XmlBaseConverter.StringToQName((string)value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return new XmlAtomicValue(base.SchemaType, (string)value);
				}
				if (type == XmlBaseConverter.XmlAtomicValueType)
				{
					return (XmlAtomicValue)value;
				}
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				return this.ToString(value, nsResolver);
			}
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (type == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00111CC5 File Offset: 0x0010FEC5
		private object ChangeTypeWildcardDestination(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value.GetType() == XmlBaseConverter.XmlAtomicValueType)
			{
				return ((XmlAtomicValue)value).ValueAs(destinationType, nsResolver);
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00112DB0 File Offset: 0x00110FB0
		private object ChangeTypeWildcardSource(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (destinationType == XmlBaseConverter.XmlAtomicValueType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			if (destinationType == XmlBaseConverter.XPathItemType)
			{
				return new XmlAtomicValue(base.SchemaType, this.ToString(value, nsResolver));
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00112E08 File Offset: 0x00111008
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (this.atomicConverter != null && (this.allowListToList || !(type != XmlBaseConverter.StringType) || !(destinationType != XmlBaseConverter.StringType)))
			{
				return base.ChangeListType(value, destinationType, nsResolver);
			}
			if (this.SupportsType(type))
			{
				throw new InvalidCastException(Res.GetString("Xml type '{0}' cannot convert from Clr type '{1}' unless the destination type is String or XmlAtomicValue.", new object[] { base.XmlTypeName, type.Name }));
			}
			if (this.SupportsType(destinationType))
			{
				throw new InvalidCastException(Res.GetString("Xml type '{0}' cannot convert to Clr type '{1}' unless the source value is a String or an XmlAtomicValue.", new object[] { base.XmlTypeName, destinationType.Name }));
			}
			throw base.CreateInvalidClrMappingException(type, destinationType);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00112EC0 File Offset: 0x001110C0
		private bool SupportsType(Type clrType)
		{
			return clrType == XmlBaseConverter.BooleanType || clrType == XmlBaseConverter.ByteType || clrType == XmlBaseConverter.ByteArrayType || clrType == XmlBaseConverter.DateTimeType || clrType == XmlBaseConverter.DateTimeOffsetType || clrType == XmlBaseConverter.DecimalType || clrType == XmlBaseConverter.DoubleType || clrType == XmlBaseConverter.Int16Type || clrType == XmlBaseConverter.Int32Type || clrType == XmlBaseConverter.Int64Type || clrType == XmlBaseConverter.SByteType || clrType == XmlBaseConverter.SingleType || clrType == XmlBaseConverter.TimeSpanType || clrType == XmlBaseConverter.UInt16Type || clrType == XmlBaseConverter.UInt32Type || clrType == XmlBaseConverter.UInt64Type || clrType == XmlBaseConverter.UriType || clrType == XmlBaseConverter.XmlQualifiedNameType;
		}

		// Token: 0x04001EE4 RID: 7908
		private bool allowListToList;

		// Token: 0x04001EE5 RID: 7909
		public static readonly XmlValueConverter Untyped = new XmlUntypedConverter(new XmlUntypedConverter(), false);

		// Token: 0x04001EE6 RID: 7910
		public static readonly XmlValueConverter UntypedList = new XmlUntypedConverter(new XmlUntypedConverter(), true);
	}
}
