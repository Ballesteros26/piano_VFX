using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A7 RID: 423
	public struct CustomStyleProperty<T> : IEquatable<CustomStyleProperty<T>>
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002EF5E File Offset: 0x0002D15E
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x0002EF66 File Offset: 0x0002D166
		public string name { get; private set; }

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002EF70 File Offset: 0x0002D170
		public CustomStyleProperty(string propertyName)
		{
			bool flag = !string.IsNullOrEmpty(propertyName) && !propertyName.StartsWith("--");
			if (flag)
			{
				throw new ArgumentException("Custom style property \"" + propertyName + "\" must start with \"--\" prefix.");
			}
			this.name = propertyName;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002EFBC File Offset: 0x0002D1BC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is CustomStyleProperty<T>);
			return !flag && this.Equals((CustomStyleProperty<T>)obj);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		public bool Equals(CustomStyleProperty<T> other)
		{
			return this.name == other.name;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002F014 File Offset: 0x0002D214
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002F034 File Offset: 0x0002D234
		public static bool operator ==(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002F050 File Offset: 0x0002D250
		public static bool operator !=(CustomStyleProperty<T> a, CustomStyleProperty<T> b)
		{
			return !(a == b);
		}
	}
}
