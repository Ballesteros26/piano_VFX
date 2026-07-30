using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	internal class X509CertificateCollection : CollectionBase, IEnumerable
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00013A1B File Offset: 0x00011C1B
		public X509CertificateCollection()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00013A23 File Offset: 0x00011C23
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00013A32 File Offset: 0x00011C32
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x1700009A RID: 154
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

		// Token: 0x06000313 RID: 787 RVA: 0x00013A63 File Offset: 0x00011C63
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00013A80 File Offset: 0x00011C80
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

		// Token: 0x06000315 RID: 789 RVA: 0x00013AB8 File Offset: 0x00011CB8
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

		// Token: 0x06000316 RID: 790 RVA: 0x00013AFC File Offset: 0x00011CFC
		public bool Contains(X509Certificate value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00013B0B File Offset: 0x00011D0B
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00013B1A File Offset: 0x00011D1A
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00013B22 File Offset: 0x00011D22
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00013B2F File Offset: 0x00011D2F
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00013B3C File Offset: 0x00011D3C
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

		// Token: 0x0600031C RID: 796 RVA: 0x00013B98 File Offset: 0x00011D98
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00013BA7 File Offset: 0x00011DA7
		public void Remove(X509Certificate value)
		{
			base.InnerList.Remove(value);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00013BB8 File Offset: 0x00011DB8
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

		// Token: 0x0200005E RID: 94
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x0600031F RID: 799 RVA: 0x00013BF8 File Offset: 0x00011DF8
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x06000320 RID: 800 RVA: 0x00013C0C File Offset: 0x00011E0C
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000321 RID: 801 RVA: 0x00013C1E File Offset: 0x00011E1E
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000322 RID: 802 RVA: 0x00013C2B File Offset: 0x00011E2B
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000323 RID: 803 RVA: 0x00013C38 File Offset: 0x00011E38
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x06000324 RID: 804 RVA: 0x00013C2B File Offset: 0x00011E2B
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000325 RID: 805 RVA: 0x00013C38 File Offset: 0x00011E38
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0400050B RID: 1291
			private IEnumerator enumerator;
		}
	}
}
