using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>Represents part of an output-cache entry that is stored in memory.</summary>
	// Token: 0x02000691 RID: 1681
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class MemoryResponseElement : ResponseElement
	{
		/// <summary>Gets an array that contains all or part of an output-cache response.</summary>
		/// <returns>An array of byte objects.</returns>
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x000C9168 File Offset: 0x000C7368
		// (set) Token: 0x06004788 RID: 18312 RVA: 0x000C9170 File Offset: 0x000C7370
		public byte[] Buffer { get; private set; }

		/// <summary>Gets the size of the array that is referenced by the <see cref="P:System.Web.Caching.MemoryResponseElement.Buffer" /> property.</summary>
		/// <returns>The size of the array.</returns>
		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x000C9179 File Offset: 0x000C7379
		// (set) Token: 0x0600478A RID: 18314 RVA: 0x000C9181 File Offset: 0x000C7381
		public long Length { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.MemoryResponseElement" /> class.</summary>
		/// <param name="buffer">An array of bytes that contains a part of an output-cache response. </param>
		/// <param name="length">The size of the array in <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than the size of <paramref name="buffer" />.</exception>
		// Token: 0x0600478B RID: 18315 RVA: 0x000C918A File Offset: 0x000C738A
		public MemoryResponseElement(byte[] buffer, long length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length < 0L || length > (long)buffer.Length)
			{
				throw new ArgumentOutOfRangeException("length", "is less than zero or greater than the size of buffer.");
			}
			this.Buffer = buffer;
			this.Length = length;
		}
	}
}
