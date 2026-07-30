using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x02000167 RID: 359
	internal sealed class SqlCachedBuffer : INullable
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x00005C14 File Offset: 0x00003E14
		private SqlCachedBuffer()
		{
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000565F8 File Offset: 0x000547F8
		private SqlCachedBuffer(List<byte[]> cachedBytes)
		{
			this._cachedBytes = cachedBytes;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00056607 File Offset: 0x00054807
		internal List<byte[]> CachedBytes
		{
			get
			{
				return this._cachedBytes;
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00056610 File Offset: 0x00054810
		internal static bool TryCreate(SqlMetaDataPriv metadata, TdsParser parser, TdsParserStateObject stateObj, out SqlCachedBuffer buffer)
		{
			int num = 0;
			List<byte[]> list = new List<byte[]>();
			buffer = null;
			ulong num2;
			if (!parser.TryPlpBytesLeft(stateObj, out num2))
			{
				return false;
			}
			while (num2 != 0UL)
			{
				do
				{
					num = ((num2 > 2048UL) ? 2048 : ((int)num2));
					byte[] array = new byte[num];
					if (!stateObj.TryReadPlpBytes(ref array, 0, num, out num))
					{
						return false;
					}
					if (list.Count == 0)
					{
						SqlCachedBuffer.AddByteOrderMark(array, list);
					}
					list.Add(array);
					num2 -= (ulong)((long)num);
				}
				while (num2 > 0UL);
				if (!parser.TryPlpBytesLeft(stateObj, out num2))
				{
					return false;
				}
				if (num2 <= 0UL)
				{
					break;
				}
				continue;
			}
			buffer = new SqlCachedBuffer(list);
			return true;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0005669D File Offset: 0x0005489D
		private static void AddByteOrderMark(byte[] byteArr, List<byte[]> cachedBytes)
		{
			if (byteArr.Length < 2 || byteArr[0] != 223 || byteArr[1] != 255)
			{
				cachedBytes.Add(TdsEnums.XMLUNICODEBOMBYTES);
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000566C4 File Offset: 0x000548C4
		internal Stream ToStream()
		{
			return new SqlCachedStream(this);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000566CC File Offset: 0x000548CC
		public override string ToString()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._cachedBytes.Count == 0)
			{
				return string.Empty;
			}
			return new SqlXml(this.ToStream()).Value;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000566FF File Offset: 0x000548FF
		internal SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(this.ToString());
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0005671A File Offset: 0x0005491A
		internal SqlXml ToSqlXml()
		{
			return new SqlXml(this.ToStream());
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00056727 File Offset: 0x00054927
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal XmlReader ToXmlReader()
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this.ToStream(), false, false);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00056736 File Offset: 0x00054936
		public bool IsNull
		{
			get
			{
				return this._cachedBytes == null;
			}
		}

		// Token: 0x04000B5E RID: 2910
		public static readonly SqlCachedBuffer Null = new SqlCachedBuffer();

		// Token: 0x04000B5F RID: 2911
		private const int _maxChunkSize = 2048;

		// Token: 0x04000B60 RID: 2912
		private List<byte[]> _cachedBytes;
	}
}
