using System;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000666 RID: 1638
	internal enum PossibleXmlStates
	{
		// Token: 0x04002A46 RID: 10822
		None,
		// Token: 0x04002A47 RID: 10823
		WithinSequence,
		// Token: 0x04002A48 RID: 10824
		EnumAttrs,
		// Token: 0x04002A49 RID: 10825
		WithinContent,
		// Token: 0x04002A4A RID: 10826
		WithinAttr,
		// Token: 0x04002A4B RID: 10827
		WithinComment,
		// Token: 0x04002A4C RID: 10828
		WithinPI,
		// Token: 0x04002A4D RID: 10829
		Any
	}
}
