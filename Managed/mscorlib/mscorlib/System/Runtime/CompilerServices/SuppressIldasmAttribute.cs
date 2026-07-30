using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Prevents the Ildasm.exe (MSIL Disassembler) from disassembling an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000891 RID: 2193
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
	public sealed class SuppressIldasmAttribute : Attribute
	{
	}
}
