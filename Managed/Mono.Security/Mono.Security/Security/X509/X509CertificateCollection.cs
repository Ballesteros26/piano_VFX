using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class X509CertificateCollection : CollectionBase, IEnumerable
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00009B56 File Offset: 0x00007D56
		public X509CertificateCollection()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009B5E File Offset: 0x00007D5E
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00009B6D File Offset: 0x00007D6D
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000041 RID: 65
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009B9E File Offset: 0x00007D9E
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009BBC File Offset: 0x00007DBC
		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009C38 File Offset: 0x00007E38
		public bool Contains(X509Certificate value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009C47 File Offset: 0x00007E47
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009C56 File Offset: 0x00007E56
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009C5E File Offset: 0x00007E5E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009C6B File Offset: 0x00007E6B
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009C78 File Offset: 0x00007E78
		public int IndexOf(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] hash = value.Hash;
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (this.Compare(x509Certificate.Hash, hash))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009CE3 File Offset: 0x00007EE3
		public void Remove(X509Certificate value)
		{
			base.InnerList.Remove(value);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x020000C6 RID: 198
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x0600074F RID: 1871 RVA: 0x00021839 File Offset: 0x0001FA39
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x0002184D File Offset: 0x0001FA4D
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x0002185F File Offset: 0x0001FA5F
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0002186C File Offset: 0x0001FA6C
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x00021879 File Offset: 0x0001FA79
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x00021886 File Offset: 0x0001FA86
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x00021893 File Offset: 0x0001FA93
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x040004C8 RID: 1224
			private IEnumerator enumerator;
		}
	}
}
