using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public abstract class Asn1Object : ISerializable
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x000173F1 File Offset: 0x000155F1
		public Asn1Object(Asn1Identifier id)
		{
			this.id = id;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00017400 File Offset: 0x00015600
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000551 RID: 1361
		public abstract void encode(Asn1Encoder enc, Stream out_Renamed);

		// Token: 0x06000552 RID: 1362 RVA: 0x00017402 File Offset: 0x00015602
		public virtual Asn1Identifier getIdentifier()
		{
			return this.id;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001740A File Offset: 0x0001560A
		public virtual void setIdentifier(Asn1Identifier id)
		{
			this.id = id;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00017414 File Offset: 0x00015614
		[CLSCompliant(false)]
		public sbyte[] getEncoding(Asn1Encoder enc)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				this.encode(enc, memoryStream);
			}
			catch (IOException ex)
			{
				throw new SystemException("IOException while encoding to byte array: " + ex.ToString());
			}
			return SupportClass.ToSByteArray(memoryStream.ToArray());
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00017464 File Offset: 0x00015664
		[CLSCompliant(false)]
		public override string ToString()
		{
			string[] array = new string[] { "[UNIVERSAL ", "[APPLICATION ", "[CONTEXT ", "[PRIVATE " };
			StringBuilder stringBuilder = new StringBuilder();
			Asn1Identifier identifier = this.getIdentifier();
			stringBuilder.Append(array[identifier.Asn1Class]).Append(identifier.Tag).Append("] ");
			return stringBuilder.ToString();
		}

		// Token: 0x040004AE RID: 1198
		private Asn1Identifier id;
	}
}
