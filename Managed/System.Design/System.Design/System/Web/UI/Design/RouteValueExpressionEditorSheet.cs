using System;
using Unity;

namespace System.Web.UI.Design
{
	/// <summary>Represents a design-time editor sheet for the properties of a RouteValue expression in the UI of a designer host at design time.</summary>
	// Token: 0x0200017D RID: 381
	public class RouteValueExpressionEditorSheet : ExpressionEditorSheet
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.RouteValueExpressionEditorSheet" /> class.</summary>
		/// <param name="expression">A RouteValue expression, used to initialize the expression editor sheet.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services</param>
		// Token: 0x06000B0C RID: 2828 RVA: 0x00009519 File Offset: 0x00007719
		public RouteValueExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the URL parameter to be evaluated by the RouteValue expression.</summary>
		/// <returns>The name of the URL parameter to be evaluated by the RouteValue expression.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00009519 File Offset: 0x00007719
		public string RouteValue
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns a RouteValue expression that is formed by the expression editor sheet property values.</summary>
		/// <returns>The RouteValue expression string for the current settings in the sheet.</returns>
		// Token: 0x06000B0F RID: 2831 RVA: 0x0000970B File Offset: 0x0000790B
		public override string GetExpression()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
