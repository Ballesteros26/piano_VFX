using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x02000023 RID: 35
	internal abstract class ResXDataNodeHandler
	{
		// Token: 0x060000A1 RID: 161
		public abstract object GetValue(ITypeResolutionService typeResolver);

		// Token: 0x060000A2 RID: 162
		public abstract object GetValue(AssemblyName[] assemblyNames);

		// Token: 0x060000A3 RID: 163
		public abstract string GetValueTypeName(ITypeResolutionService typeResolver);

		// Token: 0x060000A4 RID: 164
		public abstract string GetValueTypeName(AssemblyName[] assemblyNames);

		// Token: 0x060000A5 RID: 165 RVA: 0x00003F8B File Offset: 0x0000218B
		public virtual object GetValueForResX()
		{
			return this.GetValue(null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F94 File Offset: 0x00002194
		protected Type ResolveType(string typeString)
		{
			return Type.GetType(typeString);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F9C File Offset: 0x0000219C
		protected Type ResolveType(string typeString, AssemblyName[] assemblyNames)
		{
			Type type = null;
			if (assemblyNames != null)
			{
				for (int i = 0; i < assemblyNames.Length; i++)
				{
					type = Assembly.Load(assemblyNames[i]).GetType(typeString, false);
					if (type != null)
					{
						return type;
					}
				}
			}
			if (type == null)
			{
				type = this.ResolveType(typeString);
			}
			return type;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FEC File Offset: 0x000021EC
		protected Type ResolveType(string typeString, ITypeResolutionService typeResolver)
		{
			Type type = null;
			if (typeResolver != null)
			{
				type = typeResolver.GetType(typeString);
			}
			if (type == null)
			{
				type = this.ResolveType(typeString);
			}
			return type;
		}
	}
}
