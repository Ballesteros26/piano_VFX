using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BC RID: 444
	internal struct InheritedData : IEquatable<InheritedData>
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x00034C64 File Offset: 0x00032E64
		public static bool operator ==(InheritedData lhs, InheritedData rhs)
		{
			return lhs.color == rhs.color && lhs.fontSize == rhs.fontSize && lhs.unityFont == rhs.unityFont && lhs.unityFontStyleAndWeight.value == rhs.unityFontStyleAndWeight.value && lhs.unityFontStyleAndWeight.keyword == rhs.unityFontStyleAndWeight.keyword && lhs.unityTextAlign.value == rhs.unityTextAlign.value && lhs.unityTextAlign.keyword == rhs.unityTextAlign.keyword && lhs.visibility.value == rhs.visibility.value && lhs.visibility.keyword == rhs.visibility.keyword && lhs.whiteSpace.value == rhs.whiteSpace.value && lhs.whiteSpace.keyword == rhs.whiteSpace.keyword;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00034D94 File Offset: 0x00032F94
		public static bool operator !=(InheritedData lhs, InheritedData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00034DB0 File Offset: 0x00032FB0
		public bool Equals(InheritedData other)
		{
			return other == this;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00034DD0 File Offset: 0x00032FD0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is InheritedData && this.Equals((InheritedData)obj);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00034E08 File Offset: 0x00033008
		public override int GetHashCode()
		{
			int num = this.color.GetHashCode();
			num = (num * 397) ^ this.fontSize.GetHashCode();
			num = (num * 397) ^ this.unityFont.GetHashCode();
			num = (num * 397) ^ this.unityFontStyleAndWeight.GetHashCode();
			num = (num * 397) ^ this.unityTextAlign.GetHashCode();
			num = (num * 397) ^ this.visibility.GetHashCode();
			return (num * 397) ^ this.whiteSpace.GetHashCode();
		}

		// Token: 0x0400054B RID: 1355
		public StyleColor color;

		// Token: 0x0400054C RID: 1356
		public StyleLength fontSize;

		// Token: 0x0400054D RID: 1357
		public StyleFont unityFont;

		// Token: 0x0400054E RID: 1358
		public StyleEnum<FontStyle> unityFontStyleAndWeight;

		// Token: 0x0400054F RID: 1359
		public StyleEnum<TextAnchor> unityTextAlign;

		// Token: 0x04000550 RID: 1360
		public StyleEnum<Visibility> visibility;

		// Token: 0x04000551 RID: 1361
		public StyleEnum<WhiteSpace> whiteSpace;
	}
}
