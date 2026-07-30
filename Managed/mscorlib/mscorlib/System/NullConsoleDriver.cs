using System;

namespace System
{
	// Token: 0x02000222 RID: 546
	internal class NullConsoleDriver : IConsoleDriver
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x00002194 File Offset: 0x00000394
		public ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x00002194 File Offset: 0x00000394
		public int BufferHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x00002194 File Offset: 0x00000394
		public int BufferWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool CapsLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x00002194 File Offset: 0x00000394
		public int CursorLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x00002194 File Offset: 0x00000394
		public int CursorSize
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x00002194 File Offset: 0x00000394
		public int CursorTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00002194 File Offset: 0x00000394
		public bool CursorVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00002194 File Offset: 0x00000394
		public ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool KeyAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool Initialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int LargestWindowHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00015ED5 File Offset: 0x000140D5
		public int LargestWindowWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool NumberLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000604AD File Offset: 0x0005E6AD
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x00002194 File Offset: 0x00000394
		public string Title
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x00002194 File Offset: 0x00000394
		public bool TreatControlCAsInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x00002194 File Offset: 0x00000394
		public int WindowHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x00002194 File Offset: 0x00000394
		public int WindowLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x00002194 File Offset: 0x00000394
		public int WindowTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x00002194 File Offset: 0x00000394
		public int WindowWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00002194 File Offset: 0x00000394
		public void Beep(int frequency, int duration)
		{
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00002194 File Offset: 0x00000394
		public void Clear()
		{
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00002194 File Offset: 0x00000394
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00002194 File Offset: 0x00000394
		public void Init()
		{
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0000A42E File Offset: 0x0000862E
		public string ReadLine()
		{
			return null;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000604B4 File Offset: 0x0005E6B4
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return NullConsoleDriver.EmptyConsoleKeyInfo;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00002194 File Offset: 0x00000394
		public void ResetColor()
		{
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00002194 File Offset: 0x00000394
		public void SetBufferSize(int width, int height)
		{
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00002194 File Offset: 0x00000394
		public void SetCursorPosition(int left, int top)
		{
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00002194 File Offset: 0x00000394
		public void SetWindowPosition(int left, int top)
		{
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00002194 File Offset: 0x00000394
		public void SetWindowSize(int width, int height)
		{
		}

		// Token: 0x04000CD1 RID: 3281
		private static readonly ConsoleKeyInfo EmptyConsoleKeyInfo = new ConsoleKeyInfo('\0', (ConsoleKey)0, false, false, false);
	}
}
