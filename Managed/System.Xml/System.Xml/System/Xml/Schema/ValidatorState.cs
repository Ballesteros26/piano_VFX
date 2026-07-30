using System;

namespace System.Xml.Schema
{
	// Token: 0x0200048C RID: 1164
	internal enum ValidatorState
	{
		// Token: 0x04001E41 RID: 7745
		None,
		// Token: 0x04001E42 RID: 7746
		Start,
		// Token: 0x04001E43 RID: 7747
		TopLevelAttribute,
		// Token: 0x04001E44 RID: 7748
		TopLevelTextOrWS,
		// Token: 0x04001E45 RID: 7749
		Element,
		// Token: 0x04001E46 RID: 7750
		Attribute,
		// Token: 0x04001E47 RID: 7751
		EndOfAttributes,
		// Token: 0x04001E48 RID: 7752
		Text,
		// Token: 0x04001E49 RID: 7753
		Whitespace,
		// Token: 0x04001E4A RID: 7754
		EndElement,
		// Token: 0x04001E4B RID: 7755
		SkipToEndElement,
		// Token: 0x04001E4C RID: 7756
		Finish
	}
}
