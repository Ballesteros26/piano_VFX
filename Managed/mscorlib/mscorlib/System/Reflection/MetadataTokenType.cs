using System;

namespace System.Reflection
{
	// Token: 0x020002EB RID: 747
	[Serializable]
	internal enum MetadataTokenType
	{
		// Token: 0x04001223 RID: 4643
		Module,
		// Token: 0x04001224 RID: 4644
		TypeRef = 16777216,
		// Token: 0x04001225 RID: 4645
		TypeDef = 33554432,
		// Token: 0x04001226 RID: 4646
		FieldDef = 67108864,
		// Token: 0x04001227 RID: 4647
		MethodDef = 100663296,
		// Token: 0x04001228 RID: 4648
		ParamDef = 134217728,
		// Token: 0x04001229 RID: 4649
		InterfaceImpl = 150994944,
		// Token: 0x0400122A RID: 4650
		MemberRef = 167772160,
		// Token: 0x0400122B RID: 4651
		CustomAttribute = 201326592,
		// Token: 0x0400122C RID: 4652
		Permission = 234881024,
		// Token: 0x0400122D RID: 4653
		Signature = 285212672,
		// Token: 0x0400122E RID: 4654
		Event = 335544320,
		// Token: 0x0400122F RID: 4655
		Property = 385875968,
		// Token: 0x04001230 RID: 4656
		ModuleRef = 436207616,
		// Token: 0x04001231 RID: 4657
		TypeSpec = 452984832,
		// Token: 0x04001232 RID: 4658
		Assembly = 536870912,
		// Token: 0x04001233 RID: 4659
		AssemblyRef = 587202560,
		// Token: 0x04001234 RID: 4660
		File = 637534208,
		// Token: 0x04001235 RID: 4661
		ExportedType = 654311424,
		// Token: 0x04001236 RID: 4662
		ManifestResource = 671088640,
		// Token: 0x04001237 RID: 4663
		GenericPar = 704643072,
		// Token: 0x04001238 RID: 4664
		MethodSpec = 721420288,
		// Token: 0x04001239 RID: 4665
		String = 1879048192,
		// Token: 0x0400123A RID: 4666
		Name = 1895825408,
		// Token: 0x0400123B RID: 4667
		BaseType = 1912602624,
		// Token: 0x0400123C RID: 4668
		Invalid = 2147483647
	}
}
