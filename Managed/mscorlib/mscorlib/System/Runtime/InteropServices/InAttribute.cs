using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from the caller to the callee, but not back to the caller.</summary>
	// Token: 0x020008C2 RID: 2242
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class InAttribute : Attribute
	{
		// Token: 0x0600550C RID: 21772 RVA: 0x001285ED File Offset: 0x001267ED
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsIn)
			{
				return null;
			}
			return new InAttribute();
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x001285FE File Offset: 0x001267FE
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsIn;
		}
	}
}
