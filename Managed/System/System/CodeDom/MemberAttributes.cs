using System;

namespace System.CodeDom
{
	/// <summary>Defines member attribute identifiers for class members.</summary>
	// Token: 0x020007A0 RID: 1952
	public enum MemberAttributes
	{
		/// <summary>An abstract member.</summary>
		// Token: 0x04002E0F RID: 11791
		Abstract = 1,
		/// <summary>A member that cannot be overridden in a derived class.</summary>
		// Token: 0x04002E10 RID: 11792
		Final,
		/// <summary>A static member. In Visual Basic, this is equivalent to the Shared keyword.</summary>
		// Token: 0x04002E11 RID: 11793
		Static,
		/// <summary>A member that overrides a base class member.</summary>
		// Token: 0x04002E12 RID: 11794
		Override,
		/// <summary>A constant member.</summary>
		// Token: 0x04002E13 RID: 11795
		Const,
		/// <summary>A new member.</summary>
		// Token: 0x04002E14 RID: 11796
		New = 16,
		/// <summary>An overloaded member. Some languages, such as Visual Basic, require overloaded members to be explicitly indicated.</summary>
		// Token: 0x04002E15 RID: 11797
		Overloaded = 256,
		/// <summary>A member that is accessible to any class within the same assembly.</summary>
		// Token: 0x04002E16 RID: 11798
		Assembly = 4096,
		/// <summary>A member that is accessible within its class, and derived classes in the same assembly.</summary>
		// Token: 0x04002E17 RID: 11799
		FamilyAndAssembly = 8192,
		/// <summary>A member that is accessible within the family of its class and derived classes.</summary>
		// Token: 0x04002E18 RID: 11800
		Family = 12288,
		/// <summary>A member that is accessible within its class, its derived classes in any assembly, and any class in the same assembly.</summary>
		// Token: 0x04002E19 RID: 11801
		FamilyOrAssembly = 16384,
		/// <summary>A private member.</summary>
		// Token: 0x04002E1A RID: 11802
		Private = 20480,
		/// <summary>A public member.</summary>
		// Token: 0x04002E1B RID: 11803
		Public = 24576,
		/// <summary>An access mask.</summary>
		// Token: 0x04002E1C RID: 11804
		AccessMask = 61440,
		/// <summary>A scope mask.</summary>
		// Token: 0x04002E1D RID: 11805
		ScopeMask = 15,
		/// <summary>A VTable mask.</summary>
		// Token: 0x04002E1E RID: 11806
		VTableMask = 240
	}
}
