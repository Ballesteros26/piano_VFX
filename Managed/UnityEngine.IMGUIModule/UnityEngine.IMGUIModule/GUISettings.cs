using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x06000272 RID: 626
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A0A8 File Offset: 0x000082A8
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000A0C0 File Offset: 0x000082C0
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				this.m_DoubleClickSelectsWord = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A0CC File Offset: 0x000082CC
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				this.m_TripleClickSelectsLine = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A0F0 File Offset: 0x000082F0
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000A108 File Offset: 0x00008308
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				this.m_CursorColor = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A114 File Offset: 0x00008314
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000A149 File Offset: 0x00008349
		public float cursorFlashSpeed
		{
			get
			{
				bool flag = this.m_CursorFlashSpeed >= 0f;
				float num;
				if (flag)
				{
					num = this.m_CursorFlashSpeed;
				}
				else
				{
					num = GUISettings.Internal_GetCursorFlashSpeed();
				}
				return num;
			}
			set
			{
				this.m_CursorFlashSpeed = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A154 File Offset: 0x00008354
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000A16C File Offset: 0x0000836C
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				this.m_SelectionColor = value;
			}
		}

		// Token: 0x0400009C RID: 156
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x0400009D RID: 157
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
