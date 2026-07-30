using System;
using System.Globalization;
using System.Text;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000599 RID: 1433
	internal class Template : ProtoTemplate
	{
		// Token: 0x060038BD RID: 14525 RVA: 0x0013EF41 File Offset: 0x0013D141
		public Template(QilName name, string match, QilName mode, double priority, XslVersion xslVer)
			: base(XslNodeType.Template, name, xslVer)
		{
			this.Match = match;
			this.Mode = mode;
			this.Priority = priority;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0013EF64 File Offset: 0x0013D164
		public override string GetDebugName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<xsl:template");
			if (this.Match != null)
			{
				stringBuilder.Append(" match=\"");
				stringBuilder.Append(this.Match);
				stringBuilder.Append('"');
			}
			if (this.Name != null)
			{
				stringBuilder.Append(" name=\"");
				stringBuilder.Append(this.Name.QualifiedName);
				stringBuilder.Append('"');
			}
			if (!double.IsNaN(this.Priority))
			{
				stringBuilder.Append(" priority=\"");
				stringBuilder.Append(this.Priority.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append('"');
			}
			if (this.Mode.LocalName.Length != 0)
			{
				stringBuilder.Append(" mode=\"");
				stringBuilder.Append(this.Mode.QualifiedName);
				stringBuilder.Append('"');
			}
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x040024F8 RID: 9464
		public readonly string Match;

		// Token: 0x040024F9 RID: 9465
		public readonly QilName Mode;

		// Token: 0x040024FA RID: 9466
		public readonly double Priority;

		// Token: 0x040024FB RID: 9467
		public int ImportPrecedence;

		// Token: 0x040024FC RID: 9468
		public int OrderNumber;
	}
}
