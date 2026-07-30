using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200022F RID: 559
	public enum DictationCompletionCause
	{
		// Token: 0x04000784 RID: 1924
		Complete,
		// Token: 0x04000785 RID: 1925
		AudioQualityFailure,
		// Token: 0x04000786 RID: 1926
		Canceled,
		// Token: 0x04000787 RID: 1927
		TimeoutExceeded,
		// Token: 0x04000788 RID: 1928
		PauseLimitExceeded,
		// Token: 0x04000789 RID: 1929
		NetworkFailure,
		// Token: 0x0400078A RID: 1930
		MicrophoneUnavailable,
		// Token: 0x0400078B RID: 1931
		UnknownError
	}
}
