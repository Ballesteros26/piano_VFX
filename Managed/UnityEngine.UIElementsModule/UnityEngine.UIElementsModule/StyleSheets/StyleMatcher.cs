using System;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000272 RID: 626
	internal class StyleMatcher : BaseStyleMatcher
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000527D7 File Offset: 0x000509D7
		private string current
		{
			get
			{
				return base.hasCurrent ? this.m_PropertyParts[this.m_CurrentIndex] : null;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000527F1 File Offset: 0x000509F1
		public override int valueCount
		{
			get
			{
				return this.m_PropertyParts.Length;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000527FB File Offset: 0x000509FB
		public override bool isVariable
		{
			get
			{
				return base.hasCurrent && this.current.StartsWith("var(");
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00052818 File Offset: 0x00050A18
		private void Initialize(string propertyValue)
		{
			base.Initialize();
			this.m_PropertyParts = this.m_Parser.Parse(propertyValue);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00052834 File Offset: 0x00050A34
		public MatchResult Match(Expression exp, string propertyValue)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = string.IsNullOrEmpty(propertyValue);
			MatchResult matchResult2;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult2 = matchResult;
			}
			else
			{
				this.Initialize(propertyValue);
				string current = this.current;
				bool flag2 = current == "initial" || current.StartsWith("env(");
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = this.current;
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = this.current;
					}
				}
				matchResult2 = matchResult;
			}
			return matchResult2;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00052908 File Offset: 0x00050B08
		protected override bool MatchKeyword(string keyword)
		{
			return this.current != null && keyword == this.current.ToLower();
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00052938 File Offset: 0x00050B38
		protected override bool MatchNumber()
		{
			string current = this.current;
			Match match = StyleMatcher.s_NumberRegex.Match(current);
			return match.Success;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00052964 File Offset: 0x00050B64
		protected override bool MatchInteger()
		{
			string current = this.current;
			Match match = StyleMatcher.s_IntegerRegex.Match(current);
			return match.Success;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00052990 File Offset: 0x00050B90
		protected override bool MatchLength()
		{
			string current = this.current;
			Match match = StyleMatcher.s_LengthRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000529D8 File Offset: 0x00050BD8
		protected override bool MatchPercentage()
		{
			string current = this.current;
			Match match = StyleMatcher.s_PercentRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00052A20 File Offset: 0x00050C20
		protected override bool MatchColor()
		{
			string current = this.current;
			Match match = StyleMatcher.s_HexColorRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_RgbRegex.Match(current);
				bool success2 = match.Success;
				if (success2)
				{
					flag = true;
				}
				else
				{
					match = StyleMatcher.s_RgbaRegex.Match(current);
					bool success3 = match.Success;
					if (success3)
					{
						flag = true;
					}
					else
					{
						Color clear = Color.clear;
						bool flag2 = StyleSheetColor.TryGetColor(current, out clear);
						flag = flag2;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00052AAC File Offset: 0x00050CAC
		protected override bool MatchResource()
		{
			string current = this.current;
			Match match = StyleMatcher.s_ResourceRegex.Match(current);
			bool flag = !match.Success;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(text);
				flag2 = !match.Success;
			}
			return flag2;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00052B14 File Offset: 0x00050D14
		protected override bool MatchUrl()
		{
			string current = this.current;
			Match match = StyleMatcher.s_UrlRegex.Match(current);
			bool flag = !match.Success;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(text);
				flag2 = !match.Success;
			}
			return flag2;
		}

		// Token: 0x0400092A RID: 2346
		private StylePropertyValueParser m_Parser = new StylePropertyValueParser();

		// Token: 0x0400092B RID: 2347
		private string[] m_PropertyParts;

		// Token: 0x0400092C RID: 2348
		private static readonly Regex s_NumberRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?$", 8);

		// Token: 0x0400092D RID: 2349
		private static readonly Regex s_IntegerRegex = new Regex("^[+-]?\\d+$", 8);

		// Token: 0x0400092E RID: 2350
		private static readonly Regex s_ZeroRegex = new Regex("^0(?:\\.0+)?$", 8);

		// Token: 0x0400092F RID: 2351
		private static readonly Regex s_LengthRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:px)$", 8);

		// Token: 0x04000930 RID: 2352
		private static readonly Regex s_PercentRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:%)$", 8);

		// Token: 0x04000931 RID: 2353
		private static readonly Regex s_HexColorRegex = new Regex("^#[a-fA-F0-9]{3}(?:[a-fA-F0-9]{3})?$", 8);

		// Token: 0x04000932 RID: 2354
		private static readonly Regex s_RgbRegex = new Regex("^rgb\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)$", 8);

		// Token: 0x04000933 RID: 2355
		private static readonly Regex s_RgbaRegex = new Regex("rgba\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*([\\d.]+)\\s*\\)$", 8);

		// Token: 0x04000934 RID: 2356
		private static readonly Regex s_VarFunctionRegex = new Regex("^var\\(.+\\)$", 8);

		// Token: 0x04000935 RID: 2357
		private static readonly Regex s_ResourceRegex = new Regex("^resource\\((.+)\\)$", 8);

		// Token: 0x04000936 RID: 2358
		private static readonly Regex s_UrlRegex = new Regex("^url\\((.+)\\)$", 8);
	}
}
