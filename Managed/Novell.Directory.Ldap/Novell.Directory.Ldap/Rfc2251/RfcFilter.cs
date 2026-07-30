using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000068 RID: 104
	public class RfcFilter : Asn1Choice
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00010BC6 File Offset: 0x0000EDC6
		public RfcFilter(string filter)
			: base(null)
		{
			this.ChoiceValue = this.parse(filter);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public RfcFilter()
			: base(null)
		{
			this.filterStack = new Stack();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		private Asn1Tagged parse(string filterExpr)
		{
			if (filterExpr == null || filterExpr.Equals(""))
			{
				filterExpr = new StringBuilder("(objectclass=*)").ToString();
			}
			int num;
			if ((num = filterExpr.IndexOf('\\')) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(filterExpr);
				int i = num;
				while (i < stringBuilder.Length - 1)
				{
					char c = stringBuilder[i++];
					if (c == '\\')
					{
						c = stringBuilder[i];
						if (c == '*' || c == '(' || c == ')' || c == '\\')
						{
							stringBuilder.Remove(i, i + 1 - i);
							stringBuilder.Insert(i, Convert.ToString((int)c, 16));
							i += 2;
						}
					}
				}
				filterExpr = stringBuilder.ToString();
			}
			if (filterExpr[0] != '(' && filterExpr[filterExpr.Length - 1] != ')')
			{
				filterExpr = "(" + filterExpr + ")";
			}
			int num2 = (int)filterExpr[0];
			int length = filterExpr.Length;
			if (num2 != 40)
			{
				throw new LdapLocalException("MISSING_LEFT_PAREN", 87);
			}
			if (filterExpr[length - 1] != ')')
			{
				throw new LdapLocalException("MISSING_RIGHT_PAREN", 87);
			}
			int num3 = 0;
			for (int j = 0; j < length; j++)
			{
				if (filterExpr[j] == '(')
				{
					num3++;
				}
				if (filterExpr[j] == ')')
				{
					num3--;
				}
			}
			if (num3 > 0)
			{
				throw new LdapLocalException("MISSING_RIGHT_PAREN", 87);
			}
			if (num3 < 0)
			{
				throw new LdapLocalException("MISSING_LEFT_PAREN", 87);
			}
			this.ft = new RfcFilter.FilterTokenizer(this, filterExpr);
			return this.parseFilter();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010D79 File Offset: 0x0000EF79
		private Asn1Tagged parseFilter()
		{
			this.ft.getLeftParen();
			Asn1Tagged asn1Tagged = this.parseFilterComp();
			this.ft.getRightParen();
			return asn1Tagged;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010D98 File Offset: 0x0000EF98
		private Asn1Tagged parseFilterComp()
		{
			Asn1Tagged asn1Tagged = null;
			int opOrAttr = this.ft.OpOrAttr;
			if (opOrAttr > 1)
			{
				if (opOrAttr != 2)
				{
					int filterType = this.ft.FilterType;
					string value = this.ft.Value;
					switch (filterType)
					{
					case 3:
						if (value.Equals("*"))
						{
							asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, false, 7), new RfcAttributeDescription(this.ft.Attr), false);
						}
						else if (value.IndexOf('*') != -1)
						{
							SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(value, "*", true);
							Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(5);
							int count = tokenizer.Count;
							int num = 0;
							string text = new StringBuilder("").ToString();
							while (tokenizer.HasMoreTokens())
							{
								string text2 = tokenizer.NextToken();
								num++;
								if (text2.Equals("*"))
								{
									if (text.Equals(text2))
									{
										asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), new RfcLdapString(this.unescapeString("")), false));
									}
								}
								else if (num == 1)
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 0), new RfcLdapString(this.unescapeString(text2)), false));
								}
								else if (num < count)
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), new RfcLdapString(this.unescapeString(text2)), false));
								}
								else
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 2), new RfcLdapString(this.unescapeString(text2)), false));
								}
								text = text2;
							}
							asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, 4), new RfcSubstringFilter(new RfcAttributeDescription(this.ft.Attr), asn1SequenceOf), false);
						}
						else
						{
							asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcAttributeValueAssertion(new RfcAttributeDescription(this.ft.Attr), new RfcAssertionValue(this.unescapeString(value))), false);
						}
						break;
					case 5:
					case 6:
					case 8:
						asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, filterType), new RfcAttributeValueAssertion(new RfcAttributeDescription(this.ft.Attr), new RfcAssertionValue(this.unescapeString(value))), false);
						break;
					case 9:
					{
						string text3 = null;
						string text4 = null;
						bool flag = false;
						SupportClass.Tokenizer tokenizer2 = new SupportClass.Tokenizer(this.ft.Attr, ":");
						bool flag2 = true;
						while (tokenizer2.HasMoreTokens())
						{
							string text5 = tokenizer2.NextToken().Trim();
							if (flag2 && !text5.Equals(":"))
							{
								text3 = text5;
							}
							else if (text5.Equals("dn"))
							{
								flag = true;
							}
							else if (!text5.Equals(":"))
							{
								text4 = text5;
							}
							flag2 = false;
						}
						asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, 9), new RfcMatchingRuleAssertion((text4 == null) ? null : new RfcMatchingRuleId(text4), (text3 == null) ? null : new RfcAttributeDescription(text3), new RfcAssertionValue(this.unescapeString(value)), (!flag) ? null : new Asn1Boolean(true)), false);
						break;
					}
					}
				}
				else
				{
					asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, opOrAttr), this.parseFilter(), true);
				}
			}
			else
			{
				asn1Tagged = new Asn1Tagged(new Asn1Identifier(2, true, opOrAttr), this.parseFilterList(), false);
			}
			return asn1Tagged;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000110DC File Offset: 0x0000F2DC
		private Asn1SetOf parseFilterList()
		{
			Asn1SetOf asn1SetOf = new Asn1SetOf();
			asn1SetOf.add(this.parseFilter());
			while (this.ft.peekChar() == '(')
			{
				asn1SetOf.add(this.parseFilter());
			}
			return asn1SetOf;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011119 File Offset: 0x0000F319
		internal static int hex2int(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			if (c < 'a' || c > 'f')
			{
				return -1;
			}
			return (int)(c - 'a' + '\n');
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011150 File Offset: 0x0000F350
		private sbyte[] unescapeString(string string_Renamed)
		{
			sbyte[] array = new sbyte[string_Renamed.Length * 3];
			bool flag = false;
			bool flag2 = false;
			int length = string_Renamed.Length;
			char[] array2 = new char[1];
			char c = '\0';
			int i = 0;
			int num = 0;
			while (i < length)
			{
				char c2 = string_Renamed[i];
				if (flag)
				{
					int num2;
					if ((num2 = RfcFilter.hex2int(c2)) < 0)
					{
						throw new LdapLocalException("INVALID_ESCAPE", new object[] { c2 }, 87);
					}
					if (flag2)
					{
						c = (char)(num2 << 4);
						flag2 = false;
					}
					else
					{
						c |= (char)num2;
						array[num++] = (sbyte)c;
						flag = (flag2 = false);
					}
				}
				else if (c2 == '\\')
				{
					flag = (flag2 = true);
				}
				else
				{
					try
					{
						if ((c2 < '\u0001' || c2 > '\'') && (c2 < '+' || c2 > '[') && c2 < ']')
						{
							string text = "";
							array2[0] = c2;
							foreach (sbyte b in SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(new string(array2))))
							{
								if (b >= 0 && b < 16)
								{
									text = text + "\\0" + Convert.ToString((int)b & 255, 16);
								}
								else
								{
									text = text + "\\" + Convert.ToString((int)b & 255, 16);
								}
							}
							throw new LdapLocalException("INVALID_CHAR_IN_FILTER", new object[] { c2, text }, 87);
						}
						if (c2 <= '\u007f')
						{
							array[num++] = (sbyte)c2;
						}
						else
						{
							array2[0] = c2;
							sbyte[] array3 = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(new string(array2)));
							Array.Copy(array3, 0, array, num, array3.Length);
							num += array3.Length;
						}
						flag = false;
					}
					catch (IOException)
					{
						throw new SystemException("UTF-8 String encoding not supported by JVM");
					}
				}
				i++;
			}
			if (flag2 || flag)
			{
				throw new LdapLocalException("SHORT_ESCAPE", 87);
			}
			sbyte[] array4 = new sbyte[num];
			Array.Copy(array, 0, array4, 0, num);
			array = null;
			return array4;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011380 File Offset: 0x0000F580
		private void addObject(Asn1Object current)
		{
			if (this.filterStack == null)
			{
				this.filterStack = new Stack();
			}
			if (base.choiceValue() == null)
			{
				this.ChoiceValue = current;
			}
			else
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)this.filterStack.Peek();
				Asn1Object asn1Object = asn1Tagged.taggedValue();
				if (asn1Object == null)
				{
					asn1Tagged.TaggedValue = current;
					this.filterStack.Push(current);
				}
				else if (asn1Object is Asn1SetOf)
				{
					((Asn1SetOf)asn1Object).add(current);
				}
				else if (asn1Object is Asn1Set)
				{
					((Asn1Set)asn1Object).add(current);
				}
				else if (asn1Object.getIdentifier().Tag == 2)
				{
					throw new LdapLocalException("Attemp to create more than one 'not' sub-filter", 87);
				}
			}
			int tag = current.getIdentifier().Tag;
			if (tag == 0 || tag == 1 || tag == 2)
			{
				this.filterStack.Push(current);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001144C File Offset: 0x0000F64C
		public virtual void startSubstrings(string attrName)
		{
			this.finalFound = false;
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(5);
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, 4), new RfcSubstringFilter(new RfcAttributeDescription(attrName), asn1SequenceOf), false);
			this.addObject(asn1Object);
			SupportClass.StackPush(this.filterStack, asn1SequenceOf);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011498 File Offset: 0x0000F698
		[CLSCompliant(false)]
		public virtual void addSubstring(int type, sbyte[] value_Renamed)
		{
			try
			{
				Asn1SequenceOf asn1SequenceOf = (Asn1SequenceOf)this.filterStack.Peek();
				if (type != 0 && type != 1 && type != 2)
				{
					throw new LdapLocalException("Attempt to add an invalid substring type", 87);
				}
				if (type == 0 && asn1SequenceOf.size() != 0)
				{
					throw new LdapLocalException("Attempt to add an initial substring match after the first substring", 87);
				}
				if (this.finalFound)
				{
					throw new LdapLocalException("Attempt to add a substring match after a final substring match", 87);
				}
				if (type == 2)
				{
					this.finalFound = true;
				}
				asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, type), new RfcLdapString(value_Renamed), false));
			}
			catch (InvalidCastException)
			{
				throw new LdapLocalException("A call to addSubstring occured without calling startSubstring", 87);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011540 File Offset: 0x0000F740
		public virtual void endSubstrings()
		{
			try
			{
				this.finalFound = false;
				if (((Asn1SequenceOf)this.filterStack.Peek()).size() == 0)
				{
					throw new LdapLocalException("Empty substring filter", 87);
				}
			}
			catch (InvalidCastException)
			{
				throw new LdapLocalException("Missmatched ending of substrings", 87);
			}
			this.filterStack.Pop();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000115A4 File Offset: 0x0000F7A4
		[CLSCompliant(false)]
		public virtual void addAttributeValueAssertion(int rfcType, string attrName, sbyte[] value_Renamed)
		{
			if (this.filterStack != null && this.filterStack.Count != 0 && this.filterStack.Peek() is Asn1SequenceOf)
			{
				throw new LdapLocalException("Cannot insert an attribute assertion in a substring", 87);
			}
			if (rfcType != 3 && rfcType != 5 && rfcType != 6 && rfcType != 8)
			{
				throw new LdapLocalException("Invalid filter type for AttributeValueAssertion", 87);
			}
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), new RfcAttributeValueAssertion(new RfcAttributeDescription(attrName), new RfcAssertionValue(value_Renamed)), false);
			this.addObject(asn1Object);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001162C File Offset: 0x0000F82C
		public virtual void addPresent(string attrName)
		{
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, false, 7), new RfcAttributeDescription(attrName), false);
			this.addObject(asn1Object);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011658 File Offset: 0x0000F858
		[CLSCompliant(false)]
		public virtual void addExtensibleMatch(string matchingRule, string attrName, sbyte[] value_Renamed, bool useDNMatching)
		{
			Asn1Object asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, 9), new RfcMatchingRuleAssertion((matchingRule == null) ? null : new RfcMatchingRuleId(matchingRule), (attrName == null) ? null : new RfcAttributeDescription(attrName), new RfcAssertionValue(value_Renamed), (!useDNMatching) ? null : new Asn1Boolean(true)), false);
			this.addObject(asn1Object);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000116AC File Offset: 0x0000F8AC
		public virtual void startNestedFilter(int rfcType)
		{
			Asn1Object asn1Object;
			if (rfcType == 0 || rfcType == 1)
			{
				asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), new Asn1SetOf(), false);
			}
			else
			{
				if (rfcType != 2)
				{
					throw new LdapLocalException("Attempt to create a nested filter other than AND, OR or NOT", 87);
				}
				asn1Object = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), null, true);
			}
			this.addObject(asn1Object);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011700 File Offset: 0x0000F900
		public virtual void endNestedFilter(int rfcType)
		{
			if (rfcType == 2)
			{
				this.filterStack.Pop();
			}
			if (((Asn1Object)this.filterStack.Peek()).getIdentifier().Tag != rfcType)
			{
				throw new LdapLocalException("Missmatched ending of nested filter", 87);
			}
			this.filterStack.Pop();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00011753 File Offset: 0x0000F953
		public virtual IEnumerator getFilterIterator()
		{
			return new RfcFilter.FilterIterator(this, (Asn1Tagged)base.choiceValue());
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00011768 File Offset: 0x0000F968
		public virtual string filterToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			RfcFilter.stringFilter(this.getFilterIterator(), stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00011790 File Offset: 0x0000F990
		private static void stringFilter(IEnumerator itr, StringBuilder filter)
		{
			filter.Append('(');
			while (itr.MoveNext())
			{
				object obj = itr.Current;
				if (obj is int)
				{
					switch ((int)obj)
					{
					case 0:
						filter.Append('&');
						break;
					case 1:
						filter.Append('|');
						break;
					case 2:
						filter.Append('!');
						break;
					case 3:
					{
						filter.Append((string)itr.Current);
						filter.Append('=');
						sbyte[] array = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array));
						break;
					}
					case 4:
					{
						filter.Append((string)itr.Current);
						filter.Append('=');
						bool flag = false;
						while (itr.MoveNext())
						{
							switch ((int)itr.Current)
							{
							case 0:
								filter.Append((string)itr.Current);
								filter.Append('*');
								flag = false;
								break;
							case 1:
								if (flag)
								{
									filter.Append('*');
								}
								filter.Append((string)itr.Current);
								filter.Append('*');
								flag = false;
								break;
							case 2:
								if (flag)
								{
									filter.Append('*');
								}
								filter.Append((string)itr.Current);
								break;
							}
						}
						break;
					}
					case 5:
					{
						filter.Append((string)itr.Current);
						filter.Append(">=");
						sbyte[] array2 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array2));
						break;
					}
					case 6:
					{
						filter.Append((string)itr.Current);
						filter.Append("<=");
						sbyte[] array3 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array3));
						break;
					}
					case 7:
						filter.Append((string)itr.Current);
						filter.Append("=*");
						break;
					case 8:
					{
						filter.Append((string)itr.Current);
						filter.Append("~=");
						sbyte[] array4 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(array4));
						break;
					}
					case 9:
					{
						string text = (string)itr.Current;
						filter.Append((string)itr.Current);
						filter.Append(':');
						filter.Append(text);
						filter.Append(":=");
						filter.Append((string)itr.Current);
						break;
					}
					}
				}
				else if (obj is IEnumerator)
				{
					RfcFilter.stringFilter((IEnumerator)obj, filter);
				}
			}
			filter.Append(')');
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00011A70 File Offset: 0x0000FC70
		private static string byteString(sbyte[] value_Renamed)
		{
			string text = null;
			if (Base64.isValidUTF8(value_Renamed, true))
			{
				try
				{
					return new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(value_Renamed)));
				}
				catch (IOException ex)
				{
					throw new SystemException("Default JVM does not support UTF-8 encoding" + ex);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value_Renamed.Length; i++)
			{
				if (value_Renamed[i] >= 0)
				{
					stringBuilder.Append("\\0");
					stringBuilder.Append(Convert.ToString((short)value_Renamed[i], 16));
				}
				else
				{
					stringBuilder.Append("\\" + Convert.ToString((short)value_Renamed[i], 16).Substring(6));
				}
			}
			text = stringBuilder.ToString();
			return text;
		}

		// Token: 0x04000233 RID: 563
		public const int AND = 0;

		// Token: 0x04000234 RID: 564
		public const int OR = 1;

		// Token: 0x04000235 RID: 565
		public const int NOT = 2;

		// Token: 0x04000236 RID: 566
		public const int EQUALITY_MATCH = 3;

		// Token: 0x04000237 RID: 567
		public const int SUBSTRINGS = 4;

		// Token: 0x04000238 RID: 568
		public const int GREATER_OR_EQUAL = 5;

		// Token: 0x04000239 RID: 569
		public const int LESS_OR_EQUAL = 6;

		// Token: 0x0400023A RID: 570
		public const int PRESENT = 7;

		// Token: 0x0400023B RID: 571
		public const int APPROX_MATCH = 8;

		// Token: 0x0400023C RID: 572
		public const int EXTENSIBLE_MATCH = 9;

		// Token: 0x0400023D RID: 573
		public const int INITIAL = 0;

		// Token: 0x0400023E RID: 574
		public const int ANY = 1;

		// Token: 0x0400023F RID: 575
		public const int FINAL = 2;

		// Token: 0x04000240 RID: 576
		private RfcFilter.FilterTokenizer ft;

		// Token: 0x04000241 RID: 577
		private Stack filterStack;

		// Token: 0x04000242 RID: 578
		private bool finalFound;

		// Token: 0x020000F5 RID: 245
		private class FilterIterator : IEnumerator
		{
			// Token: 0x0600062B RID: 1579 RVA: 0x0001949E File Offset: 0x0001769E
			public void Reset()
			{
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x000194A0 File Offset: 0x000176A0
			private void InitBlock(RfcFilter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x0600062D RID: 1581 RVA: 0x000194AC File Offset: 0x000176AC
			public virtual object Current
			{
				get
				{
					object obj = null;
					if (!this.tagReturned)
					{
						this.tagReturned = true;
						obj = this.root.getIdentifier().Tag;
					}
					else
					{
						Asn1Object asn1Object = this.root.taggedValue();
						if (asn1Object is RfcLdapString)
						{
							this.hasMore = false;
							obj = ((RfcLdapString)asn1Object).stringValue();
						}
						else if (asn1Object is RfcSubstringFilter)
						{
							RfcSubstringFilter rfcSubstringFilter = (RfcSubstringFilter)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
								obj = ((RfcAttributeDescription)rfcSubstringFilter.get_Renamed(0)).stringValue();
							}
							else if (this.index % 2 == 0)
							{
								obj = ((Asn1Tagged)((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).get_Renamed(this.index / 2)).getIdentifier().Tag;
								this.index++;
							}
							else
							{
								obj = ((RfcLdapString)((Asn1Tagged)((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).get_Renamed(this.index / 2)).taggedValue()).stringValue();
								this.index++;
							}
							if (this.index / 2 >= ((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).size())
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is RfcAttributeValueAssertion)
						{
							RfcAttributeValueAssertion rfcAttributeValueAssertion = (RfcAttributeValueAssertion)asn1Object;
							if (this.index == -1)
							{
								obj = rfcAttributeValueAssertion.AttributeDescription;
								this.index = 1;
							}
							else if (this.index == 1)
							{
								obj = rfcAttributeValueAssertion.AssertionValue;
								this.index = 2;
								this.hasMore = false;
							}
						}
						else if (asn1Object is RfcMatchingRuleAssertion)
						{
							Asn1Structured asn1Structured = (RfcMatchingRuleAssertion)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
							}
							int num = this.index;
							this.index = num + 1;
							obj = ((Asn1OctetString)((Asn1Tagged)asn1Structured.get_Renamed(num)).taggedValue()).stringValue();
							if (this.index > 2)
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is Asn1SetOf)
						{
							Asn1SetOf asn1SetOf = (Asn1SetOf)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
							}
							RfcFilter rfcFilter = this.enclosingInstance;
							Asn1Structured asn1Structured2 = asn1SetOf;
							int num = this.index;
							this.index = num + 1;
							obj = new RfcFilter.FilterIterator(rfcFilter, (Asn1Tagged)asn1Structured2.get_Renamed(num));
							if (this.index >= asn1SetOf.size())
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is Asn1Tagged)
						{
							obj = new RfcFilter.FilterIterator(this.enclosingInstance, (Asn1Tagged)asn1Object);
							this.hasMore = false;
						}
					}
					return obj;
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x0600062E RID: 1582 RVA: 0x00019733 File Offset: 0x00017933
			public RfcFilter Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x0001973B File Offset: 0x0001793B
			public FilterIterator(RfcFilter enclosingInstance, Asn1Tagged root)
			{
				this.InitBlock(enclosingInstance);
				this.root = root;
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x0001975F File Offset: 0x0001795F
			public virtual bool MoveNext()
			{
				return this.hasMore;
			}

			// Token: 0x06000631 RID: 1585 RVA: 0x00019767 File Offset: 0x00017967
			public void remove()
			{
				throw new NotSupportedException("Remove is not supported on a filter iterator");
			}

			// Token: 0x040004EB RID: 1259
			private RfcFilter enclosingInstance;

			// Token: 0x040004EC RID: 1260
			internal Asn1Tagged root;

			// Token: 0x040004ED RID: 1261
			internal bool tagReturned;

			// Token: 0x040004EE RID: 1262
			internal int index = -1;

			// Token: 0x040004EF RID: 1263
			private bool hasMore = true;
		}

		// Token: 0x020000F6 RID: 246
		internal class FilterTokenizer
		{
			// Token: 0x06000632 RID: 1586 RVA: 0x00019773 File Offset: 0x00017973
			private void InitBlock(RfcFilter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001977C File Offset: 0x0001797C
			public virtual int OpOrAttr
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num = (int)this.filter[this.offset];
					int num2;
					if (num == 38)
					{
						this.offset++;
						num2 = 0;
					}
					else if (num == 124)
					{
						this.offset++;
						num2 = 1;
					}
					else if (num == 33)
					{
						this.offset++;
						num2 = 2;
					}
					else
					{
						if (this.filter.Substring(this.offset).StartsWith(":="))
						{
							throw new LdapLocalException("NO_MATCHING_RULE", 87);
						}
						if (this.filter.Substring(this.offset).StartsWith("::=") || this.filter.Substring(this.offset).StartsWith(":::="))
						{
							throw new LdapLocalException("NO_DN_NOR_MATCHING_RULE", 87);
						}
						string text = "=~<>()";
						StringBuilder stringBuilder = new StringBuilder();
						while (text.IndexOf(this.filter[this.offset]) == -1 && !this.filter.Substring(this.offset).StartsWith(":="))
						{
							StringBuilder stringBuilder2 = stringBuilder;
							string text2 = this.filter;
							int num3 = this.offset;
							this.offset = num3 + 1;
							stringBuilder2.Append(text2[num3]);
						}
						this.attr = stringBuilder.ToString().Trim();
						if (this.attr.Length == 0 || this.attr[0] == ';')
						{
							throw new LdapLocalException("NO_ATTRIBUTE_NAME", 87);
						}
						int i = 0;
						while (i < this.attr.Length)
						{
							char c = this.attr[i];
							if (!char.IsLetterOrDigit(c) && c != '-' && c != '.' && c != ';' && c != ':')
							{
								if (c == '\\')
								{
									throw new LdapLocalException("INVALID_ESC_IN_DESCR", 87);
								}
								throw new LdapLocalException("INVALID_CHAR_IN_DESCR", new object[] { c }, 87);
							}
							else
							{
								i++;
							}
						}
						i = this.attr.IndexOf(';');
						if (i != -1 && i == this.attr.Length - 1)
						{
							throw new LdapLocalException("NO_OPTION", 87);
						}
						num2 = -1;
					}
					return num2;
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000634 RID: 1588 RVA: 0x000199C8 File Offset: 0x00017BC8
			public virtual int FilterType
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num;
					if (this.filter.Substring(this.offset).StartsWith(">="))
					{
						this.offset += 2;
						num = 5;
					}
					else if (this.filter.Substring(this.offset).StartsWith("<="))
					{
						this.offset += 2;
						num = 6;
					}
					else if (this.filter.Substring(this.offset).StartsWith("~="))
					{
						this.offset += 2;
						num = 8;
					}
					else if (this.filter.Substring(this.offset).StartsWith(":="))
					{
						this.offset += 2;
						num = 9;
					}
					else
					{
						if (this.filter[this.offset] != '=')
						{
							throw new LdapLocalException("INVALID_FILTER_COMPARISON", 87);
						}
						this.offset++;
						num = 3;
					}
					return num;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019AE8 File Offset: 0x00017CE8
			public virtual string Value
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num = this.filter.IndexOf(')', this.offset);
					if (num == -1)
					{
						num = this.filterLength;
					}
					string text = this.filter.Substring(this.offset, num - this.offset);
					this.offset = num;
					return text;
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000636 RID: 1590 RVA: 0x00019B4F File Offset: 0x00017D4F
			public virtual string Attr
			{
				get
				{
					return this.attr;
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000637 RID: 1591 RVA: 0x00019B57 File Offset: 0x00017D57
			public RfcFilter Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x00019B5F File Offset: 0x00017D5F
			public FilterTokenizer(RfcFilter enclosingInstance, string filter)
			{
				this.InitBlock(enclosingInstance);
				this.filter = filter;
				this.offset = 0;
				this.filterLength = filter.Length;
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x00019B88 File Offset: 0x00017D88
			public void getLeftParen()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				string text = this.filter;
				int num = this.offset;
				this.offset = num + 1;
				if (text[num] != '(')
				{
					throw new LdapLocalException("EXPECTING_LEFT_PAREN", new object[] { this.filter[--this.offset] }, 87);
				}
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x00019C08 File Offset: 0x00017E08
			public void getRightParen()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				string text = this.filter;
				int num = this.offset;
				this.offset = num + 1;
				if (text[num] != ')')
				{
					throw new LdapLocalException("EXPECTING_RIGHT_PAREN", new object[] { this.filter[this.offset - 1] }, 87);
				}
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x00019C7E File Offset: 0x00017E7E
			public char peekChar()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				return this.filter[this.offset];
			}

			// Token: 0x040004F0 RID: 1264
			private RfcFilter enclosingInstance;

			// Token: 0x040004F1 RID: 1265
			private string filter;

			// Token: 0x040004F2 RID: 1266
			private string attr;

			// Token: 0x040004F3 RID: 1267
			private int offset;

			// Token: 0x040004F4 RID: 1268
			private int filterLength;
		}
	}
}
