using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for validation controls that perform typed comparisons. </summary>
	// Token: 0x02000335 RID: 821
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseCompareValidator : BaseValidator
	{
		/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001C84 RID: 7300 RVA: 0x00047300 File Offset: 0x00045500
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (base.RenderUplevel && this.Page != null)
			{
				base.RegisterExpandoAttribute(this.ClientID, "type", this.Type.ToString());
				ValidationDataType type = this.Type;
				if (type != ValidationDataType.Date)
				{
					if (type == ValidationDataType.Currency)
					{
						NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
						base.RegisterExpandoAttribute(this.ClientID, "decimalchar", numberFormat.CurrencyDecimalSeparator, true);
						base.RegisterExpandoAttribute(this.ClientID, "groupchar", numberFormat.CurrencyGroupSeparator, true);
						base.RegisterExpandoAttribute(this.ClientID, "digits", numberFormat.CurrencyDecimalDigits.ToString());
						base.RegisterExpandoAttribute(this.ClientID, "groupsize", numberFormat.CurrencyGroupSizes[0].ToString());
					}
				}
				else
				{
					DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
					string shortDatePattern = dateTimeFormat.ShortDatePattern;
					string text = (shortDatePattern.StartsWith("y", true, Helpers.InvariantCulture) ? "ymd" : (shortDatePattern.StartsWith("m", true, Helpers.InvariantCulture) ? "mdy" : "dmy"));
					base.RegisterExpandoAttribute(this.ClientID, "dateorder", text);
					base.RegisterExpandoAttribute(this.ClientID, "cutoffyear", dateTimeFormat.Calendar.TwoDigitYearMax.ToString());
				}
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Determines whether the specified string can be converted to the specified data type. This version of the overloaded method tests currency, double, and date values using the format used by the current culture.</summary>
		/// <returns>true if the specified data string can be converted to the specified data type; otherwise, false.</returns>
		/// <param name="text">The string to test.</param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
		// Token: 0x06001C85 RID: 7301 RVA: 0x00047470 File Offset: 0x00045670
		public static bool CanConvert(string text, ValidationDataType type)
		{
			object obj;
			return BaseCompareValidator.Convert(text, type, out obj);
		}

		/// <summary>Converts the specified text into an object of the specified data type. This version of the overloaded method converts currency, double, and date values using the format used by the current culture.</summary>
		/// <returns>true if the conversion is successful; otherwise, false.</returns>
		/// <param name="text">The text to convert.</param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
		/// <param name="value">When this method returns, contains an object with the conversion result. This parameter is passed uninitialized.</param>
		// Token: 0x06001C86 RID: 7302 RVA: 0x00047486 File Offset: 0x00045686
		protected static bool Convert(string text, ValidationDataType type, out object value)
		{
			return BaseCompareValidator.Convert(text, type, false, out value);
		}

		/// <summary>Compares two strings using the specified operator and data type. This version of the overloaded method compares currency, double, and date values using the format used by the current culture.</summary>
		/// <returns>true if the <paramref name="leftValue" /> parameter relates to the <paramref name="rightValue" /> parameter in the manner specified by the <paramref name="op" /> parameter; otherwise, false.</returns>
		/// <param name="leftText">The value on the left side of the operator.</param>
		/// <param name="rightText">The value on the right side of the operator.</param>
		/// <param name="op">One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values. </param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
		// Token: 0x06001C87 RID: 7303 RVA: 0x00047491 File Offset: 0x00045691
		protected static bool Compare(string leftText, string rightText, ValidationCompareOperator op, ValidationDataType type)
		{
			return BaseCompareValidator.Compare(leftText, false, rightText, false, op, type);
		}

		/// <summary>Determines whether the validation control can be rendered for a newer ("uplevel") browser.</summary>
		/// <returns>true if the validation control can be rendered for an "uplevel" browser; otherwise, false.</returns>
		// Token: 0x06001C88 RID: 7304 RVA: 0x0004749E File Offset: 0x0004569E
		protected override bool DetermineRenderUplevel()
		{
			return base.DetermineRenderUplevel();
		}

		/// <summary>Determines the order in which the month, day, and year appear in a date value for the current culture.</summary>
		/// <returns>A string that represents the order in which the month, day, and year appear in a date value for the current culture.</returns>
		// Token: 0x06001C89 RID: 7305 RVA: 0x000474A8 File Offset: 0x000456A8
		protected static string GetDateElementOrder()
		{
			string text = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (char c in text.ToLower(Helpers.InvariantCulture))
			{
				if (c == 'm' || c == 'd' || c == 'y')
				{
					if (c == 'm')
					{
						if (!flag3)
						{
							stringBuilder.Append("m");
						}
						flag3 = true;
					}
					else if (c == 'y')
					{
						if (!flag2)
						{
							stringBuilder.Append("y");
						}
						flag2 = true;
					}
					else
					{
						if (!flag)
						{
							stringBuilder.Append("d");
						}
						flag = true;
					}
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Generates the four-digit year representation of the specified two-digit year.</summary>
		/// <returns>The four-digit year representation of the specified two-digit year.</returns>
		/// <param name="shortYear">A two-digit year.</param>
		// Token: 0x06001C8A RID: 7306 RVA: 0x00047560 File Offset: 0x00045760
		protected static int GetFullYear(int shortYear)
		{
			int cutoffYear = BaseCompareValidator.CutoffYear;
			int num = cutoffYear % 100;
			if (shortYear <= num)
			{
				return cutoffYear - num + shortYear;
			}
			return cutoffYear - num - 100 + shortYear;
		}

		/// <summary>Gets or sets a value indicating whether values are converted to a culture-neutral format before being compared.</summary>
		/// <returns>true to convert values to a culture-neutral format before they are compared; otherwise, false.The default is false.</returns>
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0004758A File Offset: 0x0004578A
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0004759D File Offset: 0x0004579D
		[Themeable(false)]
		[DefaultValue(false)]
		public bool CultureInvariantValues
		{
			get
			{
				return this.ViewState.GetBool("CultureInvariantValues", false);
			}
			set
			{
				this.ViewState["CultureInvariantValues"] = value;
			}
		}

		/// <summary>Gets the maximum year that can be represented by a two-digit year.</summary>
		/// <returns>The maximum year that can be represented by a two-digit year.</returns>
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x000475B5 File Offset: 0x000457B5
		protected static int CutoffYear
		{
			get
			{
				return CultureInfo.CurrentCulture.Calendar.TwoDigitYearMax;
			}
		}

		/// <summary>Gets or sets the data type that the values being compared are converted to before the comparison is made.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> enumeration values. The default value is String.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified data type is not one of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</exception>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x000475C6 File Offset: 0x000457C6
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x000475F1 File Offset: 0x000457F1
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue(ValidationDataType.String)]
		public ValidationDataType Type
		{
			get
			{
				if (this.ViewState["Type"] != null)
				{
					return (ValidationDataType)this.ViewState["Type"];
				}
				return ValidationDataType.String;
			}
			set
			{
				this.ViewState["Type"] = value;
			}
		}

		/// <summary>Determines whether the specified string can be converted to the specified data type. This version of the overloaded method allows you to specify whether values are tested using a culture-neutral format.</summary>
		/// <returns>true if the specified data string can be converted to the specified data type; otherwise, false.</returns>
		/// <param name="text">The string to test.</param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> enumeration values.</param>
		/// <param name="cultureInvariant">true to test values using a culture-neutral format; otherwise, false.</param>
		// Token: 0x06001C90 RID: 7312 RVA: 0x0004760C File Offset: 0x0004580C
		public static bool CanConvert(string text, ValidationDataType type, bool cultureInvariant)
		{
			object obj;
			return BaseCompareValidator.Convert(text, type, cultureInvariant, out obj);
		}

		/// <summary>Compares two strings using the specified operator and validation data type. This version of the overload allows you to specify whether values are compared using a culture-neutral format.</summary>
		/// <returns>true if the <paramref name="leftValue" /> parameter relates to the <paramref name="rightValue" /> parameter in the manner specified by the <paramref name="op" /> parameter; otherwise, false.</returns>
		/// <param name="leftText">The value on the left side of the operator.</param>
		/// <param name="cultureInvariantLeftText">true to convert the left side value to a culture-neutral format; otherwise, false.</param>
		/// <param name="rightText">The value on the right side of the operator.</param>
		/// <param name="cultureInvariantRightText">true to convert the right side value to a culture-neutral format; otherwise, false.</param>
		/// <param name="op">One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" /> values.</param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
		// Token: 0x06001C91 RID: 7313 RVA: 0x00047624 File Offset: 0x00045824
		protected static bool Compare(string leftText, bool cultureInvariantLeftText, string rightText, bool cultureInvariantRightText, ValidationCompareOperator op, ValidationDataType type)
		{
			object obj;
			if (!BaseCompareValidator.Convert(leftText, type, cultureInvariantLeftText, out obj))
			{
				return false;
			}
			if (op == ValidationCompareOperator.DataTypeCheck)
			{
				return true;
			}
			object obj2;
			if (!BaseCompareValidator.Convert(rightText, type, cultureInvariantRightText, out obj2))
			{
				return true;
			}
			int num = ((IComparable)obj).CompareTo((IComparable)obj2);
			switch (op)
			{
			case ValidationCompareOperator.Equal:
				return num == 0;
			case ValidationCompareOperator.NotEqual:
				return num != 0;
			case ValidationCompareOperator.GreaterThan:
				return num > 0;
			case ValidationCompareOperator.GreaterThanEqual:
				return num >= 0;
			case ValidationCompareOperator.LessThan:
				return num < 0;
			case ValidationCompareOperator.LessThanEqual:
				return num <= 0;
			default:
				return false;
			}
		}

		/// <summary>Converts the specified text into an object of the specified data type. This version of the overloaded method allows you to specify whether values are converted using a culture-neutral format.</summary>
		/// <returns>true if the conversion is successful; otherwise, false.</returns>
		/// <param name="text">The text to convert.</param>
		/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.ValidationDataType" /> values.</param>
		/// <param name="cultureInvariant">true to convert values to a culture-neutral format; otherwise, false.</param>
		/// <param name="value">When this method returns, contains an object with the conversion result. This parameter is passed uninitialized.</param>
		// Token: 0x06001C92 RID: 7314 RVA: 0x000476B0 File Offset: 0x000458B0
		protected static bool Convert(string text, ValidationDataType type, bool cultureInvariant, out object value)
		{
			bool flag;
			try
			{
				switch (type)
				{
				case ValidationDataType.String:
					value = text;
					flag = value != null;
					break;
				case ValidationDataType.Integer:
				{
					IFormatProvider formatProvider = (cultureInvariant ? NumberFormatInfo.InvariantInfo : NumberFormatInfo.CurrentInfo);
					value = int.Parse(text, formatProvider);
					flag = true;
					break;
				}
				case ValidationDataType.Double:
				{
					IFormatProvider formatProvider2 = (cultureInvariant ? NumberFormatInfo.InvariantInfo : NumberFormatInfo.CurrentInfo);
					value = double.Parse(text, formatProvider2);
					flag = true;
					break;
				}
				case ValidationDataType.Date:
				{
					IFormatProvider formatProvider3 = (cultureInvariant ? DateTimeFormatInfo.InvariantInfo : DateTimeFormatInfo.CurrentInfo);
					value = DateTime.Parse(text, formatProvider3);
					flag = true;
					break;
				}
				case ValidationDataType.Currency:
				{
					IFormatProvider formatProvider4 = (cultureInvariant ? NumberFormatInfo.InvariantInfo : NumberFormatInfo.CurrentInfo);
					value = decimal.Parse(text, NumberStyles.Currency, formatProvider4);
					flag = true;
					break;
				}
				default:
					value = null;
					flag = false;
					break;
				}
			}
			catch
			{
				value = null;
				flag = false;
			}
			return flag;
		}
	}
}
