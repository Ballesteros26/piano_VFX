using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Threading;

namespace System.Timers
{
	/// <summary>Generates recurring events in an application.</summary>
	// Token: 0x02000130 RID: 304
	[DefaultEvent("Elapsed")]
	[DefaultProperty("Interval")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Timer : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets all the properties to their initial values.</summary>
		// Token: 0x0600083B RID: 2107 RVA: 0x00028274 File Offset: 0x00026474
		public Timer()
		{
			this.interval = 100.0;
			this.enabled = false;
			this.autoReset = true;
			this.initializing = false;
			this.delayedEnable = false;
			this.callback = new TimerCallback(this.MyTimerCallback);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets the <see cref="P:System.Timers.Timer.Interval" /> property to the specified number of milliseconds.</summary>
		/// <param name="interval">The time, in milliseconds, between events. The value must be greater than zero and less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="interval" /> parameter is less than or equal to zero, or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x0600083C RID: 2108 RVA: 0x000282C4 File Offset: 0x000264C4
		public Timer(double interval)
			: this()
		{
			if (interval <= 0.0)
			{
				throw new ArgumentException(global::SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[] { "interval", interval }));
			}
			this.interval = (double)Timer.CalculateRoundedInterval(interval, true);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the specified interval elapses or only after the first time it elapses.</summary>
		/// <returns>true if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the interval elapses; false if it should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once, after the first time the interval elapses. The default is true.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00028318 File Offset: 0x00026518
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00028320 File Offset: 0x00026520
		[Category("Behavior")]
		[TimersDescription("Indicates whether the timer will be restarted when it is enabled.")]
		[DefaultValue(true)]
		public bool AutoReset
		{
			get
			{
				return this.autoReset;
			}
			set
			{
				if (base.DesignMode)
				{
					this.autoReset = value;
					return;
				}
				if (this.autoReset != value)
				{
					this.autoReset = value;
					if (this.timer != null)
					{
						this.UpdateTimer();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>true if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This property cannot be set because the timer has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Timers.Timer.Interval" /> property was set to a value greater than <see cref="F:System.Int32.MaxValue" /> before the timer was enabled. </exception>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00028350 File Offset: 0x00026550
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00028358 File Offset: 0x00026558
		[DefaultValue(false)]
		[TimersDescription("Indicates whether the timer is enabled to fire events at a defined interval.")]
		[Category("Behavior")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (base.DesignMode)
				{
					this.delayedEnable = value;
					this.enabled = value;
					return;
				}
				if (this.initializing)
				{
					this.delayedEnable = value;
					return;
				}
				if (this.enabled != value)
				{
					if (!value)
					{
						if (this.timer != null)
						{
							this.cookie = null;
							this.timer.Dispose();
							this.timer = null;
						}
						this.enabled = value;
						return;
					}
					this.enabled = value;
					if (this.timer == null)
					{
						if (this.disposed)
						{
							throw new ObjectDisposedException(base.GetType().Name);
						}
						int num = Timer.CalculateRoundedInterval(this.interval, false);
						this.cookie = new object();
						this.timer = new Timer(this.callback, this.cookie, num, this.autoReset ? num : (-1));
						return;
					}
					else
					{
						this.UpdateTimer();
					}
				}
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00028430 File Offset: 0x00026630
		private static int CalculateRoundedInterval(double interval, bool argumentCheck = false)
		{
			double num = Math.Ceiling(interval);
			if (num <= 2147483647.0 && num > 0.0)
			{
				return (int)num;
			}
			if (argumentCheck)
			{
				throw new ArgumentException(global::SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[] { "interval", interval }));
			}
			throw new ArgumentOutOfRangeException(global::SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[] { "interval", interval }));
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000284B0 File Offset: 0x000266B0
		private void UpdateTimer()
		{
			int num = Timer.CalculateRoundedInterval(this.interval, false);
			this.timer.Change(num, this.autoReset ? num : (-1));
		}

		/// <summary>Gets or sets the interval at which to raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>The time, in milliseconds, between <see cref="E:System.Timers.Timer.Elapsed" /> events. The value must be greater than zero, and less than or equal to <see cref="F:System.Int32.MaxValue" />. The default is 100 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero.-or-The interval is greater than <see cref="F:System.Int32.MaxValue" />, and the timer is currently enabled. (If the timer is not currently enabled, no exception is thrown until it becomes enabled.) </exception>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000284E3 File Offset: 0x000266E3
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x000284EC File Offset: 0x000266EC
		[TimersDescription("The number of milliseconds between timer events.")]
		[DefaultValue(100.0)]
		[Category("Behavior")]
		[SettingsBindable(true)]
		public double Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException(global::SR.GetString("'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}.", new object[] { value, 0 }));
				}
				this.interval = value;
				if (this.timer != null)
				{
					this.UpdateTimer();
				}
			}
		}

		/// <summary>Occurs when the interval elapses.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000845 RID: 2117 RVA: 0x00028542 File Offset: 0x00026742
		// (remove) Token: 0x06000846 RID: 2118 RVA: 0x0002855B File Offset: 0x0002675B
		[TimersDescription("Occurs when the Interval has elapsed.")]
		[Category("Behavior")]
		public event ElapsedEventHandler Elapsed
		{
			add
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Combine(this.onIntervalElapsed, value);
			}
			remove
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Remove(this.onIntervalElapsed, value);
			}
		}

		/// <summary>Gets or sets the site that binds the <see cref="T:System.Timers.Timer" /> to its container in design mode.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> interface representing the site that binds the <see cref="T:System.Timers.Timer" /> object to its container.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0002858C File Offset: 0x0002678C
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x00028574 File Offset: 0x00026774
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (base.DesignMode)
				{
					this.enabled = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal event-handler calls that are issued when an interval has elapsed.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> representing the object used to marshal the event-handler calls that are issued when an interval has elapsed. The default is null.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00028594 File Offset: 0x00026794
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x000285EE File Offset: 0x000267EE
		[Browsable(false)]
		[TimersDescription("The object used to marshal the event handler calls issued when an interval has elapsed.")]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x0600084B RID: 2123 RVA: 0x000285F7 File Offset: 0x000267F7
		public void BeginInit()
		{
			this.Close();
			this.initializing = true;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Timers.Timer" />.</summary>
		// Token: 0x0600084C RID: 2124 RVA: 0x00028606 File Offset: 0x00026806
		public void Close()
		{
			this.initializing = false;
			this.delayedEnable = false;
			this.enabled = false;
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Timers.Timer" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600084D RID: 2125 RVA: 0x00028637 File Offset: 0x00026837
		protected override void Dispose(bool disposing)
		{
			this.Close();
			this.disposed = true;
			base.Dispose(disposing);
		}

		/// <summary>Ends the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600084E RID: 2126 RVA: 0x0002864D File Offset: 0x0002684D
		public void EndInit()
		{
			this.initializing = false;
			this.Enabled = this.delayedEnable;
		}

		/// <summary>Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to true.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Timers.Timer" /> is created with an interval equal to or greater than <see cref="F:System.Int32.MaxValue" /> + 1, or set to an interval less than zero.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600084F RID: 2127 RVA: 0x00028662 File Offset: 0x00026862
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to false.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000850 RID: 2128 RVA: 0x0002866B File Offset: 0x0002686B
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00028674 File Offset: 0x00026874
		private void MyTimerCallback(object state)
		{
			if (state != this.cookie)
			{
				return;
			}
			if (!this.autoReset)
			{
				this.enabled = false;
			}
			ElapsedEventArgs elapsedEventArgs = new ElapsedEventArgs(DateTime.Now);
			try
			{
				ElapsedEventHandler elapsedEventHandler = this.onIntervalElapsed;
				if (elapsedEventHandler != null)
				{
					if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
					{
						this.SynchronizingObject.BeginInvoke(elapsedEventHandler, new object[] { this, elapsedEventArgs });
					}
					else
					{
						elapsedEventHandler(this, elapsedEventArgs);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000D9D RID: 3485
		private double interval;

		// Token: 0x04000D9E RID: 3486
		private bool enabled;

		// Token: 0x04000D9F RID: 3487
		private bool initializing;

		// Token: 0x04000DA0 RID: 3488
		private bool delayedEnable;

		// Token: 0x04000DA1 RID: 3489
		private ElapsedEventHandler onIntervalElapsed;

		// Token: 0x04000DA2 RID: 3490
		private bool autoReset;

		// Token: 0x04000DA3 RID: 3491
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000DA4 RID: 3492
		private bool disposed;

		// Token: 0x04000DA5 RID: 3493
		private Timer timer;

		// Token: 0x04000DA6 RID: 3494
		private TimerCallback callback;

		// Token: 0x04000DA7 RID: 3495
		private object cookie;
	}
}
