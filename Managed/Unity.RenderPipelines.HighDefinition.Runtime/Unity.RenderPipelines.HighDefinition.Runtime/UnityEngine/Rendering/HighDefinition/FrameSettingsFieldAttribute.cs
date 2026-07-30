using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Field)]
	internal class FrameSettingsFieldAttribute : Attribute
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00003788 File Offset: 0x00001988
		public FrameSettingsFieldAttribute(int group, FrameSettingsField autoName = FrameSettingsField.None, string displayedName = null, string tooltip = null, FrameSettingsFieldAttribute.DisplayType type = FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, Type targetType = null, FrameSettingsField[] positiveDependencies = null, FrameSettingsField[] negativeDependencies = null, int customOrderInGroup = -1)
		{
			if (string.IsNullOrEmpty(displayedName))
			{
				displayedName = autoName.ToString().CamelToPascalCaseWithSpace(true);
			}
			this.group = group;
			if (customOrderInGroup != -1)
			{
				FrameSettingsFieldAttribute.autoOrder = customOrderInGroup;
			}
			this.orderInGroup = FrameSettingsFieldAttribute.autoOrder++;
			this.displayedName = displayedName;
			this.type = type;
			this.targetType = targetType;
			this.dependencySeparator = ((positiveDependencies != null) ? positiveDependencies.Length : 0);
			this.dependencies = new FrameSettingsField[this.dependencySeparator + ((negativeDependencies != null) ? negativeDependencies.Length : 0)];
			if (positiveDependencies != null)
			{
				positiveDependencies.CopyTo(this.dependencies, 0);
			}
			if (negativeDependencies != null)
			{
				negativeDependencies.CopyTo(this.dependencies, this.dependencySeparator);
			}
			FrameSettingsField[] array = this.dependencies;
			this.indentLevel = ((array != null) ? array.Length : 0);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003864 File Offset: 0x00001A64
		public bool IsNegativeDependency(FrameSettingsField frameSettingsField)
		{
			return Array.FindIndex<FrameSettingsField>(this.dependencies, (FrameSettingsField fsf) => fsf == frameSettingsField) >= this.dependencySeparator;
		}

		// Token: 0x04000071 RID: 113
		public readonly FrameSettingsFieldAttribute.DisplayType type;

		// Token: 0x04000072 RID: 114
		public readonly string displayedName;

		// Token: 0x04000073 RID: 115
		public readonly string tooltip;

		// Token: 0x04000074 RID: 116
		public readonly int group;

		// Token: 0x04000075 RID: 117
		public readonly int orderInGroup;

		// Token: 0x04000076 RID: 118
		public readonly Type targetType;

		// Token: 0x04000077 RID: 119
		public readonly int indentLevel;

		// Token: 0x04000078 RID: 120
		public readonly FrameSettingsField[] dependencies;

		// Token: 0x04000079 RID: 121
		private readonly int dependencySeparator;

		// Token: 0x0400007A RID: 122
		private static int autoOrder;

		// Token: 0x0200018B RID: 395
		public enum DisplayType
		{
			// Token: 0x040010A6 RID: 4262
			BoolAsCheckbox,
			// Token: 0x040010A7 RID: 4263
			BoolAsEnumPopup,
			// Token: 0x040010A8 RID: 4264
			Others
		}
	}
}
