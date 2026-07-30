using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000030 RID: 48
	internal class StyleElement
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000E16C File Offset: 0x0000C36C
		public StyleElement(Style s, TokenClass token_class, Major major, Minor minor, int param, string text)
		{
			this.token_class = token_class;
			this.major = major;
			this.minor = minor;
			this.param = param;
			this.text = text;
			lock (s)
			{
				if (s.Elements == null)
				{
					s.Elements = this;
				}
				else
				{
					StyleElement elements = s.Elements;
					while (elements.next != null)
					{
						elements = elements.next;
					}
					elements.next = this;
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000E214 File Offset: 0x0000C414
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000E21C File Offset: 0x0000C41C
		public TokenClass TokenClass
		{
			get
			{
				return this.token_class;
			}
			set
			{
				this.token_class = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000E228 File Offset: 0x0000C428
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000E230 File Offset: 0x0000C430
		public Major Major
		{
			get
			{
				return this.major;
			}
			set
			{
				this.major = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000E23C File Offset: 0x0000C43C
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000E244 File Offset: 0x0000C444
		public Minor Minor
		{
			get
			{
				return this.minor;
			}
			set
			{
				this.minor = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000E250 File Offset: 0x0000C450
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000E258 File Offset: 0x0000C458
		public int Param
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000E264 File Offset: 0x0000C464
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000E26C File Offset: 0x0000C46C
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x040004E2 RID: 1250
		private TokenClass token_class;

		// Token: 0x040004E3 RID: 1251
		private Major major;

		// Token: 0x040004E4 RID: 1252
		private Minor minor;

		// Token: 0x040004E5 RID: 1253
		private int param;

		// Token: 0x040004E6 RID: 1254
		private string text;

		// Token: 0x040004E7 RID: 1255
		private StyleElement next;
	}
}
