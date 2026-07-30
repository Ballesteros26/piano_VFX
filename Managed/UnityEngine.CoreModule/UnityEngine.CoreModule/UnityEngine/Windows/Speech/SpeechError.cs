using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200022D RID: 557
	public enum SpeechError
	{
		// Token: 0x04000775 RID: 1909
		NoError,
		// Token: 0x04000776 RID: 1910
		TopicLanguageNotSupported,
		// Token: 0x04000777 RID: 1911
		GrammarLanguageMismatch,
		// Token: 0x04000778 RID: 1912
		GrammarCompilationFailure,
		// Token: 0x04000779 RID: 1913
		AudioQualityFailure,
		// Token: 0x0400077A RID: 1914
		PauseLimitExceeded,
		// Token: 0x0400077B RID: 1915
		TimeoutExceeded,
		// Token: 0x0400077C RID: 1916
		NetworkFailure,
		// Token: 0x0400077D RID: 1917
		MicrophoneUnavailable,
		// Token: 0x0400077E RID: 1918
		UnknownError
	}
}
