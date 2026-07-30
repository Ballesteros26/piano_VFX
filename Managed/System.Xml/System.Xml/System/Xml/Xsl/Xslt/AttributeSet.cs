using System;
using System.Text;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000598 RID: 1432
	internal class AttributeSet : ProtoTemplate
	{
		// Token: 0x060038B9 RID: 14521 RVA: 0x0013EEE9 File Offset: 0x0013D0E9
		public AttributeSet(QilName name, XslVersion xslVer)
			: base(XslNodeType.AttributeSet, name, xslVer)
		{
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x0013EEF4 File Offset: 0x0013D0F4
		public override string GetDebugName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<xsl:attribute-set name=\"");
			stringBuilder.Append(this.Name.QualifiedName);
			stringBuilder.Append("\">");
			return stringBuilder.ToString();
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0013EF2A File Offset: 0x0013D12A
		public new void AddContent(XslNode node)
		{
			base.AddContent(node);
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x0013EF33 File Offset: 0x0013D133
		public void MergeContent(AttributeSet other)
		{
			base.InsertContent(other.Content);
		}

		// Token: 0x040024F7 RID: 9463
		public CycleCheck CycleCheck;
	}
}
