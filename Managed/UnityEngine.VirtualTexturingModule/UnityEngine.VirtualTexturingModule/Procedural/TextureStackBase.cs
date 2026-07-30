using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000016 RID: 22
	public class TextureStackBase<T> : IDisposable where T : struct
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000027B0 File Offset: 0x000009B0
		public int PopRequests(NativeSlice<TextureStackRequestHandle<T>> requestHandles)
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
			}
			bool flag2 = requestHandles.Length < this.creationParams.maxActiveRequests;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Provided slice has invalid length ({0} given, {1} required).", requestHandles.Length, this.creationParams.maxActiveRequests));
			}
			return Binding.PopRequests(this.handle, (IntPtr)requestHandles.GetUnsafePtr<TextureStackRequestHandle<T>>());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002848 File Offset: 0x00000A48
		public bool IsValid()
		{
			return this.handle > 0UL;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002864 File Offset: 0x00000A64
		public TextureStackBase(string _name, CreationParameters _creationParams, bool gpuGeneration)
		{
			this.name = _name;
			this.creationParams = _creationParams;
			this.creationParams.borderSize = TextureStackBase<T>.borderSize;
			this.creationParams.gpuGeneration = (gpuGeneration ? 1 : 0);
			this.creationParams.Validate();
			this.handle = Binding.Create(this.creationParams);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void Dispose()
		{
			bool flag = this.IsValid();
			if (flag)
			{
				Binding.Destroy(this.handle);
				this.handle = 0UL;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028F8 File Offset: 0x00000AF8
		public void BindToMaterialPropertyBlock(MaterialPropertyBlock mpb)
		{
			bool flag = mpb == null;
			if (flag)
			{
				throw new ArgumentNullException("mbp");
			}
			bool flag2 = !this.IsValid();
			if (flag2)
			{
				throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
			}
			Binding.BindToMaterialPropertyBlock(this.handle, mpb, this.name);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002954 File Offset: 0x00000B54
		public void BindToMaterial(Material mat)
		{
			bool flag = mat == null;
			if (flag)
			{
				throw new ArgumentNullException("mat");
			}
			bool flag2 = !this.IsValid();
			if (flag2)
			{
				throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
			}
			Binding.BindToMaterial(this.handle, mat, this.name);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void BindGlobally()
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
			}
			Binding.BindGlobally(this.handle, this.name);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029F4 File Offset: 0x00000BF4
		public void RequestRegion(Rect r, int mipMap, int numMips)
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
			}
			Binding.RequestRegion(this.handle, r, mipMap, numMips);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A38 File Offset: 0x00000C38
		public void InvalidateRegion(Rect r, int mipMap, int numMips)
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
			}
			Binding.InvalidateRegion(this.handle, r, mipMap, numMips);
		}

		// Token: 0x04000046 RID: 70
		internal ulong handle;

		// Token: 0x04000047 RID: 71
		public static readonly int borderSize = 8;

		// Token: 0x04000048 RID: 72
		private string name;

		// Token: 0x04000049 RID: 73
		private CreationParameters creationParams;

		// Token: 0x0400004A RID: 74
		public const int AllMips = 2147483647;
	}
}
