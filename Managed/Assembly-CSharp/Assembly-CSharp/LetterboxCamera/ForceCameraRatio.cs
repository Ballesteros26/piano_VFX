using System;
using System.Collections.Generic;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class ForceCameraRatio : MonoBehaviour
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00016F18 File Offset: 0x00015118
		private void Start()
		{
			if (this.findCamerasAutomatically)
			{
				this.FindAllCamerasInScene();
			}
			else if (this.cameras == null || this.cameras.Count == 0)
			{
				this.cameras = new List<CameraRatio>();
			}
			this.ValidateCameraArray();
			for (int i = 0; i < this.cameras.Count; i++)
			{
				this.cameras[i].ResetOriginViewport();
			}
			if (this.createCameraForLetterBoxRendering)
			{
				this.letterBoxCamera = new GameObject().AddComponent<Camera>();
				this.letterBoxCamera.backgroundColor = this.letterBoxCameraColor;
				this.letterBoxCamera.cullingMask = 0;
				this.letterBoxCamera.depth = -100f;
				this.letterBoxCamera.farClipPlane = 1f;
				this.letterBoxCamera.useOcclusionCulling = false;
				this.letterBoxCamera.allowHDR = false;
				this.letterBoxCamera.clearFlags = CameraClearFlags.Color;
				this.letterBoxCamera.name = "Letter Box Camera";
				for (int j = 0; j < this.cameras.Count; j++)
				{
					if (this.cameras[j].camera.depth == -100f)
					{
						Debug.LogError(this.cameras[j].camera.name + " has a depth of -100 and may conflict with the Letter Box Camera in Forced Camera Ratio!");
					}
				}
			}
			if (this.forceRatioOnAwake)
			{
				this.CalculateAndSetAllCameraRatios();
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00017074 File Offset: 0x00015274
		private void Update()
		{
			if (this.listenForWindowChanges)
			{
				this.CalculateAndSetAllCameraRatios();
				if (this.letterBoxCamera != null)
				{
					this.letterBoxCamera.backgroundColor = this.letterBoxCameraColor;
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000170A4 File Offset: 0x000152A4
		private CameraRatio GetCameraRatioByCamera(Camera _camera)
		{
			if (this.cameras == null)
			{
				return null;
			}
			for (int i = 0; i < this.cameras.Count; i++)
			{
				if (this.cameras[i] != null && this.cameras[i].camera == _camera)
				{
					return this.cameras[i];
				}
			}
			return null;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00017108 File Offset: 0x00015308
		private void ValidateCameraArray()
		{
			for (int i = this.cameras.Count - 1; i >= 0; i--)
			{
				if (this.cameras[i].camera == null)
				{
					this.cameras.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00017154 File Offset: 0x00015354
		public void FindAllCamerasInScene()
		{
			Camera[] array = global::UnityEngine.Object.FindObjectsOfType<Camera>();
			this.cameras = new List<CameraRatio>();
			for (int i = 0; i < array.Length; i++)
			{
				if (this.createCameraForLetterBoxRendering || array[i] != this.letterBoxCamera)
				{
					this.cameras.Add(new CameraRatio(array[i], new Vector2(0.5f, 0.5f)));
				}
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000171BC File Offset: 0x000153BC
		public void CalculateAndSetAllCameraRatios()
		{
			float num = this.ratio.x / this.ratio.y;
			float num2 = (float)Screen.width / (float)Screen.height;
			bool flag = false;
			float num3 = num / num2;
			float num4 = num2 / num;
			if (num2 > num)
			{
				flag = false;
			}
			for (int i = 0; i < this.cameras.Count; i++)
			{
				this.cameras[i].SetAnchorBasedOnEnum(this.cameras[i].anchor);
				this.cameras[i].CalculateAndSetCameraRatio(num3, num4, flag);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00017254 File Offset: 0x00015454
		public void SetCameraAnchor(Camera _camera, Vector2 _anchor)
		{
			CameraRatio cameraRatioByCamera = this.GetCameraRatioByCamera(_camera);
			if (cameraRatioByCamera != null)
			{
				cameraRatioByCamera.vectorAnchor = _anchor;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00017273 File Offset: 0x00015473
		public CameraRatio[] GetCameras()
		{
			if (this.cameras == null)
			{
				this.cameras = new List<CameraRatio>();
			}
			return this.cameras.ToArray();
		}

		// Token: 0x0400045B RID: 1115
		public Vector2 ratio = new Vector2(16f, 9f);

		// Token: 0x0400045C RID: 1116
		public bool forceRatioOnAwake = true;

		// Token: 0x0400045D RID: 1117
		public bool listenForWindowChanges = true;

		// Token: 0x0400045E RID: 1118
		public bool createCameraForLetterBoxRendering = true;

		// Token: 0x0400045F RID: 1119
		public bool findCamerasAutomatically = true;

		// Token: 0x04000460 RID: 1120
		public Color letterBoxCameraColor = new Color(0f, 0f, 0f, 1f);

		// Token: 0x04000461 RID: 1121
		public List<CameraRatio> cameras;

		// Token: 0x04000462 RID: 1122
		public Camera letterBoxCamera;
	}
}
