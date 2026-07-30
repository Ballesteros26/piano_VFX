using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x0200021F RID: 543
	[NativeType(Header = "Runtime/Transform/ScriptBindings/TransformAccess.bindings.h", CodegenOptions = CodegenOptions.Custom)]
	public struct TransformAccessArray : IDisposable
	{
		// Token: 0x06001814 RID: 6164 RVA: 0x00026E30 File Offset: 0x00025030
		public TransformAccessArray(Transform[] transforms, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(transforms.Length, desiredJobCount, out this);
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00026E4B File Offset: 0x0002504B
		public TransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(capacity, desiredJobCount, out this);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00026E57 File Offset: 0x00025057
		public static void Allocate(int capacity, int desiredJobCount, out TransformAccessArray array)
		{
			array.m_TransformArray = TransformAccessArray.Create(capacity, desiredJobCount);
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00026E68 File Offset: 0x00025068
		public bool isCreated
		{
			get
			{
				return this.m_TransformArray != IntPtr.Zero;
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00026E8A File Offset: 0x0002508A
		public void Dispose()
		{
			TransformAccessArray.DestroyTransformAccessArray(this.m_TransformArray);
			this.m_TransformArray = IntPtr.Zero;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00026EA4 File Offset: 0x000250A4
		internal IntPtr GetTransformAccessArrayForSchedule()
		{
			return this.m_TransformArray;
		}

		// Token: 0x170004D1 RID: 1233
		public Transform this[int index]
		{
			get
			{
				return TransformAccessArray.GetTransform(this.m_TransformArray, index);
			}
			set
			{
				TransformAccessArray.SetTransform(this.m_TransformArray, index, value);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x00026EEC File Offset: 0x000250EC
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x00026F09 File Offset: 0x00025109
		public int capacity
		{
			get
			{
				return TransformAccessArray.GetCapacity(this.m_TransformArray);
			}
			set
			{
				TransformAccessArray.SetCapacity(this.m_TransformArray, value);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x00026F1C File Offset: 0x0002511C
		public int length
		{
			get
			{
				return TransformAccessArray.GetLength(this.m_TransformArray);
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00026F39 File Offset: 0x00025139
		public void Add(Transform transform)
		{
			TransformAccessArray.Add(this.m_TransformArray, transform);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00026F49 File Offset: 0x00025149
		public void RemoveAtSwapBack(int index)
		{
			TransformAccessArray.RemoveAtSwapBack(this.m_TransformArray, index);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00026F59 File Offset: 0x00025159
		public void SetTransforms(Transform[] transforms)
		{
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06001822 RID: 6178
		[NativeMethod(Name = "TransformAccessArrayBindings::Create", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Create(int capacity, int desiredJobCount);

		// Token: 0x06001823 RID: 6179
		[NativeMethod(Name = "DestroyTransformAccessArray", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void DestroyTransformAccessArray(IntPtr transformArray);

		// Token: 0x06001824 RID: 6180
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransforms", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void SetTransforms(IntPtr transformArrayIntPtr, Transform[] transforms);

		// Token: 0x06001825 RID: 6181
		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransform", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void Add(IntPtr transformArrayIntPtr, Transform transform);

		// Token: 0x06001826 RID: 6182
		[NativeMethod(Name = "TransformAccessArrayBindings::RemoveAtSwapBack", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void RemoveAtSwapBack(IntPtr transformArrayIntPtr, int index);

		// Token: 0x06001827 RID: 6183
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedTransformAccess", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern IntPtr GetSortedTransformAccess(IntPtr transformArrayIntPtr);

		// Token: 0x06001828 RID: 6184
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedToUserIndex", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern IntPtr GetSortedToUserIndex(IntPtr transformArrayIntPtr);

		// Token: 0x06001829 RID: 6185
		[NativeMethod(Name = "TransformAccessArrayBindings::GetLength", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern int GetLength(IntPtr transformArrayIntPtr);

		// Token: 0x0600182A RID: 6186
		[NativeMethod(Name = "TransformAccessArrayBindings::GetCapacity", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern int GetCapacity(IntPtr transformArrayIntPtr);

		// Token: 0x0600182B RID: 6187
		[NativeMethod(Name = "TransformAccessArrayBindings::SetCapacity", IsFreeFunction = true)]
		[MethodImpl(4096)]
		internal static extern void SetCapacity(IntPtr transformArrayIntPtr, int capacity);

		// Token: 0x0600182C RID: 6188
		[NativeMethod(Name = "TransformAccessArrayBindings::GetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern Transform GetTransform(IntPtr transformArrayIntPtr, int index);

		// Token: 0x0600182D RID: 6189
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransform", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal static extern void SetTransform(IntPtr transformArrayIntPtr, int index, Transform transform);

		// Token: 0x04000761 RID: 1889
		private IntPtr m_TransformArray;
	}
}
