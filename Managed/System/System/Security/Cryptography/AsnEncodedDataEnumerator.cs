using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000395 RID: 917
	public sealed class AsnEncodedDataEnumerator : IEnumerator
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x0006ED32 File Offset: 0x0006CF32
		internal AsnEncodedDataEnumerator(AsnEncodedDataCollection collection)
		{
			this._collection = collection;
			this._position = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in the collection.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0006ED48 File Offset: 0x0006CF48
		public AsnEncodedData Current
		{
			get
			{
				if (this._position < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._collection[this._position];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0006ED48 File Offset: 0x0006CF48
		object IEnumerator.Current
		{
			get
			{
				if (this._position < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._collection[this._position];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001BCD RID: 7117 RVA: 0x0006ED6C File Offset: 0x0006CF6C
		public bool MoveNext()
		{
			int num = this._position + 1;
			this._position = num;
			if (num < this._collection.Count)
			{
				return true;
			}
			this._position = this._collection.Count - 1;
			return false;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001BCE RID: 7118 RVA: 0x0006EDAD File Offset: 0x0006CFAD
		public void Reset()
		{
			this._position = -1;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal AsnEncodedDataEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040018F9 RID: 6393
		private AsnEncodedDataCollection _collection;

		// Token: 0x040018FA RID: 6394
		private int _position;
	}
}
