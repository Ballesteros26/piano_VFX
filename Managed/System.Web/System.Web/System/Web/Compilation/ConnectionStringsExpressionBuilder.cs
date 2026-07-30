using System;
using System.CodeDom;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Retrieves, or generates code to retrieve, values from the &lt;connectionStrings&gt; section of the Web.config file.</summary>
	// Token: 0x0200064E RID: 1614
	[ExpressionEditor("System.Web.UI.Design.ConnectionStringsExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ExpressionPrefix("ConnectionStrings")]
	public class ConnectionStringsExpressionBuilder : ExpressionBuilder
	{
		/// <summary>Returns a value from the &lt;connectionStrings&gt; section of the Web.config file.</summary>
		/// <returns>The <see cref="T:System.Object" /> associated with a key in the &lt;connectionStrings&gt; section of the Web.config file.</returns>
		/// <param name="target">The object that contains the expression.</param>
		/// <param name="entry">The property to which the expression is bound.</param>
		/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ConnectionStringsExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Properties for the control or page.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection string name could not be found in the Web.config file.</exception>
		// Token: 0x06004566 RID: 17766 RVA: 0x000BDFE4 File Offset: 0x000BC1E4
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return ConnectionStringsExpressionBuilder.GetConnectionString(entry.Expression.Trim());
		}

		/// <summary>Returns a code expression to evaluate during page parsing.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that invokes a method.</returns>
		/// <param name="entry">An object that represents information about the property bound to by the expression.</param>
		/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ConnectionStringsExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x06004567 RID: 17767 RVA: 0x000BDFF8 File Offset: 0x000BC1F8
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			Pair pair = parsedData as Pair;
			return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(ConnectionStringsExpressionBuilder)), ((bool)pair.Second) ? "GetConnectionStringProviderName" : "GetConnectionString", new CodeExpression[]
			{
				new CodePrimitiveExpression(pair.First)
			});
		}

		/// <summary>Returns a connection string from the &lt;connectionStrings&gt; section of the Web.config file.</summary>
		/// <returns>The connection string as a <see cref="T:System.String" /> for this connection string name.</returns>
		/// <param name="connectionStringName">The name of the connection string.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection string name could not be found in the Web.config file.</exception>
		// Token: 0x06004568 RID: 17768 RVA: 0x000BE050 File Offset: 0x000BC250
		public static string GetConnectionString(string connectionStringName)
		{
			ConnectionStringSettings connectionStringSettings = WebConfigurationManager.ConnectionStrings[connectionStringName];
			if (connectionStringSettings == null)
			{
				return string.Empty;
			}
			return connectionStringSettings.ConnectionString;
		}

		/// <summary>Returns the connection string provider from the &lt;connectionStrings&gt; section of the Web.config file.</summary>
		/// <returns>The provider as a <see cref="T:System.String" /> for this connection string name.</returns>
		/// <param name="connectionStringName">The name of the connection string.</param>
		/// <exception cref="T:System.InvalidOperationException">The connection string name could not be found in the Web.config file.</exception>
		// Token: 0x06004569 RID: 17769 RVA: 0x000BE078 File Offset: 0x000BC278
		public static string GetConnectionStringProviderName(string connectionStringName)
		{
			ConnectionStringSettings connectionStringSettings = WebConfigurationManager.ConnectionStrings[connectionStringName];
			if (connectionStringSettings == null)
			{
				return string.Empty;
			}
			return connectionStringSettings.ProviderName;
		}

		/// <summary>Returns an object that represents the parsed expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the parsed representation of the expression.</returns>
		/// <param name="expression">The value of the declarative expression.</param>
		/// <param name="propertyType">The targeted type for the expression.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x0600456A RID: 17770 RVA: 0x000BE0A0 File Offset: 0x000BC2A0
		public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			bool flag = false;
			string text = string.Empty;
			if (!string.IsNullOrEmpty(expression))
			{
				int num = expression.Length;
				if (expression.EndsWith(".providername", StringComparison.InvariantCultureIgnoreCase))
				{
					flag = true;
					num -= 13;
				}
				else if (expression.EndsWith(".connectionstring", StringComparison.InvariantCultureIgnoreCase))
				{
					num -= 17;
				}
				text = expression.Substring(0, num);
			}
			return new Pair(text, flag);
		}

		/// <summary>Returns a value indicating whether an expression can be evaluated in a page that is not compiled.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}
	}
}
