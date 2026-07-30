using System;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x0200029B RID: 667
	internal sealed class OdbcEnvironment
	{
		// Token: 0x06001C7A RID: 7290 RVA: 0x00005C14 File Offset: 0x00003E14
		private OdbcEnvironment()
		{
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0008D9DC File Offset: 0x0008BBDC
		internal static OdbcEnvironmentHandle GetGlobalEnvironmentHandle()
		{
			OdbcEnvironmentHandle odbcEnvironmentHandle = OdbcEnvironment.s_globalEnvironmentHandle as OdbcEnvironmentHandle;
			if (odbcEnvironmentHandle == null)
			{
				object obj = OdbcEnvironment.s_globalEnvironmentHandleLock;
				lock (obj)
				{
					odbcEnvironmentHandle = OdbcEnvironment.s_globalEnvironmentHandle as OdbcEnvironmentHandle;
					if (odbcEnvironmentHandle == null)
					{
						odbcEnvironmentHandle = new OdbcEnvironmentHandle();
						OdbcEnvironment.s_globalEnvironmentHandle = odbcEnvironmentHandle;
					}
				}
			}
			return odbcEnvironmentHandle;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0008DA40 File Offset: 0x0008BC40
		internal static void ReleaseObjectPool()
		{
			object obj = Interlocked.Exchange(ref OdbcEnvironment.s_globalEnvironmentHandle, null);
			if (obj != null)
			{
				(obj as OdbcEnvironmentHandle).Dispose();
			}
		}

		// Token: 0x0400153A RID: 5434
		private static object s_globalEnvironmentHandle;

		// Token: 0x0400153B RID: 5435
		private static object s_globalEnvironmentHandleLock = new object();
	}
}
