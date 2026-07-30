using System;

namespace System.Security
{
	/// <summary>Specifies that code or an assembly performs security-critical operations.</summary>
	// Token: 0x02000531 RID: 1329
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class. </summary>
		// Token: 0x06003C2C RID: 15404 RVA: 0x00002180 File Offset: 0x00000380
		public SecurityCriticalAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityCriticalAttribute" /> class with the specified scope. </summary>
		/// <param name="scope">One of the enumeration values that specifies the scope of the attribute. </param>
		// Token: 0x06003C2D RID: 15405 RVA: 0x000D8F0A File Offset: 0x000D710A
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		/// <summary>Gets the scope for the attribute.</summary>
		/// <returns>One of the enumeration values that specifies the scope of the attribute. The default is <see cref="F:System.Security.SecurityCriticalScope.Explicit" />, which indicates that the attribute applies only to the immediate target.</returns>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000D8F19 File Offset: 0x000D7119
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001F2C RID: 7980
		private SecurityCriticalScope _val;
	}
}
