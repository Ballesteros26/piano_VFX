using System;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>Represents part of an output-cache entry, stored as a file.</summary>
	// Token: 0x0200068E RID: 1678
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class FileResponseElement : ResponseElement
	{
		/// <summary>Gets the size of the data, starting at the offset that contains the data for a <see cref="T:System.Web.Caching.FileResponseElement" /> instance.</summary>
		/// <returns>The size of the data.</returns>
		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x000C8FD7 File Offset: 0x000C71D7
		// (set) Token: 0x06004777 RID: 18295 RVA: 0x000C8FDF File Offset: 0x000C71DF
		public long Length { get; private set; }

		/// <summary>Gets the position in the file where the data from a <see cref="T:System.Web.Caching.FileResponseElement" /> instance starts. </summary>
		/// <returns>The starting point of the data in the file.</returns>
		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x000C8FE8 File Offset: 0x000C71E8
		// (set) Token: 0x06004779 RID: 18297 RVA: 0x000C8FF0 File Offset: 0x000C71F0
		public long Offset { get; private set; }

		/// <summary>Gets the location of the file that contains data from a <see cref="T:System.Web.Caching.FileResponseElement" /> instance.</summary>
		/// <returns>The fully qualified path of the file.</returns>
		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000C8FF9 File Offset: 0x000C71F9
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x000C9001 File Offset: 0x000C7201
		public string Path { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.FileResponseElement" /> class. </summary>
		/// <param name="path">The fully qualified path for the file.</param>
		/// <param name="offset">The position in the file where the string starts.</param>
		/// <param name="length">The length of the data, starting at the offset that represents the output-cache data in the file defined by <paramref name="path" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero.</exception>
		// Token: 0x0600477C RID: 18300 RVA: 0x000C900C File Offset: 0x000C720C
		public FileResponseElement(string path, long offset, long length)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", "is less than zero.");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "is less than zero.");
			}
			this.Length = length;
			this.Offset = offset;
			this.Path = path;
		}
	}
}
