using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000059 RID: 89
	public class LetterboxGameDemo : MonoBehaviour
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x00014CD3 File Offset: 0x00012ED3
		public void Start()
		{
			this.targetRatio = new Vector2(5f, 4f);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00014CEA File Offset: 0x00012EEA
		public void Update()
		{
			this.cameraManager.ratio = Vector2.Lerp(this.cameraManager.ratio, this.targetRatio, this.letterboxRate * Time.deltaTime);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00014D1C File Offset: 0x00012F1C
		public void OnGUI()
		{
			Rect rect = new Rect(20f, 10f, 200f, 50f);
			if (!this.inRatio)
			{
				GUI.color = new Color(1f, 0.1f, 0.1f);
				if (GUI.Button(rect, "Letterbox off :("))
				{
					this.inRatio = true;
					this.letterboxRate = this.letterboxInRate;
					this.targetRatio = new Vector2(16f, 9f);
					return;
				}
			}
			else if (this.inRatio)
			{
				GUI.color = new Color(0.1f, 1f, 0.1f);
				if (GUI.Button(rect, "LETTERBOX ON!"))
				{
					this.inRatio = false;
					this.letterboxRate = this.letterboxOutRate;
					this.targetRatio = new Vector2(5f, 4f);
				}
			}
		}

		// Token: 0x04000410 RID: 1040
		public ForceCameraRatio cameraManager;

		// Token: 0x04000411 RID: 1041
		public float letterboxInRate = 2f;

		// Token: 0x04000412 RID: 1042
		public float letterboxOutRate = 10f;

		// Token: 0x04000413 RID: 1043
		private float letterboxRate;

		// Token: 0x04000414 RID: 1044
		private Vector2 targetRatio;

		// Token: 0x04000415 RID: 1045
		private bool inRatio;
	}
}
