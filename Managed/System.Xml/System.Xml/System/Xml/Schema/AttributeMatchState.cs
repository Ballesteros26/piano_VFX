using System;

namespace System.Xml.Schema
{
	// Token: 0x02000418 RID: 1048
	internal enum AttributeMatchState
	{
		// Token: 0x04001B20 RID: 6944
		AttributeFound,
		// Token: 0x04001B21 RID: 6945
		AnyIdAttributeFound,
		// Token: 0x04001B22 RID: 6946
		UndeclaredElementAndAttribute,
		// Token: 0x04001B23 RID: 6947
		UndeclaredAttribute,
		// Token: 0x04001B24 RID: 6948
		AnyAttributeLax,
		// Token: 0x04001B25 RID: 6949
		AnyAttributeSkip,
		// Token: 0x04001B26 RID: 6950
		ProhibitedAnyAttribute,
		// Token: 0x04001B27 RID: 6951
		ProhibitedAttribute,
		// Token: 0x04001B28 RID: 6952
		AttributeNameMismatch,
		// Token: 0x04001B29 RID: 6953
		ValidateAttributeInvalidCall
	}
}
