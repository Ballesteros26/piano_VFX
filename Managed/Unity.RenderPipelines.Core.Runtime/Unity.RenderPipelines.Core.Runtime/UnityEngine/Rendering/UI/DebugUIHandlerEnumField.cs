using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x0200009A RID: 154
	public class DebugUIHandlerEnumField : DebugUIHandlerWidget
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000EF14 File Offset: 0x0000D114
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.EnumField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000EF45 File Offset: 0x0000D145
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000EF6A File Offset: 0x0000D16A
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000EF8E File Offset: 0x0000D18E
		public override void OnAction()
		{
			this.OnIncrement(false);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000EF98 File Offset: 0x0000D198
		public override void OnIncrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] enumValues = this.m_Field.enumValues;
			int num = this.m_Field.currentIndex;
			if (num == enumValues.Length - 1)
			{
				num = 0;
			}
			else if (fast)
			{
				int[] array = this.m_Field.quickSeparators;
				if (array == null)
				{
					this.m_Field.InitQuickSeparators();
					array = this.m_Field.quickSeparators;
				}
				int num2 = 0;
				while (num2 < array.Length && num + 1 > array[num2])
				{
					num2++;
				}
				if (num2 == array.Length)
				{
					num = 0;
				}
				else
				{
					num = array[num2];
				}
			}
			else
			{
				num++;
			}
			this.m_Field.SetValue(enumValues[num]);
			this.m_Field.currentIndex = num;
			this.UpdateValueLabel();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F04C File Offset: 0x0000D24C
		public override void OnDecrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] enumValues = this.m_Field.enumValues;
			int num = this.m_Field.currentIndex;
			if (num == 0)
			{
				if (fast)
				{
					int[] array = this.m_Field.quickSeparators;
					if (array == null)
					{
						this.m_Field.InitQuickSeparators();
						array = this.m_Field.quickSeparators;
					}
					num = array[array.Length - 1];
				}
				else
				{
					num = enumValues.Length - 1;
				}
			}
			else if (fast)
			{
				int[] array2 = this.m_Field.quickSeparators;
				if (array2 == null)
				{
					this.m_Field.InitQuickSeparators();
					array2 = this.m_Field.quickSeparators;
				}
				int num2 = array2.Length - 1;
				while (num2 > 0 && num <= array2[num2])
				{
					num2--;
				}
				num = array2[num2];
			}
			else
			{
				num--;
			}
			this.m_Field.SetValue(enumValues[num]);
			this.m_Field.currentIndex = num;
			this.UpdateValueLabel();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000F130 File Offset: 0x0000D330
		protected virtual void UpdateValueLabel()
		{
			int num = this.m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			this.valueLabel.text = "< " + this.m_Field.enumNames[num].text + " >";
		}

		// Token: 0x040001EF RID: 495
		public Text nameLabel;

		// Token: 0x040001F0 RID: 496
		public Text valueLabel;

		// Token: 0x040001F1 RID: 497
		protected internal DebugUI.EnumField m_Field;
	}
}
