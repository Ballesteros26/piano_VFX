using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from callee back to caller.</summary>
	// Token: 0x020008C3 RID: 2243
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x0600550F RID: 21775 RVA: 0x00128606 File Offset: 0x00126806
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00128617 File Offset: 0x00126817
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOut;
		}
	}
}
