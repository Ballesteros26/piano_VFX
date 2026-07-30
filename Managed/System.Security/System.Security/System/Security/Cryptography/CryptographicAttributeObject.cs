using System;

namespace System.Security.Cryptography
{
	/// <summary>Contains a type and a collection of values associated with that type.</summary>
	// Token: 0x02000013 RID: 19
	public sealed class CryptographicAttributeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		// Token: 0x0600003A RID: 58 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public CryptographicAttributeObject(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._oid = new Oid(oid);
			this._list = new AsnEncodedDataCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> class using an attribute represented by the specified <see cref="T:System.Security.Cryptography.Oid" /> object and the set of values associated with that attribute represented by the specified <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection.</summary>
		/// <param name="oid">The attribute to store in this <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object.</param>
		/// <param name="values">The set of values associated with the attribute represented by the <paramref name="oid" /> parameter.</param>
		/// <exception cref="T:System.InvalidOperationException">The collection contains duplicate items. </exception>
		// Token: 0x0600003B RID: 59 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public CryptographicAttributeObject(Oid oid, AsnEncodedDataCollection values)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._oid = new Oid(oid);
			if (values == null)
			{
				this._list = new AsnEncodedDataCollection();
				return;
			}
			this._list = values;
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.Oid" /> object that specifies the object identifier for the attribute.</summary>
		/// <returns>The object identifier for the attribute.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E29 File Offset: 0x00001029
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> collection that contains the set of values that are associated with the attribute.</summary>
		/// <returns>The set of values that is associated with the attribute.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002E31 File Offset: 0x00001031
		public AsnEncodedDataCollection Values
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x04000098 RID: 152
		private Oid _oid;

		// Token: 0x04000099 RID: 153
		private AsnEncodedDataCollection _list;
	}
}
