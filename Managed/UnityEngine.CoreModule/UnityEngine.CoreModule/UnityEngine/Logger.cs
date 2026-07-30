using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x02000164 RID: 356
	public class Logger : ILogger, ILogHandler
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x000166AA File Offset: 0x000148AA
		private Logger()
		{
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000166B4 File Offset: 0x000148B4
		public Logger(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
			this.logEnabled = true;
			this.filterLogType = LogType.Log;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x000166D6 File Offset: 0x000148D6
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x000166DE File Offset: 0x000148DE
		public ILogHandler logHandler { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x000166E7 File Offset: 0x000148E7
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x000166EF File Offset: 0x000148EF
		public bool logEnabled { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000166F8 File Offset: 0x000148F8
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x00016700 File Offset: 0x00014900
		public LogType filterLogType { get; set; }

		// Token: 0x0600101A RID: 4122 RVA: 0x0001670C File Offset: 0x0001490C
		public bool IsLogTypeAllowed(LogType logType)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				bool flag = logType == LogType.Exception;
				if (flag)
				{
					return true;
				}
				bool flag2 = this.filterLogType != LogType.Exception;
				if (flag2)
				{
					return logType <= this.filterLogType;
				}
			}
			return false;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00016758 File Offset: 0x00014958
		private static string GetString(object message)
		{
			bool flag = message == null;
			string text;
			if (flag)
			{
				text = "Null";
			}
			else
			{
				IFormattable formattable = message as IFormattable;
				bool flag2 = formattable != null;
				if (flag2)
				{
					text = formattable.ToString(null, CultureInfo.InvariantCulture);
				}
				else
				{
					text = message.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000167A4 File Offset: 0x000149A4
		public void Log(LogType logType, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000167E0 File Offset: 0x000149E0
		public void Log(LogType logType, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0001681C File Offset: 0x00014A1C
		public void Log(LogType logType, string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0001685C File Offset: 0x00014A5C
		public void Log(LogType logType, string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0001689C File Offset: 0x00014A9C
		public void Log(object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}", new object[] { Logger.GetString(message) });
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000168D8 File Offset: 0x00014AD8
		public void Log(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00016918 File Offset: 0x00014B18
		public void Log(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00016958 File Offset: 0x00014B58
		public void LogWarning(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00016998 File Offset: 0x00014B98
		public void LogWarning(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000169D8 File Offset: 0x00014BD8
		public void LogError(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00016A18 File Offset: 0x00014C18
		public void LogError(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00016A58 File Offset: 0x00014C58
		public void LogFormat(LogType logType, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, format, args);
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00016A84 File Offset: 0x00014C84
		public void LogException(Exception exception)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, null);
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00016AAC File Offset: 0x00014CAC
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, format, args);
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public void LogException(Exception exception, Object context)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, context);
			}
		}

		// Token: 0x040005A5 RID: 1445
		private const string kNoTagFormat = "{0}";

		// Token: 0x040005A6 RID: 1446
		private const string kTagFormat = "{0}: {1}";
	}
}
