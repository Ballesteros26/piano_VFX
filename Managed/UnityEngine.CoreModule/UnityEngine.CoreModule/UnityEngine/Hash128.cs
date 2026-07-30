using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000157 RID: 343
	[NativeHeader("Runtime/Utilities/Hash128.h")]
	[NativeHeader("Runtime/Export/Hashing/Hash128.bindings.h")]
	[UsedByNativeCode]
	[Serializable]
	public struct Hash128 : IComparable, IComparable<Hash128>, IEquatable<Hash128>
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x00014D5C File Offset: 0x00012F5C
		public Hash128(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
		{
			this.m_u32_0 = u32_0;
			this.m_u32_1 = u32_1;
			this.m_u32_2 = u32_2;
			this.m_u32_3 = u32_3;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00014D7C File Offset: 0x00012F7C
		public unsafe Hash128(ulong u64_0, ulong u64_1)
		{
			uint* ptr = (uint*)(&u64_0);
			uint* ptr2 = (uint*)(&u64_1);
			this.m_u32_0 = *ptr;
			this.m_u32_1 = ptr[1];
			this.m_u32_2 = *ptr2;
			this.m_u32_3 = ptr2[1];
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00014DB8 File Offset: 0x00012FB8
		internal unsafe ulong u64_0
		{
			get
			{
				fixed (uint* ptr = &this.m_u32_0)
				{
					uint* ptr2 = ptr;
					return (ulong)(*(long*)ptr2);
				}
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00014DD8 File Offset: 0x00012FD8
		internal unsafe ulong u64_1
		{
			get
			{
				fixed (uint* ptr = &this.m_u32_2)
				{
					uint* ptr2 = ptr;
					return (ulong)(*(long*)ptr2);
				}
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00014DF6 File Offset: 0x00012FF6
		public bool isValid
		{
			get
			{
				return this.m_u32_0 != 0U || this.m_u32_1 != 0U || this.m_u32_2 != 0U || this.m_u32_3 > 0U;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00014E1C File Offset: 0x0001301C
		public int CompareTo(Hash128 rhs)
		{
			bool flag = this < rhs;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				bool flag2 = this > rhs;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00014E58 File Offset: 0x00013058
		public override string ToString()
		{
			return Hash128.Hash128ToStringImpl(this);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00014E78 File Offset: 0x00013078
		[FreeFunction("StringToHash128", IsThreadSafe = true)]
		public static Hash128 Parse(string hashString)
		{
			Hash128 hash;
			Hash128.Parse_Injected(hashString, out hash);
			return hash;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00014E8E File Offset: 0x0001308E
		[FreeFunction("Hash128ToString", IsThreadSafe = true)]
		private static string Hash128ToStringImpl(Hash128 hash)
		{
			return Hash128.Hash128ToStringImpl_Injected(ref hash);
		}

		// Token: 0x06000FB2 RID: 4018
		[FreeFunction("ComputeHash128FromScriptString", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void ComputeFromString(string data, ref Hash128 hash);

		// Token: 0x06000FB3 RID: 4019
		[FreeFunction("ComputeHash128FromScriptPointer", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void ComputeFromPtr(IntPtr data, int start, int count, int elemSize, ref Hash128 hash);

		// Token: 0x06000FB4 RID: 4020
		[FreeFunction("ComputeHash128FromScriptArray", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void ComputeFromArray(Array data, int start, int count, int elemSize, ref Hash128 hash);

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00014E98 File Offset: 0x00013098
		public static Hash128 Compute(string data)
		{
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromString(data, ref hash);
			return hash;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00014EBC File Offset: 0x000130BC
		public static Hash128 Compute<T>(NativeArray<T> data) where T : struct
		{
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, data.Length, UnsafeUtility.SizeOf<T>(), ref hash);
			return hash;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00014EF8 File Offset: 0x000130F8
		public static Hash128 Compute<T>(NativeArray<T> data, int start, int count) where T : struct
		{
			bool flag = start < 0 || count < 0 || start + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), start, count, UnsafeUtility.SizeOf<T>(), ref hash);
			return hash;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00014F64 File Offset: 0x00013164
		public static Hash128 Compute<T>(T[] data) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Compute must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromArray(data, 0, data.Length, UnsafeUtility.SizeOf<T>(), ref hash);
			return hash;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00014FB4 File Offset: 0x000131B4
		public static Hash128 Compute<T>(T[] data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Compute must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromArray(data, start, count, UnsafeUtility.SizeOf<T>(), ref hash);
			return hash;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00015038 File Offset: 0x00013238
		public static Hash128 Compute<T>(List<T> data) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Compute", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), 0, data.Count, UnsafeUtility.SizeOf<T>(), ref hash);
			return hash;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000150A0 File Offset: 0x000132A0
		public static Hash128 Compute<T>(List<T> data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Compute", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), start, count, UnsafeUtility.SizeOf<T>(), ref hash);
			return hash;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00015138 File Offset: 0x00013338
		public unsafe static Hash128 Compute<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr = &val)
			{
				void* ptr2 = (void*)ptr;
				Hash128 hash = default(Hash128);
				Hash128.ComputeFromPtr((IntPtr)ptr2, 0, 1, UnsafeUtility.SizeOf<T>(), ref hash);
				return hash;
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00015170 File Offset: 0x00013370
		public static Hash128 Compute(int val)
		{
			Hash128 hash = default(Hash128);
			hash.Append(val);
			return hash;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00015194 File Offset: 0x00013394
		public static Hash128 Compute(float val)
		{
			Hash128 hash = default(Hash128);
			hash.Append(val);
			return hash;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000151B8 File Offset: 0x000133B8
		public unsafe static Hash128 Compute(void* data, ulong size)
		{
			Hash128 hash = default(Hash128);
			Hash128.ComputeFromPtr(new IntPtr(data), 0, (int)size, 1, ref hash);
			return hash;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000151E5 File Offset: 0x000133E5
		public void Append(string data)
		{
			Hash128.ComputeFromString(data, ref this);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000151F0 File Offset: 0x000133F0
		public void Append<T>(NativeArray<T> data) where T : struct
		{
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, data.Length, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00015214 File Offset: 0x00013414
		public void Append<T>(NativeArray<T> data, int start, int count) where T : struct
		{
			bool flag = start < 0 || count < 0 || start + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00015274 File Offset: 0x00013474
		public void Append<T>(T[] data) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Append must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			Hash128.ComputeFromArray(data, 0, data.Length, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000152B8 File Offset: 0x000134B8
		public void Append<T>(T[] data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Append must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromArray(data, start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0001532C File Offset: 0x0001352C
		public void Append<T>(List<T> data) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Append", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), 0, data.Count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00015384 File Offset: 0x00013584
		public void Append<T>(List<T> data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Append", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0001540C File Offset: 0x0001360C
		public unsafe void Append<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr = &val)
			{
				void* ptr2 = (void*)ptr;
				Hash128.ComputeFromPtr((IntPtr)ptr2, 0, 1, UnsafeUtility.SizeOf<T>(), ref this);
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00015438 File Offset: 0x00013638
		public void Append(int val)
		{
			this.ShortHash4((uint)val);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00015443 File Offset: 0x00013643
		public unsafe void Append(float val)
		{
			this.ShortHash4(*(uint*)(&val));
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00015451 File Offset: 0x00013651
		public unsafe void Append(void* data, ulong size)
		{
			Hash128.ComputeFromPtr(new IntPtr(data), 0, (int)size, 1, ref this);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00015468 File Offset: 0x00013668
		public override bool Equals(object obj)
		{
			return obj is Hash128 && this == (Hash128)obj;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00015498 File Offset: 0x00013698
		public bool Equals(Hash128 obj)
		{
			return this == obj;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000154B8 File Offset: 0x000136B8
		public override int GetHashCode()
		{
			return this.m_u32_0.GetHashCode() ^ this.m_u32_1.GetHashCode() ^ this.m_u32_2.GetHashCode() ^ this.m_u32_3.GetHashCode();
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000154FC File Offset: 0x000136FC
		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is Hash128);
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				Hash128 hash = (Hash128)obj;
				num = this.CompareTo(hash);
			}
			return num;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00015538 File Offset: 0x00013738
		public static bool operator ==(Hash128 hash1, Hash128 hash2)
		{
			return hash1.m_u32_0 == hash2.m_u32_0 && hash1.m_u32_1 == hash2.m_u32_1 && hash1.m_u32_2 == hash2.m_u32_2 && hash1.m_u32_3 == hash2.m_u32_3;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00015588 File Offset: 0x00013788
		public static bool operator !=(Hash128 hash1, Hash128 hash2)
		{
			return !(hash1 == hash2);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000155A4 File Offset: 0x000137A4
		public static bool operator <(Hash128 x, Hash128 y)
		{
			bool flag = x.m_u32_0 != y.m_u32_0;
			bool flag2;
			if (flag)
			{
				flag2 = x.m_u32_0 < y.m_u32_0;
			}
			else
			{
				bool flag3 = x.m_u32_1 != y.m_u32_1;
				if (flag3)
				{
					flag2 = x.m_u32_1 < y.m_u32_1;
				}
				else
				{
					bool flag4 = x.m_u32_2 != y.m_u32_2;
					if (flag4)
					{
						flag2 = x.m_u32_2 < y.m_u32_2;
					}
					else
					{
						flag2 = x.m_u32_3 < y.m_u32_3;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00015638 File Offset: 0x00013838
		public static bool operator >(Hash128 x, Hash128 y)
		{
			bool flag = x < y;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = x == y;
				flag2 = !flag3;
			}
			return flag2;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0001566C File Offset: 0x0001386C
		private void ShortHash4(uint data)
		{
			ulong u64_ = this.u64_0;
			ulong u64_2 = this.u64_1;
			ulong num = 16045690984833335023UL;
			ulong num2 = 16045690984833335023UL;
			num2 += 288230376151711744UL;
			num += (ulong)data;
			Hash128.ShortEnd(ref u64_, ref u64_2, ref num, ref num2);
			this.m_u32_0 = (uint)u64_;
			this.m_u32_1 = (uint)(u64_ >> 32);
			this.m_u32_2 = (uint)u64_2;
			this.m_u32_3 = (uint)(u64_2 >> 32);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000156E4 File Offset: 0x000138E4
		private static void ShortEnd(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			h3 ^= h2;
			Hash128.Rot64(ref h2, 15);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 52);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 26);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 51);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 28);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 9);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 47);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 54);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 32);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 25);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 63);
			h1 += h0;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000157EF File Offset: 0x000139EF
		private static void Rot64(ref ulong x, int k)
		{
			x = (x << k) | (x >> 64 - k);
		}

		// Token: 0x06000FD6 RID: 4054
		[MethodImpl(4096)]
		private static extern void Parse_Injected(string hashString, out Hash128 ret);

		// Token: 0x06000FD7 RID: 4055
		[MethodImpl(4096)]
		private static extern string Hash128ToStringImpl_Injected(ref Hash128 hash);

		// Token: 0x04000447 RID: 1095
		private uint m_u32_0;

		// Token: 0x04000448 RID: 1096
		private uint m_u32_1;

		// Token: 0x04000449 RID: 1097
		private uint m_u32_2;

		// Token: 0x0400044A RID: 1098
		private uint m_u32_3;

		// Token: 0x0400044B RID: 1099
		private const ulong kConst = 16045690984833335023UL;
	}
}
