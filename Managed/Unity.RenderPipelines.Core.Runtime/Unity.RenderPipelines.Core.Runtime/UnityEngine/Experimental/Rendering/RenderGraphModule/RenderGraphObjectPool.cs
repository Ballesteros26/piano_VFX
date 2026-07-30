using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000010 RID: 16
	public sealed class RenderGraphObjectPool
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000031CA File Offset: 0x000013CA
		internal RenderGraphObjectPool()
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000031F4 File Offset: 0x000013F4
		public T[] GetTempArray<T>(int size)
		{
			Stack<object> stack;
			if (!this.m_ArrayPool.TryGetValue(new ValueTuple<Type, int>(typeof(T), size), out stack))
			{
				stack = new Stack<object>();
				this.m_ArrayPool.Add(new ValueTuple<Type, int>(typeof(T), size), stack);
			}
			T[] array = ((stack.Count > 0) ? ((T[])stack.Pop()) : new T[size]);
			this.m_AllocatedArrays.Add(new ValueTuple<object, ValueTuple<Type, int>>(array, new ValueTuple<Type, int>(typeof(T), size)));
			return array;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003284 File Offset: 0x00001484
		public MaterialPropertyBlock GetTempMaterialPropertyBlock()
		{
			MaterialPropertyBlock materialPropertyBlock = RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.sharedPool.Get();
			materialPropertyBlock.Clear();
			this.m_AllocatedMaterialPropertyBlocks.Add(materialPropertyBlock);
			return materialPropertyBlock;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032B0 File Offset: 0x000014B0
		internal void ReleaseAllTempAlloc()
		{
			foreach (ValueTuple<object, ValueTuple<Type, int>> valueTuple in this.m_AllocatedArrays)
			{
				Stack<object> stack;
				this.m_ArrayPool.TryGetValue(valueTuple.Item2, out stack);
				stack.Push(valueTuple.Item1);
			}
			this.m_AllocatedArrays.Clear();
			foreach (MaterialPropertyBlock materialPropertyBlock in this.m_AllocatedMaterialPropertyBlocks)
			{
				RenderGraphObjectPool.SharedObjectPool<MaterialPropertyBlock>.sharedPool.Release(materialPropertyBlock);
			}
			this.m_AllocatedMaterialPropertyBlocks.Clear();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000337C File Offset: 0x0000157C
		internal T Get<T>() where T : new()
		{
			return RenderGraphObjectPool.SharedObjectPool<T>.sharedPool.Get();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003388 File Offset: 0x00001588
		internal void Release<T>(T value) where T : new()
		{
			RenderGraphObjectPool.SharedObjectPool<T>.sharedPool.Release(value);
		}

		// Token: 0x0400003D RID: 61
		private Dictionary<ValueTuple<Type, int>, Stack<object>> m_ArrayPool = new Dictionary<ValueTuple<Type, int>, Stack<object>>();

		// Token: 0x0400003E RID: 62
		private List<ValueTuple<object, ValueTuple<Type, int>>> m_AllocatedArrays = new List<ValueTuple<object, ValueTuple<Type, int>>>();

		// Token: 0x0400003F RID: 63
		private List<MaterialPropertyBlock> m_AllocatedMaterialPropertyBlocks = new List<MaterialPropertyBlock>();

		// Token: 0x020000B2 RID: 178
		private class SharedObjectPool<T> where T : new()
		{
			// Token: 0x06000489 RID: 1161 RVA: 0x0001117C File Offset: 0x0000F37C
			public T Get()
			{
				if (this.m_Pool.Count != 0)
				{
					return this.m_Pool.Pop();
				}
				return new T();
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0001119C File Offset: 0x0000F39C
			public void Release(T value)
			{
				this.m_Pool.Push(value);
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x0600048B RID: 1163 RVA: 0x000111AA File Offset: 0x0000F3AA
			public static RenderGraphObjectPool.SharedObjectPool<T> sharedPool
			{
				get
				{
					return RenderGraphObjectPool.SharedObjectPool<T>.s_Instance.Value;
				}
			}

			// Token: 0x04000255 RID: 597
			private Stack<T> m_Pool = new Stack<T>();

			// Token: 0x04000256 RID: 598
			private static readonly Lazy<RenderGraphObjectPool.SharedObjectPool<T>> s_Instance = new Lazy<RenderGraphObjectPool.SharedObjectPool<T>>();
		}
	}
}
