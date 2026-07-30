using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Provides a way to index and retrieve a collection of <see cref="T:System.Web.IHttpModule" /> objects.</summary>
	// Token: 0x02000099 RID: 153
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpModuleCollection : NameObjectCollectionBase
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0000665D File Offset: 0x0000485D
		internal HttpModuleCollection()
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001107D File Offset: 0x0000F27D
		internal void AddModule(string key, IHttpModule m)
		{
			base.BaseAdd(key, m);
		}

		/// <summary>Copies members of the module collection to an <see cref="T:System.Array" />, beginning at the specified index of the array.</summary>
		/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
		/// <param name="index">The index of the destination <see cref="T:System.Array" /> where copying starts. </param>
		// Token: 0x0600075E RID: 1886 RVA: 0x00010420 File Offset: 0x0000E620
		public void CopyTo(Array dest, int index)
		{
			base.BaseGetAllValues().CopyTo(dest, index);
		}

		/// <summary>Returns the key (name) of the <see cref="T:System.Web.IHttpModule" /> object at the specified numerical index.</summary>
		/// <returns>The name of the <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">Index of the key to retrieve from the collection. </param>
		// Token: 0x0600075F RID: 1887 RVA: 0x00011087 File Offset: 0x0000F287
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.IHttpModule" /> object with the specified index from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.IHttpModule" /> object to return from the collection. </param>
		// Token: 0x06000760 RID: 1888 RVA: 0x000112E1 File Offset: 0x0000F4E1
		public IHttpModule Get(int index)
		{
			return (IHttpModule)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.IHttpModule" /> object with the specified name from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="name" /> parameter.</returns>
		/// <param name="name">The key of the item to be retrieved. </param>
		// Token: 0x06000761 RID: 1889 RVA: 0x000112EF File Offset: 0x0000F4EF
		public IHttpModule Get(string name)
		{
			return (IHttpModule)base.BaseGet(name);
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpModule" /> object with the specified name from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> object module specified by the <paramref name="name" /> parameter.</returns>
		/// <param name="name">The key of the item to be retrieved. </param>
		// Token: 0x170002CB RID: 715
		public IHttpModule this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpModule" /> object with the specified numerical index from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> object module specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.IHttpModule" /> object to retrieve from the collection. </param>
		// Token: 0x170002CC RID: 716
		public IHttpModule this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets a string array containing all the keys (module names) in the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>An array of module names.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000110BE File Offset: 0x0000F2BE
		public string[] AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}
	}
}
