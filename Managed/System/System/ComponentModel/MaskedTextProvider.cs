using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace System.ComponentModel
{
	/// <summary>Represents a mask-parsing service that can be used by any number of controls that support masking, such as the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
	// Token: 0x020002AF RID: 687
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class MaskedTextProvider : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		// Token: 0x06001541 RID: 5441 RVA: 0x00053F2B File Offset: 0x0005212B
		public MaskedTextProvider(string mask)
			: this(mask, null, true, '_', '\0', false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		// Token: 0x06001542 RID: 5442 RVA: 0x00053F3A File Offset: 0x0005213A
		public MaskedTextProvider(string mask, bool restrictToAscii)
			: this(mask, null, true, '_', '\0', restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and culture.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		// Token: 0x06001543 RID: 5443 RVA: 0x00053F49 File Offset: 0x00052149
		public MaskedTextProvider(string mask, CultureInfo culture)
			: this(mask, culture, true, '_', '\0', false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		// Token: 0x06001544 RID: 5444 RVA: 0x00053F58 File Offset: 0x00052158
		public MaskedTextProvider(string mask, CultureInfo culture, bool restrictToAscii)
			: this(mask, culture, true, '_', '\0', restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">true to allow the prompt character as input; otherwise false. </param>
		// Token: 0x06001545 RID: 5445 RVA: 0x00053F67 File Offset: 0x00052167
		public MaskedTextProvider(string mask, char passwordChar, bool allowPromptAsInput)
			: this(mask, null, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">true to allow the prompt character as input; otherwise false. </param>
		// Token: 0x06001546 RID: 5446 RVA: 0x00053F76 File Offset: 0x00052176
		public MaskedTextProvider(string mask, CultureInfo culture, char passwordChar, bool allowPromptAsInput)
			: this(mask, culture, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, prompt usage value, prompt character, password character, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="allowPromptAsInput">A <see cref="T:System.Boolean" /> value that specifies whether the prompt character should be allowed as a valid input character. </param>
		/// <param name="promptChar">A <see cref="T:System.Char" /> that will be displayed as a placeholder for user input.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		/// <exception cref="T:System.ArgumentException">The mask parameter is null or <see cref="F:System.String.Empty" />.-or-The mask contains one or more non-printable characters. </exception>
		// Token: 0x06001547 RID: 5447 RVA: 0x00053F88 File Offset: 0x00052188
		public MaskedTextProvider(string mask, CultureInfo culture, bool allowPromptAsInput, char promptChar, char passwordChar, bool restrictToAscii)
		{
			if (string.IsNullOrEmpty(mask))
			{
				throw new ArgumentException(global::SR.GetString("The Mask value cannot be null or empty."), "mask");
			}
			for (int i = 0; i < mask.Length; i++)
			{
				if (!MaskedTextProvider.IsPrintableChar(mask[i]))
				{
					throw new ArgumentException(global::SR.GetString("The specified mask contains invalid characters."));
				}
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			this.flagState = default(BitVector32);
			this.mask = mask;
			this.promptChar = promptChar;
			this.passwordChar = passwordChar;
			if (culture.IsNeutralCulture)
			{
				foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (culture.Equals(cultureInfo.Parent))
					{
						this.culture = cultureInfo;
						break;
					}
				}
				if (this.culture == null)
				{
					this.culture = CultureInfo.InvariantCulture;
				}
			}
			else
			{
				this.culture = culture;
			}
			if (!this.culture.IsReadOnly)
			{
				this.culture = CultureInfo.ReadOnly(this.culture);
			}
			this.flagState[MaskedTextProvider.ALLOW_PROMPT_AS_INPUT] = allowPromptAsInput;
			this.flagState[MaskedTextProvider.ASCII_ONLY] = restrictToAscii;
			this.flagState[MaskedTextProvider.INCLUDE_PROMPT] = false;
			this.flagState[MaskedTextProvider.INCLUDE_LITERALS] = true;
			this.flagState[MaskedTextProvider.RESET_ON_PROMPT] = true;
			this.flagState[MaskedTextProvider.SKIP_SPACE] = true;
			this.flagState[MaskedTextProvider.RESET_ON_LITERALS] = true;
			this.Initialize();
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00054108 File Offset: 0x00052308
		private void Initialize()
		{
			this.testString = new StringBuilder();
			this.stringDescriptor = new List<MaskedTextProvider.CharDescriptor>();
			MaskedTextProvider.CaseConversion caseConversion = MaskedTextProvider.CaseConversion.None;
			bool flag = false;
			int num = 0;
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Literal;
			string text = string.Empty;
			int i = 0;
			while (i < this.mask.Length)
			{
				char c = this.mask[i];
				if (!flag)
				{
					if (c <= 'C')
					{
						switch (c)
						{
						case '#':
							goto IL_019E;
						case '$':
							text = this.culture.NumberFormat.CurrencySymbol;
							charType = MaskedTextProvider.CharType.Separator;
							goto IL_01BE;
						case '%':
							goto IL_01B8;
						case '&':
							break;
						default:
							switch (c)
							{
							case ',':
								text = this.culture.NumberFormat.NumberGroupSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_01BE;
							case '-':
								goto IL_01B8;
							case '.':
								text = this.culture.NumberFormat.NumberDecimalSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_01BE;
							case '/':
								text = this.culture.DateTimeFormat.DateSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_01BE;
							case '0':
								break;
							default:
								switch (c)
								{
								case '9':
								case '?':
								case 'C':
									goto IL_019E;
								case ':':
									text = this.culture.DateTimeFormat.TimeSeparator;
									charType = MaskedTextProvider.CharType.Separator;
									goto IL_01BE;
								case ';':
								case '=':
								case '@':
								case 'B':
									goto IL_01B8;
								case '<':
									caseConversion = MaskedTextProvider.CaseConversion.ToLower;
									goto IL_022A;
								case '>':
									caseConversion = MaskedTextProvider.CaseConversion.ToUpper;
									goto IL_022A;
								case 'A':
									break;
								default:
									goto IL_01B8;
								}
								break;
							}
							break;
						}
					}
					else if (c <= '\\')
					{
						if (c != 'L')
						{
							if (c != '\\')
							{
								goto IL_01B8;
							}
							flag = true;
							charType = MaskedTextProvider.CharType.Literal;
							goto IL_022A;
						}
					}
					else
					{
						if (c == 'a')
						{
							goto IL_019E;
						}
						if (c != '|')
						{
							goto IL_01B8;
						}
						caseConversion = MaskedTextProvider.CaseConversion.None;
						goto IL_022A;
					}
					this.requiredEditChars++;
					c = this.promptChar;
					charType = MaskedTextProvider.CharType.EditRequired;
					goto IL_01BE;
					IL_019E:
					this.optionalEditChars++;
					c = this.promptChar;
					charType = MaskedTextProvider.CharType.EditOptional;
					goto IL_01BE;
					IL_01B8:
					charType = MaskedTextProvider.CharType.Literal;
					goto IL_01BE;
				}
				flag = false;
				goto IL_01BE;
				IL_022A:
				i++;
				continue;
				IL_01BE:
				MaskedTextProvider.CharDescriptor charDescriptor = new MaskedTextProvider.CharDescriptor(i, charType);
				if (MaskedTextProvider.IsEditPosition(charDescriptor))
				{
					charDescriptor.CaseConversion = caseConversion;
				}
				if (charType != MaskedTextProvider.CharType.Separator)
				{
					text = c.ToString();
				}
				foreach (char c2 in text)
				{
					this.testString.Append(c2);
					this.stringDescriptor.Add(charDescriptor);
					num++;
				}
				goto IL_022A;
			}
			this.testString.Capacity = this.testString.Length;
		}

		/// <summary>Gets a value indicating whether the prompt character should be treated as a valid input character or not.</summary>
		/// <returns>true if the user can enter <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" /> into the control; otherwise, false. The default is true. </returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0005436D File Offset: 0x0005256D
		public bool AllowPromptAsInput
		{
			get
			{
				return this.flagState[MaskedTextProvider.ALLOW_PROMPT_AS_INPUT];
			}
		}

		/// <summary>Gets the number of editable character positions that have already been successfully assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions in the input mask that have already been assigned a character value in the formatted string.</returns>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0005437F File Offset: 0x0005257F
		public int AssignedEditPositionCount
		{
			get
			{
				return this.assignedCharCount;
			}
		}

		/// <summary>Gets the number of editable character positions in the input mask that have not yet been assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions that not yet been assigned a character value.</returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00054387 File Offset: 0x00052587
		public int AvailableEditPositionCount
		{
			get
			{
				return this.EditPositionCount - this.assignedCharCount;
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.ComponentModel.MaskedTextProvider" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.MaskedTextProvider" /> object this method creates, cast as an object.</returns>
		// Token: 0x0600154C RID: 5452 RVA: 0x00054398 File Offset: 0x00052598
		public object Clone()
		{
			Type type = base.GetType();
			MaskedTextProvider maskedTextProvider;
			if (type == MaskedTextProvider.maskTextProviderType)
			{
				maskedTextProvider = new MaskedTextProvider(this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly);
			}
			else
			{
				object[] array = new object[] { this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly };
				maskedTextProvider = SecurityUtils.SecureCreateInstance(type, array) as MaskedTextProvider;
			}
			maskedTextProvider.ResetOnPrompt = false;
			maskedTextProvider.ResetOnSpace = false;
			maskedTextProvider.SkipLiterals = false;
			for (int i = 0; i < this.testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
				{
					maskedTextProvider.Replace(this.testString[i], i);
				}
			}
			maskedTextProvider.ResetOnPrompt = this.ResetOnPrompt;
			maskedTextProvider.ResetOnSpace = this.ResetOnSpace;
			maskedTextProvider.SkipLiterals = this.SkipLiterals;
			maskedTextProvider.IncludeLiterals = this.IncludeLiterals;
			maskedTextProvider.IncludePrompt = this.IncludePrompt;
			return maskedTextProvider;
		}

		/// <summary>Gets the culture that determines the value of the localizable separators and placeholders in the input mask.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> containing the culture information associated with the input mask.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000544DF File Offset: 0x000526DF
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the default password character used obscure user input. </summary>
		/// <returns>A <see cref="T:System.Char" /> that represents the default password character.</returns>
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000544E7 File Offset: 0x000526E7
		public static char DefaultPasswordChar
		{
			get
			{
				return '*';
			}
		}

		/// <summary>Gets the number of editable positions in the formatted string.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable positions in the formatted string.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000544EB File Offset: 0x000526EB
		public int EditPositionCount
		{
			get
			{
				return this.optionalEditChars + this.requiredEditChars;
			}
		}

		/// <summary>Gets a newly created enumerator for the editable positions in the formatted string. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that supports enumeration over the editable positions in the formatted string.</returns>
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000544FC File Offset: 0x000526FC
		public IEnumerator EditPositions
		{
			get
			{
				List<int> list = new List<int>();
				int num = 0;
				using (List<MaskedTextProvider.CharDescriptor>.Enumerator enumerator = this.stringDescriptor.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (MaskedTextProvider.IsEditPosition(enumerator.Current))
						{
							list.Add(num);
						}
						num++;
					}
				}
				return ((IEnumerable)list).GetEnumerator();
			}
		}

		/// <summary>Gets or sets a value that indicates whether literal characters in the input mask should be included in the formatted string.</summary>
		/// <returns>true if literals are included; otherwise, false. The default is true. </returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00054568 File Offset: 0x00052768
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x0005457A File Offset: 0x0005277A
		public bool IncludeLiterals
		{
			get
			{
				return this.flagState[MaskedTextProvider.INCLUDE_LITERALS];
			}
			set
			{
				this.flagState[MaskedTextProvider.INCLUDE_LITERALS] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is used to represent the absence of user input when displaying the formatted string. </summary>
		/// <returns>true if the prompt character is used to represent the positions where no user input was provided; otherwise, false. The default is true.</returns>
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0005458D File Offset: 0x0005278D
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x0005459F File Offset: 0x0005279F
		public bool IncludePrompt
		{
			get
			{
				return this.flagState[MaskedTextProvider.INCLUDE_PROMPT];
			}
			set
			{
				this.flagState[MaskedTextProvider.INCLUDE_PROMPT] = value;
			}
		}

		/// <summary>Gets a value indicating whether the mask accepts characters outside of the ASCII character set.</summary>
		/// <returns>true if only ASCII is accepted; false if <see cref="T:System.ComponentModel.MaskedTextProvider" /> can accept any arbitrary Unicode character. The default is false.</returns>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000545B2 File Offset: 0x000527B2
		public bool AsciiOnly
		{
			get
			{
				return this.flagState[MaskedTextProvider.ASCII_ONLY];
			}
		}

		/// <summary>Gets or sets a value that determines whether password protection should be applied to the formatted string.</summary>
		/// <returns>true if the input string is to be treated as a password string; otherwise, false. The default is false.</returns>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x000545C4 File Offset: 0x000527C4
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x000545CF File Offset: 0x000527CF
		public bool IsPassword
		{
			get
			{
				return this.passwordChar > '\0';
			}
			set
			{
				if (this.IsPassword != value)
				{
					this.passwordChar = (value ? MaskedTextProvider.DefaultPasswordChar : '\0');
				}
			}
		}

		/// <summary>Gets the upper bound of the range of invalid indexes.</summary>
		/// <returns>A value representing the largest invalid index, as determined by the provider implementation. For example, if the lowest valid index is 0, this property will return -1.</returns>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public static int InvalidIndex
		{
			get
			{
				return -1;
			}
		}

		/// <summary>Gets the index in the mask of the rightmost input character that has been assigned to the mask.</summary>
		/// <returns>If at least one input character has been assigned to the mask, an <see cref="T:System.Int32" /> containing the index of rightmost assigned position; otherwise, if no position has been assigned, <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x000545EB File Offset: 0x000527EB
		public int LastAssignedPosition
		{
			get
			{
				return this.FindAssignedEditPositionFrom(this.testString.Length - 1, false);
			}
		}

		/// <summary>Gets the length of the mask, absent any mask modifier characters.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of positions in the mask, excluding characters that modify mask input. </returns>
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00054601 File Offset: 0x00052801
		public int Length
		{
			get
			{
				return this.testString.Length;
			}
		}

		/// <summary>Gets the input mask.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the full mask.</returns>
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0005460E File Offset: 0x0005280E
		public string Mask
		{
			get
			{
				return this.mask;
			}
		}

		/// <summary>Gets a value indicating whether all required inputs have been entered into the formatted string.</summary>
		/// <returns>true if all required input has been entered into the mask; otherwise, false.</returns>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00054616 File Offset: 0x00052816
		public bool MaskCompleted
		{
			get
			{
				return this.requiredCharCount == this.requiredEditChars;
			}
		}

		/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the formatted string. </summary>
		/// <returns>true if all required and optional inputs have been entered; otherwise, false. </returns>
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00054626 File Offset: 0x00052826
		public bool MaskFull
		{
			get
			{
				return this.assignedCharCount == this.EditPositionCount;
			}
		}

		/// <summary>Gets or sets the character to be substituted for the actual input characters.</summary>
		/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
		/// <exception cref="T:System.InvalidOperationException">The password character specified when setting this property is the same as the current prompt character, <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00054636 File Offset: 0x00052836
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00054640 File Offset: 0x00052840
		public char PasswordChar
		{
			get
			{
				return this.passwordChar;
			}
			set
			{
				if (value == this.promptChar)
				{
					throw new InvalidOperationException(global::SR.GetString("The PasswordChar and PromptChar values cannot be the same."));
				}
				if (!MaskedTextProvider.IsValidPasswordChar(value) && value != '\0')
				{
					throw new ArgumentException(global::SR.GetString("The specified character value is not allowed for this property."));
				}
				if (value != this.passwordChar)
				{
					this.passwordChar = value;
				}
			}
		}

		/// <summary>Gets or sets the character used to represent the absence of user input for all available edit positions.</summary>
		/// <returns>The character used to prompt the user for input. The default is an underscore (_). </returns>
		/// <exception cref="T:System.InvalidOperationException">The prompt character specified when setting this property is the same as the current password character, <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x00054691 File Offset: 0x00052891
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0005469C File Offset: 0x0005289C
		public char PromptChar
		{
			get
			{
				return this.promptChar;
			}
			set
			{
				if (value == this.passwordChar)
				{
					throw new InvalidOperationException(global::SR.GetString("The PasswordChar and PromptChar values cannot be the same."));
				}
				if (!MaskedTextProvider.IsPrintableChar(value))
				{
					throw new ArgumentException(global::SR.GetString("The specified character value is not allowed for this property."));
				}
				if (value != this.promptChar)
				{
					this.promptChar = value;
					for (int i = 0; i < this.testString.Length; i++)
					{
						MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
						if (this.IsEditPosition(i) && !charDescriptor.IsAssigned)
						{
							this.testString[i] = this.promptChar;
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
		/// <returns>true if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, false to indicate that the prompt character is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00054730 File Offset: 0x00052930
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x00054742 File Offset: 0x00052942
		public bool ResetOnPrompt
		{
			get
			{
				return this.flagState[MaskedTextProvider.RESET_ON_PROMPT];
			}
			set
			{
				this.flagState[MaskedTextProvider.RESET_ON_PROMPT] = value;
			}
		}

		/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
		/// <returns>true if the space input character causes the current editable position in the mask to be reset; otherwise, false to indicate that it is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00054755 File Offset: 0x00052955
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00054767 File Offset: 0x00052967
		public bool ResetOnSpace
		{
			get
			{
				return this.flagState[MaskedTextProvider.SKIP_SPACE];
			}
			set
			{
				this.flagState[MaskedTextProvider.SKIP_SPACE] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether literal character positions in the mask can be overwritten by their same values.</summary>
		/// <returns>true to allow literals to be added back; otherwise, false to not allow the user to overwrite literal characters. The default is true.</returns>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0005477A File Offset: 0x0005297A
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x0005478C File Offset: 0x0005298C
		public bool SkipLiterals
		{
			get
			{
				return this.flagState[MaskedTextProvider.RESET_ON_LITERALS];
			}
			set
			{
				this.flagState[MaskedTextProvider.RESET_ON_LITERALS] = value;
			}
		}

		/// <summary>Gets the element at the specified position in the formatted string.</summary>
		/// <returns>The <see cref="T:System.Char" /> at the specified position in the formatted string.</returns>
		/// <param name="index">A zero-based index of the element to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than or equal to the <see cref="P:System.ComponentModel.MaskedTextProvider.Length" /> of the mask.</exception>
		// Token: 0x17000489 RID: 1161
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.testString.Length)
				{
					throw new IndexOutOfRangeException(index.ToString(CultureInfo.CurrentCulture));
				}
				return this.testString[index];
			}
		}

		/// <summary>Adds the specified input character to the end of the formatted string.</summary>
		/// <returns>true if the input character was added successfully; otherwise false.</returns>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string. </param>
		// Token: 0x06001569 RID: 5481 RVA: 0x000547D4 File Offset: 0x000529D4
		public bool Add(char input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the specified input character to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <returns>true if the input character was added successfully; otherwise false.</returns>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string.</param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600156A RID: 5482 RVA: 0x000547EC File Offset: 0x000529EC
		public bool Add(char input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == this.testString.Length - 1)
			{
				testPosition = this.testString.Length;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			testPosition = lastAssignedPosition + 1;
			testPosition = this.FindEditPositionFrom(testPosition, true);
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string.</summary>
		/// <returns>true if all the characters from the input string were added successfully; otherwise false to indicate that no characters were added.</returns>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x0600156B RID: 5483 RVA: 0x0005485C File Offset: 0x00052A5C
		public bool Add(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <returns>true if all the characters from the input string were added successfully; otherwise false to indicate that no characters were added.</returns>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string. </param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600156C RID: 5484 RVA: 0x00054874 File Offset: 0x00052A74
		public bool Add(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			testPosition = this.LastAssignedPosition + 1;
			if (input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestSetString(input, testPosition, out testPosition, out resultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters.</summary>
		// Token: 0x0600156D RID: 5485 RVA: 0x000548A8 File Offset: 0x00052AA8
		public void Clear()
		{
			MaskedTextResultHint maskedTextResultHint;
			this.Clear(out maskedTextResultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters, and then outputs descriptive information.</summary>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter. </param>
		// Token: 0x0600156E RID: 5486 RVA: 0x000548C0 File Offset: 0x00052AC0
		public void Clear(out MaskedTextResultHint resultHint)
		{
			if (this.assignedCharCount == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return;
			}
			resultHint = MaskedTextResultHint.Success;
			for (int i = 0; i < this.testString.Length; i++)
			{
				this.ResetChar(i);
			}
		}

		/// <summary>Returns the position of the first assigned editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x0600156F RID: 5487 RVA: 0x000548FC File Offset: 0x00052AFC
		public int FindAssignedEditPositionFrom(int position, bool direction)
		{
			if (this.assignedCharCount == 0)
			{
				return -1;
			}
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindAssignedEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first assigned editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001570 RID: 5488 RVA: 0x00054935 File Offset: 0x00052B35
		public int FindAssignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (this.assignedCharCount == 0)
			{
				return -1;
			}
			return this.FindEditPositionInRange(startPosition, endPosition, direction, 2);
		}

		/// <summary>Returns the position of the first editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001571 RID: 5489 RVA: 0x0005494C File Offset: 0x00052B4C
		public int FindEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001572 RID: 5490 RVA: 0x0005497C File Offset: 0x00052B7C
		public int FindEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.EditOptional | MaskedTextProvider.CharType.EditRequired;
			return this.FindPositionInRange(startPosition, endPosition, direction, charType);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00054998 File Offset: 0x00052B98
		private int FindEditPositionInRange(int startPosition, int endPosition, bool direction, byte assignedStatus)
		{
			int num;
			for (;;)
			{
				num = this.FindEditPositionInRange(startPosition, endPosition, direction);
				if (num == -1)
				{
					return -1;
				}
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num];
				if (assignedStatus != 1)
				{
					if (assignedStatus != 2)
					{
						break;
					}
					if (charDescriptor.IsAssigned)
					{
						return num;
					}
				}
				else if (!charDescriptor.IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
				if (startPosition > endPosition)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Returns the position of the first non-editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001574 RID: 5492 RVA: 0x000549F8 File Offset: 0x00052BF8
		public int FindNonEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindNonEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first non-editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001575 RID: 5493 RVA: 0x00054A28 File Offset: 0x00052C28
		public int FindNonEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Separator | MaskedTextProvider.CharType.Literal;
			return this.FindPositionInRange(startPosition, endPosition, direction, charType);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00054A44 File Offset: 0x00052C44
		private int FindPositionInRange(int startPosition, int endPosition, bool direction, MaskedTextProvider.CharType charTypeFlags)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.testString.Length)
			{
				endPosition = this.testString.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return -1;
			}
			while (startPosition <= endPosition)
			{
				int num;
				if (!direction)
				{
					endPosition = (num = endPosition) - 1;
				}
				else
				{
					startPosition = (num = startPosition) + 1;
				}
				int num2 = num;
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num2];
				if ((charDescriptor.CharType & charTypeFlags) == charDescriptor.CharType)
				{
					return num2;
				}
			}
			return -1;
		}

		/// <summary>Returns the position of the first unassigned editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001577 RID: 5495 RVA: 0x00054AB4 File Offset: 0x00052CB4
		public int FindUnassignedEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindEditPositionInRange(num, num2, direction, 1);
		}

		/// <summary>Returns the position of the first unassigned editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06001578 RID: 5496 RVA: 0x00054AE4 File Offset: 0x00052CE4
		public int FindUnassignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			for (;;)
			{
				int num = this.FindEditPositionInRange(startPosition, endPosition, direction, 0);
				if (num == -1)
				{
					break;
				}
				if (!this.stringDescriptor[num].IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
			}
			return -1;
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> denotes success or failure.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value represents a success; otherwise, false if it represents failure.</returns>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value typically obtained as an output parameter from a previous operation. </param>
		// Token: 0x06001579 RID: 5497 RVA: 0x00054B27 File Offset: 0x00052D27
		public static bool GetOperationResultFromHint(MaskedTextResultHint hint)
		{
			return hint > MaskedTextResultHint.Unknown;
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string.</summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		// Token: 0x0600157A RID: 5498 RVA: 0x00054B2D File Offset: 0x00052D2D
		public bool InsertAt(char input, int position)
		{
			return position >= 0 && position < this.testString.Length && this.InsertAt(input.ToString(), position);
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string, returning the last insertion position and the status of the operation.</summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		// Token: 0x0600157B RID: 5499 RVA: 0x00054B51 File Offset: 0x00052D51
		public bool InsertAt(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			return this.InsertAt(input.ToString(), position, out testPosition, out resultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string. </summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x0600157C RID: 5500 RVA: 0x00054B64 File Offset: 0x00052D64
		public bool InsertAt(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.InsertAt(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string, returning the last insertion position and the status of the operation. </summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x0600157D RID: 5501 RVA: 0x00054B7D File Offset: 0x00052D7D
		public bool InsertAt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.InsertAtInt(input, position, out testPosition, out resultHint, false);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00054BB8 File Offset: 0x00052DB8
		private bool InsertAtInt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			if (input.Length == 0)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			if (!this.TestString(input, position, out testPosition, out resultHint))
			{
				return false;
			}
			int i = this.FindEditPositionFrom(position, true);
			bool flag = this.FindAssignedEditPositionInRange(i, testPosition, true) != -1;
			int lastAssignedPosition = this.LastAssignedPosition;
			if (flag && testPosition == this.testString.Length - 1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			int num = this.FindEditPositionFrom(testPosition + 1, true);
			if (flag)
			{
				MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
				while (num != -1)
				{
					if (this.stringDescriptor[i].IsAssigned && !this.TestChar(this.testString[i], num, out maskedTextResultHint))
					{
						resultHint = maskedTextResultHint;
						testPosition = num;
						return false;
					}
					if (i != lastAssignedPosition)
					{
						i = this.FindEditPositionFrom(i + 1, true);
						num = this.FindEditPositionFrom(num + 1, true);
					}
					else
					{
						if (maskedTextResultHint > resultHint)
						{
							resultHint = maskedTextResultHint;
							goto IL_00EF;
						}
						goto IL_00EF;
					}
				}
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			IL_00EF:
			if (testOnly)
			{
				return true;
			}
			if (flag)
			{
				while (i >= position)
				{
					if (this.stringDescriptor[i].IsAssigned)
					{
						this.SetChar(this.testString[i], num);
					}
					else
					{
						this.ResetChar(num);
					}
					num = this.FindEditPositionFrom(num - 1, false);
					i = this.FindEditPositionFrom(i - 1, false);
				}
			}
			this.SetString(input, position);
			return true;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00054D11 File Offset: 0x00052F11
		private static bool IsAscii(char c)
		{
			return c >= '!' && c <= '~';
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00054D22 File Offset: 0x00052F22
		private static bool IsAciiAlphanumeric(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00054D49 File Offset: 0x00052F49
		private static bool IsAlphanumeric(char c)
		{
			return char.IsLetter(c) || char.IsDigit(c);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00054D5B File Offset: 0x00052F5B
		private static bool IsAsciiLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		/// <summary>Determines whether the specified position is available for assignment.</summary>
		/// <returns>true if the specified position in the formatted string is editable and has not been assigned to yet; otherwise false.</returns>
		/// <param name="position">The zero-based position in the mask to test.</param>
		// Token: 0x06001583 RID: 5507 RVA: 0x00054D78 File Offset: 0x00052F78
		public bool IsAvailablePosition(int position)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return MaskedTextProvider.IsEditPosition(charDescriptor) && !charDescriptor.IsAssigned;
		}

		/// <summary>Determines whether the specified position is editable. </summary>
		/// <returns>true if the specified position in the formatted string is editable; otherwise false.</returns>
		/// <param name="position">The zero-based position in the mask to test.</param>
		// Token: 0x06001584 RID: 5508 RVA: 0x00054DB9 File Offset: 0x00052FB9
		public bool IsEditPosition(int position)
		{
			return position >= 0 && position < this.testString.Length && MaskedTextProvider.IsEditPosition(this.stringDescriptor[position]);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00054DE0 File Offset: 0x00052FE0
		private static bool IsEditPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired || charDescriptor.CharType == MaskedTextProvider.CharType.EditOptional;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00054DF6 File Offset: 0x00052FF6
		private static bool IsLiteralPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.Literal || charDescriptor.CharType == MaskedTextProvider.CharType.Separator;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00054E0C File Offset: 0x0005300C
		private static bool IsPrintableChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ';
		}

		/// <summary>Determines whether the specified character is a valid input character.</summary>
		/// <returns>true if the specified character contains a valid input value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06001588 RID: 5512 RVA: 0x00054E2D File Offset: 0x0005302D
		public static bool IsValidInputChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		/// <summary>Determines whether the specified character is a valid mask character.</summary>
		/// <returns>true if the specified character contains a valid mask value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06001589 RID: 5513 RVA: 0x00054E2D File Offset: 0x0005302D
		public static bool IsValidMaskChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		/// <summary>Determines whether the specified character is a valid password character.</summary>
		/// <returns>true if the specified character contains a valid password value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x0600158A RID: 5514 RVA: 0x00054E35 File Offset: 0x00053035
		public static bool IsValidPasswordChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c) || c == '\0';
		}

		/// <summary>Removes the last assigned character from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		// Token: 0x0600158B RID: 5515 RVA: 0x00054E48 File Offset: 0x00053048
		public bool Remove()
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Remove(out num, out maskedTextResultHint);
		}

		/// <summary>Removes the last assigned character from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="testPosition">The zero-based position in the formatted string where the character was actually removed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600158C RID: 5516 RVA: 0x00054E60 File Offset: 0x00053060
		public bool Remove(out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == -1)
			{
				testPosition = 0;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			this.ResetChar(lastAssignedPosition);
			testPosition = lastAssignedPosition;
			resultHint = MaskedTextResultHint.Success;
			return true;
		}

		/// <summary>Removes the assigned character at the specified position from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="position">The zero-based position of the assigned character to remove.</param>
		// Token: 0x0600158D RID: 5517 RVA: 0x00054E8E File Offset: 0x0005308E
		public bool RemoveAt(int position)
		{
			return this.RemoveAt(position, position);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		// Token: 0x0600158E RID: 5518 RVA: 0x00054E98 File Offset: 0x00053098
		public bool RemoveAt(int startPosition, int endPosition)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.RemoveAt(startPosition, endPosition, out num, out maskedTextResultHint);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string of where the characters were actually removed; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600158F RID: 5519 RVA: 0x00054EB1 File Offset: 0x000530B1
		public bool RemoveAt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.RemoveAtInt(startPosition, endPosition, out testPosition, out resultHint, false);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00054EEC File Offset: 0x000530EC
		private bool RemoveAtInt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			int num = this.FindEditPositionInRange(startPosition, endPosition, true);
			resultHint = MaskedTextResultHint.NoEffect;
			if (num == -1 || num > lastAssignedPosition)
			{
				testPosition = startPosition;
				return true;
			}
			testPosition = startPosition;
			bool flag = endPosition < lastAssignedPosition;
			if (this.FindAssignedEditPositionInRange(startPosition, endPosition, true) != -1)
			{
				resultHint = MaskedTextResultHint.Success;
			}
			if (flag)
			{
				int num2 = this.FindEditPositionFrom(endPosition + 1, true);
				int num3 = num2;
				startPosition = num;
				MaskedTextResultHint maskedTextResultHint;
				for (;;)
				{
					char c = this.testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num2];
					if ((c != this.PromptChar || charDescriptor.IsAssigned) && !this.TestChar(c, num, out maskedTextResultHint))
					{
						break;
					}
					if (num2 == lastAssignedPosition)
					{
						goto IL_00B0;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				resultHint = maskedTextResultHint;
				testPosition = num;
				return false;
				IL_00B0:
				if (MaskedTextResultHint.SideEffect > resultHint)
				{
					resultHint = MaskedTextResultHint.SideEffect;
				}
				if (testOnly)
				{
					return true;
				}
				num2 = num3;
				num = startPosition;
				for (;;)
				{
					char c2 = this.testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[num2];
					if (c2 == this.PromptChar && !charDescriptor2.IsAssigned)
					{
						this.ResetChar(num);
					}
					else
					{
						this.SetChar(c2, num);
						this.ResetChar(num2);
					}
					if (num2 == lastAssignedPosition)
					{
						break;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				startPosition = num + 1;
			}
			if (startPosition <= endPosition)
			{
				this.ResetString(startPosition, endPosition);
			}
			return true;
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		// Token: 0x06001591 RID: 5521 RVA: 0x00055034 File Offset: 0x00053234
		public bool Replace(char input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06001592 RID: 5522 RVA: 0x00055050 File Offset: 0x00053250
		public bool Replace(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			testPosition = position;
			if (!this.TestEscapeChar(input, testPosition))
			{
				testPosition = this.FindEditPositionFrom(testPosition, true);
			}
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = position;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		/// <summary>Replaces a single character between the specified starting and ending positions with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts. </param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends. </param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06001593 RID: 5523 RVA: 0x000550B4 File Offset: 0x000532B4
		public bool Replace(char input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition == endPosition)
			{
				testPosition = startPosition;
				return this.TestSetChar(input, startPosition, out resultHint);
			}
			return this.Replace(input.ToString(), startPosition, endPosition, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06001594 RID: 5524 RVA: 0x00055114 File Offset: 0x00053314
		public bool Replace(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06001595 RID: 5525 RVA: 0x00055130 File Offset: 0x00053330
		public bool Replace(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(position, position, out testPosition, out resultHint);
			}
			return this.TestSetString(input, position, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters between the specified starting and ending positions with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts. </param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends. </param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06001596 RID: 5526 RVA: 0x0005518C File Offset: 0x0005338C
		public bool Replace(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(startPosition, endPosition, out testPosition, out resultHint);
			}
			if (!this.TestString(input, startPosition, out testPosition, out resultHint))
			{
				return false;
			}
			if (this.assignedCharCount > 0)
			{
				if (testPosition < endPosition)
				{
					int num;
					MaskedTextResultHint maskedTextResultHint;
					if (!this.RemoveAtInt(testPosition + 1, endPosition, out num, out maskedTextResultHint, false))
					{
						testPosition = num;
						resultHint = maskedTextResultHint;
						return false;
					}
					if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
				}
				else if (testPosition > endPosition)
				{
					int lastAssignedPosition = this.LastAssignedPosition;
					int i = testPosition + 1;
					int num2 = endPosition + 1;
					MaskedTextResultHint maskedTextResultHint;
					for (;;)
					{
						num2 = this.FindEditPositionFrom(num2, true);
						i = this.FindEditPositionFrom(i, true);
						if (i == -1)
						{
							goto Block_12;
						}
						if (!this.TestChar(this.testString[num2], i, out maskedTextResultHint))
						{
							goto Block_13;
						}
						if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
						{
							resultHint = MaskedTextResultHint.Success;
						}
						if (num2 == lastAssignedPosition)
						{
							break;
						}
						num2++;
						i++;
					}
					while (i > testPosition)
					{
						this.SetChar(this.testString[num2], i);
						num2 = this.FindEditPositionFrom(num2 - 1, false);
						i = this.FindEditPositionFrom(i - 1, false);
					}
					goto IL_0162;
					Block_12:
					testPosition = this.testString.Length;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
					Block_13:
					testPosition = i;
					resultHint = maskedTextResultHint;
					return false;
				}
			}
			IL_0162:
			this.SetString(input, startPosition);
			return true;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00055304 File Offset: 0x00053504
		private void ResetChar(int testPosition)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[testPosition];
			if (this.IsEditPosition(testPosition) && charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = false;
				this.testString[testPosition] = this.promptChar;
				this.assignedCharCount--;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this.requiredCharCount--;
				}
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0005536D File Offset: 0x0005356D
		private void ResetString(int startPosition, int endPosition)
		{
			startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
			if (startPosition != -1)
			{
				endPosition = this.FindAssignedEditPositionFrom(endPosition, false);
				while (startPosition <= endPosition)
				{
					startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
					this.ResetChar(startPosition);
					startPosition++;
				}
			}
		}

		/// <summary>Sets the formatted string to the specified input string.</summary>
		/// <returns>true if all the characters were successfully set; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x06001599 RID: 5529 RVA: 0x000553A4 File Offset: 0x000535A4
		public bool Set(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Set(input, out num, out maskedTextResultHint);
		}

		/// <summary>Sets the formatted string to the specified input string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully set; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually set; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the set operation. An output parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x0600159A RID: 5530 RVA: 0x000553BC File Offset: 0x000535BC
		public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = 0;
			if (input.Length == 0)
			{
				this.Clear(out resultHint);
				return true;
			}
			if (!this.TestSetString(input, testPosition, out testPosition, out resultHint))
			{
				return false;
			}
			int num = this.FindAssignedEditPositionFrom(testPosition + 1, true);
			if (num != -1)
			{
				this.ResetString(num, this.testString.Length - 1);
			}
			return true;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00055424 File Offset: 0x00053624
		private void SetChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			this.SetChar(input, position, charDescriptor);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00055448 File Offset: 0x00053648
		private void SetChar(char input, int position, MaskedTextProvider.CharDescriptor charDescriptor)
		{
			MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[position];
			if (this.TestEscapeChar(input, position, charDescriptor))
			{
				this.ResetChar(position);
				return;
			}
			if (char.IsLetter(input))
			{
				if (char.IsUpper(input))
				{
					if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToLower)
					{
						input = this.culture.TextInfo.ToLower(input);
					}
				}
				else if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToUpper)
				{
					input = this.culture.TextInfo.ToUpper(input);
				}
			}
			this.testString[position] = input;
			if (!charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = true;
				this.assignedCharCount++;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this.requiredCharCount++;
				}
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00055500 File Offset: 0x00053700
		private void SetString(string input, int testPosition)
		{
			foreach (char c in input)
			{
				if (!this.TestEscapeChar(c, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
				}
				this.SetChar(c, testPosition);
				testPosition++;
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0005554C File Offset: 0x0005374C
		private bool TestChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (!MaskedTextProvider.IsPrintableChar(input))
			{
				resultHint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			if (MaskedTextProvider.IsLiteralPosition(charDescriptor))
			{
				if (this.SkipLiterals && input == this.testString[position])
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
					return true;
				}
				resultHint = MaskedTextResultHint.NonEditPosition;
				return false;
			}
			else
			{
				if (input == this.promptChar)
				{
					if (this.ResetOnPrompt)
					{
						if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
						{
							resultHint = MaskedTextResultHint.SideEffect;
						}
						else
						{
							resultHint = MaskedTextResultHint.CharacterEscaped;
						}
						return true;
					}
					if (!this.AllowPromptAsInput)
					{
						resultHint = MaskedTextResultHint.PromptCharNotAllowed;
						return false;
					}
				}
				if (input == ' ' && this.ResetOnSpace)
				{
					if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
					else
					{
						resultHint = MaskedTextResultHint.CharacterEscaped;
					}
					return true;
				}
				char c = this.mask[charDescriptor.MaskPosition];
				if (c <= '0')
				{
					if (c != '#')
					{
						if (c != '&')
						{
							if (c == '0')
							{
								if (!char.IsDigit(input))
								{
									resultHint = MaskedTextResultHint.DigitExpected;
									return false;
								}
							}
						}
						else if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
					else if (!char.IsDigit(input) && input != '-' && input != '+' && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c <= 'C')
				{
					if (c != '9')
					{
						switch (c)
						{
						case '?':
							if (!char.IsLetter(input) && input != ' ')
							{
								resultHint = MaskedTextResultHint.LetterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'A':
							if (!MaskedTextProvider.IsAlphanumeric(input))
							{
								resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'C':
							if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly && input != ' ')
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						}
					}
					else if (!char.IsDigit(input) && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c != 'L')
				{
					if (c == 'a')
					{
						if (!MaskedTextProvider.IsAlphanumeric(input) && input != ' ')
						{
							resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
							return false;
						}
						if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
				}
				else
				{
					if (!char.IsLetter(input))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
					{
						resultHint = MaskedTextResultHint.AsciiCharacterExpected;
						return false;
					}
				}
				if (input == this.testString[position] && charDescriptor.IsAssigned)
				{
					resultHint = MaskedTextResultHint.NoEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.Success;
				}
				return true;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000557AC File Offset: 0x000539AC
		private bool TestEscapeChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return this.TestEscapeChar(input, position, charDescriptor);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000557D0 File Offset: 0x000539D0
		private bool TestEscapeChar(char input, int position, MaskedTextProvider.CharDescriptor charDex)
		{
			if (MaskedTextProvider.IsLiteralPosition(charDex))
			{
				return this.SkipLiterals && input == this.testString[position];
			}
			return (this.ResetOnPrompt && input == this.promptChar) || (this.ResetOnSpace && input == ' ');
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00055820 File Offset: 0x00053A20
		private bool TestSetChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (this.TestChar(input, position, out resultHint))
			{
				if (resultHint == MaskedTextResultHint.Success || resultHint == MaskedTextResultHint.SideEffect)
				{
					this.SetChar(input, position);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00055842 File Offset: 0x00053A42
		private bool TestSetString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (this.TestString(input, position, out testPosition, out resultHint))
			{
				this.SetString(input, position);
				return true;
			}
			return false;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0005585C File Offset: 0x00053A5C
		private bool TestString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = position;
			if (input.Length == 0)
			{
				return true;
			}
			MaskedTextResultHint maskedTextResultHint = resultHint;
			foreach (char c in input)
			{
				if (testPosition >= this.testString.Length)
				{
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
				}
				if (!this.TestEscapeChar(c, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
					if (testPosition == -1)
					{
						testPosition = this.testString.Length;
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						return false;
					}
				}
				if (!this.TestChar(c, testPosition, out maskedTextResultHint))
				{
					resultHint = maskedTextResultHint;
					return false;
				}
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				testPosition++;
			}
			testPosition--;
			return true;
		}

		/// <summary>Returns the formatted string in a displayable form.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes prompts and mask literals.</returns>
		// Token: 0x060015A4 RID: 5540 RVA: 0x00055908 File Offset: 0x00053B08
		public string ToDisplayString()
		{
			if (!this.IsPassword || this.assignedCharCount == 0)
			{
				return this.testString.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder(this.testString.Length);
			for (int i = 0; i < this.testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				stringBuilder.Append((MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned) ? this.passwordChar : this.testString[i]);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns the formatted string that includes all the assigned character values.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values.</returns>
		// Token: 0x060015A5 RID: 5541 RVA: 0x00055996 File Offset: 0x00053B96
		public override string ToString()
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, 0, this.testString.Length);
		}

		/// <summary>Returns the formatted string, optionally including password characters.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes literals, prompts, and optionally password characters.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		// Token: 0x060015A6 RID: 5542 RVA: 0x000559B7 File Offset: 0x00053BB7
		public string ToString(bool ignorePasswordChar)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, 0, this.testString.Length);
		}

		/// <summary>Returns a substring of the formatted string.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x060015A7 RID: 5543 RVA: 0x000559D8 File Offset: 0x00053BD8
		public string ToString(int startPosition, int length)
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including password characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes literals, prompts, and optionally password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x060015A8 RID: 5544 RVA: 0x000559EF File Offset: 0x00053BEF
		public string ToString(bool ignorePasswordChar, int startPosition, int length)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns the formatted string, optionally including prompt and literal characters.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values and optionally includes literals and prompts.</returns>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to include literal characters in the return string; otherwise, false.</param>
		// Token: 0x060015A9 RID: 5545 RVA: 0x00055A06 File Offset: 0x00053C06
		public string ToString(bool includePrompt, bool includeLiterals)
		{
			return this.ToString(true, includePrompt, includeLiterals, 0, this.testString.Length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt and literal characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals and prompts; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to include literal characters in the return string; otherwise, false.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x060015AA RID: 5546 RVA: 0x00055A1D File Offset: 0x00053C1D
		public string ToString(bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			return this.ToString(true, includePrompt, includeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt, literal, and password characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals, prompts, and password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to return literal characters in the return string; otherwise, false.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x060015AB RID: 5547 RVA: 0x00055A2C File Offset: 0x00053C2C
		public string ToString(bool ignorePasswordChar, bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			if (length <= 0)
			{
				return string.Empty;
			}
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (startPosition >= this.testString.Length)
			{
				return string.Empty;
			}
			int num = this.testString.Length - startPosition;
			if (length > num)
			{
				length = num;
			}
			if ((!this.IsPassword || ignorePasswordChar) && (includePrompt && includeLiterals))
			{
				return this.testString.ToString(startPosition, length);
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = startPosition + length - 1;
			if (!includePrompt)
			{
				int num3 = (includeLiterals ? this.FindNonEditPositionInRange(startPosition, num2, false) : MaskedTextProvider.InvalidIndex);
				int num4 = this.FindAssignedEditPositionInRange((num3 == MaskedTextProvider.InvalidIndex) ? startPosition : num3, num2, false);
				num2 = ((num4 != MaskedTextProvider.InvalidIndex) ? num4 : num3);
				if (num2 == MaskedTextProvider.InvalidIndex)
				{
					return string.Empty;
				}
			}
			int i = startPosition;
			while (i <= num2)
			{
				char c = this.testString[i];
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				MaskedTextProvider.CharType charType = charDescriptor.CharType;
				if (charType - MaskedTextProvider.CharType.EditOptional > 1)
				{
					if (charType != MaskedTextProvider.CharType.Separator && charType != MaskedTextProvider.CharType.Literal)
					{
						goto IL_012F;
					}
					if (includeLiterals)
					{
						goto IL_012F;
					}
				}
				else if (charDescriptor.IsAssigned)
				{
					if (!this.IsPassword || ignorePasswordChar)
					{
						goto IL_012F;
					}
					stringBuilder.Append(this.passwordChar);
				}
				else
				{
					if (includePrompt)
					{
						goto IL_012F;
					}
					stringBuilder.Append(' ');
				}
				IL_0138:
				i++;
				continue;
				IL_012F:
				stringBuilder.Append(c);
				goto IL_0138;
			}
			return stringBuilder.ToString();
		}

		/// <summary>Tests whether the specified character could be set successfully at the specified position.</summary>
		/// <returns>true if the specified character is valid for the specified position; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x060015AC RID: 5548 RVA: 0x00055B85 File Offset: 0x00053D85
		public bool VerifyChar(char input, int position, out MaskedTextResultHint hint)
		{
			hint = MaskedTextResultHint.NoEffect;
			if (position < 0 || position >= this.testString.Length)
			{
				hint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.TestChar(input, position, out hint);
		}

		/// <summary>Tests whether the specified character would be escaped at the specified position.</summary>
		/// <returns>true if the specified character would be escaped at the specified position; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		// Token: 0x060015AD RID: 5549 RVA: 0x00055BAB File Offset: 0x00053DAB
		public bool VerifyEscapeChar(char input, int position)
		{
			return position >= 0 && position < this.testString.Length && this.TestEscapeChar(input, position);
		}

		/// <summary>Tests whether the specified string could be set successfully.</summary>
		/// <returns>true if the specified string represents valid input; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		// Token: 0x060015AE RID: 5550 RVA: 0x00055BCC File Offset: 0x00053DCC
		public bool VerifyString(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.VerifyString(input, out num, out maskedTextResultHint);
		}

		/// <summary>Tests whether the specified string could be set successfully, and then outputs position and descriptive information.</summary>
		/// <returns>true if the specified string represents valid input; otherwise, false. </returns>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		/// <param name="testPosition">If successful, the zero-based position of the last character actually tested; otherwise, the first position where the test failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the test operation. An output parameter.</param>
		// Token: 0x060015AF RID: 5551 RVA: 0x00055BE4 File Offset: 0x00053DE4
		public bool VerifyString(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			testPosition = 0;
			if (input == null || input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestString(input, 0, out testPosition, out resultHint);
		}

		// Token: 0x0400132E RID: 4910
		private const char spaceChar = ' ';

		// Token: 0x0400132F RID: 4911
		private const char defaultPromptChar = '_';

		// Token: 0x04001330 RID: 4912
		private const char nullPasswordChar = '\0';

		// Token: 0x04001331 RID: 4913
		private const bool defaultAllowPrompt = true;

		// Token: 0x04001332 RID: 4914
		private const int invalidIndex = -1;

		// Token: 0x04001333 RID: 4915
		private const byte editAny = 0;

		// Token: 0x04001334 RID: 4916
		private const byte editUnassigned = 1;

		// Token: 0x04001335 RID: 4917
		private const byte editAssigned = 2;

		// Token: 0x04001336 RID: 4918
		private const bool forward = true;

		// Token: 0x04001337 RID: 4919
		private const bool backward = false;

		// Token: 0x04001338 RID: 4920
		private static int ASCII_ONLY = BitVector32.CreateMask();

		// Token: 0x04001339 RID: 4921
		private static int ALLOW_PROMPT_AS_INPUT = BitVector32.CreateMask(MaskedTextProvider.ASCII_ONLY);

		// Token: 0x0400133A RID: 4922
		private static int INCLUDE_PROMPT = BitVector32.CreateMask(MaskedTextProvider.ALLOW_PROMPT_AS_INPUT);

		// Token: 0x0400133B RID: 4923
		private static int INCLUDE_LITERALS = BitVector32.CreateMask(MaskedTextProvider.INCLUDE_PROMPT);

		// Token: 0x0400133C RID: 4924
		private static int RESET_ON_PROMPT = BitVector32.CreateMask(MaskedTextProvider.INCLUDE_LITERALS);

		// Token: 0x0400133D RID: 4925
		private static int RESET_ON_LITERALS = BitVector32.CreateMask(MaskedTextProvider.RESET_ON_PROMPT);

		// Token: 0x0400133E RID: 4926
		private static int SKIP_SPACE = BitVector32.CreateMask(MaskedTextProvider.RESET_ON_LITERALS);

		// Token: 0x0400133F RID: 4927
		private static Type maskTextProviderType = typeof(MaskedTextProvider);

		// Token: 0x04001340 RID: 4928
		private BitVector32 flagState;

		// Token: 0x04001341 RID: 4929
		private CultureInfo culture;

		// Token: 0x04001342 RID: 4930
		private StringBuilder testString;

		// Token: 0x04001343 RID: 4931
		private int assignedCharCount;

		// Token: 0x04001344 RID: 4932
		private int requiredCharCount;

		// Token: 0x04001345 RID: 4933
		private int requiredEditChars;

		// Token: 0x04001346 RID: 4934
		private int optionalEditChars;

		// Token: 0x04001347 RID: 4935
		private string mask;

		// Token: 0x04001348 RID: 4936
		private char passwordChar;

		// Token: 0x04001349 RID: 4937
		private char promptChar;

		// Token: 0x0400134A RID: 4938
		private List<MaskedTextProvider.CharDescriptor> stringDescriptor;

		// Token: 0x020002B0 RID: 688
		private enum CaseConversion
		{
			// Token: 0x0400134C RID: 4940
			None,
			// Token: 0x0400134D RID: 4941
			ToLower,
			// Token: 0x0400134E RID: 4942
			ToUpper
		}

		// Token: 0x020002B1 RID: 689
		[Flags]
		private enum CharType
		{
			// Token: 0x04001350 RID: 4944
			EditOptional = 1,
			// Token: 0x04001351 RID: 4945
			EditRequired = 2,
			// Token: 0x04001352 RID: 4946
			Separator = 4,
			// Token: 0x04001353 RID: 4947
			Literal = 8,
			// Token: 0x04001354 RID: 4948
			Modifier = 16
		}

		// Token: 0x020002B2 RID: 690
		private class CharDescriptor
		{
			// Token: 0x060015B1 RID: 5553 RVA: 0x00055C84 File Offset: 0x00053E84
			public CharDescriptor(int maskPos, MaskedTextProvider.CharType charType)
			{
				this.MaskPosition = maskPos;
				this.CharType = charType;
			}

			// Token: 0x060015B2 RID: 5554 RVA: 0x00055C9C File Offset: 0x00053E9C
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "MaskPosition[{0}] <CaseConversion.{1}><CharType.{2}><IsAssigned: {3}", new object[] { this.MaskPosition, this.CaseConversion, this.CharType, this.IsAssigned });
			}

			// Token: 0x04001355 RID: 4949
			public int MaskPosition;

			// Token: 0x04001356 RID: 4950
			public MaskedTextProvider.CaseConversion CaseConversion;

			// Token: 0x04001357 RID: 4951
			public MaskedTextProvider.CharType CharType;

			// Token: 0x04001358 RID: 4952
			public bool IsAssigned;
		}
	}
}
