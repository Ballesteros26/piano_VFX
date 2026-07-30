using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Timers;

namespace Microsoft.Win32
{
	/// <summary>Provides access to system event notifications. This class cannot be inherited.</summary>
	// Token: 0x020000D7 RID: 215
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class SystemEvents
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x000020EB File Offset: 0x000002EB
		private SystemEvents()
		{
		}

		/// <summary>Creates a new window timer associated with the system events window.</summary>
		/// <returns>The ID of the new timer.</returns>
		/// <param name="interval">Specifies the interval between timer notifications, in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed, or the attempt to create the timer did not succeed.</exception>
		// Token: 0x060004BC RID: 1212 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public static IntPtr CreateTimer(int interval)
		{
			int hashCode = Guid.NewGuid().GetHashCode();
			Timer timer = new Timer((double)interval);
			timer.Elapsed += SystemEvents.InternalTimerElapsed;
			SystemEvents.TimerStore.Add(hashCode, timer);
			return new IntPtr(hashCode);
		}

		/// <summary>Terminates the timer specified by the given id.</summary>
		/// <param name="timerId">The ID of the timer to terminate. </param>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed, or the attempt to terminate the timer did not succeed. </exception>
		// Token: 0x060004BD RID: 1213 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		public static void KillTimer(IntPtr timerId)
		{
			Timer timer = (Timer)SystemEvents.TimerStore[timerId.GetHashCode()];
			timer.Stop();
			timer.Elapsed -= SystemEvents.InternalTimerElapsed;
			timer.Dispose();
			SystemEvents.TimerStore.Remove(timerId.GetHashCode());
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		private static void InternalTimerElapsed(object e, ElapsedEventArgs args)
		{
			if (SystemEvents.TimerElapsed != null)
			{
				SystemEvents.TimerElapsed(null, new TimerElapsedEventArgs(IntPtr.Zero));
			}
		}

		/// <summary>Invokes the specified delegate using the thread that listens for system events.</summary>
		/// <param name="method">A delegate to invoke using the thread that listens for system events. </param>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public static void InvokeOnEventsThread(Delegate method)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when the user changes the display settings.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060004C0 RID: 1216 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004C1 RID: 1217 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		public static event EventHandler DisplaySettingsChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the display settings are changing.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004C2 RID: 1218 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004C3 RID: 1219 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event EventHandler DisplaySettingsChanging
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs before the thread that listens for system events is terminated.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event EventHandler EventsThreadShutdown
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the user adds fonts to or removes fonts from the system.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004C6 RID: 1222 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004C7 RID: 1223 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event EventHandler InstalledFontsChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the system is running out of available RAM.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004C8 RID: 1224 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004C9 RID: 1225 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		[Browsable(false)]
		[Obsolete("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static event EventHandler LowMemory
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the user switches to an application that uses a different palette.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004CA RID: 1226 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004CB RID: 1227 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event EventHandler PaletteChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the user suspends or resumes the system.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004CC RID: 1228 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004CD RID: 1229 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event PowerModeChangedEventHandler PowerModeChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the user is logging off or shutting down the system.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004CE RID: 1230 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004CF RID: 1231 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event SessionEndedEventHandler SessionEnded
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the user is trying to log off or shut down the system.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004D0 RID: 1232 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004D1 RID: 1233 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event SessionEndingEventHandler SessionEnding
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the currently logged-in user has changed.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004D2 RID: 1234 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004D3 RID: 1235 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event SessionSwitchEventHandler SessionSwitch
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the user changes the time on the system clock.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004D4 RID: 1236 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004D5 RID: 1237 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event EventHandler TimeChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when a windows timer interval has expired.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060004D6 RID: 1238 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		// (remove) Token: 0x060004D7 RID: 1239 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		public static event TimerElapsedEventHandler TimerElapsed;

		/// <summary>Occurs when a user preference has changed.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060004D8 RID: 1240 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004D9 RID: 1241 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event UserPreferenceChangedEventHandler UserPreferenceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Occurs when a user preference is changing.</summary>
		/// <exception cref="T:System.InvalidOperationException">System event notifications are not supported under the current context. Server processes, for example, might not support global system event notifications.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The attempt to create a system events window thread did not succeed.</exception>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060004DA RID: 1242 RVA: 0x000027E8 File Offset: 0x000009E8
		// (remove) Token: 0x060004DB RID: 1243 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO("Currently does nothing on Mono")]
		public static event UserPreferenceChangingEventHandler UserPreferenceChanging
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x04000B9B RID: 2971
		private static Hashtable TimerStore = new Hashtable();
	}
}
