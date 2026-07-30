using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200015A RID: 346
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00038779 File Offset: 0x00036979
		internal CompiledRegexRunner()
		{
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00038781 File Offset: 0x00036981
		internal void SetDelegates(NoParamDelegate go, FindFirstCharDelegate firstChar, NoParamDelegate trackCount)
		{
			this.goMethod = go;
			this.findFirstCharMethod = firstChar;
			this.initTrackCountMethod = trackCount;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00038798 File Offset: 0x00036998
		protected override void Go()
		{
			this.goMethod(this);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000387A6 File Offset: 0x000369A6
		protected override bool FindFirstChar()
		{
			return this.findFirstCharMethod(this);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000387B4 File Offset: 0x000369B4
		protected override void InitTrackCount()
		{
			this.initTrackCountMethod(this);
		}

		// Token: 0x04000F65 RID: 3941
		private NoParamDelegate goMethod;

		// Token: 0x04000F66 RID: 3942
		private FindFirstCharDelegate findFirstCharMethod;

		// Token: 0x04000F67 RID: 3943
		private NoParamDelegate initTrackCountMethod;
	}
}
