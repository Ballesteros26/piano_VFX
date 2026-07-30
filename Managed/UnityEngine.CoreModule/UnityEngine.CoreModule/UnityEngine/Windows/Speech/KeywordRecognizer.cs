using System;
using System.Collections.Generic;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000232 RID: 562
	public sealed class KeywordRecognizer : PhraseRecognizer
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00027806 File Offset: 0x00025A06
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0002780E File Offset: 0x00025A0E
		public IEnumerable<string> Keywords { get; private set; }

		// Token: 0x0600188C RID: 6284 RVA: 0x00027817 File Offset: 0x00025A17
		public KeywordRecognizer(string[] keywords)
			: this(keywords, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00027824 File Offset: 0x00025A24
		public KeywordRecognizer(string[] keywords, ConfidenceLevel minimumConfidence)
		{
			bool flag = keywords == null;
			if (flag)
			{
				throw new ArgumentNullException("keywords");
			}
			bool flag2 = keywords.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("At least one keyword must be specified.", "keywords");
			}
			int num = keywords.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag3 = keywords[i] == null;
				if (flag3)
				{
					throw new ArgumentNullException(string.Format("Keyword at index {0} is null.", i));
				}
			}
			this.Keywords = keywords;
			this.m_Recognizer = PhraseRecognizer.CreateFromKeywords(this, keywords, minimumConfidence);
		}
	}
}
