using System;
using System.Runtime.ExceptionServices;

namespace System.Security
{
	/// <summary>Represents text that should be kept confidential. The text is encrypted for privacy when being used, and deleted from computer memory when no longer needed. This class cannot be inherited.</summary>
	// Token: 0x02000548 RID: 1352
	[MonoTODO("work in progress - encryption is missing")]
	public sealed class SecureString : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this instance.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06003CD4 RID: 15572 RVA: 0x000DAD60 File Offset: 0x000D8F60
		public SecureString()
		{
			this.Alloc(8, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class from a subarray of <see cref="T:System.Char" /> objects.</summary>
		/// <param name="value">A pointer to an array of <see cref="T:System.Char" /> objects.</param>
		/// <param name="length">The number of elements of <paramref name="value" /> to include in the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than 65536.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000DAD70 File Offset: 0x000D8F70
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			this.length = length;
			this.Alloc(length, false);
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = *(value++);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			this.Encrypt();
		}

		/// <summary>Gets the number of characters in the current secure string.</summary>
		/// <returns>The number of <see cref="T:System.Char" /> objects in this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000DADF8 File Offset: 0x000D8FF8
		public int Length
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SecureString");
				}
				return this.length;
			}
		}

		/// <summary>Appends a character to the end of the current secure string.</summary>
		/// <param name="c">A character to append to this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Performing this operation would make the length of this secure string greater than 65536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x06003CD7 RID: 15575 RVA: 0x000DAE14 File Offset: 0x000D9014
		[HandleProcessCorruptedStateExceptions]
		public void AppendChar(char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (this.length == 65536)
			{
				throw new ArgumentOutOfRangeException("length", "> 65536");
			}
			try
			{
				this.Decrypt();
				int num = this.length * 2;
				int num2 = this.length + 1;
				this.length = num2;
				this.Alloc(num2, true);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Deletes the value of the current secure string.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		// Token: 0x06003CD8 RID: 15576 RVA: 0x000DAEC8 File Offset: 0x000D90C8
		public void Clear()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			Array.Clear(this.data, 0, this.data.Length);
			this.length = 0;
		}

		/// <summary>Creates a copy of the current secure string.</summary>
		/// <returns>A duplicate of this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x06003CD9 RID: 15577 RVA: 0x000DAF1B File Offset: 0x000D911B
		public SecureString Copy()
		{
			return new SecureString
			{
				data = (byte[])this.data.Clone(),
				length = this.length
			};
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Security.SecureString" /> object.</summary>
		// Token: 0x06003CDA RID: 15578 RVA: 0x000DAF44 File Offset: 0x000D9144
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.disposed = true;
			if (this.data != null)
			{
				Array.Clear(this.data, 0, this.data.Length);
				this.data = null;
			}
			this.length = 0;
		}

		/// <summary>Inserts a character in this secure string at the specified index position.</summary>
		/// <param name="index">The index position where parameter <paramref name="c" /> is inserted.</param>
		/// <param name="c">The character to insert.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than the length of this secure string.-or-Performing this operation would make the length of this secure string greater than 65536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x06003CDB RID: 15579 RVA: 0x000DAF78 File Offset: 0x000D9178
		[HandleProcessCorruptedStateExceptions]
		public void InsertAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index > this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			if (this.length >= 65536)
			{
				string text = Locale.GetText("Maximum string size is '{0}'.", new object[] { 65536 });
				throw new ArgumentOutOfRangeException("index", text);
			}
			try
			{
				this.Decrypt();
				int num = this.length + 1;
				this.length = num;
				this.Alloc(num, true);
				int num2 = index * 2;
				Buffer.BlockCopy(this.data, num2, this.data, num2 + 2, this.data.Length - num2 - 2);
				this.data[num2++] = (byte)(c >> 8);
				this.data[num2] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Indicates whether this secure string is marked read-only.</summary>
		/// <returns>true if this secure string is marked read-only; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x06003CDC RID: 15580 RVA: 0x000DB07C File Offset: 0x000D927C
		public bool IsReadOnly()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			return this.read_only;
		}

		/// <summary>Makes the text value of this secure string read-only.   </summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x06003CDD RID: 15581 RVA: 0x000DB097 File Offset: 0x000D9297
		public void MakeReadOnly()
		{
			this.read_only = true;
		}

		/// <summary>Removes the character at the specified index position from this secure string.</summary>
		/// <param name="index">The index position of a character in this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x06003CDE RID: 15582 RVA: 0x000DB0A0 File Offset: 0x000D92A0
		[HandleProcessCorruptedStateExceptions]
		public void RemoveAt(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, index * 2 + 2, this.data, index * 2, this.data.Length - index * 2 - 2);
				int num = this.length - 1;
				this.length = num;
				this.Alloc(num, true);
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Replaces the existing character at the specified index position with another character.</summary>
		/// <param name="index">The index position of an existing character in this secure string</param>
		/// <param name="c">A character that replaces the existing character.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x06003CDF RID: 15583 RVA: 0x000DB154 File Offset: 0x000D9354
		[HandleProcessCorruptedStateExceptions]
		public void SetAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				int num = index * 2;
				this.data[num++] = (byte)(c >> 8);
				this.data[num] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000DB1E8 File Offset: 0x000D93E8
		private void Encrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000DB1E8 File Offset: 0x000D93E8
		private void Decrypt()
		{
			if (this.data != null)
			{
				int num = this.data.Length;
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000DB1FC File Offset: 0x000D93FC
		private void Alloc(int length, bool realloc)
		{
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			int num = (length >> 3) + (((length & 7) == 0) ? 0 : 1) << 4;
			if (realloc && this.data != null && num == this.data.Length)
			{
				return;
			}
			if (realloc)
			{
				byte[] array = new byte[num];
				Array.Copy(this.data, 0, array, 0, Math.Min(this.data.Length, array.Length));
				Array.Clear(this.data, 0, this.data.Length);
				this.data = array;
				return;
			}
			this.data = new byte[num];
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000DB29C File Offset: 0x000D949C
		internal byte[] GetBuffer()
		{
			byte[] array = new byte[this.length << 1];
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, 0, array, 0, array.Length);
			}
			finally
			{
				this.Encrypt();
			}
			return array;
		}

		// Token: 0x04001F5C RID: 8028
		private const int BlockSize = 16;

		// Token: 0x04001F5D RID: 8029
		private const int MaxSize = 65536;

		// Token: 0x04001F5E RID: 8030
		private int length;

		// Token: 0x04001F5F RID: 8031
		private bool disposed;

		// Token: 0x04001F60 RID: 8032
		private bool read_only;

		// Token: 0x04001F61 RID: 8033
		private byte[] data;
	}
}
