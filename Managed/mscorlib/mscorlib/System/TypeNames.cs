using System;

namespace System
{
	// Token: 0x02000240 RID: 576
	internal class TypeNames
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x00066F8C File Offset: 0x0006518C
		internal static TypeName FromDisplay(string displayName)
		{
			return new TypeNames.Display(displayName);
		}

		// Token: 0x02000241 RID: 577
		internal abstract class ATypeName : TypeName, IEquatable<TypeName>
		{
			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06001B32 RID: 6962
			public abstract string DisplayName { get; }

			// Token: 0x06001B33 RID: 6963
			public abstract TypeName NestedName(TypeIdentifier innerName);

			// Token: 0x06001B34 RID: 6964 RVA: 0x00066F94 File Offset: 0x00065194
			public bool Equals(TypeName other)
			{
				return other != null && this.DisplayName == other.DisplayName;
			}

			// Token: 0x06001B35 RID: 6965 RVA: 0x00066FAC File Offset: 0x000651AC
			public override int GetHashCode()
			{
				return this.DisplayName.GetHashCode();
			}

			// Token: 0x06001B36 RID: 6966 RVA: 0x00066FB9 File Offset: 0x000651B9
			public override bool Equals(object other)
			{
				return this.Equals(other as TypeName);
			}
		}

		// Token: 0x02000242 RID: 578
		private class Display : TypeNames.ATypeName
		{
			// Token: 0x06001B38 RID: 6968 RVA: 0x00066FC7 File Offset: 0x000651C7
			internal Display(string displayName)
			{
				this.displayName = displayName;
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00066FD6 File Offset: 0x000651D6
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x06001B3A RID: 6970 RVA: 0x00066FDE File Offset: 0x000651DE
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return new TypeNames.Display(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000F52 RID: 3922
			private string displayName;
		}
	}
}
