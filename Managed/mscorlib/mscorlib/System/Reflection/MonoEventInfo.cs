using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000323 RID: 803
	internal struct MonoEventInfo
	{
		// Token: 0x0600234B RID: 9035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_event_info(MonoEvent ev, out MonoEventInfo info);

		// Token: 0x0600234C RID: 9036 RVA: 0x0008229C File Offset: 0x0008049C
		internal static MonoEventInfo GetEventInfo(MonoEvent ev)
		{
			MonoEventInfo monoEventInfo;
			MonoEventInfo.get_event_info(ev, out monoEventInfo);
			return monoEventInfo;
		}

		// Token: 0x04001338 RID: 4920
		public Type declaring_type;

		// Token: 0x04001339 RID: 4921
		public Type reflected_type;

		// Token: 0x0400133A RID: 4922
		public string name;

		// Token: 0x0400133B RID: 4923
		public MethodInfo add_method;

		// Token: 0x0400133C RID: 4924
		public MethodInfo remove_method;

		// Token: 0x0400133D RID: 4925
		public MethodInfo raise_method;

		// Token: 0x0400133E RID: 4926
		public EventAttributes attrs;

		// Token: 0x0400133F RID: 4927
		public MethodInfo[] other_methods;
	}
}
