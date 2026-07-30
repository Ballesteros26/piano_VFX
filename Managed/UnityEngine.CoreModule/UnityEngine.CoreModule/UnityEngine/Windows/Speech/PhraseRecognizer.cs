using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000224 RID: 548
	public abstract class PhraseRecognizer : IDisposable
	{
		// Token: 0x06001841 RID: 6209
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		protected static extern IntPtr CreateFromKeywords(object self, string[] keywords, ConfidenceLevel minimumConfidence);

		// Token: 0x06001842 RID: 6210
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(4096)]
		protected static extern IntPtr CreateFromGrammarFile(object self, string grammarFilePath, ConfidenceLevel minimumConfidence);

		// Token: 0x06001843 RID: 6211
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Start_Internal(IntPtr recognizer);

		// Token: 0x06001844 RID: 6212
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Stop_Internal(IntPtr recognizer);

		// Token: 0x06001845 RID: 6213
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern bool IsRunning_Internal(IntPtr recognizer);

		// Token: 0x06001846 RID: 6214
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Destroy(IntPtr recognizer);

		// Token: 0x06001847 RID: 6215
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void DestroyThreaded(IntPtr recognizer);

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001848 RID: 6216 RVA: 0x00027084 File Offset: 0x00025284
		// (remove) Token: 0x06001849 RID: 6217 RVA: 0x000270BC File Offset: 0x000252BC
		[field: DebuggerBrowsable(0)]
		public event PhraseRecognizer.PhraseRecognizedDelegate OnPhraseRecognized;

		// Token: 0x0600184A RID: 6218 RVA: 0x000166AA File Offset: 0x000148AA
		internal PhraseRecognizer()
		{
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000270F4 File Offset: 0x000252F4
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					PhraseRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00027154 File Offset: 0x00025354
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Start_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00027184 File Offset: 0x00025384
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				PhraseRecognizer.Stop_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000271B4 File Offset: 0x000253B4
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				PhraseRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000271F8 File Offset: 0x000253F8
		public bool IsRunning
		{
			get
			{
				return this.m_Recognizer != IntPtr.Zero && PhraseRecognizer.IsRunning_Internal(this.m_Recognizer);
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0002722C File Offset: 0x0002542C
		[RequiredByNativeCode]
		private void InvokePhraseRecognizedEvent(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, long phraseStartFileTime, long phraseDurationTicks)
		{
			PhraseRecognizer.PhraseRecognizedDelegate onPhraseRecognized = this.OnPhraseRecognized;
			bool flag = onPhraseRecognized != null;
			if (flag)
			{
				onPhraseRecognized(new PhraseRecognizedEventArgs(text, confidence, semanticMeanings, DateTime.FromFileTime(phraseStartFileTime), TimeSpan.FromTicks(phraseDurationTicks)));
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00027268 File Offset: 0x00025468
		[RequiredByNativeCode]
		private unsafe static SemanticMeaning[] MarshalSemanticMeaning(IntPtr keys, IntPtr values, IntPtr valueSizes, int valueCount)
		{
			SemanticMeaning[] array = new SemanticMeaning[valueCount];
			int num = 0;
			for (int i = 0; i < valueCount; i++)
			{
				uint num2 = *(uint*)((byte*)(void*)valueSizes + (IntPtr)i * 4);
				SemanticMeaning semanticMeaning = new SemanticMeaning
				{
					key = new string(*(IntPtr*)((byte*)(void*)keys + (IntPtr)i * (IntPtr)sizeof(char*))),
					values = new string[num2]
				};
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					semanticMeaning.values[num3] = new string(*(IntPtr*)((byte*)(void*)values + (IntPtr)(num + num3) * (IntPtr)sizeof(char*)));
					num3++;
				}
				array[i] = semanticMeaning;
				num += (int)num2;
			}
			return array;
		}

		// Token: 0x04000764 RID: 1892
		protected IntPtr m_Recognizer;

		// Token: 0x02000225 RID: 549
		// (Invoke) Token: 0x06001853 RID: 6227
		public delegate void PhraseRecognizedDelegate(PhraseRecognizedEventArgs args);
	}
}
