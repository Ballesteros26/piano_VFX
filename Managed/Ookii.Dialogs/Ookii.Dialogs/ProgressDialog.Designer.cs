using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000014 RID: 20
	[DefaultEvent("DoWork")]
	[DefaultProperty("Text")]
	[Description("Represents a dialog that can be used to report progress to the user.")]
	public class ProgressDialog : Component
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000C3 RID: 195 RVA: 0x00004CB4 File Offset: 0x00002EB4
		// (remove) Token: 0x060000C4 RID: 196 RVA: 0x00004CEC File Offset: 0x00002EEC
		[field: DebuggerBrowsable(0)]
		public event DoWorkEventHandler DoWork;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000C5 RID: 197 RVA: 0x00004D24 File Offset: 0x00002F24
		// (remove) Token: 0x060000C6 RID: 198 RVA: 0x00004D5C File Offset: 0x00002F5C
		[field: DebuggerBrowsable(0)]
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000C7 RID: 199 RVA: 0x00004D94 File Offset: 0x00002F94
		// (remove) Token: 0x060000C8 RID: 200 RVA: 0x00004DCC File Offset: 0x00002FCC
		[field: DebuggerBrowsable(0)]
		public event ProgressChangedEventHandler ProgressChanged;

		// Token: 0x060000C9 RID: 201 RVA: 0x00004E01 File Offset: 0x00003001
		public ProgressDialog()
			: this(null)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004E0C File Offset: 0x0000300C
		public ProgressDialog(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
			this.ProgressBarStyle = ProgressBarStyle.ProgressBar;
			this.ShowCancelButton = true;
			this.MinimizeBox = true;
			bool flag2 = !NativeMethods.IsWindowsVistaOrLater;
			if (flag2)
			{
				this.Animation = AnimationResource.GetShellAnimation(ShellAnimation.FlyingPapers);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004E74 File Offset: 0x00003074
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004E95 File Offset: 0x00003095
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text in the progress dialog's title bar.")]
		[DefaultValue("")]
		public string WindowTitle
		{
			get
			{
				return this._windowTitle ?? string.Empty;
			}
			set
			{
				this._windowTitle = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004EA0 File Offset: 0x000030A0
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004EC4 File Offset: 0x000030C4
		[Localizable(true)]
		[Category("Appearance")]
		[Description("A short description of the operation being carried out.")]
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(1U, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004F04 File Offset: 0x00003104
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004F1C File Offset: 0x0000311C
		[Category("Behavior")]
		[Description("Indicates whether path strings in the Text property should be compacted if they are too large to fit on one line.")]
		[DefaultValue(false)]
		public bool UseCompactPathsForText
		{
			get
			{
				return this._useCompactPathsForText;
			}
			set
			{
				this._useCompactPathsForText = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(1U, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004F5C File Offset: 0x0000315C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004F80 File Offset: 0x00003180
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional details about the operation being carried out.")]
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return this._description ?? string.Empty;
			}
			set
			{
				this._description = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(2U, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004FC0 File Offset: 0x000031C0
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004FD8 File Offset: 0x000031D8
		[Category("Behavior")]
		[Description("Indicates whether path strings in the Description property should be compacted if they are too large to fit on one line.")]
		[DefaultValue(false)]
		public bool UseCompactPathsForDescription
		{
			get
			{
				return this._useCompactPathsForDescription;
			}
			set
			{
				this._useCompactPathsForDescription = value;
				bool flag = this._dialog != null;
				if (flag)
				{
					this._dialog.SetLine(2U, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005018 File Offset: 0x00003218
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00005039 File Offset: 0x00003239
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text that will be shown after the Cancel button is pressed.")]
		[DefaultValue("")]
		public string CancellationText
		{
			get
			{
				return this._cancellationText ?? string.Empty;
			}
			set
			{
				this._cancellationText = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005043 File Offset: 0x00003243
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000504B File Offset: 0x0000324B
		[Category("Appearance")]
		[Description("Indicates whether an estimate of the remaining time will be shown.")]
		[DefaultValue(false)]
		public bool ShowTimeRemaining { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005054 File Offset: 0x00003254
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000505C File Offset: 0x0000325C
		[Category("Appearance")]
		[Description("Indicates whether the dialog has a cancel button. Do not set to false unless absolutely necessary.")]
		[DefaultValue(true)]
		public bool ShowCancelButton { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005065 File Offset: 0x00003265
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000506D File Offset: 0x0000326D
		[Category("Window Style")]
		[Description("Indicates whether the progress dialog has a minimize button.")]
		[DefaultValue(true)]
		public bool MinimizeBox { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005078 File Offset: 0x00003278
		[Browsable(false)]
		public bool CancellationPending
		{
			get
			{
				this._backgroundWorker.ReportProgress(-1);
				return this._cancellationPending;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000509D File Offset: 0x0000329D
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000050A5 File Offset: 0x000032A5
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public AnimationResource Animation { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000050AE File Offset: 0x000032AE
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000050B6 File Offset: 0x000032B6
		[Category("Appearance")]
		[Description("Indicates the style of the progress bar.")]
		[DefaultValue(ProgressBarStyle.ProgressBar)]
		public ProgressBarStyle ProgressBarStyle { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000050C0 File Offset: 0x000032C0
		[Browsable(false)]
		public bool IsBusy
		{
			get
			{
				return this._backgroundWorker.IsBusy;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000050DD File Offset: 0x000032DD
		public void Show()
		{
			this.Show(null);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000050E8 File Offset: 0x000032E8
		public void Show(object argument)
		{
			this.RunProgressDialog(IntPtr.Zero, argument);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000050F8 File Offset: 0x000032F8
		public void ShowDialog()
		{
			this.ShowDialog(null, null);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005104 File Offset: 0x00003304
		public void ShowDialog(IWin32Window owner)
		{
			this.ShowDialog(owner, null);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005110 File Offset: 0x00003310
		public void ShowDialog(IWin32Window owner, object argument)
		{
			this.RunProgressDialog((owner == null) ? NativeMethods.GetActiveWindow() : owner.Handle, argument);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000512B File Offset: 0x0000332B
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null, null, null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005139 File Offset: 0x00003339
		public void ReportProgress(int percentProgress, string text, string description)
		{
			this.ReportProgress(percentProgress, text, description, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005148 File Offset: 0x00003348
		public void ReportProgress(int percentProgress, string text, string description, object userState)
		{
			bool flag = percentProgress < 0 || percentProgress > 100;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("percentProgress");
			}
			bool flag2 = this._dialog == null;
			if (flag2)
			{
				throw new InvalidOperationException(Resources.ProgressDialogNotRunningError);
			}
			this._backgroundWorker.ReportProgress(percentProgress, new ProgressDialog.ProgressChangedData
			{
				Text = text,
				Description = description,
				UserState = userState
			});
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000051B4 File Offset: 0x000033B4
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWork = this.DoWork;
			bool flag = doWork != null;
			if (flag)
			{
				doWork.Invoke(this, e);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000051DC File Offset: 0x000033DC
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompleted = this.RunWorkerCompleted;
			bool flag = runWorkerCompleted != null;
			if (flag)
			{
				runWorkerCompleted.Invoke(this, e);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005204 File Offset: 0x00003404
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChanged = this.ProgressChanged;
			bool flag = progressChanged != null;
			if (flag)
			{
				progressChanged.Invoke(this, e);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000522C File Offset: 0x0000342C
		private void RunProgressDialog(IntPtr owner, object argument)
		{
			bool isBusy = this._backgroundWorker.IsBusy;
			if (isBusy)
			{
				throw new InvalidOperationException(Resources.ProgressDialogRunning);
			}
			bool flag = this.Animation != null;
			if (flag)
			{
				try
				{
					this._currentAnimationModuleHandle = this.Animation.LoadLibrary();
				}
				catch (Win32Exception ex)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnimationLoadErrorFormat, new object[] { ex.Message }), ex);
				}
				catch (FileNotFoundException ex2)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnimationLoadErrorFormat, new object[] { ex2.Message }), ex2);
				}
			}
			this._cancellationPending = false;
			this._dialog = (ProgressDialog)new ProgressDialogRCW();
			this._dialog.SetTitle(this.WindowTitle);
			bool flag2 = this.Animation != null;
			if (flag2)
			{
				this._dialog.SetAnimation(this._currentAnimationModuleHandle, (ushort)this.Animation.ResourceId);
			}
			bool flag3 = this.CancellationText.Length > 0;
			if (flag3)
			{
				this._dialog.SetCancelMsg(this.CancellationText, null);
			}
			this._dialog.SetLine(1U, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
			this._dialog.SetLine(2U, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
			ProgressDialogFlags progressDialogFlags = ProgressDialogFlags.Normal;
			bool flag4 = owner != IntPtr.Zero;
			if (flag4)
			{
				progressDialogFlags |= ProgressDialogFlags.Modal;
			}
			ProgressBarStyle progressBarStyle = this.ProgressBarStyle;
			if (progressBarStyle != ProgressBarStyle.None)
			{
				if (progressBarStyle == ProgressBarStyle.MarqueeProgressBar)
				{
					bool isWindowsVistaOrLater = NativeMethods.IsWindowsVistaOrLater;
					if (isWindowsVistaOrLater)
					{
						progressDialogFlags |= ProgressDialogFlags.MarqueeProgress;
					}
					else
					{
						progressDialogFlags |= ProgressDialogFlags.NoProgressBar;
					}
				}
			}
			else
			{
				progressDialogFlags |= ProgressDialogFlags.NoProgressBar;
			}
			bool showTimeRemaining = this.ShowTimeRemaining;
			if (showTimeRemaining)
			{
				progressDialogFlags |= ProgressDialogFlags.AutoTime;
			}
			bool flag5 = !this.ShowCancelButton;
			if (flag5)
			{
				progressDialogFlags |= ProgressDialogFlags.NoCancel;
			}
			bool flag6 = !this.MinimizeBox;
			if (flag6)
			{
				progressDialogFlags |= ProgressDialogFlags.NoMinimize;
			}
			this._dialog.StartProgressDialog(owner, null, progressDialogFlags, IntPtr.Zero);
			this._backgroundWorker.RunWorkerAsync(argument);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005444 File Offset: 0x00003644
		private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.OnDoWork(e);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005450 File Offset: 0x00003650
		private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._dialog.StopProgressDialog();
			Marshal.ReleaseComObject(this._dialog);
			this._dialog = null;
			bool flag = this._currentAnimationModuleHandle != null;
			if (flag)
			{
				this._currentAnimationModuleHandle.Dispose();
				this._currentAnimationModuleHandle = null;
			}
			this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs((!e.Cancelled && e.Error == null) ? e.Result : null, e.Error, e.Cancelled));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000054D0 File Offset: 0x000036D0
		private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this._cancellationPending = this._dialog.HasUserCancelled();
			bool flag = e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100;
			if (flag)
			{
				this._dialog.SetProgress((uint)e.ProgressPercentage, 100U);
				ProgressDialog.ProgressChangedData progressChangedData = e.UserState as ProgressDialog.ProgressChangedData;
				bool flag2 = progressChangedData != null;
				if (flag2)
				{
					bool flag3 = progressChangedData.Text != null;
					if (flag3)
					{
						this.Text = progressChangedData.Text;
					}
					bool flag4 = progressChangedData.Description != null;
					if (flag4)
					{
						this.Description = progressChangedData.Description;
					}
					this.OnProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, progressChangedData.UserState));
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005588 File Offset: 0x00003788
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					bool flag = this.components != null;
					if (flag)
					{
						this.components.Dispose();
					}
					bool flag2 = this._currentAnimationModuleHandle != null;
					if (flag2)
					{
						this._currentAnimationModuleHandle.Dispose();
						this._currentAnimationModuleHandle = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000055F8 File Offset: 0x000037F8
		private void InitializeComponent()
		{
			this._backgroundWorker = new BackgroundWorker();
			this._backgroundWorker.WorkerReportsProgress = true;
			this._backgroundWorker.WorkerSupportsCancellation = true;
			this._backgroundWorker.DoWork += new DoWorkEventHandler(this._backgroundWorker_DoWork);
			this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
			this._backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this._backgroundWorker_ProgressChanged);
		}

		// Token: 0x04000053 RID: 83
		private string _windowTitle;

		// Token: 0x04000054 RID: 84
		private string _text;

		// Token: 0x04000055 RID: 85
		private string _description;

		// Token: 0x04000056 RID: 86
		private IProgressDialog _dialog;

		// Token: 0x04000057 RID: 87
		private string _cancellationText;

		// Token: 0x04000058 RID: 88
		private bool _useCompactPathsForText;

		// Token: 0x04000059 RID: 89
		private bool _useCompactPathsForDescription;

		// Token: 0x0400005A RID: 90
		private SafeModuleHandle _currentAnimationModuleHandle;

		// Token: 0x0400005B RID: 91
		private bool _cancellationPending;

		// Token: 0x04000064 RID: 100
		private IContainer components = null;

		// Token: 0x04000065 RID: 101
		private BackgroundWorker _backgroundWorker;

		// Token: 0x0200006F RID: 111
		private class ProgressChangedData
		{
			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060002FB RID: 763 RVA: 0x0000A33D File Offset: 0x0000853D
			// (set) Token: 0x060002FC RID: 764 RVA: 0x0000A345 File Offset: 0x00008545
			public string Text { get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060002FD RID: 765 RVA: 0x0000A34E File Offset: 0x0000854E
			// (set) Token: 0x060002FE RID: 766 RVA: 0x0000A356 File Offset: 0x00008556
			public string Description { get; set; }

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000A35F File Offset: 0x0000855F
			// (set) Token: 0x06000300 RID: 768 RVA: 0x0000A367 File Offset: 0x00008567
			public object UserState { get; set; }
		}
	}
}
