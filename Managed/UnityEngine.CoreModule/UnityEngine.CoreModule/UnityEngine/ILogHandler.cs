using System;

namespace UnityEngine
{
	// Token: 0x02000163 RID: 355
	public interface ILogHandler
	{
		// Token: 0x06001010 RID: 4112
		void LogFormat(LogType logType, Object context, string format, params object[] args);

		// Token: 0x06001011 RID: 4113
		void LogException(Exception exception, Object context);
	}
}
