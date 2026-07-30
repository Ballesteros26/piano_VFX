using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000BC RID: 188
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	internal sealed class DebugLogHandler : ILogHandler
	{
		// Token: 0x06000468 RID: 1128
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		internal static extern void Internal_Log(LogType level, LogOption options, string msg, Object obj);

		// Token: 0x06000469 RID: 1129
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		internal static extern void Internal_LogException(Exception exception, Object obj);

		// Token: 0x0600046A RID: 1130 RVA: 0x000068DC File Offset: 0x00004ADC
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, LogOption.None, string.Format(format, args), context);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000068F0 File Offset: 0x00004AF0
		public void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, logOptions, string.Format(format, args), context);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00006908 File Offset: 0x00004B08
		public void LogException(Exception exception, Object context)
		{
			bool flag = exception == null;
			if (flag)
			{
				throw new ArgumentNullException("exception");
			}
			DebugLogHandler.Internal_LogException(exception, context);
		}
	}
}
