using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Implements a cryptographic Random Number Generator (RNG) using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x020006A1 RID: 1697
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x0600489C RID: 18588 RVA: 0x001056BF File Offset: 0x001038BF
		static RNGCryptoServiceProvider()
		{
			if (RNGCryptoServiceProvider.RngOpen())
			{
				RNGCryptoServiceProvider._lock = new object();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		// Token: 0x0600489D RID: 18589 RVA: 0x001056D2 File Offset: 0x001038D2
		public RNGCryptoServiceProvider()
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="rgb">A byte array. This value is ignored.</param>
		// Token: 0x0600489E RID: 18590 RVA: 0x001056EC File Offset: 0x001038EC
		public RNGCryptoServiceProvider(byte[] rgb)
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(rgb);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="cspParams">The parameters to pass to the cryptographic service provider (CSP). </param>
		// Token: 0x0600489F RID: 18591 RVA: 0x001056D2 File Offset: 0x001038D2
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="str">The string input. This parameter is ignored.</param>
		// Token: 0x060048A0 RID: 18592 RVA: 0x00105706 File Offset: 0x00103906
		public RNGCryptoServiceProvider(string str)
		{
			if (str == null)
			{
				this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			}
			else
			{
				this._handle = RNGCryptoServiceProvider.RngInitialize(Encoding.UTF8.GetBytes(str));
			}
			this.Check();
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x0010573B File Offset: 0x0010393B
		private void Check()
		{
			if (this._handle == IntPtr.Zero)
			{
				throw new CryptographicException(Locale.GetText("Couldn't access random source."));
			}
		}

		// Token: 0x060048A2 RID: 18594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RngOpen();

		// Token: 0x060048A3 RID: 18595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RngInitialize(byte[] seed);

		// Token: 0x060048A4 RID: 18596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RngGetBytes(IntPtr handle, byte[] data);

		// Token: 0x060048A5 RID: 18597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RngClose(IntPtr handle);

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060048A6 RID: 18598 RVA: 0x00105760 File Offset: 0x00103960
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (RNGCryptoServiceProvider._lock == null)
			{
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data);
			}
			else
			{
				object @lock = RNGCryptoServiceProvider._lock;
				lock (@lock)
				{
					this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data);
				}
			}
			this.Check();
		}

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random nonzero values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random nonzero values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060048A7 RID: 18599 RVA: 0x001057DC File Offset: 0x001039DC
		public override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = new byte[data.Length * 2];
			int i = 0;
			while (i < data.Length)
			{
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, array);
				this.Check();
				int num = 0;
				while (num < array.Length && i != data.Length)
				{
					if (array[num] != 0)
					{
						data[i++] = array[num];
					}
					num++;
				}
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x00105848 File Offset: 0x00103A48
		~RNGCryptoServiceProvider()
		{
			if (this._handle != IntPtr.Zero)
			{
				RNGCryptoServiceProvider.RngClose(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x00105898 File Offset: 0x00103A98
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04002602 RID: 9730
		private static object _lock;

		// Token: 0x04002603 RID: 9731
		private IntPtr _handle;
	}
}
