using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200063C RID: 1596
	internal sealed class BuildManagerDirectoryBuilder
	{
		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x000BC2E2 File Offset: 0x000BA4E2
		private CompilationSection CompilationSection
		{
			get
			{
				if (this.compilationSection == null)
				{
					this.compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
				}
				return this.compilationSection;
			}
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x000BC307 File Offset: 0x000BA507
		public BuildManagerDirectoryBuilder(VirtualPath virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			this.vpp = HostingEnvironment.VirtualPathProvider;
			this.virtualPath = virtualPath;
			this.virtualPathDirectory = VirtualPathUtility.GetDirectory(virtualPath.Absolute);
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x000BC340 File Offset: 0x000BA540
		public List<BuildProviderGroup> Build(bool single)
		{
			if (StrUtils.StartsWith(this.virtualPath.AppRelative, "~/App_Themes/"))
			{
				ThemeDirectoryBuildProvider themeDirectoryBuildProvider = new ThemeDirectoryBuildProvider();
				themeDirectoryBuildProvider.SetVirtualPath(this.virtualPath);
				return this.GetSingleBuildProviderGroup(themeDirectoryBuildProvider);
			}
			CompilationSection compilationSection = this.CompilationSection;
			BuildProviderCollection buildProviderCollection = ((compilationSection != null) ? compilationSection.BuildProviders : null);
			if (buildProviderCollection == null || buildProviderCollection.Count == 0)
			{
				return null;
			}
			if (this.virtualPath.IsFake)
			{
				BuildProvider buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(this.virtualPath, buildProviderCollection);
				if (buildProvider == null)
				{
					return null;
				}
				return this.GetSingleBuildProviderGroup(buildProvider);
			}
			else
			{
				if (single)
				{
					this.AddVirtualFile(this.GetVirtualFile(this.virtualPath.Absolute), buildProviderCollection);
				}
				else
				{
					Dictionary<string, bool> dictionary = new Dictionary<string, bool>(RuntimeHelpers.StringEqualityComparer);
					this.AddVirtualDir(this.GetVirtualDirectory(this.virtualPath.Absolute), buildProviderCollection, dictionary);
					if (this.buildProviders == null || this.buildProviders.Count == 0)
					{
						this.AddVirtualFile(this.GetVirtualFile(this.virtualPath.Absolute), buildProviderCollection);
					}
				}
				if (this.buildProviders == null || this.buildProviders.Count == 0)
				{
					return null;
				}
				List<BuildProviderGroup> list = new List<BuildProviderGroup>();
				foreach (BuildProvider buildProvider2 in this.buildProviders.Values)
				{
					this.AssignToGroup(buildProvider2, list);
				}
				if (list == null || list.Count == 0)
				{
					list = null;
					return null;
				}
				list.Reverse();
				return list;
			}
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x000BC4C0 File Offset: 0x000BA6C0
		private bool AddBuildProvider(BuildProvider buildProvider)
		{
			if (this.buildProviders == null)
			{
				this.buildProviders = new Dictionary<string, BuildProvider>(RuntimeHelpers.StringEqualityComparer);
			}
			string text = buildProvider.VirtualPath;
			if (this.buildProviders.ContainsKey(text))
			{
				return false;
			}
			this.buildProviders.Add(text, buildProvider);
			return true;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000BC50C File Offset: 0x000BA70C
		private void AddVirtualDir(VirtualDirectory vdir, BuildProviderCollection bpcoll, Dictionary<string, bool> cache)
		{
			if (vdir == null)
			{
				return;
			}
			List<string> list = new List<string>();
			foreach (object obj in vdir.Files)
			{
				string text = ((VirtualFile)obj).VirtualPath;
				if (!BuildManager.IgnoreVirtualPath(text))
				{
					BuildProvider buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(text, bpcoll);
					if (buildProvider != null && this.AddBuildProvider(buildProvider))
					{
						IDictionary<string, bool> dictionary = buildProvider.ExtractDependencies();
						if (dictionary != null)
						{
							list.Clear();
							foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
							{
								string key = keyValuePair.Key;
								string directory = VirtualPathUtility.GetDirectory(key);
								if (!cache.ContainsKey(directory))
								{
									cache.Add(directory, true);
									this.AddVirtualDir(this.GetVirtualDirectory(key), bpcoll, cache);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x000BC614 File Offset: 0x000BA814
		private void AddVirtualFile(VirtualFile file, BuildProviderCollection bpcoll)
		{
			if (file == null || BuildManager.IgnoreVirtualPath(file.VirtualPath))
			{
				return;
			}
			BuildProvider buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(file.VirtualPath, bpcoll);
			if (buildProvider == null)
			{
				return;
			}
			this.AddBuildProvider(buildProvider);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000BC64C File Offset: 0x000BA84C
		private List<BuildProviderGroup> GetSingleBuildProviderGroup(BuildProvider bp)
		{
			List<BuildProviderGroup> list = new List<BuildProviderGroup>();
			BuildProviderGroup buildProviderGroup = new BuildProviderGroup();
			buildProviderGroup.AddProvider(bp);
			list.Add(buildProviderGroup);
			return list;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000BC672 File Offset: 0x000BA872
		private VirtualDirectory GetVirtualDirectory(string virtualPath)
		{
			if (!this.vpp.DirectoryExists(VirtualPathUtility.GetDirectory(virtualPath)))
			{
				return null;
			}
			return this.vpp.GetDirectory(virtualPath);
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000BC695 File Offset: 0x000BA895
		private VirtualFile GetVirtualFile(string virtualPath)
		{
			if (!this.vpp.FileExists(virtualPath))
			{
				return null;
			}
			return this.vpp.GetFile(virtualPath);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000BC6B4 File Offset: 0x000BA8B4
		private Type GetBuildProviderCodeDomType(BuildProvider bp)
		{
			CompilerType compilerType = bp.CodeCompilerType;
			if (compilerType == null)
			{
				string text = bp.LanguageName;
				if (string.IsNullOrEmpty(text))
				{
					text = this.CompilationSection.DefaultLanguage;
				}
				compilerType = BuildManager.GetDefaultCompilerTypeForLanguage(text, this.CompilationSection, false);
			}
			Type type = ((compilerType != null) ? compilerType.CodeDomProviderType : null);
			if (type == null)
			{
				throw new HttpException("Unable to determine code compilation language provider for virtual path '" + bp.VirtualPath + "'.");
			}
			return type;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000BC724 File Offset: 0x000BA924
		private void AssignToGroup(BuildProvider buildProvider, List<BuildProviderGroup> groups)
		{
			if (this.IsDependencyCycle(buildProvider))
			{
				throw new HttpException("Dependency cycles are not suppported: " + buildProvider.VirtualPath);
			}
			BuildProviderGroup buildProviderGroup = null;
			string directory = VirtualPathUtility.GetDirectory(buildProvider.VirtualPath);
			if (BuildManager.HasCachedItemNoLock(buildProvider.VirtualPath))
			{
				return;
			}
			StringComparison stringComparison = RuntimeHelpers.StringComparison;
			if (buildProvider is ApplicationFileBuildProvider || buildProvider is ThemeDirectoryBuildProvider)
			{
				buildProviderGroup = new BuildProviderGroup();
				buildProviderGroup.Standalone = true;
				this.InsertGroup(buildProviderGroup, groups);
			}
			else
			{
				Type buildProviderCodeDomType = this.GetBuildProviderCodeDomType(buildProvider);
				foreach (BuildProviderGroup buildProviderGroup2 in groups)
				{
					if (!buildProviderGroup2.Standalone)
					{
						if (buildProviderGroup2.Count == 0)
						{
							buildProviderGroup = buildProviderGroup2;
							break;
						}
						bool flag = true;
						foreach (BuildProvider buildProvider2 in buildProviderGroup2)
						{
							if (this.IsDependency(buildProvider, buildProvider2))
							{
								flag = false;
								break;
							}
							if (string.Compare(directory, VirtualPathUtility.GetDirectory(buildProvider2.VirtualPath), stringComparison) != 0)
							{
								flag = false;
								break;
							}
							if (buildProviderCodeDomType != null)
							{
								Type buildProviderCodeDomType2 = this.GetBuildProviderCodeDomType(buildProvider2);
								if (buildProviderCodeDomType2 != null && buildProviderCodeDomType2 != buildProviderCodeDomType)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							buildProviderGroup = buildProviderGroup2;
							break;
						}
					}
				}
				if (buildProviderGroup == null)
				{
					buildProviderGroup = new BuildProviderGroup();
					this.InsertGroup(buildProviderGroup, groups);
				}
			}
			buildProviderGroup.AddProvider(buildProvider);
			if (string.Compare(directory, this.virtualPathDirectory, stringComparison) == 0)
			{
				buildProviderGroup.Master = true;
			}
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x000BC8CC File Offset: 0x000BAACC
		private void InsertGroup(BuildProviderGroup group, List<BuildProviderGroup> groups)
		{
			if (group.Application)
			{
				groups.Insert(groups.Count - 1, group);
				return;
			}
			int num;
			if (group.Standalone)
			{
				num = groups.FindLastIndex(new Predicate<BuildProviderGroup>(BuildManagerDirectoryBuilder.SkipApplicationGroup));
			}
			else
			{
				num = groups.FindLastIndex(new Predicate<BuildProviderGroup>(BuildManagerDirectoryBuilder.SkipStandaloneGroups));
			}
			if (num == -1)
			{
				groups.Add(group);
				return;
			}
			groups.Insert((num == 0) ? 0 : (num - 1), group);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x000BC93D File Offset: 0x000BAB3D
		private static bool SkipStandaloneGroups(BuildProviderGroup group)
		{
			return group != null && group.Standalone;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000BC94A File Offset: 0x000BAB4A
		private static bool SkipApplicationGroup(BuildProviderGroup group)
		{
			return group != null && group.Application;
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000BC958 File Offset: 0x000BAB58
		private bool IsDependency(BuildProvider bp1, BuildProvider bp2)
		{
			IDictionary<string, bool> dictionary = bp1.ExtractDependencies();
			if (dictionary == null)
			{
				return false;
			}
			if (dictionary.ContainsKey(bp2.VirtualPath))
			{
				return true;
			}
			foreach (KeyValuePair<string, bool> keyValuePair in dictionary)
			{
				BuildProvider buildProvider;
				if (this.buildProviders.TryGetValue(keyValuePair.Key, out buildProvider) && this.IsDependency(buildProvider, bp2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000BC9E0 File Offset: 0x000BABE0
		private bool IsDependencyCycle(BuildProvider buildProvider)
		{
			return this.IsDependencyCycle(new Dictionary<BuildProvider, bool> { { buildProvider, true } }, buildProvider.ExtractDependencies());
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x000BCA08 File Offset: 0x000BAC08
		private bool IsDependencyCycle(Dictionary<BuildProvider, bool> cache, IDictionary<string, bool> deps)
		{
			if (deps == null)
			{
				return false;
			}
			foreach (KeyValuePair<string, bool> keyValuePair in deps)
			{
				BuildProvider buildProvider;
				if (this.buildProviders.TryGetValue(keyValuePair.Key, out buildProvider))
				{
					if (cache.ContainsKey(buildProvider))
					{
						return true;
					}
					cache.Add(buildProvider, true);
					if (this.IsDependencyCycle(cache, buildProvider.ExtractDependencies()))
					{
						return true;
					}
					cache.Remove(buildProvider);
				}
			}
			return false;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000BCA98 File Offset: 0x000BAC98
		public static BuildProvider GetBuildProvider(string virtualPath, BuildProviderCollection coll)
		{
			return BuildManagerDirectoryBuilder.GetBuildProvider(new VirtualPath(virtualPath), coll);
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x000BCAA8 File Offset: 0x000BACA8
		public static BuildProvider GetBuildProvider(VirtualPath virtualPath, BuildProviderCollection coll)
		{
			if (virtualPath == null || string.IsNullOrEmpty(virtualPath.Original) || coll == null)
			{
				return null;
			}
			string extension = virtualPath.Extension;
			BuildProvider buildProvider = coll.GetProviderInstanceForExtension(extension);
			if (buildProvider == null)
			{
				if (string.Compare(extension, ".asax", StringComparison.OrdinalIgnoreCase) == 0)
				{
					buildProvider = new ApplicationFileBuildProvider();
				}
				else if (StrUtils.StartsWith(virtualPath.AppRelative, "~/App_Themes/"))
				{
					buildProvider = new ThemeDirectoryBuildProvider();
				}
				if (buildProvider != null)
				{
					buildProvider.SetVirtualPath(virtualPath);
				}
				return buildProvider;
			}
			object[] customAttributes = buildProvider.GetType().GetCustomAttributes(typeof(BuildProviderAppliesToAttribute), true);
			if (customAttributes != null && customAttributes.Length != 0 && (((BuildProviderAppliesToAttribute)customAttributes[0]).AppliesTo & BuildProviderAppliesTo.Web) == (BuildProviderAppliesTo)0)
			{
				return null;
			}
			buildProvider.SetVirtualPath(virtualPath);
			return buildProvider;
		}

		// Token: 0x040024B1 RID: 9393
		private readonly VirtualPath virtualPath;

		// Token: 0x040024B2 RID: 9394
		private readonly string virtualPathDirectory;

		// Token: 0x040024B3 RID: 9395
		private CompilationSection compilationSection;

		// Token: 0x040024B4 RID: 9396
		private Dictionary<string, BuildProvider> buildProviders;

		// Token: 0x040024B5 RID: 9397
		private VirtualPathProvider vpp;

		// Token: 0x0200063D RID: 1597
		private sealed class BuildProviderItem
		{
			// Token: 0x060044C9 RID: 17609 RVA: 0x000BCB4F File Offset: 0x000BAD4F
			public BuildProviderItem(BuildProvider bp, int listIndex, int parentIndex)
			{
				this.Provider = bp;
				this.ListIndex = listIndex;
				this.ParentIndex = parentIndex;
			}

			// Token: 0x040024B6 RID: 9398
			public BuildProvider Provider;

			// Token: 0x040024B7 RID: 9399
			public int ListIndex;

			// Token: 0x040024B8 RID: 9400
			public int ParentIndex;
		}
	}
}
