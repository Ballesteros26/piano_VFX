using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Burst.LowLevel
{
	// Token: 0x02000078 RID: 120
	[NativeHeader("Runtime/Burst/Burst.h")]
	[NativeHeader("Runtime/Burst/BurstDelegateCache.h")]
	[StaticAccessor("BurstCompilerService::Get()", StaticAccessorType.Arrow)]
	internal static class BurstCompilerService
	{
		// Token: 0x06000188 RID: 392
		[NativeMethod("Initialize")]
		[MethodImpl(4096)]
		private static extern string InitializeInternal(string path, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags);

		// Token: 0x06000189 RID: 393
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern string GetDisassembly(MethodInfo m, string compilerOptions);

		// Token: 0x0600018A RID: 394
		[FreeFunction]
		[MethodImpl(4096)]
		public static extern int CompileAsyncDelegateMethod(object delegateMethod, string compilerOptions);

		// Token: 0x0600018B RID: 395
		[FreeFunction]
		[MethodImpl(4096)]
		public unsafe static extern void* GetAsyncCompiledAsyncDelegateMethod(int userID);

		// Token: 0x0600018C RID: 396
		[ThreadSafe]
		[MethodImpl(4096)]
		public unsafe static extern void* GetOrCreateSharedMemory(ref Hash128 key, uint size_of, uint alignment);

		// Token: 0x0600018D RID: 397
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern string GetMethodSignature(MethodInfo method);

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600018E RID: 398
		public static extern bool IsInitialized
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600018F RID: 399
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void SetCurrentExecutionMode(uint environment);

		// Token: 0x06000190 RID: 400
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern uint GetCurrentExecutionMode();

		// Token: 0x06000191 RID: 401
		[ThreadSafe]
		[FreeFunction("DefaultBurstLogCallback")]
		[MethodImpl(4096)]
		public unsafe static extern void Log(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		// Token: 0x06000192 RID: 402 RVA: 0x00003F24 File Offset: 0x00002124
		public static void Initialize(string folderRuntime, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags)
		{
			bool flag = folderRuntime == null;
			if (flag)
			{
				throw new ArgumentNullException("folderRuntime");
			}
			bool flag2 = extractCompilerFlags == null;
			if (flag2)
			{
				throw new ArgumentNullException("extractCompilerFlags");
			}
			bool flag3 = !Directory.Exists(folderRuntime);
			if (flag3)
			{
				Debug.LogError("Unable to initialize the burst JIT compiler. The folder `" + folderRuntime + "` does not exist");
			}
			else
			{
				string text = BurstCompilerService.InitializeInternal(folderRuntime, extractCompilerFlags);
				bool flag4 = !string.IsNullOrEmpty(text);
				if (flag4)
				{
					Debug.LogError("Unexpected error while trying to initialize the burst JIT compiler: " + text);
				}
			}
		}

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x06000194 RID: 404
		public delegate bool ExtractCompilerFlags(Type jobType, out string flags);

		// Token: 0x0200007A RID: 122
		public enum BurstLogType
		{
			// Token: 0x04000128 RID: 296
			Info,
			// Token: 0x04000129 RID: 297
			Warning,
			// Token: 0x0400012A RID: 298
			Error
		}
	}
}
