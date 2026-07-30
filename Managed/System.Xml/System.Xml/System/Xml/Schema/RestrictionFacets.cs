using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003B1 RID: 945
	internal class RestrictionFacets
	{
		// Token: 0x04001971 RID: 6513
		internal int Length;

		// Token: 0x04001972 RID: 6514
		internal int MinLength;

		// Token: 0x04001973 RID: 6515
		internal int MaxLength;

		// Token: 0x04001974 RID: 6516
		internal ArrayList Patterns;

		// Token: 0x04001975 RID: 6517
		internal ArrayList Enumeration;

		// Token: 0x04001976 RID: 6518
		internal XmlSchemaWhiteSpace WhiteSpace;

		// Token: 0x04001977 RID: 6519
		internal object MaxInclusive;

		// Token: 0x04001978 RID: 6520
		internal object MaxExclusive;

		// Token: 0x04001979 RID: 6521
		internal object MinInclusive;

		// Token: 0x0400197A RID: 6522
		internal object MinExclusive;

		// Token: 0x0400197B RID: 6523
		internal int TotalDigits;

		// Token: 0x0400197C RID: 6524
		internal int FractionDigits;

		// Token: 0x0400197D RID: 6525
		internal RestrictionFlags Flags;

		// Token: 0x0400197E RID: 6526
		internal RestrictionFlags FixedFlags;
	}
}
