using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000061 RID: 97
	public class Demo : MonoBehaviour
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00015EC8 File Offset: 0x000140C8
		private void Awake()
		{
			this.currentDefault = this.startingDefault;
			this.EnableCameras(this.singlePlayerCameras, true);
			this.EnableCameras(this.twoPlayerCameras, false);
			this.EnableCameras(this.fourPlayerCameras, false);
			this.EnableCameras(this.crazyCameras, false);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00015F18 File Offset: 0x00014118
		private void EnableCameras(Camera[] _cameras, bool _enable)
		{
			for (int i = 0; i < _cameras.Length; i++)
			{
				_cameras[i].enabled = _enable;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00015F3C File Offset: 0x0001413C
		private void OnGUI()
		{
			if (Input.mousePosition.x < 0f || Input.mousePosition.x > 120f || Input.mousePosition.y < 0f || Input.mousePosition.y > (float)Screen.height)
			{
				this.guiColorProgress = Util.Clamp(0.2f, 1f, this.guiColorProgress - Time.deltaTime * 2f);
			}
			else
			{
				this.guiColorProgress = Util.Clamp(0.2f, 1f, this.guiColorProgress + Time.deltaTime * 2f);
			}
			GUI.color = Color.Lerp(this.guiOffColor, this.guiOnColor, this.guiColorProgress);
			GUI.Label(new Rect(10f, 10f, 100f, 25f), "Camera Ratio:");
			string text = GUI.TextField(new Rect(10f, 35f, 45f, 25f), this.forceRatio.ratio.x.ToString());
			string text2 = GUI.TextField(new Rect(65f, 35f, 45f, 25f), this.forceRatio.ratio.y.ToString());
			bool flag = false;
			if (GUI.Button(new Rect(10f, 70f, 45f, 25f), "<"))
			{
				flag = true;
				this.currentDefault--;
				if (this.currentDefault < 0)
				{
					this.currentDefault = this.defaultRatios.Length - 1;
				}
				this.forceRatio.ratio = new Vector2(this.defaultRatios[this.currentDefault].x, this.defaultRatios[this.currentDefault].y);
			}
			if (GUI.Button(new Rect(65f, 70f, 45f, 25f), ">"))
			{
				flag = true;
				this.currentDefault++;
				if (this.currentDefault >= this.defaultRatios.Length)
				{
					this.currentDefault = 0;
				}
				this.forceRatio.ratio = new Vector2(this.defaultRatios[this.currentDefault].x, this.defaultRatios[this.currentDefault].y);
			}
			float num;
			float num2;
			if (!flag && float.TryParse(text, out num) && float.TryParse(text2, out num2))
			{
				this.forceRatio.ratio = new Vector2(num, num2);
			}
			if (GUI.Button(new Rect(10f, 105f, 100f, 25f), "Single Camera"))
			{
				this.EnableCameras(this.singlePlayerCameras, true);
				this.EnableCameras(this.twoPlayerCameras, false);
				this.EnableCameras(this.fourPlayerCameras, false);
				this.EnableCameras(this.crazyCameras, false);
			}
			if (GUI.Button(new Rect(10f, 140f, 100f, 25f), "Two Player"))
			{
				this.EnableCameras(this.singlePlayerCameras, false);
				this.EnableCameras(this.twoPlayerCameras, true);
				this.EnableCameras(this.fourPlayerCameras, false);
				this.EnableCameras(this.crazyCameras, false);
			}
			if (GUI.Button(new Rect(10f, 175f, 100f, 25f), "Four Player"))
			{
				this.EnableCameras(this.singlePlayerCameras, false);
				this.EnableCameras(this.twoPlayerCameras, false);
				this.EnableCameras(this.fourPlayerCameras, true);
				this.EnableCameras(this.crazyCameras, false);
			}
			if (GUI.Button(new Rect(10f, 215f, 100f, 25f), "Various Angles"))
			{
				this.EnableCameras(this.singlePlayerCameras, false);
				this.EnableCameras(this.twoPlayerCameras, false);
				this.EnableCameras(this.fourPlayerCameras, false);
				this.EnableCameras(this.crazyCameras, true);
			}
			GUI.Label(new Rect(10f, 250f, 100f, 25f), "Letterbox Color");
			string text3;
			if (this.forceRatio.letterBoxCameraColor.r == 0f)
			{
				text3 = "";
			}
			else
			{
				text3 = (this.forceRatio.letterBoxCameraColor.r * 255f).ToString();
			}
			string text4;
			if (this.forceRatio.letterBoxCameraColor.g == 0f)
			{
				text4 = "";
			}
			else
			{
				text4 = (this.forceRatio.letterBoxCameraColor.g * 255f).ToString();
			}
			string text5;
			if (this.forceRatio.letterBoxCameraColor.b == 0f)
			{
				text5 = "";
			}
			else
			{
				text5 = (this.forceRatio.letterBoxCameraColor.b * 255f).ToString();
			}
			string text6 = GUI.TextField(new Rect(10f, 275f, 35f, 25f), text3);
			string text7 = GUI.TextField(new Rect(45f, 275f, 35f, 25f), text4);
			string text8 = GUI.TextField(new Rect(80f, 275f, 35f, 25f), text5);
			if (text6 == "")
			{
				text6 = "0";
			}
			if (text7 == "")
			{
				text7 = "0";
			}
			if (text8 == "")
			{
				text8 = "0";
			}
			float num3;
			float num4;
			float num5;
			if (float.TryParse(text6, out num3) && float.TryParse(text7, out num4) && float.TryParse(text8, out num5))
			{
				if (num3 > 0f)
				{
					num3 /= 255f;
				}
				else
				{
					num3 = 0f;
				}
				if (num4 > 0f)
				{
					num4 /= 255f;
				}
				else
				{
					num4 = 0f;
				}
				if (num5 > 0f)
				{
					num5 /= 255f;
				}
				else
				{
					num5 = 0f;
				}
				this.forceRatio.letterBoxCameraColor = new Color(Util.Clamp(0f, 1f, num3), Util.Clamp(0f, 1f, num4), Util.Clamp(0f, 1f, num5), 1f);
			}
		}

		// Token: 0x04000444 RID: 1092
		public ForceCameraRatio forceRatio;

		// Token: 0x04000445 RID: 1093
		public Camera[] fourPlayerCameras;

		// Token: 0x04000446 RID: 1094
		public Camera[] twoPlayerCameras;

		// Token: 0x04000447 RID: 1095
		public Camera[] singlePlayerCameras;

		// Token: 0x04000448 RID: 1096
		public Camera[] crazyCameras;

		// Token: 0x04000449 RID: 1097
		public int startingDefault;

		// Token: 0x0400044A RID: 1098
		public Vector2[] defaultRatios;

		// Token: 0x0400044B RID: 1099
		private Color guiOnColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x0400044C RID: 1100
		private Color guiOffColor = new Color(1f, 1f, 1f, 0.2f);

		// Token: 0x0400044D RID: 1101
		private float guiColorProgress = 1f;

		// Token: 0x0400044E RID: 1102
		private int currentDefault;
	}
}
