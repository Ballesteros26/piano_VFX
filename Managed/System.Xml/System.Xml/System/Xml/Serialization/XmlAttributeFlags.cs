using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200032B RID: 811
	internal enum XmlAttributeFlags
	{
		// Token: 0x04001714 RID: 5908
		Enum = 1,
		// Token: 0x04001715 RID: 5909
		Array,
		// Token: 0x04001716 RID: 5910
		Text = 4,
		// Token: 0x04001717 RID: 5911
		ArrayItems = 8,
		// Token: 0x04001718 RID: 5912
		Elements = 16,
		// Token: 0x04001719 RID: 5913
		Attribute = 32,
		// Token: 0x0400171A RID: 5914
		Root = 64,
		// Token: 0x0400171B RID: 5915
		Type = 128,
		// Token: 0x0400171C RID: 5916
		AnyElements = 256,
		// Token: 0x0400171D RID: 5917
		AnyAttribute = 512,
		// Token: 0x0400171E RID: 5918
		ChoiceIdentifier = 1024,
		// Token: 0x0400171F RID: 5919
		XmlnsDeclarations = 2048
	}
}
