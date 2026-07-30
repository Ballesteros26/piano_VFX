using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200065A RID: 1626
	internal sealed class LinuxNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060033B0 RID: 13232 RVA: 0x000C0F63 File Offset: 0x000BF163
		// (remove) Token: 0x060033B1 RID: 13233 RVA: 0x000C0F6C File Offset: 0x000BF16C
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060033B2 RID: 13234 RVA: 0x000C0F75 File Offset: 0x000BF175
		// (remove) Token: 0x060033B3 RID: 13235 RVA: 0x000C0F7E File Offset: 0x000BF17E
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000C0F87 File Offset: 0x000BF187
		public bool HasRegisteredEvents
		{
			get
			{
				return this.AddressChanged != null || this.AvailabilityChanged != null;
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Dispose()
		{
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000C0F9C File Offset: 0x000BF19C
		private bool EnsureSocket()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.nl_sock != null)
				{
					return true;
				}
				IntPtr intPtr = LinuxNetworkChange.CreateNLSocket();
				if (intPtr.ToInt64() == -1L)
				{
					return false;
				}
				SafeSocketHandle safeSocketHandle = new SafeSocketHandle(intPtr, true);
				this.nl_sock = new Socket(AddressFamily.Unspecified, SocketType.Raw, ProtocolType.Udp, safeSocketHandle);
				this.nl_args = new SocketAsyncEventArgs();
				this.nl_args.SetBuffer(new byte[8192], 0, 8192);
				this.nl_args.Completed += this.OnDataAvailable;
				this.nl_sock.ReceiveAsync(this.nl_args);
			}
			return true;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000C1068 File Offset: 0x000BF268
		private void MaybeCloseSocket()
		{
			if (this.nl_sock == null || this.AvailabilityChanged != null || this.AddressChanged != null)
			{
				return;
			}
			LinuxNetworkChange.CloseNLSocket(this.nl_sock.Handle);
			GC.SuppressFinalize(this.nl_sock);
			this.nl_sock = null;
			this.nl_args = null;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000C10B8 File Offset: 0x000BF2B8
		private bool GetAvailability()
		{
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkInterface.OperationalStatus == OperationalStatus.Up)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000C10F4 File Offset: 0x000BF2F4
		private void OnAvailabilityChanged(object unused)
		{
			NetworkAvailabilityChangedEventHandler availabilityChanged = this.AvailabilityChanged;
			if (availabilityChanged != null)
			{
				availabilityChanged(null, new NetworkAvailabilityEventArgs(this.GetAvailability()));
			}
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000C1120 File Offset: 0x000BF320
		private void OnAddressChanged(object unused)
		{
			NetworkAddressChangedEventHandler addressChanged = this.AddressChanged;
			if (addressChanged != null)
			{
				addressChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000C1144 File Offset: 0x000BF344
		private void OnEventDue(object unused)
		{
			object @lock = this._lock;
			LinuxNetworkChange.EventType eventType;
			lock (@lock)
			{
				eventType = this.pending_events;
				this.pending_events = (LinuxNetworkChange.EventType)0;
				this.timer.Change(-1, -1);
			}
			if ((eventType & LinuxNetworkChange.EventType.Availability) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAvailabilityChanged));
			}
			if ((eventType & LinuxNetworkChange.EventType.Address) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAddressChanged));
			}
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000C11C8 File Offset: 0x000BF3C8
		private void QueueEvent(LinuxNetworkChange.EventType type)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.timer == null)
				{
					this.timer = new Timer(new TimerCallback(this.OnEventDue));
				}
				if (this.pending_events == (LinuxNetworkChange.EventType)0)
				{
					this.timer.Change(150, -1);
				}
				this.pending_events |= type;
			}
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000C124C File Offset: 0x000BF44C
		private unsafe void OnDataAvailable(object sender, SocketAsyncEventArgs args)
		{
			if (this.nl_sock == null)
			{
				return;
			}
			byte[] array;
			byte* ptr;
			if ((array = args.Buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			LinuxNetworkChange.EventType eventType = LinuxNetworkChange.ReadEvents(this.nl_sock.Handle, new IntPtr((void*)ptr), args.BytesTransferred, 8192);
			array = null;
			this.nl_sock.ReceiveAsync(this.nl_args);
			if (eventType != (LinuxNetworkChange.EventType)0)
			{
				this.QueueEvent(eventType);
			}
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000C12BF File Offset: 0x000BF4BF
		private void Register(NetworkAddressChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Combine(this.AddressChanged, d);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000C12DF File Offset: 0x000BF4DF
		private void Register(NetworkAvailabilityChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Combine(this.AvailabilityChanged, d);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000C1300 File Offset: 0x000BF500
		private void Unregister(NetworkAddressChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Remove(this.AddressChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000C1358 File Offset: 0x000BF558
		private void Unregister(NetworkAvailabilityChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Remove(this.AvailabilityChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x060033C2 RID: 13250
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateNLSocket();

		// Token: 0x060033C3 RID: 13251
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern LinuxNetworkChange.EventType ReadEvents(IntPtr sock, IntPtr buffer, int count, int size);

		// Token: 0x060033C4 RID: 13252
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CloseNLSocket(IntPtr sock);

		// Token: 0x0400292C RID: 10540
		private object _lock = new object();

		// Token: 0x0400292D RID: 10541
		private Socket nl_sock;

		// Token: 0x0400292E RID: 10542
		private SocketAsyncEventArgs nl_args;

		// Token: 0x0400292F RID: 10543
		private LinuxNetworkChange.EventType pending_events;

		// Token: 0x04002930 RID: 10544
		private Timer timer;

		// Token: 0x04002931 RID: 10545
		private NetworkAddressChangedEventHandler AddressChanged;

		// Token: 0x04002932 RID: 10546
		private NetworkAvailabilityChangedEventHandler AvailabilityChanged;

		// Token: 0x04002933 RID: 10547
		private const string LIBNAME = "MonoPosixHelper";

		// Token: 0x0200065B RID: 1627
		[Flags]
		private enum EventType
		{
			// Token: 0x04002935 RID: 10549
			Availability = 1,
			// Token: 0x04002936 RID: 10550
			Address = 2
		}
	}
}
