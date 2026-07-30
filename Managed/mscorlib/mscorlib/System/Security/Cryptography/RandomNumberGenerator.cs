using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of cryptographic random number generators derive.</summary>
	// Token: 0x02000671 RID: 1649
	[ComVisible(true)]
	public abstract class RandomNumberGenerator : IDisposable
	{
		/// <summary>When overridden in a derived class, creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060046CC RID: 18124 RVA: 0x000F8F8E File Offset: 0x000F718E
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		/// <summary>When overridden in a derived class, creates an instance of the specified implementation of a cryptographic random number generator.</summary>
		/// <returns>A new instance of a cryptographic random number generator.</returns>
		/// <param name="rngName">The name of the random number generator implementation to use. </param>
		// Token: 0x060046CD RID: 18125 RVA: 0x000F8F9A File Offset: 0x000F719A
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		/// <summary>When overridden in a derived class, releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> class.</summary>
		// Token: 0x060046CE RID: 18126 RVA: 0x000F8FA7 File Offset: 0x000F71A7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060046CF RID: 18127 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random bytes. </param>
		// Token: 0x060046D0 RID: 18128
		public abstract void GetBytes(byte[] data);

		// Token: 0x060046D1 RID: 18129 RVA: 0x000F8FB8 File Offset: 0x000F71B8
		public virtual void GetBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (offset + count > data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count > 0)
			{
				byte[] array = new byte[count];
				this.GetBytes(array);
				Array.Copy(array, 0, data, offset, count);
			}
		}

		/// <summary>When overridden in a derived class, fills an array of bytes with a cryptographically strong random sequence of nonzero values.</summary>
		/// <param name="data">The array to fill with cryptographically strong random nonzero bytes. </param>
		// Token: 0x060046D2 RID: 18130 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}
	}
}
