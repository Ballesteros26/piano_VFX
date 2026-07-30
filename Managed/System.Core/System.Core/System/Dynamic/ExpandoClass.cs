using System;
using System.Collections.Generic;

namespace System.Dynamic
{
	// Token: 0x02000322 RID: 802
	internal class ExpandoClass
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x0004E79D File Offset: 0x0004C99D
		internal ExpandoClass()
		{
			this._hashCode = 6551;
			this._keys = Array.Empty<string>();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0004E7BB File Offset: 0x0004C9BB
		internal ExpandoClass(string[] keys, int hashCode)
		{
			this._hashCode = hashCode;
			this._keys = keys;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0004E7D4 File Offset: 0x0004C9D4
		internal ExpandoClass FindNewClass(string newKey)
		{
			int num = this._hashCode ^ newKey.GetHashCode();
			ExpandoClass expandoClass3;
			lock (this)
			{
				List<WeakReference> transitionList = this.GetTransitionList(num);
				for (int i = 0; i < transitionList.Count; i++)
				{
					ExpandoClass expandoClass = transitionList[i].Target as ExpandoClass;
					if (expandoClass == null)
					{
						transitionList.RemoveAt(i);
						i--;
					}
					else if (string.Equals(expandoClass._keys[expandoClass._keys.Length - 1], newKey, StringComparison.Ordinal))
					{
						return expandoClass;
					}
				}
				string[] array = new string[this._keys.Length + 1];
				Array.Copy(this._keys, 0, array, 0, this._keys.Length);
				array[this._keys.Length] = newKey;
				ExpandoClass expandoClass2 = new ExpandoClass(array, num);
				transitionList.Add(new WeakReference(expandoClass2));
				expandoClass3 = expandoClass2;
			}
			return expandoClass3;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0004E8D0 File Offset: 0x0004CAD0
		private List<WeakReference> GetTransitionList(int hashCode)
		{
			if (this._transitions == null)
			{
				this._transitions = new Dictionary<int, List<WeakReference>>();
			}
			List<WeakReference> list;
			if (!this._transitions.TryGetValue(hashCode, out list))
			{
				list = (this._transitions[hashCode] = new List<WeakReference>());
			}
			return list;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0004E914 File Offset: 0x0004CB14
		internal int GetValueIndex(string name, bool caseInsensitive, ExpandoObject obj)
		{
			if (caseInsensitive)
			{
				return this.GetValueIndexCaseInsensitive(name, obj);
			}
			return this.GetValueIndexCaseSensitive(name);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0004E92C File Offset: 0x0004CB2C
		internal int GetValueIndexCaseSensitive(string name)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (string.Equals(this._keys[i], name, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0004E960 File Offset: 0x0004CB60
		private int GetValueIndexCaseInsensitive(string name, ExpandoObject obj)
		{
			int num = -1;
			object lockObject = obj.LockObject;
			lock (lockObject)
			{
				for (int i = this._keys.Length - 1; i >= 0; i--)
				{
					if (string.Equals(this._keys[i], name, StringComparison.OrdinalIgnoreCase) && !obj.IsDeletedMember(i))
					{
						if (num != -1)
						{
							return -2;
						}
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0004E9E0 File Offset: 0x0004CBE0
		internal string[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x04000B02 RID: 2818
		private readonly string[] _keys;

		// Token: 0x04000B03 RID: 2819
		private readonly int _hashCode;

		// Token: 0x04000B04 RID: 2820
		private Dictionary<int, List<WeakReference>> _transitions;

		// Token: 0x04000B05 RID: 2821
		private const int EmptyHashCode = 6551;

		// Token: 0x04000B06 RID: 2822
		internal static readonly ExpandoClass Empty = new ExpandoClass();
	}
}
