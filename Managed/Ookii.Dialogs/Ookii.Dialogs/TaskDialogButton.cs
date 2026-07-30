using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x0200001B RID: 27
	public class TaskDialogButton : TaskDialogItem
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000077E2 File Offset: 0x000059E2
		public TaskDialogButton()
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000077EC File Offset: 0x000059EC
		public TaskDialogButton(ButtonType type)
			: base((int)type)
		{
			this._type = type;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000077FE File Offset: 0x000059FE
		public TaskDialogButton(IContainer container)
			: base(container)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007809 File Offset: 0x00005A09
		public TaskDialogButton(string text)
		{
			base.Text = text;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000781C File Offset: 0x00005A1C
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00007834 File Offset: 0x00005A34
		[Category("Appearance")]
		[Description("The type of the button.")]
		[DefaultValue(ButtonType.Custom)]
		public ButtonType ButtonType
		{
			get
			{
				return this._type;
			}
			set
			{
				bool flag = value > ButtonType.Custom;
				if (flag)
				{
					this.CheckDuplicateButton(value, null);
					this._type = value;
					base.Id = (int)value;
				}
				else
				{
					this._type = value;
					this.AutoAssignId();
					base.UpdateOwner();
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007880 File Offset: 0x00005A80
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000078A1 File Offset: 0x00005AA1
		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the note associated with a command link button.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string CommandLinkNote
		{
			get
			{
				return this._commandLinkNote ?? string.Empty;
			}
			set
			{
				this._commandLinkNote = value;
				base.UpdateOwner();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000078B4 File Offset: 0x00005AB4
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000078CC File Offset: 0x00005ACC
		[Category("Behavior")]
		[Description("Indicates if the button is the default button on the dialog.")]
		[DefaultValue(false)]
		public bool Default
		{
			get
			{
				return this._default;
			}
			set
			{
				this._default = value;
				bool flag = value && base.Owner != null;
				if (flag)
				{
					foreach (TaskDialogButton taskDialogButton in base.Owner.Buttons)
					{
						bool flag2 = taskDialogButton != this;
						if (flag2)
						{
							taskDialogButton.Default = false;
						}
					}
				}
				base.UpdateOwner();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00007954 File Offset: 0x00005B54
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000796C File Offset: 0x00005B6C
		[Category("Behavior")]
		[Description("Indicates whether the Task Dialog button or command link should have a User Account Control (UAC) shield icon (in other words, whether the action invoked by the button requires elevation).")]
		[DefaultValue(false)]
		public bool ElevationRequired
		{
			get
			{
				return this._elevationRequired;
			}
			set
			{
				this._elevationRequired = value;
				bool flag = base.Owner != null;
				if (flag)
				{
					base.Owner.SetButtonElevationRequired(this);
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000799C File Offset: 0x00005B9C
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000079B4 File Offset: 0x00005BB4
		internal override int Id
		{
			get
			{
				return base.Id;
			}
			set
			{
				bool flag = base.Id != value;
				if (flag)
				{
					bool flag2 = this._type > ButtonType.Custom;
					if (flag2)
					{
						throw new InvalidOperationException(Resources.NonCustomTaskDialogButtonIdError);
					}
					base.Id = value;
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000079F4 File Offset: 0x00005BF4
		internal override void AutoAssignId()
		{
			bool flag = this._type == ButtonType.Custom;
			if (flag)
			{
				base.AutoAssignId();
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007A16 File Offset: 0x00005C16
		internal override void CheckDuplicate(TaskDialogItem itemToExclude)
		{
			this.CheckDuplicateButton(this._type, itemToExclude);
			base.CheckDuplicate(itemToExclude);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007A30 File Offset: 0x00005C30
		internal NativeMethods.TaskDialogCommonButtonFlags ButtonFlag
		{
			get
			{
				switch (this._type)
				{
				case ButtonType.Ok:
					return NativeMethods.TaskDialogCommonButtonFlags.OkButton;
				case ButtonType.Cancel:
					return NativeMethods.TaskDialogCommonButtonFlags.CancelButton;
				case ButtonType.Retry:
					return NativeMethods.TaskDialogCommonButtonFlags.RetryButton;
				case ButtonType.Yes:
					return NativeMethods.TaskDialogCommonButtonFlags.YesButton;
				case ButtonType.No:
					return NativeMethods.TaskDialogCommonButtonFlags.NoButton;
				case ButtonType.Close:
					return NativeMethods.TaskDialogCommonButtonFlags.CloseButton;
				}
				return (NativeMethods.TaskDialogCommonButtonFlags)0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007A90 File Offset: 0x00005C90
		protected override IEnumerable ItemCollection
		{
			get
			{
				bool flag = base.Owner != null;
				IEnumerable enumerable;
				if (flag)
				{
					enumerable = base.Owner.Buttons;
				}
				else
				{
					enumerable = null;
				}
				return enumerable;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007AC0 File Offset: 0x00005CC0
		private void CheckDuplicateButton(ButtonType type, TaskDialogItem itemToExclude)
		{
			bool flag = type != ButtonType.Custom && base.Owner != null;
			if (flag)
			{
				foreach (TaskDialogButton taskDialogButton in base.Owner.Buttons)
				{
					bool flag2 = taskDialogButton != this && taskDialogButton != itemToExclude && taskDialogButton.ButtonType == type;
					if (flag2)
					{
						throw new InvalidOperationException(Resources.DuplicateButtonTypeError);
					}
				}
			}
		}

		// Token: 0x0400008E RID: 142
		private ButtonType _type;

		// Token: 0x0400008F RID: 143
		private bool _elevationRequired;

		// Token: 0x04000090 RID: 144
		private bool _default;

		// Token: 0x04000091 RID: 145
		private string _commandLinkNote;
	}
}
