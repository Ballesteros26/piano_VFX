using System;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000277 RID: 631
	internal class StyleValidator
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x000532DF File Offset: 0x000514DF
		public StyleValidator()
		{
			this.m_SyntaxParser = new StyleSyntaxParser();
			this.m_StyleMatcher = new StyleMatcher();
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00053300 File Offset: 0x00051500
		public StyleValidationResult ValidateProperty(string name, string value)
		{
			StyleValidationResult styleValidationResult = new StyleValidationResult
			{
				status = StyleValidationStatus.Ok
			};
			bool flag = name.StartsWith("--");
			StyleValidationResult styleValidationResult2;
			if (flag)
			{
				styleValidationResult2 = styleValidationResult;
			}
			else
			{
				string text;
				bool flag2 = !StylePropertyCache.TryGetSyntax(name, out text);
				if (flag2)
				{
					string text2 = StylePropertyCache.FindClosestPropertyName(name);
					styleValidationResult.status = StyleValidationStatus.Error;
					styleValidationResult.message = "Unknown property '" + name + "'";
					bool flag3 = !string.IsNullOrEmpty(text2);
					if (flag3)
					{
						styleValidationResult.message = styleValidationResult.message + " (did you mean '" + text2 + "'?)";
					}
					styleValidationResult2 = styleValidationResult;
				}
				else
				{
					Expression expression = this.m_SyntaxParser.Parse(text);
					bool flag4 = expression == null;
					if (flag4)
					{
						styleValidationResult.status = StyleValidationStatus.Error;
						styleValidationResult.message = string.Concat(new string[] { "Invalid '", name, "' property syntax '", text, "'" });
						styleValidationResult2 = styleValidationResult;
					}
					else
					{
						MatchResult matchResult = this.m_StyleMatcher.Match(expression, value);
						bool flag5 = !matchResult.success;
						if (flag5)
						{
							styleValidationResult.errorValue = matchResult.errorValue;
							switch (matchResult.errorCode)
							{
							case MatchResultErrorCode.Syntax:
							{
								styleValidationResult.status = StyleValidationStatus.Error;
								bool flag6 = this.IsUnitMissing(text, value);
								if (flag6)
								{
									styleValidationResult.hint = "Property expects a unit. Did you forget to add px or %?";
								}
								else
								{
									bool flag7 = this.IsUnsupportedColor(text);
									if (flag7)
									{
										styleValidationResult.hint = "Unsupported color '" + value + "'.";
									}
								}
								styleValidationResult.message = string.Concat(new string[] { "Expected (", text, ") but found '", matchResult.errorValue, "'" });
								break;
							}
							case MatchResultErrorCode.EmptyValue:
								styleValidationResult.status = StyleValidationStatus.Error;
								styleValidationResult.message = "Expected (" + text + ") but found empty value";
								break;
							case MatchResultErrorCode.ExpectedEndOfValue:
								styleValidationResult.status = StyleValidationStatus.Warning;
								styleValidationResult.message = "Expected end of value but found '" + matchResult.errorValue + "'";
								break;
							default:
								Debug.LogAssertion(string.Format("Unexpected error code '{0}'", matchResult.errorCode));
								break;
							}
						}
						styleValidationResult2 = styleValidationResult;
					}
				}
			}
			return styleValidationResult2;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00053544 File Offset: 0x00051744
		private bool IsUnitMissing(string propertySyntax, string propertyValue)
		{
			float num;
			return float.TryParse(propertyValue, ref num) && (propertySyntax.Contains("<length>") || propertySyntax.Contains("<percentage>"));
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00053580 File Offset: 0x00051780
		private bool IsUnsupportedColor(string propertySyntax)
		{
			return propertySyntax.StartsWith("<color>");
		}

		// Token: 0x04000944 RID: 2372
		private StyleSyntaxParser m_SyntaxParser;

		// Token: 0x04000945 RID: 2373
		private StyleMatcher m_StyleMatcher;
	}
}
