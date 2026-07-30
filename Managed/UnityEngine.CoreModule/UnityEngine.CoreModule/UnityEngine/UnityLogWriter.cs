using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000165 RID: 357
	[NativeHeader("Runtime/Export/Logging/UnityLogWriter.bindings.h")]
	internal class UnityLogWriter : TextWriter
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x00016B00 File Offset: 0x00014D00
		[ThreadAndSerializationSafe]
		public static void WriteStringToUnityLog(string s)
		{
			bool flag = s == null;
			if (!flag)
			{
				UnityLogWriter.WriteStringToUnityLogImpl(s);
			}
		}

		// Token: 0x0600102C RID: 4140
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void WriteStringToUnityLogImpl(string s);

		// Token: 0x0600102D RID: 4141 RVA: 0x00016B1F File Offset: 0x00014D1F
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00016B30 File Offset: 0x00014D30
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00016B47 File Offset: 0x00014D47
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00016B57 File Offset: 0x00014D57
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00016B61 File Offset: 0x00014D61
		public override void Write(char[] buffer, int index, int count)
		{
			UnityLogWriter.WriteStringToUnityLogImpl(new string(buffer, index, count));
		}
	}
}
