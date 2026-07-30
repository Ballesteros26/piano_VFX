using System;
using System.CodeDom;
using System.Web.Routing;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Creates a URL that corresponds to specified URL parameter values.</summary>
	// Token: 0x02000669 RID: 1641
	[ExpressionEditor("System.Web.UI.Design.RouteUrlExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ExpressionPrefix("Routes")]
	public class RouteUrlExpressionBuilder : ExpressionBuilder
	{
		/// <summary>Gets a value that indicates whether an expression can be evaluated in a page that is not compiled.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		/// <summary>Creates a URL that corresponds to specified URL parameter values and to a route URL format.</summary>
		/// <returns>The URL that corresponds to the specified URL parameter values and to the selected route. The route is selected by matching route URL patterns to the specified list of parameters. In case more than one route matches a specified list of parameters, a route name can also be specified to indicate which route to select.</returns>
		/// <param name="target">Not used in this implementation.</param>
		/// <param name="entry">The property that the expression is bound to.</param>
		/// <param name="parsedData">Not used in this implementation.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x0600462F RID: 17967 RVA: 0x000C12ED File Offset: 0x000BF4ED
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			if (entry == null)
			{
				throw new NullReferenceException(".NET emulation (entry == null)");
			}
			if (context == null)
			{
				throw new NullReferenceException(".NET emulation (context == null)");
			}
			return RouteUrlExpressionBuilder.GetRouteUrl(context.TemplateControl, entry.Expression);
		}

		/// <summary>Returns a code expression that is used to perform the property assignment in the generated page class.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> instance that is used in the property assignment.</returns>
		/// <param name="entry">The property that the expression is bound to.</param>
		/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.RouteUrlExpressionBuilder.TryParseRouteExpression(System.String,System.Web.Routing.RouteValueDictionary,System.String@)" />.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x06004630 RID: 17968 RVA: 0x000C1320 File Offset: 0x000BF520
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			if (entry == null)
			{
				throw new NullReferenceException(".NET emulation (entry == null)");
			}
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(RouteUrlExpressionBuilder)), "GetRouteUrl");
			CodeExpressionCollection parameters = codeMethodInvokeExpression.Parameters;
			parameters.Add(new CodeThisReferenceExpression());
			parameters.Add(new CodePrimitiveExpression(entry.Expression));
			return codeMethodInvokeExpression;
		}

		/// <summary>Creates a URL that corresponds to specified route keys for a route URL format.</summary>
		/// <returns>The URL that corresponds to the route URL format of the current <see cref="T:System.Web.Routing.Route" /> object.</returns>
		/// <param name="control">The control that the expression is bound to.</param>
		/// <param name="expression">The expression as specified in markup.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="control" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method was unable to parse the expression that was specified in markup. For more information, see <see cref="M:System.Web.Compilation.RouteUrlExpressionBuilder.TryParseRouteExpression(System.String,System.Web.Routing.RouteValueDictionary,System.String@)" />.</exception>
		// Token: 0x06004631 RID: 17969 RVA: 0x000C1384 File Offset: 0x000BF584
		public static string GetRouteUrl(Control control, string expression)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			string text;
			if (!RouteUrlExpressionBuilder.TryParseRouteExpression(expression, routeValueDictionary, out text))
			{
				throw new InvalidOperationException("Invalid expression, RouteUrlExpressionBuilder expects a string with format: RouteName=route,Key1=Value1,Key2=Value2");
			}
			return control.GetRouteUrl(text, routeValueDictionary);
		}

		/// <summary>Parses an expression into a collection of route keys and values, and optionally into a route name.</summary>
		/// <returns>true if parsing was successful; otherwise, false.</returns>
		/// <param name="expression">The expression as specified in markup.</param>
		/// <param name="routeValues">The collection of route keys and their associated values.</param>
		/// <param name="routeName">When this method returns, contains a string that represents the name of the route, if <paramref name="expression" /> contains a route key named RouteName. This parameter is passed uninitialized.</param>
		// Token: 0x06004632 RID: 17970 RVA: 0x000C13C4 File Offset: 0x000BF5C4
		public static bool TryParseRouteExpression(string expression, RouteValueDictionary routeValues, out string routeName)
		{
			routeName = null;
			if (string.IsNullOrEmpty(expression))
			{
				return false;
			}
			if (routeValues == null)
			{
				throw new NullReferenceException(".NET emulation (routeValues == null)");
			}
			string[] array = expression.Split(RouteUrlExpressionBuilder.expressionSplitChars);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(RouteUrlExpressionBuilder.keyValueSplitChars);
				if (array2.Length != 2)
				{
					return false;
				}
				string text = array2[0].Trim();
				if (text == string.Empty)
				{
					return false;
				}
				if (string.Compare(text, "routename", StringComparison.OrdinalIgnoreCase) == 0)
				{
					routeName = array2[1].Trim();
				}
				else if (routeValues.ContainsKey(text))
				{
					routeValues[text] = array2[1].Trim();
				}
				else
				{
					routeValues.Add(text, array2[1].Trim());
				}
			}
			return true;
		}

		// Token: 0x0400252D RID: 9517
		private static readonly char[] expressionSplitChars = new char[] { ',' };

		// Token: 0x0400252E RID: 9518
		private static readonly char[] keyValueSplitChars = new char[] { '=' };
	}
}
