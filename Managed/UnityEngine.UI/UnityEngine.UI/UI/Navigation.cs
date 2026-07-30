using System;

namespace UnityEngine.UI
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public struct Navigation : IEquatable<Navigation>
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000F62C File Offset: 0x0000D82C
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000F634 File Offset: 0x0000D834
		public Navigation.Mode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F63D File Offset: 0x0000D83D
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000F645 File Offset: 0x0000D845
		public Selectable selectOnUp
		{
			get
			{
				return this.m_SelectOnUp;
			}
			set
			{
				this.m_SelectOnUp = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000F64E File Offset: 0x0000D84E
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000F656 File Offset: 0x0000D856
		public Selectable selectOnDown
		{
			get
			{
				return this.m_SelectOnDown;
			}
			set
			{
				this.m_SelectOnDown = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000F65F File Offset: 0x0000D85F
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000F667 File Offset: 0x0000D867
		public Selectable selectOnLeft
		{
			get
			{
				return this.m_SelectOnLeft;
			}
			set
			{
				this.m_SelectOnLeft = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F670 File Offset: 0x0000D870
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000F678 File Offset: 0x0000D878
		public Selectable selectOnRight
		{
			get
			{
				return this.m_SelectOnRight;
			}
			set
			{
				this.m_SelectOnRight = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F684 File Offset: 0x0000D884
		public static Navigation defaultNavigation
		{
			get
			{
				return new Navigation
				{
					m_Mode = Navigation.Mode.Automatic
				};
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		public bool Equals(Navigation other)
		{
			return this.mode == other.mode && this.selectOnUp == other.selectOnUp && this.selectOnDown == other.selectOnDown && this.selectOnLeft == other.selectOnLeft && this.selectOnRight == other.selectOnRight;
		}

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		private Navigation.Mode m_Mode;

		// Token: 0x040000FA RID: 250
		[SerializeField]
		private Selectable m_SelectOnUp;

		// Token: 0x040000FB RID: 251
		[SerializeField]
		private Selectable m_SelectOnDown;

		// Token: 0x040000FC RID: 252
		[SerializeField]
		private Selectable m_SelectOnLeft;

		// Token: 0x040000FD RID: 253
		[SerializeField]
		private Selectable m_SelectOnRight;

		// Token: 0x0200009E RID: 158
		[Flags]
		public enum Mode
		{
			// Token: 0x040002BE RID: 702
			None = 0,
			// Token: 0x040002BF RID: 703
			Horizontal = 1,
			// Token: 0x040002C0 RID: 704
			Vertical = 2,
			// Token: 0x040002C1 RID: 705
			Automatic = 3,
			// Token: 0x040002C2 RID: 706
			Explicit = 4
		}
	}
}
