using System;
using System.Collections;
using System.IO;
using System.Web.Caching;

namespace System.Web.Hosting
{
	/// <summary>Provides a set of methods that enable a Web application to retrieve resources from a virtual file system.</summary>
	// Token: 0x0200055A RID: 1370
	public abstract class VirtualPathProvider : MarshalByRefObject
	{
		/// <summary>Gets a reference to a previously registered <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object in the compilation system.</summary>
		/// <returns>The next <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object in the compilation system.</returns>
		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x0009EF4E File Offset: 0x0009D14E
		protected internal VirtualPathProvider Previous
		{
			get
			{
				return this.prev;
			}
		}

		/// <summary>Initializes the <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance.</summary>
		// Token: 0x06003B3B RID: 15163 RVA: 0x0000393A File Offset: 0x00001B3A
		protected virtual void Initialize()
		{
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0009EF56 File Offset: 0x0009D156
		internal void InitializeAndSetPrevious(VirtualPathProvider prev)
		{
			this.prev = prev;
			this.Initialize();
		}

		/// <summary>Combines a base path with a relative path to return a complete path to a virtual resource.</summary>
		/// <returns>The complete path to a virtual resource.</returns>
		/// <param name="basePath">The base path for the application.</param>
		/// <param name="relativePath">The path to the virtual resource, relative to the base path.</param>
		// Token: 0x06003B3D RID: 15165 RVA: 0x0009EF65 File Offset: 0x0009D165
		public virtual string CombineVirtualPaths(string basePath, string relativePath)
		{
			return VirtualPathUtility.Combine(basePath, relativePath);
		}

		/// <summary>Gets a value that indicates whether a directory exists in the virtual file system.</summary>
		/// <returns>true if the directory exists in the virtual file system; otherwise, false.</returns>
		/// <param name="virtualDir">The path to the virtual directory.</param>
		// Token: 0x06003B3E RID: 15166 RVA: 0x0009EF6E File Offset: 0x0009D16E
		public virtual bool DirectoryExists(string virtualDir)
		{
			return this.prev != null && this.prev.DirectoryExists(virtualDir);
		}

		/// <summary>Gets a value that indicates whether a file exists in the virtual file system.</summary>
		/// <returns>true if the file exists in the virtual file system; otherwise, false.</returns>
		/// <param name="virtualPath">The path to the virtual file.</param>
		// Token: 0x06003B3F RID: 15167 RVA: 0x0009EF86 File Offset: 0x0009D186
		public virtual bool FileExists(string virtualPath)
		{
			return this.prev != null && this.prev.FileExists(virtualPath);
		}

		/// <summary>Creates a cache dependency based on the specified virtual paths.</summary>
		/// <returns>A <see cref="T:System.Web.Caching.CacheDependency" /> object for the specified virtual resources.</returns>
		/// <param name="virtualPath">The path to the primary virtual resource.</param>
		/// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
		/// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
		// Token: 0x06003B40 RID: 15168 RVA: 0x0009EF9E File Offset: 0x0009D19E
		public virtual CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			if (this.prev != null)
			{
				return this.prev.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
			}
			return null;
		}

		/// <summary>Returns a cache key to use for the specified virtual path.</summary>
		/// <returns>A cache key for the specified virtual resource.</returns>
		/// <param name="virtualPath">The path to the virtual resource.</param>
		// Token: 0x06003B41 RID: 15169 RVA: 0x0009EFB8 File Offset: 0x0009D1B8
		public virtual string GetCacheKey(string virtualPath)
		{
			if (this.prev != null)
			{
				return this.prev.GetCacheKey(virtualPath);
			}
			return null;
		}

		/// <summary>Gets a virtual directory from the virtual file system.</summary>
		/// <returns>A descendent of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class that represents a directory in the virtual file system.</returns>
		/// <param name="virtualDir">The path to the virtual directory.</param>
		// Token: 0x06003B42 RID: 15170 RVA: 0x0009EFD0 File Offset: 0x0009D1D0
		public virtual VirtualDirectory GetDirectory(string virtualDir)
		{
			if (this.prev != null)
			{
				return this.prev.GetDirectory(virtualDir);
			}
			return null;
		}

		/// <summary>Gets a virtual file from the virtual file system.</summary>
		/// <returns>A descendent of the <see cref="T:System.Web.Hosting.VirtualFile" /> class that represents a file in the virtual file system.</returns>
		/// <param name="virtualPath">The path to the virtual file.</param>
		// Token: 0x06003B43 RID: 15171 RVA: 0x0009EFE8 File Offset: 0x0009D1E8
		public virtual VirtualFile GetFile(string virtualPath)
		{
			if (this.prev != null)
			{
				return this.prev.GetFile(virtualPath);
			}
			return null;
		}

		/// <summary>Returns a hash of the specified virtual paths.</summary>
		/// <returns>A hash of the specified virtual paths.</returns>
		/// <param name="virtualPath">The path to the primary virtual resource.</param>
		/// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
		// Token: 0x06003B44 RID: 15172 RVA: 0x0009F000 File Offset: 0x0009D200
		public virtual string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			if (this.prev != null)
			{
				return this.prev.GetFileHash(virtualPath, virtualPathDependencies);
			}
			return null;
		}

		/// <summary>Gives the <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003B45 RID: 15173 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Returns a stream from a virtual file.</summary>
		/// <returns>A read-only <see cref="T:System.IO.Stream" /> object for the specified virtual file or resource.</returns>
		/// <param name="virtualPath">The path to the virtual file.</param>
		// Token: 0x06003B46 RID: 15174 RVA: 0x0009F01C File Offset: 0x0009D21C
		public static Stream OpenFile(string virtualPath)
		{
			VirtualFile file = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath);
			if (file != null)
			{
				return file.Open();
			}
			return null;
		}

		// Token: 0x04001FF7 RID: 8183
		private VirtualPathProvider prev;
	}
}
