using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("IMGUIScriptingClasses.h")]
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyle
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002CF RID: 719
		// (set) Token: 0x060002D0 RID: 720
		[NativeProperty("Name", false, TargetType.Function)]
		internal extern string rawName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002D1 RID: 721
		// (set) Token: 0x060002D2 RID: 722
		[NativeProperty("Font", false, TargetType.Function)]
		public extern Font font
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002D3 RID: 723
		// (set) Token: 0x060002D4 RID: 724
		[NativeProperty("m_ImagePosition", false, TargetType.Field)]
		public extern ImagePosition imagePosition
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002D5 RID: 725
		// (set) Token: 0x060002D6 RID: 726
		[NativeProperty("m_Alignment", false, TargetType.Field)]
		public extern TextAnchor alignment
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D8 RID: 728
		[NativeProperty("m_WordWrap", false, TargetType.Field)]
		public extern bool wordWrap
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002D9 RID: 729
		// (set) Token: 0x060002DA RID: 730
		[NativeProperty("m_Clipping", false, TargetType.Field)]
		public extern TextClipping clipping
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000AFF4 File Offset: 0x000091F4
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000B00A File Offset: 0x0000920A
		[NativeProperty("m_ContentOffset", false, TargetType.Field)]
		public Vector2 contentOffset
		{
			get
			{
				Vector2 vector;
				this.get_contentOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_contentOffset_Injected(ref value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002DD RID: 733
		// (set) Token: 0x060002DE RID: 734
		[NativeProperty("m_FixedWidth", false, TargetType.Field)]
		public extern float fixedWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002DF RID: 735
		// (set) Token: 0x060002E0 RID: 736
		[NativeProperty("m_FixedHeight", false, TargetType.Field)]
		public extern float fixedHeight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002E1 RID: 737
		// (set) Token: 0x060002E2 RID: 738
		[NativeProperty("m_StretchWidth", false, TargetType.Field)]
		public extern bool stretchWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002E3 RID: 739
		// (set) Token: 0x060002E4 RID: 740
		[NativeProperty("m_StretchHeight", false, TargetType.Field)]
		public extern bool stretchHeight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002E5 RID: 741
		// (set) Token: 0x060002E6 RID: 742
		[NativeProperty("m_FontSize", false, TargetType.Field)]
		public extern int fontSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002E7 RID: 743
		// (set) Token: 0x060002E8 RID: 744
		[NativeProperty("m_FontStyle", false, TargetType.Field)]
		public extern FontStyle fontStyle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002E9 RID: 745
		// (set) Token: 0x060002EA RID: 746
		[NativeProperty("m_RichText", false, TargetType.Field)]
		public extern bool richText
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B014 File Offset: 0x00009214
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000B02A File Offset: 0x0000922A
		[Obsolete("Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.", false)]
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		public Vector2 clipOffset
		{
			get
			{
				Vector2 vector;
				this.get_clipOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_clipOffset_Injected(ref value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B034 File Offset: 0x00009234
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000B04A File Offset: 0x0000924A
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		internal Vector2 Internal_clipOffset
		{
			get
			{
				Vector2 vector;
				this.get_Internal_clipOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_Internal_clipOffset_Injected(ref value);
			}
		}

		// Token: 0x060002EF RID: 751
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create(GUIStyle self);

		// Token: 0x060002F0 RID: 752
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Copy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Copy(GUIStyle self, GUIStyle other);

		// Token: 0x060002F1 RID: 753
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr self);

		// Token: 0x060002F2 RID: 754
		[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x060002F3 RID: 755
		[FreeFunction(Name = "GUIStyle_Bindings::AssignStyleState", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void AssignStyleState(int idx, IntPtr srcStyleState);

		// Token: 0x060002F4 RID: 756
		[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x060002F5 RID: 757
		[FreeFunction(Name = "GUIStyle_Bindings::AssignRectOffset", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

		// Token: 0x060002F6 RID: 758
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetLineHeight")]
		[MethodImpl(4096)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B054 File Offset: 0x00009254
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
		private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Internal_Draw_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B066 File Offset: 0x00009266
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
		private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Internal_Draw2_Injected(ref position, content, controlID, on);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B074 File Offset: 0x00009274
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawCursor", HasExplicitThis = true)]
		private void Internal_DrawCursor(Rect position, GUIContent content, int pos, Color cursorColor)
		{
			this.Internal_DrawCursor_Injected(ref position, content, pos, ref cursorColor);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B084 File Offset: 0x00009284
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawWithTextSelection", HasExplicitThis = true)]
		private void Internal_DrawWithTextSelection(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor)
		{
			this.Internal_DrawWithTextSelection_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, drawSelectionAsComposition, cursorFirst, cursorLast, ref cursorColor, ref selectionColor);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B0AC File Offset: 0x000092AC
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorPixelPosition", HasExplicitThis = true)]
		internal Vector2 Internal_GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 vector;
			this.Internal_GetCursorPixelPosition_Injected(ref position, content, cursorStringIndex, out vector);
			return vector;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B0C6 File Offset: 0x000092C6
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorStringIndex", HasExplicitThis = true)]
		internal int Internal_GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex_Injected(ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B0D3 File Offset: 0x000092D3
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetSelectedRenderedText", HasExplicitThis = true)]
		internal string Internal_GetSelectedRenderedText(Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex)
		{
			return this.Internal_GetSelectedRenderedText_Injected(ref localPosition, mContent, selectIndex, cursorIndex);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B0E1 File Offset: 0x000092E1
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetHyperlinksRect", HasExplicitThis = true)]
		internal Rect[] Internal_GetHyperlinksRect(Rect localPosition, GUIContent mContent)
		{
			return this.Internal_GetHyperlinksRect_Injected(ref localPosition, mContent);
		}

		// Token: 0x060002FF RID: 767
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetNumCharactersThatFitWithinWidth", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern int Internal_GetNumCharactersThatFitWithinWidth(string text, float width);

		// Token: 0x06000300 RID: 768 RVA: 0x0000B0EC File Offset: 0x000092EC
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSize(GUIContent content)
		{
			Vector2 vector;
			this.Internal_CalcSize_Injected(content, out vector);
			return vector;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B104 File Offset: 0x00009304
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSizeWithConstraints", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSizeWithConstraints(GUIContent content, Vector2 maxSize)
		{
			Vector2 vector;
			this.Internal_CalcSizeWithConstraints_Injected(content, ref maxSize, out vector);
			return vector;
		}

		// Token: 0x06000302 RID: 770
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcHeight", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern float Internal_CalcHeight(GUIContent content, float width);

		// Token: 0x06000303 RID: 771 RVA: 0x0000B120 File Offset: 0x00009320
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcMinMaxWidth", HasExplicitThis = true)]
		private Vector2 Internal_CalcMinMaxWidth(GUIContent content)
		{
			Vector2 vector;
			this.Internal_CalcMinMaxWidth_Injected(content, out vector);
			return vector;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B137 File Offset: 0x00009337
		[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
		internal static void SetMouseTooltip(string tooltip, Rect screenRect)
		{
			GUIStyle.SetMouseTooltip_Injected(tooltip, ref screenRect);
		}

		// Token: 0x06000305 RID: 773
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorFlashOffset")]
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x06000306 RID: 774
		[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
		[MethodImpl(4096)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x06000307 RID: 775 RVA: 0x0000B141 File Offset: 0x00009341
		public GUIStyle()
		{
			this.m_Ptr = GUIStyle.Internal_Create(this);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B158 File Offset: 0x00009358
		public GUIStyle(GUIStyle other)
		{
			bool flag = other == null;
			if (flag)
			{
				Debug.LogError("Copied style is null. Using StyleNotFound instead.");
				other = GUISkin.error;
			}
			this.m_Ptr = GUIStyle.Internal_Copy(this, other);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B198 File Offset: 0x00009398
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					GUIStyle.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B1F0 File Offset: 0x000093F0
		internal static void CleanupRoots()
		{
			GUIStyle.s_None = null;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B1FC File Offset: 0x000093FC
		internal void InternalOnAfterDeserialize()
		{
			this.m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(0));
			this.m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(1));
			this.m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(2));
			this.m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(3));
			this.m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(4));
			this.m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(5));
			this.m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(6));
			this.m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(7));
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000B2A4 File Offset: 0x000094A4
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000B2CF File Offset: 0x000094CF
		public string name
		{
			get
			{
				string text;
				if ((text = this.m_Name) == null)
				{
					text = (this.m_Name = this.rawName);
				}
				return text;
			}
			set
			{
				this.m_Name = value;
				this.rawName = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000B2E4 File Offset: 0x000094E4
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000B316 File Offset: 0x00009516
		public GUIStyleState normal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Normal) == null)
				{
					guistyleState = (this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000B328 File Offset: 0x00009528
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000B35A File Offset: 0x0000955A
		public GUIStyleState hover
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Hover) == null)
				{
					guistyleState = (this.m_Hover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(1)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000B36C File Offset: 0x0000956C
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000B39E File Offset: 0x0000959E
		public GUIStyleState active
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Active) == null)
				{
					guistyleState = (this.m_Active = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(2)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000B3B0 File Offset: 0x000095B0
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000B3E2 File Offset: 0x000095E2
		public GUIStyleState onNormal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnNormal) == null)
				{
					guistyleState = (this.m_OnNormal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(4)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B3F4 File Offset: 0x000095F4
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000B426 File Offset: 0x00009626
		public GUIStyleState onHover
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnHover) == null)
				{
					guistyleState = (this.m_OnHover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(5)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000B438 File Offset: 0x00009638
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000B46A File Offset: 0x0000966A
		public GUIStyleState onActive
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnActive) == null)
				{
					guistyleState = (this.m_OnActive = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(6)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000B47C File Offset: 0x0000967C
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000B4AE File Offset: 0x000096AE
		public GUIStyleState focused
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Focused) == null)
				{
					guistyleState = (this.m_Focused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(3)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B4C0 File Offset: 0x000096C0
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000B4F2 File Offset: 0x000096F2
		public GUIStyleState onFocused
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnFocused) == null)
				{
					guistyleState = (this.m_OnFocused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(7)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B504 File Offset: 0x00009704
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000B536 File Offset: 0x00009736
		public RectOffset border
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Border) == null)
				{
					rectOffset = (this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B548 File Offset: 0x00009748
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000B57A File Offset: 0x0000977A
		public RectOffset margin
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Margin) == null)
				{
					rectOffset = (this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B58C File Offset: 0x0000978C
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000B5BE File Offset: 0x000097BE
		public RectOffset padding
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Padding) == null)
				{
					rectOffset = (this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B5D0 File Offset: 0x000097D0
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000B602 File Offset: 0x00009802
		public RectOffset overflow
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Overflow) == null)
				{
					rectOffset = (this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B613 File Offset: 0x00009813
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B625 File Offset: 0x00009825
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.none, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000B63C File Offset: 0x0000983C
		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(text), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B655 File Offset: 0x00009855
		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(image), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B66E File Offset: 0x0000986E
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B682 File Offset: 0x00009882
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			this.Draw(position, content, controlID, false, false, false, false);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B693 File Offset: 0x00009893
		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Draw(position, content, controlID, false, false, on, false);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B6A5 File Offset: 0x000098A5
		public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
		{
			this.Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B6C4 File Offset: 0x000098C4
		private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			bool flag = controlId == -1;
			if (flag)
			{
				this.Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
			}
			else
			{
				this.Internal_Draw2(position, content, controlId, on);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B6FC File Offset: 0x000098FC
		public void DrawCursor(Rect position, GUIContent content, int controlID, int character)
		{
			Event current = Event.current;
			bool flag = current.type == EventType.Repaint;
			if (flag)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				bool flag2 = cursorFlashSpeed == 0f || num < 0.5f;
				if (flag2)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				this.Internal_DrawCursor(position, content, character, cursorColor);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B794 File Offset: 0x00009994
		internal void DrawWithTextSelection(Rect position, GUIContent content, bool isActive, bool hasKeyboardFocus, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition, Color selectionColor)
		{
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			bool flag = cursorFlashSpeed == 0f || num < 0.5f;
			if (flag)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			bool flag2 = position.Contains(Event.current.mousePosition);
			this.Internal_DrawWithTextSelection(position, content, flag2, isActive, false, hasKeyboardFocus, drawSelectionAsComposition, firstSelectedCharacter, lastSelectedCharacter, cursorColor, selectionColor);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B834 File Offset: 0x00009A34
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			this.DrawWithTextSelection(position, content, controlID == GUIUtility.hotControl, controlID == GUIUtility.keyboardControl && GUIStyle.showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition, GUI.skin.settings.selectionColor);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B878 File Offset: 0x00009A78
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B88C File Offset: 0x00009A8C
		public static implicit operator GUIStyle(string str)
		{
			bool flag = GUISkin.current == null;
			GUIStyle guistyle;
			if (flag)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				guistyle = GUISkin.error;
			}
			else
			{
				guistyle = GUISkin.current.GetStyle(str);
			}
			return guistyle;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public static GUIStyle none
		{
			get
			{
				GUIStyle guistyle;
				if ((guistyle = GUIStyle.s_None) == null)
				{
					guistyle = (GUIStyle.s_None = new GUIStyle());
				}
				return guistyle;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			return this.Internal_GetCursorPixelPosition(position, content, cursorStringIndex);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B900 File Offset: 0x00009B00
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex(position, content, cursorPixelPosition);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B91C File Offset: 0x00009B1C
		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return this.Internal_GetNumCharactersThatFitWithinWidth(text, width);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B938 File Offset: 0x00009B38
		public Vector2 CalcSize(GUIContent content)
		{
			return this.Internal_CalcSize(content);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B954 File Offset: 0x00009B54
		internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
		{
			return this.Internal_CalcSizeWithConstraints(content, constraints);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B970 File Offset: 0x00009B70
		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth != 0f) ? this.fixedWidth : Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right), (this.fixedHeight != 0f) ? this.fixedHeight : Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public float CalcHeight(GUIContent content, float width)
		{
			return this.Internal_CalcHeight(content, width);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000BA16 File Offset: 0x00009C16
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BA40 File Offset: 0x00009C40
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			Vector2 vector = this.Internal_CalcMinMaxWidth(content);
			minWidth = vector.x;
			maxWidth = vector.y;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BA68 File Offset: 0x00009C68
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[] { this.name });
		}

		// Token: 0x06000340 RID: 832
		[MethodImpl(4096)]
		private extern void get_contentOffset_Injected(out Vector2 ret);

		// Token: 0x06000341 RID: 833
		[MethodImpl(4096)]
		private extern void set_contentOffset_Injected(ref Vector2 value);

		// Token: 0x06000342 RID: 834
		[MethodImpl(4096)]
		private extern void get_clipOffset_Injected(out Vector2 ret);

		// Token: 0x06000343 RID: 835
		[MethodImpl(4096)]
		private extern void set_clipOffset_Injected(ref Vector2 value);

		// Token: 0x06000344 RID: 836
		[MethodImpl(4096)]
		private extern void get_Internal_clipOffset_Injected(out Vector2 ret);

		// Token: 0x06000345 RID: 837
		[MethodImpl(4096)]
		private extern void set_Internal_clipOffset_Injected(ref Vector2 value);

		// Token: 0x06000346 RID: 838
		[MethodImpl(4096)]
		private extern void Internal_Draw_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		// Token: 0x06000347 RID: 839
		[MethodImpl(4096)]
		private extern void Internal_Draw2_Injected(ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x06000348 RID: 840
		[MethodImpl(4096)]
		private extern void Internal_DrawCursor_Injected(ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x06000349 RID: 841
		[MethodImpl(4096)]
		private extern void Internal_DrawWithTextSelection_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, ref Color cursorColor, ref Color selectionColor);

		// Token: 0x0600034A RID: 842
		[MethodImpl(4096)]
		private extern void Internal_GetCursorPixelPosition_Injected(ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x0600034B RID: 843
		[MethodImpl(4096)]
		private extern int Internal_GetCursorStringIndex_Injected(ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x0600034C RID: 844
		[MethodImpl(4096)]
		private extern string Internal_GetSelectedRenderedText_Injected(ref Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex);

		// Token: 0x0600034D RID: 845
		[MethodImpl(4096)]
		private extern Rect[] Internal_GetHyperlinksRect_Injected(ref Rect localPosition, GUIContent mContent);

		// Token: 0x0600034E RID: 846
		[MethodImpl(4096)]
		private extern void Internal_CalcSize_Injected(GUIContent content, out Vector2 ret);

		// Token: 0x0600034F RID: 847
		[MethodImpl(4096)]
		private extern void Internal_CalcSizeWithConstraints_Injected(GUIContent content, ref Vector2 maxSize, out Vector2 ret);

		// Token: 0x06000350 RID: 848
		[MethodImpl(4096)]
		private extern void Internal_CalcMinMaxWidth_Injected(GUIContent content, out Vector2 ret);

		// Token: 0x06000351 RID: 849
		[MethodImpl(4096)]
		private static extern void SetMouseTooltip_Injected(string tooltip, ref Rect screenRect);

		// Token: 0x040000C5 RID: 197
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000C6 RID: 198
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x040000C7 RID: 199
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x040000C8 RID: 200
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x040000C9 RID: 201
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x040000CA RID: 202
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x040000CB RID: 203
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x040000CC RID: 204
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x040000CD RID: 205
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x040000D0 RID: 208
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x040000D1 RID: 209
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x040000D2 RID: 210
		[NonSerialized]
		private string m_Name;

		// Token: 0x040000D3 RID: 211
		internal static bool showKeyboardFocus = true;

		// Token: 0x040000D4 RID: 212
		private static GUIStyle s_None;
	}
}
