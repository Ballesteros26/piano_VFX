using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FE RID: 254
	public abstract class TextInputBaseField<TValueType> : BaseField<TValueType>
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001DAFD File Offset: 0x0001BCFD
		protected TextInputBaseField<TValueType>.TextInputBase textInputBase
		{
			get
			{
				return this.m_TextInputBase;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001DB05 File Offset: 0x0001BD05
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001DB0D File Offset: 0x0001BD0D
		internal TextHandle textHandle { get; private set; } = TextHandle.New();

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001DB18 File Offset: 0x0001BD18
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001DB35 File Offset: 0x0001BD35
		public string text
		{
			get
			{
				return this.m_TextInputBase.text;
			}
			protected set
			{
				this.m_TextInputBase.text = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001DB48 File Offset: 0x0001BD48
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001DB65 File Offset: 0x0001BD65
		public bool isReadOnly
		{
			get
			{
				return this.m_TextInputBase.isReadOnly;
			}
			set
			{
				this.m_TextInputBase.isReadOnly = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001DB78 File Offset: 0x0001BD78
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001DB95 File Offset: 0x0001BD95
		public bool isPasswordField
		{
			get
			{
				return this.m_TextInputBase.isPasswordField;
			}
			set
			{
				this.m_TextInputBase.isPasswordField = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001DBA5 File Offset: 0x0001BDA5
		public Color selectionColor
		{
			get
			{
				return this.m_TextInputBase.selectionColor;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001DBB2 File Offset: 0x0001BDB2
		public Color cursorColor
		{
			get
			{
				return this.m_TextInputBase.cursorColor;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001DBBF File Offset: 0x0001BDBF
		public int cursorIndex
		{
			get
			{
				return this.m_TextInputBase.cursorIndex;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public int selectIndex
		{
			get
			{
				return this.m_TextInputBase.selectIndex;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001DBDC File Offset: 0x0001BDDC
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001DBF9 File Offset: 0x0001BDF9
		public int maxLength
		{
			get
			{
				return this.m_TextInputBase.maxLength;
			}
			set
			{
				this.m_TextInputBase.maxLength = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001DC29 File Offset: 0x0001BE29
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_TextInputBase.doubleClickSelectsWord;
			}
			set
			{
				this.m_TextInputBase.doubleClickSelectsWord = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001DC59 File Offset: 0x0001BE59
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TextInputBase.tripleClickSelectsLine;
			}
			set
			{
				this.m_TextInputBase.tripleClickSelectsLine = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001DC6C File Offset: 0x0001BE6C
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001DC89 File Offset: 0x0001BE89
		public bool isDelayed
		{
			get
			{
				return this.m_TextInputBase.isDelayed;
			}
			set
			{
				this.m_TextInputBase.isDelayed = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001DCB9 File Offset: 0x0001BEB9
		public char maskChar
		{
			get
			{
				return this.m_TextInputBase.maskChar;
			}
			set
			{
				this.m_TextInputBase.maskChar = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001DCC9 File Offset: 0x0001BEC9
		internal TextEditorEventHandler editorEventHandler
		{
			get
			{
				return this.m_TextInputBase.editorEventHandler;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001DCD6 File Offset: 0x0001BED6
		internal TextEditorEngine editorEngine
		{
			get
			{
				return this.m_TextInputBase.editorEngine;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001DCE3 File Offset: 0x0001BEE3
		internal bool hasFocus
		{
			get
			{
				return this.m_TextInputBase.hasFocus;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		public void SelectAll()
		{
			this.m_TextInputBase.SelectAll();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001DCFF File Offset: 0x0001BEFF
		internal void SyncTextEngine()
		{
			this.m_TextInputBase.SyncTextEngine();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001DD0E File Offset: 0x0001BF0E
		internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText)
		{
			this.m_TextInputBase.DrawWithTextSelectionAndCursor(mgc, newText, base.scaledPixelsPerPoint);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001DD25 File Offset: 0x0001BF25
		protected TextInputBaseField(int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: this(null, maxLength, maskChar, textInputBase)
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001DD34 File Offset: 0x0001BF34
		protected TextInputBaseField(string label, int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase)
			: base(label, textInputBase)
		{
			base.tabIndex = 0;
			base.delegatesFocus = false;
			base.AddToClassList(TextInputBaseField<TValueType>.ussClassName);
			base.labelElement.AddToClassList(TextInputBaseField<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
			this.m_TextInputBase = textInputBase;
			this.m_TextInputBase.maxLength = maxLength;
			this.m_TextInputBase.maskChar = maskChar;
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			TextHandle textHandle = this.textHandle;
			textHandle.useLegacy = e.destinationPanel.contextType == ContextType.Editor;
			this.textHandle = textHandle;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001DE00 File Offset: 0x0001C000
		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag2)
				{
					KeyDownEvent keyDownEvent = evt as KeyDownEvent;
					char? c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
					int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
					int num2 = 3;
					bool flag3;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : default(char?));
						num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
						num2 = 10;
						flag3 = (num.GetValueOrDefault() == num2) & (num != null);
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						VisualElement visualInput = base.visualInput;
						if (visualInput != null)
						{
							visualInput.Focus();
						}
					}
				}
			}
		}

		// Token: 0x04000359 RID: 857
		private static CustomStyleProperty<Color> s_SelectionColorProperty = new CustomStyleProperty<Color>("--unity-selection-color");

		// Token: 0x0400035A RID: 858
		private static CustomStyleProperty<Color> s_CursorColorProperty = new CustomStyleProperty<Color>("--unity-cursor-color");

		// Token: 0x0400035B RID: 859
		private TextInputBaseField<TValueType>.TextInputBase m_TextInputBase;

		// Token: 0x0400035C RID: 860
		internal const int kMaxLengthNone = -1;

		// Token: 0x0400035D RID: 861
		internal const char kMaskCharDefault = '*';

		// Token: 0x0400035F RID: 863
		public new static readonly string ussClassName = "unity-base-text-field";

		// Token: 0x04000360 RID: 864
		public new static readonly string labelUssClassName = TextInputBaseField<TValueType>.ussClassName + "__label";

		// Token: 0x04000361 RID: 865
		public new static readonly string inputUssClassName = TextInputBaseField<TValueType>.ussClassName + "__input";

		// Token: 0x04000362 RID: 866
		public static readonly string textInputUssName = "unity-text-input";

		// Token: 0x020000FF RID: 255
		public new class UxmlTraits : BaseFieldTraits<string, UxmlStringAttributeDescription>
		{
			// Token: 0x0600075D RID: 1885 RVA: 0x0001DF74 File Offset: 0x0001C174
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)ve;
				textInputBaseField.maxLength = this.m_MaxLength.GetValueFromBag(bag, cc);
				textInputBaseField.isPasswordField = this.m_Password.GetValueFromBag(bag, cc);
				textInputBaseField.isReadOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				string valueFromBag = this.m_MaskCharacter.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					textInputBaseField.maskChar = valueFromBag.get_Chars(0);
				}
				textInputBaseField.text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000363 RID: 867
			private UxmlIntAttributeDescription m_MaxLength = new UxmlIntAttributeDescription
			{
				name = "max-length",
				obsoleteNames = new string[] { "maxLength" },
				defaultValue = -1
			};

			// Token: 0x04000364 RID: 868
			private UxmlBoolAttributeDescription m_Password = new UxmlBoolAttributeDescription
			{
				name = "password"
			};

			// Token: 0x04000365 RID: 869
			private UxmlStringAttributeDescription m_MaskCharacter = new UxmlStringAttributeDescription
			{
				name = "mask-character",
				obsoleteNames = new string[] { "maskCharacter" },
				defaultValue = '*'.ToString()
			};

			// Token: 0x04000366 RID: 870
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x04000367 RID: 871
			private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
			{
				name = "readonly"
			};
		}

		// Token: 0x02000100 RID: 256
		protected abstract class TextInputBase : VisualElement, ITextInputField, IEventHandler, ITextElement
		{
			// Token: 0x0600075F RID: 1887 RVA: 0x0001E0DE File Offset: 0x0001C2DE
			private void SaveValueAndText()
			{
				this.m_OriginalText = this.text;
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x0001E0ED File Offset: 0x0001C2ED
			private void RestoreValueAndText()
			{
				this.text = this.m_OriginalText;
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x0001E0FD File Offset: 0x0001C2FD
			public void SelectAll()
			{
				TextEditorEngine editorEngine = this.editorEngine;
				if (editorEngine != null)
				{
					editorEngine.SelectAll();
				}
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x0001E112 File Offset: 0x0001C312
			internal void SelectNone()
			{
				TextEditorEngine editorEngine = this.editorEngine;
				if (editorEngine != null)
				{
					editorEngine.SelectNone();
				}
			}

			// Token: 0x06000763 RID: 1891 RVA: 0x0001E128 File Offset: 0x0001C328
			private void UpdateText(string value)
			{
				bool flag = this.text != value;
				if (flag)
				{
					using (InputEvent pooled = InputEvent.GetPooled(this.text, value))
					{
						pooled.target = base.parent;
						this.text = value;
						VisualElement parent = base.parent;
						if (parent != null)
						{
							parent.SendEvent(pooled);
						}
					}
				}
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x0001E19C File Offset: 0x0001C39C
			protected virtual TValueType StringToValue(string str)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
			internal void UpdateValueFromText()
			{
				TValueType tvalueType = this.StringToValue(this.text);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				textInputBaseField.value = tvalueType;
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
			public int cursorIndex
			{
				get
				{
					return this.editorEngine.cursorIndex;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
			public int selectIndex
			{
				get
				{
					return this.editorEngine.selectIndex;
				}
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001E211 File Offset: 0x0001C411
			bool ITextInputField.isReadOnly
			{
				get
				{
					return this.isReadOnly;
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001E219 File Offset: 0x0001C419
			// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001E221 File Offset: 0x0001C421
			public bool isReadOnly { get; set; }

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001E22A File Offset: 0x0001C42A
			// (set) Token: 0x0600076C RID: 1900 RVA: 0x0001E232 File Offset: 0x0001C432
			public int maxLength { get; set; }

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001E23B File Offset: 0x0001C43B
			// (set) Token: 0x0600076E RID: 1902 RVA: 0x0001E243 File Offset: 0x0001C443
			public char maskChar { get; set; }

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001E24C File Offset: 0x0001C44C
			// (set) Token: 0x06000770 RID: 1904 RVA: 0x0001E254 File Offset: 0x0001C454
			public virtual bool isPasswordField { get; set; }

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001E25D File Offset: 0x0001C45D
			// (set) Token: 0x06000772 RID: 1906 RVA: 0x0001E265 File Offset: 0x0001C465
			public bool doubleClickSelectsWord { get; set; }

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001E26E File Offset: 0x0001C46E
			// (set) Token: 0x06000774 RID: 1908 RVA: 0x0001E276 File Offset: 0x0001C476
			public bool tripleClickSelectsLine { get; set; }

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001E27F File Offset: 0x0001C47F
			// (set) Token: 0x06000776 RID: 1910 RVA: 0x0001E287 File Offset: 0x0001C487
			internal bool isDelayed { get; set; }

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001E290 File Offset: 0x0001C490
			// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001E298 File Offset: 0x0001C498
			internal bool isDragging { get; set; }

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
			private bool touchScreenTextField
			{
				get
				{
					return TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
			public Color selectionColor
			{
				get
				{
					return this.m_SelectionColor;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
			public Color cursorColor
			{
				get
				{
					return this.m_CursorColor;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
			internal bool hasFocus
			{
				get
				{
					return base.elementPanel != null && base.elementPanel.focusController.GetLeafFocusedElement() == this;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001E308 File Offset: 0x0001C508
			// (set) Token: 0x0600077E RID: 1918 RVA: 0x0001E310 File Offset: 0x0001C510
			internal TextEditorEventHandler editorEventHandler { get; private set; }

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001E319 File Offset: 0x0001C519
			// (set) Token: 0x06000780 RID: 1920 RVA: 0x0001E321 File Offset: 0x0001C521
			internal TextEditorEngine editorEngine { get; private set; }

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001E32C File Offset: 0x0001C52C
			// (set) Token: 0x06000782 RID: 1922 RVA: 0x0001E344 File Offset: 0x0001C544
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					bool flag = this.m_Text == value;
					if (!flag)
					{
						this.m_Text = value;
						this.editorEngine.text = value;
						base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
				}
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x0001E384 File Offset: 0x0001C584
			internal TextInputBase()
			{
				this.isReadOnly = false;
				base.focusable = true;
				base.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
				this.m_Text = string.Empty;
				base.name = TextInputBaseField<string>.textInputUssName;
				base.requireMeasureFunction = true;
				this.editorEngine = new TextEditorEngine(new TextEditorEngine.OnDetectFocusChangeFunction(this.OnDetectFocusChange), new TextEditorEngine.OnIndexChangeFunction(this.OnCursorIndexChange));
				bool touchScreenTextField = this.touchScreenTextField;
				if (touchScreenTextField)
				{
					this.editorEventHandler = new TouchScreenTextEditorEventHandler(this.editorEngine, this);
				}
				else
				{
					this.doubleClickSelectsWord = true;
					this.tripleClickSelectsLine = true;
					this.editorEventHandler = new KeyboardTextEditorEventHandler(this.editorEngine, this);
				}
				this.editorEngine.style = new GUIStyle(this.editorEngine.style);
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
				base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
			private DropdownMenuAction.Status CutCopyActionStatus(DropdownMenuAction a)
			{
				return (this.editorEngine.hasSelection && !this.isPasswordField) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
			private DropdownMenuAction.Status PasteActionStatus(DropdownMenuAction a)
			{
				return this.editorEngine.CanPaste() ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x0001E518 File Offset: 0x0001C718
			private void ProcessMenuCommand(string command)
			{
				using (ExecuteCommandEvent pooled = CommandEventBase<ExecuteCommandEvent>.GetPooled(command))
				{
					pooled.target = this;
					this.SendEvent(pooled);
				}
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x0001E55C File Offset: 0x0001C75C
			private void Cut(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Cut");
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x0001E56B File Offset: 0x0001C76B
			private void Copy(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Copy");
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x0001E57A File Offset: 0x0001C77A
			private void Paste(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Paste");
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x0001E58C File Offset: 0x0001C78C
			private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
			{
				Color clear = Color.clear;
				Color clear2 = Color.clear;
				ICustomStyle customStyle = e.customStyle;
				bool flag = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_SelectionColorProperty, out clear);
				if (flag)
				{
					this.m_SelectionColor = clear;
				}
				bool flag2 = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_CursorColorProperty, out clear2);
				if (flag2)
				{
					this.m_CursorColor = clear2;
				}
				TextInputBaseField<TValueType>.TextInputBase.SyncGUIStyle(this, this.editorEngine.style);
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x0001E5F1 File Offset: 0x0001C7F1
			private void OnAttachToPanel(AttachToPanelEvent e)
			{
				this.m_TextHandle.useLegacy = e.destinationPanel.contextType == ContextType.Editor;
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x0001E610 File Offset: 0x0001C810
			internal virtual void SyncTextEngine()
			{
				this.editorEngine.text = this.CullString(this.text);
				this.editorEngine.SaveBackup();
				this.editorEngine.position = base.layout;
				this.editorEngine.DetectFocusChange();
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x0001E660 File Offset: 0x0001C860
			internal string CullString(string s)
			{
				bool flag = this.maxLength >= 0 && s != null && s.Length > this.maxLength;
				string text;
				if (flag)
				{
					text = s.Substring(0, this.maxLength);
				}
				else
				{
					text = s;
				}
				return text;
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
			internal void OnGenerateVisualContent(MeshGenerationContext mgc)
			{
				string text = this.text;
				bool isPasswordField = this.isPasswordField;
				if (isPasswordField)
				{
					text = "".PadRight(this.text.Length, this.maskChar);
				}
				bool touchScreenTextField = this.touchScreenTextField;
				if (touchScreenTextField)
				{
					TouchScreenTextEditorEventHandler touchScreenTextEditorEventHandler = this.editorEventHandler as TouchScreenTextEditorEventHandler;
					bool flag = touchScreenTextEditorEventHandler != null;
					if (flag)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
				}
				else
				{
					bool flag2 = !this.hasFocus;
					if (flag2)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					else
					{
						this.DrawWithTextSelectionAndCursor(mgc, text, base.scaledPixelsPerPoint);
					}
				}
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x0001E764 File Offset: 0x0001C964
			internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText, float pixelsPerPoint)
			{
				Color color = ((base.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				KeyboardTextEditorEventHandler keyboardTextEditorEventHandler = this.editorEventHandler as KeyboardTextEditorEventHandler;
				bool flag = keyboardTextEditorEventHandler == null;
				if (!flag)
				{
					keyboardTextEditorEventHandler.PreDrawCursor(newText);
					int cursorIndex = this.editorEngine.cursorIndex;
					int selectIndex = this.editorEngine.selectIndex;
					Rect localPosition = this.editorEngine.localPosition;
					Vector2 vector = this.editorEngine.scrollOffset;
					float num = TextHandle.ComputeTextScaling(base.worldTransform, pixelsPerPoint);
					MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, this.text);
					textParams.text = " ";
					textParams.wordWrapWidth = 0f;
					textParams.wordWrap = false;
					float num2 = this.m_TextHandle.ComputeTextHeight(textParams, num);
					float num3 = 0f;
					bool flag2 = this.editorEngine.multiline && base.resolvedStyle.whiteSpace == WhiteSpace.Normal;
					if (flag2)
					{
						num3 = base.contentRect.width;
						vector = Vector2.zero;
					}
					Vector2 vector2 = this.editorEngine.graphicalCursorPos - vector;
					vector2.y += num2;
					GUIUtility.compositionCursorPos = this.LocalToWorld(vector2);
					Color cursorColor = this.cursorColor;
					int num4 = (string.IsNullOrEmpty(GUIUtility.compositionString) ? selectIndex : (cursorIndex + GUIUtility.compositionString.Length));
					bool flag3 = cursorIndex != num4 && !this.isDragging;
					if (flag3)
					{
						int num5 = ((cursorIndex < num4) ? cursorIndex : num4);
						int num6 = ((cursorIndex > num4) ? cursorIndex : num4);
						CursorPositionStylePainterParameters cursorPositionStylePainterParameters = CursorPositionStylePainterParameters.GetDefault(this, this.text);
						cursorPositionStylePainterParameters.text = this.editorEngine.text;
						cursorPositionStylePainterParameters.wordWrapWidth = num3;
						cursorPositionStylePainterParameters.cursorIndex = num5;
						Vector2 vector3 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
						cursorPositionStylePainterParameters.cursorIndex = num6;
						Vector2 vector4 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
						vector3 -= vector;
						vector4 -= vector;
						bool flag4 = Mathf.Approximately(vector3.y, vector4.y);
						if (flag4)
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector3.x, vector3.y, vector4.x - vector3.x, num2),
								color = this.selectionColor,
								playmodeTintColor = color
							});
						}
						else
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector3.x, vector3.y, base.contentRect.xMax - vector3.x, num2),
								color = this.selectionColor,
								playmodeTintColor = color
							});
							float num7 = vector4.y - vector3.y - num2;
							bool flag5 = num7 > 0f;
							if (flag5)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector3.y + num2, base.contentRect.width, num7),
									color = this.selectionColor,
									playmodeTintColor = color
								});
							}
							bool flag6 = vector4.x != base.contentRect.x;
							if (flag6)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector4.y, vector4.x, num2),
									color = this.selectionColor,
									playmodeTintColor = color
								});
							}
						}
					}
					bool flag7 = !string.IsNullOrEmpty(this.editorEngine.text) && base.contentRect.width > 0f && base.contentRect.height > 0f;
					if (flag7)
					{
						textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, this.text);
						textParams.rect = new Rect(base.contentRect.x - vector.x, base.contentRect.y - vector.y, base.contentRect.width + vector.x, base.contentRect.height + vector.y);
						textParams.text = this.editorEngine.text;
						mgc.Text(textParams, this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					bool flag8 = !this.isReadOnly && !this.isDragging;
					if (flag8)
					{
						bool flag9 = cursorIndex == num4 && base.computedStyle.unityFont.value != null;
						if (flag9)
						{
							CursorPositionStylePainterParameters cursorPositionStylePainterParameters = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							cursorPositionStylePainterParameters.text = this.editorEngine.text;
							cursorPositionStylePainterParameters.wordWrapWidth = num3;
							cursorPositionStylePainterParameters.cursorIndex = cursorIndex;
							Vector2 vector5 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
							vector5 -= vector;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector5.x, vector5.y, 1f, num2),
								color = cursorColor,
								playmodeTintColor = color
							});
						}
						bool flag10 = this.editorEngine.altCursorPosition != -1;
						if (flag10)
						{
							CursorPositionStylePainterParameters cursorPositionStylePainterParameters = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							cursorPositionStylePainterParameters.text = this.editorEngine.text.Substring(0, this.editorEngine.altCursorPosition);
							cursorPositionStylePainterParameters.wordWrapWidth = num3;
							cursorPositionStylePainterParameters.cursorIndex = this.editorEngine.altCursorPosition;
							Vector2 vector6 = this.m_TextHandle.GetCursorPosition(cursorPositionStylePainterParameters, num);
							vector6 -= vector;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector6.x, vector6.y, 1f, num2),
								color = cursorColor,
								playmodeTintColor = color
							});
						}
					}
					keyboardTextEditorEventHandler.PostDrawCursor();
				}
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x0001EDC4 File Offset: 0x0001CFC4
			internal virtual bool AcceptCharacter(char c)
			{
				return !this.isReadOnly;
			}

			// Token: 0x06000791 RID: 1937 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
			protected virtual void BuildContextualMenu(ContextualMenuPopulateEvent evt)
			{
				bool flag = ((evt != null) ? evt.target : null) is TextInputBaseField<TValueType>.TextInputBase;
				if (flag)
				{
					bool flag2 = !this.isReadOnly;
					if (flag2)
					{
						evt.menu.AppendAction("Cut", new Action<DropdownMenuAction>(this.Cut), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutCopyActionStatus), null);
					}
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutCopyActionStatus), null);
					bool flag3 = !this.isReadOnly;
					if (flag3)
					{
						evt.menu.AppendAction("Paste", new Action<DropdownMenuAction>(this.Paste), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.PasteActionStatus), null);
					}
				}
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
			private void OnDetectFocusChange()
			{
				bool flag = this.editorEngine.m_HasFocus && !this.hasFocus;
				if (flag)
				{
					this.editorEngine.OnFocus();
				}
				bool flag2 = !this.editorEngine.m_HasFocus && this.hasFocus;
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
				}
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
			private void OnCursorIndexChange()
			{
				base.IncrementVersion(VersionChangeType.Repaint);
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x0001EF08 File Offset: 0x0001D108
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.m_Text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return TextElement.MeasureVisualElementTextSize(this, text, desiredWidth, widthMode, desiredHeight, heightMode, this.m_TextHandle);
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x0001EF48 File Offset: 0x0001D148
			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				bool flag = base.elementPanel != null && base.elementPanel.contextualMenuManager != null;
				if (flag)
				{
					base.elementPanel.contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
				}
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : default(long?));
				long num2 = EventBase<ContextualMenuPopulateEvent>.TypeId();
				bool flag2 = (num.GetValueOrDefault() == num2) & (num != null);
				if (flag2)
				{
					ContextualMenuPopulateEvent contextualMenuPopulateEvent = evt as ContextualMenuPopulateEvent;
					int count = contextualMenuPopulateEvent.menu.MenuItems().Count;
					this.BuildContextualMenu(contextualMenuPopulateEvent);
					bool flag3 = count > 0 && contextualMenuPopulateEvent.menu.MenuItems().Count > count;
					if (flag3)
					{
						contextualMenuPopulateEvent.menu.InsertSeparator(null, count);
					}
				}
				else
				{
					bool flag4 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
					if (flag4)
					{
						this.SaveValueAndText();
					}
					else
					{
						bool flag5 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
						if (flag5)
						{
							KeyDownEvent keyDownEvent = evt as KeyDownEvent;
							bool flag6 = keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Escape;
							if (flag6)
							{
								this.RestoreValueAndText();
								base.parent.Focus();
							}
						}
					}
				}
				this.editorEventHandler.ExecuteDefaultActionAtTarget(evt);
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x0001F099 File Offset: 0x0001D299
			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				this.editorEventHandler.ExecuteDefaultAction(evt);
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001F0B1 File Offset: 0x0001D2B1
			bool ITextInputField.hasFocus
			{
				get
				{
					return this.hasFocus;
				}
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x0001F0B9 File Offset: 0x0001D2B9
			void ITextInputField.SyncTextEngine()
			{
				this.SyncTextEngine();
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
			bool ITextInputField.AcceptCharacter(char c)
			{
				return this.AcceptCharacter(c);
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
			string ITextInputField.CullString(string s)
			{
				return this.CullString(s);
			}

			// Token: 0x0600079B RID: 1947 RVA: 0x0001F0F9 File Offset: 0x0001D2F9
			void ITextInputField.UpdateText(string value)
			{
				this.UpdateText(value);
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001F104 File Offset: 0x0001D304
			TextEditorEngine ITextInputField.editorEngine
			{
				get
				{
					return this.editorEngine;
				}
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001F10C File Offset: 0x0001D30C
			bool ITextInputField.isDelayed
			{
				get
				{
					return this.isDelayed;
				}
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x0001F114 File Offset: 0x0001D314
			void ITextInputField.UpdateValueFromText()
			{
				this.UpdateValueFromText();
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x0001F11E File Offset: 0x0001D31E
			private void DeferGUIStyleRectSync()
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x0001F138 File Offset: 0x0001D338
			private void OnPercentResolved(GeometryChangedEvent evt)
			{
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
				GUIStyle style = this.editorEngine.style;
				int num = (int)base.resolvedStyle.marginLeft;
				int num2 = (int)base.resolvedStyle.marginTop;
				int num3 = (int)base.resolvedStyle.marginRight;
				int num4 = (int)base.resolvedStyle.marginBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, num, num2, num3, num4);
				num = (int)base.resolvedStyle.paddingLeft;
				num2 = (int)base.resolvedStyle.paddingTop;
				num3 = (int)base.resolvedStyle.paddingRight;
				num4 = (int)base.resolvedStyle.paddingBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, num, num2, num3, num4);
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
			private static void SyncGUIStyle(TextInputBaseField<TValueType>.TextInputBase textInput, GUIStyle style)
			{
				ComputedStyle computedStyle = textInput.computedStyle;
				style.alignment = computedStyle.unityTextAlign.value;
				style.wordWrap = computedStyle.whiteSpace.value == WhiteSpace.Normal;
				style.clipping = ((computedStyle.overflow.value == OverflowInternal.Visible) ? TextClipping.Overflow : TextClipping.Clip);
				bool flag = computedStyle.unityFont.value != null;
				if (flag)
				{
					style.font = computedStyle.unityFont.value;
				}
				style.fontSize = (int)computedStyle.fontSize.value.value;
				style.fontStyle = computedStyle.unityFontStyleAndWeight.value;
				int num = computedStyle.unitySliceLeft.value;
				int num2 = computedStyle.unitySliceTop.value;
				int num3 = computedStyle.unitySliceRight.value;
				int num4 = computedStyle.unitySliceBottom.value;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.border, num, num2, num3, num4);
				bool flag2 = TextInputBaseField<TValueType>.TextInputBase.IsLayoutUsingPercent(textInput);
				if (flag2)
				{
					textInput.DeferGUIStyleRectSync();
				}
				else
				{
					num = (int)computedStyle.marginLeft.value.value;
					num2 = (int)computedStyle.marginTop.value.value;
					num3 = (int)computedStyle.marginRight.value.value;
					num4 = (int)computedStyle.marginBottom.value.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, num, num2, num3, num4);
					num = (int)computedStyle.paddingLeft.value.value;
					num2 = (int)computedStyle.paddingTop.value.value;
					num3 = (int)computedStyle.paddingRight.value.value;
					num4 = (int)computedStyle.paddingBottom.value.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, num, num2, num3, num4);
				}
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x0001F42C File Offset: 0x0001D62C
			private static bool IsLayoutUsingPercent(VisualElement ve)
			{
				ComputedStyle computedStyle = ve.computedStyle;
				bool flag = computedStyle.marginLeft.value.unit == LengthUnit.Percent || computedStyle.marginTop.value.unit == LengthUnit.Percent || computedStyle.marginRight.value.unit == LengthUnit.Percent || computedStyle.marginBottom.value.unit == LengthUnit.Percent;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = computedStyle.paddingLeft.value.unit == LengthUnit.Percent || computedStyle.paddingTop.value.unit == LengthUnit.Percent || computedStyle.paddingRight.value.unit == LengthUnit.Percent || computedStyle.paddingBottom.value.unit == LengthUnit.Percent;
					flag2 = flag3;
				}
				return flag2;
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x0001F52A File Offset: 0x0001D72A
			private static void AssignRect(RectOffset rect, int left, int top, int right, int bottom)
			{
				rect.left = left;
				rect.top = top;
				rect.right = right;
				rect.bottom = bottom;
			}

			// Token: 0x04000368 RID: 872
			private string m_OriginalText;

			// Token: 0x04000371 RID: 881
			private Color m_SelectionColor = Color.clear;

			// Token: 0x04000372 RID: 882
			private Color m_CursorColor = Color.grey;

			// Token: 0x04000375 RID: 885
			private TextHandle m_TextHandle = TextHandle.New();

			// Token: 0x04000376 RID: 886
			private string m_Text;
		}
	}
}
