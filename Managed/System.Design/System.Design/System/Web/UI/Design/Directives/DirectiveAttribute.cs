using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.Design.Directives
{
	/// <summary>Specifies a custom attribute of a directive.</summary>
	// Token: 0x020001CD RID: 461
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class DirectiveAttribute : Attribute
	{
		/// <summary>Gets or sets an attribute of a directive that specifies whether the attribute is allowed on mobile pages.</summary>
		/// <returns>true if the attribute is allowed on mobile pages; otherwise, false.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00016858 File Offset: 0x00014A58
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00009519 File Offset: 0x00007719
		public bool AllowedOnMobilePages
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the type that is associated with the attribute.</summary>
		/// <returns>The type that is associated with the attribute.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00009519 File Offset: 0x00007719
		public string BuilderType
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set whether a culture value that is associated with the attribute.</summary>
		/// <returns>true if a culture value that is associated with the attribute; otherwise, false.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00016874 File Offset: 0x00014A74
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00009519 File Offset: 0x00007719
		public bool Culture
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value representing the renamed type of the attribute.</summary>
		/// <returns>A value representing the renamed type of the attribute.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00009519 File Offset: 0x00007719
		public string RenameType
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether server language extensions are available.</summary>
		/// <returns>true if server language extensions are available; otherwise, false.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00016890 File Offset: 0x00014A90
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00009519 File Offset: 0x00007719
		public bool ServerLanguageExtensions
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether server language names are available.</summary>
		/// <returns>true if server language names are available; otherwise, false.</returns>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000168AC File Offset: 0x00014AAC
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00009519 File Offset: 0x00007719
		public bool ServerLanguageNames
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
