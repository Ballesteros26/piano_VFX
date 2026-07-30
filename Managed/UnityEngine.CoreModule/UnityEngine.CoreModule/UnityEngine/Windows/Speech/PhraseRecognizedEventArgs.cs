using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000231 RID: 561
	public struct PhraseRecognizedEventArgs
	{
		// Token: 0x06001889 RID: 6281 RVA: 0x000277DE File Offset: 0x000259DE
		internal PhraseRecognizedEventArgs(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, DateTime phraseStartTime, TimeSpan phraseDuration)
		{
			this.text = text;
			this.confidence = confidence;
			this.semanticMeanings = semanticMeanings;
			this.phraseStartTime = phraseStartTime;
			this.phraseDuration = phraseDuration;
		}

		// Token: 0x0400078E RID: 1934
		public readonly ConfidenceLevel confidence;

		// Token: 0x0400078F RID: 1935
		public readonly SemanticMeaning[] semanticMeanings;

		// Token: 0x04000790 RID: 1936
		public readonly string text;

		// Token: 0x04000791 RID: 1937
		public readonly DateTime phraseStartTime;

		// Token: 0x04000792 RID: 1938
		public readonly TimeSpan phraseDuration;
	}
}
