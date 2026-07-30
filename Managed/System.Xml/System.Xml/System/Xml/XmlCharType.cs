using System;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000285 RID: 645
	internal struct XmlCharType
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0008BB88 File Offset: 0x00089D88
		private static object StaticLock
		{
			get
			{
				if (XmlCharType.s_Lock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref XmlCharType.s_Lock, obj, null);
				}
				return XmlCharType.s_Lock;
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0008BBB4 File Offset: 0x00089DB4
		private static void InitInstance()
		{
			object staticLock = XmlCharType.StaticLock;
			lock (staticLock)
			{
				if (XmlCharType.s_CharProperties == null)
				{
					XmlCharType.s_CharProperties = new byte[65536];
					XmlCharType.SetProperties("\t\n\r\r  ", 1);
					XmlCharType.SetProperties("AZazÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣", 2);
					XmlCharType.SetProperties("AZ__azÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣", 4);
					XmlCharType.SetProperties("-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ\u0300\u0345\u0360\u0361ΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ\u0483\u0486ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆ\u0591\u05a1\u05a3\u05b9\u05bb\u05bd\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4אתװײءغـ\u0652٠٩\u0670ڷںھۀێېۓە\u06e8\u06ea\u06ed۰۹\u0901\u0903अह\u093c\u094d\u0951\u0954क़\u0963०९\u0981\u0983অঌএঐওনপরললশহ\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd\u09d7\u09d7ড়ঢ়য়\u09e3০ৱ\u0a02\u0a02ਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4dਖ਼ੜਫ਼ਫ਼੦ੴ\u0a81\u0a83અઋઍઍએઑઓનપરલળવહ\u0abc\u0ac5\u0ac7\u0ac9\u0acb\u0acdૠૠ૦૯\u0b01\u0b03ଅଌଏଐଓନପରଲଳଶହ\u0b3c\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57ଡ଼ଢ଼ୟୡ୦୯\u0b82ஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹ\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0bd7\u0bd7௧௯\u0c01\u0c03అఌఎఐఒనపళవహ\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56ౠౡ౦౯\u0c82\u0c83ಅಌಎಐಒನಪಳವಹ\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6ೞೞೠೡ೦೯\u0d02\u0d03അഌഎഐഒനപഹ\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57ൠൡ൦൯กฮะ\u0e3aเ\u0e4e๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະ\u0eb9\u0ebbຽເໄໆໆ\u0ec8\u0ecd໐໙\u0f18\u0f19༠༩\u0f35\u0f35\u0f37\u0f37\u0f39\u0f39\u0f3eཇཉཀྵ\u0f71\u0f84\u0f86ྋ\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad\u0fb1\u0fb7\u0fb9\u0fb9ႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ\u20d0\u20dc\u20e1\u20e1ΩΩKÅ℮℮ↀↂ々々〇〇〡\u302f〱〵ぁゔ\u3099\u309aゝゞァヺーヾㄅㄬ一龥가힣", 8);
					XmlCharType.SetProperties("\t\n\r\r \ud7ff\ue000\ufffd", 16);
					XmlCharType.SetProperties("-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ\u0300\u0345\u0360\u0361ΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ\u0483\u0486ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆ\u0591\u05a1\u05a3\u05b9\u05bb\u05bd\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4אתװײءغـ\u0652٠٩\u0670ڷںھۀێېۓە\u06e8\u06ea\u06ed۰۹\u0901\u0903अह\u093c\u094d\u0951\u0954क़\u0963०९\u0981\u0983অঌএঐওনপরললশহ\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd\u09d7\u09d7ড়ঢ়য়\u09e3০ৱ\u0a02\u0a02ਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4dਖ਼ੜਫ਼ਫ਼੦ੴ\u0a81\u0a83અઋઍઍએઑઓનપરલળવહ\u0abc\u0ac5\u0ac7\u0ac9\u0acb\u0acdૠૠ૦૯\u0b01\u0b03ଅଌଏଐଓନପରଲଳଶହ\u0b3c\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57ଡ଼ଢ଼ୟୡ୦୯\u0b82ஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹ\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0bd7\u0bd7௧௯\u0c01\u0c03అఌఎఐఒనపళవహ\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56ౠౡ౦౯\u0c82\u0c83ಅಌಎಐಒನಪಳವಹ\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6ೞೞೠೡ೦೯\u0d02\u0d03അഌഎഐഒനപഹ\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57ൠൡ൦൯กฮะ\u0e3aเ\u0e4e๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະ\u0eb9\u0ebbຽເໄໆໆ\u0ec8\u0ecd໐໙\u0f18\u0f19༠༩\u0f35\u0f35\u0f37\u0f37\u0f39\u0f39\u0f3eཇཉཀྵ\u0f71\u0f84\u0f86ྋ\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad\u0fb1\u0fb7\u0fb9\u0fb9ႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ\u20d0\u20dc\u20e1\u20e1ΩΩKÅ℮℮ↀↂ々々〇〇〡\u302f〱〵ぁゔ\u3099\u309aゝゞァヺーヾㄅㄬ一龥가힣", 32);
					XmlCharType.SetProperties(" %';=\\^\ud7ff\ue000\ufffd", 64);
					XmlCharType.SetProperties(" !#%(;==?\ud7ff\ue000\ufffd", 128);
				}
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0008BC68 File Offset: 0x00089E68
		private static void SetProperties(string ranges, byte value)
		{
			for (int i = 0; i < ranges.Length; i += 2)
			{
				int j = (int)ranges[i];
				int num = (int)ranges[i + 1];
				while (j <= num)
				{
					byte[] array = XmlCharType.s_CharProperties;
					int num2 = j;
					array[num2] |= value;
					j++;
				}
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0008BCB5 File Offset: 0x00089EB5
		private XmlCharType(byte[] charProperties)
		{
			this.charProperties = charProperties;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0008BCBE File Offset: 0x00089EBE
		public static XmlCharType Instance
		{
			get
			{
				if (XmlCharType.s_CharProperties == null)
				{
					XmlCharType.InitInstance();
				}
				return new XmlCharType(XmlCharType.s_CharProperties);
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0008BCDA File Offset: 0x00089EDA
		public bool IsWhiteSpace(char ch)
		{
			return (this.charProperties[(int)ch] & 1) > 0;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0008BCE9 File Offset: 0x00089EE9
		public bool IsExtender(char ch)
		{
			return ch == '·';
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0008BCF3 File Offset: 0x00089EF3
		public bool IsNCNameSingleChar(char ch)
		{
			return (this.charProperties[(int)ch] & 8) > 0;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0008BD02 File Offset: 0x00089F02
		public bool IsStartNCNameSingleChar(char ch)
		{
			return (this.charProperties[(int)ch] & 4) > 0;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0008BD11 File Offset: 0x00089F11
		public bool IsNameSingleChar(char ch)
		{
			return this.IsNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0008BD23 File Offset: 0x00089F23
		public bool IsStartNameSingleChar(char ch)
		{
			return this.IsStartNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0008BD35 File Offset: 0x00089F35
		public bool IsCharData(char ch)
		{
			return (this.charProperties[(int)ch] & 16) > 0;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0008BD45 File Offset: 0x00089F45
		public bool IsPubidChar(char ch)
		{
			return ch < '\u0080' && ((int)"␀\0ﾻ꿿\uffff蟿\ufffe߿"[(int)(ch >> 4)] & (1 << (int)(ch & '\u000f'))) != 0;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0008BD6B File Offset: 0x00089F6B
		internal bool IsTextChar(char ch)
		{
			return (this.charProperties[(int)ch] & 64) > 0;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0008BD7B File Offset: 0x00089F7B
		internal bool IsAttributeValueChar(char ch)
		{
			return (this.charProperties[(int)ch] & 128) > 0;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0008BD8E File Offset: 0x00089F8E
		public bool IsLetter(char ch)
		{
			return (this.charProperties[(int)ch] & 2) > 0;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0008BD9D File Offset: 0x00089F9D
		public bool IsNCNameCharXml4e(char ch)
		{
			return (this.charProperties[(int)ch] & 32) > 0;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0008BDAD File Offset: 0x00089FAD
		public bool IsStartNCNameCharXml4e(char ch)
		{
			return this.IsLetter(ch) || ch == '_';
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0008BDBF File Offset: 0x00089FBF
		public bool IsNameCharXml4e(char ch)
		{
			return this.IsNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0008BDD1 File Offset: 0x00089FD1
		public bool IsStartNameCharXml4e(char ch)
		{
			return this.IsStartNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0008BDE3 File Offset: 0x00089FE3
		public static bool IsDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0008BDEF File Offset: 0x00089FEF
		public static bool IsHexDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57) || XmlCharType.InRange((int)ch, 97, 102) || XmlCharType.InRange((int)ch, 65, 70);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0008BE15 File Offset: 0x0008A015
		internal static bool IsHighSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 56319);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0008BE27 File Offset: 0x0008A027
		internal static bool IsLowSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 56320, 57343);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0008BE39 File Offset: 0x0008A039
		internal static bool IsSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 57343);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0008BE4B File Offset: 0x0008A04B
		internal static int CombineSurrogateChar(int lowChar, int highChar)
		{
			return (lowChar - 56320) | ((highChar - 55296 << 10) + 65536);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0008BE68 File Offset: 0x0008A068
		internal static void SplitSurrogateChar(int combinedChar, out char lowChar, out char highChar)
		{
			int num = combinedChar - 65536;
			lowChar = (char)(56320 + num % 1024);
			highChar = (char)(55296 + num / 1024);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0008BE9D File Offset: 0x0008A09D
		internal bool IsOnlyWhitespace(string str)
		{
			return this.IsOnlyWhitespaceWithPos(str) == -1;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0008BEAC File Offset: 0x0008A0AC
		internal int IsOnlyWhitespaceWithPos(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[(int)str[i]] & 1) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0008BEE4 File Offset: 0x0008A0E4
		internal int IsOnlyCharData(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[(int)str[i]] & 16) == 0)
					{
						if (i + 1 >= str.Length || !XmlCharType.IsHighSurrogate((int)str[i]) || !XmlCharType.IsLowSurrogate((int)str[i + 1]))
						{
							return i;
						}
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0008BF48 File Offset: 0x0008A148
		internal static bool IsOnlyDigits(string str, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0008BF74 File Offset: 0x0008A174
		internal static bool IsOnlyDigits(char[] chars, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(chars[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0008BF9C File Offset: 0x0008A19C
		internal int IsPublicId(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (!this.IsPubidChar(str[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0008BFCF File Offset: 0x0008A1CF
		private static bool InRange(int value, int start, int end)
		{
			return value - start <= end - start;
		}

		// Token: 0x04000FDC RID: 4060
		internal const int SurHighStart = 55296;

		// Token: 0x04000FDD RID: 4061
		internal const int SurHighEnd = 56319;

		// Token: 0x04000FDE RID: 4062
		internal const int SurLowStart = 56320;

		// Token: 0x04000FDF RID: 4063
		internal const int SurLowEnd = 57343;

		// Token: 0x04000FE0 RID: 4064
		internal const int SurMask = 64512;

		// Token: 0x04000FE1 RID: 4065
		internal const int fWhitespace = 1;

		// Token: 0x04000FE2 RID: 4066
		internal const int fLetter = 2;

		// Token: 0x04000FE3 RID: 4067
		internal const int fNCStartNameSC = 4;

		// Token: 0x04000FE4 RID: 4068
		internal const int fNCNameSC = 8;

		// Token: 0x04000FE5 RID: 4069
		internal const int fCharData = 16;

		// Token: 0x04000FE6 RID: 4070
		internal const int fNCNameXml4e = 32;

		// Token: 0x04000FE7 RID: 4071
		internal const int fText = 64;

		// Token: 0x04000FE8 RID: 4072
		internal const int fAttrValue = 128;

		// Token: 0x04000FE9 RID: 4073
		private const string s_PublicIdBitmap = "␀\0ﾻ꿿\uffff蟿\ufffe߿";

		// Token: 0x04000FEA RID: 4074
		private const uint CharPropertiesSize = 65536U;

		// Token: 0x04000FEB RID: 4075
		internal const string s_Whitespace = "\t\n\r\r  ";

		// Token: 0x04000FEC RID: 4076
		private const string s_NCStartName = "AZ__azÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣";

		// Token: 0x04000FED RID: 4077
		private const string s_NCName = "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ\u0300\u0345\u0360\u0361ΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ\u0483\u0486ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆ\u0591\u05a1\u05a3\u05b9\u05bb\u05bd\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4אתװײءغـ\u0652٠٩\u0670ڷںھۀێېۓە\u06e8\u06ea\u06ed۰۹\u0901\u0903अह\u093c\u094d\u0951\u0954क़\u0963०९\u0981\u0983অঌএঐওনপরললশহ\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd\u09d7\u09d7ড়ঢ়য়\u09e3০ৱ\u0a02\u0a02ਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4dਖ਼ੜਫ਼ਫ਼੦ੴ\u0a81\u0a83અઋઍઍએઑઓનપરલળવહ\u0abc\u0ac5\u0ac7\u0ac9\u0acb\u0acdૠૠ૦૯\u0b01\u0b03ଅଌଏଐଓନପରଲଳଶହ\u0b3c\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57ଡ଼ଢ଼ୟୡ୦୯\u0b82ஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹ\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0bd7\u0bd7௧௯\u0c01\u0c03అఌఎఐఒనపళవహ\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56ౠౡ౦౯\u0c82\u0c83ಅಌಎಐಒನಪಳವಹ\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6ೞೞೠೡ೦೯\u0d02\u0d03അഌഎഐഒനപഹ\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57ൠൡ൦൯กฮะ\u0e3aเ\u0e4e๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະ\u0eb9\u0ebbຽເໄໆໆ\u0ec8\u0ecd໐໙\u0f18\u0f19༠༩\u0f35\u0f35\u0f37\u0f37\u0f39\u0f39\u0f3eཇཉཀྵ\u0f71\u0f84\u0f86ྋ\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad\u0fb1\u0fb7\u0fb9\u0fb9ႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ\u20d0\u20dc\u20e1\u20e1ΩΩKÅ℮℮ↀↂ々々〇〇〡\u302f〱〵ぁゔ\u3099\u309aゝゞァヺーヾㄅㄬ一龥가힣";

		// Token: 0x04000FEE RID: 4078
		private const string s_CharData = "\t\n\r\r \ud7ff\ue000\ufffd";

		// Token: 0x04000FEF RID: 4079
		private const string s_PublicID = "\n\n\r\r !#%';==?Z__az";

		// Token: 0x04000FF0 RID: 4080
		private const string s_Text = " %';=\\^\ud7ff\ue000\ufffd";

		// Token: 0x04000FF1 RID: 4081
		private const string s_AttrValue = " !#%(;==?\ud7ff\ue000\ufffd";

		// Token: 0x04000FF2 RID: 4082
		private const string s_LetterXml4e = "AZazÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣";

		// Token: 0x04000FF3 RID: 4083
		private const string s_NCNameXml4e = "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ\u0300\u0345\u0360\u0361ΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ\u0483\u0486ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆ\u0591\u05a1\u05a3\u05b9\u05bb\u05bd\u05bf\u05bf\u05c1\u05c2\u05c4\u05c4אתװײءغـ\u0652٠٩\u0670ڷںھۀێېۓە\u06e8\u06ea\u06ed۰۹\u0901\u0903अह\u093c\u094d\u0951\u0954क़\u0963०९\u0981\u0983অঌএঐওনপরললশহ\u09bc\u09bc\u09be\u09c4\u09c7\u09c8\u09cb\u09cd\u09d7\u09d7ড়ঢ়য়\u09e3০ৱ\u0a02\u0a02ਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ\u0a3c\u0a3c\u0a3e\u0a42\u0a47\u0a48\u0a4b\u0a4dਖ਼ੜਫ਼ਫ਼੦ੴ\u0a81\u0a83અઋઍઍએઑઓનપરલળવહ\u0abc\u0ac5\u0ac7\u0ac9\u0acb\u0acdૠૠ૦૯\u0b01\u0b03ଅଌଏଐଓନପରଲଳଶହ\u0b3c\u0b43\u0b47\u0b48\u0b4b\u0b4d\u0b56\u0b57ଡ଼ଢ଼ୟୡ୦୯\u0b82ஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹ\u0bbe\u0bc2\u0bc6\u0bc8\u0bca\u0bcd\u0bd7\u0bd7௧௯\u0c01\u0c03అఌఎఐఒనపళవహ\u0c3e\u0c44\u0c46\u0c48\u0c4a\u0c4d\u0c55\u0c56ౠౡ౦౯\u0c82\u0c83ಅಌಎಐಒನಪಳವಹ\u0cbe\u0cc4\u0cc6\u0cc8\u0cca\u0ccd\u0cd5\u0cd6ೞೞೠೡ೦೯\u0d02\u0d03അഌഎഐഒനപഹ\u0d3e\u0d43\u0d46\u0d48\u0d4a\u0d4d\u0d57\u0d57ൠൡ൦൯กฮะ\u0e3aเ\u0e4e๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະ\u0eb9\u0ebbຽເໄໆໆ\u0ec8\u0ecd໐໙\u0f18\u0f19༠༩\u0f35\u0f35\u0f37\u0f37\u0f39\u0f39\u0f3eཇཉཀྵ\u0f71\u0f84\u0f86ྋ\u0f90\u0f95\u0f97\u0f97\u0f99\u0fad\u0fb1\u0fb7\u0fb9\u0fb9ႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ\u20d0\u20dc\u20e1\u20e1ΩΩKÅ℮℮ↀↂ々々〇〇〡\u302f〱〵ぁゔ\u3099\u309aゝゞァヺーヾㄅㄬ一龥가힣";

		// Token: 0x04000FF4 RID: 4084
		private static object s_Lock;

		// Token: 0x04000FF5 RID: 4085
		private static volatile byte[] s_CharProperties;

		// Token: 0x04000FF6 RID: 4086
		internal byte[] charProperties;
	}
}
