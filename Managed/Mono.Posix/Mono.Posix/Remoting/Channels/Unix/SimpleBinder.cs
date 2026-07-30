using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000081 RID: 129
	internal class SimpleBinder : SerializationBinder
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0000E624 File Offset: 0x0000C824
		public override Type BindToType(string assemblyName, string typeName)
		{
			Assembly assembly;
			if (assemblyName.IndexOf(',') != -1)
			{
				try
				{
					assembly = Assembly.Load(assemblyName);
					if (assembly == null)
					{
						return null;
					}
					Type type = assembly.GetType(typeName);
					if (type != null)
					{
						return type;
					}
				}
				catch
				{
				}
			}
			assembly = Assembly.LoadWithPartialName(assemblyName);
			if (assembly == null)
			{
				return null;
			}
			return assembly.GetType(typeName, true);
		}

		// Token: 0x040004A4 RID: 1188
		public static SimpleBinder Instance = new SimpleBinder();
	}
}
