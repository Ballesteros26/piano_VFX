using System;

namespace System
{
	// Token: 0x0200023E RID: 574
	internal interface TypeName : IEquatable<TypeName>
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001B2D RID: 6957
		string DisplayName { get; }

		// Token: 0x06001B2E RID: 6958
		TypeName NestedName(TypeIdentifier innerName);
	}
}
