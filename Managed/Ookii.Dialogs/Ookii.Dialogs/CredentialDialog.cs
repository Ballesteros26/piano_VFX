using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000006 RID: 6
	[DefaultProperty("MainInstruction")]
	[DefaultEvent("UserNameChanged")]
	[Description("Allows access to credential UI for generic credentials.")]
	public class CredentialDialog : Component
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002354 File Offset: 0x00000554
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x0000238C File Offset: 0x0000058C
		[Category("Property Changed")]
		[Description("Event raised when the value of the UserName property changes.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler UserNameChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000010 RID: 16 RVA: 0x000023C4 File Offset: 0x000005C4
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x000023FC File Offset: 0x000005FC
		[Category("Property Changed")]
		[Description("Event raised when the value of the Password property changes.")]
		[field: DebuggerBrowsable(0)]
		public event EventHandler PasswordChanged;

		// Token: 0x06000012 RID: 18 RVA: 0x00002431 File Offset: 0x00000631
		public CredentialDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002454 File Offset: 0x00000654
		public CredentialDialog(IContainer container)
		{
			bool flag = container != null;
			if (flag)
			{
				container.Add(this);
			}
			this.InitializeComponent();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000249A File Offset: 0x0000069A
		[Category("Behavior")]
		[Description("Indicates whether to use the application instance credential cache.")]
		[DefaultValue(false)]
		public bool UseApplicationInstanceCredentialCache { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024A4 File Offset: 0x000006A4
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000024BC File Offset: 0x000006BC
		[Category("Appearance")]
		[Description("Indicates whether the \"Save password\" checkbox is checked.")]
		[DefaultValue(false)]
		public bool IsSaveChecked
		{
			get
			{
				return this._isSaveChecked;
			}
			set
			{
				this._confirmTarget = null;
				this._isSaveChecked = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024D0 File Offset: 0x000006D0
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024ED File Offset: 0x000006ED
		[Browsable(false)]
		public string Password
		{
			get
			{
				return this._credentials.Password;
			}
			private set
			{
				this._confirmTarget = null;
				this._credentials.Password = value;
				this.OnPasswordChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002510 File Offset: 0x00000710
		[Browsable(false)]
		public NetworkCredential Credentials
		{
			get
			{
				return this._credentials;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002528 File Offset: 0x00000728
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000254E File Offset: 0x0000074E
		[Browsable(false)]
		public string UserName
		{
			get
			{
				return this._credentials.UserName ?? string.Empty;
			}
			private set
			{
				this._confirmTarget = null;
				this._credentials.UserName = value;
				this.OnUserNameChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002574 File Offset: 0x00000774
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002595 File Offset: 0x00000795
		[Category("Behavior")]
		[Description("The target for the credentials, typically the server name prefixed by an application-specific identifier.")]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				return this._target ?? string.Empty;
			}
			set
			{
				this._target = value;
				this._confirmTarget = null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025A8 File Offset: 0x000007A8
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000025C9 File Offset: 0x000007C9
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The title of the credentials dialog.")]
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025F5 File Offset: 0x000007F5
		[Localizable(true)]
		[Category("Appearance")]
		[Description("A brief message that will be displayed in the dialog box.")]
		[DefaultValue("")]
		public string MainInstruction
		{
			get
			{
				return this._caption ?? string.Empty;
			}
			set
			{
				this._caption = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002621 File Offset: 0x00000821
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional text to display in the dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Content
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000262B File Offset: 0x0000082B
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002633 File Offset: 0x00000833
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Indicates how the text of the MainInstruction and Content properties is displayed on Windows XP.")]
		[DefaultValue(DownlevelTextMode.MainInstructionAndContent)]
		public DownlevelTextMode DownlevelTextMode { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000263C File Offset: 0x0000083C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002644 File Offset: 0x00000844
		[Category("Appearance")]
		[Description("Indicates whether a check box is shown on the dialog that allows the user to choose whether to save the credentials or not.")]
		[DefaultValue(false)]
		public bool ShowSaveCheckBox { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000264D File Offset: 0x0000084D
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002655 File Offset: 0x00000855
		[Category("Behavior")]
		[Description("Indicates whether the dialog should be displayed even when saved credentials exist for the specified target.")]
		[DefaultValue(false)]
		public bool ShowUIForSavedCredentials { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000265E File Offset: 0x0000085E
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002666 File Offset: 0x00000866
		public bool IsStoredCredential { get; private set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002670 File Offset: 0x00000870
		[SecurityPermission(6, Flags = 2)]
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000268C File Offset: 0x0000088C
		[SecurityPermission(6, Flags = 2)]
		public DialogResult ShowDialog(IWin32Window owner)
		{
			bool flag = string.IsNullOrEmpty(this._target);
			if (flag)
			{
				throw new InvalidOperationException(Resources.CredentialEmptyTargetError);
			}
			this.UserName = "";
			this.Password = "";
			this.IsStoredCredential = false;
			bool flag2 = this.RetrieveCredentialsFromApplicationInstanceCache();
			DialogResult dialogResult;
			if (flag2)
			{
				this.IsStoredCredential = true;
				this._confirmTarget = this.Target;
				dialogResult = DialogResult.OK;
			}
			else
			{
				bool flag3 = false;
				bool flag4 = this.ShowSaveCheckBox && this.RetrieveCredentials();
				if (flag4)
				{
					this.IsSaveChecked = true;
					bool flag5 = !this.ShowUIForSavedCredentials;
					if (flag5)
					{
						this.IsStoredCredential = true;
						this._confirmTarget = this.Target;
						return DialogResult.OK;
					}
					flag3 = true;
				}
				IntPtr intPtr = ((owner == null) ? NativeMethods.GetActiveWindow() : owner.Handle);
				bool isWindowsVistaOrLater = NativeMethods.IsWindowsVistaOrLater;
				bool flag6;
				if (isWindowsVistaOrLater)
				{
					flag6 = this.PromptForCredentialsCredUIWin(intPtr, flag3);
				}
				else
				{
					flag6 = this.PromptForCredentialsCredUI(intPtr, flag3);
				}
				dialogResult = (flag6 ? DialogResult.OK : DialogResult.Cancel);
			}
			return dialogResult;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002788 File Offset: 0x00000988
		public void ConfirmCredentials(bool confirm)
		{
			bool flag = this._confirmTarget == null || this._confirmTarget != this.Target;
			if (flag)
			{
				throw new InvalidOperationException(Resources.CredentialPromptNotCalled);
			}
			this._confirmTarget = null;
			bool flag2 = this.IsSaveChecked && confirm;
			if (flag2)
			{
				bool useApplicationInstanceCredentialCache = this.UseApplicationInstanceCredentialCache;
				if (useApplicationInstanceCredentialCache)
				{
					Dictionary<string, NetworkCredential> applicationInstanceCredentialCache = CredentialDialog._applicationInstanceCredentialCache;
					lock (applicationInstanceCredentialCache)
					{
						CredentialDialog._applicationInstanceCredentialCache[this.Target] = new NetworkCredential(this.UserName, this.Password);
					}
				}
				CredentialDialog.StoreCredential(this.Target, this.Credentials);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002840 File Offset: 0x00000A40
		public static void StoreCredential(string target, NetworkCredential credential)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = target.Length == 0;
			if (flag2)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			bool flag3 = credential == null;
			if (flag3)
			{
				throw new ArgumentNullException("credential");
			}
			NativeMethods.CREDENTIAL credential2 = default(NativeMethods.CREDENTIAL);
			credential2.UserName = credential.UserName;
			credential2.TargetName = target;
			credential2.Persist = NativeMethods.CredPersist.Enterprise;
			byte[] array = CredentialDialog.EncryptPassword(credential.Password);
			credential2.CredentialBlob = Marshal.AllocHGlobal(array.Length);
			try
			{
				Marshal.Copy(array, 0, credential2.CredentialBlob, array.Length);
				credential2.CredentialBlobSize = (uint)array.Length;
				credential2.Type = NativeMethods.CredTypes.CRED_TYPE_GENERIC;
				bool flag4 = !NativeMethods.CredWrite(ref credential2, 0);
				if (flag4)
				{
					throw new CredentialException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeCoTaskMem(credential2.CredentialBlob);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002938 File Offset: 0x00000B38
		public static NetworkCredential RetrieveCredential(string target)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = target.Length == 0;
			if (flag2)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			NetworkCredential networkCredential = CredentialDialog.RetrieveCredentialFromApplicationInstanceCache(target);
			bool flag3 = networkCredential != null;
			NetworkCredential networkCredential2;
			if (flag3)
			{
				networkCredential2 = networkCredential;
			}
			else
			{
				IntPtr intPtr;
				bool flag4 = NativeMethods.CredRead(target, NativeMethods.CredTypes.CRED_TYPE_GENERIC, 0, out intPtr);
				int lastWin32Error = Marshal.GetLastWin32Error();
				bool flag5 = flag4;
				if (flag5)
				{
					try
					{
						NativeMethods.CREDENTIAL credential = (NativeMethods.CREDENTIAL)Marshal.PtrToStructure(intPtr, typeof(NativeMethods.CREDENTIAL));
						byte[] array = new byte[credential.CredentialBlobSize];
						Marshal.Copy(credential.CredentialBlob, array, 0, array.Length);
						networkCredential = new NetworkCredential(credential.UserName, CredentialDialog.DecryptPassword(array));
					}
					finally
					{
						NativeMethods.CredFree(intPtr);
					}
					networkCredential2 = networkCredential;
				}
				else
				{
					bool flag6 = lastWin32Error == 1168;
					if (!flag6)
					{
						throw new CredentialException(lastWin32Error);
					}
					networkCredential2 = null;
				}
			}
			return networkCredential2;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A3C File Offset: 0x00000C3C
		public static NetworkCredential RetrieveCredentialFromApplicationInstanceCache(string target)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = target.Length == 0;
			if (flag2)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			Dictionary<string, NetworkCredential> applicationInstanceCredentialCache = CredentialDialog._applicationInstanceCredentialCache;
			lock (applicationInstanceCredentialCache)
			{
				NetworkCredential networkCredential;
				bool flag3 = CredentialDialog._applicationInstanceCredentialCache.TryGetValue(target, ref networkCredential);
				if (flag3)
				{
					return networkCredential;
				}
			}
			return null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public static bool DeleteCredential(string target)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = target.Length == 0;
			if (flag2)
			{
				throw new ArgumentException(Resources.CredentialEmptyTargetError, "target");
			}
			bool flag3 = false;
			Dictionary<string, NetworkCredential> applicationInstanceCredentialCache = CredentialDialog._applicationInstanceCredentialCache;
			lock (applicationInstanceCredentialCache)
			{
				flag3 = CredentialDialog._applicationInstanceCredentialCache.Remove(target);
			}
			bool flag4 = NativeMethods.CredDelete(target, NativeMethods.CredTypes.CRED_TYPE_GENERIC, 0);
			if (flag4)
			{
				flag3 = true;
			}
			else
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				bool flag5 = lastWin32Error != 1168;
				if (flag5)
				{
					throw new CredentialException(lastWin32Error);
				}
			}
			return flag3;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B78 File Offset: 0x00000D78
		protected virtual void OnUserNameChanged(EventArgs e)
		{
			bool flag = this.UserNameChanged != null;
			if (flag)
			{
				this.UserNameChanged.Invoke(this, e);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BA4 File Offset: 0x00000DA4
		protected virtual void OnPasswordChanged(EventArgs e)
		{
			bool flag = this.PasswordChanged != null;
			if (flag)
			{
				this.PasswordChanged.Invoke(this, e);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BD0 File Offset: 0x00000DD0
		private bool PromptForCredentialsCredUI(IntPtr owner, bool storedCredentials)
		{
			NativeMethods.CREDUI_INFO credui_INFO = this.CreateCredUIInfo(owner, true);
			NativeMethods.CREDUI_FLAGS credui_FLAGS = NativeMethods.CREDUI_FLAGS.DO_NOT_PERSIST | NativeMethods.CREDUI_FLAGS.ALWAYS_SHOW_UI | NativeMethods.CREDUI_FLAGS.GENERIC_CREDENTIALS;
			bool showSaveCheckBox = this.ShowSaveCheckBox;
			if (showSaveCheckBox)
			{
				credui_FLAGS |= NativeMethods.CREDUI_FLAGS.SHOW_SAVE_CHECK_BOX;
			}
			StringBuilder stringBuilder = new StringBuilder(513);
			stringBuilder.Append(this.UserName);
			StringBuilder stringBuilder2 = new StringBuilder(256);
			stringBuilder2.Append(this.Password);
			NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.CredUIPromptForCredentials(ref credui_INFO, this.Target, IntPtr.Zero, 0, stringBuilder, 513U, stringBuilder2, 256U, ref this._isSaveChecked, credui_FLAGS);
			NativeMethods.CredUIReturnCodes credUIReturnCodes2 = credUIReturnCodes;
			bool flag;
			if (credUIReturnCodes2 != NativeMethods.CredUIReturnCodes.NO_ERROR)
			{
				if (credUIReturnCodes2 != NativeMethods.CredUIReturnCodes.ERROR_CANCELLED)
				{
					throw new CredentialException((int)credUIReturnCodes);
				}
				flag = false;
			}
			else
			{
				this.UserName = stringBuilder.ToString();
				this.Password = stringBuilder2.ToString();
				bool showSaveCheckBox2 = this.ShowSaveCheckBox;
				if (showSaveCheckBox2)
				{
					this._confirmTarget = this.Target;
					bool flag2 = storedCredentials && !this.IsSaveChecked;
					if (flag2)
					{
						CredentialDialog.DeleteCredential(this.Target);
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002CD4 File Offset: 0x00000ED4
		private bool PromptForCredentialsCredUIWin(IntPtr owner, bool storedCredentials)
		{
			NativeMethods.CREDUI_INFO credui_INFO = this.CreateCredUIInfo(owner, false);
			NativeMethods.CredUIWinFlags credUIWinFlags = NativeMethods.CredUIWinFlags.Generic;
			bool showSaveCheckBox = this.ShowSaveCheckBox;
			if (showSaveCheckBox)
			{
				credUIWinFlags |= NativeMethods.CredUIWinFlags.Checkbox;
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			bool flag4;
			try
			{
				uint num = 0U;
				bool flag = this.UserName.Length > 0;
				if (flag)
				{
					NativeMethods.CredPackAuthenticationBuffer(0U, this.UserName, this.Password, IntPtr.Zero, ref num);
					bool flag2 = num > 0U;
					if (flag2)
					{
						intPtr = Marshal.AllocCoTaskMem((int)num);
						bool flag3 = !NativeMethods.CredPackAuthenticationBuffer(0U, this.UserName, this.Password, intPtr, ref num);
						if (flag3)
						{
							throw new CredentialException(Marshal.GetLastWin32Error());
						}
					}
				}
				uint num2 = 0U;
				uint num3;
				NativeMethods.CredUIReturnCodes credUIReturnCodes = NativeMethods.CredUIPromptForWindowsCredentials(ref credui_INFO, 0U, ref num2, intPtr, num, out zero, out num3, ref this._isSaveChecked, credUIWinFlags);
				NativeMethods.CredUIReturnCodes credUIReturnCodes2 = credUIReturnCodes;
				if (credUIReturnCodes2 != NativeMethods.CredUIReturnCodes.NO_ERROR)
				{
					if (credUIReturnCodes2 != NativeMethods.CredUIReturnCodes.ERROR_CANCELLED)
					{
						throw new CredentialException((int)credUIReturnCodes);
					}
					flag4 = false;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(513);
					StringBuilder stringBuilder2 = new StringBuilder(256);
					uint capacity = (uint)stringBuilder.Capacity;
					uint capacity2 = (uint)stringBuilder2.Capacity;
					uint num4 = 0U;
					bool flag5 = !NativeMethods.CredUnPackAuthenticationBuffer(0U, zero, num3, stringBuilder, ref capacity, null, ref num4, stringBuilder2, ref capacity2);
					if (flag5)
					{
						throw new CredentialException(Marshal.GetLastWin32Error());
					}
					this.UserName = stringBuilder.ToString();
					this.Password = stringBuilder2.ToString();
					bool showSaveCheckBox2 = this.ShowSaveCheckBox;
					if (showSaveCheckBox2)
					{
						this._confirmTarget = this.Target;
						bool flag6 = storedCredentials && !this.IsSaveChecked;
						if (flag6)
						{
							CredentialDialog.DeleteCredential(this.Target);
						}
					}
					flag4 = true;
				}
			}
			finally
			{
				bool flag7 = intPtr != IntPtr.Zero;
				if (flag7)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
				bool flag8 = zero != IntPtr.Zero;
				if (flag8)
				{
					Marshal.FreeCoTaskMem(zero);
				}
			}
			return flag4;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EC4 File Offset: 0x000010C4
		private NativeMethods.CREDUI_INFO CreateCredUIInfo(IntPtr owner, bool downlevelText)
		{
			NativeMethods.CREDUI_INFO credui_INFO = default(NativeMethods.CREDUI_INFO);
			credui_INFO.cbSize = Marshal.SizeOf(credui_INFO);
			credui_INFO.hwndParent = owner;
			if (downlevelText)
			{
				credui_INFO.pszCaptionText = this.WindowTitle;
				switch (this.DownlevelTextMode)
				{
				case DownlevelTextMode.MainInstructionAndContent:
				{
					bool flag = this.MainInstruction.Length == 0;
					if (flag)
					{
						credui_INFO.pszMessageText = this.Content;
					}
					else
					{
						bool flag2 = this.Content.Length == 0;
						if (flag2)
						{
							credui_INFO.pszMessageText = this.MainInstruction;
						}
						else
						{
							credui_INFO.pszMessageText = this.MainInstruction + Environment.NewLine + Environment.NewLine + this.Content;
						}
					}
					break;
				}
				case DownlevelTextMode.MainInstructionOnly:
					credui_INFO.pszMessageText = this.MainInstruction;
					break;
				case DownlevelTextMode.ContentOnly:
					credui_INFO.pszMessageText = this.Content;
					break;
				}
			}
			else
			{
				credui_INFO.pszMessageText = this.Content;
				credui_INFO.pszCaptionText = this.MainInstruction;
			}
			return credui_INFO;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FD4 File Offset: 0x000011D4
		private bool RetrieveCredentials()
		{
			NetworkCredential networkCredential = CredentialDialog.RetrieveCredential(this.Target);
			bool flag = networkCredential != null;
			bool flag2;
			if (flag)
			{
				this.UserName = networkCredential.UserName;
				this.Password = networkCredential.Password;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000301C File Offset: 0x0000121C
		private static byte[] EncryptPassword(string password)
		{
			return ProtectedData.Protect(Encoding.UTF8.GetBytes(password), null, 0);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003044 File Offset: 0x00001244
		private static string DecryptPassword(byte[] encrypted)
		{
			string text;
			try
			{
				text = Encoding.UTF8.GetString(ProtectedData.Unprotect(encrypted, null, 0));
			}
			catch (CryptographicException)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003084 File Offset: 0x00001284
		private bool RetrieveCredentialsFromApplicationInstanceCache()
		{
			bool useApplicationInstanceCredentialCache = this.UseApplicationInstanceCredentialCache;
			if (useApplicationInstanceCredentialCache)
			{
				NetworkCredential networkCredential = CredentialDialog.RetrieveCredentialFromApplicationInstanceCache(this.Target);
				bool flag = networkCredential != null;
				if (flag)
				{
					this.UserName = networkCredential.UserName;
					this.Password = networkCredential.Password;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000030D8 File Offset: 0x000012D8
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003128 File Offset: 0x00001328
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x04000010 RID: 16
		private string _confirmTarget;

		// Token: 0x04000011 RID: 17
		private NetworkCredential _credentials = new NetworkCredential();

		// Token: 0x04000012 RID: 18
		private bool _isSaveChecked;

		// Token: 0x04000013 RID: 19
		private string _target;

		// Token: 0x04000014 RID: 20
		private static readonly Dictionary<string, NetworkCredential> _applicationInstanceCredentialCache = new Dictionary<string, NetworkCredential>();

		// Token: 0x04000015 RID: 21
		private string _caption;

		// Token: 0x04000016 RID: 22
		private string _text;

		// Token: 0x04000017 RID: 23
		private string _windowTitle;

		// Token: 0x0400001F RID: 31
		private IContainer components = null;
	}
}
