using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x0200023E RID: 574
	[StaticAccessor("VideoCaptureBindings", StaticAccessorType.DoubleColon)]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeHeader("PlatformDependent/Win/Webcam/VideoCaptureBindings.h")]
	[StructLayout(0)]
	public class VideoCapture : IDisposable
	{
		// Token: 0x060018DC RID: 6364 RVA: 0x00027FC8 File Offset: 0x000261C8
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(VideoCapture.CaptureResultType resultType, long hResult)
		{
			return new VideoCapture.VideoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00027FF4 File Offset: 0x000261F4
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(long hResult)
		{
			VideoCapture.VideoCaptureResult videoCaptureResult = default(VideoCapture.VideoCaptureResult);
			bool flag = hResult == VideoCapture.HR_SUCCESS;
			VideoCapture.CaptureResultType captureResultType;
			if (flag)
			{
				captureResultType = VideoCapture.CaptureResultType.Success;
			}
			else
			{
				captureResultType = VideoCapture.CaptureResultType.UnknownError;
			}
			videoCaptureResult.resultType = captureResultType;
			videoCaptureResult.hResult = hResult;
			return videoCaptureResult;
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00028038 File Offset: 0x00026238
		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = VideoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					VideoCapture.s_SupportedResolutions = VideoCapture.GetSupportedResolutions_Internal();
				}
				return VideoCapture.s_SupportedResolutions;
			}
		}

		// Token: 0x060018DF RID: 6367
		[NativeName("GetSupportedResolutions")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		private static extern Resolution[] GetSupportedResolutions_Internal();

		// Token: 0x060018E0 RID: 6368 RVA: 0x00028068 File Offset: 0x00026268
		public static IEnumerable<float> GetSupportedFrameRatesForResolution(Resolution resolution)
		{
			return VideoCapture.GetSupportedFrameRatesForResolution_Internal(resolution.width, resolution.height);
		}

		// Token: 0x060018E1 RID: 6369
		[NativeName("GetSupportedFrameRatesForResolution")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		private static extern float[] GetSupportedFrameRatesForResolution_Internal(int resolutionWidth, int resolutionHeight);

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060018E2 RID: 6370
		public extern bool IsRecording
		{
			[NativeMethod("VideoCaptureBindings::IsRecording", HasExplicitThis = true)]
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00028094 File Offset: 0x00026294
		public static void CreateAsync(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000280C0 File Offset: 0x000262C0
		public static void CreateAsync(VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		// Token: 0x060018E5 RID: 6373
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Instantiate")]
		[MethodImpl(4096)]
		private static extern void Instantiate_Internal(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback);

		// Token: 0x060018E6 RID: 6374 RVA: 0x000280EC File Offset: 0x000262EC
		[RequiredByNativeCode]
		private static void InvokeOnCreatedVideoCaptureResourceDelegate(VideoCapture.OnVideoCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new VideoCapture(nativePtr));
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00028124 File Offset: 0x00026324
		private VideoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00028138 File Offset: 0x00026338
		public void StartVideoModeAsync(CameraParameters setupParams, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			bool flag = onVideoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onVideoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			bool flag3 = setupParams.frameRate == 0f;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera frame rate must be set to a supported recording frame rate.");
			}
			this.StartVideoMode_Internal(setupParams, audioState, onVideoModeStartedCallback);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000281B2 File Offset: 0x000263B2
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StartVideoMode", HasExplicitThis = true)]
		private void StartVideoMode_Internal(CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			this.StartVideoMode_Internal_Injected(ref cameraParameters, audioState, onVideoModeStartedCallback);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000281BE File Offset: 0x000263BE
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStartedDelegate(VideoCapture.OnVideoModeStartedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060018EB RID: 6379
		[NativeMethod("VideoCaptureBindings::StopVideoMode", HasExplicitThis = true)]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		public extern void StopVideoModeAsync([NotNull] VideoCapture.OnVideoModeStoppedCallback onVideoModeStoppedCallback);

		// Token: 0x060018EC RID: 6380 RVA: 0x000281CE File Offset: 0x000263CE
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStoppedDelegate(VideoCapture.OnVideoModeStoppedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000281E0 File Offset: 0x000263E0
		public void StartRecordingAsync(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback)
		{
			bool flag = onStartedRecordingVideoCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onStartedRecordingVideoCallback");
			}
			bool flag2 = string.IsNullOrEmpty(filename);
			if (flag2)
			{
				throw new ArgumentNullException("filename");
			}
			filename = filename.Replace("/", "\\");
			string directoryName = Path.GetDirectoryName(filename);
			bool flag3 = !string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName);
			if (flag3)
			{
				throw new ArgumentException("The specified directory does not exist.", "filename");
			}
			FileInfo fileInfo = new FileInfo(filename);
			bool flag4 = fileInfo.Exists && fileInfo.IsReadOnly;
			if (flag4)
			{
				throw new ArgumentException("Cannot write to the file because it is read-only.", "filename");
			}
			this.StartRecordingVideoToDisk_Internal(filename, onStartedRecordingVideoCallback);
		}

		// Token: 0x060018EE RID: 6382
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StartRecordingVideoToDisk", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void StartRecordingVideoToDisk_Internal(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback);

		// Token: 0x060018EF RID: 6383 RVA: 0x00028294 File Offset: 0x00026494
		[RequiredByNativeCode]
		private static void InvokeOnStartedRecordingVideoToDiskDelegate(VideoCapture.OnStartedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060018F0 RID: 6384
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StopRecordingVideoToDisk", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void StopRecordingAsync([NotNull] VideoCapture.OnStoppedRecordingVideoCallback onStoppedRecordingVideoCallback);

		// Token: 0x060018F1 RID: 6385 RVA: 0x000282A4 File Offset: 0x000264A4
		[RequiredByNativeCode]
		private static void InvokeOnStoppedRecordingVideoToDiskDelegate(VideoCapture.OnStoppedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060018F2 RID: 6386
		[NativeMethod("VideoCaptureBindings::GetUnsafePointerToVideoDeviceController", HasExplicitThis = true)]
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		public extern IntPtr GetUnsafePointerToVideoDeviceController();

		// Token: 0x060018F3 RID: 6387 RVA: 0x000282B4 File Offset: 0x000264B4
		public void Dispose()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018F4 RID: 6388
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::Dispose", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void Dispose_Internal();

		// Token: 0x060018F5 RID: 6389 RVA: 0x000282F4 File Offset: 0x000264F4
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_NativePtr != IntPtr.Zero;
				if (flag)
				{
					this.DisposeThreaded_Internal();
					this.m_NativePtr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060018F6 RID: 6390
		[NativeMethod("VideoCaptureBindings::DisposeThreaded", HasExplicitThis = true)]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern void DisposeThreaded_Internal();

		// Token: 0x060018F8 RID: 6392
		[MethodImpl(4096)]
		private extern void StartVideoMode_Internal_Injected(ref CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback);

		// Token: 0x040007A4 RID: 1956
		internal IntPtr m_NativePtr;

		// Token: 0x040007A5 RID: 1957
		private static Resolution[] s_SupportedResolutions;

		// Token: 0x040007A6 RID: 1958
		private static readonly long HR_SUCCESS = 0L;

		// Token: 0x0200023F RID: 575
		public enum CaptureResultType
		{
			// Token: 0x040007A8 RID: 1960
			Success,
			// Token: 0x040007A9 RID: 1961
			UnknownError
		}

		// Token: 0x02000240 RID: 576
		public enum AudioState
		{
			// Token: 0x040007AB RID: 1963
			MicAudio,
			// Token: 0x040007AC RID: 1964
			ApplicationAudio,
			// Token: 0x040007AD RID: 1965
			ApplicationAndMicAudio,
			// Token: 0x040007AE RID: 1966
			None
		}

		// Token: 0x02000241 RID: 577
		public struct VideoCaptureResult
		{
			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00028354 File Offset: 0x00026554
			public bool success
			{
				get
				{
					return this.resultType == VideoCapture.CaptureResultType.Success;
				}
			}

			// Token: 0x040007AF RID: 1967
			public VideoCapture.CaptureResultType resultType;

			// Token: 0x040007B0 RID: 1968
			public long hResult;
		}

		// Token: 0x02000242 RID: 578
		// (Invoke) Token: 0x060018FB RID: 6395
		public delegate void OnVideoCaptureResourceCreatedCallback(VideoCapture captureObject);

		// Token: 0x02000243 RID: 579
		// (Invoke) Token: 0x060018FF RID: 6399
		public delegate void OnVideoModeStartedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000244 RID: 580
		// (Invoke) Token: 0x06001903 RID: 6403
		public delegate void OnVideoModeStoppedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000245 RID: 581
		// (Invoke) Token: 0x06001907 RID: 6407
		public delegate void OnStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000246 RID: 582
		// (Invoke) Token: 0x0600190B RID: 6411
		public delegate void OnStoppedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);
	}
}
