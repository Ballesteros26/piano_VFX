using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NatSuite.Devices.Internal
{
	// Token: 0x0200003A RID: 58
	public static class Bridge
	{
		// Token: 0x060001FF RID: 511
		[DllImport("NatDevice", EntryPoint = "NDDispose")]
		public static extern void Dispose(this IntPtr device);

		// Token: 0x06000200 RID: 512
		[DllImport("NatDevice", EntryPoint = "NDUniqueID")]
		public static extern void UniqueID(this IntPtr device, [MarshalAs(UnmanagedType.LPStr)] StringBuilder dest);

		// Token: 0x06000201 RID: 513
		[DllImport("NatDevice", EntryPoint = "NDRunning")]
		public static extern bool Running(this IntPtr camera);

		// Token: 0x06000202 RID: 514
		[DllImport("NatDevice", EntryPoint = "NDAudioDeviceStartRunning")]
		public static extern bool StartRunning(this IntPtr device, Bridge.SampleBufferDelegate callback, IntPtr context);

		// Token: 0x06000203 RID: 515
		[DllImport("NatDevice", EntryPoint = "NDStopRunning")]
		public static extern void StopRunning(this IntPtr device);

		// Token: 0x06000204 RID: 516
		[DllImport("NatDevice", EntryPoint = "NDAudioDevices")]
		public static extern void AudioDevices(out IntPtr outDevicesArray, out int outDevicesArrayCount);

		// Token: 0x06000205 RID: 517
		[DllImport("NatDevice", EntryPoint = "NDName")]
		public static extern void Name(this IntPtr device, [MarshalAs(UnmanagedType.LPStr)] StringBuilder dest);

		// Token: 0x06000206 RID: 518
		[DllImport("NatDevice", EntryPoint = "NDEchoCancellation")]
		public static extern bool EchoCancellation(this IntPtr device);

		// Token: 0x06000207 RID: 519
		[DllImport("NatDevice", EntryPoint = "NDSampleRate")]
		public static extern int SampleRate(this IntPtr device);

		// Token: 0x06000208 RID: 520
		[DllImport("NatDevice", EntryPoint = "NDSetSampleRate")]
		public static extern void SampleRate(this IntPtr device, int sampleRate);

		// Token: 0x06000209 RID: 521
		[DllImport("NatDevice", EntryPoint = "NDChannelCount")]
		public static extern int ChannelCount(this IntPtr device);

		// Token: 0x0600020A RID: 522
		[DllImport("NatDevice", EntryPoint = "NDSetChannelCount")]
		public static extern void ChannelCount(this IntPtr device, int sampleRate);

		// Token: 0x0600020B RID: 523
		[DllImport("NatDevice", EntryPoint = "NDCameraDevices")]
		public static extern void CameraDevices(out IntPtr outDevicesArray, out int outDevicesArrayCount);

		// Token: 0x0600020C RID: 524
		[DllImport("NatDevice", EntryPoint = "NDFrontFacing")]
		public static extern bool FrontFacing(this IntPtr device);

		// Token: 0x0600020D RID: 525
		[DllImport("NatDevice", EntryPoint = "NDFlashSupported")]
		public static extern bool FlashSupported(this IntPtr device);

		// Token: 0x0600020E RID: 526
		[DllImport("NatDevice", EntryPoint = "NDTorchSupported")]
		public static extern bool TorchSupported(this IntPtr device);

		// Token: 0x0600020F RID: 527
		[DllImport("NatDevice", EntryPoint = "NDExposureLockSupported")]
		public static extern bool ExposureLockSupported(this IntPtr device);

		// Token: 0x06000210 RID: 528
		[DllImport("NatDevice", EntryPoint = "NDFocusLockSupported")]
		public static extern bool FocusLockSupported(this IntPtr device);

		// Token: 0x06000211 RID: 529
		[DllImport("NatDevice", EntryPoint = "NDWhiteBalanceLockSupported")]
		public static extern bool WhiteBalanceLockSupported(this IntPtr device);

		// Token: 0x06000212 RID: 530
		[DllImport("NatDevice", EntryPoint = "NDFieldOfView")]
		public static extern void FieldOfView(this IntPtr device, out float x, out float y);

		// Token: 0x06000213 RID: 531
		[DllImport("NatDevice", EntryPoint = "NDExposureRange")]
		public static extern void ExposureRange(this IntPtr device, out float min, out float max);

		// Token: 0x06000214 RID: 532
		[DllImport("NatDevice", EntryPoint = "NDZoomRange")]
		public static extern void ZoomRange(this IntPtr device, out float min, out float max);

		// Token: 0x06000215 RID: 533
		[DllImport("NatDevice", EntryPoint = "NDPreviewResolution")]
		public static extern void PreviewResolution(this IntPtr device, out int width, out int height);

		// Token: 0x06000216 RID: 534
		[DllImport("NatDevice", EntryPoint = "NDSetPreviewResolution")]
		public static extern void PreviewResolution(this IntPtr device, int width, int height);

		// Token: 0x06000217 RID: 535
		[DllImport("NatDevice", EntryPoint = "NDPhotoResolution")]
		public static extern void PhotoResolution(this IntPtr device, out int width, out int height);

		// Token: 0x06000218 RID: 536
		[DllImport("NatDevice", EntryPoint = "NDSetPhotoResolution")]
		public static extern void PhotoResolution(this IntPtr device, int width, int height);

		// Token: 0x06000219 RID: 537
		[DllImport("NatDevice", EntryPoint = "NDFramerate")]
		public static extern int Framerate(this IntPtr device);

		// Token: 0x0600021A RID: 538
		[DllImport("NatDevice", EntryPoint = "NDSetFramerate")]
		public static extern void Framerate(this IntPtr device, int framerate);

		// Token: 0x0600021B RID: 539
		[DllImport("NatDevice", EntryPoint = "NDExposureBias")]
		public static extern float ExposureBias(this IntPtr device);

		// Token: 0x0600021C RID: 540
		[DllImport("NatDevice", EntryPoint = "NDSetExposureBias")]
		public static extern void ExposureBias(this IntPtr device, float bias);

		// Token: 0x0600021D RID: 541
		[DllImport("NatDevice", EntryPoint = "NDSetExposurePoint")]
		public static extern void ExposurePoint(this IntPtr device, float x, float y);

		// Token: 0x0600021E RID: 542
		[DllImport("NatDevice", EntryPoint = "NDExposureLock")]
		public static extern bool ExposureLock(this IntPtr device);

		// Token: 0x0600021F RID: 543
		[DllImport("NatDevice", EntryPoint = "NDSetExposureLock")]
		public static extern void ExposureLock(this IntPtr device, bool locked);

		// Token: 0x06000220 RID: 544
		[DllImport("NatDevice", EntryPoint = "NDFlashMode")]
		public static extern FlashMode FlashMode(this IntPtr device);

		// Token: 0x06000221 RID: 545
		[DllImport("NatDevice", EntryPoint = "NDSetFlashMode")]
		public static extern void FlashMode(this IntPtr device, FlashMode state);

		// Token: 0x06000222 RID: 546
		[DllImport("NatDevice", EntryPoint = "NDFocusLock")]
		public static extern bool FocusLock(this IntPtr device);

		// Token: 0x06000223 RID: 547
		[DllImport("NatDevice", EntryPoint = "NDSetFocusLock")]
		public static extern void FocusLock(this IntPtr device, bool locked);

		// Token: 0x06000224 RID: 548
		[DllImport("NatDevice", EntryPoint = "NDSetFocusPoint")]
		public static extern void FocusPoint(this IntPtr device, float x, float y);

		// Token: 0x06000225 RID: 549
		[DllImport("NatDevice", EntryPoint = "NDTorchEnabled")]
		public static extern bool TorchEnabled(this IntPtr device);

		// Token: 0x06000226 RID: 550
		[DllImport("NatDevice", EntryPoint = "NDSetTorchEnabled")]
		public static extern void TorchEnabled(this IntPtr device, bool enabled);

		// Token: 0x06000227 RID: 551
		[DllImport("NatDevice", EntryPoint = "NDWhiteBalanceLock")]
		public static extern bool WhiteBalanceLock(this IntPtr device);

		// Token: 0x06000228 RID: 552
		[DllImport("NatDevice", EntryPoint = "NDSetWhiteBalanceLock")]
		public static extern void WhiteBalanceLock(this IntPtr device, bool locked);

		// Token: 0x06000229 RID: 553
		[DllImport("NatDevice", EntryPoint = "NDZoomRatio")]
		public static extern float ZoomRatio(this IntPtr device);

		// Token: 0x0600022A RID: 554
		[DllImport("NatDevice", EntryPoint = "NDSetZoomRatio")]
		public static extern void ZoomRatio(this IntPtr device, float ratio);

		// Token: 0x0600022B RID: 555
		[DllImport("NatDevice", EntryPoint = "NDSetOrientation")]
		public static extern void Orientation(this IntPtr device, FrameOrientation orentation);

		// Token: 0x0600022C RID: 556
		[DllImport("NatDevice", EntryPoint = "NDCameraDeviceStartRunning")]
		public static extern void StartRunning(this IntPtr device, Bridge.FrameDelegate handler, IntPtr context);

		// Token: 0x0600022D RID: 557
		[DllImport("NatDevice", EntryPoint = "NDCapturePhoto")]
		public static extern void CapturePhoto(this IntPtr device, Bridge.FrameDelegate handler, IntPtr context);

		// Token: 0x040003CB RID: 971
		private const string Assembly = "NatDevice";

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x0600036B RID: 875
		public delegate void FrameDelegate(IntPtr context, IntPtr pixelBuffer, int width, int height, long timestamp);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x0600036F RID: 879
		public delegate void SampleBufferDelegate(IntPtr context, IntPtr sampleBuffer, int sampleCount, long timestamp);
	}
}
