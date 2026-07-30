using System;
using System.Collections;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000148 RID: 328
	internal class SecureHashCodeProvider : IHashCodeProvider
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0002A408 File Offset: 0x00028608
		public static SecureHashCodeProvider Default
		{
			get
			{
				object obj = SecureHashCodeProvider.sync;
				SecureHashCodeProvider secureHashCodeProvider;
				lock (obj)
				{
					if (SecureHashCodeProvider.singleton == null)
					{
						SecureHashCodeProvider.singleton = new SecureHashCodeProvider();
					}
					else if (SecureHashCodeProvider.singleton.m_text == null)
					{
						if (!SecureHashCodeProvider.AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
						{
							SecureHashCodeProvider.singleton = new SecureHashCodeProvider();
						}
					}
					else if (!SecureHashCodeProvider.AreEqual(SecureHashCodeProvider.singleton.m_text, CultureInfo.CurrentCulture))
					{
						SecureHashCodeProvider.singleton = new SecureHashCodeProvider();
					}
					secureHashCodeProvider = SecureHashCodeProvider.singleton;
				}
				return secureHashCodeProvider;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0002A4A4 File Offset: 0x000286A4
		public static SecureHashCodeProvider DefaultInvariant
		{
			get
			{
				return SecureHashCodeProvider.singletonInvariant;
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002A4E5 File Offset: 0x000286E5
		public SecureHashCodeProvider()
		{
			if (!SecureHashCodeProvider.AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
			{
				this.m_text = CultureInfo.CurrentCulture.TextInfo;
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002A50E File Offset: 0x0002870E
		public SecureHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (!SecureHashCodeProvider.AreEqual(culture, CultureInfo.InvariantCulture))
			{
				this.m_text = culture.TextInfo;
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002A53D File Offset: 0x0002873D
		private static bool AreEqual(CultureInfo a, CultureInfo b)
		{
			return a.LCID == b.LCID;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002A54D File Offset: 0x0002874D
		private static bool AreEqual(TextInfo info, CultureInfo culture)
		{
			return info.LCID == culture.LCID;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002A560 File Offset: 0x00028760
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			int num = SecureHashCodeProvider.seed;
			if (this.m_text != null && !SecureHashCodeProvider.AreEqual(this.m_text, CultureInfo.InvariantCulture))
			{
				foreach (char c in this.m_text.ToLower(text))
				{
					num = num * 31 + (int)c;
				}
			}
			else
			{
				for (int j = 0; j < text.Length; j++)
				{
					char c = char.ToLower(text[j], CultureInfo.InvariantCulture);
					num = num * 31 + (int)c;
				}
			}
			return num;
		}

		// Token: 0x0400121B RID: 4635
		private static readonly SecureHashCodeProvider singletonInvariant = new SecureHashCodeProvider(CultureInfo.InvariantCulture);

		// Token: 0x0400121C RID: 4636
		private static SecureHashCodeProvider singleton;

		// Token: 0x0400121D RID: 4637
		private static readonly object sync = new object();

		// Token: 0x0400121E RID: 4638
		private static readonly int seed = (int)DateTime.UtcNow.Ticks;

		// Token: 0x0400121F RID: 4639
		private TextInfo m_text;
	}
}
