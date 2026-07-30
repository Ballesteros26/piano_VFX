using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	/// <summary>Provides static methods to convert input text into names for code entities.</summary>
	// Token: 0x020002D1 RID: 721
	public class CodeIdentifier
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.CodeIdentifier" /> class. </summary>
		// Token: 0x06001B1E RID: 6942 RVA: 0x000020FD File Offset: 0x000002FD
		[Obsolete("This class should never get constructed as it contains only static methods.")]
		public CodeIdentifier()
		{
		}

		/// <summary>Produces a Pascal-case string from an input string. </summary>
		/// <returns>A Pascal-case version of the parameter string.</returns>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		// Token: 0x06001B1F RID: 6943 RVA: 0x00096A40 File Offset: 0x00094C40
		public static string MakePascal(string identifier)
		{
			identifier = CodeIdentifier.MakeValid(identifier);
			if (identifier.Length <= 2)
			{
				return identifier.ToUpper(CultureInfo.InvariantCulture);
			}
			if (char.IsLower(identifier[0]))
			{
				return char.ToUpper(identifier[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + identifier.Substring(1);
			}
			return identifier;
		}

		/// <summary>Produces a camel-case string from an input string. </summary>
		/// <returns>A camel-case version of the parameter string.</returns>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		// Token: 0x06001B20 RID: 6944 RVA: 0x00096AA4 File Offset: 0x00094CA4
		public static string MakeCamel(string identifier)
		{
			identifier = CodeIdentifier.MakeValid(identifier);
			if (identifier.Length <= 2)
			{
				return identifier.ToLower(CultureInfo.InvariantCulture);
			}
			if (char.IsUpper(identifier[0]))
			{
				return char.ToLower(identifier[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + identifier.Substring(1);
			}
			return identifier;
		}

		/// <summary>Produces a valid code entity name from an input string. </summary>
		/// <returns>A string that can be used as a code identifier, such as the name of a method parameter.</returns>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		// Token: 0x06001B21 RID: 6945 RVA: 0x00096B08 File Offset: 0x00094D08
		public static string MakeValid(string identifier)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < identifier.Length && stringBuilder.Length < 511)
			{
				char c = identifier[num];
				if (CodeIdentifier.IsValid(c))
				{
					if (stringBuilder.Length == 0 && !CodeIdentifier.IsValidStart(c))
					{
						stringBuilder.Append("Item");
					}
					stringBuilder.Append(c);
				}
				num++;
			}
			if (stringBuilder.Length == 0)
			{
				return "Item";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00096B81 File Offset: 0x00094D81
		internal static string MakeValidInternal(string identifier)
		{
			if (identifier.Length > 30)
			{
				return "Item";
			}
			return CodeIdentifier.MakeValid(identifier);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00096B99 File Offset: 0x00094D99
		private static bool IsValidStart(char c)
		{
			return char.GetUnicodeCategory(c) != UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00096BA8 File Offset: 0x00094DA8
		private static bool IsValid(char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.ConnectorPunctuation:
				return true;
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.LetterNumber:
			case UnicodeCategory.OtherNumber:
			case UnicodeCategory.SpaceSeparator:
			case UnicodeCategory.LineSeparator:
			case UnicodeCategory.ParagraphSeparator:
			case UnicodeCategory.Control:
			case UnicodeCategory.Format:
			case UnicodeCategory.Surrogate:
			case UnicodeCategory.PrivateUse:
			case UnicodeCategory.DashPunctuation:
			case UnicodeCategory.OpenPunctuation:
			case UnicodeCategory.ClosePunctuation:
			case UnicodeCategory.InitialQuotePunctuation:
			case UnicodeCategory.FinalQuotePunctuation:
			case UnicodeCategory.OtherPunctuation:
			case UnicodeCategory.MathSymbol:
			case UnicodeCategory.CurrencySymbol:
			case UnicodeCategory.ModifierSymbol:
			case UnicodeCategory.OtherSymbol:
			case UnicodeCategory.OtherNotAssigned:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00096C41 File Offset: 0x00094E41
		internal static void CheckValidIdentifier(string ident)
		{
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(ident))
			{
				throw new ArgumentException(Res.GetString("Identifier '{0}' is not CLS-compliant.", new object[] { ident }), "ident");
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00096C6A File Offset: 0x00094E6A
		internal static string GetCSharpName(string name)
		{
			return CodeIdentifier.EscapeKeywords(name.Replace('+', '.'), CodeIdentifier.csharp);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00096C80 File Offset: 0x00094E80
		private static int GetCSharpName(Type t, Type[] parameters, int index, StringBuilder sb)
		{
			if (t.DeclaringType != null && t.DeclaringType != t)
			{
				index = CodeIdentifier.GetCSharpName(t.DeclaringType, parameters, index, sb);
				sb.Append(".");
			}
			string name = t.Name;
			int num = name.IndexOf('`');
			if (num < 0)
			{
				num = name.IndexOf('!');
			}
			if (num > 0)
			{
				CodeIdentifier.EscapeKeywords(name.Substring(0, num), CodeIdentifier.csharp, sb);
				sb.Append("<");
				int num2 = int.Parse(name.Substring(num + 1), CultureInfo.InvariantCulture) + index;
				while (index < num2)
				{
					sb.Append(CodeIdentifier.GetCSharpName(parameters[index]));
					if (index < num2 - 1)
					{
						sb.Append(",");
					}
					index++;
				}
				sb.Append(">");
			}
			else
			{
				CodeIdentifier.EscapeKeywords(name, CodeIdentifier.csharp, sb);
			}
			return index;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00096D64 File Offset: 0x00094F64
		internal static string GetCSharpName(Type t)
		{
			int num = 0;
			while (t.IsArray)
			{
				t = t.GetElementType();
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("global::");
			string @namespace = t.Namespace;
			if (@namespace != null && @namespace.Length > 0)
			{
				string[] array = @namespace.Split(new char[] { '.' });
				for (int i = 0; i < array.Length; i++)
				{
					CodeIdentifier.EscapeKeywords(array[i], CodeIdentifier.csharp, stringBuilder);
					stringBuilder.Append(".");
				}
			}
			Type[] array2 = ((t.IsGenericType || t.ContainsGenericParameters) ? t.GetGenericArguments() : new Type[0]);
			CodeIdentifier.GetCSharpName(t, array2, 0, stringBuilder);
			for (int j = 0; j < num; j++)
			{
				stringBuilder.Append("[]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00096E3C File Offset: 0x0009503C
		private static void EscapeKeywords(string identifier, CodeDomProvider codeProvider, StringBuilder sb)
		{
			if (identifier == null || identifier.Length == 0)
			{
				return;
			}
			int num = 0;
			while (identifier.EndsWith("[]", StringComparison.Ordinal))
			{
				num++;
				identifier = identifier.Substring(0, identifier.Length - 2);
			}
			if (identifier.Length > 0)
			{
				CodeIdentifier.CheckValidIdentifier(identifier);
				identifier = codeProvider.CreateEscapedIdentifier(identifier);
				sb.Append(identifier);
			}
			for (int i = 0; i < num; i++)
			{
				sb.Append("[]");
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00096EB4 File Offset: 0x000950B4
		private static string EscapeKeywords(string identifier, CodeDomProvider codeProvider)
		{
			if (identifier == null || identifier.Length == 0)
			{
				return identifier;
			}
			string[] array = identifier.Split(new char[] { '.', ',', '<', '>' });
			StringBuilder stringBuilder = new StringBuilder();
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (num >= 0)
				{
					stringBuilder.Append(identifier.Substring(num, 1));
				}
				num++;
				num += array[i].Length;
				CodeIdentifier.EscapeKeywords(array[i].Trim(), codeProvider, stringBuilder);
			}
			if (stringBuilder.Length != identifier.Length)
			{
				return stringBuilder.ToString();
			}
			return identifier;
		}

		// Token: 0x040015CB RID: 5579
		internal static CodeDomProvider csharp = new CSharpCodeProvider();

		// Token: 0x040015CC RID: 5580
		internal const int MaxIdentifierLength = 511;
	}
}
