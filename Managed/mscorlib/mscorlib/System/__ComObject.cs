using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Interop;

namespace System
{
	// Token: 0x0200025D RID: 605
	[StructLayout(LayoutKind.Sequential)]
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x06001BF6 RID: 7158
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern __ComObject CreateRCW(Type t);

		// Token: 0x06001BF7 RID: 7159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseInterfaces();

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000695C0 File Offset: 0x000677C0
		~__ComObject()
		{
			if (this.hash_table != IntPtr.Zero)
			{
				if (this.synchronization_context != null)
				{
					this.synchronization_context.Post(delegate(object state)
					{
						this.ReleaseInterfaces();
					}, this);
				}
				else
				{
					this.ReleaseInterfaces();
				}
			}
			this.proxy = null;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00069628 File Offset: 0x00067828
		public __ComObject()
		{
			this.Initialize(base.GetType());
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0006963C File Offset: 0x0006783C
		internal __ComObject(Type t)
		{
			this.Initialize(t);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0006964C File Offset: 0x0006784C
		internal __ComObject(IntPtr pItf, ComInteropProxy p)
		{
			this.proxy = p;
			this.InitializeApartmentDetails();
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out this.iunknown));
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00069685 File Offset: 0x00067885
		internal void Initialize(IntPtr pUnk, ComInteropProxy p)
		{
			this.proxy = p;
			this.InitializeApartmentDetails();
			this.iunknown = pUnk;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0006969B File Offset: 0x0006789B
		internal void Initialize(Type t)
		{
			this.InitializeApartmentDetails();
			if (this.iunknown != IntPtr.Zero)
			{
				return;
			}
			this.iunknown = __ComObject.CreateIUnknown(t);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000696C4 File Offset: 0x000678C4
		internal static IntPtr CreateIUnknown(Type t)
		{
			RuntimeHelpers.RunClassConstructor(t.TypeHandle);
			ObjectCreationDelegate objectCreationCallback = ExtensibleClassFactory.GetObjectCreationCallback(t);
			IntPtr intPtr;
			if (objectCreationCallback != null)
			{
				intPtr = objectCreationCallback(IntPtr.Zero);
				if (intPtr == IntPtr.Zero)
				{
					throw new COMException(string.Format("ObjectCreationDelegate for type {0} failed to return a valid COM object", t));
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(__ComObject.CoCreateInstance(__ComObject.GetCLSID(t), IntPtr.Zero, 21U, __ComObject.IID_IUnknown, out intPtr));
			}
			return intPtr;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00069730 File Offset: 0x00067930
		private void InitializeApartmentDetails()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				return;
			}
			this.synchronization_context = SynchronizationContext.Current;
			if (this.synchronization_context != null && this.synchronization_context.GetType() == typeof(SynchronizationContext))
			{
				this.synchronization_context = null;
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00069780 File Offset: 0x00067980
		private static Guid GetCLSID(Type t)
		{
			if (t.IsImport)
			{
				return t.GUID;
			}
			Type type = t.BaseType;
			while (type != typeof(object))
			{
				if (type.IsImport)
				{
					return type.GUID;
				}
				type = type.BaseType;
			}
			throw new COMException("Could not find base COM type for type " + t.ToString());
		}

		// Token: 0x06001C01 RID: 7169
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInterfaceInternal(Type t, bool throwException);

		// Token: 0x06001C02 RID: 7170 RVA: 0x000697E2 File Offset: 0x000679E2
		internal IntPtr GetInterface(Type t, bool throwException)
		{
			this.CheckIUnknown();
			return this.GetInterfaceInternal(t, throwException);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000697F2 File Offset: 0x000679F2
		internal IntPtr GetInterface(Type t)
		{
			return this.GetInterface(t, true);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000697FC File Offset: 0x000679FC
		private void CheckIUnknown()
		{
			if (this.iunknown == IntPtr.Zero)
			{
				throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0006981B File Offset: 0x00067A1B
		internal IntPtr IUnknown
		{
			get
			{
				if (this.iunknown == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return this.iunknown;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00069840 File Offset: 0x00067A40
		internal IntPtr IDispatch
		{
			get
			{
				IntPtr @interface = this.GetInterface(typeof(IDispatch));
				if (@interface == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return @interface;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0006986A File Offset: 0x00067A6A
		internal static Guid IID_IUnknown
		{
			get
			{
				return new Guid("00000000-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00069876 File Offset: 0x00067A76
		internal static Guid IID_IDispatch
		{
			get
			{
				return new Guid("00020400-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00069884 File Offset: 0x00067A84
		public override bool Equals(object obj)
		{
			this.CheckIUnknown();
			if (obj == null)
			{
				return false;
			}
			__ComObject _ComObject = obj as __ComObject;
			return _ComObject != null && this.iunknown == _ComObject.IUnknown;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000698B9 File Offset: 0x00067AB9
		public override int GetHashCode()
		{
			this.CheckIUnknown();
			return this.iunknown.ToInt32();
		}

		// Token: 0x06001C0B RID: 7179
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, out IntPtr pUnk);

		// Token: 0x04000FA4 RID: 4004
		private IntPtr iunknown;

		// Token: 0x04000FA5 RID: 4005
		private IntPtr hash_table;

		// Token: 0x04000FA6 RID: 4006
		private SynchronizationContext synchronization_context;

		// Token: 0x04000FA7 RID: 4007
		private ComInteropProxy proxy;
	}
}
