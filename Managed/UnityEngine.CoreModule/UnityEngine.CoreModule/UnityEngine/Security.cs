using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001CF RID: 463
	public sealed class Security
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x000219A0 File Offset: 0x0001FBA0
		[EditorBrowsable(1)]
		[Obsolete("This was an internal method which is no longer used", true)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData, string authorizationKey)
		{
			return null;
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000219B4 File Offset: 0x0001FBB4
		[Obsolete("This was an internal method which is no longer used", true)]
		[EditorBrowsable(1)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData)
		{
			return null;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000219C8 File Offset: 0x0001FBC8
		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		[ExcludeFromDocs]
		public static bool PrefetchSocketPolicy(string ip, int atPort)
		{
			int num = 3000;
			return Security.PrefetchSocketPolicy(ip, atPort, num);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000219E8 File Offset: 0x0001FBE8
		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		public static bool PrefetchSocketPolicy(string ip, int atPort, [DefaultValue("3000")] int timeout)
		{
			return false;
		}
	}
}
