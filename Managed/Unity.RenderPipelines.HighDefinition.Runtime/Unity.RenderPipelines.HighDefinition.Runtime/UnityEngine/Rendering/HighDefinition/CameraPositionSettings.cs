using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	public struct CameraPositionSettings
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00052D94 File Offset: 0x00050F94
		public static CameraPositionSettings NewDefault()
		{
			return new CameraPositionSettings
			{
				mode = CameraPositionSettings.Mode.ComputeWorldToCameraMatrix,
				position = Vector3.zero,
				rotation = Quaternion.identity,
				worldToCameraMatrix = Matrix4x4.identity
			};
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00052DD6 File Offset: 0x00050FD6
		public Matrix4x4 ComputeWorldToCameraMatrix()
		{
			return GeometryUtils.CalculateWorldToCameraMatrixRHS(this.position, this.rotation);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00052DEC File Offset: 0x00050FEC
		public Matrix4x4 GetUsedWorldToCameraMatrix()
		{
			CameraPositionSettings.Mode mode = this.mode;
			if (mode == CameraPositionSettings.Mode.ComputeWorldToCameraMatrix)
			{
				return this.ComputeWorldToCameraMatrix();
			}
			if (mode != CameraPositionSettings.Mode.UseWorldToCameraMatrixField)
			{
				throw new ArgumentException();
			}
			return this.worldToCameraMatrix;
		}

		// Token: 0x04001012 RID: 4114
		[Obsolete("Since 2019.3, use CameraPositionSettings.NewDefault() instead.")]
		public static readonly CameraPositionSettings @default;

		// Token: 0x04001013 RID: 4115
		public CameraPositionSettings.Mode mode;

		// Token: 0x04001014 RID: 4116
		public Vector3 position;

		// Token: 0x04001015 RID: 4117
		public Quaternion rotation;

		// Token: 0x04001016 RID: 4118
		public Matrix4x4 worldToCameraMatrix;

		// Token: 0x02000295 RID: 661
		public enum Mode
		{
			// Token: 0x040016F1 RID: 5873
			ComputeWorldToCameraMatrix,
			// Token: 0x040016F2 RID: 5874
			UseWorldToCameraMatrixField
		}
	}
}
