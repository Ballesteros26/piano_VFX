using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000221 RID: 545
	public static class PhraseRecognitionSystem
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600182F RID: 6191
		public static extern bool isSupported
		{
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[ThreadSafe]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001830 RID: 6192
		public static extern SpeechSystemStatus Status
		{
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001831 RID: 6193
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		public static extern void Restart();

		// Token: 0x06001832 RID: 6194
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		public static extern void Shutdown();

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001833 RID: 6195 RVA: 0x00026F6C File Offset: 0x0002516C
		// (remove) Token: 0x06001834 RID: 6196 RVA: 0x00026FA0 File Offset: 0x000251A0
		[field: DebuggerBrowsable(0)]
		public static event PhraseRecognitionSystem.ErrorDelegate OnError;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001835 RID: 6197 RVA: 0x00026FD4 File Offset: 0x000251D4
		// (remove) Token: 0x06001836 RID: 6198 RVA: 0x00027008 File Offset: 0x00025208
		[field: DebuggerBrowsable(0)]
		public static event PhraseRecognitionSystem.StatusDelegate OnStatusChanged;

		// Token: 0x06001837 RID: 6199 RVA: 0x0002703C File Offset: 0x0002523C
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.ErrorDelegate onError = PhraseRecognitionSystem.OnError;
			bool flag = onError != null;
			if (flag)
			{
				onError(errorCode);
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00027060 File Offset: 0x00025260
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.StatusDelegate onStatusChanged = PhraseRecognitionSystem.OnStatusChanged;
			bool flag = onStatusChanged != null;
			if (flag)
			{
				onStatusChanged(status);
			}
		}

		// Token: 0x02000222 RID: 546
		// (Invoke) Token: 0x0600183A RID: 6202
		public delegate void ErrorDelegate(SpeechError errorCode);

		// Token: 0x02000223 RID: 547
		// (Invoke) Token: 0x0600183E RID: 6206
		public delegate void StatusDelegate(SpeechSystemStatus status);
	}
}
