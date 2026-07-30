using System;
using System.ComponentModel;

namespace System.Web.UI.Design
{
	/// <summary>Represents a design-time editor sheet for a custom expression. This class must be inherited.</summary>
	// Token: 0x0200007A RID: 122
	public abstract class ExpressionEditorSheet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> class.</summary>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000918B File Offset: 0x0000738B
		protected ExpressionEditorSheet(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <summary>When overridden in a derived class, returns the expression string that is formed by the expression editor sheet property values.</summary>
		/// <returns>The custom expression string for the current property values.</returns>
		// Token: 0x060003F6 RID: 1014
		public abstract string GetExpression();

		/// <summary>Gets a value that indicates whether the expression string is valid.</summary>
		/// <returns>true, if the expression string is valid; otherwise false.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000234B File Offset: 0x0000054B
		[Browsable(false)]
		public virtual bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the service provider implementation that is used by the expression editor sheet.</summary>
		/// <returns>An <see cref="T:System.IServiceProvider" />, typically provided by the design host, that can be used to obtain additional design-time services.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000919A File Offset: 0x0000739A
		[Browsable(false)]
		public IServiceProvider ServiceProvider
		{
			get
			{
				return this.serviceProvider;
			}
		}

		// Token: 0x0400012E RID: 302
		private IServiceProvider serviceProvider;
	}
}
