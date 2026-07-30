using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D1 RID: 465
	internal class StyleVariableResolver
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00036D82 File Offset: 0x00034F82
		public List<StylePropertyValue> resolvedValues
		{
			get
			{
				return this.m_ResolvedValues;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00036D8A File Offset: 0x00034F8A
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00036D92 File Offset: 0x00034F92
		public StyleVariableContext variableContext { get; set; }

		// Token: 0x06000E9B RID: 3739 RVA: 0x00036D9B File Offset: 0x00034F9B
		public void Init(StyleProperty property, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_ResolvedValues.Clear();
			this.m_Sheet = sheet;
			this.m_Property = property;
			this.m_Handles = handles;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00036DC0 File Offset: 0x00034FC0
		public void AddValue(StyleValueHandle handle)
		{
			this.m_ResolvedValues.Add(new StylePropertyValue
			{
				sheet = this.m_Sheet,
				handle = handle
			});
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00036DF8 File Offset: 0x00034FF8
		public StyleVariableResolver.Result ResolveVarFunction(ref int index)
		{
			this.m_ResolvedVarStack.Clear();
			this.m_ValidationExpression = null;
			bool flag = !this.m_Property.isCustomProperty;
			if (flag)
			{
				string text;
				bool flag2 = !StylePropertyCache.TryGetSyntax(this.m_Property.name, out text);
				if (flag2)
				{
					Debug.LogAssertion("Unknown style property " + this.m_Property.name);
					return StyleVariableResolver.Result.Invalid;
				}
				this.m_ValidationExpression = StyleVariableResolver.s_SyntaxParser.Parse(text);
			}
			int num;
			string text2;
			StyleVariableResolver.ParseVarFunction(this.m_Sheet, this.m_Handles, ref index, out num, out text2);
			StyleVariableResolver.Result result = this.ResolveVariable(text2);
			bool flag3 = result > StyleVariableResolver.Result.Valid;
			if (flag3)
			{
				bool flag4 = result == StyleVariableResolver.Result.NotFound && num > 1 && !this.m_Property.isCustomProperty;
				if (flag4)
				{
					StyleValueHandle[] handles = this.m_Handles;
					int num2 = index + 1;
					index = num2;
					StyleValueHandle styleValueHandle = handles[num2];
					Debug.Assert(styleValueHandle.valueType == StyleValueType.FunctionSeparator, string.Format("Unexpected value type {0} in var function", styleValueHandle.valueType));
					bool flag5 = styleValueHandle.valueType == StyleValueType.FunctionSeparator && index + 1 < this.m_Handles.Length;
					if (flag5)
					{
						index++;
						result = this.ResolveFallback(ref index);
					}
				}
				else
				{
					this.m_ResolvedValues.Clear();
				}
			}
			return result;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00036F58 File Offset: 0x00035158
		private StyleVariableResolver.Result ResolveVariable(string variableName)
		{
			StyleVariable styleVariable;
			bool flag = !this.variableContext.TryFindVariable(variableName, out styleVariable);
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.NotFound;
			}
			else
			{
				bool flag2 = this.m_ResolvedVarStack.Contains(styleVariable.name);
				if (flag2)
				{
					styleVariable = default(StyleVariable);
					result = StyleVariableResolver.Result.NotFound;
				}
				else
				{
					this.m_ResolvedVarStack.Push(styleVariable.name);
					StyleVariableResolver.Result result2 = StyleVariableResolver.Result.Valid;
					int num = 0;
					while (num < styleVariable.handles.Length && result2 == StyleVariableResolver.Result.Valid)
					{
						StyleValueHandle styleValueHandle = styleVariable.handles[num];
						bool flag3 = styleValueHandle.IsVarFunction();
						if (flag3)
						{
							int num2;
							string text;
							StyleVariableResolver.ParseVarFunction(styleVariable.sheet, styleVariable.handles, ref num, out num2, out text);
							result2 = this.ResolveVariable(text);
						}
						else
						{
							StylePropertyValue stylePropertyValue = new StylePropertyValue
							{
								sheet = styleVariable.sheet,
								handle = styleValueHandle
							};
							result2 = this.ValidateResolve(stylePropertyValue);
						}
						num++;
					}
					this.m_ResolvedVarStack.Pop();
					result = result2;
				}
			}
			return result;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00037068 File Offset: 0x00035268
		private StyleVariableResolver.Result ValidateResolve(StylePropertyValue spv)
		{
			bool flag = this.m_ResolvedValues.Count + 1 > 100;
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.Invalid;
			}
			else
			{
				this.m_ResolvedValues.Add(spv);
				bool isCustomProperty = this.m_Property.isCustomProperty;
				if (isCustomProperty)
				{
					result = StyleVariableResolver.Result.Valid;
				}
				else
				{
					MatchResult matchResult = this.m_Matcher.Match(this.m_ValidationExpression, this.m_ResolvedValues);
					bool flag2 = !matchResult.success;
					if (flag2)
					{
						this.m_ResolvedValues.RemoveAt(this.m_ResolvedValues.Count - 1);
					}
					result = (matchResult.success ? StyleVariableResolver.Result.Valid : StyleVariableResolver.Result.Invalid);
				}
			}
			return result;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00037104 File Offset: 0x00035304
		private StyleVariableResolver.Result ResolveFallback(ref int index)
		{
			StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
			while (index < this.m_Handles.Length && result == StyleVariableResolver.Result.Valid)
			{
				StyleValueHandle styleValueHandle = this.m_Handles[index];
				bool flag = styleValueHandle.IsVarFunction();
				if (flag)
				{
					int num;
					string text;
					StyleVariableResolver.ParseVarFunction(this.m_Sheet, this.m_Handles, ref index, out num, out text);
					result = this.ResolveVariable(text);
					bool flag2 = result == StyleVariableResolver.Result.NotFound;
					if (flag2)
					{
						bool flag3 = num > 1;
						if (flag3)
						{
							StyleValueHandle[] handles = this.m_Handles;
							int num2 = index + 1;
							index = num2;
							styleValueHandle = handles[num2];
							Debug.Assert(styleValueHandle.valueType == StyleValueType.FunctionSeparator, string.Format("Unexpected value type {0} in var function", styleValueHandle.valueType));
							bool flag4 = styleValueHandle.valueType == StyleValueType.FunctionSeparator && index + 1 < this.m_Handles.Length;
							if (flag4)
							{
								index++;
								result = this.ResolveFallback(ref index);
							}
						}
					}
				}
				else
				{
					StylePropertyValue stylePropertyValue = new StylePropertyValue
					{
						sheet = this.m_Sheet,
						handle = styleValueHandle
					};
					result = this.ValidateResolve(stylePropertyValue);
				}
				index++;
			}
			return result;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00037238 File Offset: 0x00035438
		private static void ParseVarFunction(StyleSheet sheet, StyleValueHandle[] handles, ref int index, out int argCount, out string variableName)
		{
			int num = index + 1;
			index = num;
			argCount = (int)sheet.ReadFloat(handles[num]);
			num = index + 1;
			index = num;
			variableName = sheet.ReadVariable(handles[num]);
		}

		// Token: 0x040005E7 RID: 1511
		internal const int kMaxResolves = 100;

		// Token: 0x040005E8 RID: 1512
		private static StyleSyntaxParser s_SyntaxParser = new StyleSyntaxParser();

		// Token: 0x040005E9 RID: 1513
		private StylePropertyValueMatcher m_Matcher = new StylePropertyValueMatcher();

		// Token: 0x040005EA RID: 1514
		private List<StylePropertyValue> m_ResolvedValues = new List<StylePropertyValue>();

		// Token: 0x040005EB RID: 1515
		private Stack<string> m_ResolvedVarStack = new Stack<string>();

		// Token: 0x040005EC RID: 1516
		private Expression m_ValidationExpression;

		// Token: 0x040005ED RID: 1517
		private StyleProperty m_Property;

		// Token: 0x040005EE RID: 1518
		private StyleSheet m_Sheet;

		// Token: 0x040005EF RID: 1519
		private StyleValueHandle[] m_Handles;

		// Token: 0x020001D2 RID: 466
		public enum Result
		{
			// Token: 0x040005F2 RID: 1522
			Valid,
			// Token: 0x040005F3 RID: 1523
			Invalid,
			// Token: 0x040005F4 RID: 1524
			NotFound
		}
	}
}
