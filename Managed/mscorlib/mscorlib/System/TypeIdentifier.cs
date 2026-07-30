using System;

namespace System
{
	// Token: 0x0200023F RID: 575
	internal interface TypeIdentifier : TypeName, IEquatable<TypeName>
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001B2F RID: 6959
		string InternalName { get; }
	}
}
