using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200008D RID: 141
	internal class CharEntityEncoderFallback : EncoderFallback
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x000163A1 File Offset: 0x000145A1
		internal CharEntityEncoderFallback()
		{
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000163A9 File Offset: 0x000145A9
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			if (this.fallbackBuffer == null)
			{
				this.fallbackBuffer = new CharEntityEncoderFallbackBuffer(this);
			}
			return this.fallbackBuffer;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000163C5 File Offset: 0x000145C5
		public override int MaxCharCount
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000163C9 File Offset: 0x000145C9
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x000163D1 File Offset: 0x000145D1
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
			set
			{
				this.startOffset = value;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000163DA File Offset: 0x000145DA
		internal void Reset(int[] textContentMarks, int endMarkPos)
		{
			this.textContentMarks = textContentMarks;
			this.endMarkPos = endMarkPos;
			this.curMarkPos = 0;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000163F4 File Offset: 0x000145F4
		internal bool CanReplaceAt(int index)
		{
			int num = this.curMarkPos;
			int num2 = this.startOffset + index;
			while (num < this.endMarkPos && num2 >= this.textContentMarks[num + 1])
			{
				num++;
			}
			this.curMarkPos = num;
			return (num & 1) != 0;
		}

		// Token: 0x04000305 RID: 773
		private CharEntityEncoderFallbackBuffer fallbackBuffer;

		// Token: 0x04000306 RID: 774
		private int[] textContentMarks;

		// Token: 0x04000307 RID: 775
		private int endMarkPos;

		// Token: 0x04000308 RID: 776
		private int curMarkPos;

		// Token: 0x04000309 RID: 777
		private int startOffset;
	}
}
