using System;

namespace System.Xml.Schema
{
	// Token: 0x020003F1 RID: 1009
	internal class Datatype_uuid : Datatype_anySimpleType
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000E56A7 File Offset: 0x000E38A7
		public override Type ValueType
		{
			get
			{
				return Datatype_uuid.atomicValueType;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000E56AE File Offset: 0x000E38AE
		internal override Type ListValueType
		{
			get
			{
				return Datatype_uuid.listValueType;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x0000226C File Offset: 0x0000046C
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000E56B8 File Offset: 0x000E38B8
		internal override int Compare(object value1, object value2)
		{
			if (!((Guid)value1).Equals(value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000E56E0 File Offset: 0x000E38E0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			try
			{
				obj = XmlConvert.ToGuid(s);
			}
			catch (XmlSchemaException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new XmlSchemaException(Res.GetString("The value '{0}' is invalid according to its data type.", new object[] { s }), ex2);
			}
			return obj;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000E5738 File Offset: 0x000E3938
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Guid guid;
			Exception ex = XmlConvert.TryToGuid(s, out guid);
			if (ex == null)
			{
				typedValue = guid;
				return null;
			}
			return ex;
		}

		// Token: 0x04001A1D RID: 6685
		private static readonly Type atomicValueType = typeof(Guid);

		// Token: 0x04001A1E RID: 6686
		private static readonly Type listValueType = typeof(Guid[]);
	}
}
