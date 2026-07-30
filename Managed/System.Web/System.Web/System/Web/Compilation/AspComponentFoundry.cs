using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200061B RID: 1563
	internal class AspComponentFoundry
	{
		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000B31E9 File Offset: 0x000B13E9
		private Dictionary<string, AspComponent> Components
		{
			get
			{
				if (this.components == null)
				{
					this.components = new Dictionary<string, AspComponent>(StringComparer.OrdinalIgnoreCase);
				}
				return this.components;
			}
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000B320C File Offset: 0x000B140C
		public AspComponentFoundry()
		{
			this.foundries = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			Assembly assembly = typeof(AspComponentFoundry).Assembly;
			this.RegisterFoundry("asp", assembly, "System.Web.UI.WebControls");
			this.RegisterFoundry("", "object", typeof(ObjectTag));
			this.RegisterConfigControls();
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000B3270 File Offset: 0x000B1470
		public AspComponent GetComponent(string tagName)
		{
			if (tagName == null || tagName.Length == 0)
			{
				return null;
			}
			AspComponent aspComponent;
			if (this.components != null && this.components.TryGetValue(tagName, out aspComponent))
			{
				return aspComponent;
			}
			int num = tagName.IndexOf(':');
			string text;
			string text2;
			if (num > -1)
			{
				if (num == 0)
				{
					throw new Exception("Empty TagPrefix is not valid.");
				}
				if (num + 1 == tagName.Length)
				{
					return null;
				}
				text = tagName.Substring(0, num);
				text2 = tagName.Substring(num + 1);
			}
			else
			{
				text = string.Empty;
				text2 = tagName;
			}
			object obj = this.foundries[text];
			if (obj == null)
			{
				return null;
			}
			AspComponentFoundry.Foundry foundry = obj as AspComponentFoundry.Foundry;
			if (foundry != null)
			{
				return this.CreateComponent(foundry, tagName, text, text2);
			}
			ArrayList arrayList = obj as ArrayList;
			if (arrayList == null)
			{
				return null;
			}
			Exception ex = null;
			foreach (object obj2 in arrayList)
			{
				AspComponentFoundry.Foundry foundry2 = (AspComponentFoundry.Foundry)obj2;
				try
				{
					AspComponent aspComponent2 = this.CreateComponent(foundry2, tagName, text, text2);
					if (aspComponent2 != null)
					{
						return aspComponent2;
					}
				}
				catch (Exception ex)
				{
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return null;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x000B33A8 File Offset: 0x000B15A8
		private AspComponent CreateComponent(AspComponentFoundry.Foundry foundry, string tagName, string prefix, string tag)
		{
			string text;
			string text2;
			Type type = foundry.GetType(tag, out text, out text2);
			if (type == null)
			{
				return null;
			}
			AspComponent aspComponent = new AspComponent(type, text2, prefix, text, foundry.FromConfig);
			this.Components.Add(tagName, aspComponent);
			return aspComponent;
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000B33EB File Offset: 0x000B15EB
		public void RegisterFoundry(string foundryName, Assembly assembly, string nameSpace)
		{
			this.RegisterFoundry(foundryName, assembly, nameSpace, false);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x000B33F8 File Offset: 0x000B15F8
		public void RegisterFoundry(string foundryName, Assembly assembly, string nameSpace, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.AssemblyFoundry(assembly, nameSpace)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000B341F File Offset: 0x000B161F
		public void RegisterFoundry(string foundryName, string tagName, Type type)
		{
			this.RegisterFoundry(foundryName, tagName, type, false);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000B342C File Offset: 0x000B162C
		public void RegisterFoundry(string foundryName, string tagName, Type type, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.TagNameFoundry(tagName, type)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x000B3453 File Offset: 0x000B1653
		public void RegisterFoundry(string foundryName, string tagName, string source)
		{
			this.RegisterFoundry(foundryName, tagName, source, false);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000B3460 File Offset: 0x000B1660
		public void RegisterFoundry(string foundryName, string tagName, string source, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.TagNameFoundry(tagName, source)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000B3488 File Offset: 0x000B1688
		public void RegisterAssemblyFoundry(string foundryName, string assemblyName, string nameSpace, bool fromConfig)
		{
			this.InternalRegister(foundryName, new AspComponentFoundry.AssemblyFoundry(assemblyName, nameSpace)
			{
				FromConfig = fromConfig
			}, fromConfig);
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x000B34B0 File Offset: 0x000B16B0
		private void RegisterConfigControls()
		{
			PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
			if (pagesSection == null)
			{
				return;
			}
			TagPrefixCollection controls = pagesSection.Controls;
			if (controls == null || controls.Count == 0)
			{
				return;
			}
			IList codeAssemblies = BuildManager.CodeAssemblies;
			bool flag = codeAssemblies != null && codeAssemblies.Count > 0;
			foreach (object obj in controls)
			{
				TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)obj;
				if (!string.IsNullOrEmpty(tagPrefixInfo.TagName))
				{
					this.RegisterFoundry(tagPrefixInfo.TagPrefix, tagPrefixInfo.TagName, tagPrefixInfo.Source, true);
				}
				else
				{
					if (string.IsNullOrEmpty(tagPrefixInfo.Assembly))
					{
						if (!flag)
						{
							continue;
						}
						using (IEnumerator enumerator2 = codeAssemblies.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								Assembly assembly = obj2 as Assembly;
								if (!(assembly == null))
								{
									this.RegisterFoundry(tagPrefixInfo.TagPrefix, assembly, tagPrefixInfo.Namespace, true);
								}
							}
							continue;
						}
					}
					if (!string.IsNullOrEmpty(tagPrefixInfo.Namespace))
					{
						this.RegisterAssemblyFoundry(tagPrefixInfo.TagPrefix, tagPrefixInfo.Assembly, tagPrefixInfo.Namespace, true);
					}
				}
			}
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000B361C File Offset: 0x000B181C
		private void InternalRegister(string foundryName, AspComponentFoundry.Foundry foundry, bool fromConfig)
		{
			object obj = this.foundries[foundryName];
			AspComponentFoundry.Foundry foundry2 = null;
			if (obj is AspComponentFoundry.CompoundFoundry)
			{
				((AspComponentFoundry.CompoundFoundry)obj).Add(foundry);
				return;
			}
			if (obj == null || obj is ArrayList || (obj is AspComponentFoundry.AssemblyFoundry && foundry is AspComponentFoundry.AssemblyFoundry))
			{
				foundry2 = foundry;
			}
			else if (obj != null)
			{
				AspComponentFoundry.CompoundFoundry compoundFoundry = new AspComponentFoundry.CompoundFoundry(foundryName);
				compoundFoundry.Add((AspComponentFoundry.Foundry)obj);
				compoundFoundry.Add(foundry);
				foundry2 = foundry;
				foundry2.FromConfig = fromConfig;
			}
			if (foundry2 == null)
			{
				return;
			}
			if (obj == null)
			{
				this.foundries[foundryName] = foundry2;
				return;
			}
			ArrayList arrayList = obj as ArrayList;
			if (arrayList == null)
			{
				arrayList = new ArrayList(2);
				arrayList.Add(obj);
				this.foundries[foundryName] = arrayList;
			}
			if (foundry2 is AspComponentFoundry.AssemblyFoundry)
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					if (arrayList[i] is AspComponentFoundry.AssemblyFoundry)
					{
						arrayList.Insert(i, foundry2);
						return;
					}
				}
				arrayList.Add(foundry2);
				return;
			}
			arrayList.Insert(0, foundry2);
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x000B370C File Offset: 0x000B190C
		public bool LookupFoundry(string foundryName)
		{
			return this.foundries.Contains(foundryName);
		}

		// Token: 0x040023EF RID: 9199
		private Hashtable foundries;

		// Token: 0x040023F0 RID: 9200
		private Dictionary<string, AspComponent> components;

		// Token: 0x0200061C RID: 1564
		private abstract class Foundry
		{
			// Token: 0x17001534 RID: 5428
			// (get) Token: 0x06004334 RID: 17204 RVA: 0x000B371A File Offset: 0x000B191A
			// (set) Token: 0x06004335 RID: 17205 RVA: 0x000B3722 File Offset: 0x000B1922
			public bool FromConfig
			{
				get
				{
					return this._fromConfig;
				}
				set
				{
					this._fromConfig = value;
				}
			}

			// Token: 0x06004336 RID: 17206
			public abstract Type GetType(string componentName, out string source, out string ns);

			// Token: 0x040023F1 RID: 9201
			private bool _fromConfig;
		}

		// Token: 0x0200061D RID: 1565
		private class TagNameFoundry : AspComponentFoundry.Foundry
		{
			// Token: 0x17001535 RID: 5429
			// (get) Token: 0x06004338 RID: 17208 RVA: 0x000B372B File Offset: 0x000B192B
			public bool FromWebConfig
			{
				get
				{
					return this.source != null;
				}
			}

			// Token: 0x06004339 RID: 17209 RVA: 0x000B3736 File Offset: 0x000B1936
			public TagNameFoundry(string tagName, string source)
			{
				this.tagName = tagName;
				this.source = source;
			}

			// Token: 0x0600433A RID: 17210 RVA: 0x000B374C File Offset: 0x000B194C
			public TagNameFoundry(string tagName, Type type)
			{
				this.tagName = tagName;
				this.type = type;
			}

			// Token: 0x0600433B RID: 17211 RVA: 0x000B3762 File Offset: 0x000B1962
			public override Type GetType(string componentName, out string source, out string ns)
			{
				source = null;
				ns = null;
				if (string.Compare(componentName, this.tagName, true, Helpers.InvariantCulture) != 0)
				{
					return null;
				}
				source = this.source;
				return this.LoadType();
			}

			// Token: 0x0600433C RID: 17212 RVA: 0x000B3790 File Offset: 0x000B1990
			private Type LoadType()
			{
				if (this.type != null)
				{
					return this.type;
				}
				HttpContext httpContext = HttpContext.Current;
				string text;
				string text2;
				if (VirtualPathUtility.IsAppRelative(this.source))
				{
					text = this.source;
					text2 = httpContext.Request.MapPath(this.source);
				}
				else
				{
					text = VirtualPathUtility.ToAppRelative(this.source);
					text2 = this.source;
				}
				if ((this.type = CachingCompiler.GetTypeFromCache(text2)) != null)
				{
					return this.type;
				}
				this.type = BuildManager.GetCompiledType(text);
				if (this.type != null)
				{
					AspGenerator.AddTypeToCache(null, text2, this.type);
					BuildManager.AddToReferencedAssemblies(this.type.Assembly);
				}
				return this.type;
			}

			// Token: 0x17001536 RID: 5430
			// (get) Token: 0x0600433D RID: 17213 RVA: 0x000B384E File Offset: 0x000B1A4E
			public string TagName
			{
				get
				{
					return this.tagName;
				}
			}

			// Token: 0x040023F2 RID: 9202
			private string tagName;

			// Token: 0x040023F3 RID: 9203
			private Type type;

			// Token: 0x040023F4 RID: 9204
			private string source;
		}

		// Token: 0x0200061E RID: 1566
		private class AssemblyFoundry : AspComponentFoundry.Foundry
		{
			// Token: 0x0600433E RID: 17214 RVA: 0x000B3856 File Offset: 0x000B1A56
			public AssemblyFoundry(Assembly assembly, string nameSpace)
			{
				this.assembly = assembly;
				this.nameSpace = nameSpace;
				if (assembly != null)
				{
					this.assemblyName = assembly.FullName;
					return;
				}
				this.assemblyName = null;
			}

			// Token: 0x0600433F RID: 17215 RVA: 0x000B3889 File Offset: 0x000B1A89
			public AssemblyFoundry(string assemblyName, string nameSpace)
			{
				this.assembly = null;
				this.nameSpace = nameSpace;
				this.assemblyName = assemblyName;
			}

			// Token: 0x06004340 RID: 17216 RVA: 0x000B38A8 File Offset: 0x000B1AA8
			public override Type GetType(string componentName, out string source, out string ns)
			{
				source = null;
				ns = this.nameSpace;
				if (this.assembly == null && this.assemblyName != null)
				{
					this.assembly = this.GetAssemblyByName(this.assemblyName, true);
				}
				string text = this.nameSpace + "." + componentName;
				if (this.assembly != null)
				{
					return this.assembly.GetType(text, false, true);
				}
				IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
				if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
				{
					foreach (object obj in topLevelAssemblies)
					{
						Assembly assembly = (Assembly)obj;
						if (!(assembly == null))
						{
							Type type = assembly.GetType(text, false, true);
							if (type != null)
							{
								return type;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06004341 RID: 17217 RVA: 0x000B3998 File Offset: 0x000B1B98
			private Assembly GetAssemblyByName(string name, bool throwOnMissing)
			{
				if (this.assemblyCache == null)
				{
					this.assemblyCache = new Dictionary<string, Assembly>();
				}
				if (this.assemblyCache.ContainsKey(name))
				{
					return this.assemblyCache[name];
				}
				Assembly assembly = null;
				Exception ex = null;
				if (name.IndexOf(',') != -1)
				{
					try
					{
						assembly = Assembly.Load(name);
					}
					catch (Exception ex)
					{
					}
				}
				if (assembly == null)
				{
					try
					{
						assembly = Assembly.LoadWithPartialName(name);
					}
					catch (Exception ex)
					{
					}
				}
				if (!(assembly == null))
				{
					this.assemblyCache.Add(name, assembly);
					return assembly;
				}
				if (throwOnMissing)
				{
					throw new HttpException("Assembly " + name + " not found", ex);
				}
				return null;
			}

			// Token: 0x040023F5 RID: 9205
			private string nameSpace;

			// Token: 0x040023F6 RID: 9206
			private Assembly assembly;

			// Token: 0x040023F7 RID: 9207
			private string assemblyName;

			// Token: 0x040023F8 RID: 9208
			private Dictionary<string, Assembly> assemblyCache;
		}

		// Token: 0x0200061F RID: 1567
		private class CompoundFoundry : AspComponentFoundry.Foundry
		{
			// Token: 0x06004342 RID: 17218 RVA: 0x000B3A54 File Offset: 0x000B1C54
			public CompoundFoundry(string tagPrefix)
			{
				this.tagPrefix = tagPrefix;
				this.tagnames = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			}

			// Token: 0x06004343 RID: 17219 RVA: 0x000B3A74 File Offset: 0x000B1C74
			public void Add(AspComponentFoundry.Foundry foundry)
			{
				if (foundry is AspComponentFoundry.AssemblyFoundry)
				{
					this.assemblyFoundry = (AspComponentFoundry.AssemblyFoundry)foundry;
					return;
				}
				AspComponentFoundry.TagNameFoundry tagNameFoundry = (AspComponentFoundry.TagNameFoundry)foundry;
				string tagName = tagNameFoundry.TagName;
				if (!this.tagnames.Contains(tagName))
				{
					this.tagnames.Add(tagName, foundry);
					return;
				}
				if (tagNameFoundry.FromWebConfig)
				{
					return;
				}
				throw new ApplicationException(string.Format("{0}:{1} already registered.", this.tagPrefix, tagName));
			}

			// Token: 0x06004344 RID: 17220 RVA: 0x000B3AE0 File Offset: 0x000B1CE0
			public override Type GetType(string componentName, out string source, out string ns)
			{
				source = null;
				ns = null;
				AspComponentFoundry.Foundry foundry = this.tagnames[componentName] as AspComponentFoundry.Foundry;
				if (foundry != null)
				{
					return foundry.GetType(componentName, out source, out ns);
				}
				if (this.assemblyFoundry != null)
				{
					try
					{
						return this.assemblyFoundry.GetType(componentName, out source, out ns);
					}
					catch
					{
					}
				}
				throw new ApplicationException(string.Format("Type {0} not registered for prefix {1}", componentName, this.tagPrefix));
			}

			// Token: 0x040023F9 RID: 9209
			private AspComponentFoundry.AssemblyFoundry assemblyFoundry;

			// Token: 0x040023FA RID: 9210
			private Hashtable tagnames;

			// Token: 0x040023FB RID: 9211
			private string tagPrefix;
		}
	}
}
