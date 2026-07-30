using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace System
{
	// Token: 0x0200024A RID: 586
	internal class TypeSpec
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x000671F9 File Offset: 0x000653F9
		internal bool HasModifiers
		{
			get
			{
				return this.modifier_spec != null;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00067204 File Offset: 0x00065404
		internal bool IsNested
		{
			get
			{
				return this.nested != null && this.nested.Count > 0;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0006721E File Offset: 0x0006541E
		internal bool IsByRef
		{
			get
			{
				return this.is_byref;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x00067226 File Offset: 0x00065426
		internal TypeName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0006722E File Offset: 0x0006542E
		internal IEnumerable<TypeName> Nested
		{
			get
			{
				if (this.nested != null)
				{
					return this.nested;
				}
				return EmptyArray<TypeName>.Value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x00067244 File Offset: 0x00065444
		internal IEnumerable<ModifierSpec> Modifiers
		{
			get
			{
				if (this.modifier_spec != null)
				{
					return this.modifier_spec;
				}
				return EmptyArray<ModifierSpec>.Value;
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0006725C File Offset: 0x0006545C
		private string GetDisplayFullName(TypeSpec.DisplayNameFormat flags)
		{
			bool flag = (flags & TypeSpec.DisplayNameFormat.WANT_ASSEMBLY) > TypeSpec.DisplayNameFormat.Default;
			bool flag2 = (flags & TypeSpec.DisplayNameFormat.NO_MODIFIERS) == TypeSpec.DisplayNameFormat.Default;
			StringBuilder stringBuilder = new StringBuilder(this.name.DisplayName);
			if (this.nested != null)
			{
				foreach (TypeIdentifier typeIdentifier in this.nested)
				{
					stringBuilder.Append('+').Append(typeIdentifier.DisplayName);
				}
			}
			if (this.generic_params != null)
			{
				stringBuilder.Append('[');
				for (int i = 0; i < this.generic_params.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					if (this.generic_params[i].assembly_name != null)
					{
						stringBuilder.Append('[').Append(this.generic_params[i].DisplayFullName).Append(']');
					}
					else
					{
						stringBuilder.Append(this.generic_params[i].DisplayFullName);
					}
				}
				stringBuilder.Append(']');
			}
			if (flag2)
			{
				this.GetModifierString(stringBuilder);
			}
			if (this.assembly_name != null && flag)
			{
				stringBuilder.Append(", ").Append(this.assembly_name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000673B8 File Offset: 0x000655B8
		internal string ModifierString()
		{
			return this.GetModifierString(new StringBuilder()).ToString();
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x000673CC File Offset: 0x000655CC
		private StringBuilder GetModifierString(StringBuilder sb)
		{
			if (this.modifier_spec != null)
			{
				foreach (ModifierSpec modifierSpec in this.modifier_spec)
				{
					modifierSpec.Append(sb);
				}
			}
			if (this.is_byref)
			{
				sb.Append('&');
			}
			return sb;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00067438 File Offset: 0x00065638
		internal string DisplayFullName
		{
			get
			{
				if (this.display_fullname == null)
				{
					this.display_fullname = this.GetDisplayFullName(TypeSpec.DisplayNameFormat.Default);
				}
				return this.display_fullname;
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00067458 File Offset: 0x00065658
		internal static TypeSpec Parse(string typeName)
		{
			int num = 0;
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			TypeSpec typeSpec = TypeSpec.Parse(typeName, ref num, false, true);
			if (num < typeName.Length)
			{
				throw new ArgumentException("Count not parse the whole type name", "typeName");
			}
			return typeSpec;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00067498 File Offset: 0x00065698
		internal static string EscapeDisplayName(string internalName)
		{
			StringBuilder stringBuilder = new StringBuilder(internalName.Length);
			int i = 0;
			while (i < internalName.Length)
			{
				char c = internalName[i];
				switch (c)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					goto IL_0056;
				case '\'':
				case '(':
				case ')':
					goto IL_0067;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						goto IL_0056;
					default:
						goto IL_0067;
					}
					break;
				}
				IL_006F:
				i++;
				continue;
				IL_0056:
				stringBuilder.Append('\\').Append(c);
				goto IL_006F;
				IL_0067:
				stringBuilder.Append(c);
				goto IL_006F;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00067528 File Offset: 0x00065728
		internal static string UnescapeInternalName(string displayName)
		{
			StringBuilder stringBuilder = new StringBuilder(displayName.Length);
			for (int i = 0; i < displayName.Length; i++)
			{
				char c = displayName[i];
				if (c == '\\' && ++i < displayName.Length)
				{
					c = displayName[i];
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00067584 File Offset: 0x00065784
		internal static bool NeedsEscaping(string internalName)
		{
			foreach (char c in internalName)
			{
				switch (c)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					return true;
				case '\'':
				case '(':
				case ')':
					break;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000675EC File Offset: 0x000657EC
		internal Type Resolve(Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
		{
			Assembly assembly = null;
			if (assemblyResolver == null && typeResolver == null)
			{
				return Type.GetType(this.DisplayFullName, throwOnError, ignoreCase);
			}
			if (this.assembly_name != null)
			{
				if (assemblyResolver != null)
				{
					assembly = assemblyResolver(new AssemblyName(this.assembly_name));
				}
				else
				{
					assembly = Assembly.Load(this.assembly_name);
				}
				if (assembly == null)
				{
					if (throwOnError)
					{
						throw new FileNotFoundException("Could not resolve assembly '" + this.assembly_name + "'");
					}
					return null;
				}
			}
			Type type = null;
			if (typeResolver != null)
			{
				type = typeResolver(assembly, this.name.DisplayName, ignoreCase);
			}
			else
			{
				type = assembly.GetType(this.name.DisplayName, false, ignoreCase);
			}
			if (!(type == null))
			{
				if (this.nested != null)
				{
					foreach (TypeIdentifier typeIdentifier in this.nested)
					{
						Type nestedType = type.GetNestedType(typeIdentifier.DisplayName, BindingFlags.Public | BindingFlags.NonPublic);
						if (nestedType == null)
						{
							if (throwOnError)
							{
								throw new TypeLoadException("Could not resolve type '" + typeIdentifier + "'");
							}
							return null;
						}
						else
						{
							type = nestedType;
						}
					}
				}
				if (this.generic_params != null)
				{
					Type[] array = new Type[this.generic_params.Count];
					int i = 0;
					while (i < array.Length)
					{
						Type type2 = this.generic_params[i].Resolve(assemblyResolver, typeResolver, throwOnError, ignoreCase);
						if (type2 == null)
						{
							if (throwOnError)
							{
								throw new TypeLoadException("Could not resolve type '" + this.generic_params[i].name + "'");
							}
							return null;
						}
						else
						{
							array[i] = type2;
							i++;
						}
					}
					type = type.MakeGenericType(array);
				}
				if (this.modifier_spec != null)
				{
					foreach (ModifierSpec modifierSpec in this.modifier_spec)
					{
						type = modifierSpec.Resolve(type);
					}
				}
				if (this.is_byref)
				{
					type = type.MakeByRefType();
				}
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException("Could not resolve type '" + this.name + "'");
			}
			return null;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00067830 File Offset: 0x00065A30
		private void AddName(string type_name)
		{
			if (this.name == null)
			{
				this.name = TypeSpec.ParsedTypeIdentifier(type_name);
				return;
			}
			if (this.nested == null)
			{
				this.nested = new List<TypeIdentifier>();
			}
			this.nested.Add(TypeSpec.ParsedTypeIdentifier(type_name));
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0006786B File Offset: 0x00065A6B
		private void AddModifier(ModifierSpec md)
		{
			if (this.modifier_spec == null)
			{
				this.modifier_spec = new List<ModifierSpec>();
			}
			this.modifier_spec.Add(md);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0006788C File Offset: 0x00065A8C
		private static void SkipSpace(string name, ref int pos)
		{
			int num = pos;
			while (num < name.Length && char.IsWhiteSpace(name[num]))
			{
				num++;
			}
			pos = num;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000678BC File Offset: 0x00065ABC
		private static void BoundCheck(int idx, string s)
		{
			if (idx >= s.Length)
			{
				throw new ArgumentException("Invalid generic arguments spec", "typeName");
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000678D7 File Offset: 0x00065AD7
		private static TypeIdentifier ParsedTypeIdentifier(string displayName)
		{
			return TypeIdentifiers.FromDisplay(displayName);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000678E0 File Offset: 0x00065AE0
		private static TypeSpec Parse(string name, ref int p, bool is_recurse, bool allow_aqn)
		{
			int i = p;
			bool flag = false;
			TypeSpec typeSpec = new TypeSpec();
			TypeSpec.SkipSpace(name, ref i);
			int num = i;
			while (i < name.Length)
			{
				char c = name[i];
				switch (c)
				{
				case '&':
				case '*':
					goto IL_0098;
				case '\'':
				case '(':
				case ')':
					break;
				case '+':
					typeSpec.AddName(name.Substring(num, i - num));
					num = i + 1;
					break;
				case ',':
					goto IL_0077;
				default:
					switch (c)
					{
					case '[':
						goto IL_0098;
					case '\\':
						i++;
						break;
					case ']':
						goto IL_0077;
					}
					break;
				}
				IL_00D6:
				if (!flag)
				{
					i++;
					continue;
				}
				break;
				IL_0077:
				typeSpec.AddName(name.Substring(num, i - num));
				num = i + 1;
				flag = true;
				if (is_recurse && !allow_aqn)
				{
					p = i;
					return typeSpec;
				}
				goto IL_00D6;
				IL_0098:
				if (name[i] != '[' && is_recurse)
				{
					throw new ArgumentException("Generic argument can't be byref or pointer type", "typeName");
				}
				typeSpec.AddName(name.Substring(num, i - num));
				num = i + 1;
				flag = true;
				goto IL_00D6;
			}
			if (num < i)
			{
				typeSpec.AddName(name.Substring(num, i - num));
			}
			else if (num == i)
			{
				typeSpec.AddName(string.Empty);
			}
			if (flag)
			{
				while (i < name.Length)
				{
					char c = name[i];
					if (c <= '*')
					{
						if (c != '&')
						{
							if (c != '*')
							{
								goto IL_04BE;
							}
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Can't have a pointer to a byref type", "typeName");
							}
							int num2 = 1;
							while (i + 1 < name.Length && name[i + 1] == '*')
							{
								i++;
								num2++;
							}
							typeSpec.AddModifier(new PointerSpec(num2));
						}
						else
						{
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Can't have a byref of a byref", "typeName");
							}
							typeSpec.is_byref = true;
						}
					}
					else if (c != ',')
					{
						if (c != '[')
						{
							if (c != ']')
							{
								goto IL_04BE;
							}
							if (is_recurse)
							{
								p = i;
								return typeSpec;
							}
							throw new ArgumentException("Unmatched ']'", "typeName");
						}
						else
						{
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Byref qualifier must be the last one of a type", "typeName");
							}
							i++;
							if (i >= name.Length)
							{
								throw new ArgumentException("Invalid array/generic spec", "typeName");
							}
							TypeSpec.SkipSpace(name, ref i);
							if (name[i] != ',' && name[i] != '*' && name[i] != ']')
							{
								List<TypeSpec> list = new List<TypeSpec>();
								if (typeSpec.HasModifiers)
								{
									throw new ArgumentException("generic args after array spec or pointer type", "typeName");
								}
								while (i < name.Length)
								{
									TypeSpec.SkipSpace(name, ref i);
									bool flag2 = name[i] == '[';
									if (flag2)
									{
										i++;
									}
									list.Add(TypeSpec.Parse(name, ref i, true, flag2));
									TypeSpec.BoundCheck(i, name);
									if (flag2)
									{
										if (name[i] != ']')
										{
											throw new ArgumentException("Unclosed assembly-qualified type name at " + name[i].ToString(), "typeName");
										}
										i++;
										TypeSpec.BoundCheck(i, name);
									}
									if (name[i] == ']')
									{
										break;
									}
									if (name[i] != ',')
									{
										throw new ArgumentException("Invalid generic arguments separator " + name[i].ToString(), "typeName");
									}
									i++;
								}
								if (i >= name.Length || name[i] != ']')
								{
									throw new ArgumentException("Error parsing generic params spec", "typeName");
								}
								typeSpec.generic_params = list;
							}
							else
							{
								int num3 = 1;
								bool flag3 = false;
								while (i < name.Length && name[i] != ']')
								{
									if (name[i] == '*')
									{
										if (flag3)
										{
											throw new ArgumentException("Array spec cannot have 2 bound dimensions", "typeName");
										}
										flag3 = true;
									}
									else
									{
										if (name[i] != ',')
										{
											throw new ArgumentException("Invalid character in array spec " + name[i].ToString(), "typeName");
										}
										num3++;
									}
									i++;
									TypeSpec.SkipSpace(name, ref i);
								}
								if (i >= name.Length || name[i] != ']')
								{
									throw new ArgumentException("Error parsing array spec", "typeName");
								}
								if (num3 > 1 && flag3)
								{
									throw new ArgumentException("Invalid array spec, multi-dimensional array cannot be bound", "typeName");
								}
								typeSpec.AddModifier(new ArraySpec(num3, flag3));
							}
						}
					}
					else if (is_recurse && allow_aqn)
					{
						int num4 = i;
						while (num4 < name.Length && name[num4] != ']')
						{
							num4++;
						}
						if (num4 >= name.Length)
						{
							throw new ArgumentException("Unmatched ']' while parsing generic argument assembly name");
						}
						typeSpec.assembly_name = name.Substring(i + 1, num4 - i - 1).Trim();
						p = num4;
						return typeSpec;
					}
					else
					{
						if (is_recurse)
						{
							p = i;
							return typeSpec;
						}
						if (allow_aqn)
						{
							typeSpec.assembly_name = name.Substring(i + 1).Trim();
							i = name.Length;
						}
					}
					i++;
					continue;
					IL_04BE:
					throw new ArgumentException(string.Concat(new object[]
					{
						"Bad type def, can't handle '",
						name[i].ToString(),
						"' at ",
						i
					}), "typeName");
				}
			}
			p = i;
			return typeSpec;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00067E01 File Offset: 0x00066001
		internal TypeName TypeNameWithoutModifiers()
		{
			return new TypeSpec.TypeSpecTypeName(this, false);
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00067E0A File Offset: 0x0006600A
		internal TypeName TypeName
		{
			get
			{
				return new TypeSpec.TypeSpecTypeName(this, true);
			}
		}

		// Token: 0x04000F5B RID: 3931
		private TypeIdentifier name;

		// Token: 0x04000F5C RID: 3932
		private string assembly_name;

		// Token: 0x04000F5D RID: 3933
		private List<TypeIdentifier> nested;

		// Token: 0x04000F5E RID: 3934
		private List<TypeSpec> generic_params;

		// Token: 0x04000F5F RID: 3935
		private List<ModifierSpec> modifier_spec;

		// Token: 0x04000F60 RID: 3936
		private bool is_byref;

		// Token: 0x04000F61 RID: 3937
		private string display_fullname;

		// Token: 0x0200024B RID: 587
		[Flags]
		internal enum DisplayNameFormat
		{
			// Token: 0x04000F63 RID: 3939
			Default = 0,
			// Token: 0x04000F64 RID: 3940
			WANT_ASSEMBLY = 1,
			// Token: 0x04000F65 RID: 3941
			NO_MODIFIERS = 2
		}

		// Token: 0x0200024C RID: 588
		private class TypeSpecTypeName : TypeNames.ATypeName, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001B73 RID: 7027 RVA: 0x00067E13 File Offset: 0x00066013
			internal TypeSpecTypeName(TypeSpec ts, bool wantModifiers)
			{
				this.ts = ts;
				this.want_modifiers = wantModifiers;
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06001B74 RID: 7028 RVA: 0x00067E29 File Offset: 0x00066029
			public override string DisplayName
			{
				get
				{
					if (this.want_modifiers)
					{
						return this.ts.DisplayFullName;
					}
					return this.ts.GetDisplayFullName(TypeSpec.DisplayNameFormat.NO_MODIFIERS);
				}
			}

			// Token: 0x06001B75 RID: 7029 RVA: 0x00067063 File Offset: 0x00065263
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000F66 RID: 3942
			private TypeSpec ts;

			// Token: 0x04000F67 RID: 3943
			private bool want_modifiers;
		}
	}
}
