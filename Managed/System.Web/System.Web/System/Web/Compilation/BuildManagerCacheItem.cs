using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace System.Web.Compilation
{
	// Token: 0x0200063B RID: 1595
	internal sealed class BuildManagerCacheItem
	{
		// Token: 0x060044B4 RID: 17588 RVA: 0x000BC1EB File Offset: 0x000BA3EB
		public BuildManagerCacheItem(Assembly assembly, BuildProvider bp, CompilerResults results)
		{
			this.BuiltAssembly = assembly;
			this.CompiledCustomString = bp.GetCustomString(results);
			this.VirtualPath = bp.VirtualPath;
			this.Type = bp.GetGeneratedType(results);
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000BC220 File Offset: 0x000BA420
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("BuildCacheItem [");
			bool flag = true;
			if (!string.IsNullOrEmpty(this.CompiledCustomString))
			{
				stringBuilder.Append("compiledCustomString: " + this.CompiledCustomString);
				flag = false;
			}
			if (this.BuiltAssembly != null)
			{
				stringBuilder.Append((flag ? string.Empty : "; ") + "assembly: " + this.BuiltAssembly.ToString());
				flag = false;
			}
			if (!string.IsNullOrEmpty(this.VirtualPath))
			{
				stringBuilder.Append((flag ? string.Empty : "; ") + "virtualPath: " + this.VirtualPath);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040024AD RID: 9389
		public readonly string CompiledCustomString;

		// Token: 0x040024AE RID: 9390
		public readonly Assembly BuiltAssembly;

		// Token: 0x040024AF RID: 9391
		public readonly string VirtualPath;

		// Token: 0x040024B0 RID: 9392
		public readonly Type Type;
	}
}
