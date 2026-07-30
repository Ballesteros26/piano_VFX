using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200052A RID: 1322
	internal class NumberAction : ContainerAction
	{
		// Token: 0x0600352B RID: 13611 RVA: 0x0012B860 File Offset: 0x00129A60
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string text = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Level))
			{
				if (text != "any" && text != "multiple" && text != "single")
				{
					throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "level", text });
				}
				this.level = text;
			}
			else if (Ref.Equal(localName, compiler.Atoms.Count))
			{
				this.countPattern = text;
				this.countKey = compiler.AddQuery(text, true, true, true);
			}
			else if (Ref.Equal(localName, compiler.Atoms.From))
			{
				this.from = text;
				this.fromKey = compiler.AddQuery(text, true, true, true);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Value))
			{
				this.value = text;
				this.valueKey = compiler.AddQuery(text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Format))
			{
				this.formatAvt = Avt.CompileAvt(compiler, text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Lang))
			{
				this.langAvt = Avt.CompileAvt(compiler, text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.LetterValue))
			{
				this.letterAvt = Avt.CompileAvt(compiler, text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.GroupingSeparator))
			{
				this.groupingSepAvt = Avt.CompileAvt(compiler, text);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.GroupingSize))
				{
					return false;
				}
				this.groupingSizeAvt = Avt.CompileAvt(compiler, text);
			}
			return true;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x0012BA24 File Offset: 0x00129C24
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckEmpty(compiler);
			this.forwardCompatibility = compiler.ForwardCompatibility;
			this.formatTokens = NumberAction.ParseFormat(CompiledAction.PrecalculateAvt(ref this.formatAvt));
			this.letter = this.ParseLetter(CompiledAction.PrecalculateAvt(ref this.letterAvt));
			this.lang = CompiledAction.PrecalculateAvt(ref this.langAvt);
			this.groupingSep = CompiledAction.PrecalculateAvt(ref this.groupingSepAvt);
			if (this.groupingSep != null && this.groupingSep.Length > 1)
			{
				throw XsltException.Create("The value of the '{0}' attribute must be a single character.", new string[] { "grouping-separator" });
			}
			this.groupingSize = CompiledAction.PrecalculateAvt(ref this.groupingSizeAvt);
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0012BADC File Offset: 0x00129CDC
		private int numberAny(Processor processor, ActionFrame frame)
		{
			int num = 0;
			XPathNavigator xpathNavigator = frame.Node;
			if (xpathNavigator.NodeType == XPathNodeType.Attribute || xpathNavigator.NodeType == XPathNodeType.Namespace)
			{
				xpathNavigator = xpathNavigator.Clone();
				xpathNavigator.MoveToParent();
			}
			XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
			if (this.fromKey != -1)
			{
				bool flag = false;
				while (!processor.Matches(xpathNavigator2, this.fromKey))
				{
					if (!xpathNavigator2.MoveToParent())
					{
						IL_0056:
						XPathNodeIterator xpathNodeIterator = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
						while (xpathNodeIterator.MoveNext())
						{
							if (processor.Matches(xpathNodeIterator.Current, this.fromKey))
							{
								flag = true;
								num = 0;
							}
							else if (this.MatchCountKey(processor, frame.Node, xpathNodeIterator.Current))
							{
								num++;
							}
							if (xpathNodeIterator.Current.IsSamePosition(xpathNavigator))
							{
								break;
							}
						}
						if (!flag)
						{
							return 0;
						}
						return num;
					}
				}
				flag = true;
				goto IL_0056;
			}
			xpathNavigator2.MoveToRoot();
			XPathNodeIterator xpathNodeIterator2 = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
			while (xpathNodeIterator2.MoveNext())
			{
				if (this.MatchCountKey(processor, frame.Node, xpathNodeIterator2.Current))
				{
					num++;
				}
				if (xpathNodeIterator2.Current.IsSamePosition(xpathNavigator))
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x0012BBE6 File Offset: 0x00129DE6
		private bool checkFrom(Processor processor, XPathNavigator nav)
		{
			if (this.fromKey == -1)
			{
				return true;
			}
			while (!processor.Matches(nav, this.fromKey))
			{
				if (!nav.MoveToParent())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x0012BC0D File Offset: 0x00129E0D
		private bool moveToCount(XPathNavigator nav, Processor processor, XPathNavigator contextNode)
		{
			while (this.fromKey == -1 || !processor.Matches(nav, this.fromKey))
			{
				if (this.MatchCountKey(processor, contextNode, nav))
				{
					return true;
				}
				if (!nav.MoveToParent())
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x0012BC40 File Offset: 0x00129E40
		private int numberCount(XPathNavigator nav, Processor processor, XPathNavigator contextNode)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			int num = 1;
			if (xpathNavigator.MoveToParent())
			{
				xpathNavigator.MoveToFirstChild();
				while (!xpathNavigator.IsSamePosition(nav))
				{
					if (this.MatchCountKey(processor, contextNode, xpathNavigator))
					{
						num++;
					}
					if (!xpathNavigator.MoveToNext())
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x0012BC88 File Offset: 0x00129E88
		private static object SimplifyValue(object value)
		{
			if (Type.GetTypeCode(value.GetType()) == TypeCode.Object)
			{
				XPathNodeIterator xpathNodeIterator = value as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					if (xpathNodeIterator.MoveNext())
					{
						return xpathNodeIterator.Current.Value;
					}
					return string.Empty;
				}
				else
				{
					XPathNavigator xpathNavigator = value as XPathNavigator;
					if (xpathNavigator != null)
					{
						return xpathNavigator.Value;
					}
				}
			}
			return value;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x0012BCDC File Offset: 0x00129EDC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			ArrayList numberList = processor.NumberList;
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
			}
			else
			{
				numberList.Clear();
				if (this.valueKey != -1)
				{
					numberList.Add(NumberAction.SimplifyValue(processor.Evaluate(frame, this.valueKey)));
				}
				else if (this.level == "any")
				{
					int num = this.numberAny(processor, frame);
					if (num != 0)
					{
						numberList.Add(num);
					}
				}
				else
				{
					bool flag = this.level == "multiple";
					XPathNavigator node = frame.Node;
					XPathNavigator xpathNavigator = frame.Node.Clone();
					if (xpathNavigator.NodeType == XPathNodeType.Attribute || xpathNavigator.NodeType == XPathNodeType.Namespace)
					{
						xpathNavigator.MoveToParent();
					}
					while (this.moveToCount(xpathNavigator, processor, node))
					{
						numberList.Insert(0, this.numberCount(xpathNavigator, processor, node));
						if (!flag || !xpathNavigator.MoveToParent())
						{
							break;
						}
					}
					if (!this.checkFrom(processor, xpathNavigator))
					{
						numberList.Clear();
					}
				}
				frame.StoredOutput = NumberAction.Format(numberList, (this.formatAvt == null) ? this.formatTokens : NumberAction.ParseFormat(this.formatAvt.Evaluate(processor, frame)), (this.langAvt == null) ? this.lang : this.langAvt.Evaluate(processor, frame), (this.letterAvt == null) ? this.letter : this.ParseLetter(this.letterAvt.Evaluate(processor, frame)), (this.groupingSepAvt == null) ? this.groupingSep : this.groupingSepAvt.Evaluate(processor, frame), (this.groupingSizeAvt == null) ? this.groupingSize : this.groupingSizeAvt.Evaluate(processor, frame));
			}
			if (!processor.TextEvent(frame.StoredOutput))
			{
				frame.State = 2;
				return;
			}
			frame.Finished();
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x0012BEAC File Offset: 0x0012A0AC
		private bool MatchCountKey(Processor processor, XPathNavigator contextNode, XPathNavigator nav)
		{
			if (this.countKey != -1)
			{
				return processor.Matches(nav, this.countKey);
			}
			return contextNode.Name == nav.Name && this.BasicNodeType(contextNode.NodeType) == this.BasicNodeType(nav.NodeType);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x0012BF00 File Offset: 0x0012A100
		private XPathNodeType BasicNodeType(XPathNodeType type)
		{
			if (type == XPathNodeType.SignificantWhitespace || type == XPathNodeType.Whitespace)
			{
				return XPathNodeType.Text;
			}
			return type;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x0012BF10 File Offset: 0x0012A110
		private static string Format(ArrayList numberlist, List<NumberAction.FormatInfo> tokens, string lang, string letter, string groupingSep, string groupingSize)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			if (tokens != null)
			{
				num = tokens.Count;
			}
			NumberAction.NumberingFormat numberingFormat = new NumberAction.NumberingFormat();
			if (groupingSize != null)
			{
				try
				{
					numberingFormat.setGroupingSize(Convert.ToInt32(groupingSize, CultureInfo.InvariantCulture));
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			if (groupingSep != null)
			{
				int length = groupingSep.Length;
				numberingFormat.setGroupingSeparator(groupingSep);
			}
			if (0 < num)
			{
				NumberAction.FormatInfo formatInfo = tokens[0];
				NumberAction.FormatInfo formatInfo2 = null;
				if (num % 2 == 1)
				{
					formatInfo2 = tokens[num - 1];
					num--;
				}
				NumberAction.FormatInfo formatInfo3 = ((2 < num) ? tokens[num - 2] : NumberAction.DefaultSeparator);
				NumberAction.FormatInfo formatInfo4 = ((0 < num) ? tokens[num - 1] : NumberAction.DefaultFormat);
				if (formatInfo != null)
				{
					stringBuilder.Append(formatInfo.formatString);
				}
				int count = numberlist.Count;
				for (int i = 0; i < count; i++)
				{
					int num2 = i * 2;
					bool flag = num2 < num;
					if (0 < i)
					{
						NumberAction.FormatInfo formatInfo5 = (flag ? tokens[num2] : formatInfo3);
						stringBuilder.Append(formatInfo5.formatString);
					}
					NumberAction.FormatInfo formatInfo6 = (flag ? tokens[num2 + 1] : formatInfo4);
					numberingFormat.setNumberingType(formatInfo6.numSequence);
					numberingFormat.setMinLen(formatInfo6.length);
					stringBuilder.Append(numberingFormat.FormatItem(numberlist[i]));
				}
				if (formatInfo2 != null)
				{
					stringBuilder.Append(formatInfo2.formatString);
				}
			}
			else
			{
				numberingFormat.setNumberingType(NumberingSequence.FirstDecimal);
				for (int j = 0; j < numberlist.Count; j++)
				{
					if (j != 0)
					{
						stringBuilder.Append(".");
					}
					stringBuilder.Append(numberingFormat.FormatItem(numberlist[j]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x0012C0D4 File Offset: 0x0012A2D4
		private static void mapFormatToken(string wsToken, int startLen, int tokLen, out NumberingSequence seq, out int pminlen)
		{
			char c = wsToken[startLen];
			bool flag = false;
			pminlen = 1;
			seq = NumberingSequence.Nil;
			int num = (int)c;
			if (num <= 2406)
			{
				if (num != 48 && num != 2406)
				{
					goto IL_0071;
				}
			}
			else if (num != 3664 && num != 51067 && num != 65296)
			{
				goto IL_0071;
			}
			do
			{
				pminlen++;
			}
			while (--tokLen > 0 && c == wsToken[++startLen]);
			if (wsToken[startLen] != c + '\u0001')
			{
				flag = true;
			}
			IL_0071:
			if (!flag)
			{
				num = (int)wsToken[startLen];
				if (num <= 3665)
				{
					if (num <= 1072)
					{
						if (num <= 73)
						{
							if (num == 49)
							{
								seq = NumberingSequence.FirstDecimal;
								goto IL_031F;
							}
							if (num == 65)
							{
								seq = NumberingSequence.FirstAlpha;
								goto IL_031F;
							}
							if (num == 73)
							{
								seq = NumberingSequence.FirstSpecial;
								goto IL_031F;
							}
						}
						else if (num <= 105)
						{
							if (num == 97)
							{
								seq = NumberingSequence.LCLetter;
								goto IL_031F;
							}
							if (num == 105)
							{
								seq = NumberingSequence.LCRoman;
								goto IL_031F;
							}
						}
						else
						{
							if (num == 1040)
							{
								seq = NumberingSequence.UCRus;
								goto IL_031F;
							}
							if (num == 1072)
							{
								seq = NumberingSequence.LCRus;
								goto IL_031F;
							}
						}
					}
					else if (num <= 2309)
					{
						if (num == 1488)
						{
							seq = NumberingSequence.Hebrew;
							goto IL_031F;
						}
						if (num == 1571)
						{
							seq = NumberingSequence.ArabicScript;
							goto IL_031F;
						}
						if (num == 2309)
						{
							seq = NumberingSequence.Hindi2;
							goto IL_031F;
						}
					}
					else if (num <= 2407)
					{
						if (num == 2325)
						{
							seq = NumberingSequence.Hindi1;
							goto IL_031F;
						}
						if (num == 2407)
						{
							seq = NumberingSequence.Hindi3;
							goto IL_031F;
						}
					}
					else
					{
						if (num == 3585)
						{
							seq = NumberingSequence.Thai1;
							goto IL_031F;
						}
						if (num == 3665)
						{
							seq = NumberingSequence.Thai2;
							goto IL_031F;
						}
					}
				}
				else if (num <= 23376)
				{
					if (num <= 12593)
					{
						if (num == 12450)
						{
							seq = NumberingSequence.DAiueo;
							goto IL_031F;
						}
						if (num == 12452)
						{
							seq = NumberingSequence.DIroha;
							goto IL_031F;
						}
						if (num == 12593)
						{
							seq = NumberingSequence.DChosung;
							goto IL_031F;
						}
					}
					else if (num <= 22769)
					{
						if (num == 19968)
						{
							seq = NumberingSequence.FEDecimal;
							goto IL_031F;
						}
						if (num == 22769)
						{
							seq = NumberingSequence.DbNum3;
							goto IL_031F;
						}
					}
					else
					{
						if (num == 22777)
						{
							seq = NumberingSequence.ChnCmplx;
							goto IL_031F;
						}
						if (num == 23376)
						{
							seq = NumberingSequence.Zodiac2;
							goto IL_031F;
						}
					}
				}
				else if (num <= 51068)
				{
					if (num != 30002)
					{
						if (num == 44032)
						{
							seq = NumberingSequence.Ganada;
							goto IL_031F;
						}
						if (num == 51068)
						{
							seq = NumberingSequence.KorDbNum1;
							goto IL_031F;
						}
					}
					else
					{
						if (tokLen > 1 && wsToken[startLen + 1] == '子')
						{
							seq = NumberingSequence.Zodiac3;
							tokLen--;
							startLen++;
							goto IL_031F;
						}
						seq = NumberingSequence.Zodiac1;
						goto IL_031F;
					}
				}
				else if (num <= 65297)
				{
					if (num == 54616)
					{
						seq = NumberingSequence.KorDbNum3;
						goto IL_031F;
					}
					if (num == 65297)
					{
						seq = NumberingSequence.DArabic;
						goto IL_031F;
					}
				}
				else
				{
					if (num == 65393)
					{
						seq = NumberingSequence.Aiueo;
						goto IL_031F;
					}
					if (num == 65394)
					{
						seq = NumberingSequence.Iroha;
						goto IL_031F;
					}
				}
				seq = NumberingSequence.FirstDecimal;
			}
			IL_031F:
			if (flag)
			{
				seq = NumberingSequence.FirstDecimal;
				pminlen = 0;
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x0012C40C File Offset: 0x0012A60C
		private static List<NumberAction.FormatInfo> ParseFormat(string formatString)
		{
			if (formatString == null || formatString.Length == 0)
			{
				return null;
			}
			int i = 0;
			bool flag = CharUtil.IsAlphaNumeric(formatString[i]);
			List<NumberAction.FormatInfo> list = new List<NumberAction.FormatInfo>();
			int num = 0;
			if (flag)
			{
				list.Add(null);
			}
			while (i <= formatString.Length)
			{
				bool flag2 = ((i < formatString.Length) ? CharUtil.IsAlphaNumeric(formatString[i]) : (!flag));
				if (flag != flag2)
				{
					NumberAction.FormatInfo formatInfo = new NumberAction.FormatInfo();
					if (flag)
					{
						NumberAction.mapFormatToken(formatString, num, i - num, out formatInfo.numSequence, out formatInfo.length);
					}
					else
					{
						formatInfo.isSeparator = true;
						formatInfo.formatString = formatString.Substring(num, i - num);
					}
					num = i;
					i++;
					list.Add(formatInfo);
					flag = flag2;
				}
				else
				{
					i++;
				}
			}
			return list;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x0012C4CC File Offset: 0x0012A6CC
		private string ParseLetter(string letter)
		{
			if (letter == null || letter == "traditional" || letter == "alphabetic")
			{
				return letter;
			}
			if (!this.forwardCompatibility)
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "letter-value", letter });
			}
			return null;
		}

		// Token: 0x040021E4 RID: 8676
		private const long msofnfcNil = 0L;

		// Token: 0x040021E5 RID: 8677
		private const long msofnfcTraditional = 1L;

		// Token: 0x040021E6 RID: 8678
		private const long msofnfcAlwaysFormat = 2L;

		// Token: 0x040021E7 RID: 8679
		private const int cchMaxFormat = 63;

		// Token: 0x040021E8 RID: 8680
		private const int cchMaxFormatDecimal = 11;

		// Token: 0x040021E9 RID: 8681
		private static NumberAction.FormatInfo DefaultFormat = new NumberAction.FormatInfo(false, "0");

		// Token: 0x040021EA RID: 8682
		private static NumberAction.FormatInfo DefaultSeparator = new NumberAction.FormatInfo(true, ".");

		// Token: 0x040021EB RID: 8683
		private const int OutputNumber = 2;

		// Token: 0x040021EC RID: 8684
		private string level;

		// Token: 0x040021ED RID: 8685
		private string countPattern;

		// Token: 0x040021EE RID: 8686
		private int countKey = -1;

		// Token: 0x040021EF RID: 8687
		private string from;

		// Token: 0x040021F0 RID: 8688
		private int fromKey = -1;

		// Token: 0x040021F1 RID: 8689
		private string value;

		// Token: 0x040021F2 RID: 8690
		private int valueKey = -1;

		// Token: 0x040021F3 RID: 8691
		private Avt formatAvt;

		// Token: 0x040021F4 RID: 8692
		private Avt langAvt;

		// Token: 0x040021F5 RID: 8693
		private Avt letterAvt;

		// Token: 0x040021F6 RID: 8694
		private Avt groupingSepAvt;

		// Token: 0x040021F7 RID: 8695
		private Avt groupingSizeAvt;

		// Token: 0x040021F8 RID: 8696
		private List<NumberAction.FormatInfo> formatTokens;

		// Token: 0x040021F9 RID: 8697
		private string lang;

		// Token: 0x040021FA RID: 8698
		private string letter;

		// Token: 0x040021FB RID: 8699
		private string groupingSep;

		// Token: 0x040021FC RID: 8700
		private string groupingSize;

		// Token: 0x040021FD RID: 8701
		private bool forwardCompatibility;

		// Token: 0x0200052B RID: 1323
		internal class FormatInfo
		{
			// Token: 0x0600353B RID: 13627 RVA: 0x0012C55D File Offset: 0x0012A75D
			public FormatInfo(bool isSeparator, string formatString)
			{
				this.isSeparator = isSeparator;
				this.formatString = formatString;
			}

			// Token: 0x0600353C RID: 13628 RVA: 0x000020FD File Offset: 0x000002FD
			public FormatInfo()
			{
			}

			// Token: 0x040021FE RID: 8702
			public bool isSeparator;

			// Token: 0x040021FF RID: 8703
			public NumberingSequence numSequence;

			// Token: 0x04002200 RID: 8704
			public int length;

			// Token: 0x04002201 RID: 8705
			public string formatString;
		}

		// Token: 0x0200052C RID: 1324
		private class NumberingFormat : NumberFormatterBase
		{
			// Token: 0x0600353D RID: 13629 RVA: 0x0012C573 File Offset: 0x0012A773
			internal NumberingFormat()
			{
			}

			// Token: 0x0600353E RID: 13630 RVA: 0x0012C57B File Offset: 0x0012A77B
			internal void setNumberingType(NumberingSequence seq)
			{
				this.seq = seq;
			}

			// Token: 0x0600353F RID: 13631 RVA: 0x0012C584 File Offset: 0x0012A784
			internal void setMinLen(int cMinLen)
			{
				this.cMinLen = cMinLen;
			}

			// Token: 0x06003540 RID: 13632 RVA: 0x0012C58D File Offset: 0x0012A78D
			internal void setGroupingSeparator(string separator)
			{
				this.separator = separator;
			}

			// Token: 0x06003541 RID: 13633 RVA: 0x0012C596 File Offset: 0x0012A796
			internal void setGroupingSize(int sizeGroup)
			{
				if (0 <= sizeGroup && sizeGroup <= 9)
				{
					this.sizeGroup = sizeGroup;
				}
			}

			// Token: 0x06003542 RID: 13634 RVA: 0x0012C5A8 File Offset: 0x0012A7A8
			internal string FormatItem(object value)
			{
				double num;
				if (value is int)
				{
					num = (double)((int)value);
				}
				else
				{
					num = XmlConvert.ToXPathDouble(value);
					if (0.5 > num || double.IsPositiveInfinity(num))
					{
						return XmlConvert.ToXPathString(value);
					}
					num = XmlConvert.XPathRound(num);
				}
				NumberingSequence numberingSequence = this.seq;
				if (numberingSequence != NumberingSequence.FirstDecimal)
				{
					if (numberingSequence - NumberingSequence.FirstAlpha > 1)
					{
						if (numberingSequence - NumberingSequence.FirstSpecial <= 1)
						{
							if (num <= 32767.0)
							{
								StringBuilder stringBuilder = new StringBuilder();
								NumberFormatterBase.ConvertToRoman(stringBuilder, num, this.seq == NumberingSequence.FirstSpecial);
								return stringBuilder.ToString();
							}
						}
					}
					else if (num <= 2147483647.0)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						NumberFormatterBase.ConvertToAlphabetic(stringBuilder2, num, (this.seq == NumberingSequence.FirstAlpha) ? 'A' : 'a', 26);
						return stringBuilder2.ToString();
					}
				}
				return NumberAction.NumberingFormat.ConvertToArabic(num, this.cMinLen, this.sizeGroup, this.separator);
			}

			// Token: 0x06003543 RID: 13635 RVA: 0x0012C67C File Offset: 0x0012A87C
			private static string ConvertToArabic(double val, int minLength, int groupSize, string groupSeparator)
			{
				string text;
				if (groupSize != 0 && groupSeparator != null)
				{
					NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
					numberFormatInfo.NumberGroupSizes = new int[] { groupSize };
					numberFormatInfo.NumberGroupSeparator = groupSeparator;
					if (Math.Floor(val) == val)
					{
						numberFormatInfo.NumberDecimalDigits = 0;
					}
					text = val.ToString("N", numberFormatInfo);
				}
				else
				{
					text = Convert.ToString(val, CultureInfo.InvariantCulture);
				}
				if (text.Length >= minLength)
				{
					return text;
				}
				StringBuilder stringBuilder = new StringBuilder(minLength);
				stringBuilder.Append('0', minLength - text.Length);
				stringBuilder.Append(text);
				return stringBuilder.ToString();
			}

			// Token: 0x04002202 RID: 8706
			private NumberingSequence seq;

			// Token: 0x04002203 RID: 8707
			private int cMinLen;

			// Token: 0x04002204 RID: 8708
			private string separator;

			// Token: 0x04002205 RID: 8709
			private int sizeGroup;
		}
	}
}
