using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x0200033D RID: 829
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button. </param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu. </param>
		// Token: 0x06001A0C RID: 6668 RVA: 0x0006A3A2 File Offset: 0x000685A2
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this.execHandler = handler;
			this.commandID = command;
			this.status = 3;
		}

		/// <summary>Gets or sets a value indicating whether this menu item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false.</returns>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0006A3BF File Offset: 0x000685BF
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0006A3CC File Offset: 0x000685CC
		public virtual bool Checked
		{
			get
			{
				return (this.status & 4) != 0;
			}
			set
			{
				this.SetStatus(4, value);
			}
		}

		/// <summary>Gets a value indicating whether this menu item is available.</summary>
		/// <returns>true if the item is enabled; otherwise, false.</returns>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0006A3D6 File Offset: 0x000685D6
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x0006A3E3 File Offset: 0x000685E3
		public virtual bool Enabled
		{
			get
			{
				return (this.status & 2) != 0;
			}
			set
			{
				this.SetStatus(2, value);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0006A3F0 File Offset: 0x000685F0
		private void SetStatus(int mask, bool value)
		{
			int num = this.status;
			if (value)
			{
				num |= mask;
			}
			else
			{
				num &= ~mask;
			}
			if (num != this.status)
			{
				this.status = num;
				this.OnCommandChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />. </returns>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0006A42D File Offset: 0x0006862D
		public virtual IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new HybridDictionary();
				}
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is supported.</summary>
		/// <returns>true if the item is supported, which is the default; otherwise, false.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0006A448 File Offset: 0x00068648
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x0006A455 File Offset: 0x00068655
		public virtual bool Supported
		{
			get
			{
				return (this.status & 1) != 0;
			}
			set
			{
				this.SetStatus(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is visible.</summary>
		/// <returns>true if the item is visible; otherwise, false.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0006A45F File Offset: 0x0006865F
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0006A46D File Offset: 0x0006866D
		public virtual bool Visible
		{
			get
			{
				return (this.status & 16) == 0;
			}
			set
			{
				this.SetStatus(16, !value);
			}
		}

		/// <summary>Occurs when the menu command changes.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001A17 RID: 6679 RVA: 0x0006A47B File Offset: 0x0006867B
		// (remove) Token: 0x06001A18 RID: 6680 RVA: 0x0006A494 File Offset: 0x00068694
		public event EventHandler CommandChanged
		{
			add
			{
				this.statusHandler = (EventHandler)Delegate.Combine(this.statusHandler, value);
			}
			remove
			{
				this.statusHandler = (EventHandler)Delegate.Remove(this.statusHandler, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0006A4AD File Offset: 0x000686AD
		public virtual CommandID CommandID
		{
			get
			{
				return this.commandID;
			}
		}

		/// <summary>Invokes the command.</summary>
		// Token: 0x06001A1A RID: 6682 RVA: 0x0006A4B8 File Offset: 0x000686B8
		public virtual void Invoke()
		{
			if (this.execHandler != null)
			{
				try
				{
					this.execHandler(this, EventArgs.Empty);
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Invokes the command with the given parameter.</summary>
		/// <param name="arg">An optional argument for use by the command.</param>
		// Token: 0x06001A1B RID: 6683 RVA: 0x0006A4FC File Offset: 0x000686FC
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		/// <summary>Gets the OLE command status code for this menu item.</summary>
		/// <returns>An integer containing a mixture of status flags that reflect the state of this menu item.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0006A504 File Offset: 0x00068704
		public virtual int OleStatus
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommand.CommandChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A1D RID: 6685 RVA: 0x0006A50C File Offset: 0x0006870C
		protected virtual void OnCommandChanged(EventArgs e)
		{
			if (this.statusHandler != null)
			{
				this.statusHandler(this, e);
			}
		}

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x06001A1E RID: 6686 RVA: 0x0006A524 File Offset: 0x00068724
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this.status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this.status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this.status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this.status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x0400147D RID: 5245
		private EventHandler execHandler;

		// Token: 0x0400147E RID: 5246
		private EventHandler statusHandler;

		// Token: 0x0400147F RID: 5247
		private CommandID commandID;

		// Token: 0x04001480 RID: 5248
		private int status;

		// Token: 0x04001481 RID: 5249
		private IDictionary properties;

		// Token: 0x04001482 RID: 5250
		private const int ENABLED = 2;

		// Token: 0x04001483 RID: 5251
		private const int INVISIBLE = 16;

		// Token: 0x04001484 RID: 5252
		private const int CHECKED = 4;

		// Token: 0x04001485 RID: 5253
		private const int SUPPORTED = 1;
	}
}
