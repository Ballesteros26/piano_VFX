using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes the operand type of Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x02000373 RID: 883
	[ComVisible(true)]
	[Serializable]
	public enum OperandType
	{
		/// <summary>The operand is a 32-bit integer branch target.</summary>
		// Token: 0x04001596 RID: 5526
		InlineBrTarget,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04001597 RID: 5527
		InlineField,
		/// <summary>The operand is a 32-bit integer.</summary>
		// Token: 0x04001598 RID: 5528
		InlineI,
		/// <summary>The operand is a 64-bit integer.</summary>
		// Token: 0x04001599 RID: 5529
		InlineI8,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x0400159A RID: 5530
		InlineMethod,
		/// <summary>No operand.</summary>
		// Token: 0x0400159B RID: 5531
		InlineNone,
		/// <summary>The operand is reserved and should not be used.</summary>
		// Token: 0x0400159C RID: 5532
		[Obsolete("This API has been deprecated.")]
		InlinePhi,
		/// <summary>The operand is a 64-bit IEEE floating point number.</summary>
		// Token: 0x0400159D RID: 5533
		InlineR,
		/// <summary>The operand is a 32-bit metadata signature token.</summary>
		// Token: 0x0400159E RID: 5534
		InlineSig = 9,
		/// <summary>The operand is a 32-bit metadata string token.</summary>
		// Token: 0x0400159F RID: 5535
		InlineString,
		/// <summary>The operand is the 32-bit integer argument to a switch instruction.</summary>
		// Token: 0x040015A0 RID: 5536
		InlineSwitch,
		/// <summary>The operand is a FieldRef, MethodRef, or TypeRef token.</summary>
		// Token: 0x040015A1 RID: 5537
		InlineTok,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x040015A2 RID: 5538
		InlineType,
		/// <summary>The operand is 16-bit integer containing the ordinal of a local variable or an argument.</summary>
		// Token: 0x040015A3 RID: 5539
		InlineVar,
		/// <summary>The operand is an 8-bit integer branch target.</summary>
		// Token: 0x040015A4 RID: 5540
		ShortInlineBrTarget,
		/// <summary>The operand is an 8-bit integer.</summary>
		// Token: 0x040015A5 RID: 5541
		ShortInlineI,
		/// <summary>The operand is a 32-bit IEEE floating point number.</summary>
		// Token: 0x040015A6 RID: 5542
		ShortInlineR,
		/// <summary>The operand is an 8-bit integer containing the ordinal of a local variable or an argumenta.</summary>
		// Token: 0x040015A7 RID: 5543
		ShortInlineVar
	}
}
