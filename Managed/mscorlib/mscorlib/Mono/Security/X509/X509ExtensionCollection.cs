using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x02000062 RID: 98
	internal sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000347 RID: 839 RVA: 0x00013A1B File Offset: 0x00011C1B
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000144A4 File Offset: 0x000126A4
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

		// Token: 0x06000349 RID: 841 RVA: 0x00014502 File Offset: 0x00012702
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

		// Token: 0x0600034A RID: 842 RVA: 0x00014534 File Offset: 0x00012734
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

		// Token: 0x0600034B RID: 843 RVA: 0x00014580 File Offset: 0x00012780
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

		// Token: 0x0600034C RID: 844 RVA: 0x000145D7 File Offset: 0x000127D7
		public bool Contains(X509Extension extension)
		{
			return this.IndexOf(extension) != -1;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000145E6 File Offset: 0x000127E6
		public bool Contains(string oid)
		{
			return this.IndexOf(oid) != -1;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000145F5 File Offset: 0x000127F5
		public void CopyTo(X509Extension[] extensions, int index)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			base.InnerList.CopyTo(extensions, index);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00014614 File Offset: 0x00012814
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

		// Token: 0x06000350 RID: 848 RVA: 0x00014664 File Offset: 0x00012864
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

		// Token: 0x06000351 RID: 849 RVA: 0x000146B6 File Offset: 0x000128B6
		public void Insert(int index, X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Insert(index, extension);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000146D3 File Offset: 0x000128D3
		public void Remove(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Remove(extension);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000146F0 File Offset: 0x000128F0
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

		// Token: 0x06000354 RID: 852 RVA: 0x00013B22 File Offset: 0x00011D22
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x170000A6 RID: 166
		public X509Extension this[int index]
		{
			get
			{
				return (X509Extension)base.InnerList[index];
			}
		}

		// Token: 0x170000A7 RID: 167
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

		// Token: 0x06000357 RID: 855 RVA: 0x00014764 File Offset: 0x00012964
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

		// Token: 0x0400051C RID: 1308
		private bool readOnly;
	}
}
