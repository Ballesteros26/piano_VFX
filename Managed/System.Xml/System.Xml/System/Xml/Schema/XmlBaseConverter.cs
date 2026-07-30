using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000493 RID: 1171
	internal abstract class XmlBaseConverter : XmlValueConverter
	{
		// Token: 0x06002E80 RID: 11904 RVA: 0x0010E41C File Offset: 0x0010C61C
		protected XmlBaseConverter(XmlSchemaType schemaType)
		{
			XmlSchemaDatatype datatype = schemaType.Datatype;
			while (schemaType != null && !(schemaType is XmlSchemaSimpleType))
			{
				schemaType = schemaType.BaseXmlSchemaType;
			}
			if (schemaType == null)
			{
				schemaType = XmlSchemaType.GetBuiltInSimpleType(datatype.TypeCode);
			}
			this.schemaType = schemaType;
			this.typeCode = schemaType.TypeCode;
			this.clrTypeDefault = schemaType.Datatype.ValueType;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0010E480 File Offset: 0x0010C680
		protected XmlBaseConverter(XmlTypeCode typeCode)
		{
			if (typeCode != XmlTypeCode.Item)
			{
				if (typeCode != XmlTypeCode.Node)
				{
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						this.clrTypeDefault = XmlBaseConverter.XmlAtomicValueType;
					}
				}
				else
				{
					this.clrTypeDefault = XmlBaseConverter.XPathNavigatorType;
				}
			}
			else
			{
				this.clrTypeDefault = XmlBaseConverter.XPathItemType;
			}
			this.typeCode = typeCode;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x0010E4CE File Offset: 0x0010C6CE
		protected XmlBaseConverter(XmlBaseConverter converterAtomic)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = Array.CreateInstance(converterAtomic.DefaultClrType, 0).GetType();
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x0010E505 File Offset: 0x0010C705
		protected XmlBaseConverter(XmlBaseConverter converterAtomic, Type clrTypeDefault)
		{
			this.schemaType = converterAtomic.schemaType;
			this.typeCode = converterAtomic.typeCode;
			this.clrTypeDefault = clrTypeDefault;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x0010E52C File Offset: 0x0010C72C
		public override bool ToBoolean(bool value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0010E545 File Offset: 0x0010C745
		public override bool ToBoolean(DateTime value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0010E55E File Offset: 0x0010C75E
		public override bool ToBoolean(DateTimeOffset value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x0010E577 File Offset: 0x0010C777
		public override bool ToBoolean(decimal value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x0010E590 File Offset: 0x0010C790
		public override bool ToBoolean(double value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0010E5A9 File Offset: 0x0010C7A9
		public override bool ToBoolean(int value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0010E5C2 File Offset: 0x0010C7C2
		public override bool ToBoolean(long value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0010E5DB File Offset: 0x0010C7DB
		public override bool ToBoolean(float value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0010E5F4 File Offset: 0x0010C7F4
		public override bool ToBoolean(string value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x0010E5F4 File Offset: 0x0010C7F4
		public override bool ToBoolean(object value)
		{
			return (bool)this.ChangeType(value, XmlBaseConverter.BooleanType, null);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x0010E608 File Offset: 0x0010C808
		public override DateTime ToDateTime(bool value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0010E621 File Offset: 0x0010C821
		public override DateTime ToDateTime(DateTime value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x0010E63A File Offset: 0x0010C83A
		public override DateTime ToDateTime(DateTimeOffset value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x0010E653 File Offset: 0x0010C853
		public override DateTime ToDateTime(decimal value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0010E66C File Offset: 0x0010C86C
		public override DateTime ToDateTime(double value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x0010E685 File Offset: 0x0010C885
		public override DateTime ToDateTime(int value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0010E69E File Offset: 0x0010C89E
		public override DateTime ToDateTime(long value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x0010E6B7 File Offset: 0x0010C8B7
		public override DateTime ToDateTime(float value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x0010E6D0 File Offset: 0x0010C8D0
		public override DateTime ToDateTime(string value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x0010E6D0 File Offset: 0x0010C8D0
		public override DateTime ToDateTime(object value)
		{
			return (DateTime)this.ChangeType(value, XmlBaseConverter.DateTimeType, null);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x0010E6E4 File Offset: 0x0010C8E4
		public override DateTimeOffset ToDateTimeOffset(bool value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0010E6FD File Offset: 0x0010C8FD
		public override DateTimeOffset ToDateTimeOffset(DateTime value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0010E716 File Offset: 0x0010C916
		public override DateTimeOffset ToDateTimeOffset(DateTimeOffset value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0010E72F File Offset: 0x0010C92F
		public override DateTimeOffset ToDateTimeOffset(decimal value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x0010E748 File Offset: 0x0010C948
		public override DateTimeOffset ToDateTimeOffset(double value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0010E761 File Offset: 0x0010C961
		public override DateTimeOffset ToDateTimeOffset(int value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x0010E77A File Offset: 0x0010C97A
		public override DateTimeOffset ToDateTimeOffset(long value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x0010E793 File Offset: 0x0010C993
		public override DateTimeOffset ToDateTimeOffset(float value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x0010E7AC File Offset: 0x0010C9AC
		public override DateTimeOffset ToDateTimeOffset(string value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x0010E7AC File Offset: 0x0010C9AC
		public override DateTimeOffset ToDateTimeOffset(object value)
		{
			return (DateTimeOffset)this.ChangeType(value, XmlBaseConverter.DateTimeOffsetType, null);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0010E7C0 File Offset: 0x0010C9C0
		public override decimal ToDecimal(bool value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0010E7D9 File Offset: 0x0010C9D9
		public override decimal ToDecimal(DateTime value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x0010E7F2 File Offset: 0x0010C9F2
		public override decimal ToDecimal(DateTimeOffset value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x0010E80B File Offset: 0x0010CA0B
		public override decimal ToDecimal(decimal value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0010E824 File Offset: 0x0010CA24
		public override decimal ToDecimal(double value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0010E83D File Offset: 0x0010CA3D
		public override decimal ToDecimal(int value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0010E856 File Offset: 0x0010CA56
		public override decimal ToDecimal(long value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x0010E86F File Offset: 0x0010CA6F
		public override decimal ToDecimal(float value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0010E888 File Offset: 0x0010CA88
		public override decimal ToDecimal(string value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0010E888 File Offset: 0x0010CA88
		public override decimal ToDecimal(object value)
		{
			return (decimal)this.ChangeType(value, XmlBaseConverter.DecimalType, null);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0010E89C File Offset: 0x0010CA9C
		public override double ToDouble(bool value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0010E8B5 File Offset: 0x0010CAB5
		public override double ToDouble(DateTime value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x0010E8CE File Offset: 0x0010CACE
		public override double ToDouble(DateTimeOffset value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0010E8E7 File Offset: 0x0010CAE7
		public override double ToDouble(decimal value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x0010E900 File Offset: 0x0010CB00
		public override double ToDouble(double value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x0010E919 File Offset: 0x0010CB19
		public override double ToDouble(int value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x0010E932 File Offset: 0x0010CB32
		public override double ToDouble(long value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x0010E94B File Offset: 0x0010CB4B
		public override double ToDouble(float value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x0010E964 File Offset: 0x0010CB64
		public override double ToDouble(string value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x0010E964 File Offset: 0x0010CB64
		public override double ToDouble(object value)
		{
			return (double)this.ChangeType(value, XmlBaseConverter.DoubleType, null);
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0010E978 File Offset: 0x0010CB78
		public override int ToInt32(bool value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0010E991 File Offset: 0x0010CB91
		public override int ToInt32(DateTime value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0010E9AA File Offset: 0x0010CBAA
		public override int ToInt32(DateTimeOffset value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0010E9C3 File Offset: 0x0010CBC3
		public override int ToInt32(decimal value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0010E9DC File Offset: 0x0010CBDC
		public override int ToInt32(double value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x0010E9F5 File Offset: 0x0010CBF5
		public override int ToInt32(int value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0010EA0E File Offset: 0x0010CC0E
		public override int ToInt32(long value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0010EA27 File Offset: 0x0010CC27
		public override int ToInt32(float value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0010EA40 File Offset: 0x0010CC40
		public override int ToInt32(string value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0010EA40 File Offset: 0x0010CC40
		public override int ToInt32(object value)
		{
			return (int)this.ChangeType(value, XmlBaseConverter.Int32Type, null);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0010EA54 File Offset: 0x0010CC54
		public override long ToInt64(bool value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0010EA6D File Offset: 0x0010CC6D
		public override long ToInt64(DateTime value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0010EA86 File Offset: 0x0010CC86
		public override long ToInt64(DateTimeOffset value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0010EA9F File Offset: 0x0010CC9F
		public override long ToInt64(decimal value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0010EAB8 File Offset: 0x0010CCB8
		public override long ToInt64(double value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x0010EAD1 File Offset: 0x0010CCD1
		public override long ToInt64(int value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0010EAEA File Offset: 0x0010CCEA
		public override long ToInt64(long value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0010EB03 File Offset: 0x0010CD03
		public override long ToInt64(float value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0010EB1C File Offset: 0x0010CD1C
		public override long ToInt64(string value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0010EB1C File Offset: 0x0010CD1C
		public override long ToInt64(object value)
		{
			return (long)this.ChangeType(value, XmlBaseConverter.Int64Type, null);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0010EB30 File Offset: 0x0010CD30
		public override float ToSingle(bool value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0010EB49 File Offset: 0x0010CD49
		public override float ToSingle(DateTime value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0010EB62 File Offset: 0x0010CD62
		public override float ToSingle(DateTimeOffset value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0010EB7B File Offset: 0x0010CD7B
		public override float ToSingle(decimal value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0010EB94 File Offset: 0x0010CD94
		public override float ToSingle(double value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0010EBAD File Offset: 0x0010CDAD
		public override float ToSingle(int value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x0010EBC6 File Offset: 0x0010CDC6
		public override float ToSingle(long value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x0010EBDF File Offset: 0x0010CDDF
		public override float ToSingle(float value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x0010EBF8 File Offset: 0x0010CDF8
		public override float ToSingle(string value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x0010EBF8 File Offset: 0x0010CDF8
		public override float ToSingle(object value)
		{
			return (float)this.ChangeType(value, XmlBaseConverter.SingleType, null);
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x0010EC0C File Offset: 0x0010CE0C
		public override string ToString(bool value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0010EC25 File Offset: 0x0010CE25
		public override string ToString(DateTime value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x0010EC3E File Offset: 0x0010CE3E
		public override string ToString(DateTimeOffset value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x0010EC57 File Offset: 0x0010CE57
		public override string ToString(decimal value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0010EC70 File Offset: 0x0010CE70
		public override string ToString(double value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0010EC89 File Offset: 0x0010CE89
		public override string ToString(int value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x0010ECA2 File Offset: 0x0010CEA2
		public override string ToString(long value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x0010ECBB File Offset: 0x0010CEBB
		public override string ToString(float value)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, null);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x0010ECD4 File Offset: 0x0010CED4
		public override string ToString(string value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x0010ECD4 File Offset: 0x0010CED4
		public override string ToString(object value, IXmlNamespaceResolver nsResolver)
		{
			return (string)this.ChangeType(value, XmlBaseConverter.StringType, nsResolver);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x0010ECE8 File Offset: 0x0010CEE8
		public override string ToString(string value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x0010ECF2 File Offset: 0x0010CEF2
		public override string ToString(object value)
		{
			return this.ToString(value, null);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x0010ECFC File Offset: 0x0010CEFC
		public override object ChangeType(bool value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x0010ED0C File Offset: 0x0010CF0C
		public override object ChangeType(DateTime value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x0010ED1C File Offset: 0x0010CF1C
		public override object ChangeType(DateTimeOffset value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x0010ED2C File Offset: 0x0010CF2C
		public override object ChangeType(decimal value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x0010ED3C File Offset: 0x0010CF3C
		public override object ChangeType(double value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x0010ED4C File Offset: 0x0010CF4C
		public override object ChangeType(int value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x0010ED5C File Offset: 0x0010CF5C
		public override object ChangeType(long value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x0010ED6C File Offset: 0x0010CF6C
		public override object ChangeType(float value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x0010ED7C File Offset: 0x0010CF7C
		public override object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			return this.ChangeType(value, destinationType, nsResolver);
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x0010ED87 File Offset: 0x0010CF87
		public override object ChangeType(string value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x0010ED92 File Offset: 0x0010CF92
		public override object ChangeType(object value, Type destinationType)
		{
			return this.ChangeType(value, destinationType, null);
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x0010ED9D File Offset: 0x0010CF9D
		protected XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x0010EDA5 File Offset: 0x0010CFA5
		protected XmlTypeCode TypeCode
		{
			get
			{
				return this.typeCode;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x0010EDB0 File Offset: 0x0010CFB0
		protected string XmlTypeName
		{
			get
			{
				XmlSchemaType baseXmlSchemaType = this.schemaType;
				if (baseXmlSchemaType != null)
				{
					while (baseXmlSchemaType.QualifiedName.IsEmpty)
					{
						baseXmlSchemaType = baseXmlSchemaType.BaseXmlSchemaType;
					}
					return XmlBaseConverter.QNameToString(baseXmlSchemaType.QualifiedName);
				}
				if (this.typeCode == XmlTypeCode.Node)
				{
					return "node";
				}
				if (this.typeCode == XmlTypeCode.AnyAtomicType)
				{
					return "xdt:anyAtomicType";
				}
				return "item";
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x0010EE0D File Offset: 0x0010D00D
		protected Type DefaultClrType
		{
			get
			{
				return this.clrTypeDefault;
			}
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x0010EE15 File Offset: 0x0010D015
		protected static bool IsDerivedFrom(Type derivedType, Type baseType)
		{
			while (derivedType != null)
			{
				if (derivedType == baseType)
				{
					return true;
				}
				derivedType = derivedType.BaseType;
			}
			return false;
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x0010EE38 File Offset: 0x0010D038
		protected Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("Xml type '{0}' does not support Clr type '{1}'.", new object[] { this.XmlTypeName, sourceType.Name }));
			}
			return new InvalidCastException(Res.GetString("Xml type '{0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.", new object[] { this.XmlTypeName, sourceType.Name, destinationType.Name }));
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x0010EEA8 File Offset: 0x0010D0A8
		protected static string QNameToString(XmlQualifiedName name)
		{
			if (name.Namespace.Length == 0)
			{
				return name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return "xs:" + name.Name;
			}
			if (name.Namespace == "http://www.w3.org/2003/11/xpath-datatypes")
			{
				return "xdt:" + name.Name;
			}
			return "{" + name.Namespace + "}" + name.Name;
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x0010EF2A File Offset: 0x0010D12A
		protected virtual object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			throw this.CreateInvalidClrMappingException(value.GetType(), destinationType);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x0010EF39 File Offset: 0x0010D139
		protected static byte[] StringToBase64Binary(string value)
		{
			return Convert.FromBase64String(XmlConvert.TrimString(value));
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x0010EF46 File Offset: 0x0010D146
		protected static DateTime StringToDate(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0010EF54 File Offset: 0x0010D154
		protected static DateTime StringToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x0010EF64 File Offset: 0x0010D164
		protected static TimeSpan StringToDayTimeDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToTimeSpan(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0010EF84 File Offset: 0x0010D184
		protected static TimeSpan StringToDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToTimeSpan(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0010EFA1 File Offset: 0x0010D1A1
		protected static DateTime StringToGDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0010EFB0 File Offset: 0x0010D1B0
		protected static DateTime StringToGMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0010EFC2 File Offset: 0x0010D1C2
		protected static DateTime StringToGMonthDay(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x0010EFD1 File Offset: 0x0010D1D1
		protected static DateTime StringToGYear(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x0010EFE0 File Offset: 0x0010D1E0
		protected static DateTime StringToGYearMonth(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x0010EFEE File Offset: 0x0010D1EE
		protected static DateTimeOffset StringToDateOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0010EFFC File Offset: 0x0010D1FC
		protected static DateTimeOffset StringToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0010F00A File Offset: 0x0010D20A
		protected static DateTimeOffset StringToGDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0010F019 File Offset: 0x0010D219
		protected static DateTimeOffset StringToGMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0010F02B File Offset: 0x0010D22B
		protected static DateTimeOffset StringToGMonthDayOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x0010F03A File Offset: 0x0010D23A
		protected static DateTimeOffset StringToGYearOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0010F049 File Offset: 0x0010D249
		protected static DateTimeOffset StringToGYearMonthOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0010F058 File Offset: 0x0010D258
		protected static byte[] StringToHexBinary(string value)
		{
			byte[] array;
			try
			{
				array = XmlConvert.FromBinHexString(XmlConvert.TrimString(value), false);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			return array;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0010F090 File Offset: 0x0010D290
		protected static XmlQualifiedName StringToQName(string value, IXmlNamespaceResolver nsResolver)
		{
			value = value.Trim();
			string text;
			string text2;
			try
			{
				ValidateNames.ParseQNameThrow(value, out text, out text2);
			}
			catch (XmlException ex)
			{
				throw new FormatException(ex.Message);
			}
			if (nsResolver == null)
			{
				throw new InvalidCastException(Res.GetString("The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.", new object[] { value, text }));
			}
			string text3 = nsResolver.LookupNamespace(text);
			if (text3 == null)
			{
				throw new InvalidCastException(Res.GetString("The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.", new object[] { value, text }));
			}
			return new XmlQualifiedName(text2, text3);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0010F120 File Offset: 0x0010D320
		protected static DateTime StringToTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0010F12E File Offset: 0x0010D32E
		protected static DateTimeOffset StringToTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0010F13C File Offset: 0x0010D33C
		protected static TimeSpan StringToYearMonthDuration(string value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToTimeSpan(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0010F159 File Offset: 0x0010D359
		protected static string AnyUriToString(Uri value)
		{
			return value.OriginalString;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x0010F161 File Offset: 0x0010D361
		protected static string Base64BinaryToString(byte[] value)
		{
			return Convert.ToBase64String(value);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x0010F16C File Offset: 0x0010D36C
		protected static string DateToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x0010F190 File Offset: 0x0010D390
		protected static string DateTimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0010F1B4 File Offset: 0x0010D3B4
		protected static string DayTimeDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.DayTimeDuration).ToString(XsdDuration.DurationType.DayTimeDuration);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0010F1D4 File Offset: 0x0010D3D4
		protected static string DurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.Duration).ToString(XsdDuration.DurationType.Duration);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0010F1F4 File Offset: 0x0010D3F4
		protected static string GDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0010F218 File Offset: 0x0010D418
		protected static string GMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0010F240 File Offset: 0x0010D440
		protected static string GMonthDayToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x0010F264 File Offset: 0x0010D464
		protected static string GYearToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0010F288 File Offset: 0x0010D488
		protected static string GYearMonthToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0010F2AC File Offset: 0x0010D4AC
		protected static string DateOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Date).ToString();
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0010F2D0 File Offset: 0x0010D4D0
		protected static string DateTimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x0010F2F4 File Offset: 0x0010D4F4
		protected static string GDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GDay).ToString();
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0010F318 File Offset: 0x0010D518
		protected static string GMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonth).ToString();
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0010F340 File Offset: 0x0010D540
		protected static string GMonthDayOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GMonthDay).ToString();
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0010F364 File Offset: 0x0010D564
		protected static string GYearOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYear).ToString();
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0010F388 File Offset: 0x0010D588
		protected static string GYearMonthOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.GYearMonth).ToString();
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0010F3AC File Offset: 0x0010D5AC
		protected static string QNameToString(XmlQualifiedName qname, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				return "{" + qname.Namespace + "}" + qname.Name;
			}
			string text = nsResolver.LookupPrefix(qname.Namespace);
			if (text == null)
			{
				throw new InvalidCastException(Res.GetString("The QName '{0}' cannot be represented as a String.  A prefix for namespace '{1}' cannot be found.", new object[]
				{
					qname.ToString(),
					qname.Namespace
				}));
			}
			if (text.Length == 0)
			{
				return qname.Name;
			}
			return text + ":" + qname.Name;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x0010F430 File Offset: 0x0010D630
		protected static string TimeToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x0010F454 File Offset: 0x0010D654
		protected static string TimeOffsetToString(DateTimeOffset value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.Time).ToString();
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x0010F478 File Offset: 0x0010D678
		protected static string YearMonthDurationToString(TimeSpan value)
		{
			return new XsdDuration(value, XsdDuration.DurationType.YearMonthDuration).ToString(XsdDuration.DurationType.YearMonthDuration);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x0010F495 File Offset: 0x0010D695
		internal static DateTime DateTimeOffsetToDateTime(DateTimeOffset value)
		{
			return value.LocalDateTime;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x0010F4A0 File Offset: 0x0010D6A0
		internal static int DecimalToInt32(decimal value)
		{
			if (value < -2147483648m || value > 2147483647m)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				}));
			}
			return (int)value;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0010F500 File Offset: 0x0010D700
		protected static long DecimalToInt64(decimal value)
		{
			if (value < -9223372036854775808m || value > 9223372036854775807m)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"Int64"
				}));
			}
			return (long)value;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x0010F568 File Offset: 0x0010D768
		protected static ulong DecimalToUInt64(decimal value)
		{
			if (value < 0m || value > 18446744073709551615m)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"UInt64"
				}));
			}
			return (ulong)value;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0010F5BE File Offset: 0x0010D7BE
		protected static byte Int32ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"Byte"
				}));
			}
			return (byte)value;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x0010F5F5 File Offset: 0x0010D7F5
		protected static short Int32ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"Int16"
				}));
			}
			return (short)value;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0010F630 File Offset: 0x0010D830
		protected static sbyte Int32ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"SByte"
				}));
			}
			return (sbyte)value;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0010F665 File Offset: 0x0010D865
		protected static ushort Int32ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"UInt16"
				}));
			}
			return (ushort)value;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x0010F69C File Offset: 0x0010D89C
		protected static int Int64ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"Int32"
				}));
			}
			return (int)value;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0010F6D9 File Offset: 0x0010D8D9
		protected static uint Int64ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)(-1)))
			{
				throw new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new string[]
				{
					XmlConvert.ToString(value),
					"UInt32"
				}));
			}
			return (uint)value;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0010F70E File Offset: 0x0010D90E
		protected static DateTime UntypedAtomicToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x0010F720 File Offset: 0x0010D920
		protected static DateTimeOffset UntypedAtomicToDateTimeOffset(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x04001EC4 RID: 7876
		private XmlSchemaType schemaType;

		// Token: 0x04001EC5 RID: 7877
		private XmlTypeCode typeCode;

		// Token: 0x04001EC6 RID: 7878
		private Type clrTypeDefault;

		// Token: 0x04001EC7 RID: 7879
		protected static readonly Type ICollectionType = typeof(ICollection);

		// Token: 0x04001EC8 RID: 7880
		protected static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x04001EC9 RID: 7881
		protected static readonly Type IListType = typeof(IList);

		// Token: 0x04001ECA RID: 7882
		protected static readonly Type ObjectArrayType = typeof(object[]);

		// Token: 0x04001ECB RID: 7883
		protected static readonly Type StringArrayType = typeof(string[]);

		// Token: 0x04001ECC RID: 7884
		protected static readonly Type XmlAtomicValueArrayType = typeof(XmlAtomicValue[]);

		// Token: 0x04001ECD RID: 7885
		protected static readonly Type DecimalType = typeof(decimal);

		// Token: 0x04001ECE RID: 7886
		protected static readonly Type Int32Type = typeof(int);

		// Token: 0x04001ECF RID: 7887
		protected static readonly Type Int64Type = typeof(long);

		// Token: 0x04001ED0 RID: 7888
		protected static readonly Type StringType = typeof(string);

		// Token: 0x04001ED1 RID: 7889
		protected static readonly Type XmlAtomicValueType = typeof(XmlAtomicValue);

		// Token: 0x04001ED2 RID: 7890
		protected static readonly Type ObjectType = typeof(object);

		// Token: 0x04001ED3 RID: 7891
		protected static readonly Type ByteType = typeof(byte);

		// Token: 0x04001ED4 RID: 7892
		protected static readonly Type Int16Type = typeof(short);

		// Token: 0x04001ED5 RID: 7893
		protected static readonly Type SByteType = typeof(sbyte);

		// Token: 0x04001ED6 RID: 7894
		protected static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x04001ED7 RID: 7895
		protected static readonly Type UInt32Type = typeof(uint);

		// Token: 0x04001ED8 RID: 7896
		protected static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x04001ED9 RID: 7897
		protected static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x04001EDA RID: 7898
		protected static readonly Type DoubleType = typeof(double);

		// Token: 0x04001EDB RID: 7899
		protected static readonly Type SingleType = typeof(float);

		// Token: 0x04001EDC RID: 7900
		protected static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x04001EDD RID: 7901
		protected static readonly Type DateTimeOffsetType = typeof(DateTimeOffset);

		// Token: 0x04001EDE RID: 7902
		protected static readonly Type BooleanType = typeof(bool);

		// Token: 0x04001EDF RID: 7903
		protected static readonly Type ByteArrayType = typeof(byte[]);

		// Token: 0x04001EE0 RID: 7904
		protected static readonly Type XmlQualifiedNameType = typeof(XmlQualifiedName);

		// Token: 0x04001EE1 RID: 7905
		protected static readonly Type UriType = typeof(Uri);

		// Token: 0x04001EE2 RID: 7906
		protected static readonly Type TimeSpanType = typeof(TimeSpan);

		// Token: 0x04001EE3 RID: 7907
		protected static readonly Type XPathNavigatorType = typeof(XPathNavigator);
	}
}
