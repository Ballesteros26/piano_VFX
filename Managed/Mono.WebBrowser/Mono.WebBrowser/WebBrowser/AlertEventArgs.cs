using System;
using System.Collections.Specialized;

namespace Mono.WebBrowser
{
	// Token: 0x0200000E RID: 14
	public class AlertEventArgs : EventArgs
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000021F6 File Offset: 0x000003F6
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000021FE File Offset: 0x000003FE
		public DialogType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002207 File Offset: 0x00000407
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000220F File Offset: 0x0000040F
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002218 File Offset: 0x00000418
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002220 File Offset: 0x00000420
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002229 File Offset: 0x00000429
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002231 File Offset: 0x00000431
		public string Text2
		{
			get
			{
				return this.text2;
			}
			set
			{
				this.text2 = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000223A File Offset: 0x0000043A
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002242 File Offset: 0x00000442
		public string CheckMessage
		{
			get
			{
				return this.checkMsg;
			}
			set
			{
				this.checkMsg = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000224B File Offset: 0x0000044B
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002253 File Offset: 0x00000453
		public bool CheckState
		{
			get
			{
				return this.checkState;
			}
			set
			{
				this.checkState = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000225C File Offset: 0x0000045C
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002264 File Offset: 0x00000464
		public DialogButtonFlags DialogButtons
		{
			get
			{
				return this.dialogButtons;
			}
			set
			{
				this.dialogButtons = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000226D File Offset: 0x0000046D
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002275 File Offset: 0x00000475
		public StringCollection Buttons
		{
			get
			{
				return this.buttons;
			}
			set
			{
				this.buttons = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000227E File Offset: 0x0000047E
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002286 File Offset: 0x00000486
		public StringCollection Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000228F File Offset: 0x0000048F
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002297 File Offset: 0x00000497
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000022A0 File Offset: 0x000004A0
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000022A8 File Offset: 0x000004A8
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000022B1 File Offset: 0x000004B1
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000022CD File Offset: 0x000004CD
		public bool BoolReturn
		{
			get
			{
				return this.returnValue is bool && (bool)this.returnValue;
			}
			set
			{
				this.returnValue = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000022DB File Offset: 0x000004DB
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000022F7 File Offset: 0x000004F7
		public int IntReturn
		{
			get
			{
				if (this.returnValue is int)
				{
					return (int)this.returnValue;
				}
				return -1;
			}
			set
			{
				this.returnValue = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002305 File Offset: 0x00000505
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002325 File Offset: 0x00000525
		public string StringReturn
		{
			get
			{
				if (this.returnValue is string)
				{
					return (string)this.returnValue;
				}
				return string.Empty;
			}
			set
			{
				this.returnValue = value;
			}
		}

		// Token: 0x04000058 RID: 88
		private DialogType type;

		// Token: 0x04000059 RID: 89
		private string title;

		// Token: 0x0400005A RID: 90
		private string text;

		// Token: 0x0400005B RID: 91
		private string text2;

		// Token: 0x0400005C RID: 92
		private string username;

		// Token: 0x0400005D RID: 93
		private string password;

		// Token: 0x0400005E RID: 94
		private string checkMsg;

		// Token: 0x0400005F RID: 95
		private bool checkState;

		// Token: 0x04000060 RID: 96
		private DialogButtonFlags dialogButtons;

		// Token: 0x04000061 RID: 97
		private StringCollection buttons;

		// Token: 0x04000062 RID: 98
		private StringCollection options;

		// Token: 0x04000063 RID: 99
		private object returnValue;
	}
}
