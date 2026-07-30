using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	[NativeConditional("PLATFORM_ANDROID")]
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[UsedByNativeCode]
	public static class AndroidJNIHelper
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		public static extern bool debug
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000601C File Offset: 0x0000421C
		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			return AndroidJNIHelper.GetConstructorID(javaClass, "");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000603C File Offset: 0x0000423C
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006058 File Offset: 0x00004258
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, "", false);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006078 File Offset: 0x00004278
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, false);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006094 File Offset: 0x00004294
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000060B0 File Offset: 0x000042B0
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, "", false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000060D0 File Offset: 0x000042D0
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature)
		{
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000060EC File Offset: 0x000042EC
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006108 File Offset: 0x00004308
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006120 File Offset: 0x00004320
		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			GCHandle gchandle = GCHandle.Alloc(proxy);
			IntPtr intPtr;
			try
			{
				intPtr = _AndroidJNIHelper.CreateJavaProxy(GCHandle.ToIntPtr(gchandle), proxy);
			}
			catch
			{
				gchandle.Free();
				throw;
			}
			return intPtr;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006164 File Offset: 0x00004364
		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000617C File Offset: 0x0000437C
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return _AndroidJNIHelper.CreateJNIArgArray(args);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006194 File Offset: 0x00004394
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000061A0 File Offset: 0x000043A0
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000061BC File Offset: 0x000043BC
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000061D8 File Offset: 0x000043D8
		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000061F0 File Offset: 0x000043F0
		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006208 File Offset: 0x00004408
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006220 File Offset: 0x00004420
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000623C File Offset: 0x0000443C
		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006258 File Offset: 0x00004458
		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}
	}
}
