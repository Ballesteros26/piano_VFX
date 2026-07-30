using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that ASP.NET server controls use to identify a validation property. This class cannot be inherited.</summary>
	// Token: 0x02000245 RID: 581
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ValidationPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the validation property. </param>
		// Token: 0x060017F1 RID: 6129 RVA: 0x00040D8A File Offset: 0x0003EF8A
		public ValidationPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the ASP.NET server control's validation property.</summary>
		/// <returns>The name of the validation property.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00040D99 File Offset: 0x0003EF99
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04001600 RID: 5632
		private string name;
	}
}
