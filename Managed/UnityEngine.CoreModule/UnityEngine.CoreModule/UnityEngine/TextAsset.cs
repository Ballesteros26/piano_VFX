using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001BD RID: 445
	[NativeHeader("Runtime/Scripting/TextAsset.h")]
	public class TextAsset : Object
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060013FF RID: 5119
		public extern byte[] bytes
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001400 RID: 5120
		[MethodImpl(4096)]
		private static extern void Internal_CreateInstance([Writable] TextAsset self, string text);

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00020B18 File Offset: 0x0001ED18
		public string text
		{
			get
			{
				return TextAsset.DecodeString(this.bytes);
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00020B38 File Offset: 0x0001ED38
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00020B50 File Offset: 0x0001ED50
		public TextAsset()
			: this(TextAsset.CreateOptions.CreateNativeObject, null)
		{
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00020B5C File Offset: 0x0001ED5C
		public TextAsset(string text)
			: this(TextAsset.CreateOptions.CreateNativeObject, text)
		{
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00020B68 File Offset: 0x0001ED68
		internal TextAsset(TextAsset.CreateOptions options, string text)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstance(this, text);
			}
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00020B90 File Offset: 0x0001ED90
		internal static string DecodeString(byte[] bytes)
		{
			Encoding encoding = null;
			int num = 0;
			int num2 = TextAsset.EncodingUtility.encodingLookup.Length;
			int i = 0;
			while (i < num2)
			{
				byte[] key = TextAsset.EncodingUtility.encodingLookup[i].Key;
				num = key.Length;
				bool flag = bytes.Length >= num;
				if (flag)
				{
					for (int j = 0; j < num; j++)
					{
						bool flag2 = key[j] != bytes[j];
						if (flag2)
						{
							num = -1;
						}
					}
					bool flag3 = num < 0;
					if (!flag3)
					{
						try
						{
							Encoding value = TextAsset.EncodingUtility.encodingLookup[i].Value;
							string @string = value.GetString(bytes, num, bytes.Length - num);
							encoding = value;
							break;
						}
						catch
						{
						}
					}
				}
				IL_00A9:
				i++;
				continue;
				goto IL_00A9;
			}
			bool flag4 = encoding == null;
			if (flag4)
			{
				encoding = TextAsset.EncodingUtility.targetEncoding;
				num = 0;
			}
			return encoding.GetString(bytes, num, bytes.Length - num);
		}

		// Token: 0x020001BE RID: 446
		internal enum CreateOptions
		{
			// Token: 0x04000663 RID: 1635
			None,
			// Token: 0x04000664 RID: 1636
			CreateNativeObject
		}

		// Token: 0x020001BF RID: 447
		private static class EncodingUtility
		{
			// Token: 0x06001407 RID: 5127 RVA: 0x00020C90 File Offset: 0x0001EE90
			static EncodingUtility()
			{
				Encoding encoding = new UTF32Encoding(true, true, true);
				Encoding encoding2 = new UTF32Encoding(false, true, true);
				Encoding encoding3 = new UnicodeEncoding(true, true, true);
				Encoding encoding4 = new UnicodeEncoding(false, true, true);
				Encoding encoding5 = new UTF8Encoding(true, true);
				TextAsset.EncodingUtility.encodingLookup = new KeyValuePair<byte[], Encoding>[]
				{
					new KeyValuePair<byte[], Encoding>(encoding.GetPreamble(), encoding),
					new KeyValuePair<byte[], Encoding>(encoding2.GetPreamble(), encoding2),
					new KeyValuePair<byte[], Encoding>(encoding3.GetPreamble(), encoding3),
					new KeyValuePair<byte[], Encoding>(encoding4.GetPreamble(), encoding4),
					new KeyValuePair<byte[], Encoding>(encoding5.GetPreamble(), encoding5)
				};
			}

			// Token: 0x04000665 RID: 1637
			internal static readonly KeyValuePair<byte[], Encoding>[] encodingLookup;

			// Token: 0x04000666 RID: 1638
			internal static readonly Encoding targetEncoding = Encoding.GetEncoding(Encoding.UTF8.CodePage, new EncoderReplacementFallback("\ufffd"), new DecoderReplacementFallback("\ufffd"));
		}
	}
}
