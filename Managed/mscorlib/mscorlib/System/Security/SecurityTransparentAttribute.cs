using System;

namespace System.Security
{
	/// <summary>Specifies that an assembly cannot cause an elevation of privilege. </summary>
	// Token: 0x02000534 RID: 1332
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityTransparentAttribute : Attribute
	{
	}
}
