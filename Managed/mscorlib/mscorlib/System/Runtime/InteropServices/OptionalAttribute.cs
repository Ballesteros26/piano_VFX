using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a parameter is optional.</summary>
	// Token: 0x020008C4 RID: 2244
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x06005512 RID: 21778 RVA: 0x0012861F File Offset: 0x0012681F
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x00128630 File Offset: 0x00126830
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOptional;
		}
	}
}
