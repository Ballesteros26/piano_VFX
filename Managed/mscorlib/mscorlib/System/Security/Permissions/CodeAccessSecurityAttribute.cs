using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for code access security.</summary>
	// Token: 0x02000585 RID: 1413
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeAccessSecurityAttribute : SecurityAttribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003F4A RID: 16202 RVA: 0x000E2589 File Offset: 0x000E0789
		protected CodeAccessSecurityAttribute(SecurityAction action)
			: base(action)
		{
		}
	}
}
