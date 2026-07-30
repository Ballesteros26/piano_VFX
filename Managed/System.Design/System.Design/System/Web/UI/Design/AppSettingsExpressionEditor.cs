using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides properties and methods for evaluating and editing an application setting expression in a configuration file at design time.</summary>
	// Token: 0x02000050 RID: 80
	public class AppSettingsExpressionEditor : ExpressionEditor
	{
		/// <summary>Evaluates an application setting expression string and provides the design-time value for a control property.</summary>
		/// <returns>The object referenced by <paramref name="expression" />, if the expression evaluation succeeded; otherwise, null.</returns>
		/// <param name="expression">An application setting expression string to evaluate. <paramref name="expression" /> does not include the AppSettings expression prefix.</param>
		/// <param name="parseTimeData">An object containing additional parsing information for evaluating <paramref name="expression" />.</param>
		/// <param name="propertyType">The control property type.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x0600029E RID: 670 RVA: 0x0000234B File Offset: 0x0000054B
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an expression editor sheet for an application setting expression.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> implementation that defines the application setting expression properties.</returns>
		/// <param name="expression">The expression string set for a control property, used to initialize the expression editor sheet. <paramref name="expression" /> does not include the AppSettings expression prefix.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x0600029F RID: 671 RVA: 0x0000234B File Offset: 0x0000054B
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}
	}
}
