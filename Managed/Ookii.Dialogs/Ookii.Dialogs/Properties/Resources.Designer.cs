using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Ookii.Dialogs.Properties
{
	// Token: 0x02000029 RID: 41
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00009B74 File Offset: 0x00007D74
		internal Resources()
		{
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009B80 File Offset: 0x00007D80
		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Ookii.Dialogs.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009BC8 File Offset: 0x00007DC8
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00009BDF File Offset: 0x00007DDF
		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009BE8 File Offset: 0x00007DE8
		internal static string AnimationLoadErrorFormat
		{
			get
			{
				return Resources.ResourceManager.GetString("AnimationLoadErrorFormat", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009C10 File Offset: 0x00007E10
		internal static string CredentialEmptyTargetError
		{
			get
			{
				return Resources.ResourceManager.GetString("CredentialEmptyTargetError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009C38 File Offset: 0x00007E38
		internal static string CredentialError
		{
			get
			{
				return Resources.ResourceManager.GetString("CredentialError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009C60 File Offset: 0x00007E60
		internal static string CredentialPromptNotCalled
		{
			get
			{
				return Resources.ResourceManager.GetString("CredentialPromptNotCalled", Resources.resourceCulture);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009C88 File Offset: 0x00007E88
		internal static string DuplicateButtonTypeError
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateButtonTypeError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00009CB0 File Offset: 0x00007EB0
		internal static string DuplicateItemIdError
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateItemIdError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009CD8 File Offset: 0x00007ED8
		internal static string FileNotFoundFormat
		{
			get
			{
				return Resources.ResourceManager.GetString("FileNotFoundFormat", Resources.resourceCulture);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009D00 File Offset: 0x00007F00
		internal static string GlassNotSupportedError
		{
			get
			{
				return Resources.ResourceManager.GetString("GlassNotSupportedError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009D28 File Offset: 0x00007F28
		internal static string Help
		{
			get
			{
				return Resources.ResourceManager.GetString("Help", Resources.resourceCulture);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009D50 File Offset: 0x00007F50
		internal static string InvalidFilterString
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidFilterString", Resources.resourceCulture);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009D78 File Offset: 0x00007F78
		internal static string InvalidTaskDialogItemIdError
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidTaskDialogItemIdError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00009DA0 File Offset: 0x00007FA0
		internal static string NoAssociatedTaskDialogError
		{
			get
			{
				return Resources.ResourceManager.GetString("NoAssociatedTaskDialogError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009DC8 File Offset: 0x00007FC8
		internal static string NonCustomTaskDialogButtonIdError
		{
			get
			{
				return Resources.ResourceManager.GetString("NonCustomTaskDialogButtonIdError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00009DF0 File Offset: 0x00007FF0
		internal static string Preview
		{
			get
			{
				return Resources.ResourceManager.GetString("Preview", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009E18 File Offset: 0x00008018
		internal static string ProgressDialogNotRunningError
		{
			get
			{
				return Resources.ResourceManager.GetString("ProgressDialogNotRunningError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009E40 File Offset: 0x00008040
		internal static string ProgressDialogRunning
		{
			get
			{
				return Resources.ResourceManager.GetString("ProgressDialogRunning", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00009E68 File Offset: 0x00008068
		internal static string TaskDialogEmptyButtonLabelError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogEmptyButtonLabelError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009E90 File Offset: 0x00008090
		internal static string TaskDialogIllegalCrossThreadCallError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogIllegalCrossThreadCallError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00009EB8 File Offset: 0x000080B8
		internal static string TaskDialogItemHasOwnerError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogItemHasOwnerError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00009EE0 File Offset: 0x000080E0
		internal static string TaskDialogNoButtonsError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogNoButtonsError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009F08 File Offset: 0x00008108
		internal static string TaskDialogNotRunningError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogNotRunningError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009F30 File Offset: 0x00008130
		internal static string TaskDialogRunningError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogRunningError", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009F58 File Offset: 0x00008158
		internal static string TaskDialogsNotSupportedError
		{
			get
			{
				return Resources.ResourceManager.GetString("TaskDialogsNotSupportedError", Resources.resourceCulture);
			}
		}

		// Token: 0x040000C3 RID: 195
		private static ResourceManager resourceMan;

		// Token: 0x040000C4 RID: 196
		private static CultureInfo resourceCulture;
	}
}
