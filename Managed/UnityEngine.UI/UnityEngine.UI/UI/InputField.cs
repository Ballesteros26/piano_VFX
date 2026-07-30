using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000018 RID: 24
	[AddComponentMenu("UI/Input Field", 31)]
	public class InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000090E8 File Offset: 0x000072E8
		private BaseInput input
		{
			get
			{
				if (EventSystem.current && EventSystem.current.currentInputModule)
				{
					return EventSystem.current.currentInputModule.input;
				}
				return null;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00009118 File Offset: 0x00007318
		private string compositionString
		{
			get
			{
				if (!(this.input != null))
				{
					return Input.compositionString;
				}
				return this.input.compositionString;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000913C File Offset: 0x0000733C
		protected InputField()
		{
			this.EnforceTextHOverflow();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000091E4 File Offset: 0x000073E4
		protected Mesh mesh
		{
			get
			{
				if (this.m_Mesh == null)
				{
					this.m_Mesh = new Mesh();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00009205 File Offset: 0x00007405
		protected TextGenerator cachedInputTextGenerator
		{
			get
			{
				if (this.m_InputTextCache == null)
				{
					this.m_InputTextCache = new TextGenerator();
				}
				return this.m_InputTextCache;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00009230 File Offset: 0x00007430
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00009220 File Offset: 0x00007420
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform != RuntimePlatform.tvOS) || this.m_HideMobileInput;
			}
			set
			{
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009259 File Offset: 0x00007459
		private bool shouldActivateOnSelect
		{
			get
			{
				return Application.platform != RuntimePlatform.tvOS;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00009267 File Offset: 0x00007467
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000926F File Offset: 0x0000746F
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.SetText(value, true);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009279 File Offset: 0x00007479
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009284 File Offset: 0x00007484
		private void SetText(string value, bool sendCallback = true)
		{
			if (this.text == value)
			{
				return;
			}
			if (value == null)
			{
				value = "";
			}
			value = value.Replace("\0", string.Empty);
			if (this.m_LineType == InputField.LineType.SingleLine)
			{
				value = value.Replace("\n", "").Replace("\t", "");
			}
			if (this.onValidateInput != null || this.characterValidation != InputField.CharacterValidation.None)
			{
				this.m_Text = "";
				InputField.OnValidateInput onValidateInput = this.onValidateInput ?? new InputField.OnValidateInput(this.Validate);
				this.m_CaretPosition = (this.m_CaretSelectPosition = value.Length);
				int num = ((this.characterLimit > 0) ? Math.Min(this.characterLimit, value.Length) : value.Length);
				for (int i = 0; i < num; i++)
				{
					char c = onValidateInput(this.m_Text, this.m_Text.Length, value[i]);
					if (c != '\0')
					{
						this.m_Text += c.ToString();
					}
				}
			}
			else
			{
				this.m_Text = ((this.characterLimit > 0 && value.Length > this.characterLimit) ? value.Substring(0, this.characterLimit) : value);
			}
			if (this.m_Keyboard != null)
			{
				this.m_Keyboard.text = this.m_Text;
			}
			if (this.m_CaretPosition > this.m_Text.Length)
			{
				this.m_CaretPosition = (this.m_CaretSelectPosition = this.m_Text.Length);
			}
			else if (this.m_CaretSelectPosition > this.m_Text.Length)
			{
				this.m_CaretSelectPosition = this.m_Text.Length;
			}
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
			this.UpdateLabel();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00009444 File Offset: 0x00007644
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000944C File Offset: 0x0000764C
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00009454 File Offset: 0x00007654
		public float caretBlinkRate
		{
			get
			{
				return this.m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_CaretBlinkRate, value) && this.m_AllowInput)
				{
					this.SetCaretActive();
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00009472 File Offset: 0x00007672
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000947A File Offset: 0x0000767A
		public int caretWidth
		{
			get
			{
				return this.m_CaretWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CaretWidth, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00009490 File Offset: 0x00007690
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00009498 File Offset: 0x00007698
		public Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (this.m_TextComponent != null)
				{
					this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
					this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
					this.m_TextComponent.UnregisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
				}
				if (SetPropertyUtility.SetClass<Text>(ref this.m_TextComponent, value))
				{
					this.EnforceTextHOverflow();
					if (this.m_TextComponent != null)
					{
						this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
						this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
						this.m_TextComponent.RegisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
					}
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000955F File Offset: 0x0000775F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00009567 File Offset: 0x00007767
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass<Graphic>(ref this.m_Placeholder, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00009576 File Offset: 0x00007776
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00009592 File Offset: 0x00007792
		public Color caretColor
		{
			get
			{
				if (!this.customCaretColor)
				{
					return this.textComponent.color;
				}
				return this.m_CaretColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_CaretColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000095A8 File Offset: 0x000077A8
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000095B0 File Offset: 0x000077B0
		public bool customCaretColor
		{
			get
			{
				return this.m_CustomCaretColor;
			}
			set
			{
				if (this.m_CustomCaretColor != value)
				{
					this.m_CustomCaretColor = value;
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000095C8 File Offset: 0x000077C8
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000095D0 File Offset: 0x000077D0
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_SelectionColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000095E6 File Offset: 0x000077E6
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000095EE File Offset: 0x000077EE
		public InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_OnEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.SubmitEvent>(ref this.m_OnEndEdit, value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000095FD File Offset: 0x000077FD
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00009605 File Offset: 0x00007805
		[Obsolete("onValueChange has been renamed to onValueChanged")]
		public InputField.OnChangeEvent onValueChange
		{
			get
			{
				return this.onValueChanged;
			}
			set
			{
				this.onValueChanged = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000960E File Offset: 0x0000780E
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00009616 File Offset: 0x00007816
		public InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00009625 File Offset: 0x00007825
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000962D File Offset: 0x0000782D
		public InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000963C File Offset: 0x0000783C
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00009644 File Offset: 0x00007844
		public int characterLimit
		{
			get
			{
				return this.m_CharacterLimit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CharacterLimit, Math.Max(0, value)))
				{
					this.UpdateLabel();
					if (this.m_Keyboard != null)
					{
						this.m_Keyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00009674 File Offset: 0x00007874
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000967C File Offset: 0x0000787C
		public InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00009692 File Offset: 0x00007892
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000969A File Offset: 0x0000789A
		public InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new InputField.ContentType[]
					{
						InputField.ContentType.Standard,
						InputField.ContentType.Autocorrected
					});
					this.EnforceTextHOverflow();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000096C0 File Offset: 0x000078C0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000096C8 File Offset: 0x000078C8
		public InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000096DE File Offset: 0x000078DE
		public TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				return this.m_Keyboard;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000096E6 File Offset: 0x000078E6
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000096EE File Offset: 0x000078EE
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TouchScreenKeyboardType>(ref this.m_KeyboardType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009704 File Offset: 0x00007904
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000970C File Offset: 0x0000790C
		public InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009722 File Offset: 0x00007922
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000972A File Offset: 0x0000792A
		public bool readOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
			set
			{
				this.m_ReadOnly = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009733 File Offset: 0x00007933
		public bool multiLine
		{
			get
			{
				return this.m_LineType == InputField.LineType.MultiLineNewline || this.lineType == InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00009749 File Offset: 0x00007949
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00009751 File Offset: 0x00007951
		public char asteriskChar
		{
			get
			{
				return this.m_AsteriskChar;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<char>(ref this.m_AsteriskChar, value))
				{
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00009767 File Offset: 0x00007967
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000976F File Offset: 0x0000796F
		protected void ClampPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00009796 File Offset: 0x00007996
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000097AA File Offset: 0x000079AA
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionString.Length;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000097BF File Offset: 0x000079BF
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000097D3 File Offset: 0x000079D3
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000097E8 File Offset: 0x000079E8
		private bool hasSelection
		{
			get
			{
				return this.caretPositionInternal != this.caretSelectPositionInternal;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000097BF File Offset: 0x000079BF
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000097FB File Offset: 0x000079FB
		public int caretPosition
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009796 File Offset: 0x00007996
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000980B File Offset: 0x00007A0B
		public int selectionAnchorPosition
		{
			get
			{
				return this.m_CaretPosition + this.compositionString.Length;
			}
			set
			{
				if (this.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000097BF File Offset: 0x000079BF
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000982E File Offset: 0x00007A2E
		public int selectionFocusPosition
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				if (this.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009854 File Offset: 0x00007A54
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_DrawStart = 0;
			this.m_DrawEnd = this.m_Text.Length;
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.m_TextComponent.RegisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
				this.UpdateLabel();
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000991C File Offset: 0x00007B1C
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.m_TextComponent.UnregisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			base.OnDisable();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000099CE File Offset: 0x00007BCE
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while (this.isFocused && this.m_CaretBlinkRate > 0f)
			{
				float num = 1f / this.m_CaretBlinkRate;
				bool flag = (Time.unscaledTime - this.m_BlinkStartTime) % num < num / 2f;
				if (this.m_CaretVisible != flag)
				{
					this.m_CaretVisible = flag;
					if (!this.hasSelection)
					{
						this.MarkGeometryAsDirty();
					}
				}
				yield return null;
			}
			this.m_BlinkCoroutine = null;
			yield break;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000099DD File Offset: 0x00007BDD
		private void SetCaretVisible()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_CaretVisible = true;
			this.m_BlinkStartTime = Time.unscaledTime;
			this.SetCaretActive();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009A00 File Offset: 0x00007C00
		private void SetCaretActive()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			if (this.m_CaretBlinkRate > 0f)
			{
				if (this.m_BlinkCoroutine == null)
				{
					this.m_BlinkCoroutine = base.StartCoroutine(this.CaretBlink());
					return;
				}
			}
			else
			{
				this.m_CaretVisible = true;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009A3A File Offset: 0x00007C3A
		private void UpdateCaretMaterial()
		{
			if (this.m_TextComponent != null && this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009A78 File Offset: 0x00007C78
		protected void OnFocus()
		{
			this.SelectAll();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009A80 File Offset: 0x00007C80
		protected void SelectAll()
		{
			this.caretPositionInternal = this.text.Length;
			this.caretSelectPositionInternal = 0;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009A9C File Offset: 0x00007C9C
		public void MoveTextEnd(bool shift)
		{
			int length = this.text.Length;
			if (shift)
			{
				this.caretSelectPositionInternal = length;
			}
			else
			{
				this.caretPositionInternal = length;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009ADC File Offset: 0x00007CDC
		public void MoveTextStart(bool shift)
		{
			int num = 0;
			if (shift)
			{
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009B10 File Offset: 0x00007D10
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009B17 File Offset: 0x00007D17
		private static string clipboard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009B1F File Offset: 0x00007D1F
		private bool InPlaceEditing()
		{
			return !TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009B30 File Offset: 0x00007D30
		private void UpdateCaretFromKeyboard()
		{
			RangeInt selection = this.m_Keyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (this.caretPositionInternal != start)
			{
				flag = true;
				this.caretPositionInternal = start;
			}
			if (this.caretSelectPositionInternal != end)
			{
				this.caretSelectPositionInternal = end;
				flag = true;
			}
			if (flag)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009B94 File Offset: 0x00007D94
		protected virtual void LateUpdate()
		{
			if (this.m_ShouldActivateNextUpdate)
			{
				if (!this.isFocused)
				{
					this.ActivateInputFieldInternal();
					this.m_ShouldActivateNextUpdate = false;
					return;
				}
				this.m_ShouldActivateNextUpdate = false;
			}
			this.AssignPositioningIfNeeded();
			if (!this.isFocused || this.InPlaceEditing())
			{
				return;
			}
			if (this.m_Keyboard == null || this.m_Keyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_Keyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_Keyboard.text;
					}
					if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Canceled)
					{
						this.m_WasCanceled = true;
					}
				}
				this.OnDeselect(null);
				return;
			}
			string text = this.m_Keyboard.text;
			if (this.m_Text != text)
			{
				if (this.m_ReadOnly)
				{
					this.m_Keyboard.text = this.m_Text;
				}
				else
				{
					this.m_Text = "";
					foreach (char c in text)
					{
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (this.onValidateInput != null)
						{
							c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
						}
						else if (this.characterValidation != InputField.CharacterValidation.None)
						{
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
						}
						if (this.lineType == InputField.LineType.MultiLineSubmit && c == '\n')
						{
							this.m_Keyboard.text = this.m_Text;
							this.OnDeselect(null);
							return;
						}
						if (c != '\0')
						{
							this.m_Text += c.ToString();
						}
					}
					if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
					{
						this.m_Text = this.m_Text.Substring(0, this.characterLimit);
					}
					if (this.m_Keyboard.canGetSelection)
					{
						this.UpdateCaretFromKeyboard();
					}
					else
					{
						this.caretPositionInternal = (this.caretSelectPositionInternal = this.m_Text.Length);
					}
					if (this.m_Text != text)
					{
						this.m_Keyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && this.m_Keyboard.canSetSelection)
			{
				this.m_Keyboard.selection = new RangeInt(this.caretPositionInternal, this.caretSelectPositionInternal - this.caretPositionInternal);
			}
			else if (this.m_Keyboard.canGetSelection && !this.m_HideMobileInput)
			{
				this.UpdateCaretFromKeyboard();
			}
			if (this.m_Keyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009E34 File Offset: 0x00008034
		[Obsolete("This function is no longer used. Please use RectTransformUtility.ScreenPointToLocalPointInRectangle() instead.")]
		public Vector2 ScreenToLocal(Vector2 screen)
		{
			Canvas canvas = this.m_TextComponent.canvas;
			if (canvas == null)
			{
				return screen;
			}
			Vector3 vector = Vector3.zero;
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				vector = this.m_TextComponent.transform.InverseTransformPoint(screen);
			}
			else if (canvas.worldCamera != null)
			{
				Ray ray = canvas.worldCamera.ScreenPointToRay(screen);
				Plane plane = new Plane(this.m_TextComponent.transform.forward, this.m_TextComponent.transform.position);
				float num;
				plane.Raycast(ray, out num);
				vector = this.m_TextComponent.transform.InverseTransformPoint(ray.GetPoint(num));
			}
			return new Vector2(vector.x, vector.y);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009EFC File Offset: 0x000080FC
		private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			float num = pos.y * this.m_TextComponent.pixelsPerUnit;
			float num2 = 0f;
			int i = 0;
			while (i < generator.lineCount)
			{
				float topY = generator.lines[i].topY;
				float num3 = topY - (float)generator.lines[i].height;
				if (num > topY)
				{
					float num4 = topY - num2;
					if (num > topY - 0.5f * num4)
					{
						return i - 1;
					}
					return i;
				}
				else
				{
					if (num > num3)
					{
						return i;
					}
					num2 = num3;
					i++;
				}
			}
			return generator.lineCount;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009F94 File Offset: 0x00008194
		protected int GetCharacterIndexFromPosition(Vector2 pos)
		{
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount == 0)
			{
				return 0;
			}
			int unclampedCharacterLineFromPosition = this.GetUnclampedCharacterLineFromPosition(pos, cachedTextGenerator);
			if (unclampedCharacterLineFromPosition < 0)
			{
				return 0;
			}
			if (unclampedCharacterLineFromPosition >= cachedTextGenerator.lineCount)
			{
				return cachedTextGenerator.characterCountVisible;
			}
			int startCharIdx = cachedTextGenerator.lines[unclampedCharacterLineFromPosition].startCharIdx;
			int lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, unclampedCharacterLineFromPosition);
			int num = startCharIdx;
			while (num < lineEndPosition && num < cachedTextGenerator.characterCountVisible)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num];
				Vector2 vector = uicharInfo.cursorPos / this.m_TextComponent.pixelsPerUnit;
				float num2 = pos.x - vector.x;
				float num3 = vector.x + uicharInfo.charWidth / this.m_TextComponent.pixelsPerUnit - pos.x;
				if (num2 < num3)
				{
					return num;
				}
				num++;
			}
			return lineEndPosition;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A063 File Offset: 0x00008263
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.InPlaceEditing() || this.m_HideMobileInput);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000A09D File Offset: 0x0000829D
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
			{
				return;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, zero, eventData.pressEventCamera, out vector);
			this.caretSelectPositionInternal = this.GetCharacterIndexFromPosition(vector) + this.m_DrawStart;
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A15A File Offset: 0x0000835A
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 zero = Vector2.zero;
				if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
				{
					break;
				}
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, zero, eventData.pressEventCamera, out vector);
				Rect rect = this.textComponent.rectTransform.rect;
				if (this.multiLine)
				{
					if (vector.y > rect.yMax)
					{
						this.MoveUp(true, true);
					}
					else if (vector.y < rect.yMin)
					{
						this.MoveDown(true, true);
					}
				}
				else if (vector.x < rect.xMin)
				{
					this.MoveLeft(true, false);
				}
				else if (vector.x > rect.xMax)
				{
					this.MoveRight(true, false);
				}
				this.UpdateLabel();
				float num = (this.multiLine ? 0.1f : 0.05f);
				if (this.m_WaitForSecondsRealtime == null)
				{
					this.m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num);
				}
				else
				{
					this.m_WaitForSecondsRealtime.waitTime = num;
				}
				yield return this.m_WaitForSecondsRealtime;
			}
			this.m_DragCoroutine = null;
			yield break;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A170 File Offset: 0x00008370
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A184 File Offset: 0x00008384
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_Keyboard == null || !this.m_Keyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			if (allowInput)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector);
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCharacterIndexFromPosition(vector) + this.m_DrawStart);
			}
			this.UpdateLabel();
			eventData.Use();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A230 File Offset: 0x00008430
		protected InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			bool flag = ((SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) ? ((modifiers & EventModifiers.Command) > EventModifiers.None) : ((modifiers & EventModifiers.Control) > EventModifiers.None));
			bool flag2 = (modifiers & EventModifiers.Shift) > EventModifiers.None;
			bool flag3 = (modifiers & EventModifiers.Alt) > EventModifiers.None;
			bool flag4 = flag && !flag3 && !flag2;
			KeyCode keyCode = evt.keyCode;
			if (keyCode <= KeyCode.A)
			{
				if (keyCode <= KeyCode.Return)
				{
					if (keyCode == KeyCode.Backspace)
					{
						this.Backspace();
						return InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_01C4;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_WasCanceled = true;
						return InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_01C4;
					}
					if (flag4)
					{
						this.SelectAll();
						return InputField.EditState.Continue;
					}
					goto IL_01C4;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_01C4;
					}
					if (flag4)
					{
						this.Append(InputField.clipboard);
						this.UpdateLabel();
						return InputField.EditState.Continue;
					}
					goto IL_01C4;
				}
				else
				{
					if (flag4)
					{
						if (this.inputType != InputField.InputType.Password)
						{
							InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							InputField.clipboard = "";
						}
						return InputField.EditState.Continue;
					}
					goto IL_01C4;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.ForwardSpace();
					return InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.Insert:
					goto IL_01C4;
				case KeyCode.UpArrow:
					this.MoveUp(flag2);
					return InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(flag2);
					return InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(flag2, flag);
					return InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(flag2, flag);
					return InputField.EditState.Continue;
				case KeyCode.Home:
					this.MoveTextStart(flag2);
					return InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveTextEnd(flag2);
					return InputField.EditState.Continue;
				default:
					goto IL_01C4;
				}
			}
			else
			{
				if (flag4)
				{
					if (this.inputType != InputField.InputType.Password)
					{
						InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return InputField.EditState.Continue;
				}
				goto IL_01C4;
			}
			if (this.lineType != InputField.LineType.MultiLineNewline)
			{
				return InputField.EditState.Finish;
			}
			IL_01C4:
			char c = evt.character;
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionString.Length > 0)
			{
				this.UpdateLabel();
			}
			return InputField.EditState.Continue;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A45F File Offset: 0x0000865F
		private bool IsValidChar(char c)
		{
			return c != '\u007f' && (c == '\t' || c == '\n' || this.m_TextComponent.font.HasCharacter(c));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A485 File Offset: 0x00008685
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A490 File Offset: 0x00008690
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				if (this.m_ProcessingEvent.rawType == EventType.KeyDown)
				{
					flag = true;
					if (this.KeyPressed(this.m_ProcessingEvent) == InputField.EditState.Finish)
					{
						this.DeactivateInputField();
						break;
					}
				}
				EventType type = this.m_ProcessingEvent.type;
				if (type - EventType.ValidateCommand <= 1)
				{
					string commandName = this.m_ProcessingEvent.commandName;
					if (commandName == "SelectAll")
					{
						this.SelectAll();
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A524 File Offset: 0x00008724
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int num = this.caretPositionInternal;
			int num2 = this.caretSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return this.text.Substring(num, num2 - num);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A564 File Offset: 0x00008764
		private int FindtNextWordBegin()
		{
			if (this.caretSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal + 1);
			if (num == -1)
			{
				num = this.text.Length;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A5C4 File Offset: 0x000087C4
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindtNextWordBegin();
			}
			else
			{
				num = this.caretSelectPositionInternal + 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A630 File Offset: 0x00008830
		private int FindtPrevWordBegin()
		{
			if (this.caretSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal - 2);
			if (num == -1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A670 File Offset: 0x00008870
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindtPrevWordBegin();
			}
			else
			{
				num = this.caretSelectPositionInternal - 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A6DC File Offset: 0x000088DC
		private int DetermineCharacterLine(int charPos, TextGenerator generator)
		{
			for (int i = 0; i < generator.lineCount - 1; i++)
			{
				if (generator.lines[i + 1].startCharIdx > charPos)
				{
					return i;
				}
			}
			return generator.lineCount - 1;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A71C File Offset: 0x0000891C
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characters.Count)
			{
				return 0;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num > 0)
			{
				int num2 = this.cachedInputTextGenerator.lines[num].startCharIdx - 1;
				for (int i = this.cachedInputTextGenerator.lines[num - 1].startCharIdx; i < num2; i++)
				{
					if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
					{
						return i;
					}
				}
				return num2;
			}
			if (!goToFirstChar)
			{
				return originalPos;
			}
			return 0;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A7D0 File Offset: 0x000089D0
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return this.text.Length;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num + 1 < this.cachedInputTextGenerator.lineCount)
			{
				int lineEndPosition = InputField.GetLineEndPosition(this.cachedInputTextGenerator, num + 1);
				for (int i = this.cachedInputTextGenerator.lines[num + 1].startCharIdx; i < lineEndPosition; i++)
				{
					if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
					{
						return i;
					}
				}
				return lineEndPosition;
			}
			if (!goToLastChar)
			{
				return originalPos;
			}
			return this.text.Length;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A895 File Offset: 0x00008A95
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : this.text.Length);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretPositionInternal = (this.caretSelectPositionInternal = num);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A916 File Offset: 0x00008B16
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A920 File Offset: 0x00008B20
		private void MoveUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A98C File Offset: 0x00008B8C
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.caretPositionInternal == this.caretSelectPositionInternal)
			{
				return;
			}
			if (this.caretPositionInternal < this.caretSelectPositionInternal)
			{
				this.m_Text = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = this.caretPositionInternal;
				return;
			}
			this.m_Text = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
			this.caretPositionInternal = this.caretSelectPositionInternal;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AA58 File Offset: 0x00008C58
		private void ForwardSpace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.caretPositionInternal < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal, 1);
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.caretPositionInternal > 0)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal - 1, 1);
				this.caretSelectPositionInternal = --this.caretPositionInternal;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000AB38 File Offset: 0x00008D38
		private void Insert(char c)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			string text = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_CaretPosition, text);
			this.caretSelectPositionInternal = (this.caretPositionInternal += text.Length);
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000ABB9 File Offset: 0x00008DB9
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_Keyboard != null && this.InPlaceEditing())
			{
				this.m_Keyboard.text = this.m_Text;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000ABDC File Offset: 0x00008DDC
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.SendOnValueChanged();
			this.UpdateLabel();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000ABEA File Offset: 0x00008DEA
		private void SendOnValueChanged()
		{
			UISystemProfilerApi.AddMarker("InputField.value", this);
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000AC10 File Offset: 0x00008E10
		protected void SendOnSubmit()
		{
			UISystemProfilerApi.AddMarker("InputField.onSubmit", this);
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000AC38 File Offset: 0x00008E38
		protected virtual void Append(string input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int i = 0;
			int length = input.Length;
			while (i < length)
			{
				char c = input[i];
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\n')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000AC94 File Offset: 0x00008E94
		protected virtual void Append(char input)
		{
			if (char.IsSurrogate(input))
			{
				return;
			}
			if (this.m_ReadOnly || this.text.Length >= 16382)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int num = Math.Min(this.selectionFocusPosition, this.selectionAnchorPosition);
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(this.text, num, input);
			}
			else if (this.characterValidation != InputField.CharacterValidation.None)
			{
				input = this.Validate(this.text, num, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000AD24 File Offset: 0x00008F24
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventFontCallback)
			{
				this.m_PreventFontCallback = true;
				string text;
				if (EventSystem.current != null && base.gameObject == EventSystem.current.currentSelectedGameObject && this.compositionString.Length > 0)
				{
					text = this.text.Substring(0, this.m_CaretPosition) + this.compositionString + this.text.Substring(this.m_CaretPosition);
				}
				else
				{
					text = this.text;
				}
				string text2;
				if (this.inputType == InputField.InputType.Password)
				{
					text2 = new string(this.asteriskChar, text.Length);
				}
				else
				{
					text2 = text;
				}
				bool flag = string.IsNullOrEmpty(text);
				if (this.m_Placeholder != null)
				{
					this.m_Placeholder.enabled = flag;
				}
				if (!this.m_AllowInput)
				{
					this.m_DrawStart = 0;
					this.m_DrawEnd = this.m_Text.Length;
				}
				if (!flag)
				{
					Vector2 size = this.m_TextComponent.rectTransform.rect.size;
					TextGenerationSettings generationSettings = this.m_TextComponent.GetGenerationSettings(size);
					generationSettings.generateOutOfBounds = true;
					this.cachedInputTextGenerator.PopulateWithErrors(text2, generationSettings, base.gameObject);
					this.SetDrawRangeToContainCaretPosition(this.caretSelectPositionInternal);
					text2 = text2.Substring(this.m_DrawStart, Mathf.Min(this.m_DrawEnd, text2.Length) - this.m_DrawStart);
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2;
				this.MarkGeometryAsDirty();
				this.m_PreventFontCallback = false;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000AECC File Offset: 0x000090CC
		private bool IsSelectionVisible()
		{
			return this.m_DrawStart <= this.caretPositionInternal && this.m_DrawStart <= this.caretSelectPositionInternal && this.m_DrawEnd >= this.caretPositionInternal && this.m_DrawEnd >= this.caretSelectPositionInternal;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000AF0B File Offset: 0x0000910B
		private static int GetLineStartPosition(TextGenerator gen, int line)
		{
			line = Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000AF34 File Offset: 0x00009134
		private static int GetLineEndPosition(TextGenerator gen, int line)
		{
			line = Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx - 1;
			}
			return gen.characterCountVisible;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000AF6C File Offset: 0x0000916C
		private void SetDrawRangeToContainCaretPosition(int caretPos)
		{
			if (this.cachedInputTextGenerator.lineCount <= 0)
			{
				return;
			}
			Vector2 size = this.cachedInputTextGenerator.rectExtents.size;
			if (!this.multiLine)
			{
				IList<UICharInfo> characters = this.cachedInputTextGenerator.characters;
				if (this.m_DrawEnd > this.cachedInputTextGenerator.characterCountVisible)
				{
					this.m_DrawEnd = this.cachedInputTextGenerator.characterCountVisible;
				}
				float num = 0f;
				if (caretPos > this.m_DrawEnd || (caretPos == this.m_DrawEnd && this.m_DrawStart > 0))
				{
					this.m_DrawEnd = caretPos;
					this.m_DrawStart = this.m_DrawEnd - 1;
					while (this.m_DrawStart >= 0 && num + characters[this.m_DrawStart].charWidth <= size.x)
					{
						num += characters[this.m_DrawStart].charWidth;
						this.m_DrawStart--;
					}
					this.m_DrawStart++;
				}
				else
				{
					if (caretPos < this.m_DrawStart)
					{
						this.m_DrawStart = caretPos;
					}
					this.m_DrawEnd = this.m_DrawStart;
				}
				while (this.m_DrawEnd < this.cachedInputTextGenerator.characterCountVisible)
				{
					num += characters[this.m_DrawEnd].charWidth;
					if (num > size.x)
					{
						break;
					}
					this.m_DrawEnd++;
				}
				return;
			}
			IList<UILineInfo> lines = this.cachedInputTextGenerator.lines;
			int num2 = this.DetermineCharacterLine(caretPos, this.cachedInputTextGenerator);
			if (caretPos > this.m_DrawEnd)
			{
				this.m_DrawEnd = InputField.GetLineEndPosition(this.cachedInputTextGenerator, num2);
				float num3 = lines[num2].topY - (float)lines[num2].height;
				if (num2 == lines.Count - 1)
				{
					num3 += lines[num2].leading;
				}
				int num4 = num2;
				while (num4 > 0 && lines[num4 - 1].topY - num3 <= size.y)
				{
					num4--;
				}
				this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, num4);
				return;
			}
			if (caretPos < this.m_DrawStart)
			{
				this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, num2);
			}
			int i = this.DetermineCharacterLine(this.m_DrawStart, this.cachedInputTextGenerator);
			int j = i;
			float num5 = lines[i].topY;
			float num6 = lines[j].topY - (float)lines[j].height;
			if (j == lines.Count - 1)
			{
				num6 += lines[j].leading;
			}
			while (j < lines.Count - 1)
			{
				num6 = lines[j + 1].topY - (float)lines[j + 1].height;
				if (j + 1 == lines.Count - 1)
				{
					num6 += lines[j + 1].leading;
				}
				if (num5 - num6 > size.y)
				{
					break;
				}
				j++;
			}
			this.m_DrawEnd = InputField.GetLineEndPosition(this.cachedInputTextGenerator, j);
			while (i > 0)
			{
				num5 = lines[i - 1].topY;
				if (num5 - num6 > size.y)
				{
					break;
				}
				i--;
			}
			this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, i);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000B2AF File Offset: 0x000094AF
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B2B7 File Offset: 0x000094B7
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B2BF File Offset: 0x000094BF
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B2CC File Offset: 0x000094CC
		private void UpdateGeometry()
		{
			if (!this.shouldHideMobileInput)
			{
				return;
			}
			if (this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject(base.transform.name + " Input Caret", new Type[]
				{
					typeof(RectTransform),
					typeof(CanvasRenderer)
				});
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000B3F4 File Offset: 0x000095F4
		private void AssignPositioningIfNeeded()
		{
			if (this.m_TextComponent != null && this.caretRectTrans != null && (this.caretRectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition || this.caretRectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation || this.caretRectTrans.localScale != this.m_TextComponent.rectTransform.localScale || this.caretRectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin || this.caretRectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax || this.caretRectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition || this.caretRectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta || this.caretRectTrans.pivot != this.m_TextComponent.rectTransform.pivot))
			{
				this.caretRectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
				this.caretRectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
				this.caretRectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
				this.caretRectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
				this.caretRectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
				this.caretRectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
				this.caretRectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
				this.caretRectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B61C File Offset: 0x0000981C
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				if (!this.isFocused)
				{
					vertexHelper.FillMesh(vbo);
				}
				else
				{
					Vector2 vector = this.m_TextComponent.PixelAdjustPoint(Vector2.zero);
					if (!this.hasSelection)
					{
						this.GenerateCaret(vertexHelper, vector);
					}
					else
					{
						this.GenerateHighlight(vertexHelper, vector);
					}
					vertexHelper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B690 File Offset: 0x00009890
		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			float num = (float)this.m_CaretWidth;
			int num2 = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator == null)
			{
				return;
			}
			if (cachedTextGenerator.lineCount == 0)
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (num2 < cachedTextGenerator.characters.Count)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num2];
				zero.x = uicharInfo.cursorPos.x;
			}
			zero.x /= this.m_TextComponent.pixelsPerUnit;
			if (zero.x > this.m_TextComponent.rectTransform.rect.xMax)
			{
				zero.x = this.m_TextComponent.rectTransform.rect.xMax;
			}
			int num3 = this.DetermineCharacterLine(num2, cachedTextGenerator);
			zero.y = cachedTextGenerator.lines[num3].topY / this.m_TextComponent.pixelsPerUnit;
			float num4 = (float)cachedTextGenerator.lines[num3].height / this.m_TextComponent.pixelsPerUnit;
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i].color = this.caretColor;
			}
			this.m_CursorVerts[0].position = new Vector3(zero.x, zero.y - num4, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x + num, zero.y - num4, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num, zero.y, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x, zero.y, 0f);
			if (roundingOffset != Vector2.zero)
			{
				for (int j = 0; j < this.m_CursorVerts.Length; j++)
				{
					UIVertex uivertex = this.m_CursorVerts[j];
					uivertex.position.x = uivertex.position.x + roundingOffset.x;
					uivertex.position.y = uivertex.position.y + roundingOffset.y;
				}
			}
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			int num5 = Screen.height;
			int targetDisplay = this.m_TextComponent.canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				num5 = Display.displays[targetDisplay].renderingHeight;
			}
			Camera camera;
			if (this.m_TextComponent.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				camera = null;
			}
			else
			{
				camera = this.m_TextComponent.canvas.worldCamera;
			}
			Vector3 vector = this.m_CachedInputRenderer.gameObject.transform.TransformPoint(this.m_CursorVerts[0].position);
			Vector2 vector2 = RectTransformUtility.WorldToScreenPoint(camera, vector);
			vector2.y = (float)num5 - vector2.y;
			if (this.input != null)
			{
				this.input.compositionCursorPos = vector2;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B9CC File Offset: 0x00009BCC
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BA20 File Offset: 0x00009C20
		private void GenerateHighlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			int num = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			int num2 = Mathf.Max(0, this.caretSelectPositionInternal - this.m_DrawStart);
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			num2--;
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount <= 0)
			{
				return;
			}
			int num4 = this.DetermineCharacterLine(num, cachedTextGenerator);
			int num5 = InputField.GetLineEndPosition(cachedTextGenerator, num4);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num6 = num;
			while (num6 <= num2 && num6 < cachedTextGenerator.characterCount)
			{
				if (num6 == num5 || num6 == num2)
				{
					UICharInfo uicharInfo = cachedTextGenerator.characters[num];
					UICharInfo uicharInfo2 = cachedTextGenerator.characters[num6];
					Vector2 vector = new Vector2(uicharInfo.cursorPos.x / this.m_TextComponent.pixelsPerUnit, cachedTextGenerator.lines[num4].topY / this.m_TextComponent.pixelsPerUnit);
					Vector2 vector2 = new Vector2((uicharInfo2.cursorPos.x + uicharInfo2.charWidth) / this.m_TextComponent.pixelsPerUnit, vector.y - (float)cachedTextGenerator.lines[num4].height / this.m_TextComponent.pixelsPerUnit);
					if (vector2.x > this.m_TextComponent.rectTransform.rect.xMax || vector2.x < this.m_TextComponent.rectTransform.rect.xMin)
					{
						vector2.x = this.m_TextComponent.rectTransform.rect.xMax;
					}
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new Vector3(vector.x, vector2.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector.x, vector.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num = num6 + 1;
					num4++;
					num5 = InputField.GetLineEndPosition(cachedTextGenerator, num4);
				}
				num6++;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == InputField.CharacterValidation.Integer || this.characterValidation == InputField.CharacterValidation.Decimal)
			{
				int num = ((pos == 0 && text.Length > 0 && text[0] == '-') ? 1 : 0);
				bool flag = text.Length > 0 && text[0] == '-' && ((this.caretPositionInternal == 0 && this.caretSelectPositionInternal > 0) || (this.caretSelectPositionInternal == 0 && this.caretPositionInternal > 0));
				bool flag2 = this.caretPositionInternal == 0 || this.caretSelectPositionInternal == 0;
				if (num == 0 || flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag2))
					{
						return ch;
					}
					if ((ch == '.' || ch == ',') && this.characterValidation == InputField.CharacterValidation.Decimal && text.IndexOfAny(new char[] { '.', ',' }) == -1)
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Name)
			{
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && (pos == 0 || text[pos - 1] == ' '))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && text[pos - 1] != ' ' && text[pos - 1] != '\'')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				else
				{
					if (ch == '\'' && !text.Contains("'") && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'')))
					{
						return ch;
					}
					if (ch == ' ' && pos != 0 && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'')))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					int num2 = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num2 != 46 && c != '.')
					{
						return ch;
					}
				}
			}
			return '\0';
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_Keyboard != null && !this.m_Keyboard.active)
			{
				this.m_Keyboard.active = true;
				this.m_Keyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C050 File Offset: 0x0000A250
		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			if (TouchScreenKeyboard.isSupported)
			{
				if (this.input != null && this.input.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				this.m_Keyboard = ((this.inputType == InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, false, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == InputField.InputType.AutoCorrect, this.multiLine, false, false, "", this.characterLimit));
				this.m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
				if (!this.m_TouchKeyboardAllowsInPlaceEditing)
				{
					this.MoveTextEnd(false);
				}
			}
			if (!TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing)
			{
				if (this.input != null)
				{
					this.input.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C194 File Offset: 0x0000A394
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			if (this.shouldActivateOnSelect)
			{
				this.ActivateInputField();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C1AB File Offset: 0x0000A3AB
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		public void DeactivateInputField()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_HasDoneFocusTransition = false;
			this.m_AllowInput = false;
			if (this.m_Placeholder != null)
			{
				this.m_Placeholder.enabled = string.IsNullOrEmpty(this.m_Text);
			}
			if (this.m_TextComponent != null && this.IsInteractable())
			{
				if (this.m_WasCanceled)
				{
					this.text = this.m_OriginalText;
				}
				this.SendOnSubmit();
				if (this.m_Keyboard != null)
				{
					this.m_Keyboard.active = false;
					this.m_Keyboard = null;
				}
				this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
				if (this.input != null)
				{
					this.input.imeCompositionMode = IMECompositionMode.Auto;
				}
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C27F File Offset: 0x0000A47F
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField();
			base.OnDeselect(eventData);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C28E File Offset: 0x0000A48E
		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case InputField.ContentType.Standard:
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.Autocorrected:
				this.m_InputType = InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.IntegerNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				break;
			case InputField.ContentType.DecimalNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = InputField.CharacterValidation.Decimal;
				break;
			case InputField.ContentType.Alphanumeric:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = InputField.CharacterValidation.Alphanumeric;
				break;
			case InputField.ContentType.Name:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NamePhonePad;
				this.m_CharacterValidation = InputField.CharacterValidation.Name;
				break;
			case InputField.ContentType.EmailAddress:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = InputField.CharacterValidation.EmailAddress;
				break;
			case InputField.ContentType.Password:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.Pin:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				break;
			}
			this.EnforceTextHOverflow();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C403 File Offset: 0x0000A603
		private void EnforceTextHOverflow()
		{
			if (this.m_TextComponent != null)
			{
				if (this.multiLine)
				{
					this.m_TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
					return;
				}
				this.m_TextComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C434 File Offset: 0x0000A634
		private void SetToCustomIfContentTypeIsNot(params InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (this.contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C46E File Offset: 0x0000A66E
		private void SetToCustom()
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C483 File Offset: 0x0000A683
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Selected;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008BDA File Offset: 0x00006DDA
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				TextGenerationSettings generationSettings = this.textComponent.GetGenerationSettings(Vector2.zero);
				return this.textComponent.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.textComponent.pixelsPerUnit;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008BDA File Offset: 0x00006DDA
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000C500 File Offset: 0x0000A700
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				TextGenerationSettings generationSettings = this.textComponent.GetGenerationSettings(new Vector2(this.textComponent.rectTransform.rect.size.x, 0f));
				return this.textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.textComponent.pixelsPerUnit;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000C577 File Offset: 0x0000A777
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005DE4 File Offset: 0x00003FE4
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000081 RID: 129
		protected TouchScreenKeyboard m_Keyboard;

		// Token: 0x04000082 RID: 130
		private static readonly char[] kSeparators = new char[] { ' ', '.', ',', '\t', '\r', '\n' };

		// Token: 0x04000083 RID: 131
		[SerializeField]
		[FormerlySerializedAs("text")]
		protected Text m_TextComponent;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x04000085 RID: 133
		[SerializeField]
		private InputField.ContentType m_ContentType;

		// Token: 0x04000086 RID: 134
		[FormerlySerializedAs("inputType")]
		[SerializeField]
		private InputField.InputType m_InputType;

		// Token: 0x04000087 RID: 135
		[FormerlySerializedAs("asteriskChar")]
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x04000088 RID: 136
		[FormerlySerializedAs("keyboardType")]
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		private InputField.LineType m_LineType;

		// Token: 0x0400008A RID: 138
		[FormerlySerializedAs("hideMobileInput")]
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x0400008B RID: 139
		[FormerlySerializedAs("validation")]
		[SerializeField]
		private InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x0400008C RID: 140
		[FormerlySerializedAs("characterLimit")]
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x0400008D RID: 141
		[FormerlySerializedAs("onSubmit")]
		[FormerlySerializedAs("m_OnSubmit")]
		[FormerlySerializedAs("m_EndEdit")]
		[SerializeField]
		private InputField.SubmitEvent m_OnEndEdit = new InputField.SubmitEvent();

		// Token: 0x0400008E RID: 142
		[FormerlySerializedAs("onValueChange")]
		[FormerlySerializedAs("m_OnValueChange")]
		[SerializeField]
		private InputField.OnChangeEvent m_OnValueChanged = new InputField.OnChangeEvent();

		// Token: 0x0400008F RID: 143
		[FormerlySerializedAs("onValidateInput")]
		[SerializeField]
		private InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x04000090 RID: 144
		[FormerlySerializedAs("selectionColor")]
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x04000093 RID: 147
		[SerializeField]
		[FormerlySerializedAs("mValue")]
		protected string m_Text = string.Empty;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x04000095 RID: 149
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x04000096 RID: 150
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x04000097 RID: 151
		protected int m_CaretPosition;

		// Token: 0x04000098 RID: 152
		protected int m_CaretSelectPosition;

		// Token: 0x04000099 RID: 153
		private RectTransform caretRectTrans;

		// Token: 0x0400009A RID: 154
		protected UIVertex[] m_CursorVerts;

		// Token: 0x0400009B RID: 155
		private TextGenerator m_InputTextCache;

		// Token: 0x0400009C RID: 156
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x0400009D RID: 157
		private bool m_PreventFontCallback;

		// Token: 0x0400009E RID: 158
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x0400009F RID: 159
		private bool m_AllowInput;

		// Token: 0x040000A0 RID: 160
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x040000A1 RID: 161
		private bool m_UpdateDrag;

		// Token: 0x040000A2 RID: 162
		private bool m_DragPositionOutOfBounds;

		// Token: 0x040000A3 RID: 163
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x040000A4 RID: 164
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x040000A5 RID: 165
		protected bool m_CaretVisible;

		// Token: 0x040000A6 RID: 166
		private Coroutine m_BlinkCoroutine;

		// Token: 0x040000A7 RID: 167
		private float m_BlinkStartTime;

		// Token: 0x040000A8 RID: 168
		protected int m_DrawStart;

		// Token: 0x040000A9 RID: 169
		protected int m_DrawEnd;

		// Token: 0x040000AA RID: 170
		private Coroutine m_DragCoroutine;

		// Token: 0x040000AB RID: 171
		private string m_OriginalText = "";

		// Token: 0x040000AC RID: 172
		private bool m_WasCanceled;

		// Token: 0x040000AD RID: 173
		private bool m_HasDoneFocusTransition;

		// Token: 0x040000AE RID: 174
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x040000AF RID: 175
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x040000B0 RID: 176
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x040000B1 RID: 177
		private Event m_ProcessingEvent = new Event();

		// Token: 0x040000B2 RID: 178
		private const int k_MaxTextLength = 16382;

		// Token: 0x02000088 RID: 136
		public enum ContentType
		{
			// Token: 0x04000264 RID: 612
			Standard,
			// Token: 0x04000265 RID: 613
			Autocorrected,
			// Token: 0x04000266 RID: 614
			IntegerNumber,
			// Token: 0x04000267 RID: 615
			DecimalNumber,
			// Token: 0x04000268 RID: 616
			Alphanumeric,
			// Token: 0x04000269 RID: 617
			Name,
			// Token: 0x0400026A RID: 618
			EmailAddress,
			// Token: 0x0400026B RID: 619
			Password,
			// Token: 0x0400026C RID: 620
			Pin,
			// Token: 0x0400026D RID: 621
			Custom
		}

		// Token: 0x02000089 RID: 137
		public enum InputType
		{
			// Token: 0x0400026F RID: 623
			Standard,
			// Token: 0x04000270 RID: 624
			AutoCorrect,
			// Token: 0x04000271 RID: 625
			Password
		}

		// Token: 0x0200008A RID: 138
		public enum CharacterValidation
		{
			// Token: 0x04000273 RID: 627
			None,
			// Token: 0x04000274 RID: 628
			Integer,
			// Token: 0x04000275 RID: 629
			Decimal,
			// Token: 0x04000276 RID: 630
			Alphanumeric,
			// Token: 0x04000277 RID: 631
			Name,
			// Token: 0x04000278 RID: 632
			EmailAddress
		}

		// Token: 0x0200008B RID: 139
		public enum LineType
		{
			// Token: 0x0400027A RID: 634
			SingleLine,
			// Token: 0x0400027B RID: 635
			MultiLineSubmit,
			// Token: 0x0400027C RID: 636
			MultiLineNewline
		}

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x06000652 RID: 1618
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x0200008D RID: 141
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200008E RID: 142
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200008F RID: 143
		protected enum EditState
		{
			// Token: 0x0400027E RID: 638
			Continue,
			// Token: 0x0400027F RID: 639
			Finish
		}
	}
}
