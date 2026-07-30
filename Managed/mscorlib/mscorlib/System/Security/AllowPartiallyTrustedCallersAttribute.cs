using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Allows an assembly to be called by partially trusted code. Without this declaration, only fully trusted callers are able to use the assembly. This class cannot be inherited.</summary>
	// Token: 0x0200052E RID: 1326
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
	{
		/// <summary>Gets or sets the default partial trust visibility for code that is marked with the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> (APTCA) attribute.</summary>
		/// <returns>One of the enumeration values. The default is <see cref="F:System.Security.PartialTrustVisibilityLevel.VisibleToAllHosts" />. </returns>
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000D8EF9 File Offset: 0x000D70F9
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x000D8F01 File Offset: 0x000D7101
		public PartialTrustVisibilityLevel PartialTrustVisibilityLevel
		{
			get
			{
				return this._visibilityLevel;
			}
			set
			{
				this._visibilityLevel = value;
			}
		}

		// Token: 0x04001F25 RID: 7973
		private PartialTrustVisibilityLevel _visibilityLevel;
	}
}
