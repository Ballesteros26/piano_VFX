using System;
using System.Text;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200059D RID: 1437
	internal class Key : XslNode
	{
		// Token: 0x060038C3 RID: 14531 RVA: 0x0013F0B5 File Offset: 0x0013D2B5
		public Key(QilName name, string match, string use, XslVersion xslVer)
			: base(XslNodeType.Key, name, null, xslVer)
		{
			this.Match = match;
			this.Use = use;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x0013F0D4 File Offset: 0x0013D2D4
		public string GetDebugName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<xsl:key name=\"");
			stringBuilder.Append(this.Name.QualifiedName);
			stringBuilder.Append('"');
			if (this.Match != null)
			{
				stringBuilder.Append(" match=\"");
				stringBuilder.Append(this.Match);
				stringBuilder.Append('"');
			}
			if (this.Use != null)
			{
				stringBuilder.Append(" use=\"");
				stringBuilder.Append(this.Use);
				stringBuilder.Append('"');
			}
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x04002503 RID: 9475
		public readonly string Match;

		// Token: 0x04002504 RID: 9476
		public readonly string Use;

		// Token: 0x04002505 RID: 9477
		public QilFunction Function;
	}
}
