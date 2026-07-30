using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000074 RID: 116
	[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
	public static class UnsafeUtility
	{
		// Token: 0x06000153 RID: 339
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern int GetFieldOffsetInStruct(FieldInfo field);

		// Token: 0x06000154 RID: 340
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern int GetFieldOffsetInClass(FieldInfo field);

		// Token: 0x06000155 RID: 341 RVA: 0x00003B50 File Offset: 0x00001D50
		public static int GetFieldOffset(FieldInfo field)
		{
			bool isValueType = field.DeclaringType.IsValueType;
			int num;
			if (isValueType)
			{
				num = UnsafeUtility.GetFieldOffsetInStruct(field);
			}
			else
			{
				bool isClass = field.DeclaringType.IsClass;
				if (isClass)
				{
					num = UnsafeUtility.GetFieldOffsetInClass(field);
				}
				else
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003B94 File Offset: 0x00001D94
		public unsafe static void* PinGCObjectAndGetAddress(object target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemObjectAndGetAddress(target, out gcHandle);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public unsafe static void* PinGCArrayAndGetDataAddress(Array target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemArrayAndGetAddress(target, out gcHandle);
		}

		// Token: 0x06000158 RID: 344
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern void* PinSystemArrayAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x06000159 RID: 345
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern void* PinSystemObjectAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x0600015A RID: 346
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void ReleaseGCObject(ulong gcHandle);

		// Token: 0x0600015B RID: 347
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

		// Token: 0x0600015C RID: 348 RVA: 0x00003BCC File Offset: 0x00001DCC
		public static bool IsBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable(typeof(T));
		}

		// Token: 0x0600015D RID: 349
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		// Token: 0x0600015E RID: 350
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		// Token: 0x0600015F RID: 351 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public static bool IsValidAllocator(Allocator allocator)
		{
			return allocator > Allocator.None;
		}

		// Token: 0x06000160 RID: 352
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		// Token: 0x06000161 RID: 353
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		// Token: 0x06000162 RID: 354
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		// Token: 0x06000163 RID: 355
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		// Token: 0x06000164 RID: 356
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemSet(void* destination, byte value, long size);

		// Token: 0x06000165 RID: 357 RVA: 0x00003C06 File Offset: 0x00001E06
		public unsafe static void MemClear(void* destination, long size)
		{
			UnsafeUtility.MemSet(destination, 0, size);
		}

		// Token: 0x06000166 RID: 358
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		// Token: 0x06000167 RID: 359
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern int SizeOf(Type type);

		// Token: 0x06000168 RID: 360
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsBlittable(Type type);

		// Token: 0x06000169 RID: 361
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsUnmanaged(Type type);

		// Token: 0x0600016A RID: 362
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsValidNativeContainerElementType(Type type);

		// Token: 0x0600016B RID: 363
		[ThreadSafe]
		[MethodImpl(4096)]
		internal static extern void LogError(string msg, string filename, int linenumber);

		// Token: 0x0600016C RID: 364 RVA: 0x00003C14 File Offset: 0x00001E14
		private static bool IsBlittableValueType(Type t)
		{
			return t.IsValueType && UnsafeUtility.IsBlittable(t);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003C38 File Offset: 0x00001E38
		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			bool flag = !t.IsValueType;
			string text;
			if (flag)
			{
				text = string.Format("{0} is not blittable because it is not of value type ({1})\n", name, t);
			}
			else
			{
				bool isPrimitive = t.IsPrimitive;
				if (isPrimitive)
				{
					text = string.Format("{0} is not blittable ({1})\n", name, t);
				}
				else
				{
					string text2 = "";
					foreach (FieldInfo fieldInfo in t.GetFields(52))
					{
						bool flag2 = !UnsafeUtility.IsBlittableValueType(fieldInfo.FieldType);
						if (flag2)
						{
							text2 += UnsafeUtility.GetReasonForTypeNonBlittableImpl(fieldInfo.FieldType, string.Format("{0}.{1}", name, fieldInfo.Name));
						}
					}
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal static bool IsArrayBlittable(Array arr)
		{
			return UnsafeUtility.IsBlittableValueType(arr.GetType().GetElementType());
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003D10 File Offset: 0x00001F10
		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable<T>();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003D28 File Offset: 0x00001F28
		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type elementType = arr.GetType().GetElementType();
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(elementType, elementType.Name);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003D54 File Offset: 0x00001F54
		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003D80 File Offset: 0x00001F80
		internal static string GetReasonForTypeNonBlittable(Type t)
		{
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003DA0 File Offset: 0x00001FA0
		internal static string GetReasonForValueTypeNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003DCC File Offset: 0x00001FCC
		public static bool IsUnmanaged<T>()
		{
			int num = UnsafeUtility.IsUnmanagedCache<T>.value;
			bool flag = num == 1;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = num == 0;
				if (flag3)
				{
					num = (UnsafeUtility.IsUnmanagedCache<T>.value = (UnsafeUtility.IsUnmanaged(typeof(T)) ? 1 : (-1)));
				}
				flag2 = num == 1;
			}
			return flag2;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003E18 File Offset: 0x00002018
		public static bool IsValidNativeContainerElementType<T>()
		{
			int num = UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value;
			bool flag = num == -1;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = num == 0;
				if (flag3)
				{
					num = (UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value = (UnsafeUtility.IsValidNativeContainerElementType(typeof(T)) ? 1 : (-1)));
				}
				flag2 = num == 1;
			}
			return flag2;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00003E64 File Offset: 0x00002064
		[MethodImpl(256)]
		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			UnsafeUtility.InternalCopyPtrToStructure<T>(ptr, out output);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003E6F File Offset: 0x0000206F
		private unsafe static void InternalCopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003E7D File Offset: 0x0000207D
		[MethodImpl(256)]
		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			UnsafeUtility.InternalCopyStructureToPtr<T>(ref input, ptr);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003E6F File Offset: 0x0000206F
		private unsafe static void InternalCopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00003E88 File Offset: 0x00002088
		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + (long)index * (long)sizeof(T));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003E9C File Offset: 0x0000209C
		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + (long)index * (long)stride);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003EAB File Offset: 0x000020AB
		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)sizeof(T)) = value;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003EC0 File Offset: 0x000020C0
		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)stride) = value;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003ED0 File Offset: 0x000020D0
		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return (void*)(&output);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003ED3 File Offset: 0x000020D3
		public static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003EDB File Offset: 0x000020DB
		public static int AlignOf<T>() where T : struct
		{
			return 4;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003ED0 File Offset: 0x000020D0
		public static ref T As<U, T>(ref U from)
		{
			return ref from;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00003ED0 File Offset: 0x000020D0
		public unsafe static ref T AsRef<T>(void* ptr) where T : struct
		{
			return ref *(T*)ptr;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00003EE2 File Offset: 0x000020E2
		public unsafe static ref T ArrayElementAsRef<T>(void* ptr, int index) where T : struct
		{
			return ref *(T*)((byte*)ptr + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003EF4 File Offset: 0x000020F4
		public static int EnumToInt<T>(T enumValue) where T : struct, IConvertible
		{
			int num = 0;
			UnsafeUtility.InternalEnumToInt<T>(ref enumValue, ref num);
			return num;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003F13 File Offset: 0x00002113
		private static void InternalEnumToInt<T>(ref T enumValue, ref int intValue)
		{
			intValue = enumValue;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003F19 File Offset: 0x00002119
		public static bool EnumEquals<T>(T lhs, T rhs) where T : struct, IConvertible
		{
			return lhs == rhs;
		}

		// Token: 0x02000075 RID: 117
		internal struct IsUnmanagedCache<T>
		{
			// Token: 0x04000125 RID: 293
			internal static int value;
		}

		// Token: 0x02000076 RID: 118
		internal struct IsValidNativeContainerElementTypeCache<T>
		{
			// Token: 0x04000126 RID: 294
			internal static int value;
		}
	}
}
