using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AOT;
using UnityEngine;

namespace NatSuite.Devices.Internal
{
	// Token: 0x0200003F RID: 63
	public class NativeCameraDevice : CameraDevice
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000137AC File Offset: 0x000119AC
		public override string uniqueID
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				this.device.UniqueID(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000137D6 File Offset: 0x000119D6
		public override bool frontFacing
		{
			get
			{
				return this.device.FrontFacing();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000137E3 File Offset: 0x000119E3
		public override bool flashSupported
		{
			get
			{
				return this.device.FlashSupported();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000137F0 File Offset: 0x000119F0
		public override bool torchSupported
		{
			get
			{
				return this.device.TorchSupported();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000137FD File Offset: 0x000119FD
		public override bool exposureLockSupported
		{
			get
			{
				return this.device.ExposureLockSupported();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0001380A File Offset: 0x00011A0A
		public override bool focusLockSupported
		{
			get
			{
				return this.device.FocusLockSupported();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00013817 File Offset: 0x00011A17
		public override bool whiteBalanceLockSupported
		{
			get
			{
				return this.device.WhiteBalanceLockSupported();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00013824 File Offset: 0x00011A24
		[TupleElementNames(new string[] { "width", "height" })]
		public override ValueTuple<float, float> fieldOfView
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				float num;
				float num2;
				this.device.FieldOfView(out num, out num2);
				return new ValueTuple<float, float>(num, num2);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00013848 File Offset: 0x00011A48
		[TupleElementNames(new string[] { "min", "max" })]
		public override ValueTuple<float, float> exposureRange
		{
			[return: TupleElementNames(new string[] { "min", "max" })]
			get
			{
				float num;
				float num2;
				this.device.ExposureRange(out num, out num2);
				return new ValueTuple<float, float>(num, num2);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0001386C File Offset: 0x00011A6C
		[TupleElementNames(new string[] { "min", "max" })]
		public override ValueTuple<float, float> zoomRange
		{
			[return: TupleElementNames(new string[] { "min", "max" })]
			get
			{
				float num;
				float num2;
				this.device.ZoomRange(out num, out num2);
				return new ValueTuple<float, float>(num, num2);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00013890 File Offset: 0x00011A90
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000138B3 File Offset: 0x00011AB3
		[TupleElementNames(new string[] { "width", "height" })]
		public override ValueTuple<int, int> previewResolution
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				int num;
				int num2;
				this.device.PreviewResolution(out num, out num2);
				return new ValueTuple<int, int>(num, num2);
			}
			[param: TupleElementNames(new string[] { "width", "height" })]
			set
			{
				this.device.PreviewResolution(value.Item1, value.Item2);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000138CC File Offset: 0x00011ACC
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000138EF File Offset: 0x00011AEF
		[TupleElementNames(new string[] { "width", "height" })]
		public override ValueTuple<int, int> photoResolution
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get
			{
				int num;
				int num2;
				this.device.PhotoResolution(out num, out num2);
				return new ValueTuple<int, int>(num, num2);
			}
			[param: TupleElementNames(new string[] { "width", "height" })]
			set
			{
				this.device.PhotoResolution(value.Item1, value.Item2);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00013908 File Offset: 0x00011B08
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00013915 File Offset: 0x00011B15
		public override int frameRate
		{
			get
			{
				return this.device.Framerate();
			}
			set
			{
				this.device.Framerate(value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00013923 File Offset: 0x00011B23
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00013930 File Offset: 0x00011B30
		public override float exposureBias
		{
			get
			{
				return this.device.ExposureBias();
			}
			set
			{
				this.device.ExposureBias(value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0001393E File Offset: 0x00011B3E
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0001394B File Offset: 0x00011B4B
		public override bool exposureLock
		{
			get
			{
				return this.device.ExposureLock();
			}
			set
			{
				this.device.ExposureLock(value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00013959 File Offset: 0x00011B59
		[TupleElementNames(new string[] { "x", "y" })]
		public override ValueTuple<float, float> exposurePoint
		{
			[param: TupleElementNames(new string[] { "x", "y" })]
			set
			{
				this.device.ExposurePoint(value.Item1, value.Item2);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00013972 File Offset: 0x00011B72
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0001397F File Offset: 0x00011B7F
		public override FlashMode flashMode
		{
			get
			{
				return this.device.FlashMode();
			}
			set
			{
				this.device.FlashMode(value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0001398D File Offset: 0x00011B8D
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0001399A File Offset: 0x00011B9A
		public override bool focusLock
		{
			get
			{
				return this.device.FocusLock();
			}
			set
			{
				this.device.FocusLock(value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x06000258 RID: 600 RVA: 0x000139A8 File Offset: 0x00011BA8
		[TupleElementNames(new string[] { "x", "y" })]
		public override ValueTuple<float, float> focusPoint
		{
			[param: TupleElementNames(new string[] { "x", "y" })]
			set
			{
				this.device.FocusPoint(value.Item1, value.Item2);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000139C1 File Offset: 0x00011BC1
		// (set) Token: 0x0600025A RID: 602 RVA: 0x000139CE File Offset: 0x00011BCE
		public override bool torchEnabled
		{
			get
			{
				return this.device.TorchEnabled();
			}
			set
			{
				this.device.TorchEnabled(value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000139DC File Offset: 0x00011BDC
		// (set) Token: 0x0600025C RID: 604 RVA: 0x000139E9 File Offset: 0x00011BE9
		public override bool whiteBalanceLock
		{
			get
			{
				return this.device.WhiteBalanceLock();
			}
			set
			{
				this.device.WhiteBalanceLock(value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000139F7 File Offset: 0x00011BF7
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00013A04 File Offset: 0x00011C04
		public override float zoomRatio
		{
			get
			{
				return this.device.ZoomRatio();
			}
			set
			{
				this.device.ZoomRatio(value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00013A12 File Offset: 0x00011C12
		public override FrameOrientation orientation
		{
			set
			{
				this.device.Orientation(value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00013A20 File Offset: 0x00011C20
		public override bool running
		{
			get
			{
				return this.device.Running();
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00013A30 File Offset: 0x00011C30
		public override Task<Texture2D> StartRunning()
		{
			TaskCompletionSource<Texture2D> startTask = new TaskCompletionSource<Texture2D>();
			Action<IntPtr, int, int, long> action = delegate(IntPtr pixelBuffer, int width, int height, long timestamp)
			{
				bool flag = !this.previewTexture;
				this.previewTexture = this.previewTexture ?? new Texture2D(width, height, TextureFormat.RGBA32, false, false);
				this.previewTexture.LoadRawTextureData(pixelBuffer, width * height * 4);
				this.previewTexture.Apply();
				if (flag)
				{
					startTask.SetResult(this.previewTexture);
				}
			};
			this.device.StartRunning(new Bridge.FrameDelegate(NativeCameraDevice.OnFrame), (IntPtr)GCHandle.Alloc(action, GCHandleType.Normal));
			return startTask.Task;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00013A8E File Offset: 0x00011C8E
		public override void StopRunning()
		{
			this.device.StopRunning();
			global::UnityEngine.Object.Destroy(this.previewTexture);
			this.previewTexture = null;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00013AB0 File Offset: 0x00011CB0
		public override Task<Texture2D> CapturePhoto()
		{
			TaskCompletionSource<Texture2D> captureTask = new TaskCompletionSource<Texture2D>();
			GCHandle handle;
			Action<IntPtr, int, int, long> action = delegate(IntPtr pixelBuffer, int width, int height, long timestamp)
			{
				handle.Free();
				Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBA32, false);
				texture2D.LoadRawTextureData(pixelBuffer, width * height * 4);
				texture2D.Apply();
				captureTask.SetResult(texture2D);
			};
			handle = GCHandle.Alloc(action, GCHandleType.Normal);
			this.device.CapturePhoto(new Bridge.FrameDelegate(NativeCameraDevice.OnFrame), (IntPtr)handle);
			return captureTask.Task;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00013B18 File Offset: 0x00011D18
		public NativeCameraDevice(IntPtr device)
		{
			this.device = device;
			switch (Screen.orientation)
			{
			case ScreenOrientation.Portrait:
				this.orientation = FrameOrientation.Portrait;
				return;
			case ScreenOrientation.PortraitUpsideDown:
				this.orientation = FrameOrientation.PortraitUpsideDown;
				return;
			case ScreenOrientation.LandscapeLeft:
				this.orientation = FrameOrientation.LandscapeLeft;
				return;
			case ScreenOrientation.LandscapeRight:
				this.orientation = FrameOrientation.LandscapeRight;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00013B70 File Offset: 0x00011D70
		~NativeCameraDevice()
		{
			this.device.Dispose();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00013BA4 File Offset: 0x00011DA4
		[MonoPInvokeCallback(typeof(Bridge.FrameDelegate))]
		private static void OnFrame(IntPtr context, IntPtr pixelBuffer, int width, int height, long timestamp)
		{
			(((GCHandle)context).Target as Action<IntPtr, int, int, long>)(pixelBuffer, width, height, timestamp);
		}

		// Token: 0x040003CD RID: 973
		private readonly IntPtr device;

		// Token: 0x040003CE RID: 974
		private Texture2D previewTexture;
	}
}
