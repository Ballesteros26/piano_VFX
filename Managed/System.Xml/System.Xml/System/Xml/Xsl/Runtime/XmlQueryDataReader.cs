using System;
using System.IO;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000614 RID: 1556
	internal class XmlQueryDataReader : BinaryReader
	{
		// Token: 0x06003D1E RID: 15646 RVA: 0x0015311C File Offset: 0x0015131C
		public XmlQueryDataReader(Stream input)
			: base(input)
		{
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x00153125 File Offset: 0x00151325
		public int ReadInt32Encoded()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x0015312D File Offset: 0x0015132D
		public string ReadStringQ()
		{
			if (!this.ReadBoolean())
			{
				return null;
			}
			return this.ReadString();
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x00153140 File Offset: 0x00151340
		public sbyte ReadSByte(sbyte minValue, sbyte maxValue)
		{
			sbyte b = this.ReadSByte();
			if (b < minValue)
			{
				throw new ArgumentOutOfRangeException("minValue");
			}
			if (maxValue < b)
			{
				throw new ArgumentOutOfRangeException("maxValue");
			}
			return b;
		}
	}
}
