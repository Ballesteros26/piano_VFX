using System;

namespace UnityEngine
{
	// Token: 0x02000162 RID: 354
	public interface ILogger : ILogHandler
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000FFC RID: 4092
		// (set) Token: 0x06000FFD RID: 4093
		ILogHandler logHandler { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000FFE RID: 4094
		// (set) Token: 0x06000FFF RID: 4095
		bool logEnabled { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001000 RID: 4096
		// (set) Token: 0x06001001 RID: 4097
		LogType filterLogType { get; set; }

		// Token: 0x06001002 RID: 4098
		bool IsLogTypeAllowed(LogType logType);

		// Token: 0x06001003 RID: 4099
		void Log(LogType logType, object message);

		// Token: 0x06001004 RID: 4100
		void Log(LogType logType, object message, Object context);

		// Token: 0x06001005 RID: 4101
		void Log(LogType logType, string tag, object message);

		// Token: 0x06001006 RID: 4102
		void Log(LogType logType, string tag, object message, Object context);

		// Token: 0x06001007 RID: 4103
		void Log(object message);

		// Token: 0x06001008 RID: 4104
		void Log(string tag, object message);

		// Token: 0x06001009 RID: 4105
		void Log(string tag, object message, Object context);

		// Token: 0x0600100A RID: 4106
		void LogWarning(string tag, object message);

		// Token: 0x0600100B RID: 4107
		void LogWarning(string tag, object message, Object context);

		// Token: 0x0600100C RID: 4108
		void LogError(string tag, object message);

		// Token: 0x0600100D RID: 4109
		void LogError(string tag, object message, Object context);

		// Token: 0x0600100E RID: 4110
		void LogFormat(LogType logType, string format, params object[] args);

		// Token: 0x0600100F RID: 4111
		void LogException(Exception exception);
	}
}
