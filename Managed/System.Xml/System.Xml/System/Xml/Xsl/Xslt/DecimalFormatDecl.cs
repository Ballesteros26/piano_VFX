using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200056D RID: 1389
	internal class DecimalFormatDecl
	{
		// Token: 0x0600375D RID: 14173 RVA: 0x00134B11 File Offset: 0x00132D11
		public DecimalFormatDecl(XmlQualifiedName name, string infinitySymbol, string nanSymbol, string characters)
		{
			this.Name = name;
			this.InfinitySymbol = infinitySymbol;
			this.NanSymbol = nanSymbol;
			this.Characters = characters.ToCharArray();
		}

		// Token: 0x04002381 RID: 9089
		public readonly XmlQualifiedName Name;

		// Token: 0x04002382 RID: 9090
		public readonly string InfinitySymbol;

		// Token: 0x04002383 RID: 9091
		public readonly string NanSymbol;

		// Token: 0x04002384 RID: 9092
		public readonly char[] Characters;

		// Token: 0x04002385 RID: 9093
		public static DecimalFormatDecl Default = new DecimalFormatDecl(new XmlQualifiedName(), "Infinity", "NaN", ".,%‰0#;-");
	}
}
