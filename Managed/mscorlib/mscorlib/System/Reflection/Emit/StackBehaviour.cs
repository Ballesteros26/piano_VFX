using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes how values are pushed onto a stack or popped off a stack.</summary>
	// Token: 0x0200037E RID: 894
	[ComVisible(true)]
	[Serializable]
	public enum StackBehaviour
	{
		/// <summary>No values are popped off the stack.</summary>
		// Token: 0x040015E3 RID: 5603
		Pop0,
		/// <summary>Pops one value off the stack.</summary>
		// Token: 0x040015E4 RID: 5604
		Pop1,
		/// <summary>Pops 1 value off the stack for the first operand, and 1 value of the stack for the second operand.</summary>
		// Token: 0x040015E5 RID: 5605
		Pop1_pop1,
		/// <summary>Pops a 32-bit integer off the stack.</summary>
		// Token: 0x040015E6 RID: 5606
		Popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x040015E7 RID: 5607
		Popi_pop1,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x040015E8 RID: 5608
		Popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit integer off the stack for the second operand.</summary>
		// Token: 0x040015E9 RID: 5609
		Popi_popi8,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, a 32-bit integer off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040015EA RID: 5610
		Popi_popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x040015EB RID: 5611
		Popi_popr4,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x040015EC RID: 5612
		Popi_popr8,
		/// <summary>Pops a reference off the stack.</summary>
		// Token: 0x040015ED RID: 5613
		Popref,
		/// <summary>Pops a reference off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x040015EE RID: 5614
		Popref_pop1,
		/// <summary>Pops a reference off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x040015EF RID: 5615
		Popref_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a value off the stack for the third operand.</summary>
		// Token: 0x040015F0 RID: 5616
		Popref_popi_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit integer off the stack for the third operand.</summary>
		// Token: 0x040015F1 RID: 5617
		Popref_popi_popi8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040015F2 RID: 5618
		Popref_popi_popr4,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit floating point number off the stack for the third operand.</summary>
		// Token: 0x040015F3 RID: 5619
		Popref_popi_popr8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a reference off the stack for the third operand.</summary>
		// Token: 0x040015F4 RID: 5620
		Popref_popi_popref,
		/// <summary>No values are pushed onto the stack.</summary>
		// Token: 0x040015F5 RID: 5621
		Push0,
		/// <summary>Pushes one value onto the stack.</summary>
		// Token: 0x040015F6 RID: 5622
		Push1,
		/// <summary>Pushes 1 value onto the stack for the first operand, and 1 value onto the stack for the second operand.</summary>
		// Token: 0x040015F7 RID: 5623
		Push1_push1,
		/// <summary>Pushes a 32-bit integer onto the stack.</summary>
		// Token: 0x040015F8 RID: 5624
		Pushi,
		/// <summary>Pushes a 64-bit integer onto the stack.</summary>
		// Token: 0x040015F9 RID: 5625
		Pushi8,
		/// <summary>Pushes a 32-bit floating point number onto the stack.</summary>
		// Token: 0x040015FA RID: 5626
		Pushr4,
		/// <summary>Pushes a 64-bit floating point number onto the stack.</summary>
		// Token: 0x040015FB RID: 5627
		Pushr8,
		/// <summary>Pushes a reference onto the stack.</summary>
		// Token: 0x040015FC RID: 5628
		Pushref,
		/// <summary>Pops a variable off the stack.</summary>
		// Token: 0x040015FD RID: 5629
		Varpop,
		/// <summary>Pushes a variable onto the stack.</summary>
		// Token: 0x040015FE RID: 5630
		Varpush,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x040015FF RID: 5631
		Popref_popi_pop1
	}
}
