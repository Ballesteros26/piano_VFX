using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>Provides access to and organizes files uploaded by a client.</summary>
	// Token: 0x02000094 RID: 148
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpFileCollection : NameObjectCollectionBase
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0000665D File Offset: 0x0000485D
		internal HttpFileCollection()
		{
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001107D File Offset: 0x0000F27D
		internal void AddFile(string name, HttpPostedFile file)
		{
			base.BaseAdd(name, file);
		}

		/// <summary>Copies members of the file collection to an <see cref="T:System.Array" /> beginning at the specified index of the array.</summary>
		/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
		/// <param name="index">The index of the destination array where copying starts. </param>
		// Token: 0x0600073A RID: 1850 RVA: 0x00010420 File Offset: 0x0000E620
		public void CopyTo(Array dest, int index)
		{
			base.BaseGetAllValues().CopyTo(dest, index);
		}

		/// <summary>Returns the name of the <see cref="T:System.Web.HttpFileCollection" /> member with the specified numerical index.</summary>
		/// <returns>The name of the <see cref="T:System.Web.HttpFileCollection" /> member specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object name to be returned. </param>
		// Token: 0x0600073B RID: 1851 RVA: 0x00011087 File Offset: 0x0000F287
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.HttpPostedFile" /> object with the specified numerical index from the file collection.</summary>
		/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> object.</returns>
		/// <param name="index">The index of the object to be returned from the file collection. </param>
		// Token: 0x0600073C RID: 1852 RVA: 0x00011090 File Offset: 0x0000F290
		public HttpPostedFile Get(int index)
		{
			return (HttpPostedFile)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.HttpPostedFile" /> object with the specified name from the file collection.</summary>
		/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> object. </returns>
		/// <param name="name">The name of the object to be returned from a file collection. </param>
		// Token: 0x0600073D RID: 1853 RVA: 0x0001109E File Offset: 0x0000F29E
		public HttpPostedFile Get(string name)
		{
			return (HttpPostedFile)base.BaseGet(name);
		}

		/// <summary>Gets the object with the specified name from the file collection.</summary>
		/// <returns>The <see cref="T:System.Web.HttpPostedFile" /> specified by <paramref name="name" />.</returns>
		/// <param name="name">Name of item to be returned. </param>
		// Token: 0x170002BE RID: 702
		public HttpPostedFile this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		/// <summary>Gets the object with the specified numerical index from the <see cref="T:System.Web.HttpFileCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.HttpPostedFile" /> specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the item to get from the file collection. </param>
		// Token: 0x170002BF RID: 703
		public HttpPostedFile this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets a string array containing the keys (names) of all members in the file collection.</summary>
		/// <returns>An array of file names.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000110BE File Offset: 0x0000F2BE
		public string[] AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}

		/// <summary>Returns all files that match the specified name.</summary>
		/// <returns>The collection of files.</returns>
		/// <param name="name">The name to match.</param>
		// Token: 0x06000741 RID: 1857 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IList<HttpPostedFile> GetMultiple(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
