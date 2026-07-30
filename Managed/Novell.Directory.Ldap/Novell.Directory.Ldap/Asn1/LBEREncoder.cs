using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000E1 RID: 225
	public class LBEREncoder : Asn1Encoder, ISerializable
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00017CE9 File Offset: 0x00015EE9
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00017CEB File Offset: 0x00015EEB
		public virtual void encode(Asn1Boolean b, Stream out_Renamed)
		{
			this.encode(b.getIdentifier(), out_Renamed);
			out_Renamed.WriteByte(1);
			out_Renamed.WriteByte((byte)(b.booleanValue() ? ((sbyte)SupportClass.Identity(255L)) : 0));
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017D20 File Offset: 0x00015F20
		public void encode(Asn1Numeric n, Stream out_Renamed)
		{
			sbyte[] array = new sbyte[8];
			long num = n.longValue();
			long num2 = ((num < 0L) ? (-1L) : 0L);
			long num3 = num2 & 128L;
			sbyte b = 0;
			while (b == 0 || num != num2 || (long)((int)array[(int)(b - 1)] & 128) != num3)
			{
				array[(int)b] = (sbyte)(num & 255L);
				num >>= 8;
				b += 1;
			}
			this.encode(n.getIdentifier(), out_Renamed);
			out_Renamed.WriteByte((byte)b);
			for (int i = (int)(b - 1); i >= 0; i--)
			{
				out_Renamed.WriteByte((byte)array[i]);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00017DB2 File Offset: 0x00015FB2
		public void encode(Asn1Null n, Stream out_Renamed)
		{
			this.encode(n.getIdentifier(), out_Renamed);
			out_Renamed.WriteByte(0);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017DC8 File Offset: 0x00015FC8
		public void encode(Asn1OctetString os, Stream out_Renamed)
		{
			this.encode(os.getIdentifier(), out_Renamed);
			this.encodeLength(os.byteValue().Length, out_Renamed);
			sbyte[] array = os.byteValue();
			out_Renamed.Write(SupportClass.ToByteArray(array), 0, array.Length);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017E08 File Offset: 0x00016008
		public void encode(Asn1Structured c, Stream out_Renamed)
		{
			this.encode(c.getIdentifier(), out_Renamed);
			Asn1Object[] array = c.toArray();
			MemoryStream memoryStream = new MemoryStream();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].encode(this, memoryStream);
			}
			this.encodeLength((int)memoryStream.Length, out_Renamed);
			sbyte[] array2 = SupportClass.ToSByteArray(memoryStream.ToArray());
			out_Renamed.Write(SupportClass.ToByteArray(array2), 0, array2.Length);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017E74 File Offset: 0x00016074
		public void encode(Asn1Tagged t, Stream out_Renamed)
		{
			if (t.Explicit)
			{
				this.encode(t.getIdentifier(), out_Renamed);
				MemoryStream memoryStream = new MemoryStream();
				t.taggedValue().encode(this, memoryStream);
				this.encodeLength((int)memoryStream.Length, out_Renamed);
				sbyte[] array = SupportClass.ToSByteArray(memoryStream.ToArray());
				out_Renamed.Write(SupportClass.ToByteArray(array), 0, array.Length);
				return;
			}
			t.taggedValue().encode(this, out_Renamed);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017EE4 File Offset: 0x000160E4
		public void encode(Asn1Identifier id, Stream out_Renamed)
		{
			int asn1Class = id.Asn1Class;
			int tag = id.Tag;
			sbyte b = (sbyte)((asn1Class << 6) | (id.Constructed ? 32 : 0));
			if (tag < 30)
			{
				out_Renamed.WriteByte((byte)((int)b | tag));
				return;
			}
			out_Renamed.WriteByte((byte)(b | 31));
			this.encodeTagInteger(tag, out_Renamed);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00017F34 File Offset: 0x00016134
		private void encodeLength(int length, Stream out_Renamed)
		{
			if (length < 128)
			{
				out_Renamed.WriteByte((byte)length);
				return;
			}
			sbyte[] array = new sbyte[4];
			sbyte b = 0;
			while (length != 0)
			{
				array[(int)b] = (sbyte)(length & 255);
				length >>= 8;
				b += 1;
			}
			out_Renamed.WriteByte((byte)(128 | (int)b));
			for (int i = (int)(b - 1); i >= 0; i--)
			{
				out_Renamed.WriteByte((byte)array[i]);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00017F9C File Offset: 0x0001619C
		private void encodeTagInteger(int value_Renamed, Stream out_Renamed)
		{
			sbyte[] array = new sbyte[5];
			int num = 0;
			while (value_Renamed != 0)
			{
				array[num] = (sbyte)(value_Renamed & 127);
				value_Renamed >>= 7;
				num++;
			}
			for (int i = num - 1; i > 0; i--)
			{
				out_Renamed.WriteByte((byte)((int)array[i] | 128));
			}
			out_Renamed.WriteByte((byte)array[0]);
		}
	}
}
