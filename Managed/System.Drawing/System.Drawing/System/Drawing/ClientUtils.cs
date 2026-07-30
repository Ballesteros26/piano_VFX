using System;
using System.Collections;
using System.Security;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000014 RID: 20
	internal static class ClientUtils
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000025DC File Offset: 0x000007DC
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002619 File Offset: 0x00000819
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x02000015 RID: 21
		internal class WeakRefCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000033 RID: 51 RVA: 0x0000262B File Offset: 0x0000082B
			internal WeakRefCollection()
				: this(4)
			{
			}

			// Token: 0x06000034 RID: 52 RVA: 0x00002634 File Offset: 0x00000834
			internal WeakRefCollection(int size)
			{
				this.InnerList = new ArrayList(size);
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00002653 File Offset: 0x00000853
			internal ArrayList InnerList { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000036 RID: 54 RVA: 0x0000265B File Offset: 0x0000085B
			// (set) Token: 0x06000037 RID: 55 RVA: 0x00002663 File Offset: 0x00000863
			public int RefCheckThreshold { get; set; } = int.MaxValue;

			// Token: 0x17000006 RID: 6
			public object this[int index]
			{
				get
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject;
					if ((weakRefObject = this.InnerList[index] as ClientUtils.WeakRefCollection.WeakRefObject) != null && weakRefObject.IsAlive)
					{
						return weakRefObject.Target;
					}
					return null;
				}
				set
				{
					this.InnerList[index] = this.CreateWeakRefObject(value);
				}
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000026B4 File Offset: 0x000008B4
			public void ScavengeReferences()
			{
				int num = 0;
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (this[num] == null)
					{
						this.InnerList.RemoveAt(num);
					}
					else
					{
						num++;
					}
				}
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000026F4 File Offset: 0x000008F4
			public override bool Equals(object obj)
			{
				ClientUtils.WeakRefCollection weakRefCollection;
				if ((weakRefCollection = obj as ClientUtils.WeakRefCollection) == null)
				{
					return true;
				}
				if (weakRefCollection == null || this.Count != weakRefCollection.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this.InnerList[i] != weakRefCollection.InnerList[i] && (this.InnerList[i] == null || !this.InnerList[i].Equals(weakRefCollection.InnerList[i])))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x0000277B File Offset: 0x0000097B
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00002783 File Offset: 0x00000983
			private ClientUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(object value)
			{
				if (value == null)
				{
					return null;
				}
				return new ClientUtils.WeakRefCollection.WeakRefObject(value);
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00002790 File Offset: 0x00000990
			private static void Copy(ClientUtils.WeakRefCollection sourceList, int sourceIndex, ClientUtils.WeakRefCollection destinationList, int destinationIndex, int length)
			{
				if (sourceIndex < destinationIndex)
				{
					sourceIndex += length;
					destinationIndex += length;
					while (length > 0)
					{
						destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
						length--;
					}
					return;
				}
				while (length > 0)
				{
					destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
					length--;
				}
			}

			// Token: 0x0600003F RID: 63 RVA: 0x0000280C File Offset: 0x00000A0C
			public void RemoveByHashCode(object value)
			{
				if (value == null)
				{
					return;
				}
				int hashCode = value.GetHashCode();
				for (int i = 0; i < this.InnerList.Count; i++)
				{
					if (this.InnerList[i] != null && this.InnerList[i].GetHashCode() == hashCode)
					{
						this.RemoveAt(i);
						return;
					}
				}
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002864 File Offset: 0x00000A64
			public void Clear()
			{
				this.InnerList.Clear();
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000041 RID: 65 RVA: 0x00002871 File Offset: 0x00000A71
			public bool IsFixedSize
			{
				get
				{
					return this.InnerList.IsFixedSize;
				}
			}

			// Token: 0x06000042 RID: 66 RVA: 0x0000287E File Offset: 0x00000A7E
			public bool Contains(object value)
			{
				return this.InnerList.Contains(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002892 File Offset: 0x00000A92
			public void RemoveAt(int index)
			{
				this.InnerList.RemoveAt(index);
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000028A0 File Offset: 0x00000AA0
			public void Remove(object value)
			{
				this.InnerList.Remove(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000045 RID: 69 RVA: 0x000028B4 File Offset: 0x00000AB4
			public int IndexOf(object value)
			{
				return this.InnerList.IndexOf(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000046 RID: 70 RVA: 0x000028C8 File Offset: 0x00000AC8
			public void Insert(int index, object value)
			{
				this.InnerList.Insert(index, this.CreateWeakRefObject(value));
			}

			// Token: 0x06000047 RID: 71 RVA: 0x000028DD File Offset: 0x00000ADD
			public int Add(object value)
			{
				if (this.Count > this.RefCheckThreshold)
				{
					this.ScavengeReferences();
				}
				return this.InnerList.Add(this.CreateWeakRefObject(value));
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000048 RID: 72 RVA: 0x00002905 File Offset: 0x00000B05
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00002912 File Offset: 0x00000B12
			object ICollection.SyncRoot
			{
				get
				{
					return this.InnerList.SyncRoot;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600004A RID: 74 RVA: 0x0000291F File Offset: 0x00000B1F
			public bool IsReadOnly
			{
				get
				{
					return this.InnerList.IsReadOnly;
				}
			}

			// Token: 0x0600004B RID: 75 RVA: 0x0000292C File Offset: 0x00000B2C
			public void CopyTo(Array array, int index)
			{
				this.InnerList.CopyTo(array, index);
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600004C RID: 76 RVA: 0x0000293B File Offset: 0x00000B3B
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.InnerList.IsSynchronized;
				}
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00002948 File Offset: 0x00000B48
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x02000016 RID: 22
			internal class WeakRefObject
			{
				// Token: 0x0600004E RID: 78 RVA: 0x00002955 File Offset: 0x00000B55
				internal WeakRefObject(object obj)
				{
					this._weakHolder = new WeakReference(obj);
					this._hash = obj.GetHashCode();
				}

				// Token: 0x1700000C RID: 12
				// (get) Token: 0x0600004F RID: 79 RVA: 0x00002975 File Offset: 0x00000B75
				internal bool IsAlive
				{
					get
					{
						return this._weakHolder.IsAlive;
					}
				}

				// Token: 0x1700000D RID: 13
				// (get) Token: 0x06000050 RID: 80 RVA: 0x00002982 File Offset: 0x00000B82
				internal object Target
				{
					get
					{
						return this._weakHolder.Target;
					}
				}

				// Token: 0x06000051 RID: 81 RVA: 0x0000298F File Offset: 0x00000B8F
				public override int GetHashCode()
				{
					return this._hash;
				}

				// Token: 0x06000052 RID: 82 RVA: 0x00002998 File Offset: 0x00000B98
				public override bool Equals(object obj)
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as ClientUtils.WeakRefCollection.WeakRefObject;
					return weakRefObject == this || (weakRefObject != null && (weakRefObject.Target == this.Target || (this.Target != null && this.Target.Equals(weakRefObject.Target))));
				}

				// Token: 0x04000098 RID: 152
				private int _hash;

				// Token: 0x04000099 RID: 153
				private WeakReference _weakHolder;
			}
		}
	}
}
