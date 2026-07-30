using System;
using System.CodeDom;
using System.ComponentModel;
using System.Web.Routing;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Retrieves the value that corresponds to a specified URL parameter in a routed page. </summary>
	// Token: 0x0200066A RID: 1642
	[ExpressionEditor("System.Web.UI.Design.RouteValueExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ExpressionPrefix("Routes")]
	public class RouteValueExpressionBuilder : ExpressionBuilder
	{
		/// <summary>Gets a value that indicates whether an expression can be evaluated in a page that is not compiled.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		/// <summary>Retrieves the value that corresponds to a specified route key.</summary>
		/// <returns>The value that corresponds to the URL parameter that is specified for the current page. The method returns null if <paramref name="target" /> is null or if it does not derive from <see cref="T:System.Web.UI.Control" />.</returns>
		/// <param name="target">The control that the expression is bound to.</param>
		/// <param name="entry">The property that the expression is bound to.</param>
		/// <param name="parsedData">(This parameter is not used in this implementation.)</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x06004636 RID: 17974 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a code expression that is used to perform the property assignment in the generated page class.</summary>
		/// <returns>An expression.</returns>
		/// <param name="entry">The property that the expression is bound to.</param>
		/// <param name="parsedData">The object that represents parsed data, as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x06004637 RID: 17975 RVA: 0x000C149C File Offset: 0x000BF69C
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			if (entry == null)
			{
				throw new NullReferenceException(".NET emulation (entry == null)");
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(RouteValueExpressionBuilder)), "GetRouteValue");
			CodeThisReferenceExpression codeThisReferenceExpression = new CodeThisReferenceExpression();
			CodeExpressionCollection parameters = codeMethodInvokeExpression.Parameters;
			parameters.Add(new CodePropertyReferenceExpression(codeThisReferenceExpression, "Page"));
			parameters.Add(new CodePrimitiveExpression(entry.Expression));
			parameters.Add(new CodeTypeOfExpression(new CodeTypeReference(entry.DeclaringType)));
			parameters.Add(new CodePrimitiveExpression(entry.Name));
			return codeMethodInvokeExpression;
		}

		/// <summary>Retrieves the value that corresponds to the specified URL parameter.</summary>
		/// <returns>The value that corresponds to the specified URL parameter for the current page. If <paramref name="page" /> is null, if the <see cref="P:System.Web.UI.Page.RouteData" /> property of <paramref name="page" /> is null, or if <paramref name="key" /> is empty or null, the method returns null.</returns>
		/// <param name="page">The current page.</param>
		/// <param name="key">The URL parameter.</param>
		/// <param name="controlType">The type of the control that the expression is bound to.</param>
		/// <param name="propertyName">The name of the property that is being set by the expression.</param>
		// Token: 0x06004638 RID: 17976 RVA: 0x000C1534 File Offset: 0x000BF734
		public static object GetRouteValue(Page page, string key, Type controlType, string propertyName)
		{
			RouteData routeData = ((page != null) ? page.RouteData : null);
			if (routeData == null || string.IsNullOrEmpty(key))
			{
				return null;
			}
			object obj = routeData.Values[key];
			if (obj == null)
			{
				return null;
			}
			if (controlType == null || string.IsNullOrEmpty(propertyName) || !(obj is string))
			{
				return obj;
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(controlType);
			if (properties == null || properties.Count == 0)
			{
				return obj;
			}
			PropertyDescriptor propertyDescriptor = properties[propertyName];
			if (propertyDescriptor == null)
			{
				return obj;
			}
			TypeConverter converter = propertyDescriptor.Converter;
			if (converter == null || !converter.CanConvertFrom(typeof(string)))
			{
				return obj;
			}
			return converter.ConvertFrom(obj);
		}
	}
}
