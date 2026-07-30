using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200071D RID: 1821
	[Serializable]
	internal enum BinaryHeaderEnum
	{
		// Token: 0x040027E9 RID: 10217
		SerializedStreamHeader,
		// Token: 0x040027EA RID: 10218
		Object,
		// Token: 0x040027EB RID: 10219
		ObjectWithMap,
		// Token: 0x040027EC RID: 10220
		ObjectWithMapAssemId,
		// Token: 0x040027ED RID: 10221
		ObjectWithMapTyped,
		// Token: 0x040027EE RID: 10222
		ObjectWithMapTypedAssemId,
		// Token: 0x040027EF RID: 10223
		ObjectString,
		// Token: 0x040027F0 RID: 10224
		Array,
		// Token: 0x040027F1 RID: 10225
		MemberPrimitiveTyped,
		// Token: 0x040027F2 RID: 10226
		MemberReference,
		// Token: 0x040027F3 RID: 10227
		ObjectNull,
		// Token: 0x040027F4 RID: 10228
		MessageEnd,
		// Token: 0x040027F5 RID: 10229
		Assembly,
		// Token: 0x040027F6 RID: 10230
		ObjectNullMultiple256,
		// Token: 0x040027F7 RID: 10231
		ObjectNullMultiple,
		// Token: 0x040027F8 RID: 10232
		ArraySinglePrimitive,
		// Token: 0x040027F9 RID: 10233
		ArraySingleObject,
		// Token: 0x040027FA RID: 10234
		ArraySingleString,
		// Token: 0x040027FB RID: 10235
		CrossAppDomainMap,
		// Token: 0x040027FC RID: 10236
		CrossAppDomainString,
		// Token: 0x040027FD RID: 10237
		CrossAppDomainAssembly,
		// Token: 0x040027FE RID: 10238
		MethodCall,
		// Token: 0x040027FF RID: 10239
		MethodReturn
	}
}
