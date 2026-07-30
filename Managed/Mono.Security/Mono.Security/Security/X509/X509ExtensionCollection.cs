using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x02000019 RID: 25
	public sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000A5A3 File Offset: 0x000087A3
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A5AC File Offset: 0x000087AC
		public X509ExtensionCollection(ASN1 asn1)
			: this()
		{
			this.readOnly = true;
			if (asn1 == null)
			{
				return;
			}
			if (asn1.Tag != 48)
			{
				throw new Exception("Invalid extensions format");
			}
			for (int i = 0; i < asn1.Count; i++)
			{
				X509Extension x509Extension = new X509Extension(asn1[i]);
				base.InnerList.Add(x509Extension);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A60A File Offset: 0x0000880A
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			return base.InnerList.Add(extension);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A63C File Offset: 0x0000883C
		public void AddRange(X509Extension[] extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < extension.Length; i++)
			{
				base.InnerList.Add(extension[i]);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A688 File Offset: 0x00008888
		public void AddRange(X509ExtensionCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < collection.InnerList.Count; i++)
			{
				base.InnerList.Add(collection[i]);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A6DF File Offset: 0x000088DF
		public bool Contains(X509Extension extension)
		{
			return this.IndexOf(extension) != -1;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A6EE File Offset: 0x000088EE
		public bool Contains(string oid)
		{
			return this.IndexOf(oid) != -1;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A6FD File Offset: 0x000088FD
		public void CopyTo(X509Extension[] extensions, int index)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			base.InnerList.CopyTo(extensions, index);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A71C File Offset: 0x0000891C
		public int IndexOf(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Equals(extension))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A76C File Offset: 0x0000896C
		public int IndexOf(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Oid == oid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A7BE File Offset: 0x000089BE
		public void Insert(int index, X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Insert(index, extension);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A7DB File Offset: 0x000089DB
		public void Remove(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Remove(extension);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A7F8 File Offset: 0x000089F8
		public void Remove(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			int num = this.IndexOf(oid);
			if (num != -1)
			{
				base.InnerList.RemoveAt(num);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A82B File Offset: 0x00008A2B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x1700004B RID: 75
		public X509Extension this[int index]
		{
			get
			{
				return (X509Extension)base.InnerList[index];
			}
		}

		// Token: 0x1700004C RID: 76
		public X509Extension this[string oid]
		{
			get
			{
				int num = this.IndexOf(oid);
				if (num == -1)
				{
					return null;
				}
				return (X509Extension)base.InnerList[num];
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A878 File Offset: 0x00008A78
		public byte[] GetBytes()
		{
			if (base.InnerList.Count < 1)
			{
				return null;
			}
			ASN1 asn = new ASN1(48);
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Extension x509Extension = (X509Extension)base.InnerList[i];
				asn.Add(x509Extension.ASN1);
			}
			return asn.GetBytes();
		}

		// Token: 0x040000BF RID: 191
		private bool readOnly;
	}
}
