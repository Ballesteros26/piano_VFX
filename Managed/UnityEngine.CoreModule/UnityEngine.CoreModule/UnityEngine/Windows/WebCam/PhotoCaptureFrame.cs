using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x0200023D RID: 573
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCaptureFrame.h")]
	[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
	public sealed class PhotoCaptureFrame : IDisposable
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00027CDB File Offset: 0x00025EDB
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x00027CE3 File Offset: 0x00025EE3
		public int dataLength { get; private set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00027CEC File Offset: 0x00025EEC
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x00027CF4 File Offset: 0x00025EF4
		public bool hasLocationData { get; private set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00027CFD File Offset: 0x00025EFD
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00027D05 File Offset: 0x00025F05
		public CapturePixelFormat pixelFormat { get; private set; }

		// Token: 0x060018C8 RID: 6344
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern int GetDataLength();

		// Token: 0x060018C9 RID: 6345
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern bool GetHasLocationData();

		// Token: 0x060018CA RID: 6346
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern CapturePixelFormat GetCapturePixelFormat();

		// Token: 0x060018CB RID: 6347 RVA: 0x00027D10 File Offset: 0x00025F10
		public bool TryGetCameraToWorldMatrix(out Matrix4x4 cameraToWorldMatrix)
		{
			cameraToWorldMatrix = Matrix4x4.identity;
			bool hasLocationData = this.hasLocationData;
			bool flag;
			if (hasLocationData)
			{
				cameraToWorldMatrix = this.GetCameraToWorldMatrix();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00027D4C File Offset: 0x00025F4C
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		[NativeName("GetCameraToWorld")]
		[ThreadAndSerializationSafe]
		private Matrix4x4 GetCameraToWorldMatrix()
		{
			Matrix4x4 matrix4x;
			this.GetCameraToWorldMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00027D64 File Offset: 0x00025F64
		public bool TryGetProjectionMatrix(out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool flag;
			if (hasLocationData)
			{
				projectionMatrix = this.GetProjection();
				flag = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00027DA0 File Offset: 0x00025FA0
		public bool TryGetProjectionMatrix(float nearClipPlane, float farClipPlane, out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool flag3;
			if (hasLocationData)
			{
				float num = 0.01f;
				bool flag = nearClipPlane < num;
				if (flag)
				{
					nearClipPlane = num;
				}
				bool flag2 = farClipPlane < nearClipPlane + num;
				if (flag2)
				{
					farClipPlane = nearClipPlane + num;
				}
				projectionMatrix = this.GetProjection();
				float num2 = 1f / (farClipPlane - nearClipPlane);
				float num3 = -(farClipPlane + nearClipPlane) * num2;
				float num4 = -(2f * farClipPlane * nearClipPlane) * num2;
				projectionMatrix.m22 = num3;
				projectionMatrix.m23 = num4;
				flag3 = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00027E34 File Offset: 0x00026034
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		[ThreadAndSerializationSafe]
		private Matrix4x4 GetProjection()
		{
			Matrix4x4 matrix4x;
			this.GetProjection_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00027E4C File Offset: 0x0002604C
		public void UploadImageDataToTexture(Texture2D targetTexture)
		{
			bool flag = targetTexture == null;
			if (flag)
			{
				throw new ArgumentNullException("targetTexture");
			}
			bool flag2 = this.pixelFormat > CapturePixelFormat.BGRA32;
			if (flag2)
			{
				throw new ArgumentException("Uploading PhotoCaptureFrame to a texture is only supported with BGRA32 CameraFrameFormat!");
			}
			this.UploadImageDataToTexture_Internal(targetTexture);
		}

		// Token: 0x060018D1 RID: 6353
		[NativeName("UploadImageDataToTexture")]
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		private extern void UploadImageDataToTexture_Internal(Texture2D targetTexture);

		// Token: 0x060018D2 RID: 6354
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		public extern IntPtr GetUnsafePointerToBuffer();

		// Token: 0x060018D3 RID: 6355 RVA: 0x00027E94 File Offset: 0x00026094
		public void CopyRawImageDataIntoBuffer(List<byte> byteBuffer)
		{
			bool flag = byteBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("byteBuffer");
			}
			byte[] array = new byte[this.dataLength];
			this.CopyRawImageDataIntoBuffer_Internal(array);
			bool flag2 = byteBuffer.Capacity < array.Length;
			if (flag2)
			{
				byteBuffer.Capacity = array.Length;
			}
			byteBuffer.Clear();
			byteBuffer.AddRange(array);
		}

		// Token: 0x060018D4 RID: 6356
		[NativeName("CopyRawImageDataIntoBuffer")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		internal extern void CopyRawImageDataIntoBuffer_Internal([Out] byte[] byteArray);

		// Token: 0x060018D5 RID: 6357 RVA: 0x00027EF4 File Offset: 0x000260F4
		internal PhotoCaptureFrame(IntPtr nativePtr)
		{
			this.m_NativePtr = nativePtr;
			this.dataLength = this.GetDataLength();
			this.hasLocationData = this.GetHasLocationData();
			this.pixelFormat = this.GetCapturePixelFormat();
			GC.AddMemoryPressure((long)this.dataLength);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00027F44 File Offset: 0x00026144
		private void Cleanup()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				GC.RemoveMemoryPressure((long)this.dataLength);
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
		}

		// Token: 0x060018D7 RID: 6359
		[NativeName("Dispose")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern void Dispose_Internal();

		// Token: 0x060018D8 RID: 6360 RVA: 0x00027F87 File Offset: 0x00026187
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00027F98 File Offset: 0x00026198
		~PhotoCaptureFrame()
		{
			this.Cleanup();
		}

		// Token: 0x060018DA RID: 6362
		[MethodImpl(4096)]
		private extern void GetCameraToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060018DB RID: 6363
		[MethodImpl(4096)]
		private extern void GetProjection_Injected(out Matrix4x4 ret);

		// Token: 0x040007A0 RID: 1952
		private IntPtr m_NativePtr;
	}
}
