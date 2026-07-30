using System;
using System.Collections;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Internal;
using UnityEngine;
using UnityEngine.Rendering;

namespace NatSuite.Recorders.Inputs
{
	// Token: 0x0200004E RID: 78
	[Doc("CameraInput")]
	public sealed class CameraInput : IDisposable
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000145B0 File Offset: 0x000127B0
		[Doc("CameraInputCtor")]
		public CameraInput(IMediaRecorder recorder, IClock clock, params Camera[] cameras)
		{
			Array.Sort<Camera>(cameras, (Camera a, Camera b) => (int)(10f * (a.depth - b.depth)));
			this.recorder = recorder;
			this.clock = clock;
			this.cameras = cameras;
			this.attachment = cameras[0].gameObject.AddComponent<CameraInput.CameraInputAttachment>();
			this.frameBuffer = RenderTexture.GetTemporary(new RenderTextureDescriptor(recorder.frameSize.Item1, recorder.frameSize.Item2, RenderTextureFormat.ARGB32, 24)
			{
				sRGB = true
			});
			this.readbackBuffer = (SystemInfo.supportsAsyncGPUReadback ? null : new Texture2D(this.frameBuffer.width, this.frameBuffer.height, TextureFormat.RGBA32, false, false));
			this.pixelBuffer = new byte[this.frameBuffer.width * this.frameBuffer.height * 4];
			this.attachment.StartCoroutine(this.OnFrame());
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000146A8 File Offset: 0x000128A8
		[Doc("AudioInputDispose")]
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.attachment);
			RenderTexture.ReleaseTemporary(this.frameBuffer);
			global::UnityEngine.Object.Destroy(this.readbackBuffer);
			this.pixelBuffer = null;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000146D2 File Offset: 0x000128D2
		private IEnumerator OnFrame()
		{
			WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
			for (;;)
			{
				CameraInput.<>c__DisplayClass11_0 CS$<>8__locals1 = new CameraInput.<>c__DisplayClass11_0();
				CS$<>8__locals1.<>4__this = this;
				yield return endOfFrame;
				int num = this.frameCount;
				this.frameCount = num + 1;
				if (num % (this.frameSkip + 1) == 0)
				{
					for (int i = 0; i < this.cameras.Length; i++)
					{
						RenderTexture targetTexture = this.cameras[i].targetTexture;
						this.cameras[i].targetTexture = this.frameBuffer;
						this.cameras[i].Render();
						this.cameras[i].targetTexture = targetTexture;
					}
					CS$<>8__locals1.timestamp = this.clock.timestamp;
					if (SystemInfo.supportsAsyncGPUReadback)
					{
						AsyncGPUReadback.Request(this.frameBuffer, 0, delegate(AsyncGPUReadbackRequest request)
						{
							if (CS$<>8__locals1.<>4__this.pixelBuffer != null)
							{
								request.GetData<byte>(0).CopyTo(CS$<>8__locals1.<>4__this.pixelBuffer);
								CS$<>8__locals1.<>4__this.recorder.CommitFrame<byte>(CS$<>8__locals1.<>4__this.pixelBuffer, CS$<>8__locals1.timestamp);
							}
						});
					}
					else
					{
						RenderTexture active = RenderTexture.active;
						RenderTexture.active = this.frameBuffer;
						this.readbackBuffer.ReadPixels(new Rect(0f, 0f, (float)this.frameBuffer.width, (float)this.frameBuffer.height), 0, 0, false);
						this.readbackBuffer.GetRawTextureData<byte>().CopyTo(this.pixelBuffer);
						this.recorder.CommitFrame<byte>(this.pixelBuffer, CS$<>8__locals1.timestamp);
						RenderTexture.active = active;
					}
					CS$<>8__locals1 = null;
				}
			}
			yield break;
		}

		// Token: 0x040003E8 RID: 1000
		[Doc("CameraInputFrameSkip", "CameraInputFrameSkipDiscussion")]
		public int frameSkip;

		// Token: 0x040003E9 RID: 1001
		private readonly IMediaRecorder recorder;

		// Token: 0x040003EA RID: 1002
		private readonly IClock clock;

		// Token: 0x040003EB RID: 1003
		private readonly Camera[] cameras;

		// Token: 0x040003EC RID: 1004
		private readonly CameraInput.CameraInputAttachment attachment;

		// Token: 0x040003ED RID: 1005
		private readonly RenderTexture frameBuffer;

		// Token: 0x040003EE RID: 1006
		private readonly Texture2D readbackBuffer;

		// Token: 0x040003EF RID: 1007
		private byte[] pixelBuffer;

		// Token: 0x040003F0 RID: 1008
		private int frameCount;

		// Token: 0x0200008A RID: 138
		private sealed class CameraInputAttachment : MonoBehaviour
		{
		}
	}
}
