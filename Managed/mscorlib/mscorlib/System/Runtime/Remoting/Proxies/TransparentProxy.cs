using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Mono;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x0200076E RID: 1902
	[StructLayout(LayoutKind.Sequential)]
	internal class TransparentProxy
	{
		// Token: 0x06004E72 RID: 20082 RVA: 0x0011B570 File Offset: 0x00119770
		internal RuntimeType GetProxyType()
		{
			return (RuntimeType)Type.GetTypeFromHandle(this._class.ProxyClass.GetTypeHandle());
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x0011B59A File Offset: 0x0011979A
		private bool IsContextBoundObject
		{
			get
			{
				return this.GetProxyType().IsContextful;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x0011B5A7 File Offset: 0x001197A7
		private Context TargetContext
		{
			get
			{
				return this._rp._targetContext;
			}
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0011B5B4 File Offset: 0x001197B4
		private bool InCurrentContext()
		{
			return this.IsContextBoundObject && this.TargetContext == Thread.CurrentContext;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x0011B5D0 File Offset: 0x001197D0
		internal object LoadRemoteFieldNew(IntPtr classPtr, IntPtr fieldPtr)
		{
			RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(classPtr);
			RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(fieldPtr);
			RuntimeTypeHandle typeHandle = runtimeClassHandle.GetTypeHandle();
			FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(runtimeFieldHandle);
			if (this.InCurrentContext())
			{
				object server = this._rp._server;
				return fieldFromHandle.GetValue(server);
			}
			string fullName = Type.GetTypeFromHandle(typeHandle).FullName;
			string name = fieldFromHandle.Name;
			object[] array = new object[] { fullName, name };
			object[] array2 = new object[1];
			MethodInfo method = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new MissingMethodException("System.Object", "FieldGetter");
			}
			MonoMethodMessage monoMethodMessage = new MonoMethodMessage(method, array, array2);
			Exception ex;
			object[] array3;
			RealProxy.PrivateInvoke(this._rp, monoMethodMessage, out ex, out array3);
			if (ex != null)
			{
				throw ex;
			}
			return array3[0];
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x0011B69C File Offset: 0x0011989C
		internal void StoreRemoteField(IntPtr classPtr, IntPtr fieldPtr, object arg)
		{
			RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(classPtr);
			RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(fieldPtr);
			RuntimeTypeHandle typeHandle = runtimeClassHandle.GetTypeHandle();
			FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(runtimeFieldHandle);
			if (this.InCurrentContext())
			{
				object server = this._rp._server;
				fieldFromHandle.SetValue(server, arg);
				return;
			}
			string fullName = Type.GetTypeFromHandle(typeHandle).FullName;
			string name = fieldFromHandle.Name;
			object[] array = new object[] { fullName, name, arg };
			MethodInfo method = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new MissingMethodException("System.Object", "FieldSetter");
			}
			MonoMethodMessage monoMethodMessage = new MonoMethodMessage(method, array, null);
			Exception ex;
			object[] array2;
			RealProxy.PrivateInvoke(this._rp, monoMethodMessage, out ex, out array2);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x040029ED RID: 10733
		public RealProxy _rp;

		// Token: 0x040029EE RID: 10734
		private RuntimeRemoteClassHandle _class;

		// Token: 0x040029EF RID: 10735
		private bool _custom_type_info;
	}
}
