using System;
using System.Collections;
using System.IO;
using System.Web.Caching;

namespace System.Web.Hosting
{
	// Token: 0x0200054F RID: 1359
	internal sealed class DefaultVirtualPathProvider : VirtualPathProvider
	{
		// Token: 0x06003AC8 RID: 15048 RVA: 0x0009E769 File Offset: 0x0009C969
		internal DefaultVirtualPathProvider()
		{
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x0000393A File Offset: 0x00001B3A
		protected override void Initialize()
		{
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x0009E771 File Offset: 0x0009C971
		public override bool DirectoryExists(string virtualDir)
		{
			if (string.IsNullOrEmpty(virtualDir))
			{
				throw new ArgumentNullException("virtualDir");
			}
			return Directory.Exists(HostingEnvironment.MapPath(virtualDir));
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0009E791 File Offset: 0x0009C991
		public override bool FileExists(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			return File.Exists(HostingEnvironment.MapPath(virtualPath));
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return null;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override string GetCacheKey(string virtualPath)
		{
			return null;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0009E7B1 File Offset: 0x0009C9B1
		public override VirtualDirectory GetDirectory(string virtualDir)
		{
			if (string.IsNullOrEmpty(virtualDir))
			{
				throw new ArgumentNullException("virtualDir");
			}
			return new DefaultVirtualDirectory(virtualDir);
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0009E7CC File Offset: 0x0009C9CC
		public override VirtualFile GetFile(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			return new DefaultVirtualFile(virtualPath);
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x0009E7E7 File Offset: 0x0009C9E7
		public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			if (virtualPath == null || virtualPathDependencies == null)
			{
				throw new NullReferenceException();
			}
			return virtualPath;
		}
	}
}
