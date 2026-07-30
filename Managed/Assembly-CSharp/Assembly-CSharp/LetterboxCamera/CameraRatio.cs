using System;
using UnityEngine;

namespace LetterboxCamera
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class CameraRatio
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00016BCD File Offset: 0x00014DCD
		public CameraRatio(Camera _camera, Vector2 _anchor)
		{
			this.camera = _camera;
			this.vectorAnchor = _anchor;
			this.originViewPort = this.camera.rect;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public void ResetOriginViewport()
		{
			this.originViewPort = this.camera.rect;
			this.SetAnchorBasedOnEnum(this.anchor);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00016C14 File Offset: 0x00014E14
		public void SetAnchorBasedOnEnum(CameraRatio.CameraAnchor _anchor)
		{
			switch (_anchor)
			{
			case CameraRatio.CameraAnchor.Center:
				this.vectorAnchor = new Vector2(0.5f, 0.5f);
				return;
			case CameraRatio.CameraAnchor.Top:
				this.vectorAnchor = new Vector2(0.5f, 1f);
				return;
			case CameraRatio.CameraAnchor.Bottom:
				this.vectorAnchor = new Vector2(0.5f, 0f);
				return;
			case CameraRatio.CameraAnchor.Left:
				this.vectorAnchor = new Vector2(0f, 0.5f);
				return;
			case CameraRatio.CameraAnchor.Right:
				this.vectorAnchor = new Vector2(1f, 0.5f);
				return;
			case CameraRatio.CameraAnchor.TopLeft:
				this.vectorAnchor = new Vector2(0f, 1f);
				return;
			case CameraRatio.CameraAnchor.TopRight:
				this.vectorAnchor = new Vector2(1f, 1f);
				return;
			case CameraRatio.CameraAnchor.BottomLeft:
				this.vectorAnchor = new Vector2(0f, 0f);
				return;
			case CameraRatio.CameraAnchor.BottomRight:
				this.vectorAnchor = new Vector2(1f, 0f);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00016D14 File Offset: 0x00014F14
		public void CalculateAndSetCameraRatio(float _width, float _height, bool _horizontalLetterbox)
		{
			Rect rect = default(Rect);
			if (_horizontalLetterbox)
			{
				rect.height = _height;
				rect.width = 1f;
			}
			else
			{
				rect.height = 1f;
				rect.width = _width;
			}
			Rect rect2 = default(Rect);
			Rect rect3 = default(Rect);
			rect2.width = this.originViewPort.width;
			rect2.height = this.originViewPort.width * (rect.height / rect.width);
			rect2.x = this.originViewPort.x;
			rect2.y = Mathf.Lerp(this.originViewPort.y, this.originViewPort.y + (this.originViewPort.height - rect2.height), this.vectorAnchor.y);
			rect3.width = this.originViewPort.height * (rect.width / rect.height);
			rect3.height = this.originViewPort.height;
			rect3.x = Mathf.Lerp(this.originViewPort.x, this.originViewPort.x + (this.originViewPort.width - rect3.width), this.vectorAnchor.x);
			rect3.y = this.originViewPort.y;
			if (rect2.height >= rect3.height && rect2.width >= rect3.width)
			{
				if (rect2.height <= this.originViewPort.height && rect2.width <= this.originViewPort.width)
				{
					this.camera.rect = rect2;
					return;
				}
				this.camera.rect = rect3;
				return;
			}
			else
			{
				if (rect3.height <= this.originViewPort.height && rect3.width <= this.originViewPort.width)
				{
					this.camera.rect = rect3;
					return;
				}
				this.camera.rect = rect2;
				return;
			}
		}

		// Token: 0x04000457 RID: 1111
		[Tooltip("The Camera assigned to have an automatically calculated Viewport Ratio")]
		public Camera camera;

		// Token: 0x04000458 RID: 1112
		[Tooltip("When a Camera Viewport is shrunk to fit a ratio, it will anchor the new Viewport Rectangle at the given point (relative to the original, unshrunk Viewport)")]
		public CameraRatio.CameraAnchor anchor;

		// Token: 0x04000459 RID: 1113
		[HideInInspector]
		public Vector2 vectorAnchor;

		// Token: 0x0400045A RID: 1114
		private Rect originViewPort;

		// Token: 0x02000091 RID: 145
		public enum CameraAnchor
		{
			// Token: 0x040004D7 RID: 1239
			Center,
			// Token: 0x040004D8 RID: 1240
			Top,
			// Token: 0x040004D9 RID: 1241
			Bottom,
			// Token: 0x040004DA RID: 1242
			Left,
			// Token: 0x040004DB RID: 1243
			Right,
			// Token: 0x040004DC RID: 1244
			TopLeft,
			// Token: 0x040004DD RID: 1245
			TopRight,
			// Token: 0x040004DE RID: 1246
			BottomLeft,
			// Token: 0x040004DF RID: 1247
			BottomRight
		}
	}
}
