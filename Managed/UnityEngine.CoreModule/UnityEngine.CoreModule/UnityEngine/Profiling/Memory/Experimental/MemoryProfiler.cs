using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Profiling.Experimental;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling.Memory.Experimental
{
	// Token: 0x02000219 RID: 537
	[NativeHeader("Modules/Profiler/Runtime/MemorySnapshotManager.h")]
	public sealed class MemoryProfiler
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060017E4 RID: 6116 RVA: 0x000267CC File Offset: 0x000249CC
		// (remove) Token: 0x060017E5 RID: 6117 RVA: 0x00026800 File Offset: 0x00024A00
		[field: DebuggerBrowsable(0)]
		private static event Action<string, bool> m_SnapshotFinished;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060017E6 RID: 6118 RVA: 0x00026834 File Offset: 0x00024A34
		// (remove) Token: 0x060017E7 RID: 6119 RVA: 0x00026868 File Offset: 0x00024A68
		[field: DebuggerBrowsable(0)]
		private static event Action<string, bool, DebugScreenCapture> m_SaveScreenshotToDisk;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060017E8 RID: 6120 RVA: 0x0002689C File Offset: 0x00024A9C
		// (remove) Token: 0x060017E9 RID: 6121 RVA: 0x000268D0 File Offset: 0x00024AD0
		[field: DebuggerBrowsable(0)]
		public static event Action<MetaData> createMetaData;

		// Token: 0x060017EA RID: 6122
		[NativeMethod("StartOperation")]
		[StaticAccessor("profiling::memory::GetMemorySnapshotManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(4096)]
		private static extern void StartOperation(uint captureFlag, bool requestScreenshot, string path);

		// Token: 0x060017EB RID: 6123 RVA: 0x00026903 File Offset: 0x00024B03
		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			MemoryProfiler.TakeSnapshot(path, finishCallback, null, captureFlags);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00026910 File Offset: 0x00024B10
		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, Action<string, bool, DebugScreenCapture> screenshotCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Debug.LogWarning("Canceling snapshot, there is another snapshot in progress.");
				finishCallback.Invoke(path, false);
			}
			else
			{
				MemoryProfiler.m_SnapshotFinished += finishCallback;
				MemoryProfiler.m_SaveScreenshotToDisk += screenshotCallback;
				MemoryProfiler.StartOperation((uint)captureFlags, MemoryProfiler.m_SaveScreenshotToDisk != null, path);
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00026964 File Offset: 0x00024B64
		public static void TakeTempSnapshot(Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			string[] array = Application.dataPath.Split(new char[] { '/' });
			string text = array[array.Length - 2];
			string text2 = Application.temporaryCachePath + "/" + text + ".snap";
			MemoryProfiler.TakeSnapshot(text2, finishCallback, captureFlags);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000269B0 File Offset: 0x00024BB0
		[RequiredByNativeCode]
		private static byte[] PrepareMetadata()
		{
			bool flag = MemoryProfiler.createMetaData == null;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				MetaData metaData = new MetaData();
				MemoryProfiler.createMetaData.Invoke(metaData);
				bool flag2 = metaData.content == null;
				if (flag2)
				{
					metaData.content = "";
				}
				bool flag3 = metaData.platform == null;
				if (flag3)
				{
					metaData.platform = "";
				}
				int num = 2 * metaData.content.Length;
				int num2 = 2 * metaData.platform.Length;
				int num3 = num + num2 + 12;
				byte[] array2 = new byte[num3];
				int num4 = 0;
				num4 = MemoryProfiler.WriteIntToByteArray(array2, num4, metaData.content.Length);
				num4 = MemoryProfiler.WriteStringToByteArray(array2, num4, metaData.content);
				num4 = MemoryProfiler.WriteIntToByteArray(array2, num4, metaData.platform.Length);
				num4 = MemoryProfiler.WriteStringToByteArray(array2, num4, metaData.platform);
				array = array2;
			}
			return array;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00026AA4 File Offset: 0x00024CA4
		internal unsafe static int WriteIntToByteArray(byte[] array, int offset, int value)
		{
			byte* ptr = (byte*)(&value);
			array[offset++] = *ptr;
			array[offset++] = ptr[1];
			array[offset++] = ptr[2];
			array[offset++] = ptr[3];
			return offset;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00026AEC File Offset: 0x00024CEC
		internal unsafe static int WriteStringToByteArray(byte[] array, int offset, string value)
		{
			bool flag = value.Length != 0;
			if (flag)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					char* ptr3 = ptr + value.Length;
					while (ptr2 != ptr3)
					{
						for (int i = 0; i < 2; i++)
						{
							array[offset++] = *(byte*)(ptr2 + i / 2);
						}
						ptr2++;
					}
				}
			}
			return offset;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00026B70 File Offset: 0x00024D70
		[RequiredByNativeCode]
		private static void FinalizeSnapshot(string path, bool result)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Action<string, bool> snapshotFinished = MemoryProfiler.m_SnapshotFinished;
				MemoryProfiler.m_SnapshotFinished = null;
				snapshotFinished.Invoke(path, result);
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00026BA4 File Offset: 0x00024DA4
		[RequiredByNativeCode]
		private static void SaveScreenshotToDisk(string path, bool result, IntPtr pixelsPtr, int pixelsCount, TextureFormat format, int width, int height)
		{
			bool flag = MemoryProfiler.m_SaveScreenshotToDisk != null;
			if (flag)
			{
				Action<string, bool, DebugScreenCapture> saveScreenshotToDisk = MemoryProfiler.m_SaveScreenshotToDisk;
				MemoryProfiler.m_SaveScreenshotToDisk = null;
				DebugScreenCapture debugScreenCapture = default(DebugScreenCapture);
				if (result)
				{
					NativeArray<byte> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(pixelsPtr.ToPointer(), pixelsCount, Allocator.Persistent);
					debugScreenCapture.rawImageDataReference = nativeArray;
					debugScreenCapture.height = height;
					debugScreenCapture.width = width;
					debugScreenCapture.imageFormat = format;
				}
				saveScreenshotToDisk.Invoke(path, result, debugScreenCapture);
			}
		}
	}
}
