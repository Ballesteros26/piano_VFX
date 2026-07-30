using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NatSuite.Devices.Internal;
using UnityEngine;

namespace NatSuite.Devices
{
	// Token: 0x02000030 RID: 48
	[Doc("CameraDevice")]
	public abstract class CameraDevice : ICameraDevice, IMediaDevice, IEquatable<IMediaDevice>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060001A7 RID: 423
		[Doc("UniqueID")]
		public abstract string uniqueID { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001A8 RID: 424
		[Doc("FrontFacing")]
		public abstract bool frontFacing { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001A9 RID: 425
		[Doc("FlashSupported")]
		public abstract bool flashSupported { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001AA RID: 426
		[Doc("TorchSupported")]
		public abstract bool torchSupported { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001AB RID: 427
		[Doc("ExposureLockSupported")]
		public abstract bool exposureLockSupported { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001AC RID: 428
		[Doc("FocusLockSupported")]
		public abstract bool focusLockSupported { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001AD RID: 429
		[Doc("WhiteBalanceLockSupported")]
		public abstract bool whiteBalanceLockSupported { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001AE RID: 430
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("FieldOfView")]
		public abstract ValueTuple<float, float> fieldOfView
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001AF RID: 431
		[TupleElementNames(new string[] { "min", "max" })]
		[Doc("ExposureRange")]
		public abstract ValueTuple<float, float> exposureRange
		{
			[return: TupleElementNames(new string[] { "min", "max" })]
			get;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001B0 RID: 432
		[TupleElementNames(new string[] { "min", "max" })]
		[Doc("ZoomRange")]
		public abstract ValueTuple<float, float> zoomRange
		{
			[return: TupleElementNames(new string[] { "min", "max" })]
			get;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001B1 RID: 433
		// (set) Token: 0x060001B2 RID: 434
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("PreviewResolution")]
		public abstract ValueTuple<int, int> previewResolution
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get;
			[param: TupleElementNames(new string[] { "width", "height" })]
			set;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B4 RID: 436
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("PhotoResolution")]
		public abstract ValueTuple<int, int> photoResolution
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get;
			[param: TupleElementNames(new string[] { "width", "height" })]
			set;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001B5 RID: 437
		// (set) Token: 0x060001B6 RID: 438
		[Doc("Framerate")]
		public abstract int frameRate { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		[Doc("ExposureBias", "ExposureBiasDiscussion")]
		public abstract float exposureBias { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001B9 RID: 441
		// (set) Token: 0x060001BA RID: 442
		[Doc("ExposureLock")]
		public abstract bool exposureLock { get; set; }

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x060001BB RID: 443
		[TupleElementNames(new string[] { "x", "y" })]
		public abstract ValueTuple<float, float> exposurePoint
		{
			[param: TupleElementNames(new string[] { "x", "y" })]
			set;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001BC RID: 444
		// (set) Token: 0x060001BD RID: 445
		[Doc("PhotoFlashMode")]
		public abstract FlashMode flashMode { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001BE RID: 446
		// (set) Token: 0x060001BF RID: 447
		[Doc("FocusLock")]
		public abstract bool focusLock { get; set; }

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x060001C0 RID: 448
		[TupleElementNames(new string[] { "x", "y" })]
		public abstract ValueTuple<float, float> focusPoint
		{
			[param: TupleElementNames(new string[] { "x", "y" })]
			set;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		[Doc("TorchEnabled")]
		public abstract bool torchEnabled { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001C3 RID: 451
		// (set) Token: 0x060001C4 RID: 452
		[Doc("WhiteBalanceLock")]
		public abstract bool whiteBalanceLock { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001C5 RID: 453
		// (set) Token: 0x060001C6 RID: 454
		[Doc("ZoomRatio")]
		public abstract float zoomRatio { get; set; }

		// Token: 0x1700001E RID: 30
		// (set) Token: 0x060001C7 RID: 455
		public abstract FrameOrientation orientation { set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001C8 RID: 456
		[Doc("Running")]
		public abstract bool running { get; }

		// Token: 0x060001C9 RID: 457
		[Doc("StartPreview")]
		public abstract Task<Texture2D> StartRunning();

		// Token: 0x060001CA RID: 458
		[Doc("StopRunning")]
		public abstract void StopRunning();

		// Token: 0x060001CB RID: 459
		[Doc("CapturePhoto", "CapturePhotoDiscussion")]
		public abstract Task<Texture2D> CapturePhoto();

		// Token: 0x060001CC RID: 460 RVA: 0x00013041 File Offset: 0x00011241
		public bool Equals(IMediaDevice other)
		{
			return other != null && other is CameraDevice && other.uniqueID == this.uniqueID;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00013061 File Offset: 0x00011261
		public override string ToString()
		{
			return "camera:" + this.uniqueID;
		}
	}
}
