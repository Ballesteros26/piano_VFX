using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.Xml.Serialization
{
	// Token: 0x0200035C RID: 860
	internal static class DynamicAssemblies
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000D3B88 File Offset: 0x000D1D88
		private static FileIOPermission UnrestrictedFileIOPermission
		{
			get
			{
				if (DynamicAssemblies.fileIOPermission == null)
				{
					DynamicAssemblies.fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				}
				return DynamicAssemblies.fileIOPermission;
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000D3BA8 File Offset: 0x000D1DA8
		internal static bool IsTypeDynamic(Type type)
		{
			object obj = DynamicAssemblies.tableIsTypeDynamic[type];
			if (obj == null)
			{
				DynamicAssemblies.UnrestrictedFileIOPermission.Assert();
				Assembly assembly = type.Assembly;
				bool flag = assembly.IsDynamic || string.IsNullOrEmpty(assembly.Location);
				if (!flag)
				{
					if (type.IsArray)
					{
						flag = DynamicAssemblies.IsTypeDynamic(type.GetElementType());
					}
					else if (type.IsGenericType)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments != null)
						{
							foreach (Type type2 in genericArguments)
							{
								if (!(type2 == null) && !type2.IsGenericParameter)
								{
									flag = DynamicAssemblies.IsTypeDynamic(type2);
									if (flag)
									{
										break;
									}
								}
							}
						}
					}
				}
				obj = (DynamicAssemblies.tableIsTypeDynamic[type] = flag);
			}
			return (bool)obj;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000D3C6C File Offset: 0x000D1E6C
		internal static bool IsTypeDynamic(Type[] arguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				if (DynamicAssemblies.IsTypeDynamic(arguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000D3C98 File Offset: 0x000D1E98
		internal static void Add(Assembly a)
		{
			Hashtable hashtable = DynamicAssemblies.nameToAssemblyMap;
			lock (hashtable)
			{
				if (DynamicAssemblies.assemblyToNameMap[a] == null)
				{
					Assembly assembly = DynamicAssemblies.nameToAssemblyMap[a.FullName] as Assembly;
					string text = null;
					if (assembly == null)
					{
						text = a.FullName;
					}
					else if (assembly != a)
					{
						text = a.FullName + ", " + DynamicAssemblies.nameToAssemblyMap.Count;
					}
					if (text != null)
					{
						DynamicAssemblies.nameToAssemblyMap.Add(text, a);
						DynamicAssemblies.assemblyToNameMap.Add(a, text);
					}
				}
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000D3D60 File Offset: 0x000D1F60
		internal static Assembly Get(string fullName)
		{
			if (DynamicAssemblies.nameToAssemblyMap == null)
			{
				return null;
			}
			return (Assembly)DynamicAssemblies.nameToAssemblyMap[fullName];
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000D3D7F File Offset: 0x000D1F7F
		internal static string GetName(Assembly a)
		{
			if (DynamicAssemblies.assemblyToNameMap == null)
			{
				return null;
			}
			return (string)DynamicAssemblies.assemblyToNameMap[a];
		}

		// Token: 0x0400184A RID: 6218
		private static ArrayList assembliesInConfig = new ArrayList();

		// Token: 0x0400184B RID: 6219
		private static volatile Hashtable nameToAssemblyMap = new Hashtable();

		// Token: 0x0400184C RID: 6220
		private static volatile Hashtable assemblyToNameMap = new Hashtable();

		// Token: 0x0400184D RID: 6221
		private static Hashtable tableIsTypeDynamic = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400184E RID: 6222
		private static volatile FileIOPermission fileIOPermission;
	}
}
