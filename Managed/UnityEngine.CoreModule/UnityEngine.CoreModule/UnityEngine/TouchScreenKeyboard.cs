using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001EF RID: 495
	[NativeConditional("ENABLE_ONSCREEN_KEYBOARD")]
	[NativeHeader("Runtime/Export/TouchScreenKeyboard/TouchScreenKeyboard.bindings.h")]
	[NativeHeader("Runtime/Input/KeyboardOnScreen.h")]
	public class TouchScreenKeyboard
	{
		// Token: 0x060015EC RID: 5612
		[FreeFunction("TouchScreenKeyboard_Destroy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x060015ED RID: 5613 RVA: 0x00023DC4 File Offset: 0x00021FC4
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TouchScreenKeyboard.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00023E08 File Offset: 0x00022008
		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00023E38 File Offset: 0x00022038
		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int characterLimit)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments touchScreenKeyboard_InternalConstructorHelperArguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			touchScreenKeyboard_InternalConstructorHelperArguments.keyboardType = Convert.ToUInt32(keyboardType);
			touchScreenKeyboard_InternalConstructorHelperArguments.autocorrection = Convert.ToUInt32(autocorrection);
			touchScreenKeyboard_InternalConstructorHelperArguments.multiline = Convert.ToUInt32(multiline);
			touchScreenKeyboard_InternalConstructorHelperArguments.secure = Convert.ToUInt32(secure);
			touchScreenKeyboard_InternalConstructorHelperArguments.alert = Convert.ToUInt32(alert);
			touchScreenKeyboard_InternalConstructorHelperArguments.characterLimit = characterLimit;
			this.m_Ptr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper(ref touchScreenKeyboard_InternalConstructorHelperArguments, text, textPlaceholder);
		}

		// Token: 0x060015F0 RID: 5616
		[FreeFunction("TouchScreenKeyboard_InternalConstructorHelper")]
		[MethodImpl(4096)]
		private static extern IntPtr TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder);

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00023EB8 File Offset: 0x000220B8
		public static bool isSupported
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				if (runtimePlatform <= RuntimePlatform.MetroPlayerARM)
				{
					if (runtimePlatform != RuntimePlatform.IPhonePlayer && runtimePlatform != RuntimePlatform.Android && runtimePlatform - RuntimePlatform.MetroPlayerX86 > 2)
					{
						goto IL_003F;
					}
				}
				else if (runtimePlatform != RuntimePlatform.PS4 && runtimePlatform - RuntimePlatform.tvOS > 1 && runtimePlatform != RuntimePlatform.Stadia)
				{
					goto IL_003F;
				}
				return true;
				IL_003F:
				return false;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00023F0C File Offset: 0x0002210C
		public static bool isInPlaceEditingAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00023F20 File Offset: 0x00022120
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00023F44 File Offset: 0x00022144
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			int num = 0;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00023F68 File Offset: 0x00022168
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			int num = 0;
			string text2 = "";
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, text2, num);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00023F94 File Offset: 0x00022194
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, flag, text2, num);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00023FC0 File Offset: 0x000221C0
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, flag2, flag, text2, num);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00023FF0 File Offset: 0x000221F0
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, flag3, flag2, flag, text2, num);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00024024 File Offset: 0x00022224
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			return TouchScreenKeyboard.Open(text, keyboardType, flag4, flag3, flag2, flag, text2, num);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0002405C File Offset: 0x0002225C
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			TouchScreenKeyboardType touchScreenKeyboardType = TouchScreenKeyboardType.Default;
			return TouchScreenKeyboard.Open(text, touchScreenKeyboardType, flag4, flag3, flag2, flag, text2, num);
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060015FB RID: 5627
		// (set) Token: 0x060015FC RID: 5628
		public extern string text
		{
			[NativeName("GetText")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetText")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060015FD RID: 5629
		// (set) Token: 0x060015FE RID: 5630
		public static extern bool hideInput
		{
			[NativeName("IsInputHidden")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetInputHidden")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060015FF RID: 5631
		// (set) Token: 0x06001600 RID: 5632
		public extern bool active
		{
			[NativeName("IsActive")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetActive")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001601 RID: 5633
		[FreeFunction("TouchScreenKeyboard_GetDone")]
		[MethodImpl(4096)]
		private static extern bool GetDone(IntPtr ptr);

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00024098 File Offset: 0x00022298
		[Obsolete("Property done is deprecated, use status instead")]
		public bool done
		{
			get
			{
				return TouchScreenKeyboard.GetDone(this.m_Ptr);
			}
		}

		// Token: 0x06001603 RID: 5635
		[FreeFunction("TouchScreenKeyboard_GetWasCanceled")]
		[MethodImpl(4096)]
		private static extern bool GetWasCanceled(IntPtr ptr);

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x000240B8 File Offset: 0x000222B8
		[Obsolete("Property wasCanceled is deprecated, use status instead.")]
		public bool wasCanceled
		{
			get
			{
				return TouchScreenKeyboard.GetWasCanceled(this.m_Ptr);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001605 RID: 5637
		public extern TouchScreenKeyboard.Status status
		{
			[NativeName("GetKeyboardStatus")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001606 RID: 5638
		// (set) Token: 0x06001607 RID: 5639
		public extern int characterLimit
		{
			[NativeName("GetCharacterLimit")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetCharacterLimit")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001608 RID: 5640
		public extern bool canGetSelection
		{
			[NativeName("CanGetSelection")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001609 RID: 5641
		public extern bool canSetSelection
		{
			[NativeName("CanSetSelection")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x000240D8 File Offset: 0x000222D8
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x00024100 File Offset: 0x00022300
		public RangeInt selection
		{
			get
			{
				RangeInt rangeInt;
				TouchScreenKeyboard.GetSelection(out rangeInt.start, out rangeInt.length);
				return rangeInt;
			}
			set
			{
				bool flag = value.start < 0 || value.length < 0 || value.start + value.length > this.text.Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("selection", "Selection is out of range.");
				}
				TouchScreenKeyboard.SetSelection(value.start, value.length);
			}
		}

		// Token: 0x0600160C RID: 5644
		[MethodImpl(4096)]
		private static extern void GetSelection(out int start, out int length);

		// Token: 0x0600160D RID: 5645
		[MethodImpl(4096)]
		private static extern void SetSelection(int start, int length);

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600160E RID: 5646
		public extern TouchScreenKeyboardType type
		{
			[NativeName("GetKeyboardType")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00024164 File Offset: 0x00022364
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00002EC3 File Offset: 0x000010C3
		public int targetDisplay
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00024178 File Offset: 0x00022378
		[NativeConditional("ENABLE_ONSCREEN_KEYBOARD", "RectT<float>()")]
		public static Rect area
		{
			[NativeName("GetRect")]
			get
			{
				Rect rect;
				TouchScreenKeyboard.get_area_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001612 RID: 5650
		public static extern bool visible
		{
			[NativeName("IsVisible")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001613 RID: 5651
		[MethodImpl(4096)]
		private static extern void get_area_Injected(out Rect ret);

		// Token: 0x040006C6 RID: 1734
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x020001F0 RID: 496
		public enum Status
		{
			// Token: 0x040006C8 RID: 1736
			Visible,
			// Token: 0x040006C9 RID: 1737
			Done,
			// Token: 0x040006CA RID: 1738
			Canceled,
			// Token: 0x040006CB RID: 1739
			LostFocus
		}
	}
}
