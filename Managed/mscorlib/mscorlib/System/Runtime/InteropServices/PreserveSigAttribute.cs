using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the HRESULT or retval signature transformation that takes place during COM interop calls should be suppressed.</summary>
	// Token: 0x020008C1 RID: 2241
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06005509 RID: 21769 RVA: 0x001285C5 File Offset: 0x001267C5
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x001285DC File Offset: 0x001267DC
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
		}
	}
}
