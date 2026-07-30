using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000302 RID: 770
	internal class XmlSerializationPrimitiveWriter : XmlSerializationWriter
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x0009BE61 File Offset: 0x0009A061
		internal void Write_string(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("string", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteral("string", "", (string)o);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0009BE99 File Offset: 0x0009A099
		internal void Write_int(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("int", "");
				return;
			}
			base.WriteElementStringRaw("int", "", XmlConvert.ToString((int)o));
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0009BED0 File Offset: 0x0009A0D0
		internal void Write_boolean(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("boolean", "");
				return;
			}
			base.WriteElementStringRaw("boolean", "", XmlConvert.ToString((bool)o));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0009BF07 File Offset: 0x0009A107
		internal void Write_short(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("short", "");
				return;
			}
			base.WriteElementStringRaw("short", "", XmlConvert.ToString((short)o));
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0009BF3E File Offset: 0x0009A13E
		internal void Write_long(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("long", "");
				return;
			}
			base.WriteElementStringRaw("long", "", XmlConvert.ToString((long)o));
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0009BF75 File Offset: 0x0009A175
		internal void Write_float(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("float", "");
				return;
			}
			base.WriteElementStringRaw("float", "", XmlConvert.ToString((float)o));
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0009BFAC File Offset: 0x0009A1AC
		internal void Write_double(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("double", "");
				return;
			}
			base.WriteElementStringRaw("double", "", XmlConvert.ToString((double)o));
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0009BFE3 File Offset: 0x0009A1E3
		internal void Write_decimal(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("decimal", "");
				return;
			}
			base.WriteElementStringRaw("decimal", "", XmlConvert.ToString((decimal)o));
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0009C01A File Offset: 0x0009A21A
		internal void Write_dateTime(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("dateTime", "");
				return;
			}
			base.WriteElementStringRaw("dateTime", "", XmlSerializationWriter.FromDateTime((DateTime)o));
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0009C051 File Offset: 0x0009A251
		internal void Write_unsignedByte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedByte", "");
				return;
			}
			base.WriteElementStringRaw("unsignedByte", "", XmlConvert.ToString((byte)o));
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0009C088 File Offset: 0x0009A288
		internal void Write_byte(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("byte", "");
				return;
			}
			base.WriteElementStringRaw("byte", "", XmlConvert.ToString((sbyte)o));
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0009C0BF File Offset: 0x0009A2BF
		internal void Write_unsignedShort(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedShort", "");
				return;
			}
			base.WriteElementStringRaw("unsignedShort", "", XmlConvert.ToString((ushort)o));
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0009C0F6 File Offset: 0x0009A2F6
		internal void Write_unsignedInt(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedInt", "");
				return;
			}
			base.WriteElementStringRaw("unsignedInt", "", XmlConvert.ToString((uint)o));
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0009C12D File Offset: 0x0009A32D
		internal void Write_unsignedLong(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("unsignedLong", "");
				return;
			}
			base.WriteElementStringRaw("unsignedLong", "", XmlConvert.ToString((ulong)o));
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0009C164 File Offset: 0x0009A364
		internal void Write_base64Binary(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("base64Binary", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableStringLiteralRaw("base64Binary", "", XmlSerializationWriter.FromByteArrayBase64((byte[])o));
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0009C1A1 File Offset: 0x0009A3A1
		internal void Write_guid(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("guid", "");
				return;
			}
			base.WriteElementStringRaw("guid", "", XmlConvert.ToString((Guid)o));
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0009C1D8 File Offset: 0x0009A3D8
		internal void Write_TimeSpan(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("TimeSpan", "");
				return;
			}
			TimeSpan timeSpan = (TimeSpan)o;
			base.WriteElementStringRaw("TimeSpan", "", XmlConvert.ToString(timeSpan));
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0009C21C File Offset: 0x0009A41C
		internal void Write_char(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("char", "");
				return;
			}
			base.WriteElementString("char", "", XmlSerializationWriter.FromChar((char)o));
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0009C253 File Offset: 0x0009A453
		internal void Write_QName(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("QName", "");
				return;
			}
			base.TopLevelElement();
			base.WriteNullableQualifiedNameLiteral("QName", "", (XmlQualifiedName)o);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00002F50 File Offset: 0x00001150
		protected override void InitCallbacks()
		{
		}
	}
}
