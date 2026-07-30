using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x02000010 RID: 16
	[VFXBinder("HDRP/HDRP Camera")]
	public class HDRPCameraBinder : VFXBinderBase
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002139 File Offset: 0x00000339
		public void SetCameraProperty(string name)
		{
			this.CameraProperty = name;
			this.UpdateSubProperties();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		private void UpdateSubProperties()
		{
			if (this.AdditionalData != null)
			{
				this.m_Camera = this.AdditionalData.GetComponent<Camera>();
			}
			this.m_Position = this.CameraProperty + "_transform_position";
			this.m_Angles = this.CameraProperty + "_transform_angles";
			this.m_Scale = this.CameraProperty + "_transform_scale";
			this.m_FieldOfView = this.CameraProperty + "_fieldOfView";
			this.m_NearPlane = this.CameraProperty + "_nearPlane";
			this.m_FarPlane = this.CameraProperty + "_farPlane";
			this.m_AspectRatio = this.CameraProperty + "_aspectRatio";
			this.m_Dimensions = this.CameraProperty + "_pixelDimensions";
			this.m_DepthBuffer = this.CameraProperty + "_depthBuffer";
			this.m_ColorBuffer = this.CameraProperty + "_colorBuffer";
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000228A File Offset: 0x0000048A
		private void RequestHDRPBuffersAccess(ref HDAdditionalCameraData.BufferAccess access)
		{
			access.RequestAccess(HDAdditionalCameraData.BufferAccessType.Color);
			access.RequestAccess(HDAdditionalCameraData.BufferAccessType.Depth);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000229A File Offset: 0x0000049A
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.AdditionalData != null)
			{
				this.AdditionalData.requestGraphicsBuffer += this.RequestHDRPBuffersAccess;
			}
			this.UpdateSubProperties();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022CD File Offset: 0x000004CD
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.AdditionalData != null)
			{
				this.AdditionalData.requestGraphicsBuffer -= this.RequestHDRPBuffersAccess;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022FA File Offset: 0x000004FA
		private void OnValidate()
		{
			this.UpdateSubProperties();
			if (this.AdditionalData != null)
			{
				this.AdditionalData.requestGraphicsBuffer += this.RequestHDRPBuffersAccess;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002328 File Offset: 0x00000528
		public override bool IsValid(VisualEffect component)
		{
			return this.AdditionalData != null && this.m_Camera != null && component.HasVector3(this.m_Position) && component.HasVector3(this.m_Angles) && component.HasVector3(this.m_Scale) && component.HasFloat(this.m_FieldOfView) && component.HasFloat(this.m_NearPlane) && component.HasFloat(this.m_FarPlane) && component.HasFloat(this.m_AspectRatio) && component.HasVector2(this.m_Dimensions) && component.HasTexture(this.m_DepthBuffer) && component.HasTexture(this.m_ColorBuffer);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002420 File Offset: 0x00000620
		public override void UpdateBinding(VisualEffect component)
		{
			RTHandle graphicsBuffer = this.AdditionalData.GetGraphicsBuffer(HDAdditionalCameraData.BufferAccessType.Depth);
			RTHandle graphicsBuffer2 = this.AdditionalData.GetGraphicsBuffer(HDAdditionalCameraData.BufferAccessType.Color);
			if (graphicsBuffer == null && graphicsBuffer2 == null)
			{
				return;
			}
			component.SetVector3(this.m_Position, this.AdditionalData.transform.position);
			component.SetVector3(this.m_Angles, this.AdditionalData.transform.eulerAngles);
			component.SetVector3(this.m_Scale, this.AdditionalData.transform.lossyScale);
			component.SetFloat(this.m_FieldOfView, 0.017453292f * this.m_Camera.fieldOfView);
			component.SetFloat(this.m_NearPlane, this.m_Camera.nearClipPlane);
			component.SetFloat(this.m_FarPlane, this.m_Camera.farClipPlane);
			component.SetFloat(this.m_AspectRatio, this.m_Camera.aspect);
			component.SetVector2(this.m_Dimensions, new Vector2((float)this.m_Camera.pixelWidth * graphicsBuffer.rtHandleProperties.rtHandleScale.x, (float)this.m_Camera.pixelHeight * graphicsBuffer.rtHandleProperties.rtHandleScale.y));
			if (graphicsBuffer != null)
			{
				component.SetTexture(this.m_DepthBuffer, graphicsBuffer.rt);
			}
			if (graphicsBuffer2 != null)
			{
				component.SetTexture(this.m_ColorBuffer, graphicsBuffer2.rt);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025AC File Offset: 0x000007AC
		public override string ToString()
		{
			return string.Format(string.Format("HDRP Camera : '{0}' -> {1}", (this.AdditionalData == null) ? "null" : this.AdditionalData.gameObject.name, this.CameraProperty), Array.Empty<object>());
		}

		// Token: 0x04000037 RID: 55
		public HDAdditionalCameraData AdditionalData;

		// Token: 0x04000038 RID: 56
		private Camera m_Camera;

		// Token: 0x04000039 RID: 57
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.CameraType" })]
		[SerializeField]
		private ExposedProperty CameraProperty = "Camera";

		// Token: 0x0400003A RID: 58
		private RTHandle m_Texture;

		// Token: 0x0400003B RID: 59
		private ExposedProperty m_Position;

		// Token: 0x0400003C RID: 60
		private ExposedProperty m_Angles;

		// Token: 0x0400003D RID: 61
		private ExposedProperty m_Scale;

		// Token: 0x0400003E RID: 62
		private ExposedProperty m_FieldOfView;

		// Token: 0x0400003F RID: 63
		private ExposedProperty m_NearPlane;

		// Token: 0x04000040 RID: 64
		private ExposedProperty m_FarPlane;

		// Token: 0x04000041 RID: 65
		private ExposedProperty m_AspectRatio;

		// Token: 0x04000042 RID: 66
		private ExposedProperty m_Dimensions;

		// Token: 0x04000043 RID: 67
		private ExposedProperty m_DepthBuffer;

		// Token: 0x04000044 RID: 68
		private ExposedProperty m_ColorBuffer;
	}
}
