using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace Mono.Interop
{
	// Token: 0x0200002C RID: 44
	[StructLayout(LayoutKind.Sequential)]
	internal class ComInteropProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddProxy(IntPtr pItf, ComInteropProxy proxy);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ComInteropProxy FindProxy(IntPtr pItf);

		// Token: 0x060000F1 RID: 241 RVA: 0x00004D78 File Offset: 0x00002F78
		private ComInteropProxy(Type t)
			: base(t)
		{
			this.com_object = __ComObject.CreateRCW(t);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004D94 File Offset: 0x00002F94
		private void CacheProxy()
		{
			if (ComInteropProxy.FindProxy(this.com_object.IUnknown) == null)
			{
				ComInteropProxy.AddProxy(this.com_object.IUnknown, this);
				return;
			}
			Interlocked.Increment(ref this.ref_count);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004DC6 File Offset: 0x00002FC6
		private ComInteropProxy(IntPtr pUnk)
			: this(pUnk, typeof(__ComObject))
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004DD9 File Offset: 0x00002FD9
		internal ComInteropProxy(IntPtr pUnk, Type t)
			: base(t)
		{
			this.com_object = new __ComObject(pUnk, this);
			this.CacheProxy();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004DFC File Offset: 0x00002FFC
		internal static ComInteropProxy GetProxy(IntPtr pItf, Type t)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out intPtr));
			ComInteropProxy comInteropProxy = ComInteropProxy.FindProxy(intPtr);
			if (comInteropProxy == null)
			{
				Marshal.Release(intPtr);
				return new ComInteropProxy(intPtr);
			}
			Marshal.Release(intPtr);
			Interlocked.Increment(ref comInteropProxy.ref_count);
			return comInteropProxy;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004E4C File Offset: 0x0000304C
		internal static ComInteropProxy CreateProxy(Type t)
		{
			IntPtr intPtr = __ComObject.CreateIUnknown(t);
			ComInteropProxy comInteropProxy = ComInteropProxy.FindProxy(intPtr);
			ComInteropProxy comInteropProxy2;
			if (comInteropProxy != null)
			{
				Type type = comInteropProxy.com_object.GetType();
				if (type != t)
				{
					throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type '{1}'.", type, t));
				}
				comInteropProxy2 = comInteropProxy;
				Marshal.Release(intPtr);
			}
			else
			{
				comInteropProxy2 = new ComInteropProxy(t);
				comInteropProxy2.com_object.Initialize(intPtr, comInteropProxy2);
			}
			return comInteropProxy2;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004EB1 File Offset: 0x000030B1
		public override IMessage Invoke(IMessage msg)
		{
			Console.WriteLine("Invoke");
			Console.WriteLine(Environment.StackTrace);
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004ED1 File Offset: 0x000030D1
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004ED9 File Offset: 0x000030D9
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004EE4 File Offset: 0x000030E4
		public bool CanCastTo(Type fromType, object o)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new NotSupportedException("Only RCWs are currently supported");
			}
			return (fromType.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic && !(_ComObject.GetInterface(fromType, false) == IntPtr.Zero);
		}

		// Token: 0x040003CA RID: 970
		private __ComObject com_object;

		// Token: 0x040003CB RID: 971
		private int ref_count = 1;

		// Token: 0x040003CC RID: 972
		private string type_name;
	}
}
