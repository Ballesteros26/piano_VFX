using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005FD RID: 1533
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlCollation
	{
		// Token: 0x06003BB9 RID: 15289 RVA: 0x0014ECB6 File Offset: 0x0014CEB6
		private XmlCollation(CultureInfo cultureInfo, XmlCollation.Options options)
		{
			this.cultInfo = cultureInfo;
			this.options = options;
			this.compops = options.CompareOptions;
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x0014ECD9 File Offset: 0x0014CED9
		internal static XmlCollation CodePointCollation
		{
			get
			{
				return XmlCollation.cp;
			}
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x0014ECE0 File Offset: 0x0014CEE0
		internal static XmlCollation Create(string collationLiteral)
		{
			return XmlCollation.Create(collationLiteral, true);
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x0014ECEC File Offset: 0x0014CEEC
		internal static XmlCollation Create(string collationLiteral, bool throwOnError)
		{
			if (collationLiteral == "http://www.w3.org/2004/10/xpath-functions/collation/codepoint")
			{
				return XmlCollation.CodePointCollation;
			}
			CultureInfo cultureInfo = null;
			XmlCollation.Options options = default(XmlCollation.Options);
			Uri uri;
			if (throwOnError)
			{
				uri = new Uri(collationLiteral);
			}
			else if (!Uri.TryCreate(collationLiteral, UriKind.Absolute, out uri))
			{
				return null;
			}
			if (uri.GetLeftPart(UriPartial.Authority) == "http://collations.microsoft.com")
			{
				string text = uri.LocalPath.Substring(1);
				if (text.Length == 0)
				{
					goto IL_00C7;
				}
				try
				{
					cultureInfo = new CultureInfo(text);
					goto IL_00C7;
				}
				catch (ArgumentException)
				{
					if (!throwOnError)
					{
						return null;
					}
					throw new XslTransformException("Collation language '{0}' is not supported.", new string[] { text });
				}
			}
			if (uri.IsBaseOf(new Uri("http://www.w3.org/2004/10/xpath-functions/collation/codepoint")))
			{
				options.CompareOptions = CompareOptions.Ordinal;
			}
			else
			{
				if (!throwOnError)
				{
					return null;
				}
				throw new XslTransformException("The collation '{0}' is not supported.", new string[] { collationLiteral });
			}
			IL_00C7:
			string query = uri.Query;
			string text2 = null;
			if (query.Length != 0)
			{
				string[] array = query.Substring(1).Split(new char[] { '&' });
				int i = 0;
				while (i < array.Length)
				{
					string text3 = array[i];
					string[] array2 = text3.Split(new char[] { '=' });
					if (array2.Length != 2)
					{
						if (!throwOnError)
						{
							return null;
						}
						throw new XslTransformException("Collation option '{0}' is invalid. Options must have the following format: <option-name>=<option-value>.", new string[] { text3 });
					}
					else
					{
						string text4 = array2[0].ToUpper(CultureInfo.InvariantCulture);
						string text5 = array2[1].ToUpper(CultureInfo.InvariantCulture);
						if (text4 == "SORT")
						{
							text2 = text5;
						}
						else
						{
							uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
							int num2;
							if (num <= 1153929311U)
							{
								if (num <= 399689514U)
								{
									if (num != 346004547U)
									{
										if (num != 399689514U)
										{
											goto IL_02BB;
										}
										if (!(text4 == "IGNOREKANATYPE"))
										{
											goto IL_02BB;
										}
										num2 = 8;
									}
									else
									{
										if (!(text4 == "UPPERFIRST"))
										{
											goto IL_02BB;
										}
										num2 = 4096;
									}
								}
								else if (num != 542255445U)
								{
									if (num != 1153929311U)
									{
										goto IL_02BB;
									}
									if (!(text4 == "IGNORECASE"))
									{
										goto IL_02BB;
									}
									num2 = 1;
								}
								else
								{
									if (!(text4 == "IGNOREWIDTH"))
									{
										goto IL_02BB;
									}
									num2 = 16;
								}
							}
							else if (num <= 1618186332U)
							{
								if (num != 1537080989U)
								{
									if (num != 1618186332U)
									{
										goto IL_02BB;
									}
									if (!(text4 == "IGNORENONSPACE"))
									{
										goto IL_02BB;
									}
									num2 = 2;
								}
								else
								{
									if (!(text4 == "DESCENDINGORDER"))
									{
										goto IL_02BB;
									}
									num2 = 16384;
								}
							}
							else if (num != 1721049792U)
							{
								if (num != 3407466425U)
								{
									goto IL_02BB;
								}
								if (!(text4 == "EMPTYGREATEST"))
								{
									goto IL_02BB;
								}
								num2 = 8192;
							}
							else
							{
								if (!(text4 == "IGNORESYMBOLS"))
								{
									goto IL_02BB;
								}
								num2 = 4;
							}
							if (text5 == "0" || text5 == "FALSE")
							{
								options.SetFlag(num2, false);
								goto IL_034E;
							}
							if (text5 == "1" || text5 == "TRUE")
							{
								options.SetFlag(num2, true);
								goto IL_034E;
							}
							if (!throwOnError)
							{
								return null;
							}
							throw new XslTransformException("Collation option '{0}' cannot have the value '{1}'.", new string[]
							{
								array2[0],
								array2[1]
							});
							IL_02BB:
							if (!throwOnError)
							{
								return null;
							}
							throw new XslTransformException("Unsupported option '{0}' in collation.", new string[] { array2[0] });
						}
						IL_034E:
						i++;
					}
				}
			}
			if (options.UpperFirst && options.IgnoreCase)
			{
				options.UpperFirst = false;
			}
			if (options.Ordinal)
			{
				options.CompareOptions = CompareOptions.Ordinal;
				options.UpperFirst = false;
			}
			if (text2 != null && cultureInfo != null)
			{
				int langID = XmlCollation.GetLangID(cultureInfo.LCID);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1363454193U)
				{
					if (num <= 1283486598U)
					{
						if (num != 1278716217U)
						{
							if (num == 1283486598U)
							{
								if (text2 == "trad")
								{
									goto IL_05EE;
								}
							}
						}
						else if (text2 == "dict")
						{
							goto IL_05EE;
						}
					}
					else if (num != 1339334217U)
					{
						if (num == 1363454193U)
						{
							if (text2 == "phn")
							{
								if (langID == 1031)
								{
									cultureInfo = new CultureInfo(66567);
									goto IL_05EE;
								}
								goto IL_05EE;
							}
						}
					}
					else if (text2 == "uni")
					{
						if (langID == 1041 || langID == 1042)
						{
							cultureInfo = new CultureInfo(XmlCollation.MakeLCID(cultureInfo.LCID, 1));
							goto IL_05EE;
						}
						goto IL_05EE;
					}
				}
				else if (num <= 3314303423U)
				{
					if (num != 2751005041U)
					{
						if (num == 3314303423U)
						{
							if (text2 == "bopo")
							{
								if (langID == 1028)
								{
									cultureInfo = new CultureInfo(197636);
									goto IL_05EE;
								}
								goto IL_05EE;
							}
						}
					}
					else if (text2 == "tech")
					{
						if (langID == 1038)
						{
							cultureInfo = new CultureInfo(66574);
							goto IL_05EE;
						}
						goto IL_05EE;
					}
				}
				else if (num != 3629878817U)
				{
					if (num != 3751703171U)
					{
						if (num == 3879610370U)
						{
							if (text2 == "pron")
							{
								goto IL_05EE;
							}
						}
					}
					else if (text2 == "mod")
					{
						if (langID == 1079)
						{
							cultureInfo = new CultureInfo(66615);
							goto IL_05EE;
						}
						goto IL_05EE;
					}
				}
				else if (text2 == "strk")
				{
					if (langID == 2052 || langID == 3076 || langID == 4100 || langID == 5124)
					{
						cultureInfo = new CultureInfo(XmlCollation.MakeLCID(cultureInfo.LCID, 2));
						goto IL_05EE;
					}
					goto IL_05EE;
				}
				if (!throwOnError)
				{
					return null;
				}
				throw new XslTransformException("Unsupported sort option '{0}' in collation.", new string[] { text2 });
			}
			IL_05EE:
			return new XmlCollation(cultureInfo, options);
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x0014F304 File Offset: 0x0014D504
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			XmlCollation xmlCollation = obj as XmlCollation;
			return xmlCollation != null && this.options == xmlCollation.options && object.Equals(this.cultInfo, xmlCollation.cultInfo);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x0014F34C File Offset: 0x0014D54C
		public override int GetHashCode()
		{
			int num = this.options;
			if (this.cultInfo != null)
			{
				num ^= this.cultInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x0014F37C File Offset: 0x0014D57C
		internal void GetObjectData(BinaryWriter writer)
		{
			writer.Write((this.cultInfo != null) ? this.cultInfo.LCID : (-1));
			writer.Write(this.options);
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x0014F3AC File Offset: 0x0014D5AC
		internal XmlCollation(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			this.cultInfo = ((num != -1) ? new CultureInfo(num) : null);
			this.options = new XmlCollation.Options(reader.ReadInt32());
			this.compops = this.options.CompareOptions;
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x0014F3FB File Offset: 0x0014D5FB
		internal bool UpperFirst
		{
			get
			{
				return this.options.UpperFirst;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x0014F408 File Offset: 0x0014D608
		internal bool EmptyGreatest
		{
			get
			{
				return this.options.EmptyGreatest;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x0014F415 File Offset: 0x0014D615
		internal bool DescendingOrder
		{
			get
			{
				return this.options.DescendingOrder;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x0014F422 File Offset: 0x0014D622
		internal CultureInfo Culture
		{
			get
			{
				if (this.cultInfo == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this.cultInfo;
			}
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x0014F438 File Offset: 0x0014D638
		internal XmlSortKey CreateSortKey(string s)
		{
			SortKey sortKey = this.Culture.CompareInfo.GetSortKey(s, this.compops);
			if (!this.UpperFirst)
			{
				return new XmlStringSortKey(sortKey, this.DescendingOrder);
			}
			byte[] keyData = sortKey.KeyData;
			if (this.UpperFirst && keyData.Length != 0)
			{
				int num = 0;
				while (keyData[num] != 1)
				{
					num++;
				}
				do
				{
					num++;
				}
				while (keyData[num] != 1);
				do
				{
					num++;
					byte[] array = keyData;
					int num2 = num;
					array[num2] ^= byte.MaxValue;
				}
				while (keyData[num] != 254);
			}
			return new XmlStringSortKey(keyData, this.DescendingOrder);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x0014F4C8 File Offset: 0x0014D6C8
		private static int MakeLCID(int langid, int sortid)
		{
			return (langid & 65535) | ((sortid & 15) << 16);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x0014F4D9 File Offset: 0x0014D6D9
		private static int GetLangID(int lcid)
		{
			return lcid & 65535;
		}

		// Token: 0x04002748 RID: 10056
		private const int deDE = 1031;

		// Token: 0x04002749 RID: 10057
		private const int huHU = 1038;

		// Token: 0x0400274A RID: 10058
		private const int jaJP = 1041;

		// Token: 0x0400274B RID: 10059
		private const int kaGE = 1079;

		// Token: 0x0400274C RID: 10060
		private const int koKR = 1042;

		// Token: 0x0400274D RID: 10061
		private const int zhTW = 1028;

		// Token: 0x0400274E RID: 10062
		private const int zhCN = 2052;

		// Token: 0x0400274F RID: 10063
		private const int zhHK = 3076;

		// Token: 0x04002750 RID: 10064
		private const int zhSG = 4100;

		// Token: 0x04002751 RID: 10065
		private const int zhMO = 5124;

		// Token: 0x04002752 RID: 10066
		private const int zhTWbopo = 197636;

		// Token: 0x04002753 RID: 10067
		private const int deDEphon = 66567;

		// Token: 0x04002754 RID: 10068
		private const int huHUtech = 66574;

		// Token: 0x04002755 RID: 10069
		private const int kaGEmode = 66615;

		// Token: 0x04002756 RID: 10070
		private CultureInfo cultInfo;

		// Token: 0x04002757 RID: 10071
		private XmlCollation.Options options;

		// Token: 0x04002758 RID: 10072
		private CompareOptions compops;

		// Token: 0x04002759 RID: 10073
		private static XmlCollation cp = new XmlCollation(CultureInfo.InvariantCulture, new XmlCollation.Options(1073741824));

		// Token: 0x0400275A RID: 10074
		private const int LOCALE_CURRENT = -1;

		// Token: 0x020005FE RID: 1534
		private struct Options
		{
			// Token: 0x06003BC9 RID: 15305 RVA: 0x0014F4FD File Offset: 0x0014D6FD
			public Options(int value)
			{
				this.value = value;
			}

			// Token: 0x06003BCA RID: 15306 RVA: 0x0014F506 File Offset: 0x0014D706
			public bool GetFlag(int flag)
			{
				return (this.value & flag) != 0;
			}

			// Token: 0x06003BCB RID: 15307 RVA: 0x0014F513 File Offset: 0x0014D713
			public void SetFlag(int flag, bool value)
			{
				if (value)
				{
					this.value |= flag;
					return;
				}
				this.value &= ~flag;
			}

			// Token: 0x17000C3A RID: 3130
			// (get) Token: 0x06003BCC RID: 15308 RVA: 0x0014F536 File Offset: 0x0014D736
			// (set) Token: 0x06003BCD RID: 15309 RVA: 0x0014F543 File Offset: 0x0014D743
			public bool UpperFirst
			{
				get
				{
					return this.GetFlag(4096);
				}
				set
				{
					this.SetFlag(4096, value);
				}
			}

			// Token: 0x17000C3B RID: 3131
			// (get) Token: 0x06003BCE RID: 15310 RVA: 0x0014F551 File Offset: 0x0014D751
			public bool EmptyGreatest
			{
				get
				{
					return this.GetFlag(8192);
				}
			}

			// Token: 0x17000C3C RID: 3132
			// (get) Token: 0x06003BCF RID: 15311 RVA: 0x0014F55E File Offset: 0x0014D75E
			public bool DescendingOrder
			{
				get
				{
					return this.GetFlag(16384);
				}
			}

			// Token: 0x17000C3D RID: 3133
			// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x0014F56B File Offset: 0x0014D76B
			public bool IgnoreCase
			{
				get
				{
					return this.GetFlag(1);
				}
			}

			// Token: 0x17000C3E RID: 3134
			// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x0014F574 File Offset: 0x0014D774
			public bool Ordinal
			{
				get
				{
					return this.GetFlag(1073741824);
				}
			}

			// Token: 0x17000C3F RID: 3135
			// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x0014F581 File Offset: 0x0014D781
			// (set) Token: 0x06003BD3 RID: 15315 RVA: 0x0014F58F File Offset: 0x0014D78F
			public CompareOptions CompareOptions
			{
				get
				{
					return (CompareOptions)(this.value & -28673);
				}
				set
				{
					this.value = (this.value & 28672) | (int)value;
				}
			}

			// Token: 0x06003BD4 RID: 15316 RVA: 0x0014F5A5 File Offset: 0x0014D7A5
			public static implicit operator int(XmlCollation.Options options)
			{
				return options.value;
			}

			// Token: 0x0400275B RID: 10075
			public const int FlagUpperFirst = 4096;

			// Token: 0x0400275C RID: 10076
			public const int FlagEmptyGreatest = 8192;

			// Token: 0x0400275D RID: 10077
			public const int FlagDescendingOrder = 16384;

			// Token: 0x0400275E RID: 10078
			private const int Mask = 28672;

			// Token: 0x0400275F RID: 10079
			private int value;
		}
	}
}
