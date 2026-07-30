using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>An <see cref="T:System.Web.UI.ITemplate" /> implementation that is called from the generated page class code. This class cannot be inherited.</summary>
	// Token: 0x020001B3 RID: 435
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class CompiledTemplateBuilder : ITemplate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.CompiledTemplateBuilder" /> class.</summary>
		/// <param name="buildTemplateMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledTemplateBuilder.InstantiateIn(System.Web.UI.Control)" /> method call.</param>
		// Token: 0x060010C1 RID: 4289 RVA: 0x0002E2F6 File Offset: 0x0002C4F6
		public CompiledTemplateBuilder(BuildTemplateMethod buildTemplateMethod)
		{
			this.templateMethod = buildTemplateMethod;
		}

		/// <summary>Populates the <see cref="T:System.Web.UI.Control" /> object with the child controls contained in the template.</summary>
		/// <param name="container">A <see cref="T:System.Web.UI.Control" /> that represents the container used to store the child controls in the template.</param>
		// Token: 0x060010C2 RID: 4290 RVA: 0x0002E305 File Offset: 0x0002C505
		public void InstantiateIn(Control container)
		{
			this.templateMethod(container);
		}

		// Token: 0x0400139B RID: 5019
		private BuildTemplateMethod templateMethod;
	}
}
