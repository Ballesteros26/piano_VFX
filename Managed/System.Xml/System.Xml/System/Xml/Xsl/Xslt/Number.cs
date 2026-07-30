using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200059F RID: 1439
	internal class Number : XslNode
	{
		// Token: 0x060038C5 RID: 14533 RVA: 0x0013F174 File Offset: 0x0013D374
		public Number(NumberLevel level, string count, string from, string value, string format, string lang, string letterValue, string groupingSeparator, string groupingSize, XslVersion xslVer)
			: base(XslNodeType.Number, null, null, xslVer)
		{
			this.Level = level;
			this.Count = count;
			this.From = from;
			this.Value = value;
			this.Format = format;
			this.Lang = lang;
			this.LetterValue = letterValue;
			this.GroupingSeparator = groupingSeparator;
			this.GroupingSize = groupingSize;
		}

		// Token: 0x0400250A RID: 9482
		public readonly NumberLevel Level;

		// Token: 0x0400250B RID: 9483
		public readonly string Count;

		// Token: 0x0400250C RID: 9484
		public readonly string From;

		// Token: 0x0400250D RID: 9485
		public readonly string Value;

		// Token: 0x0400250E RID: 9486
		public readonly string Format;

		// Token: 0x0400250F RID: 9487
		public readonly string Lang;

		// Token: 0x04002510 RID: 9488
		public readonly string LetterValue;

		// Token: 0x04002511 RID: 9489
		public readonly string GroupingSeparator;

		// Token: 0x04002512 RID: 9490
		public readonly string GroupingSize;
	}
}
