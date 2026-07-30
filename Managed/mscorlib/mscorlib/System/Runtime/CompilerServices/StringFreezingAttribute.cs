using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Deprecated. Freezes a string literal when creating native images using the Ngen.exe (Native Image Generator). This class cannot be inherited.</summary>
	// Token: 0x02000895 RID: 2197
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[Serializable]
	public sealed class StringFreezingAttribute : Attribute
	{
	}
}
