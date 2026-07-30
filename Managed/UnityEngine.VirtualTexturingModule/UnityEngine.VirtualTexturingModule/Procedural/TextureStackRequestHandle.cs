using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x0200000F RID: 15
	public struct TextureStackRequestHandle<T> : IEquatable<TextureStackRequestHandle<T>> where T : struct
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002490 File Offset: 0x00000690
		public static bool operator !=(TextureStackRequestHandle<T> h1, TextureStackRequestHandle<T> h2)
		{
			return !(h1 == h2);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024AC File Offset: 0x000006AC
		public override bool Equals(object obj)
		{
			return obj is TextureStackRequestHandle<T> && this == (TextureStackRequestHandle<T>)obj;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024DC File Offset: 0x000006DC
		public bool Equals(TextureStackRequestHandle<T> other)
		{
			return this == other;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000024FC File Offset: 0x000006FC
		public override int GetHashCode()
		{
			return this.payload.GetHashCode();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002520 File Offset: 0x00000720
		public static bool operator ==(TextureStackRequestHandle<T> h1, TextureStackRequestHandle<T> h2)
		{
			return h1.payload == h2.payload;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002543 File Offset: 0x00000743
		public void CompleteRequest(RequestStatus status)
		{
			Binding.UpdateRequestState((IntPtr)UnsafeUtility.AddressOf<TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<RequestStatus>(ref status), 1);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002568 File Offset: 0x00000768
		public static void CompleteRequests(NativeSlice<TextureStackRequestHandle<T>> requestHandles, NativeSlice<RequestStatus> status)
		{
			bool flag = requestHandles.Length != status.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", requestHandles.Length, status.Length));
			}
			Binding.UpdateRequestState((IntPtr)requestHandles.GetUnsafePtr<TextureStackRequestHandle<T>>(), (IntPtr)status.GetUnsafePtr<RequestStatus>(), requestHandles.Length);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025E0 File Offset: 0x000007E0
		public T GetRequestParameters()
		{
			T t = new T();
			Binding.GetRequestParameters((IntPtr)UnsafeUtility.AddressOf<TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<T>(ref t), 1);
			return t;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000261C File Offset: 0x0000081C
		public static void GetRequestParameters(NativeSlice<TextureStackRequestHandle<T>> handles, NativeSlice<T> requests)
		{
			bool flag = handles.Length != requests.Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", handles.Length, requests.Length));
			}
			Binding.GetRequestParameters((IntPtr)handles.GetUnsafePtr<TextureStackRequestHandle<T>>(), (IntPtr)requests.GetUnsafePtr<T>(), handles.Length);
		}

		// Token: 0x04000022 RID: 34
		internal RequestHandlePayload payload;
	}
}
