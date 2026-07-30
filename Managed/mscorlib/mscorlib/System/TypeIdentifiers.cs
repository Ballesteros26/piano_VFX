using System;

namespace System
{
	// Token: 0x02000243 RID: 579
	internal class TypeIdentifiers
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x00066FFB File Offset: 0x000651FB
		internal static TypeIdentifier FromDisplay(string displayName)
		{
			return new TypeIdentifiers.Display(displayName);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00067003 File Offset: 0x00065203
		internal static TypeIdentifier FromInternal(string internalName)
		{
			return new TypeIdentifiers.Internal(internalName);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0006700B File Offset: 0x0006520B
		internal static TypeIdentifier FromInternal(string internalNameSpace, TypeIdentifier typeName)
		{
			return new TypeIdentifiers.Internal(internalNameSpace, typeName);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00067014 File Offset: 0x00065214
		internal static TypeIdentifier WithoutEscape(string simpleName)
		{
			return new TypeIdentifiers.NoEscape(simpleName);
		}

		// Token: 0x02000244 RID: 580
		private class Display : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001B40 RID: 6976 RVA: 0x0006701C File Offset: 0x0006521C
			internal Display(string displayName)
			{
				this.displayName = displayName;
				this.internal_name = null;
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00067032 File Offset: 0x00065232
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0006703A File Offset: 0x0006523A
			public string InternalName
			{
				get
				{
					if (this.internal_name == null)
					{
						this.internal_name = this.GetInternalName();
					}
					return this.internal_name;
				}
			}

			// Token: 0x06001B43 RID: 6979 RVA: 0x00067056 File Offset: 0x00065256
			private string GetInternalName()
			{
				return TypeSpec.UnescapeInternalName(this.displayName);
			}

			// Token: 0x06001B44 RID: 6980 RVA: 0x00067063 File Offset: 0x00065263
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000F53 RID: 3923
			private string displayName;

			// Token: 0x04000F54 RID: 3924
			private string internal_name;
		}

		// Token: 0x02000245 RID: 581
		private class Internal : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001B45 RID: 6981 RVA: 0x00067080 File Offset: 0x00065280
			internal Internal(string internalName)
			{
				this.internalName = internalName;
				this.display_name = null;
			}

			// Token: 0x06001B46 RID: 6982 RVA: 0x00067096 File Offset: 0x00065296
			internal Internal(string nameSpaceInternal, TypeIdentifier typeName)
			{
				this.internalName = nameSpaceInternal + "." + typeName.InternalName;
				this.display_name = null;
			}

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000670BC File Offset: 0x000652BC
			public override string DisplayName
			{
				get
				{
					if (this.display_name == null)
					{
						this.display_name = this.GetDisplayName();
					}
					return this.display_name;
				}
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000670D8 File Offset: 0x000652D8
			public string InternalName
			{
				get
				{
					return this.internalName;
				}
			}

			// Token: 0x06001B49 RID: 6985 RVA: 0x000670E0 File Offset: 0x000652E0
			private string GetDisplayName()
			{
				return TypeSpec.EscapeDisplayName(this.internalName);
			}

			// Token: 0x06001B4A RID: 6986 RVA: 0x00067063 File Offset: 0x00065263
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000F55 RID: 3925
			private string internalName;

			// Token: 0x04000F56 RID: 3926
			private string display_name;
		}

		// Token: 0x02000246 RID: 582
		private class NoEscape : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001B4B RID: 6987 RVA: 0x000670ED File Offset: 0x000652ED
			internal NoEscape(string simpleName)
			{
				this.simpleName = simpleName;
			}

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000670FC File Offset: 0x000652FC
			public override string DisplayName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06001B4D RID: 6989 RVA: 0x000670FC File Offset: 0x000652FC
			public string InternalName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x06001B4E RID: 6990 RVA: 0x00067063 File Offset: 0x00065263
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000F57 RID: 3927
			private string simpleName;
		}
	}
}
