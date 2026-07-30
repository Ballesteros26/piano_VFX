using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200031C RID: 796
	internal enum TypeFlags
	{
		// Token: 0x040016C6 RID: 5830
		None,
		// Token: 0x040016C7 RID: 5831
		Abstract,
		// Token: 0x040016C8 RID: 5832
		Reference,
		// Token: 0x040016C9 RID: 5833
		Special = 4,
		// Token: 0x040016CA RID: 5834
		CanBeAttributeValue = 8,
		// Token: 0x040016CB RID: 5835
		CanBeTextValue = 16,
		// Token: 0x040016CC RID: 5836
		CanBeElementValue = 32,
		// Token: 0x040016CD RID: 5837
		HasCustomFormatter = 64,
		// Token: 0x040016CE RID: 5838
		AmbiguousDataType = 128,
		// Token: 0x040016CF RID: 5839
		IgnoreDefault = 512,
		// Token: 0x040016D0 RID: 5840
		HasIsEmpty = 1024,
		// Token: 0x040016D1 RID: 5841
		HasDefaultConstructor = 2048,
		// Token: 0x040016D2 RID: 5842
		XmlEncodingNotRequired = 4096,
		// Token: 0x040016D3 RID: 5843
		UseReflection = 16384,
		// Token: 0x040016D4 RID: 5844
		CollapseWhitespace = 32768,
		// Token: 0x040016D5 RID: 5845
		OptionalValue = 65536,
		// Token: 0x040016D6 RID: 5846
		CtorInaccessible = 131072,
		// Token: 0x040016D7 RID: 5847
		UsePrivateImplementation = 262144,
		// Token: 0x040016D8 RID: 5848
		GenericInterface = 524288,
		// Token: 0x040016D9 RID: 5849
		Unsupported = 1048576
	}
}
