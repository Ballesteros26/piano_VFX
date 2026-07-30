using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a <see cref="T:System.Windows.Forms.TextBox" /> control that is hosted in a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CD RID: 205
	[DefaultProperty("GridEditName")]
	[DesignTimeVisible(false)]
	[ComVisible(true)]
	[ClassInterface(1)]
	[ToolboxItem(false)]
	public class DataGridTextBox : TextBox
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBox" /> class. </summary>
		// Token: 0x06000E03 RID: 3587 RVA: 0x00037DA8 File Offset: 0x00035FA8
		public DataGridTextBox()
		{
			this.isnavigating = true;
			this.grid = null;
			this.accepts_tab = true;
			this.accepts_return = false;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
			base.SetStyle(ControlStyles.FixedHeight, true);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.DataGridTextBox" /> is in a mode that allows either editing or navigating.</summary>
		/// <returns>true if the controls is in navigation mode, and editing has not begun; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00037DE4 File Offset: 0x00035FE4
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x00037DEC File Offset: 0x00035FEC
		public bool IsInEditOrNavigateMode
		{
			get
			{
				return this.isnavigating;
			}
			set
			{
				this.isnavigating = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x06000E06 RID: 3590 RVA: 0x00037DF8 File Offset: 0x00035FF8
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (!base.ReadOnly)
			{
				this.isnavigating = false;
				this.grid.ColumnStartedEditing(this.Bounds);
			}
			base.OnKeyPress(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000E07 RID: 3591 RVA: 0x00037E30 File Offset: 0x00036030
		protected override void OnMouseWheel(MouseEventArgs e)
		{
		}

		/// <summary>Indicates whether the key pressed is processed further (for example, to navigate, or escape). This property is read-only.</summary>
		/// <returns>true if the key is consumed, false to if the key is further processed.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that contains the key data. </param>
		// Token: 0x06000E08 RID: 3592 RVA: 0x00037E34 File Offset: 0x00036034
		protected internal override bool ProcessKeyMessage(ref Message m)
		{
			Keys keys = (Keys)m.WParam.ToInt32();
			if (this.isnavigating && this.ProcessKeyPreview(ref m))
			{
				return true;
			}
			switch (m.Msg)
			{
			case 256:
			{
				Keys keys2 = keys;
				switch (keys2)
				{
				case Keys.Left:
					return base.SelectionStart == 0 && this.ProcessKeyPreview(ref m);
				case Keys.Up:
				case Keys.Down:
					break;
				case Keys.Right:
					return base.SelectionStart + this.SelectionLength >= this.Text.Length && this.ProcessKeyPreview(ref m);
				default:
					if (keys2 != Keys.Tab)
					{
						if (keys2 == Keys.Return)
						{
							return true;
						}
						if (keys2 != Keys.F2)
						{
							return this.ProcessKeyEventArgs(ref m);
						}
						base.SelectionStart = this.Text.Length;
						this.SelectionLength = 0;
						return false;
					}
					break;
				}
				return this.ProcessKeyPreview(ref m);
			}
			case 258:
			{
				Keys keys2 = keys;
				if (keys2 != Keys.Return)
				{
					return this.ProcessKeyEventArgs(ref m);
				}
				this.isnavigating = true;
				return false;
			}
			}
			return false;
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> to which this <see cref="T:System.Windows.Forms.TextBox" /> control belongs.</summary>
		/// <param name="parentGrid">The <see cref="T:System.Windows.Forms.DataGrid" /> control that hosts the control. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E09 RID: 3593 RVA: 0x00037F50 File Offset: 0x00036150
		public void SetDataGrid(DataGrid parentGrid)
		{
			this.grid = parentGrid;
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> event.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that contains the event data. </param>
		// Token: 0x06000E0A RID: 3594 RVA: 0x00037F5C File Offset: 0x0003615C
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
			case 513:
			case 515:
				this.isnavigating = false;
				break;
			}
			base.WndProc(ref m);
		}

		// Token: 0x040009B6 RID: 2486
		private bool isnavigating;

		// Token: 0x040009B7 RID: 2487
		private DataGrid grid;
	}
}
