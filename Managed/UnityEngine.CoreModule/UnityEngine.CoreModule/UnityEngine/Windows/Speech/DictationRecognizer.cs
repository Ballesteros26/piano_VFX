using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000226 RID: 550
	public sealed class DictationRecognizer : IDisposable
	{
		// Token: 0x06001856 RID: 6230
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern IntPtr Create(object self, ConfidenceLevel minimumConfidence, DictationTopicConstraint topicConstraint);

		// Token: 0x06001857 RID: 6231
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Start(IntPtr self);

		// Token: 0x06001858 RID: 6232
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Stop(IntPtr self);

		// Token: 0x06001859 RID: 6233
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Destroy(IntPtr self);

		// Token: 0x0600185A RID: 6234
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void DestroyThreaded(IntPtr self);

		// Token: 0x0600185B RID: 6235
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern SpeechSystemStatus GetStatus(IntPtr self);

		// Token: 0x0600185C RID: 6236
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern float GetAutoSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x0600185D RID: 6237
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void SetAutoSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x0600185E RID: 6238
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern float GetInitialSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x0600185F RID: 6239
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void SetInitialSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001860 RID: 6240 RVA: 0x0002732C File Offset: 0x0002552C
		// (remove) Token: 0x06001861 RID: 6241 RVA: 0x00027364 File Offset: 0x00025564
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationHypothesisDelegate DictationHypothesis;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001862 RID: 6242 RVA: 0x0002739C File Offset: 0x0002559C
		// (remove) Token: 0x06001863 RID: 6243 RVA: 0x000273D4 File Offset: 0x000255D4
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationResultDelegate DictationResult;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001864 RID: 6244 RVA: 0x0002740C File Offset: 0x0002560C
		// (remove) Token: 0x06001865 RID: 6245 RVA: 0x00027444 File Offset: 0x00025644
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationCompletedDelegate DictationComplete;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001866 RID: 6246 RVA: 0x0002747C File Offset: 0x0002567C
		// (remove) Token: 0x06001867 RID: 6247 RVA: 0x000274B4 File Offset: 0x000256B4
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationErrorHandler DictationError;

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x000274EC File Offset: 0x000256EC
		public SpeechSystemStatus Status
		{
			get
			{
				return (this.m_Recognizer != IntPtr.Zero) ? DictationRecognizer.GetStatus(this.m_Recognizer) : SpeechSystemStatus.Stopped;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00027520 File Offset: 0x00025720
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x0002755C File Offset: 0x0002575C
		public float AutoSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = DictationRecognizer.GetAutoSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return num;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetAutoSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00027590 File Offset: 0x00025790
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x000275CC File Offset: 0x000257CC
		public float InitialSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = DictationRecognizer.GetInitialSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return num;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetInitialSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000275FD File Offset: 0x000257FD
		public DictationRecognizer()
			: this(ConfidenceLevel.Medium, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00027609 File Offset: 0x00025809
		public DictationRecognizer(ConfidenceLevel confidenceLevel)
			: this(confidenceLevel, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00027615 File Offset: 0x00025815
		public DictationRecognizer(DictationTopicConstraint topic)
			: this(ConfidenceLevel.Medium, topic)
		{
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00027621 File Offset: 0x00025821
		public DictationRecognizer(ConfidenceLevel minimumConfidence, DictationTopicConstraint topic)
		{
			this.m_Recognizer = DictationRecognizer.Create(this, minimumConfidence, topic);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0002763C File Offset: 0x0002583C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					DictationRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0002769C File Offset: 0x0002589C
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Start(this.m_Recognizer);
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000276CC File Offset: 0x000258CC
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Stop(this.m_Recognizer);
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000276FC File Offset: 0x000258FC
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				DictationRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00027740 File Offset: 0x00025940
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeHypothesisGeneratedEvent(string keyword)
		{
			DictationRecognizer.DictationHypothesisDelegate dictationHypothesis = this.DictationHypothesis;
			bool flag = dictationHypothesis != null;
			if (flag)
			{
				dictationHypothesis(keyword);
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00027768 File Offset: 0x00025968
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeResultGeneratedEvent(string keyword, ConfidenceLevel minimumConfidence)
		{
			DictationRecognizer.DictationResultDelegate dictationResult = this.DictationResult;
			bool flag = dictationResult != null;
			if (flag)
			{
				dictationResult(keyword, minimumConfidence);
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00027790 File Offset: 0x00025990
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			DictationRecognizer.DictationCompletedDelegate dictationComplete = this.DictationComplete;
			bool flag = dictationComplete != null;
			if (flag)
			{
				dictationComplete(cause);
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000277B8 File Offset: 0x000259B8
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			DictationRecognizer.DictationErrorHandler dictationError = this.DictationError;
			bool flag = dictationError != null;
			if (flag)
			{
				dictationError(error, hresult);
			}
		}

		// Token: 0x04000766 RID: 1894
		private IntPtr m_Recognizer;

		// Token: 0x02000227 RID: 551
		// (Invoke) Token: 0x0600187A RID: 6266
		public delegate void DictationHypothesisDelegate(string text);

		// Token: 0x02000228 RID: 552
		// (Invoke) Token: 0x0600187E RID: 6270
		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		// Token: 0x02000229 RID: 553
		// (Invoke) Token: 0x06001882 RID: 6274
		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		// Token: 0x0200022A RID: 554
		// (Invoke) Token: 0x06001886 RID: 6278
		public delegate void DictationErrorHandler(string error, int hresult);
	}
}
