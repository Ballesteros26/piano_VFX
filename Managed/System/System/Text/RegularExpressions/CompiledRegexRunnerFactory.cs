using System;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200015D RID: 349
	internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x000387C2 File Offset: 0x000369C2
		internal CompiledRegexRunnerFactory(DynamicMethod go, DynamicMethod firstChar, DynamicMethod trackCount)
		{
			this.goMethod = go;
			this.findFirstCharMethod = firstChar;
			this.initTrackCountMethod = trackCount;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000387E0 File Offset: 0x000369E0
		protected internal override RegexRunner CreateInstance()
		{
			CompiledRegexRunner compiledRegexRunner = new CompiledRegexRunner();
			compiledRegexRunner.SetDelegates((NoParamDelegate)this.goMethod.CreateDelegate(typeof(NoParamDelegate)), (FindFirstCharDelegate)this.findFirstCharMethod.CreateDelegate(typeof(FindFirstCharDelegate)), (NoParamDelegate)this.initTrackCountMethod.CreateDelegate(typeof(NoParamDelegate)));
			return compiledRegexRunner;
		}

		// Token: 0x04000F68 RID: 3944
		private DynamicMethod goMethod;

		// Token: 0x04000F69 RID: 3945
		private DynamicMethod findFirstCharMethod;

		// Token: 0x04000F6A RID: 3946
		private DynamicMethod initTrackCountMethod;
	}
}
