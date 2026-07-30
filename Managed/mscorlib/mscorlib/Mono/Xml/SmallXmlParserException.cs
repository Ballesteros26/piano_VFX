using System;

namespace Mono.Xml
{
	// Token: 0x0200002B RID: 43
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004D3B File Offset: 0x00002F3B
		public SmallXmlParserException(string msg, int line, int column)
			: base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004D68 File Offset: 0x00002F68
		public int Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004D70 File Offset: 0x00002F70
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x040003C8 RID: 968
		private int line;

		// Token: 0x040003C9 RID: 969
		private int column;
	}
}
