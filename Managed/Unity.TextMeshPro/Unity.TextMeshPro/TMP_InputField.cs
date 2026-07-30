using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200002A RID: 42
	[AddComponentMenu("UI/TextMeshPro - Input Field", 11)]
	public class TMP_InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement, IScrollHandler
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00008083 File Offset: 0x00006283
		private BaseInput inputSystem
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000080B3 File Offset: 0x000062B3
		private string compositionString
		{
			get
			{
				if (!(this.inputSystem != null))
				{
					return Input.compositionString;
				}
				return this.inputSystem.compositionString;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000080D4 File Offset: 0x000062D4
		private int compositionLength
		{
			get
			{
				if (this.m_ReadOnly)
				{
					return 0;
				}
				return this.compositionString.Length;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000080EC File Offset: 0x000062EC
		protected TMP_InputField()
		{
			this.SetTextComponentWrapMode();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000821E File Offset: 0x0000641E
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00008240 File Offset: 0x00006440
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000826C File Offset: 0x0000646C
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform != RuntimePlatform.tvOS) || this.m_HideMobileInput;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
				{
					SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
					return;
				}
				this.m_HideMobileInput = true;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000082A4 File Offset: 0x000064A4
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000082DC File Offset: 0x000064DC
		public bool shouldHideSoftKeyboard
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.Android)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
					{
						return true;
					}
				}
				else if (platform - RuntimePlatform.MetroPlayerX86 > 2 && platform != RuntimePlatform.tvOS)
				{
					return true;
				}
				return this.m_HideSoftKeyboard;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.Android)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
					{
						goto IL_0031;
					}
				}
				else if (platform - RuntimePlatform.MetroPlayerX86 > 2 && platform != RuntimePlatform.tvOS)
				{
					goto IL_0031;
				}
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideSoftKeyboard, value);
				goto IL_0038;
				IL_0031:
				this.m_HideSoftKeyboard = true;
				IL_0038:
				if (this.m_HideSoftKeyboard && this.m_SoftKeyboard != null && TouchScreenKeyboard.isSupported && this.m_SoftKeyboard.active)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008358 File Offset: 0x00006558
		private bool isKeyboardUsingEvents()
		{
			RuntimePlatform platform = Application.platform;
			return platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform != RuntimePlatform.tvOS;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000837C File Offset: 0x0000657C
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00008384 File Offset: 0x00006584
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

		// Token: 0x06000157 RID: 343 RVA: 0x0000838E File Offset: 0x0000658E
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008398 File Offset: 0x00006598
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
			this.m_Text = value;
			if (this.m_SoftKeyboard != null)
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
			if (this.m_StringPosition > this.m_Text.Length)
			{
				this.m_StringPosition = (this.m_StringSelectPosition = this.m_Text.Length);
			}
			else if (this.m_StringSelectPosition > this.m_Text.Length)
			{
				this.m_StringSelectPosition = this.m_Text.Length;
			}
			this.AdjustTextPositionRelativeToViewport(0f);
			this.m_forceRectTransformAdjustment = true;
			this.m_IsTextComponentUpdateRequired = true;
			this.UpdateLabel();
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000846B File Offset: 0x0000666B
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008473 File Offset: 0x00006673
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000847B File Offset: 0x0000667B
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008499 File Offset: 0x00006699
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000084A1 File Offset: 0x000066A1
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000084B7 File Offset: 0x000066B7
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000084BF File Offset: 0x000066BF
		public RectTransform textViewport
		{
			get
			{
				return this.m_TextViewport;
			}
			set
			{
				SetPropertyUtility.SetClass<RectTransform>(ref this.m_TextViewport, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000084CE File Offset: 0x000066CE
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000084D6 File Offset: 0x000066D6
		public TMP_Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_Text>(ref this.m_TextComponent, value))
				{
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000084EC File Offset: 0x000066EC
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000084F4 File Offset: 0x000066F4
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008503 File Offset: 0x00006703
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000850C File Offset: 0x0000670C
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				SetPropertyUtility.SetClass<Scrollbar>(ref this.m_VerticalScrollbar, value);
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00008579 File Offset: 0x00006779
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00008581 File Offset: 0x00006781
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_ScrollSensitivity, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008597 File Offset: 0x00006797
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000085B3 File Offset: 0x000067B3
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000085C9 File Offset: 0x000067C9
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000085D1 File Offset: 0x000067D1
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000085E9 File Offset: 0x000067E9
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000085F1 File Offset: 0x000067F1
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008607 File Offset: 0x00006807
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000860F File Offset: 0x0000680F
		public TMP_InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_OnEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnEndEdit, value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000861E File Offset: 0x0000681E
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00008626 File Offset: 0x00006826
		public TMP_InputField.SubmitEvent onSubmit
		{
			get
			{
				return this.m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnSubmit, value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008635 File Offset: 0x00006835
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000863D File Offset: 0x0000683D
		public TMP_InputField.SelectionEvent onSelect
		{
			get
			{
				return this.m_OnSelect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnSelect, value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000864C File Offset: 0x0000684C
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00008654 File Offset: 0x00006854
		public TMP_InputField.SelectionEvent onDeselect
		{
			get
			{
				return this.m_OnDeselect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnDeselect, value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008663 File Offset: 0x00006863
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000866B File Offset: 0x0000686B
		public TMP_InputField.TextSelectionEvent onTextSelection
		{
			get
			{
				return this.m_OnTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnTextSelection, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000867A File Offset: 0x0000687A
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00008682 File Offset: 0x00006882
		public TMP_InputField.TextSelectionEvent onEndTextSelection
		{
			get
			{
				return this.m_OnEndTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnEndTextSelection, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008691 File Offset: 0x00006891
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00008699 File Offset: 0x00006899
		public TMP_InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000086A8 File Offset: 0x000068A8
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000086B0 File Offset: 0x000068B0
		public TMP_InputField.TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
		{
			get
			{
				return this.m_OnTouchScreenKeyboardStatusChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TouchScreenKeyboardEvent>(ref this.m_OnTouchScreenKeyboardStatusChanged, value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000086BF File Offset: 0x000068BF
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000086C7 File Offset: 0x000068C7
		public TMP_InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000086D6 File Offset: 0x000068D6
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000086DE File Offset: 0x000068DE
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
					if (this.m_SoftKeyboard != null)
					{
						this.m_SoftKeyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000870E File Offset: 0x0000690E
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00008716 File Offset: 0x00006916
		public float pointSize
		{
			get
			{
				return this.m_GlobalPointSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_GlobalPointSize, Math.Max(0f, value)))
				{
					this.SetGlobalPointSize(this.m_GlobalPointSize);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008742 File Offset: 0x00006942
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000874A File Offset: 0x0000694A
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_GlobalFontAsset;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_FontAsset>(ref this.m_GlobalFontAsset, value))
				{
					this.SetGlobalFontAsset(this.m_GlobalFontAsset);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000876C File Offset: 0x0000696C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00008774 File Offset: 0x00006974
		public bool onFocusSelectAll
		{
			get
			{
				return this.m_OnFocusSelectAll;
			}
			set
			{
				this.m_OnFocusSelectAll = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000877D File Offset: 0x0000697D
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00008785 File Offset: 0x00006985
		public bool resetOnDeActivation
		{
			get
			{
				return this.m_ResetOnDeActivation;
			}
			set
			{
				this.m_ResetOnDeActivation = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000878E File Offset: 0x0000698E
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00008796 File Offset: 0x00006996
		public bool restoreOriginalTextOnEscape
		{
			get
			{
				return this.m_RestoreOriginalTextOnEscape;
			}
			set
			{
				this.m_RestoreOriginalTextOnEscape = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000879F File Offset: 0x0000699F
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000087A7 File Offset: 0x000069A7
		public bool isRichTextEditingAllowed
		{
			get
			{
				return this.m_isRichTextEditingAllowed;
			}
			set
			{
				this.m_isRichTextEditingAllowed = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000087B0 File Offset: 0x000069B0
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000087B8 File Offset: 0x000069B8
		public TMP_InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000087CE File Offset: 0x000069CE
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000087D6 File Offset: 0x000069D6
		public TMP_InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new TMP_InputField.ContentType[]
					{
						TMP_InputField.ContentType.Standard,
						TMP_InputField.ContentType.Autocorrected
					});
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000087FC File Offset: 0x000069FC
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00008804 File Offset: 0x00006A04
		public int lineLimit
		{
			get
			{
				return this.m_LineLimit;
			}
			set
			{
				if (this.m_LineType == TMP_InputField.LineType.SingleLine)
				{
					this.m_LineLimit = 1;
					return;
				}
				SetPropertyUtility.SetStruct<int>(ref this.m_LineLimit, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008823 File Offset: 0x00006A23
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000882B File Offset: 0x00006A2B
		public TMP_InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008841 File Offset: 0x00006A41
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00008849 File Offset: 0x00006A49
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000885F File Offset: 0x00006A5F
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00008867 File Offset: 0x00006A67
		public TMP_InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000887D File Offset: 0x00006A7D
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00008885 File Offset: 0x00006A85
		public TMP_InputValidator inputValidator
		{
			get
			{
				return this.m_InputValidator;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_InputValidator>(ref this.m_InputValidator, value))
				{
					this.SetToCustom(TMP_InputField.CharacterValidation.CustomValidator);
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000889C File Offset: 0x00006A9C
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000088A4 File Offset: 0x00006AA4
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000088AD File Offset: 0x00006AAD
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000088B5 File Offset: 0x00006AB5
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
				this.SetTextComponentRichTextMode();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000088C4 File Offset: 0x00006AC4
		public bool multiLine
		{
			get
			{
				return this.m_LineType == TMP_InputField.LineType.MultiLineNewline || this.lineType == TMP_InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000088DA File Offset: 0x00006ADA
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000088E2 File Offset: 0x00006AE2
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000088F8 File Offset: 0x00006AF8
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008900 File Offset: 0x00006B00
		protected void ClampStringPos(ref int pos)
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

		// Token: 0x060001A5 RID: 421 RVA: 0x00008927 File Offset: 0x00006B27
		protected void ClampCaretPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.m_TextComponent.textInfo.characterCount - 1)
			{
				pos = this.m_TextComponent.textInfo.characterCount - 1;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000895C File Offset: 0x00006B5C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000896B File Offset: 0x00006B6B
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampCaretPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008980 File Offset: 0x00006B80
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000898F File Offset: 0x00006B8F
		protected int stringPositionInternal
		{
			get
			{
				return this.m_StringPosition + this.compositionLength;
			}
			set
			{
				this.m_StringPosition = value;
				this.ClampStringPos(ref this.m_StringPosition);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000089A4 File Offset: 0x00006BA4
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000089B3 File Offset: 0x00006BB3
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampCaretPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000089C8 File Offset: 0x00006BC8
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000089D7 File Offset: 0x00006BD7
		protected int stringSelectPositionInternal
		{
			get
			{
				return this.m_StringSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_StringSelectPosition = value;
				this.ClampStringPos(ref this.m_StringSelectPosition);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000089EC File Offset: 0x00006BEC
		private bool hasSelection
		{
			get
			{
				return this.stringPositionInternal != this.stringSelectPositionInternal;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000089FF File Offset: 0x00006BFF
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008A07 File Offset: 0x00006C07
		public int caretPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008A1E File Offset: 0x00006C1E
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008A26 File Offset: 0x00006C26
		public int selectionAnchorPosition
		{
			get
			{
				return this.caretPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000089FF File Offset: 0x00006BFF
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00008A3F File Offset: 0x00006C3F
		public int selectionFocusPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretSelectPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008A58 File Offset: 0x00006C58
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00008A60 File Offset: 0x00006C60
		public int stringPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				this.selectionStringAnchorPosition = value;
				this.selectionStringFocusPosition = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008A77 File Offset: 0x00006C77
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00008A7F File Offset: 0x00006C7F
		public int selectionStringAnchorPosition
		{
			get
			{
				return this.stringPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008A58 File Offset: 0x00006C58
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00008A98 File Offset: 0x00006C98
		public int selectionStringFocusPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringSelectPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008AB4 File Offset: 0x00006CB4
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			if (base.GetComponent<ILayoutController>() != null)
			{
				this.m_IsDrivenByLayoutComponents = true;
				this.m_LayoutGroup = base.GetComponent<LayoutGroup>();
			}
			else
			{
				this.m_IsDrivenByLayoutComponents = false;
			}
			if (Application.isPlaying && this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject("Caret", new Type[] { typeof(TMP_SelectionCaret) });
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			this.m_RectTransform = base.GetComponent<RectTransform>();
			if (this.m_TextViewport != null)
			{
				this.m_TextViewportRectMask = this.m_TextViewport.GetComponent<RectMask2D>();
				this.UpdateMaskRegions();
			}
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				this.UpdateLabel();
			}
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<global::UnityEngine.Object>(this.ON_TEXT_CHANGED));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008C9C File Offset: 0x00006E9C
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField(false);
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<global::UnityEngine.Object>(this.ON_TEXT_CHANGED));
			base.OnDisable();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008D78 File Offset: 0x00006F78
		private void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			if (obj == this.m_TextComponent && Application.isPlaying && this.compositionLength == 0)
			{
				this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008DC6 File Offset: 0x00006FC6
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while ((this.isFocused || this.m_SelectionStillActive) && this.m_CaretBlinkRate > 0f)
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

		// Token: 0x060001BF RID: 447 RVA: 0x00008DD5 File Offset: 0x00006FD5
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

		// Token: 0x060001C0 RID: 448 RVA: 0x00008DF8 File Offset: 0x00006FF8
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

		// Token: 0x060001C1 RID: 449 RVA: 0x00008E32 File Offset: 0x00007032
		protected void OnFocus()
		{
			if (this.m_OnFocusSelectAll)
			{
				this.SelectAll();
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008E42 File Offset: 0x00007042
		protected void SelectAll()
		{
			this.m_isSelectAll = true;
			this.stringPositionInternal = this.text.Length;
			this.stringSelectPositionInternal = 0;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008E64 File Offset: 0x00007064
		public void MoveTextEnd(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int length = this.text.Length;
				if (shift)
				{
					this.stringSelectPositionInternal = length;
				}
				else
				{
					this.stringPositionInternal = length;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int num = this.m_TextComponent.textInfo.characterCount - 1;
				if (shift)
				{
					this.caretSelectPositionInternal = num;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(num);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = num);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(num));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008F00 File Offset: 0x00007100
		public void MoveTextStart(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int num = 0;
				if (shift)
				{
					this.stringSelectPositionInternal = num;
				}
				else
				{
					this.stringPositionInternal = num;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int num2 = 0;
				if (shift)
				{
					this.caretSelectPositionInternal = num2;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(num2);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = num2);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(num2));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008F80 File Offset: 0x00007180
		public void MoveToEndOfLine(bool shift, bool ctrl)
		{
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int num = (ctrl ? (this.m_TextComponent.textInfo.characterCount - 1) : this.m_TextComponent.textInfo.lineInfo[lineNumber].lastCharacterIndex);
			int index = this.m_TextComponent.textInfo.characterInfo[num].index;
			if (shift)
			{
				this.stringSelectPositionInternal = index;
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.stringPositionInternal = index;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			}
			this.UpdateLabel();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000903C File Offset: 0x0000723C
		public void MoveToStartOfLine(bool shift, bool ctrl)
		{
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int num = (ctrl ? 0 : this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex);
			int num2 = 0;
			if (num > 0)
			{
				num2 = this.m_TextComponent.textInfo.characterInfo[num - 1].index + this.m_TextComponent.textInfo.characterInfo[num - 1].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num2;
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.stringPositionInternal = num2;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			}
			this.UpdateLabel();
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000910A File Offset: 0x0000730A
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00009111 File Offset: 0x00007311
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

		// Token: 0x060001C9 RID: 457 RVA: 0x0000911C File Offset: 0x0000731C
		private bool InPlaceEditing()
		{
			return this.m_TouchKeyboardAllowsInPlaceEditing || (TouchScreenKeyboard.isSupported && (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)) || (TouchScreenKeyboard.isSupported && this.shouldHideSoftKeyboard) || !TouchScreenKeyboard.isSupported || this.shouldHideSoftKeyboard || this.shouldHideMobileInput;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009180 File Offset: 0x00007380
		private void UpdateStringPositionFromKeyboard()
		{
			RangeInt selection = this.m_SoftKeyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (this.stringPositionInternal != start)
			{
				flag = true;
				this.stringPositionInternal = start;
				this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			}
			if (this.stringSelectPositionInternal != end)
			{
				this.stringSelectPositionInternal = end;
				flag = true;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			}
			if (flag)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009208 File Offset: 0x00007408
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
			if (this.m_IsScrollbarUpdateRequired)
			{
				this.UpdateScrollbar();
				this.m_IsScrollbarUpdateRequired = false;
			}
			if (!this.isFocused && this.m_SelectionStillActive)
			{
				GameObject gameObject = ((EventSystem.current != null) ? EventSystem.current.currentSelectedGameObject : null);
				if (gameObject != null && gameObject != base.gameObject)
				{
					if (gameObject != this.m_SelectedObject)
					{
						this.m_SelectedObject = gameObject;
						if (gameObject.GetComponent<TMP_InputField>() != null)
						{
							this.m_SelectionStillActive = false;
							this.MarkGeometryAsDirty();
							this.m_SelectedObject = null;
						}
					}
					return;
				}
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					bool flag = false;
					float unscaledTime = Time.unscaledTime;
					if (this.m_KeyDownStartTime + this.m_DoubleClickDelay > unscaledTime)
					{
						flag = true;
					}
					this.m_KeyDownStartTime = unscaledTime;
					if (flag)
					{
						this.m_SelectionStillActive = false;
						this.MarkGeometryAsDirty();
						return;
					}
				}
			}
			this.UpdateMaskRegions();
			if ((this.InPlaceEditing() && this.isKeyboardUsingEvents()) || !this.isFocused)
			{
				return;
			}
			this.AssignPositioningIfNeeded();
			if (this.m_SoftKeyboard == null || this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_SoftKeyboard.text;
					}
					if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
					{
						this.SendTouchScreenKeyboardStatusChanged();
					}
					if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
					{
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						this.SendTouchScreenKeyboardStatusChanged();
					}
					if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Done)
					{
						this.m_ReleaseSelection = true;
						this.OnSubmit(null);
						this.SendTouchScreenKeyboardStatusChanged();
					}
				}
				this.OnDeselect(null);
				return;
			}
			string text = this.m_SoftKeyboard.text;
			if (this.m_Text != text)
			{
				if (this.m_ReadOnly)
				{
					this.m_SoftKeyboard.text = this.m_Text;
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
						else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
						{
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
						}
						if (this.lineType == TMP_InputField.LineType.MultiLineSubmit && c == '\n')
						{
							this.m_SoftKeyboard.text = this.m_Text;
							this.OnSubmit(null);
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
					this.UpdateStringPositionFromKeyboard();
					if (this.m_Text != text)
					{
						this.m_SoftKeyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && Application.platform == RuntimePlatform.Android)
			{
				this.UpdateStringPositionFromKeyboard();
			}
			if (this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009580 File Offset: 0x00007780
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.m_SoftKeyboard == null || this.shouldHideSoftKeyboard || this.shouldHideMobileInput);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000095CD File Offset: 0x000077CD
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000095E0 File Offset: 0x000077E0
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			CaretPosition caretPosition;
			int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out caretPosition);
			if (this.m_isRichTextEditingAllowed)
			{
				if (caretPosition == CaretPosition.Left)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
				}
				else if (caretPosition == CaretPosition.Right)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
				}
			}
			else if (caretPosition == CaretPosition.Left)
			{
				this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
			}
			else if (caretPosition == CaretPosition.Right)
			{
				this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
			}
			this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textViewport, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000979A File Offset: 0x0000799A
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textViewport, eventData.position, eventData.pressEventCamera, out vector);
				Rect rect = this.textViewport.rect;
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

		// Token: 0x060001D0 RID: 464 RVA: 0x000097B0 File Offset: 0x000079B0
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000097C4 File Offset: 0x000079C4
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_SoftKeyboard == null || !this.m_SoftKeyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			bool flag = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			bool flag2 = false;
			float unscaledTime = Time.unscaledTime;
			if (this.m_PointerDownClickStartTime + this.m_DoubleClickDelay > unscaledTime)
			{
				flag2 = true;
			}
			this.m_PointerDownClickStartTime = unscaledTime;
			if (allowInput || !this.m_OnFocusSelectAll)
			{
				CaretPosition caretPosition;
				int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out caretPosition);
				if (flag)
				{
					if (this.m_isRichTextEditingAllowed)
					{
						if (caretPosition == CaretPosition.Left)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						}
						else if (caretPosition == CaretPosition.Right)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
						}
					}
					else if (caretPosition == CaretPosition.Left)
					{
						this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
					}
					else if (caretPosition == CaretPosition.Right)
					{
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else if (this.m_isRichTextEditingAllowed)
				{
					if (caretPosition == CaretPosition.Left)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index);
					}
					else if (caretPosition == CaretPosition.Right)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
					}
				}
				else if (caretPosition == CaretPosition.Left)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength)));
				}
				else if (caretPosition == CaretPosition.Right)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
				}
				if (flag2)
				{
					int num = TMP_TextUtilities.FindIntersectingWord(this.m_TextComponent, eventData.position, eventData.pressEventCamera);
					if (num != -1)
					{
						this.caretPositionInternal = this.m_TextComponent.textInfo.wordInfo[num].firstCharacterIndex;
						this.caretSelectPositionInternal = this.m_TextComponent.textInfo.wordInfo[num].lastCharacterIndex + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].stringLength;
					}
					else
					{
						this.caretPositionInternal = cursorIndexFromPosition;
						this.caretSelectPositionInternal = this.caretPositionInternal + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						this.stringSelectPositionInternal = this.stringPositionInternal + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal));
				}
				this.m_isSelectAll = false;
			}
			this.UpdateLabel();
			eventData.Use();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009CA4 File Offset: 0x00007EA4
		protected TMP_InputField.EditState KeyPressed(Event evt)
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
						return TMP_InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_01EB;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						return TMP_InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_01EB;
					}
					if (flag4)
					{
						this.SelectAll();
						return TMP_InputField.EditState.Continue;
					}
					goto IL_01EB;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_01EB;
					}
					if (flag4)
					{
						this.Append(TMP_InputField.clipboard);
						return TMP_InputField.EditState.Continue;
					}
					goto IL_01EB;
				}
				else
				{
					if (flag4)
					{
						if (this.inputType != TMP_InputField.InputType.Password)
						{
							TMP_InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							TMP_InputField.clipboard = "";
						}
						return TMP_InputField.EditState.Continue;
					}
					goto IL_01EB;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.DeleteKey();
					return TMP_InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.Insert:
					goto IL_01EB;
				case KeyCode.UpArrow:
					this.MoveUp(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.Home:
					this.MoveToStartOfLine(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveToEndOfLine(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageUp:
					this.MovePageUp(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageDown:
					this.MovePageDown(flag2);
					return TMP_InputField.EditState.Continue;
				default:
					goto IL_01EB;
				}
			}
			else
			{
				if (flag4)
				{
					if (this.inputType != TMP_InputField.InputType.Password)
					{
						TMP_InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						TMP_InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return TMP_InputField.EditState.Continue;
				}
				goto IL_01EB;
			}
			if (this.lineType != TMP_InputField.LineType.MultiLineNewline)
			{
				this.m_ReleaseSelection = true;
				return TMP_InputField.EditState.Finish;
			}
			IL_01EB:
			char c = evt.character;
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return TMP_InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (flag2 && c == '\n')
			{
				c = '\v';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionLength > 0)
			{
				this.UpdateLabel();
			}
			return TMP_InputField.EditState.Continue;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009F02 File Offset: 0x00008102
		protected virtual bool IsValidChar(char c)
		{
			if (c == '\0')
			{
				return false;
			}
			if (c == '\u007f')
			{
				return false;
			}
			if (c != '\t')
			{
			}
			return true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009F1B File Offset: 0x0000811B
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009F28 File Offset: 0x00008128
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				EventType rawType = this.m_ProcessingEvent.rawType;
				if (rawType != EventType.KeyDown)
				{
					if (rawType != EventType.KeyUp)
					{
						if (rawType - EventType.ValidateCommand <= 1)
						{
							string commandName = this.m_ProcessingEvent.commandName;
							if (commandName == "SelectAll")
							{
								this.SelectAll();
								flag = true;
							}
						}
					}
				}
				else
				{
					flag = true;
					if (!this.m_IsCompositionActive || this.compositionLength != 0 || this.m_ProcessingEvent.character != '\0' || this.m_ProcessingEvent.modifiers != EventModifiers.None)
					{
						if (this.KeyPressed(this.m_ProcessingEvent) == TMP_InputField.EditState.Finish)
						{
							this.SendOnSubmit();
							this.DeactivateInputField(false);
						}
						else
						{
							this.m_IsTextComponentUpdateRequired = true;
							this.UpdateLabel();
						}
					}
				}
			}
			if (flag)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A000 File Offset: 0x00008200
		public virtual void OnScroll(PointerEventData eventData)
		{
			if (this.m_TextComponent.preferredHeight < this.m_TextViewport.rect.height)
			{
				return;
			}
			float num = -eventData.scrollDelta.y;
			this.m_ScrollPosition += 1f / (float)this.m_TextComponent.textInfo.lineCount * num * this.m_ScrollSensitivity;
			this.m_ScrollPosition = Mathf.Clamp01(this.m_ScrollPosition);
			this.AdjustTextPositionRelativeToViewport(this.m_ScrollPosition);
			this.m_AllowInput = false;
			if (this.m_VerticalScrollbar)
			{
				this.m_IsUpdatingScrollbarValues = true;
				this.m_VerticalScrollbar.value = this.m_ScrollPosition;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A0B4 File Offset: 0x000082B4
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int num = this.stringPositionInternal;
			int num2 = this.stringSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return this.text.Substring(num, num2 - num);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A0F4 File Offset: 0x000082F4
		private int FindNextWordBegin()
		{
			if (this.stringSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal + 1);
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

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A154 File Offset: 0x00008354
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Max(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindNextWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal < this.text.Length && char.IsHighSurrogate(this.text[this.stringSelectPositionInternal]))
				{
					num = this.stringSelectPositionInternal + 2;
				}
				else
				{
					num = this.stringSelectPositionInternal + 1;
				}
			}
			else
			{
				num = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = num);
			if (this.stringPositionInternal >= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A2D4 File Offset: 0x000084D4
		private int FindPrevWordBegin()
		{
			if (this.stringSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal - 2);
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

		// Token: 0x060001DB RID: 475 RVA: 0x0000A314 File Offset: 0x00008514
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindPrevWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal > 0 && char.IsLowSurrogate(this.text[this.stringSelectPositionInternal - 1]))
				{
					num = this.stringSelectPositionInternal - 2;
				}
				else
				{
					num = this.stringSelectPositionInternal - 1;
				}
			}
			else
			{
				num = ((this.caretSelectPositionInternal < 2) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 2].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 2].stringLength));
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = num);
			if (this.caretPositionInternal > 0 && this.stringPositionInternal <= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A4A0 File Offset: 0x000086A0
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				int num = this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[lineNumber - 1].firstCharacterIndex;
				while (i < num)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return num;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A5E0 File Offset: 0x000087E0
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
				int num = -1;
				float num2 = 32767f;
				float num3 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[lineNumber + 1].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num4 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num5 = num4 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num5 >= 0f && num5 <= 1f)
					{
						if (num5 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num4 = Mathf.Abs(num4);
						if (num4 < num2)
						{
							num = i;
							num2 = num4;
							num3 = num5;
						}
						i++;
					}
				}
				if (num == -1)
				{
					return lastCharacterIndex;
				}
				if (num3 < 0.5f)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A74C File Offset: 0x0000894C
		private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				float height = this.m_TextViewport.rect.height;
				int num = lineNumber - 1;
				while (num > 0 && this.m_TextComponent.textInfo.lineInfo[num].baseline <= this.m_TextComponent.textInfo.lineInfo[lineNumber].baseline + height)
				{
					num--;
				}
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return lastCharacterIndex;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A8EC File Offset: 0x00008AEC
		private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				float height = this.m_TextViewport.rect.height;
				int num = lineNumber + 1;
				while (num < this.m_TextComponent.textInfo.lineCount - 1 && this.m_TextComponent.textInfo.lineInfo[num].baseline >= this.m_TextComponent.textInfo.lineInfo[lineNumber].baseline - height)
				{
					num++;
				}
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return lastCharacterIndex;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000AACA File Offset: 0x00008CCA
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AB7E File Offset: 0x00008D7E
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AB88 File Offset: 0x00008D88
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
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AC21 File Offset: 0x00008E21
		private void MovePageUp(bool shift)
		{
			this.MovePageUp(shift, true);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AC2C File Offset: 0x00008E2C
		private void MovePageUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.PageUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num2 = this.m_TextViewport.rect.height;
				float num3 = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.max.y;
				float num4 = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMax;
				num2 = ((num4 > num3 + num2) ? num2 : (num4 - num3));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num2);
				this.AssignPositioningIfNeeded();
				this.m_IsScrollbarUpdateRequired = true;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000AD7F File Offset: 0x00008F7F
		private void MovePageDown(bool shift)
		{
			this.MovePageDown(shift, true);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000AD8C File Offset: 0x00008F8C
		private void MovePageDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = (this.multiLine ? this.PageDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num2 = this.m_TextViewport.rect.height;
				float num3 = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.min.y;
				float num4 = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMin;
				num2 = ((num4 > num3 + num2) ? num2 : (num4 - num3));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num2);
				this.AssignPositioningIfNeeded();
				this.m_IsScrollbarUpdateRequired = true;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000AEF0 File Offset: 0x000090F0
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.m_StringPosition == this.m_StringSelectPosition)
			{
				return;
			}
			if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
			{
				if (this.m_StringPosition < this.m_StringSelectPosition)
				{
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
				}
				else
				{
					this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					this.m_StringPosition = this.m_StringSelectPosition;
				}
				this.m_isSelectAll = false;
				return;
			}
			if (this.m_CaretPosition < this.m_CaretSelectPosition)
			{
				this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
				this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].stringLength;
				this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
				this.m_StringSelectPosition = this.m_StringPosition;
				this.m_CaretSelectPosition = this.m_CaretPosition;
				return;
			}
			this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].stringLength;
			this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
			this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
			this.m_StringPosition = this.m_StringSelectPosition;
			this.m_CaretPosition = this.m_CaretSelectPosition;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B10C File Offset: 0x0000930C
		private void DeleteKey()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_isLastKeyBackspace = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal < this.text.Length)
				{
					if (char.IsHighSurrogate(this.text[this.stringPositionInternal]))
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 2);
					}
					else
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 1);
					}
					this.m_isLastKeyBackspace = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else if (this.caretPositionInternal < this.m_TextComponent.textInfo.characterCount - 1)
			{
				int stringLength = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength;
				int index = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
				this.m_Text = this.text.Remove(index, stringLength);
				this.m_isLastKeyBackspace = true;
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B23C File Offset: 0x0000943C
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_isLastKeyBackspace = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal > 0)
				{
					int num = 1;
					if (char.IsLowSurrogate(this.text[this.stringPositionInternal - 1]))
					{
						num = 2;
					}
					this.stringSelectPositionInternal = (this.stringPositionInternal -= num);
					this.m_Text = this.text.Remove(this.stringPositionInternal, num);
					this.caretSelectPositionInternal = --this.caretPositionInternal;
					this.m_isLastKeyBackspace = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else
			{
				if (this.caretPositionInternal > 0)
				{
					int stringLength = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].stringLength;
					this.m_Text = this.text.Remove(this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index, stringLength);
					this.stringSelectPositionInternal = (this.stringPositionInternal = ((this.caretPositionInternal < 2) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 2].index + this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 2].stringLength)));
					this.caretSelectPositionInternal = --this.caretPositionInternal;
				}
				this.m_isLastKeyBackspace = true;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B40C File Offset: 0x0000960C
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

		// Token: 0x060001EC RID: 492 RVA: 0x0000B468 File Offset: 0x00009668
		protected virtual void Append(char input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(this.text, this.stringPositionInternal, input);
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator)
			{
				input = this.Validate(this.text, this.stringPositionInternal, input);
				if (input == '\0')
				{
					return;
				}
				this.SendOnValueChanged();
				this.UpdateLabel();
				return;
			}
			else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
			{
				input = this.Validate(this.text, this.stringPositionInternal, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B504 File Offset: 0x00009704
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
			this.m_Text = this.text.Insert(this.m_StringPosition, text);
			if (!char.IsHighSurrogate(c))
			{
				this.m_CaretSelectPosition = ++this.m_CaretPosition;
			}
			this.m_StringSelectPosition = ++this.m_StringPosition;
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B59F File Offset: 0x0000979F
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_SoftKeyboard != null && this.InPlaceEditing())
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B5C2 File Offset: 0x000097C2
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.UpdateLabel();
			this.SendOnValueChanged();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B5D0 File Offset: 0x000097D0
		private void SendOnValueChanged()
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B5EB File Offset: 0x000097EB
		protected void SendOnEndEdit()
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B606 File Offset: 0x00009806
		protected void SendOnSubmit()
		{
			if (this.onSubmit != null)
			{
				this.onSubmit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B621 File Offset: 0x00009821
		protected void SendOnFocus()
		{
			if (this.onSelect != null)
			{
				this.onSelect.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B63C File Offset: 0x0000983C
		protected void SendOnFocusLost()
		{
			if (this.onDeselect != null)
			{
				this.onDeselect.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B657 File Offset: 0x00009857
		protected void SendOnTextSelection()
		{
			this.m_isSelected = true;
			if (this.onTextSelection != null)
			{
				this.onTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B685 File Offset: 0x00009885
		protected void SendOnEndTextSelection()
		{
			if (!this.m_isSelected)
			{
				return;
			}
			if (this.onEndTextSelection != null)
			{
				this.onEndTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
			this.m_isSelected = false;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B6BC File Offset: 0x000098BC
		protected void SendTouchScreenKeyboardStatusChanged()
		{
			if (this.onTouchScreenKeyboardStatusChanged != null)
			{
				this.onTouchScreenKeyboardStatusChanged.Invoke(this.m_SoftKeyboard.status);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B6DC File Offset: 0x000098DC
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventCallback)
			{
				this.m_PreventCallback = true;
				string text;
				if (this.compositionLength > 0 && !this.m_ReadOnly)
				{
					this.Delete();
					text = string.Concat(new string[]
					{
						this.text.Substring(0, this.m_StringPosition),
						"<u>",
						this.compositionString,
						"</u>",
						this.text.Substring(this.m_StringPosition)
					});
					this.m_IsCompositionActive = true;
				}
				else
				{
					text = this.text;
					this.m_IsCompositionActive = false;
					this.m_ShouldUpdateIMEWindowPosition = true;
				}
				string text2;
				if (this.inputType == TMP_InputField.InputType.Password)
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
				if (!flag && !this.m_ReadOnly)
				{
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2 + "\u200b";
				if (this.m_IsDrivenByLayoutComponents)
				{
					LayoutRebuilder.MarkLayoutForRebuild(this.m_RectTransform);
				}
				if (this.m_LineLimit > 0)
				{
					this.m_TextComponent.ForceMeshUpdate(false, false);
					if (this.m_TextComponent.textInfo.lineCount > this.m_LineLimit)
					{
						int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[this.m_LineLimit - 1].lastCharacterIndex;
						int num = this.m_TextComponent.textInfo.characterInfo[lastCharacterIndex].index + this.m_TextComponent.textInfo.characterInfo[lastCharacterIndex].stringLength;
						this.text = text2.Remove(num, text2.Length - num);
						this.m_TextComponent.text = this.text + "\u200b";
					}
				}
				if (this.m_IsTextComponentUpdateRequired)
				{
					this.m_IsTextComponentUpdateRequired = false;
					this.m_TextComponent.ForceMeshUpdate(false, false);
				}
				this.MarkGeometryAsDirty();
				this.m_IsScrollbarUpdateRequired = true;
				this.m_PreventCallback = false;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B914 File Offset: 0x00009B14
		private void UpdateScrollbar()
		{
			if (this.m_VerticalScrollbar)
			{
				float num = this.m_TextViewport.rect.height / this.m_TextComponent.preferredHeight;
				this.m_IsUpdatingScrollbarValues = true;
				this.m_VerticalScrollbar.size = num;
				this.m_ScrollPosition = (this.m_VerticalScrollbar.value = this.m_TextComponent.rectTransform.anchoredPosition.y / (this.m_TextComponent.preferredHeight - this.m_TextViewport.rect.height));
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B9AA File Offset: 0x00009BAA
		private void OnScrollbarValueChange(float value)
		{
			if (this.m_IsUpdatingScrollbarValues)
			{
				this.m_IsUpdatingScrollbarValues = false;
				return;
			}
			if (value < 0f || value > 1f)
			{
				return;
			}
			this.AdjustTextPositionRelativeToViewport(value);
			this.m_ScrollPosition = value;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000027BA File Offset: 0x000009BA
		private void UpdateMaskRegions()
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private void AdjustTextPositionRelativeToViewport(float relativePosition)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (textInfo == null || textInfo.lineInfo == null || textInfo.lineCount == 0 || textInfo.lineCount > textInfo.lineInfo.Length)
			{
				return;
			}
			this.m_TextComponent.rectTransform.anchoredPosition = new Vector2(this.m_TextComponent.rectTransform.anchoredPosition.x, (this.m_TextComponent.preferredHeight - this.m_TextViewport.rect.height) * relativePosition);
			this.AssignPositioningIfNeeded();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000BA7C File Offset: 0x00009C7C
		private int GetCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		private int GetMinCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (stringIndex < this.m_TextComponent.textInfo.characterInfo[i].index + this.m_TextComponent.textInfo.characterInfo[i].stringLength)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000BB30 File Offset: 0x00009D30
		private int GetMaxCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000BB7B File Offset: 0x00009D7B
		private int GetStringIndexFromCaretPosition(int caretPosition)
		{
			this.ClampCaretPos(ref caretPosition);
			return this.m_TextComponent.textInfo.characterInfo[caretPosition].index;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000BBBC File Offset: 0x00009DBC
		private void UpdateGeometry()
		{
			if (!this.InPlaceEditing())
			{
				return;
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000BBF4 File Offset: 0x00009DF4
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

		// Token: 0x06000208 RID: 520 RVA: 0x0000BE1C File Offset: 0x0000A01C
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				if (!this.isFocused && !this.m_SelectionStillActive)
				{
					vertexHelper.FillMesh(vbo);
				}
				else
				{
					if (this.m_IsStringPositionDirty)
					{
						this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretPosition);
						this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretSelectPosition);
						this.m_IsStringPositionDirty = false;
					}
					if (this.m_IsCaretPositionDirty)
					{
						this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
						this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
						this.m_IsCaretPositionDirty = false;
					}
					if (!this.hasSelection && !this.m_ReadOnly)
					{
						this.GenerateCaret(vertexHelper, Vector2.zero);
						this.SendOnEndTextSelection();
					}
					else
					{
						this.GenerateHightlight(vertexHelper, Vector2.zero);
						this.SendOnTextSelection();
					}
					vertexHelper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BF0C File Offset: 0x0000A10C
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
			Vector2 zero = Vector2.zero;
			if (this.caretPositionInternal >= this.m_TextComponent.textInfo.characterInfo.Length)
			{
				return;
			}
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			TMP_CharacterInfo tmp_CharacterInfo;
			float num2;
			if (this.caretPositionInternal == this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex)
			{
				tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal];
				zero = new Vector2(tmp_CharacterInfo.origin, tmp_CharacterInfo.descender);
				num2 = tmp_CharacterInfo.ascender - tmp_CharacterInfo.descender;
			}
			else
			{
				tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1];
				zero = new Vector2(tmp_CharacterInfo.xAdvance, tmp_CharacterInfo.descender);
				num2 = tmp_CharacterInfo.ascender - tmp_CharacterInfo.descender;
			}
			if (this.m_SoftKeyboard != null)
			{
				int num3 = this.m_StringPosition;
				int num4 = ((this.m_SoftKeyboard.text == null) ? 0 : this.m_SoftKeyboard.text.Length);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num3 > num4)
				{
					num3 = num4;
				}
				this.m_SoftKeyboard.selection = new RangeInt(num3, 0);
			}
			if ((this.isFocused && zero != this.m_LastPosition) || this.m_forceRectTransformAdjustment || this.m_isLastKeyBackspace)
			{
				this.AdjustRectTransformRelativeToViewport(zero, num2, tmp_CharacterInfo.isVisible);
			}
			this.m_LastPosition = zero;
			float num5 = zero.y + num2;
			float num6 = num5 - num2;
			float scaleFactor = this.m_TextComponent.canvas.scaleFactor;
			this.m_CursorVerts[0].position = new Vector3(zero.x, num6, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x, num5, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num, num5, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x + num, num6, 0f);
			this.m_CursorVerts[0].color = this.caretColor;
			this.m_CursorVerts[1].color = this.caretColor;
			this.m_CursorVerts[2].color = this.caretColor;
			this.m_CursorVerts[3].color = this.caretColor;
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			if (this.m_ShouldUpdateIMEWindowPosition || lineNumber != this.m_PreviousIMEInsertionLine)
			{
				this.m_ShouldUpdateIMEWindowPosition = false;
				this.m_PreviousIMEInsertionLine = lineNumber;
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
				vector2.y = (float)Screen.height - vector2.y;
				this.inputSystem.compositionCursorPos = vector2;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C284 File Offset: 0x0000A484
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		private void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			this.UpdateMaskRegions();
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			this.m_CaretPosition = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			this.m_CaretSelectPosition = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			if (this.m_SoftKeyboard != null)
			{
				int num = ((this.m_CaretPosition < this.m_CaretSelectPosition) ? textInfo.characterInfo[this.m_CaretPosition].index : textInfo.characterInfo[this.m_CaretSelectPosition].index);
				int num2 = ((this.m_CaretPosition < this.m_CaretSelectPosition) ? (this.stringSelectPositionInternal - num) : (this.stringPositionInternal - num));
				this.m_SoftKeyboard.selection = new RangeInt(num, num2);
			}
			Vector2 vector;
			float num3;
			if (this.m_CaretSelectPosition < textInfo.characterCount)
			{
				vector = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition].origin, textInfo.characterInfo[this.m_CaretSelectPosition].descender);
				num3 = textInfo.characterInfo[this.m_CaretSelectPosition].ascender - textInfo.characterInfo[this.m_CaretSelectPosition].descender;
			}
			else
			{
				vector = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition - 1].xAdvance, textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender);
				num3 = textInfo.characterInfo[this.m_CaretSelectPosition - 1].ascender - textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender;
			}
			this.AdjustRectTransformRelativeToViewport(vector, num3, true);
			int num4 = Mathf.Max(0, this.m_CaretPosition);
			int num5 = Mathf.Max(0, this.m_CaretSelectPosition);
			if (num4 > num5)
			{
				int num6 = num4;
				num4 = num5;
				num5 = num6;
			}
			num5--;
			int num7 = textInfo.characterInfo[num4].lineNumber;
			int num8 = textInfo.lineInfo[num7].lastCharacterIndex;
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num9 = num4;
			while (num9 <= num5 && num9 < textInfo.characterCount)
			{
				if (num9 == num8 || num9 == num5)
				{
					TMP_CharacterInfo tmp_CharacterInfo = textInfo.characterInfo[num4];
					TMP_CharacterInfo tmp_CharacterInfo2 = textInfo.characterInfo[num9];
					if (num9 > 0 && tmp_CharacterInfo2.character == '\n' && textInfo.characterInfo[num9 - 1].character == '\r')
					{
						tmp_CharacterInfo2 = textInfo.characterInfo[num9 - 1];
					}
					Vector2 vector2 = new Vector2(tmp_CharacterInfo.origin, textInfo.lineInfo[num7].ascender);
					Vector2 vector3 = new Vector2(tmp_CharacterInfo2.xAdvance, textInfo.lineInfo[num7].descender);
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new Vector3(vector2.x, vector3.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector3.x, vector3.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector3.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num4 = num9 + 1;
					num7++;
					if (num7 < textInfo.lineCount)
					{
						num8 = textInfo.lineInfo[num7].lastCharacterIndex;
					}
				}
				num9++;
			}
			this.m_IsScrollbarUpdateRequired = true;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		private void AdjustRectTransformRelativeToViewport(Vector2 startPosition, float height, bool isCharVisible)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			Vector2 vector = new Vector2(startPosition.x + this.m_TextComponent.rectTransform.localPosition.x + this.m_TextViewport.localPosition.x + base.transform.localPosition.x, startPosition.y + this.m_TextComponent.rectTransform.localPosition.y + this.m_TextViewport.localPosition.y + base.transform.localPosition.y);
			Rect rect = new Rect(base.transform.localPosition.x + this.m_TextViewport.localPosition.x + this.m_TextViewport.rect.x, base.transform.localPosition.y + this.m_TextViewport.localPosition.y + this.m_TextViewport.rect.y, this.m_TextViewport.rect.width, this.m_TextViewport.rect.height);
			float num = rect.xMax - (vector.x + this.m_TextComponent.margin.z);
			if (num < 0f && (!this.multiLine || (this.multiLine && isCharVisible)))
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num, 0f);
				this.AssignPositioningIfNeeded();
			}
			float num2 = vector.x - this.m_TextComponent.margin.x - rect.xMin;
			if (num2 < 0f)
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(-num2, 0f);
				this.AssignPositioningIfNeeded();
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num3 = rect.yMax - (vector.y + height);
				if (num3 < -0.0001f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num3);
					this.AssignPositioningIfNeeded();
					this.m_IsScrollbarUpdateRequired = true;
				}
				float num4 = vector.y - rect.yMin;
				if (num4 < 0f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition -= new Vector2(0f, num4);
					this.AssignPositioningIfNeeded();
					this.m_IsScrollbarUpdateRequired = true;
				}
			}
			if (this.m_isLastKeyBackspace)
			{
				float x = this.m_TextComponent.rectTransform.anchoredPosition.x;
				float num5 = base.transform.localPosition.x + this.m_TextViewport.localPosition.x + this.m_TextComponent.rectTransform.localPosition.x + this.m_TextComponent.textInfo.characterInfo[0].origin - this.m_TextComponent.margin.x;
				float num6 = base.transform.localPosition.x + this.m_TextViewport.localPosition.x + this.m_TextComponent.rectTransform.localPosition.x + this.m_TextComponent.textInfo.characterInfo[this.m_TextComponent.textInfo.characterCount - 1].origin + this.m_TextComponent.margin.z;
				if (x > 0.0001f)
				{
					float num7 = rect.xMin - num5;
					if (x < -num7)
					{
						num7 = -x;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num7, 0f);
					this.AssignPositioningIfNeeded();
				}
				else if (x < -0.0001f)
				{
					float num8 = rect.xMax - num6;
					if (-x < num8)
					{
						num8 = -x;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num8, 0f);
					this.AssignPositioningIfNeeded();
				}
				this.m_isLastKeyBackspace = false;
			}
			this.m_forceRectTransformAdjustment = false;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == TMP_InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == TMP_InputField.CharacterValidation.Integer || this.characterValidation == TMP_InputField.CharacterValidation.Decimal)
			{
				bool flag = pos == 0 && text.Length > 0 && text[0] == '-';
				bool flag2 = this.stringPositionInternal == 0 || this.stringSelectPositionInternal == 0;
				if (!flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag2))
					{
						return ch;
					}
					if (ch == '.' && this.characterValidation == TMP_InputField.CharacterValidation.Decimal && !text.Contains("."))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Digit)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Alphanumeric)
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
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Name)
			{
				char c = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
				char c2 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && c == ' ')
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && c != ' ' && c != '\'')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				else if (ch == '\'')
				{
					if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
					{
						return ch;
					}
				}
				else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.EmailAddress)
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
					int num = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c3 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num != 46 && c3 != '.')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Regex)
			{
				if (Regex.IsMatch(ch.ToString(), this.m_RegexValue))
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator && this.m_InputValidator != null)
			{
				char c4 = this.m_InputValidator.Validate(ref text, ref pos, ch);
				this.m_Text = text;
				this.stringSelectPositionInternal = (this.stringPositionInternal = pos);
				return c4;
			}
			return '\0';
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_SoftKeyboard != null && !this.m_SoftKeyboard.active)
			{
				this.m_SoftKeyboard.active = true;
				this.m_SoftKeyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000CE64 File Offset: 0x0000B064
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
			if (TouchScreenKeyboard.isSupported && !this.shouldHideSoftKeyboard)
			{
				if (this.inputSystem.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				if (!this.shouldHideSoftKeyboard && !this.m_ReadOnly)
				{
					this.m_SoftKeyboard = ((this.inputType == TMP_InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, false, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == TMP_InputField.InputType.AutoCorrect, this.multiLine, false, false, "", this.characterLimit));
					this.OnFocus();
					if (this.m_SoftKeyboard != null)
					{
						int num = ((this.stringPositionInternal < this.stringSelectPositionInternal) ? (this.stringSelectPositionInternal - this.stringPositionInternal) : (this.stringPositionInternal - this.stringSelectPositionInternal));
						this.m_SoftKeyboard.selection = new RangeInt((this.stringPositionInternal < this.stringSelectPositionInternal) ? this.stringPositionInternal : this.stringSelectPositionInternal, num);
					}
				}
				this.m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			else
			{
				if (!TouchScreenKeyboard.isSupported && !this.m_ReadOnly)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000D006 File Offset: 0x0000B206
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.SendOnFocus();
			this.ActivateInputField();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000D01B File Offset: 0x0000B21B
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000027BA File Offset: 0x000009BA
		public void OnControlClick()
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000D02C File Offset: 0x0000B22C
		public void ReleaseSelection()
		{
			this.m_SelectionStillActive = false;
			this.MarkGeometryAsDirty();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000D03C File Offset: 0x0000B23C
		public void DeactivateInputField(bool clearSelection = false)
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
				if (this.m_WasCanceled && this.m_RestoreOriginalTextOnEscape)
				{
					this.text = this.m_OriginalText;
				}
				if (this.m_SoftKeyboard != null)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
				this.m_SelectionStillActive = true;
				if (this.m_ResetOnDeActivation || this.m_ReleaseSelection)
				{
					this.m_SelectionStillActive = false;
					this.m_ReleaseSelection = false;
					this.m_SelectedObject = null;
				}
				this.SendOnEndEdit();
				this.SendOnEndTextSelection();
				this.inputSystem.imeCompositionMode = IMECompositionMode.Auto;
			}
			this.MarkGeometryAsDirty();
			this.m_IsScrollbarUpdateRequired = true;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000D125 File Offset: 0x0000B325
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField(false);
			base.OnDeselect(eventData);
			this.SendOnFocusLost();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000D13B File Offset: 0x0000B33B
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
			this.SendOnSubmit();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000D164 File Offset: 0x0000B364
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case TMP_InputField.ContentType.Standard:
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Autocorrected:
				this.m_InputType = TMP_InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.IntegerNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Integer;
				break;
			case TMP_InputField.ContentType.DecimalNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Decimal;
				break;
			case TMP_InputField.ContentType.Alphanumeric:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Alphanumeric;
				break;
			case TMP_InputField.ContentType.Name:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Name;
				break;
			case TMP_InputField.ContentType.EmailAddress:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.EmailAddress;
				break;
			case TMP_InputField.ContentType.Password:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Pin:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Digit;
				break;
			}
			this.SetTextComponentWrapMode();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000D2B7 File Offset: 0x0000B4B7
		private void SetTextComponentWrapMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			if (this.multiLine)
			{
				this.m_TextComponent.enableWordWrapping = true;
				return;
			}
			this.m_TextComponent.enableWordWrapping = false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
		private void SetTextComponentRichTextMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			this.m_TextComponent.richText = this.m_RichText;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000D30C File Offset: 0x0000B50C
		private void SetToCustomIfContentTypeIsNot(params TMP_InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
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
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000D346 File Offset: 0x0000B546
		private void SetToCustom()
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000D35B File Offset: 0x0000B55B
		private void SetToCustom(TMP_InputField.CharacterValidation characterValidation)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000D376 File Offset: 0x0000B576
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Highlighted;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000027BA File Offset: 0x000009BA
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000D398 File Offset: 0x0000B598
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (this.m_LayoutGroup != null)
				{
					num = (float)this.m_LayoutGroup.padding.horizontal;
				}
				if (this.m_TextViewport != null)
				{
					num += this.m_TextViewport.offsetMin.x - this.m_TextViewport.offsetMax.x;
				}
				return this.m_TextComponent.preferredWidth + num;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000D426 File Offset: 0x0000B626
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000D398 File Offset: 0x0000B598
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000D430 File Offset: 0x0000B630
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (this.m_LayoutGroup != null)
				{
					num = (float)this.m_LayoutGroup.padding.vertical;
				}
				if (this.m_TextViewport != null)
				{
					num += this.m_TextViewport.offsetMin.y - this.m_TextViewport.offsetMax.y;
				}
				return this.m_TextComponent.preferredHeight + num;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000D426 File Offset: 0x0000B626
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000D4B6 File Offset: 0x0000B6B6
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public void SetGlobalPointSize(float pointSize)
		{
			TMP_Text tmp_Text = this.m_Placeholder as TMP_Text;
			if (tmp_Text != null)
			{
				tmp_Text.fontSize = pointSize;
			}
			this.textComponent.fontSize = pointSize;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		public void SetGlobalFontAsset(TMP_FontAsset fontAsset)
		{
			TMP_Text tmp_Text = this.m_Placeholder as TMP_Text;
			if (tmp_Text != null)
			{
				tmp_Text.font = fontAsset;
			}
			this.textComponent.font = fontAsset;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D541 File Offset: 0x0000B741
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000103 RID: 259
		protected TouchScreenKeyboard m_SoftKeyboard;

		// Token: 0x04000104 RID: 260
		private static readonly char[] kSeparators = new char[] { ' ', '.', ',', '\t', '\r', '\n' };

		// Token: 0x04000105 RID: 261
		protected RectTransform m_RectTransform;

		// Token: 0x04000106 RID: 262
		[SerializeField]
		protected RectTransform m_TextViewport;

		// Token: 0x04000107 RID: 263
		protected RectMask2D m_TextComponentRectMask;

		// Token: 0x04000108 RID: 264
		protected RectMask2D m_TextViewportRectMask;

		// Token: 0x04000109 RID: 265
		private Rect m_CachedViewportRect;

		// Token: 0x0400010A RID: 266
		[SerializeField]
		protected TMP_Text m_TextComponent;

		// Token: 0x0400010B RID: 267
		protected RectTransform m_TextComponentRectTransform;

		// Token: 0x0400010C RID: 268
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x0400010D RID: 269
		[SerializeField]
		protected Scrollbar m_VerticalScrollbar;

		// Token: 0x0400010E RID: 270
		[SerializeField]
		protected TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

		// Token: 0x0400010F RID: 271
		private bool m_IsDrivenByLayoutComponents;

		// Token: 0x04000110 RID: 272
		[SerializeField]
		private LayoutGroup m_LayoutGroup;

		// Token: 0x04000111 RID: 273
		private float m_ScrollPosition;

		// Token: 0x04000112 RID: 274
		[SerializeField]
		protected float m_ScrollSensitivity = 1f;

		// Token: 0x04000113 RID: 275
		[SerializeField]
		private TMP_InputField.ContentType m_ContentType;

		// Token: 0x04000114 RID: 276
		[SerializeField]
		private TMP_InputField.InputType m_InputType;

		// Token: 0x04000115 RID: 277
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x04000116 RID: 278
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x04000117 RID: 279
		[SerializeField]
		private TMP_InputField.LineType m_LineType;

		// Token: 0x04000118 RID: 280
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x04000119 RID: 281
		[SerializeField]
		private bool m_HideSoftKeyboard;

		// Token: 0x0400011A RID: 282
		[SerializeField]
		private TMP_InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x0400011B RID: 283
		[SerializeField]
		private string m_RegexValue = string.Empty;

		// Token: 0x0400011C RID: 284
		[SerializeField]
		private float m_GlobalPointSize = 14f;

		// Token: 0x0400011D RID: 285
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x0400011E RID: 286
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnEndEdit = new TMP_InputField.SubmitEvent();

		// Token: 0x0400011F RID: 287
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnSubmit = new TMP_InputField.SubmitEvent();

		// Token: 0x04000120 RID: 288
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnSelect = new TMP_InputField.SelectionEvent();

		// Token: 0x04000121 RID: 289
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnDeselect = new TMP_InputField.SelectionEvent();

		// Token: 0x04000122 RID: 290
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x04000123 RID: 291
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnEndTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x04000124 RID: 292
		[SerializeField]
		private TMP_InputField.OnChangeEvent m_OnValueChanged = new TMP_InputField.OnChangeEvent();

		// Token: 0x04000125 RID: 293
		[SerializeField]
		private TMP_InputField.TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new TMP_InputField.TouchScreenKeyboardEvent();

		// Token: 0x04000126 RID: 294
		[SerializeField]
		private TMP_InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x04000127 RID: 295
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x04000128 RID: 296
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x04000129 RID: 297
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x0400012A RID: 298
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_Text = string.Empty;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x0400012C RID: 300
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x0400012D RID: 301
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x0400012E RID: 302
		[SerializeField]
		private bool m_RichText = true;

		// Token: 0x0400012F RID: 303
		protected int m_StringPosition;

		// Token: 0x04000130 RID: 304
		protected int m_StringSelectPosition;

		// Token: 0x04000131 RID: 305
		protected int m_CaretPosition;

		// Token: 0x04000132 RID: 306
		protected int m_CaretSelectPosition;

		// Token: 0x04000133 RID: 307
		private RectTransform caretRectTrans;

		// Token: 0x04000134 RID: 308
		protected UIVertex[] m_CursorVerts;

		// Token: 0x04000135 RID: 309
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x04000136 RID: 310
		private Vector2 m_LastPosition;

		// Token: 0x04000137 RID: 311
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x04000138 RID: 312
		private bool m_AllowInput;

		// Token: 0x04000139 RID: 313
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x0400013A RID: 314
		private bool m_UpdateDrag;

		// Token: 0x0400013B RID: 315
		private bool m_DragPositionOutOfBounds;

		// Token: 0x0400013C RID: 316
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x0400013D RID: 317
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x0400013E RID: 318
		protected bool m_CaretVisible;

		// Token: 0x0400013F RID: 319
		private Coroutine m_BlinkCoroutine;

		// Token: 0x04000140 RID: 320
		private float m_BlinkStartTime;

		// Token: 0x04000141 RID: 321
		private Coroutine m_DragCoroutine;

		// Token: 0x04000142 RID: 322
		private string m_OriginalText = "";

		// Token: 0x04000143 RID: 323
		private bool m_WasCanceled;

		// Token: 0x04000144 RID: 324
		private bool m_HasDoneFocusTransition;

		// Token: 0x04000145 RID: 325
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x04000146 RID: 326
		private bool m_PreventCallback;

		// Token: 0x04000147 RID: 327
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x04000148 RID: 328
		private bool m_IsTextComponentUpdateRequired;

		// Token: 0x04000149 RID: 329
		private bool m_IsScrollbarUpdateRequired;

		// Token: 0x0400014A RID: 330
		private bool m_IsUpdatingScrollbarValues;

		// Token: 0x0400014B RID: 331
		private bool m_isLastKeyBackspace;

		// Token: 0x0400014C RID: 332
		private float m_PointerDownClickStartTime;

		// Token: 0x0400014D RID: 333
		private float m_KeyDownStartTime;

		// Token: 0x0400014E RID: 334
		private float m_DoubleClickDelay = 0.5f;

		// Token: 0x0400014F RID: 335
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x04000150 RID: 336
		private bool m_IsCompositionActive;

		// Token: 0x04000151 RID: 337
		private bool m_ShouldUpdateIMEWindowPosition;

		// Token: 0x04000152 RID: 338
		private int m_PreviousIMEInsertionLine;

		// Token: 0x04000153 RID: 339
		[SerializeField]
		protected TMP_FontAsset m_GlobalFontAsset;

		// Token: 0x04000154 RID: 340
		[SerializeField]
		protected bool m_OnFocusSelectAll = true;

		// Token: 0x04000155 RID: 341
		protected bool m_isSelectAll;

		// Token: 0x04000156 RID: 342
		[SerializeField]
		protected bool m_ResetOnDeActivation = true;

		// Token: 0x04000157 RID: 343
		private bool m_SelectionStillActive;

		// Token: 0x04000158 RID: 344
		private bool m_ReleaseSelection;

		// Token: 0x04000159 RID: 345
		private GameObject m_SelectedObject;

		// Token: 0x0400015A RID: 346
		[SerializeField]
		private bool m_RestoreOriginalTextOnEscape = true;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		protected bool m_isRichTextEditingAllowed;

		// Token: 0x0400015C RID: 348
		[SerializeField]
		protected int m_LineLimit;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		protected TMP_InputValidator m_InputValidator;

		// Token: 0x0400015E RID: 350
		private bool m_isSelected;

		// Token: 0x0400015F RID: 351
		private bool m_IsStringPositionDirty;

		// Token: 0x04000160 RID: 352
		private bool m_IsCaretPositionDirty;

		// Token: 0x04000161 RID: 353
		private bool m_forceRectTransformAdjustment;

		// Token: 0x04000162 RID: 354
		private Event m_ProcessingEvent = new Event();

		// Token: 0x02000087 RID: 135
		public enum ContentType
		{
			// Token: 0x04000544 RID: 1348
			Standard,
			// Token: 0x04000545 RID: 1349
			Autocorrected,
			// Token: 0x04000546 RID: 1350
			IntegerNumber,
			// Token: 0x04000547 RID: 1351
			DecimalNumber,
			// Token: 0x04000548 RID: 1352
			Alphanumeric,
			// Token: 0x04000549 RID: 1353
			Name,
			// Token: 0x0400054A RID: 1354
			EmailAddress,
			// Token: 0x0400054B RID: 1355
			Password,
			// Token: 0x0400054C RID: 1356
			Pin,
			// Token: 0x0400054D RID: 1357
			Custom
		}

		// Token: 0x02000088 RID: 136
		public enum InputType
		{
			// Token: 0x0400054F RID: 1359
			Standard,
			// Token: 0x04000550 RID: 1360
			AutoCorrect,
			// Token: 0x04000551 RID: 1361
			Password
		}

		// Token: 0x02000089 RID: 137
		public enum CharacterValidation
		{
			// Token: 0x04000553 RID: 1363
			None,
			// Token: 0x04000554 RID: 1364
			Digit,
			// Token: 0x04000555 RID: 1365
			Integer,
			// Token: 0x04000556 RID: 1366
			Decimal,
			// Token: 0x04000557 RID: 1367
			Alphanumeric,
			// Token: 0x04000558 RID: 1368
			Name,
			// Token: 0x04000559 RID: 1369
			Regex,
			// Token: 0x0400055A RID: 1370
			EmailAddress,
			// Token: 0x0400055B RID: 1371
			CustomValidator
		}

		// Token: 0x0200008A RID: 138
		public enum LineType
		{
			// Token: 0x0400055D RID: 1373
			SingleLine,
			// Token: 0x0400055E RID: 1374
			MultiLineSubmit,
			// Token: 0x0400055F RID: 1375
			MultiLineNewline
		}

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x060005C3 RID: 1475
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x0200008C RID: 140
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200008D RID: 141
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200008E RID: 142
		[Serializable]
		public class SelectionEvent : UnityEvent<string>
		{
		}

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class TextSelectionEvent : UnityEvent<string, int, int>
		{
		}

		// Token: 0x02000090 RID: 144
		[Serializable]
		public class TouchScreenKeyboardEvent : UnityEvent<TouchScreenKeyboard.Status>
		{
		}

		// Token: 0x02000091 RID: 145
		protected enum EditState
		{
			// Token: 0x04000561 RID: 1377
			Continue,
			// Token: 0x04000562 RID: 1378
			Finish
		}
	}
}
