using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000233 RID: 563
	public sealed class GrammarRecognizer : PhraseRecognizer
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x000278B5 File Offset: 0x00025AB5
		// (set) Token: 0x0600188F RID: 6287 RVA: 0x000278BD File Offset: 0x00025ABD
		public string GrammarFilePath { get; private set; }

		// Token: 0x06001890 RID: 6288 RVA: 0x000278C6 File Offset: 0x00025AC6
		public GrammarRecognizer(string grammarFilePath)
			: this(grammarFilePath, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000278D4 File Offset: 0x00025AD4
		public GrammarRecognizer(string grammarFilePath, ConfidenceLevel minimumConfidence)
		{
			bool flag = grammarFilePath == null;
			if (flag)
			{
				throw new ArgumentNullException("grammarFilePath");
			}
			bool flag2 = grammarFilePath.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Grammar file path cannot be empty.");
			}
			this.GrammarFilePath = grammarFilePath;
			this.m_Recognizer = PhraseRecognizer.CreateFromGrammarFile(this, grammarFilePath, minimumConfidence);
		}
	}
}
