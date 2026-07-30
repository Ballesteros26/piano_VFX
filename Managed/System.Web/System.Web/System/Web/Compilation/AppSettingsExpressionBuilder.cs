using System;
using System.CodeDom;
using System.ComponentModel;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Retrieves values, as specified in a declarative expression, from the &lt;appSettings&gt; section of the Web.config file.</summary>
	// Token: 0x02000617 RID: 1559
	[ExpressionPrefix("AppSettings")]
	[ExpressionEditor("System.Web.UI.Design.AppSettingsExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class AppSettingsExpressionBuilder : ExpressionBuilder
	{
		/// <summary>Returns a value from the &lt;appSettings&gt; section of the Web.config file.</summary>
		/// <returns>The <see cref="T:System.Object" /> associated with a key in the &lt;appSettings&gt; section of the Web.config file.</returns>
		/// <param name="target">The object that contains the property entry.</param>
		/// <param name="entry">The property to which the expression is bound..</param>
		/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x06004319 RID: 17177 RVA: 0x000B2F04 File Offset: 0x000B1104
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return AppSettingsExpressionBuilder.GetAppSetting(entry.Expression.Trim());
		}

		/// <summary>Returns a value from the &lt;appSettings&gt; section of the Web.config file.</summary>
		/// <returns>The <see cref="T:System.Object" /> associated with the key in the &lt;appSettings&gt; section of the Web.config file.</returns>
		/// <param name="key">The key for the value to be retrieved from the configuration file. </param>
		/// <exception cref="T:System.InvalidOperationException">The key is not found in Web.config.</exception>
		// Token: 0x0600431A RID: 17178 RVA: 0x000B2F16 File Offset: 0x000B1116
		public static object GetAppSetting(string key)
		{
			string text = WebConfigurationManager.AppSettings[key];
			if (text == null)
			{
				throw new InvalidOperationException(string.Format("The application setting '{0}' was not found.", key));
			}
			return text;
		}

		/// <summary>Returns a value from the &lt;appSettings&gt; section of the Web.config file with the value converted to a target type.</summary>
		/// <returns>The <see cref="T:System.Object" /> associated with the key in the &lt;appSettings&gt; section of the Web.config file.</returns>
		/// <param name="key">The key for a value to be retrieved from the configuration file.</param>
		/// <param name="targetType">The type of the object that contains the property entry.</param>
		/// <param name="propertyName">The name of the property to which the expression is bound.</param>
		/// <exception cref="T:System.InvalidOperationException">The key is not found in Web.config.- or -The return value could not be converted.</exception>
		// Token: 0x0600431B RID: 17179 RVA: 0x000B2F38 File Offset: 0x000B1138
		public static object GetAppSetting(string key, Type targetType, string propertyName)
		{
			object appSetting = AppSettingsExpressionBuilder.GetAppSetting(key);
			if (targetType == null)
			{
				return appSetting.ToString();
			}
			PropertyInfo property = targetType.GetProperty(propertyName);
			if (property == null)
			{
				return appSetting.ToString();
			}
			object obj;
			try
			{
				obj = TypeDescriptor.GetConverter(property.PropertyType).ConvertFrom(appSetting);
			}
			catch (NotSupportedException)
			{
				throw new InvalidOperationException(string.Format("Could not convert application setting '{0}'  to type '{1}' for property '{2}'.", appSetting, property.PropertyType.Name, property.Name));
			}
			return obj;
		}

		/// <summary>Returns a code expression that is used to perform the property assignment in the generated page class.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that is used in the property assignment.</returns>
		/// <param name="entry">The property to which the expression is bound.</param>
		/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x0600431C RID: 17180 RVA: 0x000B2FC0 File Offset: 0x000B11C0
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(entry.DeclaringType)[entry.PropertyInfo.Name];
			CodeExpression[] array = new CodeExpression[]
			{
				new CodePrimitiveExpression(entry.Expression.Trim()),
				new CodeTypeOfExpression(entry.Type),
				new CodePrimitiveExpression(entry.Name)
			};
			return new CodeCastExpression(propertyDescriptor.PropertyType, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetAppSetting", array));
		}

		/// <summary>Returns a value indicating whether an expression can be evaluated in a page that is not compiled.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}
	}
}
