using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200030D RID: 781
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	public struct AsyncGPUReadbackRequest
	{
		// Token: 0x06001AA6 RID: 6822 RVA: 0x0002BA8B File Offset: 0x00029C8B
		public void Update()
		{
			AsyncGPUReadbackRequest.Update_Injected(ref this);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0002BA93 File Offset: 0x00029C93
		public void WaitForCompletion()
		{
			AsyncGPUReadbackRequest.WaitForCompletion_Injected(ref this);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0002BA9C File Offset: 0x00029C9C
		public unsafe NativeArray<T> GetData<T>(int layer = 0) where T : struct
		{
			bool flag = !this.done || this.hasError;
			if (flag)
			{
				throw new InvalidOperationException("Cannot access the data as it is not available");
			}
			bool flag2 = layer < 0 || layer >= this.layerCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Layer index is out of range {0} / {1}", layer, this.layerCount));
			}
			int num = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataRaw(layer), this.layerDataSize / num, Allocator.None);
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0002BB2C File Offset: 0x00029D2C
		public bool done
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0002BB44 File Offset: 0x00029D44
		public bool hasError
		{
			get
			{
				return this.HasError();
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0002BB5C File Offset: 0x00029D5C
		public int layerCount
		{
			get
			{
				return this.GetLayerCount();
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0002BB74 File Offset: 0x00029D74
		public int layerDataSize
		{
			get
			{
				return this.GetLayerDataSize();
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0002BB8C File Offset: 0x00029D8C
		public int width
		{
			get
			{
				return this.GetWidth();
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0002BBA4 File Offset: 0x00029DA4
		public int height
		{
			get
			{
				return this.GetHeight();
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0002BBBC File Offset: 0x00029DBC
		public int depth
		{
			get
			{
				return this.GetDepth();
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0002BBD4 File Offset: 0x00029DD4
		private bool IsDone()
		{
			return AsyncGPUReadbackRequest.IsDone_Injected(ref this);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0002BBDC File Offset: 0x00029DDC
		private bool HasError()
		{
			return AsyncGPUReadbackRequest.HasError_Injected(ref this);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0002BBE4 File Offset: 0x00029DE4
		private int GetLayerCount()
		{
			return AsyncGPUReadbackRequest.GetLayerCount_Injected(ref this);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0002BBEC File Offset: 0x00029DEC
		private int GetLayerDataSize()
		{
			return AsyncGPUReadbackRequest.GetLayerDataSize_Injected(ref this);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0002BBF4 File Offset: 0x00029DF4
		private int GetWidth()
		{
			return AsyncGPUReadbackRequest.GetWidth_Injected(ref this);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0002BBFC File Offset: 0x00029DFC
		private int GetHeight()
		{
			return AsyncGPUReadbackRequest.GetHeight_Injected(ref this);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0002BC04 File Offset: 0x00029E04
		private int GetDepth()
		{
			return AsyncGPUReadbackRequest.GetDepth_Injected(ref this);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0002BC0C File Offset: 0x00029E0C
		internal void SetScriptingCallback(Action<AsyncGPUReadbackRequest> callback)
		{
			AsyncGPUReadbackRequest.SetScriptingCallback_Injected(ref this, callback);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0002BC15 File Offset: 0x00029E15
		private IntPtr GetDataRaw(int layer)
		{
			return AsyncGPUReadbackRequest.GetDataRaw_Injected(ref this, layer);
		}

		// Token: 0x06001AB9 RID: 6841
		[MethodImpl(4096)]
		private static extern void Update_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001ABA RID: 6842
		[MethodImpl(4096)]
		private static extern void WaitForCompletion_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001ABB RID: 6843
		[MethodImpl(4096)]
		private static extern bool IsDone_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001ABC RID: 6844
		[MethodImpl(4096)]
		private static extern bool HasError_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001ABD RID: 6845
		[MethodImpl(4096)]
		private static extern int GetLayerCount_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001ABE RID: 6846
		[MethodImpl(4096)]
		private static extern int GetLayerDataSize_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001ABF RID: 6847
		[MethodImpl(4096)]
		private static extern int GetWidth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001AC0 RID: 6848
		[MethodImpl(4096)]
		private static extern int GetHeight_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001AC1 RID: 6849
		[MethodImpl(4096)]
		private static extern int GetDepth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001AC2 RID: 6850
		[MethodImpl(4096)]
		private static extern void SetScriptingCallback_Injected(ref AsyncGPUReadbackRequest _unity_self, Action<AsyncGPUReadbackRequest> callback);

		// Token: 0x06001AC3 RID: 6851
		[MethodImpl(4096)]
		private static extern IntPtr GetDataRaw_Injected(ref AsyncGPUReadbackRequest _unity_self, int layer);

		// Token: 0x04000836 RID: 2102
		internal IntPtr m_Ptr;

		// Token: 0x04000837 RID: 2103
		internal int m_Version;
	}
}
