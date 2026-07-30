using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NatSuite.Devices.Internal;
using UnityEngine;

namespace NatSuite.Devices
{
	// Token: 0x02000039 RID: 57
	[Doc("WebCameraDevice")]
	public sealed class WebCameraDevice : ICameraDevice, IMediaDevice, IEquatable<IMediaDevice>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00013464 File Offset: 0x00011664
		[Doc("UniqueID")]
		public string uniqueID
		{
			get
			{
				return this.device.name;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00013480 File Offset: 0x00011680
		[Doc("FrontFacing")]
		public bool frontFacing
		{
			get
			{
				return this.device.isFrontFacing;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0001349B File Offset: 0x0001169B
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000134A3 File Offset: 0x000116A3
		[TupleElementNames(new string[] { "width", "height" })]
		[Doc("PreviewResolution")]
		public ValueTuple<int, int> previewResolution
		{
			[return: TupleElementNames(new string[] { "width", "height" })]
			get;
			[param: TupleElementNames(new string[] { "width", "height" })]
			set;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000134AC File Offset: 0x000116AC
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000134B4 File Offset: 0x000116B4
		[Doc("Framerate")]
		public int frameRate { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000134BD File Offset: 0x000116BD
		[Doc("Running")]
		public bool running
		{
			get
			{
				return this.webCamTexture.isPlaying;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000134CC File Offset: 0x000116CC
		[Doc("StartPreview")]
		public Task<Texture2D> StartRunning()
		{
			TaskCompletionSource<Texture2D> startTask = new TaskCompletionSource<Texture2D>();
			this.webCamTexture = new WebCamTexture(this.device.name, this.previewResolution.Item1, this.previewResolution.Item2, this.frameRate);
			this.webCamTexture.Play();
			this.attachment = new GameObject("NatDevice WebCameraDevice Helper").AddComponent<WebCameraDevice.WebCameraDeviceAttachment>();
			this.attachment.@delegate = delegate
			{
				if (this.webCamTexture.width == 16 || this.webCamTexture.height == 16)
				{
					return;
				}
				bool flag = !this.previewTexture;
				this.previewTexture = this.previewTexture ?? new Texture2D(this.webCamTexture.width, this.webCamTexture.height, TextureFormat.RGBA32, false, false);
				this.pixelBuffer = this.pixelBuffer ?? this.webCamTexture.GetPixels32();
				this.webCamTexture.GetPixels32(this.pixelBuffer);
				this.previewTexture.SetPixels32(this.pixelBuffer);
				this.previewTexture.Apply();
				if (flag)
				{
					startTask.SetResult(this.previewTexture);
				}
			};
			return startTask.Task;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00013568 File Offset: 0x00011768
		[Doc("StopRunning")]
		public void StopRunning()
		{
			this.attachment.@delegate = null;
			global::UnityEngine.Object.Destroy(this.attachment);
			this.webCamTexture.Stop();
			global::UnityEngine.Object.Destroy(this.webCamTexture);
			global::UnityEngine.Object.Destroy(this.previewTexture);
			this.webCamTexture = null;
			this.previewTexture = null;
			this.pixelBuffer = null;
			this.attachment = null;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000135C9 File Offset: 0x000117C9
		public WebCameraDevice(WebCamDevice device)
		{
			this.device = device;
			this.previewResolution = new ValueTuple<int, int>(1280, 720);
			this.frameRate = 30;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000135F5 File Offset: 0x000117F5
		public bool Equals(IMediaDevice other)
		{
			return other != null && other is WebCameraDevice && other.uniqueID == this.uniqueID;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00013615 File Offset: 0x00011815
		public override string ToString()
		{
			return "webcam:" + this.uniqueID;
		}

		// Token: 0x040003C4 RID: 964
		[Doc("WebCameraDeviceDevice")]
		public readonly WebCamDevice device;

		// Token: 0x040003C7 RID: 967
		private WebCamTexture webCamTexture;

		// Token: 0x040003C8 RID: 968
		private Texture2D previewTexture;

		// Token: 0x040003C9 RID: 969
		private Color32[] pixelBuffer;

		// Token: 0x040003CA RID: 970
		private WebCameraDevice.WebCameraDeviceAttachment attachment;

		// Token: 0x0200007A RID: 122
		private class WebCameraDeviceAttachment : MonoBehaviour
		{
			// Token: 0x06000365 RID: 869 RVA: 0x00017D58 File Offset: 0x00015F58
			private void Awake()
			{
				global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}

			// Token: 0x06000366 RID: 870 RVA: 0x00017F7C File Offset: 0x0001617C
			private void Update()
			{
				Action action = this.@delegate;
				if (action == null)
				{
					return;
				}
				action();
			}

			// Token: 0x04000499 RID: 1177
			public Action @delegate;
		}
	}
}
