using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyleState
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002C2 RID: 706
		// (set) Token: 0x060002C3 RID: 707
		[NativeProperty("Background", false, TargetType.Function)]
		public extern Texture2D background
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000AF20 File Offset: 0x00009120
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000AF36 File Offset: 0x00009136
		[NativeProperty("textColor", false, TargetType.Field)]
		public Color textColor
		{
			get
			{
				Color color;
				this.get_textColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_textColor_Injected(ref value);
			}
		}

		// Token: 0x060002C6 RID: 710
		[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Init();

		// Token: 0x060002C7 RID: 711
		[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AF40 File Offset: 0x00009140
		public GUIStyleState()
		{
			this.m_Ptr = GUIStyleState.Init();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AF55 File Offset: 0x00009155
		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000AF70 File Offset: 0x00009170
		internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AF8C File Offset: 0x0000918C
		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AFA8 File Offset: 0x000091A8
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Cleanup();
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060002CD RID: 717
		[MethodImpl(4096)]
		private extern void get_textColor_Injected(out Color ret);

		// Token: 0x060002CE RID: 718
		[MethodImpl(4096)]
		private extern void set_textColor_Injected(ref Color value);

		// Token: 0x040000C3 RID: 195
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000C4 RID: 196
		private readonly GUIStyle m_SourceStyle;
	}
}
