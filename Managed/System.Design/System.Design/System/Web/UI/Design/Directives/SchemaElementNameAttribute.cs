using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.Design.Directives
{
	/// <summary>Specifies a custom name for a directive attribute.</summary>
	// Token: 0x020001CF RID: 463
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SchemaElementNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.Directives.SchemaElementNameAttribute" /> class.</summary>
		/// <param name="value">The name value.</param>
		// Token: 0x06000BEA RID: 3050 RVA: 0x00002432 File Offset: 0x00000632
		public SchemaElementNameAttribute(string value)
		{
		}

		/// <summary>Gets a name value for the directive attribute.</summary>
		/// <returns>A name value for the directive attribute.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0000970B File Offset: 0x0000790B
		public string Value
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
