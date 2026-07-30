using System;

namespace System
{
	// Token: 0x02000256 RID: 598
	internal struct InputRecord
	{
		// Token: 0x04000F87 RID: 3975
		public short EventType;

		// Token: 0x04000F88 RID: 3976
		public bool KeyDown;

		// Token: 0x04000F89 RID: 3977
		public short RepeatCount;

		// Token: 0x04000F8A RID: 3978
		public short VirtualKeyCode;

		// Token: 0x04000F8B RID: 3979
		public short VirtualScanCode;

		// Token: 0x04000F8C RID: 3980
		public char Character;

		// Token: 0x04000F8D RID: 3981
		public int ControlKeyState;

		// Token: 0x04000F8E RID: 3982
		private int pad1;

		// Token: 0x04000F8F RID: 3983
		private bool pad2;
	}
}
