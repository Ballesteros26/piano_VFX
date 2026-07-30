using System;
using System.Collections;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Internal;
using UnityEngine;

namespace NatSuite.Recorders.Inputs
{
	// Token: 0x0200004F RID: 79
	[Doc("WebCamTextureInput")]
	public class WebCamTextureInput : IDisposable
	{
		// Token: 0x060002AB RID: 683 RVA: 0x000146E4 File Offset: 0x000128E4
		[Doc("WebCamTextureInputCtor")]
		public WebCamTextureInput(IMediaRecorder recorder, IClock clock, WebCamTexture webCamTexture)
		{
			this.recorder = recorder;
			this.clock = clock;
			this.webCamTexture = webCamTexture;
			this.pixelBuffer = webCamTexture.GetPixels32();
			this.attachment = new GameObject("WebCamTextureInputAttachment").AddComponent<WebCamTextureInput.WebCamTextureInputAttachment>();
			this.attachment.StartCoroutine(this.OnFrame());
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0001473F File Offset: 0x0001293F
		[Doc("AudioInputDispose")]
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.attachment.gameObject);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00014751 File Offset: 0x00012951
		private IEnumerator OnFrame()
		{
			WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
			for (;;)
			{
				yield return endOfFrame;
				if (this.webCamTexture.didUpdateThisFrame)
				{
					this.webCamTexture.GetPixels32(this.pixelBuffer);
					this.recorder.CommitFrame<Color32>(this.pixelBuffer, this.clock.timestamp);
				}
			}
			yield break;
		}

		// Token: 0x040003F1 RID: 1009
		private readonly IMediaRecorder recorder;

		// Token: 0x040003F2 RID: 1010
		private readonly IClock clock;

		// Token: 0x040003F3 RID: 1011
		private readonly WebCamTexture webCamTexture;

		// Token: 0x040003F4 RID: 1012
		private readonly Color32[] pixelBuffer;

		// Token: 0x040003F5 RID: 1013
		private readonly WebCamTextureInput.WebCamTextureInputAttachment attachment;

		// Token: 0x0200008E RID: 142
		private sealed class WebCamTextureInputAttachment : MonoBehaviour
		{
		}
	}
}
