using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000040 RID: 64
	internal class WndProcRouter : IWindowTarget, IDisposable
	{
		// Token: 0x0600021A RID: 538 RVA: 0x000087FC File Offset: 0x000069FC
		public WndProcRouter(Control control, IMessageReceiver receiver)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (receiver == null)
			{
				throw new ArgumentNullException("receiver");
			}
			this._oldTarget = control.WindowTarget;
			this._control = control;
			this._receiver = receiver;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000883A File Offset: 0x00006A3A
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008842 File Offset: 0x00006A42
		public IWindowTarget OldWindowTarget
		{
			get
			{
				return this._oldTarget;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000884A File Offset: 0x00006A4A
		public void ToControl(ref Message m)
		{
			if (this._oldTarget != null)
			{
				this._oldTarget.OnMessage(ref m);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008860 File Offset: 0x00006A60
		public void ToSystem(ref Message m)
		{
			Native.DefWndProc(ref m);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008868 File Offset: 0x00006A68
		void IWindowTarget.OnHandleChange(IntPtr newHandle)
		{
			if (this._oldTarget != null)
			{
				this._oldTarget.OnHandleChange(newHandle);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000887E File Offset: 0x00006A7E
		void IWindowTarget.OnMessage(ref Message m)
		{
			if (this._receiver != null)
			{
				this._receiver.WndProc(ref m);
				return;
			}
			this.ToControl(ref m);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000889C File Offset: 0x00006A9C
		public void Dispose()
		{
			if (this._control != null)
			{
				this._control.WindowTarget = this._oldTarget;
			}
			this._control = null;
			this._oldTarget = null;
		}

		// Token: 0x040000F3 RID: 243
		private IWindowTarget _oldTarget;

		// Token: 0x040000F4 RID: 244
		private IMessageReceiver _receiver;

		// Token: 0x040000F5 RID: 245
		private Control _control;
	}
}
