using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000322 RID: 802
	[ComDefaultInterface(typeof(_Assembly))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	internal class MonoAssembly : RuntimeAssembly
	{
		// Token: 0x06002341 RID: 9025 RVA: 0x00082182 File Offset: 0x00080382
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			return base.InternalGetType(null, name, throwOnError, ignoreCase);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000821B0 File Offset: 0x000803B0
		public override Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			foreach (Module module in this.GetModules(true))
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00082209 File Offset: 0x00080409
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00082214 File Offset: 0x00080414
		public override Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				List<Module> list = new List<Module>(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						list.Add(module);
					}
				}
				return list.ToArray();
			}
			return modulesInternal;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00082262 File Offset: 0x00080462
		[MonoTODO("Always returns the same as GetModules")]
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0008226B File Offset: 0x0008046B
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			return base.GetSatelliteAssembly(culture, null, true);
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00082276 File Offset: 0x00080476
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			return base.GetSatelliteAssembly(culture, version, true);
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00082281 File Offset: 0x00080481
		[ComVisible(false)]
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00082289 File Offset: 0x00080489
		public override bool GlobalAssemblyCache
		{
			get
			{
				return base.get_global_assembly_cache();
			}
		}
	}
}
