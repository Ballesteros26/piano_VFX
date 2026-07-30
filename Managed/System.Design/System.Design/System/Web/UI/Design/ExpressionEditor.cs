using System;
using System.Configuration;
using System.Web.Compilation;
using System.Web.Configuration;

namespace System.Web.UI.Design
{
	/// <summary>Defines a set of properties and methods for evaluating an expression that is associated with a control property at design time and to provide an expression editor sheet to the visual design host for use in the expression editor dialog box. This class is abstract.</summary>
	// Token: 0x02000079 RID: 121
	public abstract class ExpressionEditor
	{
		/// <summary>Gets the expression prefix that identifies expression strings that are supported by the expression editor implementation.</summary>
		/// <returns>A string representing the prefix for expressions supported by the class derived from the <see cref="T:System.Web.UI.Design.ExpressionEditor" />; otherwise, an empty string (""), if the expression editor does not have an associated expression prefix.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00009075 File Offset: 0x00007275
		public string ExpressionPrefix
		{
			get
			{
				return this.prefixFromReflection;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00009080 File Offset: 0x00007280
		private Type ExpressionBuilderType
		{
			set
			{
				this.expressionBuilderType = value;
				this.prefixFromReflection = "";
				object[] customAttributes = this.expressionBuilderType.GetCustomAttributes(typeof(ExpressionPrefixAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					ExpressionPrefixAttribute expressionPrefixAttribute = (ExpressionPrefixAttribute)customAttributes[0];
					this.prefixFromReflection = expressionPrefixAttribute.ExpressionPrefix;
				}
			}
		}

		/// <summary>Evaluates an expression string and provides the design-time value for a control property.</summary>
		/// <returns>The object referenced by the evaluated expression string, if the expression evaluation succeeded; otherwise, null.</returns>
		/// <param name="expression">An expression string to evaluate. The expression does not include the expression prefix.</param>
		/// <param name="parseTimeData">An object containing additional parsing information for evaluating <paramref name="expression" />. This typically is provided by the expression builder.</param>
		/// <param name="propertyType">The type of the control property to which <paramref name="expression" /> is bound.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060003F1 RID: 1009
		public abstract object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider);

		/// <summary>Returns an <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation that is associated with the specified expression prefix.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation associated with <paramref name="expressionPrefix" />; otherwise, null, if <paramref name="expressionPrefix" /> is not defined or is not associated with an <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</returns>
		/// <param name="expressionPrefix">The expression prefix used to find the associated expression editor.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceProvider" /> is null.</exception>
		// Token: 0x060003F2 RID: 1010 RVA: 0x000090D4 File Offset: 0x000072D4
		public static ExpressionEditor GetExpressionEditor(string expressionPrefix, IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				return null;
			}
			IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
			if (webApplication == null)
			{
				return null;
			}
			Configuration configuration = webApplication.OpenWebConfiguration(true);
			if (configuration == null)
			{
				return null;
			}
			global::System.Web.Configuration.ExpressionBuilder expressionBuilder = ((CompilationSection)configuration.GetSection("system.web/compilation")).ExpressionBuilders[expressionPrefix];
			if (expressionBuilder == null)
			{
				return null;
			}
			return ExpressionEditor.GetExpressionEditor(Type.GetType(expressionBuilder.Type), serviceProvider);
		}

		/// <summary>Returns an <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation that is associated with the specified expression builder type.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation associated with <paramref name="expressionBuilderType" />; otherwise, null, if <paramref name="expressionBuilderType" /> cannot be located or has no associated <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</returns>
		/// <param name="expressionBuilderType">The type of the derived expression builder class, used to locate the associated expression editor.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expressionBuilderType" /> is null.- or -<paramref name="serviceProvider" /> is null.</exception>
		// Token: 0x060003F3 RID: 1011 RVA: 0x00009140 File Offset: 0x00007340
		[MonoTODO("the docs make it sound like this still requires accessing <expressionBuilders>")]
		public static ExpressionEditor GetExpressionEditor(Type expressionBuilderType, IServiceProvider serviceProvider)
		{
			object[] customAttributes = expressionBuilderType.GetCustomAttributes(typeof(ExpressionEditorAttribute), false);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			ExpressionEditor expressionEditor = (ExpressionEditor)Activator.CreateInstance(Type.GetType(((ExpressionEditorAttribute)customAttributes[0]).EditorTypeName));
			expressionEditor.ExpressionBuilderType = expressionBuilderType;
			return expressionEditor;
		}

		/// <summary>Returns an expression editor sheet that is associated with the current expression editor.</summary>
		/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> that defines the custom expression properties.</returns>
		/// <param name="expression">The expression string set for a control property, used to initialize the expression editor sheet.</param>
		/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400012C RID: 300
		private Type expressionBuilderType;

		// Token: 0x0400012D RID: 301
		private string prefixFromReflection;
	}
}
