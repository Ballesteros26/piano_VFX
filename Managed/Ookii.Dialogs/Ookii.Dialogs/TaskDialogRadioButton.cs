using System;
using System.Collections;
using System.ComponentModel;

namespace Ookii.Dialogs
{
	// Token: 0x02000022 RID: 34
	public class TaskDialogRadioButton : TaskDialogItem
	{
		// Token: 0x060001AB RID: 427 RVA: 0x000077E2 File Offset: 0x000059E2
		public TaskDialogRadioButton()
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000077FE File Offset: 0x000059FE
		public TaskDialogRadioButton(IContainer container)
			: base(container)
		{
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008138 File Offset: 0x00006338
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00008150 File Offset: 0x00006350
		[Category("Appearance")]
		[Description("Indicates whether the radio button is checked.")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this._checked;
			}
			set
			{
				this._checked = value;
				bool flag = value && base.Owner != null;
				if (flag)
				{
					foreach (TaskDialogRadioButton taskDialogRadioButton in base.Owner.RadioButtons)
					{
						bool flag2 = taskDialogRadioButton != this;
						if (flag2)
						{
							taskDialogRadioButton.Checked = false;
						}
					}
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000081D0 File Offset: 0x000063D0
		protected override IEnumerable ItemCollection
		{
			get
			{
				bool flag = base.Owner != null;
				IEnumerable enumerable;
				if (flag)
				{
					enumerable = base.Owner.RadioButtons;
				}
				else
				{
					enumerable = null;
				}
				return enumerable;
			}
		}

		// Token: 0x040000A3 RID: 163
		private bool _checked;
	}
}
