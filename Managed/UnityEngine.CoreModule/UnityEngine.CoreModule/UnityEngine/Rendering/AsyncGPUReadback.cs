using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200030F RID: 783
	[StaticAccessor("AsyncGPUReadbackManager::GetInstance()", StaticAccessorType.Dot)]
	public static class AsyncGPUReadback
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x0002BC60 File Offset: 0x00029E60
		internal static void ValidateFormat(Texture src, GraphicsFormat dstformat)
		{
			GraphicsFormat format = GraphicsFormatUtility.GetFormat(src);
			bool flag = !SystemInfo.IsFormatSupported(format, FormatUsage.ReadPixels);
			if (flag)
			{
				Debug.LogError(string.Format("'{0}' doesn't support ReadPixels usage on this platform. Async GPU readback failed.", format));
			}
		}

		// Token: 0x06001AC6 RID: 6854
		[MethodImpl(4096)]
		public static extern void WaitAllRequests();

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0002BC9C File Offset: 0x00029E9C
		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0002BCC4 File Offset: 0x00029EC4
		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0002BCEC File Offset: 0x00029EEC
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0002BD14 File Offset: 0x00029F14
		public static AsyncGPUReadbackRequest Request(GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0002BD3C File Offset: 0x00029F3C
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0002BD64 File Offset: 0x00029F64
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			return AsyncGPUReadback.Request(src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0002BD8C File Offset: 0x00029F8C
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0002BDBC File Offset: 0x00029FBC
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_3(src, mipIndex, x, width, y, height, z, depth, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0002BDF0 File Offset: 0x00029FF0
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			return AsyncGPUReadback.Request(src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0002BE24 File Offset: 0x0002A024
		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, null);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0002BE64 File Offset: 0x0002A064
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_1(src, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0002BE98 File Offset: 0x0002A098
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_ComputeBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_1(src, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0002BF04 File Offset: 0x0002A104
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, GraphicsBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_GraphicsBuffer_2(src, size, offset, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0002BF3C File Offset: 0x0002A13C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_1(src, mipIndex, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0002BF70 File Offset: 0x0002A170
		public static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0002BF9C File Offset: 0x0002A19C
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_2(src, mipIndex, dstFormat, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0002BFDC File Offset: 0x0002A1DC
		public static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			return AsyncGPUReadback.RequestIntoNativeArray<T>(ref output, src, mipIndex, x, width, y, height, z, depth, GraphicsFormatUtility.GetGraphicsFormat(dstFormat, QualitySettings.activeColorSpace == ColorSpace.Linear), callback);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0002C014 File Offset: 0x0002A214
		public unsafe static AsyncGPUReadbackRequest RequestIntoNativeArray<T>(ref NativeArray<T> output, Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null) where T : struct
		{
			AsyncGPUReadback.ValidateFormat(src, dstFormat);
			AsyncRequestNativeArrayData asyncRequestNativeArrayData = AsyncRequestNativeArrayData.CreateAndCheckAccess<T>(output);
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = AsyncGPUReadback.Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat, &asyncRequestNativeArrayData);
			asyncGPUReadbackRequest.SetScriptingCallback(callback);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0002C060 File Offset: 0x0002A260
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_1([NotNull] ComputeBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_ComputeBuffer_1_Injected(buffer, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0002C078 File Offset: 0x0002A278
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_2([NotNull] ComputeBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_ComputeBuffer_2_Injected(src, size, offset, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0002C094 File Offset: 0x0002A294
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_1([NotNull] GraphicsBuffer buffer, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_1_Injected(buffer, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0002C0AC File Offset: 0x0002A2AC
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_GraphicsBuffer_2([NotNull] GraphicsBuffer src, int size, int offset, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_GraphicsBuffer_2_Injected(src, size, offset, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0002C0C8 File Offset: 0x0002A2C8
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_1([NotNull] Texture src, int mipIndex, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_1_Injected(src, mipIndex, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_2([NotNull] Texture src, int mipIndex, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_2_Injected(src, mipIndex, dstFormat, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0002C0FC File Offset: 0x0002A2FC
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_3([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_3_Injected(src, mipIndex, x, width, y, height, z, depth, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0002C120 File Offset: 0x0002A320
		[NativeMethod("Request")]
		private unsafe static AsyncGPUReadbackRequest Request_Internal_Texture_4([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest;
			AsyncGPUReadback.Request_Internal_Texture_4_Injected(src, mipIndex, x, width, y, height, z, depth, dstFormat, data, out asyncGPUReadbackRequest);
			return asyncGPUReadbackRequest;
		}

		// Token: 0x06001AE2 RID: 6882
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_ComputeBuffer_1_Injected(ComputeBuffer buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE3 RID: 6883
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_ComputeBuffer_2_Injected(ComputeBuffer src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE4 RID: 6884
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_1_Injected(GraphicsBuffer buffer, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE5 RID: 6885
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_GraphicsBuffer_2_Injected(GraphicsBuffer src, int size, int offset, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE6 RID: 6886
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_Texture_1_Injected(Texture src, int mipIndex, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE7 RID: 6887
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_Texture_2_Injected(Texture src, int mipIndex, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE8 RID: 6888
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_Texture_3_Injected(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);

		// Token: 0x06001AE9 RID: 6889
		[MethodImpl(4096)]
		private unsafe static extern void Request_Internal_Texture_4_Injected(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, GraphicsFormat dstFormat, AsyncRequestNativeArrayData* data, out AsyncGPUReadbackRequest ret);
	}
}
