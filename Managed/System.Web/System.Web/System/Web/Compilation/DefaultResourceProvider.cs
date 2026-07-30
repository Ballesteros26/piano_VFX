using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x0200064F RID: 1615
	internal sealed class DefaultResourceProvider : IResourceProvider
	{
		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x000BE104 File Offset: 0x000BC304
		public IResourceReader ResourceReader
		{
			get
			{
				Assembly assembly;
				string text;
				if (this.isGlobal)
				{
					assembly = HttpContext.AppGlobalResourcesAssembly;
					text = this.resource;
				}
				else
				{
					assembly = this.GetLocalResourcesAssembly();
					text = Path.GetFileName(this.resource);
					if (string.IsNullOrEmpty(text))
					{
						return null;
					}
					text += ".resources";
				}
				if (assembly == null)
				{
					return null;
				}
				Stream manifestResourceStream = assembly.GetManifestResourceStream(text);
				if (manifestResourceStream == null)
				{
					return null;
				}
				return new ResourceReader(manifestResourceStream);
			}
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000BE16F File Offset: 0x000BC36F
		public DefaultResourceProvider(string resource, bool isGlobal)
		{
			if (string.IsNullOrEmpty(resource))
			{
				throw new ArgumentNullException("resource");
			}
			this.resource = resource;
			this.isGlobal = isGlobal;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x000BE198 File Offset: 0x000BC398
		public object GetObject(string resourceKey, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(resourceKey))
			{
				return null;
			}
			ResourceManager resourceManager = this.GetResourceManager();
			if (resourceManager == null)
			{
				return null;
			}
			return resourceManager.GetObject(resourceKey, culture);
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000BE1C4 File Offset: 0x000BC3C4
		private Assembly GetLocalResourcesAssembly()
		{
			string directory = VirtualPathUtility.GetDirectory(this.resource);
			Assembly assembly = AppResourcesCompiler.GetCachedLocalResourcesAssembly(directory);
			if (assembly == null)
			{
				assembly = new AppResourcesCompiler(directory).Compile();
				if (assembly == null)
				{
					throw new MissingManifestResourceException("A resource object was not found at the specified virtualPath.");
				}
			}
			return assembly;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000BE210 File Offset: 0x000BC410
		private ResourceManager GetResourceManager()
		{
			Assembly assembly;
			string fileName;
			if (this.isGlobal)
			{
				assembly = HttpContext.AppGlobalResourcesAssembly;
				fileName = this.resource;
			}
			else
			{
				assembly = this.GetLocalResourcesAssembly();
				fileName = Path.GetFileName(this.resource);
				if (string.IsNullOrEmpty(fileName))
				{
					return null;
				}
			}
			if (assembly == null)
			{
				return null;
			}
			ResourceManager resourceManager2;
			try
			{
				if (DefaultResourceProvider.resourceManagerCache == null)
				{
					DefaultResourceProvider.resourceManagerCache = new Dictionary<DefaultResourceProvider.ResourceManagerCacheKey, ResourceManager>();
				}
				DefaultResourceProvider.ResourceManagerCacheKey resourceManagerCacheKey = new DefaultResourceProvider.ResourceManagerCacheKey(fileName, assembly);
				ResourceManager resourceManager;
				if (!DefaultResourceProvider.resourceManagerCache.TryGetValue(resourceManagerCacheKey, out resourceManager))
				{
					resourceManager = new ResourceManager(fileName, assembly);
					resourceManager.IgnoreCase = true;
					DefaultResourceProvider.resourceManagerCache.Add(resourceManagerCacheKey, resourceManager);
				}
				resourceManager2 = resourceManager;
			}
			catch (MissingManifestResourceException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to retrieve the specified global resource object.", ex);
			}
			return resourceManager2;
		}

		// Token: 0x040024E6 RID: 9446
		[ThreadStatic]
		private static Dictionary<DefaultResourceProvider.ResourceManagerCacheKey, ResourceManager> resourceManagerCache;

		// Token: 0x040024E7 RID: 9447
		private string resource;

		// Token: 0x040024E8 RID: 9448
		private bool isGlobal;

		// Token: 0x02000650 RID: 1616
		private sealed class ResourceManagerCacheKey
		{
			// Token: 0x06004572 RID: 17778 RVA: 0x000BE2D8 File Offset: 0x000BC4D8
			public ResourceManagerCacheKey(string name, Assembly asm)
			{
				this._name = name;
				this._asm = asm;
			}

			// Token: 0x06004573 RID: 17779 RVA: 0x000BE2F0 File Offset: 0x000BC4F0
			public override bool Equals(object obj)
			{
				if (!(obj is DefaultResourceProvider.ResourceManagerCacheKey))
				{
					return false;
				}
				DefaultResourceProvider.ResourceManagerCacheKey resourceManagerCacheKey = (DefaultResourceProvider.ResourceManagerCacheKey)obj;
				return resourceManagerCacheKey._asm == this._asm && this._name.Equals(resourceManagerCacheKey._name, StringComparison.Ordinal);
			}

			// Token: 0x06004574 RID: 17780 RVA: 0x000BE335 File Offset: 0x000BC535
			public override int GetHashCode()
			{
				return this._name.GetHashCode() + this._asm.GetHashCode();
			}

			// Token: 0x040024E9 RID: 9449
			private readonly string _name;

			// Token: 0x040024EA RID: 9450
			private readonly Assembly _asm;
		}
	}
}
