using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001D RID: 29
	public static class CoreUnsafeUtils
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000048FC File Offset: 0x00002AFC
		public unsafe static void CopyTo<T>(this List<T> list, void* dest, int count) where T : struct
		{
			int num = Mathf.Min(count, list.Count);
			for (int i = 0; i < num; i++)
			{
				UnsafeUtility.WriteArrayElement<T>(dest, i, list[i]);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004930 File Offset: 0x00002B30
		public unsafe static void CopyTo<T>(this T[] list, void* dest, int count) where T : struct
		{
			int num = Mathf.Min(count, list.Length);
			for (int i = 0; i < num; i++)
			{
				UnsafeUtility.WriteArrayElement<T>(dest, i, list[i]);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004964 File Offset: 0x00002B64
		public unsafe static void QuickSort(uint[] arr, int left, int right)
		{
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				CoreUnsafeUtils.QuickSort<uint, uint, CoreUnsafeUtils.UintKeyGetter>((void*)ptr, left, right);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004993 File Offset: 0x00002B93
		public unsafe static void QuickSort<T>(int count, void* data) where T : struct, IComparable<T>
		{
			CoreUnsafeUtils.QuickSort<T, T, CoreUnsafeUtils.DefaultKeyGetter<T>>(data, 0, count - 1);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000499F File Offset: 0x00002B9F
		public unsafe static void QuickSort<TValue, TKey, TGetter>(int count, void* data) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, 0, count - 1);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000049AC File Offset: 0x00002BAC
		public unsafe static void QuickSort<TValue, TKey, TGetter>(void* data, int left, int right) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			if (left < right)
			{
				int num = CoreUnsafeUtils.Partition<TValue, TKey, TGetter>(data, left, right);
				if (num >= 1)
				{
					CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, left, num);
				}
				if (num + 1 < right)
				{
					CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, num + 1, right);
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000049E4 File Offset: 0x00002BE4
		public unsafe static int IndexOf<T>(void* data, int count, T v) where T : struct, IEquatable<T>
		{
			for (int i = 0; i < count; i++)
			{
				T t = UnsafeUtility.ReadArrayElement<T>(data, i);
				if (t.Equals(v))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004A18 File Offset: 0x00002C18
		public unsafe static int CompareHashes<TOldValue, TOldGetter, TNewValue, TNewGetter>(int oldHashCount, void* oldHashes, int newHashCount, void* newHashes, int* addIndices, int* removeIndices, out int addCount, out int remCount) where TOldValue : struct where TOldGetter : struct, CoreUnsafeUtils.IKeyGetter<TOldValue, Hash128> where TNewValue : struct where TNewGetter : struct, CoreUnsafeUtils.IKeyGetter<TNewValue, Hash128>
		{
			TOldGetter toldGetter = new TOldGetter();
			TNewGetter tnewGetter = new TNewGetter();
			addCount = 0;
			remCount = 0;
			if (oldHashCount == newHashCount)
			{
				Hash128 hash = default(Hash128);
				Hash128 hash2 = default(Hash128);
				CoreUnsafeUtils.CombineHashes<TOldValue, TOldGetter>(oldHashCount, oldHashes, &hash);
				CoreUnsafeUtils.CombineHashes<TNewValue, TNewGetter>(newHashCount, newHashes, &hash2);
				if (hash == hash2)
				{
					return 0;
				}
			}
			int num = 0;
			int i = 0;
			int j = 0;
			while (i < oldHashCount || j < newHashCount)
			{
				if (i == oldHashCount)
				{
					while (j < newHashCount)
					{
						int num2 = addCount;
						addCount = num2 + 1;
						addIndices[num2] = j;
						num++;
						j++;
					}
				}
				else if (j == newHashCount)
				{
					while (i < oldHashCount)
					{
						int num2 = remCount;
						remCount = num2 + 1;
						removeIndices[num2] = i;
						num++;
						i++;
					}
				}
				else
				{
					TNewValue tnewValue = UnsafeUtility.ReadArrayElement<TNewValue>(newHashes, j);
					TOldValue toldValue = UnsafeUtility.ReadArrayElement<TOldValue>(oldHashes, i);
					Hash128 hash3 = tnewGetter.Get(ref tnewValue);
					Hash128 hash4 = toldGetter.Get(ref toldValue);
					if (hash3 == hash4)
					{
						j++;
						i++;
					}
					else if (hash3 < hash4)
					{
						while (j < newHashCount)
						{
							if (!(hash3 < hash4))
							{
								break;
							}
							int num2 = addCount;
							addCount = num2 + 1;
							addIndices[num2] = j;
							j++;
							num++;
							tnewValue = UnsafeUtility.ReadArrayElement<TNewValue>(newHashes, j);
							hash3 = tnewGetter.Get(ref tnewValue);
						}
					}
					else
					{
						while (i < oldHashCount && hash4 < hash3)
						{
							int num2 = remCount;
							remCount = num2 + 1;
							removeIndices[num2] = i;
							num++;
							i++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public unsafe static int CompareHashes(int oldHashCount, Hash128* oldHashes, int newHashCount, Hash128* newHashes, int* addIndices, int* removeIndices, out int addCount, out int remCount)
		{
			return CoreUnsafeUtils.CompareHashes<Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>, Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>>(oldHashCount, (void*)oldHashes, newHashCount, (void*)newHashes, addIndices, removeIndices, out addCount, out remCount);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public unsafe static void CombineHashes<TValue, TGetter>(int count, void* hashes, Hash128* outHash) where TValue : struct where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, Hash128>
		{
			TGetter tgetter = new TGetter();
			for (int i = 0; i < count; i++)
			{
				TValue tvalue = UnsafeUtility.ReadArrayElement<TValue>(hashes, i);
				Hash128 hash = tgetter.Get(ref tvalue);
				HashUtilities.AppendHash(ref hash, ref *outHash);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004C13 File Offset: 0x00002E13
		public unsafe static void CombineHashes(int count, Hash128* hashes, Hash128* outHash)
		{
			CoreUnsafeUtils.CombineHashes<Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>>(count, (void*)hashes, outHash);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004C20 File Offset: 0x00002E20
		private unsafe static int Partition<TValue, TKey, TGetter>(void* data, int left, int right) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			TGetter tgetter = default(TGetter);
			TValue tvalue = UnsafeUtility.ReadArrayElement<TValue>(data, left);
			TKey tkey = tgetter.Get(ref tvalue);
			left--;
			right++;
			for (;;)
			{
				TValue tvalue2 = default(TValue);
				TKey tkey2 = default(TKey);
				int num;
				do
				{
					left++;
					tvalue2 = UnsafeUtility.ReadArrayElement<TValue>(data, left);
					tkey2 = tgetter.Get(ref tvalue2);
					num = tkey2.CompareTo(tkey);
				}
				while (num < 0);
				TValue tvalue3 = default(TValue);
				TKey tkey3 = default(TKey);
				do
				{
					right--;
					tvalue3 = UnsafeUtility.ReadArrayElement<TValue>(data, right);
					tkey3 = tgetter.Get(ref tvalue3);
					num = tkey3.CompareTo(tkey);
				}
				while (num > 0);
				if (left >= right)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<TValue>(data, right, tvalue2);
				UnsafeUtility.WriteArrayElement<TValue>(data, left, tvalue3);
			}
			return right;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004CFC File Offset: 0x00002EFC
		public unsafe static bool HaveDuplicates(int[] arr)
		{
			int* ptr;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)arr.Length) * 4];
				arr.CopyTo((void*)ptr, arr.Length);
				CoreUnsafeUtils.QuickSort<int>(arr.Length, (void*)ptr);
			}
			for (int i = arr.Length - 1; i > 0; i--)
			{
				if (UnsafeUtility.ReadArrayElement<int>((void*)ptr, i).CompareTo(UnsafeUtility.ReadArrayElement<int>((void*)ptr, i - 1)) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x020000B6 RID: 182
		public struct FixedBufferStringQueue
		{
			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x00011279 File Offset: 0x0000F479
			// (set) Token: 0x06000496 RID: 1174 RVA: 0x00011281 File Offset: 0x0000F481
			public int Count { get; private set; }

			// Token: 0x06000497 RID: 1175 RVA: 0x0001128C File Offset: 0x0000F48C
			public unsafe FixedBufferStringQueue(byte* ptr, int length)
			{
				this.m_BufferStart = ptr;
				this.m_BufferLength = length;
				this.m_BufferEnd = this.m_BufferStart + this.m_BufferLength;
				this.m_ReadCursor = this.m_BufferStart;
				this.m_WriteCursor = this.m_BufferStart;
				this.Count = 0;
				this.Clear();
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x000112E0 File Offset: 0x0000F4E0
			public unsafe bool TryPush(string v)
			{
				int num = v.Length * 2 + 4;
				if (this.m_WriteCursor + num >= this.m_BufferEnd)
				{
					return false;
				}
				*(int*)this.m_WriteCursor = v.Length;
				this.m_WriteCursor += 4;
				char* ptr = (char*)this.m_WriteCursor;
				int i = 0;
				while (i < v.Length)
				{
					*ptr = v[i];
					i++;
					ptr++;
				}
				this.m_WriteCursor += 2 * v.Length;
				int num2 = this.Count + 1;
				this.Count = num2;
				return true;
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x00011370 File Offset: 0x0000F570
			public unsafe bool TryPop(out string v)
			{
				int num = *(int*)this.m_ReadCursor;
				if (num != 0)
				{
					this.m_ReadCursor += 4;
					v = new string((char*)this.m_ReadCursor, 0, num);
					this.m_ReadCursor += num * 2;
					return true;
				}
				v = null;
				return false;
			}

			// Token: 0x0600049A RID: 1178 RVA: 0x000113BB File Offset: 0x0000F5BB
			public unsafe void Clear()
			{
				this.m_WriteCursor = this.m_BufferStart;
				this.m_ReadCursor = this.m_BufferStart;
				this.Count = 0;
				UnsafeUtility.MemClear((void*)this.m_BufferStart, (long)this.m_BufferLength);
			}

			// Token: 0x04000262 RID: 610
			private unsafe byte* m_ReadCursor;

			// Token: 0x04000263 RID: 611
			private unsafe byte* m_WriteCursor;

			// Token: 0x04000264 RID: 612
			private unsafe readonly byte* m_BufferEnd;

			// Token: 0x04000265 RID: 613
			private unsafe readonly byte* m_BufferStart;

			// Token: 0x04000266 RID: 614
			private readonly int m_BufferLength;
		}

		// Token: 0x020000B7 RID: 183
		public interface IKeyGetter<TValue, TKey>
		{
			// Token: 0x0600049B RID: 1179
			TKey Get(ref TValue v);
		}

		// Token: 0x020000B8 RID: 184
		internal struct DefaultKeyGetter<T> : CoreUnsafeUtils.IKeyGetter<T, T>
		{
			// Token: 0x0600049C RID: 1180 RVA: 0x000113EE File Offset: 0x0000F5EE
			public T Get(ref T v)
			{
				return v;
			}
		}

		// Token: 0x020000B9 RID: 185
		internal struct UintKeyGetter : CoreUnsafeUtils.IKeyGetter<uint, uint>
		{
			// Token: 0x0600049D RID: 1181 RVA: 0x000113F6 File Offset: 0x0000F5F6
			public uint Get(ref uint v)
			{
				return v;
			}
		}
	}
}
