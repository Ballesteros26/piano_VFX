using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public class AndroidJavaProxy
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002168 File Offset: 0x00000368
		public AndroidJavaProxy(string javaInterface)
			: this(new AndroidJavaClass(javaInterface))
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002178 File Offset: 0x00000378
		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002194 File Offset: 0x00000394
		~AndroidJavaProxy()
		{
			AndroidJNISafe.DeleteWeakGlobalRef(this.proxyObject);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception ex = null;
			BindingFlags bindingFlags = 60;
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				array[i] = ((args[i] == null) ? typeof(AndroidJavaObject) : args[i].GetType());
			}
			try
			{
				MethodInfo method = base.GetType().GetMethod(methodName, bindingFlags, null, array, null);
				bool flag = method != null;
				if (flag)
				{
					return _AndroidJNIHelper.Box(method.Invoke(this, args));
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			string[] array2 = new string[args.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = array[j].ToString();
			}
			bool flag2 = ex != null;
			if (flag2)
			{
				throw new TargetInvocationException(string.Concat(new object[]
				{
					base.GetType(),
					".",
					methodName,
					"(",
					string.Join(",", array2),
					")"
				}), ex);
			}
			AndroidReflection.SetNativeExceptionOnProxy(this.GetRawProxy(), new Exception(string.Concat(new object[]
			{
				"No such proxy method: ",
				base.GetType(),
				".",
				methodName,
				"(",
				string.Join(",", array2),
				")"
			})), true);
			return null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002368 File Offset: 0x00000568
		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] array = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
				bool flag = !(array[i] is AndroidJavaObject);
				if (flag)
				{
					bool flag2 = javaArgs[i] != null;
					if (flag2)
					{
						javaArgs[i].Dispose();
					}
				}
			}
			return this.Invoke(methodName, array);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023D8 File Offset: 0x000005D8
		public virtual bool equals(AndroidJavaObject obj)
		{
			IntPtr intPtr = ((obj == null) ? IntPtr.Zero : obj.GetRawObject());
			return AndroidJNI.IsSameObject(this.proxyObject, intPtr);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002408 File Offset: 0x00000608
		public virtual int hashCode()
		{
			jvalue[] array = new jvalue[1];
			array[0].l = this.GetRawProxy();
			return AndroidJNISafe.CallStaticIntMethod(AndroidJavaProxy.s_JavaLangSystemClass, AndroidJavaProxy.s_HashCodeMethodID, array);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002448 File Offset: 0x00000648
		public virtual string toString()
		{
			return this + " <c# proxy java object>";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002468 File Offset: 0x00000668
		internal AndroidJavaObject GetProxyObject()
		{
			return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(this.GetRawProxy());
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002488 File Offset: 0x00000688
		internal IntPtr GetRawProxy()
		{
			IntPtr intPtr = IntPtr.Zero;
			bool flag = this.proxyObject != IntPtr.Zero;
			if (flag)
			{
				intPtr = AndroidJNI.NewLocalRef(this.proxyObject);
				bool flag2 = intPtr == IntPtr.Zero;
				if (flag2)
				{
					AndroidJNI.DeleteWeakGlobalRef(this.proxyObject);
					this.proxyObject = IntPtr.Zero;
				}
			}
			bool flag3 = intPtr == IntPtr.Zero;
			if (flag3)
			{
				intPtr = AndroidJNIHelper.CreateJavaProxy(this);
				this.proxyObject = AndroidJNI.NewWeakGlobalRef(intPtr);
			}
			return intPtr;
		}

		// Token: 0x04000005 RID: 5
		public readonly AndroidJavaClass javaInterface;

		// Token: 0x04000006 RID: 6
		internal IntPtr proxyObject = IntPtr.Zero;

		// Token: 0x04000007 RID: 7
		private static readonly GlobalJavaObjectRef s_JavaLangSystemClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("java/lang/System"));

		// Token: 0x04000008 RID: 8
		private static readonly IntPtr s_HashCodeMethodID = AndroidJNIHelper.GetMethodID(AndroidJavaProxy.s_JavaLangSystemClass, "identityHashCode", "(Ljava/lang/Object;)I", true);
	}
}
