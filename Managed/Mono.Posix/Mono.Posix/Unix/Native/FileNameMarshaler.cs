using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000025 RID: 37
	internal class FileNameMarshaler : ICustomMarshaler
	{
		// Token: 0x06000202 RID: 514 RVA: 0x000079E5 File Offset: 0x00005BE5
		public static ICustomMarshaler GetInstance(string s)
		{
			return FileNameMarshaler.Instance;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000079EC File Offset: 0x00005BEC
		public void CleanUpManagedData(object o)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000079EE File Offset: 0x00005BEE
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			UnixMarshal.FreeHeap(pNativeData);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000079F6 File Offset: 0x00005BF6
		public int GetNativeDataSize()
		{
			return IntPtr.Size;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007A00 File Offset: 0x00005C00
		public IntPtr MarshalManagedToNative(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				return IntPtr.Zero;
			}
			return UnixMarshal.StringToHeap(text, UnixEncoding.Instance);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007A28 File Offset: 0x00005C28
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return UnixMarshal.PtrToString(pNativeData, UnixEncoding.Instance);
		}

		// Token: 0x04000099 RID: 153
		private static FileNameMarshaler Instance = new FileNameMarshaler();
	}
}
