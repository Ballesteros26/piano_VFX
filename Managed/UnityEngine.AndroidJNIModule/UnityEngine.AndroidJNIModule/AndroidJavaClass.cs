using System;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000042B1 File Offset: 0x000024B1
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000042C4 File Offset: 0x000024C4
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(intPtr);
			this.m_jobject = new GlobalJavaObjectRef(IntPtr.Zero);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004310 File Offset: 0x00002510
		internal AndroidJavaClass(IntPtr jclass)
		{
			bool flag = jclass == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = new GlobalJavaObjectRef(jclass);
			this.m_jobject = new GlobalJavaObjectRef(IntPtr.Zero);
		}
	}
}
