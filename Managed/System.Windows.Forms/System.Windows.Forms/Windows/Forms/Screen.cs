using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a display device or multiple display devices on a single system.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002C4 RID: 708
	public class Screen
	{
		// Token: 0x06002EDB RID: 11995 RVA: 0x000B4D90 File Offset: 0x000B2F90
		private Screen()
		{
			this.primary = true;
			this.bounds = SystemInformation.WorkingArea;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000B4DAC File Offset: 0x000B2FAC
		private Screen(bool primary, string name, Rectangle bounds, Rectangle workarea)
		{
			this.primary = primary;
			this.name = name;
			this.bounds = bounds;
			this.workarea = workarea;
			this.bits_per_pixel = 32;
		}

		/// <summary>Gets an array of all displays on the system.</summary>
		/// <returns>An array of type <see cref="T:System.Windows.Forms.Screen" />, containing all displays on the system.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000B4E0C File Offset: 0x000B300C
		public static Screen[] AllScreens
		{
			get
			{
				return Screen.all_screens;
			}
		}

		/// <summary>Gets the primary display.</summary>
		/// <returns>The primary display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000B4E14 File Offset: 0x000B3014
		public static Screen PrimaryScreen
		{
			get
			{
				return Screen.all_screens[0];
			}
		}

		/// <summary>Gets the number of bits of memory, associated with one pixel of data.</summary>
		/// <returns>The number of bits of memory, associated with one pixel of data </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x000B4E20 File Offset: 0x000B3020
		[MonoTODO("Stub, always returns 32")]
		public int BitsPerPixel
		{
			get
			{
				return this.bits_per_pixel;
			}
		}

		/// <summary>Gets the bounds of the display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the bounds of the display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x000B4E28 File Offset: 0x000B3028
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the device name associated with a display.</summary>
		/// <returns>The device name associated with a display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x000B4E30 File Offset: 0x000B3030
		public string DeviceName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a value indicating whether a particular display is the primary device.</summary>
		/// <returns>true if this display is primary; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x000B4E38 File Offset: 0x000B3038
		public bool Primary
		{
			get
			{
				return this.primary;
			}
		}

		/// <summary>Gets the working area of the display. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the working area of the display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000B4E40 File Offset: 0x000B3040
		public Rectangle WorkingArea
		{
			get
			{
				return this.workarea;
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the specified control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified control. In multiple display environments where no display contains the control, the display closest to the specified control is returned.</returns>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EE5 RID: 12005 RVA: 0x000B4E48 File Offset: 0x000B3048
		public static Screen FromControl(Control control)
		{
			return Screen.FromPoint(control.Location);
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the object referred to by the specified handle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the object. In multiple display environments where no display contains any portion of the specified window, the display closest to the object is returned.</returns>
		/// <param name="hwnd">The window handle for which to retrieve the <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EE6 RID: 12006 RVA: 0x000B4E58 File Offset: 0x000B3058
		public static Screen FromHandle(IntPtr hwnd)
		{
			Control control = Control.FromHandle(hwnd);
			if (control != null)
			{
				return Screen.FromPoint(control.Location);
			}
			return Screen.PrimaryScreen;
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the specified point.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the point. In multiple display environments where no display contains the point, the display closest to the specified point is returned.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B4E84 File Offset: 0x000B3084
		public static Screen FromPoint(Point point)
		{
			for (int i = 0; i < Screen.all_screens.Length; i++)
			{
				if (Screen.all_screens[i].Bounds.Contains(point))
				{
					return Screen.all_screens[i];
				}
			}
			return Screen.PrimaryScreen;
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the rectangle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified rectangle. In multiple display environments where no display contains the rectangle, the display closest to the rectangle is returned.</returns>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000B4ED0 File Offset: 0x000B30D0
		public static Screen FromRectangle(Rectangle rect)
		{
			return Screen.FromPoint(new Point(rect.Left, rect.Top));
		}

		/// <summary>Retrieves the bounds of the display that contains the largest portion of the specified control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified control. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the display bounds. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EE9 RID: 12009 RVA: 0x000B4EEC File Offset: 0x000B30EC
		public static Rectangle GetBounds(Control ctl)
		{
			return Screen.FromControl(ctl).Bounds;
		}

		/// <summary>Retrieves the bounds of the display that contains the specified point.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified point. In multiple display environments where no display contains the specified point, the display closest to the point is returned.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates for which to retrieve the display bounds. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EEA RID: 12010 RVA: 0x000B4EFC File Offset: 0x000B30FC
		public static Rectangle GetBounds(Point pt)
		{
			return Screen.FromPoint(pt).Bounds;
		}

		/// <summary>Retrieves the bounds of the display that contains the largest portion of the specified rectangle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified rectangle. In multiple display environments where no monitor contains the specified rectangle, the monitor closest to the rectangle is returned.</returns>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EEB RID: 12011 RVA: 0x000B4F0C File Offset: 0x000B310C
		public static Rectangle GetBounds(Rectangle rect)
		{
			return Screen.FromRectangle(rect).Bounds;
		}

		/// <summary>Retrieves the working area for the display that contains the largest region of the specified control. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the working area. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EEC RID: 12012 RVA: 0x000B4F1C File Offset: 0x000B311C
		public static Rectangle GetWorkingArea(Control ctl)
		{
			return Screen.FromControl(ctl).WorkingArea;
		}

		/// <summary>Retrieves the working area closest to the specified point. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified point, the display closest to the point is returned.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates for which to retrieve the working area. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EED RID: 12013 RVA: 0x000B4F2C File Offset: 0x000B312C
		public static Rectangle GetWorkingArea(Point pt)
		{
			return Screen.FromPoint(pt).WorkingArea;
		}

		/// <summary>Retrieves the working area for the display that contains the largest portion of the specified rectangle. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified rectangle, the display closest to the rectangle is returned.</returns>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the working area. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002EEE RID: 12014 RVA: 0x000B4F3C File Offset: 0x000B313C
		public static Rectangle GetWorkingArea(Rectangle rect)
		{
			return Screen.FromRectangle(rect).WorkingArea;
		}

		/// <summary>Gets or sets a value indicating whether the specified object is equal to this Screen.</summary>
		/// <returns>true if the specified object is equal to this <see cref="T:System.Windows.Forms.Screen" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this <see cref="T:System.Windows.Forms.Screen" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EEF RID: 12015 RVA: 0x000B4F4C File Offset: 0x000B314C
		public override bool Equals(object obj)
		{
			if (obj is Screen)
			{
				Screen screen = (Screen)obj;
				if (this.name.Equals(screen.name) && this.primary == screen.primary && this.bounds.Equals(screen.Bounds) && this.workarea.Equals(screen.workarea))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Computes and retrieves a hash code for an object.</summary>
		/// <returns>A hash code for an object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EF0 RID: 12016 RVA: 0x000B4FCC File Offset: 0x000B31CC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Retrieves a string representing this object.</summary>
		/// <returns>A string representation of the object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002EF1 RID: 12017 RVA: 0x000B4FD4 File Offset: 0x000B31D4
		public override string ToString()
		{
			return string.Concat(new object[] { "Screen[Bounds={", this.Bounds, "} WorkingArea={", this.WorkingArea, "} Primary={", this.Primary, "} DeviceName=", this.DeviceName });
		}

		// Token: 0x04001676 RID: 5750
		private static Screen[] all_screens = new Screen[]
		{
			new Screen(true, "Mono MWF Primary Display", SystemInformation.VirtualScreen, SystemInformation.WorkingArea)
		};

		// Token: 0x04001677 RID: 5751
		private bool primary;

		// Token: 0x04001678 RID: 5752
		private Rectangle bounds;

		// Token: 0x04001679 RID: 5753
		private Rectangle workarea;

		// Token: 0x0400167A RID: 5754
		private string name;

		// Token: 0x0400167B RID: 5755
		private int bits_per_pixel;
	}
}
