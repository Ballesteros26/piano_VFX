using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Provides the context for an <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object.</summary>
	// Token: 0x02000654 RID: 1620
	public sealed class ExpressionBuilderContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> class using the specified virtual path.</summary>
		/// <param name="virtualPath">The virtual path of the file associated with the specified <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
		// Token: 0x06004581 RID: 17793 RVA: 0x000BE9C1 File Offset: 0x000BCBC1
		public ExpressionBuilderContext(string virtualPath)
		{
			this.vpath = virtualPath;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> class using the specified template control.</summary>
		/// <param name="templateControl">The <see cref="T:System.Web.UI.TemplateControl" /> to use with the specified <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
		// Token: 0x06004582 RID: 17794 RVA: 0x000BE9D0 File Offset: 0x000BCBD0
		public ExpressionBuilderContext(TemplateControl templateControl)
		{
			this.tcontrol = templateControl;
		}

		/// <summary>Provides an <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object with a reference to a <see cref="T:System.Web.UI.TemplateControl" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.TemplateControl" /> that contains this expression.</returns>
		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x000BE9DF File Offset: 0x000BCBDF
		public TemplateControl TemplateControl
		{
			get
			{
				return this.tcontrol;
			}
		}

		/// <summary>Returns a virtual path to the file associated with the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> object.</summary>
		/// <returns>The virtual path of the file associated with the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" />.</returns>
		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x000BE9E7 File Offset: 0x000BCBE7
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		// Token: 0x040024F8 RID: 9464
		private TemplateControl tcontrol;

		// Token: 0x040024F9 RID: 9465
		private string vpath;
	}
}
