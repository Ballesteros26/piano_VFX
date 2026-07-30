using System;
using System.ComponentModel;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Implements a timer that raises an event at user-defined intervals. This timer is optimized for use in Windows Forms applications and must be used in a window.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000333 RID: 819
	[DefaultProperty("Interval")]
	[DefaultEvent("Tick")]
	[ToolboxItemFilter("System.Windows.Forms", 0)]
	public class Timer : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class.</summary>
		// Token: 0x060038D2 RID: 14546 RVA: 0x000EA4C4 File Offset: 0x000E86C4
		public Timer()
		{
			this.enabled = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class together with the specified container.</summary>
		/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the timer. </param>
		// Token: 0x060038D3 RID: 14547 RVA: 0x000EA4DC File Offset: 0x000E86DC
		public Timer(IContainer container)
			: this()
		{
			container.Add(this);
		}

		/// <summary>Occurs when the specified timer interval has elapsed and the timer is enabled.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000343 RID: 835
		// (add) Token: 0x060038D5 RID: 14549 RVA: 0x000EA4F8 File Offset: 0x000E86F8
		// (remove) Token: 0x060038D6 RID: 14550 RVA: 0x000EA514 File Offset: 0x000E8714
		public event EventHandler Tick;

		/// <summary>Gets or sets whether the timer is running.</summary>
		/// <returns>true if the timer is currently enabled; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000EA530 File Offset: 0x000E8730
		// (set) Token: 0x060038D8 RID: 14552 RVA: 0x000EA538 File Offset: 0x000E8738
		[DefaultValue(false)]
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (value != this.enabled)
				{
					this.enabled = value;
					if (value)
					{
						this.expires = DateTime.UtcNow.AddMilliseconds((double)((this.interval <= Timer.Minimum) ? Timer.Minimum : this.interval));
						this.thread = Thread.CurrentThread;
						XplatUI.SetTimer(this);
					}
					else
					{
						XplatUI.KillTimer(this);
						this.thread = null;
					}
				}
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
		/// <returns>An <see cref="T:System.Int32" /> specifying the number of milliseconds before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event. The value cannot be less than one.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000EA5B8 File Offset: 0x000E87B8
		// (set) Token: 0x060038DA RID: 14554 RVA: 0x000EA5C0 File Offset: 0x000E87C0
		[DefaultValue(100)]
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("Interval", string.Format("'{0}' is not a valid value for Interval. Interval must be greater than 0.", value));
				}
				if (this.interval == value)
				{
					return;
				}
				this.interval = value;
				this.expires = DateTime.UtcNow.AddMilliseconds((double)((this.interval <= Timer.Minimum) ? Timer.Minimum : this.interval));
				if (this.enabled)
				{
					XplatUI.KillTimer(this);
					XplatUI.SetTimer(this);
				}
			}
		}

		/// <summary>Gets or sets an arbitrary string representing some type of user state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000EA650 File Offset: 0x000E8850
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x000EA658 File Offset: 0x000E8858
		[TypeConverter(typeof(StringConverter))]
		[MWFCategory("Data")]
		[Localizable(false)]
		[Bindable(true)]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.control_tag;
			}
			set
			{
				this.control_tag = value;
			}
		}

		/// <summary>Starts the timer.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060038DD RID: 14557 RVA: 0x000EA664 File Offset: 0x000E8864
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops the timer.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060038DE RID: 14558 RVA: 0x000EA670 File Offset: 0x000E8870
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000EA67C File Offset: 0x000E887C
		internal DateTime Expires
		{
			get
			{
				return this.expires;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.Timer" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Timer" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038E0 RID: 14560 RVA: 0x000EA684 File Offset: 0x000E8884
		public override string ToString()
		{
			return base.ToString() + ", Interval: " + this.Interval;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000EA6A4 File Offset: 0x000E88A4
		internal void Update(DateTime update)
		{
			this.expires = update.AddMilliseconds((double)((this.interval <= Timer.Minimum) ? Timer.Minimum : this.interval));
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000EA6E0 File Offset: 0x000E88E0
		internal void FireTick()
		{
			this.OnTick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. This is always <see cref="F:System.EventArgs.Empty" />. </param>
		// Token: 0x060038E3 RID: 14563 RVA: 0x000EA6F0 File Offset: 0x000E88F0
		protected virtual void OnTick(EventArgs e)
		{
			if (this.Tick != null)
			{
				this.Tick.Invoke(this, e);
			}
		}

		/// <summary>Disposes of the resources, other than memory, used by the timer.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources. false to release only the unmanaged resources.</param>
		// Token: 0x060038E4 RID: 14564 RVA: 0x000EA70C File Offset: 0x000E890C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.Enabled = false;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000EA71C File Offset: 0x000E891C
		internal void TickHandler(object sender, EventArgs e)
		{
			this.OnTick(e);
		}

		// Token: 0x040019C8 RID: 6600
		private bool enabled;

		// Token: 0x040019C9 RID: 6601
		private int interval = 100;

		// Token: 0x040019CA RID: 6602
		private DateTime expires;

		// Token: 0x040019CB RID: 6603
		internal Thread thread;

		// Token: 0x040019CC RID: 6604
		internal bool Busy;

		// Token: 0x040019CD RID: 6605
		internal IntPtr window;

		// Token: 0x040019CE RID: 6606
		private object control_tag;

		// Token: 0x040019CF RID: 6607
		internal static readonly int Minimum = 15;
	}
}
