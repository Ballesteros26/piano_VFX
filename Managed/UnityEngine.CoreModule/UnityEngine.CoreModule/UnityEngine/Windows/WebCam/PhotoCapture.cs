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
	// Token: 0x02000235 RID: 565
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCapture.h")]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StaticAccessor("PhotoCapture", StaticAccessorType.DoubleColon)]
	[StructLayout(0)]
	public class PhotoCapture : IDisposable
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x0002792C File Offset: 0x00025B2C
		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(PhotoCapture.CaptureResultType resultType, long hResult)
		{
			return new PhotoCapture.PhotoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00027958 File Offset: 0x00025B58
		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(long hResult)
		{
			PhotoCapture.PhotoCaptureResult photoCaptureResult = default(PhotoCapture.PhotoCaptureResult);
			bool flag = hResult == PhotoCapture.HR_SUCCESS;
			PhotoCapture.CaptureResultType captureResultType;
			if (flag)
			{
				captureResultType = PhotoCapture.CaptureResultType.Success;
			}
			else
			{
				captureResultType = PhotoCapture.CaptureResultType.UnknownError;
			}
			photoCaptureResult.resultType = captureResultType;
			photoCaptureResult.hResult = hResult;
			return photoCaptureResult;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0002799C File Offset: 0x00025B9C
		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = PhotoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					PhotoCapture.s_SupportedResolutions = PhotoCapture.GetSupportedResolutions_Internal();
				}
				return PhotoCapture.s_SupportedResolutions;
			}
		}

		// Token: 0x06001895 RID: 6293
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("GetSupportedResolutions")]
		[MethodImpl(4096)]
		private static extern Resolution[] GetSupportedResolutions_Internal();

		// Token: 0x06001896 RID: 6294 RVA: 0x000279CC File Offset: 0x00025BCC
		public static void CreateAsync(bool showHolograms, PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			PhotoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000279F8 File Offset: 0x00025BF8
		public static void CreateAsync(PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			PhotoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		// Token: 0x06001898 RID: 6296
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Instantiate")]
		[MethodImpl(4096)]
		private static extern IntPtr Instantiate_Internal(bool showHolograms, PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback);

		// Token: 0x06001899 RID: 6297 RVA: 0x00027A24 File Offset: 0x00025C24
		[RequiredByNativeCode]
		private static void InvokeOnCreatedResourceDelegate(PhotoCapture.OnCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new PhotoCapture(nativePtr));
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00027A5C File Offset: 0x00025C5C
		private PhotoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00027A70 File Offset: 0x00025C70
		public void StartPhotoModeAsync(CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback)
		{
			bool flag = onPhotoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentException("onPhotoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			this.StartPhotoMode_Internal(setupParams, onPhotoModeStartedCallback);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00027AC6 File Offset: 0x00025CC6
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("StartPhotoMode")]
		private void StartPhotoMode_Internal(CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback)
		{
			this.StartPhotoMode_Internal_Injected(ref setupParams, onPhotoModeStartedCallback);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00027AD1 File Offset: 0x00025CD1
		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStartedDelegate(PhotoCapture.OnPhotoModeStartedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x0600189E RID: 6302
		[NativeName("StopPhotoMode")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		public extern void StopPhotoModeAsync(PhotoCapture.OnPhotoModeStoppedCallback onPhotoModeStoppedCallback);

		// Token: 0x0600189F RID: 6303 RVA: 0x00027AE1 File Offset: 0x00025CE1
		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStoppedDelegate(PhotoCapture.OnPhotoModeStoppedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00027AF4 File Offset: 0x00025CF4
		public void TakePhotoAsync(string filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback)
		{
			bool flag = onCapturedPhotoToDiskCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCapturedPhotoToDiskCallback");
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
			this.CapturePhotoToDisk_Internal(filename, fileOutputFormat, onCapturedPhotoToDiskCallback);
		}

		// Token: 0x060018A1 RID: 6305
		[NativeName("CapturePhotoToDisk")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		private extern void CapturePhotoToDisk_Internal(string filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback);

		// Token: 0x060018A2 RID: 6306 RVA: 0x00027BA9 File Offset: 0x00025DA9
		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToDiskDelegate(PhotoCapture.OnCapturedToDiskCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00027BBC File Offset: 0x00025DBC
		public void TakePhotoAsync(PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback)
		{
			bool flag = onCapturedPhotoToMemoryCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCapturedPhotoToMemoryCallback");
			}
			this.CapturePhotoToMemory_Internal(onCapturedPhotoToMemoryCallback);
		}

		// Token: 0x060018A4 RID: 6308
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("CapturePhotoToMemory")]
		[MethodImpl(4096)]
		private extern void CapturePhotoToMemory_Internal(PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback);

		// Token: 0x060018A5 RID: 6309 RVA: 0x00027BE8 File Offset: 0x00025DE8
		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToMemoryDelegate(PhotoCapture.OnCapturedToMemoryCallback callback, long hResult, IntPtr photoCaptureFramePtr)
		{
			PhotoCaptureFrame photoCaptureFrame = null;
			bool flag = photoCaptureFramePtr != IntPtr.Zero;
			if (flag)
			{
				photoCaptureFrame = new PhotoCaptureFrame(photoCaptureFramePtr);
			}
			callback(PhotoCapture.MakeCaptureResult(hResult), photoCaptureFrame);
		}

		// Token: 0x060018A6 RID: 6310
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[NativeName("GetUnsafePointerToVideoDeviceController")]
		[MethodImpl(4096)]
		public extern IntPtr GetUnsafePointerToVideoDeviceController();

		// Token: 0x060018A7 RID: 6311 RVA: 0x00027C20 File Offset: 0x00025E20
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

		// Token: 0x060018A8 RID: 6312
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Dispose")]
		[MethodImpl(4096)]
		private extern void Dispose_Internal();

		// Token: 0x060018A9 RID: 6313 RVA: 0x00027C60 File Offset: 0x00025E60
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

		// Token: 0x060018AA RID: 6314
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("DisposeThreaded")]
		[MethodImpl(4096)]
		private extern void DisposeThreaded_Internal();

		// Token: 0x060018AC RID: 6316
		[MethodImpl(4096)]
		private extern void StartPhotoMode_Internal_Injected(ref CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback);

		// Token: 0x04000798 RID: 1944
		internal IntPtr m_NativePtr;

		// Token: 0x04000799 RID: 1945
		private static Resolution[] s_SupportedResolutions;

		// Token: 0x0400079A RID: 1946
		private static readonly long HR_SUCCESS = 0L;

		// Token: 0x02000236 RID: 566
		public enum CaptureResultType
		{
			// Token: 0x0400079C RID: 1948
			Success,
			// Token: 0x0400079D RID: 1949
			UnknownError
		}

		// Token: 0x02000237 RID: 567
		public struct PhotoCaptureResult
		{
			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x060018AD RID: 6317 RVA: 0x00027CC0 File Offset: 0x00025EC0
			public bool success
			{
				get
				{
					return this.resultType == PhotoCapture.CaptureResultType.Success;
				}
			}

			// Token: 0x0400079E RID: 1950
			public PhotoCapture.CaptureResultType resultType;

			// Token: 0x0400079F RID: 1951
			public long hResult;
		}

		// Token: 0x02000238 RID: 568
		// (Invoke) Token: 0x060018AF RID: 6319
		public delegate void OnCaptureResourceCreatedCallback(PhotoCapture captureObject);

		// Token: 0x02000239 RID: 569
		// (Invoke) Token: 0x060018B3 RID: 6323
		public delegate void OnPhotoModeStartedCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x0200023A RID: 570
		// (Invoke) Token: 0x060018B7 RID: 6327
		public delegate void OnPhotoModeStoppedCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x0200023B RID: 571
		// (Invoke) Token: 0x060018BB RID: 6331
		public delegate void OnCapturedToDiskCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x0200023C RID: 572
		// (Invoke) Token: 0x060018BF RID: 6335
		public delegate void OnCapturedToMemoryCallback(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame);
	}
}
