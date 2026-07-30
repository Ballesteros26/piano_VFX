using System;
using System.ComponentModel;

namespace System.DirectoryServices
{
	/// <summary>Specifies how to synchronize a directory within a domain.    </summary>
	// Token: 0x0200001C RID: 28
	public class DirectorySynchronization
	{
		/// <summary>Gets or sets the options for the directory synchronization search. </summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> object.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> values.</exception>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000208C File Offset: 0x0000028C
		[DefaultValue(DirectorySynchronizationOptions.None)]
		[DSDescription("DSDirectorySynchronizationFlag")]
		public DirectorySynchronizationOptions Option
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object.          </summary>
		// Token: 0x060000FC RID: 252 RVA: 0x00002050 File Offset: 0x00000250
		public DirectorySynchronization()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a directory synchronization option.     </summary>
		/// <param name="option">A <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> data type object that specifies how a directory synchronization search is performed.</param>
		// Token: 0x060000FD RID: 253 RVA: 0x00002050 File Offset: 0x00000250
		public DirectorySynchronization(DirectorySynchronizationOptions option)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a directory synchronization object.          </summary>
		/// <param name="sync">A <see cref="T:System.DirectoryServices.DirectorySynchronization" /> data type object.</param>
		// Token: 0x060000FE RID: 254 RVA: 0x00002050 File Offset: 0x00000250
		public DirectorySynchronization(DirectorySynchronization sync)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a cookie.          </summary>
		/// <param name="cookie">A Byte data type object that specifies the directory synchronization search cookie.</param>
		// Token: 0x060000FF RID: 255 RVA: 0x00002050 File Offset: 0x00000250
		public DirectorySynchronization(byte[] cookie)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a directory synchronization option and cookie.          </summary>
		/// <param name="option">A <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> data type object that specifies how a directory synchronization search is performed.</param>
		/// <param name="cookie">A Byte data type object that specifies the directory synchronization search cookie.</param>
		// Token: 0x06000100 RID: 256 RVA: 0x00002050 File Offset: 0x00000250
		public DirectorySynchronization(DirectorySynchronizationOptions option, byte[] cookie)
		{
		}

		/// <summary>Gets the directory synchronization search cookie.          </summary>
		/// <returns>The directory synchronization search cookie object.</returns>
		// Token: 0x06000101 RID: 257 RVA: 0x0000208C File Offset: 0x0000028C
		public byte[] GetDirectorySynchronizationCookie()
		{
			throw new NotImplementedException();
		}

		/// <summary>Resetss the directory synchronization search cookie.          </summary>
		// Token: 0x06000102 RID: 258 RVA: 0x00004060 File Offset: 0x00002260
		public void ResetDirectorySynchronizationCookie()
		{
		}

		/// <summary>Resets the directory synchronization search cookie.          </summary>
		/// <param name="cookie">A Byte data type object that contains a directory synchronization search cookie.  This method resets the cookie for this <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object instance to this value.</param>
		// Token: 0x06000103 RID: 259 RVA: 0x0000208C File Offset: 0x0000028C
		public void ResetDirectorySynchronizationCookie(byte[] cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a copy of the current <see cref="T:System.DirectoryServices.DirectorySynchronization" /> instance.          </summary>
		/// <returns>Returns a <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object.</returns>
		// Token: 0x06000104 RID: 260 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectorySynchronization Copy()
		{
			throw new NotImplementedException();
		}
	}
}
