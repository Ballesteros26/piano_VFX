using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[AssetFileNameExtension("guiskin", new string[] { })]
	[RequiredByNativeCode]
	[ExecuteInEditMode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000A1CA File Offset: 0x000083CA
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A1F2 File Offset: 0x000083F2
		internal void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A1FC File Offset: 0x000083FC
		internal static void CleanupRoots()
		{
			GUISkin.current = null;
			GUISkin.ms_Error = null;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A20C File Offset: 0x0000840C
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000A224 File Offset: 0x00008424
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				bool flag = GUISkin.current == this;
				if (flag)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A25C File Offset: 0x0000845C
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000A274 File Offset: 0x00008474
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A288 File Offset: 0x00008488
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000A2B4 File Offset: 0x000084B4
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000A2CC File Offset: 0x000084CC
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000A2E0 File Offset: 0x000084E0
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A30C File Offset: 0x0000850C
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000A324 File Offset: 0x00008524
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A338 File Offset: 0x00008538
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000A350 File Offset: 0x00008550
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000A364 File Offset: 0x00008564
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000A37C File Offset: 0x0000857C
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000A390 File Offset: 0x00008590
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A3BC File Offset: 0x000085BC
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000A3E8 File Offset: 0x000085E8
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000A400 File Offset: 0x00008600
		internal GUIStyle horizontalSliderThumbExtent
		{
			get
			{
				return this.m_horizontalSliderThumbExtent;
			}
			set
			{
				this.m_horizontalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A414 File Offset: 0x00008614
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000A42C File Offset: 0x0000862C
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A440 File Offset: 0x00008640
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000A458 File Offset: 0x00008658
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000A46C File Offset: 0x0000866C
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000A484 File Offset: 0x00008684
		internal GUIStyle verticalSliderThumbExtent
		{
			get
			{
				return this.m_verticalSliderThumbExtent;
			}
			set
			{
				this.m_verticalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A498 File Offset: 0x00008698
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000A4B0 File Offset: 0x000086B0
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A4C4 File Offset: 0x000086C4
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000A4DC File Offset: 0x000086DC
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A4F0 File Offset: 0x000086F0
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000A508 File Offset: 0x00008708
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A51C File Offset: 0x0000871C
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000A534 File Offset: 0x00008734
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000A548 File Offset: 0x00008748
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000A560 File Offset: 0x00008760
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A574 File Offset: 0x00008774
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000A58C File Offset: 0x0000878C
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A5A0 File Offset: 0x000087A0
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A5CC File Offset: 0x000087CC
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A5F8 File Offset: 0x000087F8
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000A610 File Offset: 0x00008810
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A624 File Offset: 0x00008824
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000A63C File Offset: 0x0000883C
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000A650 File Offset: 0x00008850
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000A668 File Offset: 0x00008868
		internal static GUIStyle error
		{
			get
			{
				bool flag = GUISkin.ms_Error == null;
				if (flag)
				{
					GUISkin.ms_Error = new GUIStyle();
					GUISkin.ms_Error.name = "StyleNotFoundError";
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A6A8 File Offset: 0x000088A8
		internal void Apply()
		{
			bool flag = this.m_CustomStyles == null;
			if (flag)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A6D8 File Offset: 0x000088D8
		private void BuildStyleCache()
		{
			bool flag = this.m_box == null;
			if (flag)
			{
				this.m_box = new GUIStyle();
			}
			bool flag2 = this.m_button == null;
			if (flag2)
			{
				this.m_button = new GUIStyle();
			}
			bool flag3 = this.m_toggle == null;
			if (flag3)
			{
				this.m_toggle = new GUIStyle();
			}
			bool flag4 = this.m_label == null;
			if (flag4)
			{
				this.m_label = new GUIStyle();
			}
			bool flag5 = this.m_window == null;
			if (flag5)
			{
				this.m_window = new GUIStyle();
			}
			bool flag6 = this.m_textField == null;
			if (flag6)
			{
				this.m_textField = new GUIStyle();
			}
			bool flag7 = this.m_textArea == null;
			if (flag7)
			{
				this.m_textArea = new GUIStyle();
			}
			bool flag8 = this.m_horizontalSlider == null;
			if (flag8)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			bool flag9 = this.m_horizontalSliderThumb == null;
			if (flag9)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			bool flag10 = this.m_verticalSlider == null;
			if (flag10)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			bool flag11 = this.m_verticalSliderThumb == null;
			if (flag11)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			bool flag12 = this.m_horizontalScrollbar == null;
			if (flag12)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			bool flag13 = this.m_horizontalScrollbarThumb == null;
			if (flag13)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			bool flag14 = this.m_horizontalScrollbarLeftButton == null;
			if (flag14)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			bool flag15 = this.m_horizontalScrollbarRightButton == null;
			if (flag15)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			bool flag16 = this.m_verticalScrollbar == null;
			if (flag16)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			bool flag17 = this.m_verticalScrollbarThumb == null;
			if (flag17)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			bool flag18 = this.m_verticalScrollbarUpButton == null;
			if (flag18)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			bool flag19 = this.m_verticalScrollbarDownButton == null;
			if (flag19)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			bool flag20 = this.m_ScrollView == null;
			if (flag20)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.m_Styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.m_Styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.m_Styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.m_Styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.m_Styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.m_Styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.m_Styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.m_Styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.m_Styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.m_Styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.m_Styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.m_Styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.m_Styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.m_Styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.m_Styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.m_Styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.m_Styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.m_Styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.m_Styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.m_Styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			bool flag21 = this.m_CustomStyles != null;
			if (flag21)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					bool flag22 = this.m_CustomStyles[i] == null;
					if (!flag22)
					{
						this.m_Styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			bool flag23 = !this.m_Styles.TryGetValue("HorizontalSliderThumbExtent", ref this.m_horizontalSliderThumbExtent);
			if (flag23)
			{
				this.m_horizontalSliderThumbExtent = new GUIStyle();
				this.m_horizontalSliderThumbExtent.name = "horizontalsliderthumbextent";
				this.m_Styles["HorizontalSliderThumbExtent"] = this.m_horizontalSliderThumbExtent;
			}
			bool flag24 = !this.m_Styles.TryGetValue("VerticalSliderThumbExtent", ref this.m_verticalSliderThumbExtent);
			if (flag24)
			{
				this.m_verticalSliderThumbExtent = new GUIStyle();
				this.m_Styles["VerticalSliderThumbExtent"] = this.m_verticalSliderThumbExtent;
				this.m_verticalSliderThumbExtent.name = "verticalsliderthumbextent";
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AD48 File Offset: 0x00008F48
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			bool flag = guistyle != null;
			GUIStyle guistyle2;
			if (flag)
			{
				guistyle2 = guistyle;
			}
			else
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to find style '",
					styleName,
					"' in skin '",
					base.name,
					"' ",
					(Event.current != null) ? Event.current.type.ToString() : "<called outside OnGUI>"
				}));
				guistyle2 = GUISkin.error;
			}
			return guistyle2;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public GUIStyle FindStyle(string styleName)
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			GUIStyle guistyle;
			bool flag2 = this.m_Styles.TryGetValue(styleName, ref guistyle);
			GUIStyle guistyle2;
			if (flag2)
			{
				guistyle2 = guistyle;
			}
			else
			{
				guistyle2 = null;
			}
			return guistyle2;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AE14 File Offset: 0x00009014
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			bool flag = GUISkin.m_SkinChanged != null;
			if (flag)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AE4C File Offset: 0x0000904C
		public IEnumerator GetEnumerator()
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			return this.m_Styles.Values.GetEnumerator();
		}

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private Font m_Font;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x040000AF RID: 175
		[NonSerialized]
		private GUIStyle m_horizontalSliderThumbExtent;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x040000B2 RID: 178
		[NonSerialized]
		private GUIStyle m_verticalSliderThumbExtent;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x040000BE RID: 190
		internal static GUIStyle ms_Error;

		// Token: 0x040000BF RID: 191
		private Dictionary<string, GUIStyle> m_Styles = null;

		// Token: 0x040000C0 RID: 192
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x040000C1 RID: 193
		internal static GUISkin current;

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x060002BA RID: 698
		internal delegate void SkinChangedDelegate();
	}
}
