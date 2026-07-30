using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x0200060C RID: 1548
	internal class AssemblyPathResolver
	{
		// Token: 0x060042BB RID: 17083 RVA: 0x000AFDD4 File Offset: 0x000ADFD4
		public static string GetAssemblyPath(string assemblyName)
		{
			Dictionary<string, string> dictionary = AssemblyPathResolver.assemblyCache;
			string text;
			lock (dictionary)
			{
				if (AssemblyPathResolver.assemblyCache.ContainsKey(assemblyName))
				{
					text = AssemblyPathResolver.assemblyCache[assemblyName];
				}
				else
				{
					Assembly assembly = null;
					Exception ex = null;
					if (assemblyName.IndexOf(',') != -1)
					{
						try
						{
							assembly = Assembly.Load(assemblyName);
						}
						catch (Exception ex)
						{
						}
					}
					if (assembly == null)
					{
						try
						{
							assembly = Assembly.LoadWithPartialName(assemblyName);
						}
						catch (Exception ex)
						{
						}
					}
					if (assembly == null)
					{
						throw new HttpException(string.Format("Unable to find assembly {0}", assemblyName), ex);
					}
					string localPath = new Uri(assembly.CodeBase).LocalPath;
					AssemblyPathResolver.assemblyCache.Add(assemblyName, localPath);
					text = localPath;
				}
			}
			return text;
		}

		// Token: 0x040023BE RID: 9150
		private static Dictionary<string, string> assemblyCache = new Dictionary<string, string>();
	}
}
