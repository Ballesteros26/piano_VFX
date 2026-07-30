using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000045 RID: 69
	internal enum OTFLookupTableType
	{
		// Token: 0x04000370 RID: 880
		Single_Adjustment = 16385,
		// Token: 0x04000371 RID: 881
		Pair_Adjustment,
		// Token: 0x04000372 RID: 882
		Cursive_Attachment,
		// Token: 0x04000373 RID: 883
		Mark_to_Base_Attachment,
		// Token: 0x04000374 RID: 884
		Mark_to_Ligature_Attachment,
		// Token: 0x04000375 RID: 885
		Mark_to_Mark_Attachment,
		// Token: 0x04000376 RID: 886
		Contextual_Positioning,
		// Token: 0x04000377 RID: 887
		Chaining_Contextual_Positioning,
		// Token: 0x04000378 RID: 888
		Extension_Positioning,
		// Token: 0x04000379 RID: 889
		Single_Substitution = 32769,
		// Token: 0x0400037A RID: 890
		Multiple_Substitution,
		// Token: 0x0400037B RID: 891
		Alternate_Substitution,
		// Token: 0x0400037C RID: 892
		Ligature_Substitution,
		// Token: 0x0400037D RID: 893
		Contextual_Substitution,
		// Token: 0x0400037E RID: 894
		Chaining_Contextual_Substitution,
		// Token: 0x0400037F RID: 895
		Extension_Substitution,
		// Token: 0x04000380 RID: 896
		Reverse_Chaining_Contextual_Single_Substitution
	}
}
