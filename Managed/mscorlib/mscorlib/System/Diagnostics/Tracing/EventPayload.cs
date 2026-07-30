using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AA5 RID: 2725
	internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x060062F8 RID: 25336 RVA: 0x0014274E File Offset: 0x0014094E
		internal EventPayload(List<string> payloadNames, List<object> payloadValues)
		{
			this.m_names = payloadNames;
			this.m_values = payloadValues;
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x00142764 File Offset: 0x00140964
		public ICollection<string> Keys
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x060062FA RID: 25338 RVA: 0x0014276C File Offset: 0x0014096C
		public ICollection<object> Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x170011C8 RID: 4552
		public object this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = 0;
				using (List<string>.Enumerator enumerator = this.m_names.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == key)
						{
							return this.m_values[num];
						}
						num++;
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void Add(string key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void Add(KeyValuePair<string, object> payloadEntry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x001427F4 File Offset: 0x001409F4
		public bool Contains(KeyValuePair<string, object> entry)
		{
			return this.ContainsKey(entry.Key);
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x00142804 File Offset: 0x00140A04
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			using (List<string>.Enumerator enumerator = this.m_names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == key)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06006302 RID: 25346 RVA: 0x0014286C File Offset: 0x00140A6C
		public int Count
		{
			get
			{
				return this.m_names.Count;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06006303 RID: 25347 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x00142879 File Offset: 0x00140A79
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Keys.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, object>(this.m_names[i], this.m_values[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x00142888 File Offset: 0x00140A88
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x00014B5A File Offset: 0x00012D5A
		public bool Remove(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x00014B5A File Offset: 0x00012D5A
		public bool Remove(KeyValuePair<string, object> entry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00142890 File Offset: 0x00140A90
		public bool TryGetValue(string key, out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			using (List<string>.Enumerator enumerator = this.m_names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == key)
					{
						value = this.m_values[num];
						return true;
					}
					num++;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x04003153 RID: 12627
		private List<string> m_names;

		// Token: 0x04003154 RID: 12628
		private List<object> m_values;
	}
}
