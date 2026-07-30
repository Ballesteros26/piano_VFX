using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x020002C8 RID: 712
	internal class CodeGenerator
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x00094926 File Offset: 0x00092B26
		internal static bool IsValidLanguageIndependentIdentifier(string ident)
		{
			return CodeGenerator.IsValidLanguageIndependentIdentifier(ident);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0009492E File Offset: 0x00092B2E
		internal static void ValidateIdentifiers(CodeObject e)
		{
			CodeGenerator.ValidateIdentifiers(e);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00094936 File Offset: 0x00092B36
		internal CodeGenerator(TypeBuilder typeBuilder)
		{
			this.typeBuilder = typeBuilder;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00094969 File Offset: 0x00092B69
		internal static bool IsNullableGenericType(Type type)
		{
			return type.Name == "Nullable`1";
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00002F50 File Offset: 0x00001150
		internal static void AssertHasInterface(Type type, Type iType)
		{
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0009497C File Offset: 0x00092B7C
		internal void BeginMethod(Type returnType, string methodName, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = this.typeBuilder.DefineMethod(methodName, methodAttributes, returnType, argTypes);
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000949CB File Offset: 0x00092BCB
		internal void BeginMethod(Type returnType, MethodBuilderInfo methodBuilderInfo, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = methodBuilderInfo.MethodBuilder;
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00094A08 File Offset: 0x00092C08
		private void InitILGeneration(Type[] argTypes, string[] argNames, bool isStatic)
		{
			this.methodEndLabel = this.ilGen.DefineLabel();
			this.retLabel = this.ilGen.DefineLabel();
			this.blockStack = new Stack();
			this.whileStack = new Stack();
			this.currentScope = new LocalScope();
			this.freeLocals = new Dictionary<Tuple<Type, string>, Queue<LocalBuilder>>();
			this.argList = new Dictionary<string, ArgBuilder>();
			if (!isStatic)
			{
				this.argList.Add("this", new ArgBuilder("this", 0, this.typeBuilder.BaseType));
			}
			for (int i = 0; i < argTypes.Length; i++)
			{
				ArgBuilder argBuilder = new ArgBuilder(argNames[i], this.argList.Count, argTypes[i]);
				this.argList.Add(argBuilder.Name, argBuilder);
				this.methodBuilder.DefineParameter(argBuilder.Index, ParameterAttributes.None, argBuilder.Name);
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00094AE8 File Offset: 0x00092CE8
		internal MethodBuilder EndMethod()
		{
			this.MarkLabel(this.methodEndLabel);
			this.Ret();
			MethodBuilder methodBuilder = this.methodBuilder;
			this.methodBuilder = null;
			this.ilGen = null;
			this.freeLocals = null;
			this.blockStack = null;
			this.whileStack = null;
			this.argList = null;
			this.currentScope = null;
			this.retLocal = null;
			return methodBuilder;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00094B45 File Offset: 0x00092D45
		internal MethodBuilder MethodBuilder
		{
			get
			{
				return this.methodBuilder;
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00094B4D File Offset: 0x00092D4D
		internal static Exception NotSupported(string msg)
		{
			return new NotSupportedException(msg);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00094B55 File Offset: 0x00092D55
		internal ArgBuilder GetArg(string name)
		{
			return this.argList[name];
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00094B63 File Offset: 0x00092D63
		internal LocalBuilder GetLocal(string name)
		{
			return this.currentScope[name];
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00094B71 File Offset: 0x00092D71
		internal LocalBuilder ReturnLocal
		{
			get
			{
				if (this.retLocal == null)
				{
					this.retLocal = this.DeclareLocal(this.methodBuilder.ReturnType, "_ret");
				}
				return this.retLocal;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00094B9D File Offset: 0x00092D9D
		internal Label ReturnLabel
		{
			get
			{
				return this.retLabel;
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00094BA8 File Offset: 0x00092DA8
		internal LocalBuilder GetTempLocal(Type type)
		{
			LocalBuilder localBuilder;
			if (!this.TmpLocals.TryGetValue(type, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, "_tmp" + this.TmpLocals.Count);
				this.TmpLocals.Add(type, localBuilder);
			}
			return localBuilder;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00094BF5 File Offset: 0x00092DF5
		internal Type GetVariableType(object var)
		{
			if (var is ArgBuilder)
			{
				return ((ArgBuilder)var).ArgType;
			}
			if (var is LocalBuilder)
			{
				return ((LocalBuilder)var).LocalType;
			}
			return var.GetType();
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00094C28 File Offset: 0x00092E28
		internal object GetVariable(string name)
		{
			object obj;
			if (this.TryGetVariable(name, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00094C44 File Offset: 0x00092E44
		internal bool TryGetVariable(string name, out object variable)
		{
			LocalBuilder localBuilder;
			if (this.currentScope != null && this.currentScope.TryGetValue(name, out localBuilder))
			{
				variable = localBuilder;
				return true;
			}
			ArgBuilder argBuilder;
			if (this.argList != null && this.argList.TryGetValue(name, out argBuilder))
			{
				variable = argBuilder;
				return true;
			}
			int num;
			if (int.TryParse(name, out num))
			{
				variable = num;
				return true;
			}
			variable = null;
			return false;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00094CA4 File Offset: 0x00092EA4
		internal void EnterScope()
		{
			LocalScope localScope = new LocalScope(this.currentScope);
			this.currentScope = localScope;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00094CC4 File Offset: 0x00092EC4
		internal void ExitScope()
		{
			this.currentScope.AddToFreeLocals(this.freeLocals);
			this.currentScope = this.currentScope.parent;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00094CE8 File Offset: 0x00092EE8
		private bool TryDequeueLocal(Type type, string name, out LocalBuilder local)
		{
			Tuple<Type, string> tuple = new Tuple<Type, string>(type, name);
			Queue<LocalBuilder> queue;
			if (this.freeLocals.TryGetValue(tuple, out queue))
			{
				local = queue.Dequeue();
				if (queue.Count == 0)
				{
					this.freeLocals.Remove(tuple);
				}
				return true;
			}
			local = null;
			return false;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00094D30 File Offset: 0x00092F30
		internal LocalBuilder DeclareLocal(Type type, string name)
		{
			LocalBuilder localBuilder;
			if (!this.TryDequeueLocal(type, name, out localBuilder))
			{
				localBuilder = this.ilGen.DeclareLocal(type, false);
				if (DiagnosticsSwitches.KeepTempFiles.Enabled)
				{
					localBuilder.SetLocalSymInfo(name);
				}
			}
			this.currentScope[name] = localBuilder;
			return localBuilder;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00094D78 File Offset: 0x00092F78
		internal LocalBuilder DeclareOrGetLocal(Type type, string name)
		{
			LocalBuilder localBuilder;
			if (!this.currentScope.TryGetValue(name, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, name);
			}
			return localBuilder;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00094DA0 File Offset: 0x00092FA0
		internal object For(LocalBuilder local, object start, object end)
		{
			ForState forState = new ForState(local, this.DefineLabel(), this.DefineLabel(), end);
			if (forState.Index != null)
			{
				this.Load(start);
				this.Stloc(forState.Index);
				this.Br(forState.TestLabel);
			}
			this.MarkLabel(forState.BeginLabel);
			this.blockStack.Push(forState);
			return forState;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00094E04 File Offset: 0x00093004
		internal void EndFor()
		{
			ForState forState = this.blockStack.Pop() as ForState;
			if (forState.Index != null)
			{
				this.Ldloc(forState.Index);
				this.Ldc(1);
				this.Add();
				this.Stloc(forState.Index);
				this.MarkLabel(forState.TestLabel);
				this.Ldloc(forState.Index);
				this.Load(forState.End);
				if (this.GetVariableType(forState.End).IsArray)
				{
					this.Ldlen();
				}
				else
				{
					MethodInfo method = typeof(ICollection).GetMethod("get_Count", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.Call(method);
				}
				this.Blt(forState.BeginLabel);
				return;
			}
			this.Br(forState.BeginLabel);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00094ED2 File Offset: 0x000930D2
		internal void If()
		{
			this.InternalIf(false);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00094EDB File Offset: 0x000930DB
		internal void IfNot()
		{
			this.InternalIf(true);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00094EE4 File Offset: 0x000930E4
		private OpCode GetBranchCode(Cmp cmp)
		{
			return CodeGenerator.BranchCodes[(int)cmp];
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00094EF4 File Offset: 0x000930F4
		internal void If(Cmp cmpOp)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00094F43 File Offset: 0x00093143
		internal void If(object value1, Cmp cmpOp, object value2)
		{
			this.Load(value1);
			this.Load(value2);
			this.If(cmpOp);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00094F5C File Offset: 0x0009315C
		internal void Else()
		{
			IfState ifState = this.PopIfState();
			this.Br(ifState.EndIf);
			this.MarkLabel(ifState.ElseBegin);
			ifState.ElseBegin = ifState.EndIf;
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00094FA0 File Offset: 0x000931A0
		internal void EndIf()
		{
			IfState ifState = this.PopIfState();
			if (!ifState.ElseBegin.Equals(ifState.EndIf))
			{
				this.MarkLabel(ifState.ElseBegin);
			}
			this.MarkLabel(ifState.EndIf);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00094FE2 File Offset: 0x000931E2
		internal void BeginExceptionBlock()
		{
			this.leaveLabels.Push(this.DefineLabel());
			this.ilGen.BeginExceptionBlock();
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00095006 File Offset: 0x00093206
		internal void BeginCatchBlock(Type exception)
		{
			this.ilGen.BeginCatchBlock(exception);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00095014 File Offset: 0x00093214
		internal void EndExceptionBlock()
		{
			this.ilGen.EndExceptionBlock();
			this.ilGen.MarkLabel((Label)this.leaveLabels.Pop());
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0009503C File Offset: 0x0009323C
		internal void Leave()
		{
			this.ilGen.Emit(OpCodes.Leave, (Label)this.leaveLabels.Peek());
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0009505E File Offset: 0x0009325E
		internal void Call(MethodInfo methodInfo)
		{
			if (methodInfo.IsVirtual && !methodInfo.DeclaringType.IsValueType)
			{
				this.ilGen.Emit(OpCodes.Callvirt, methodInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Call, methodInfo);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00095098 File Offset: 0x00093298
		internal void Call(ConstructorInfo ctor)
		{
			this.ilGen.Emit(OpCodes.Call, ctor);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000950AB File Offset: 0x000932AB
		internal void New(ConstructorInfo constructorInfo)
		{
			this.ilGen.Emit(OpCodes.Newobj, constructorInfo);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000950BE File Offset: 0x000932BE
		internal void InitObj(Type valueType)
		{
			this.ilGen.Emit(OpCodes.Initobj, valueType);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000950D1 File Offset: 0x000932D1
		internal void NewArray(Type elementType, object len)
		{
			this.Load(len);
			this.ilGen.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000950EC File Offset: 0x000932EC
		internal void LoadArrayElement(object obj, object arrayIndex)
		{
			Type elementType = this.GetVariableType(obj).GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
				this.Ldobj(elementType);
				return;
			}
			this.Ldelem(elementType);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00095134 File Offset: 0x00093334
		internal void StoreArrayElement(object obj, object arrayIndex, object value)
		{
			Type variableType = this.GetVariableType(obj);
			if (variableType == typeof(Array))
			{
				this.Load(obj);
				this.Call(typeof(Array).GetMethod("SetValue", new Type[]
				{
					typeof(object),
					typeof(int)
				}));
				return;
			}
			Type elementType = variableType.GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
			}
			this.Load(value);
			this.ConvertValue(this.GetVariableType(value), elementType);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Stobj(elementType);
				return;
			}
			this.Stelem(elementType);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000951ED File Offset: 0x000933ED
		private static bool IsStruct(Type objType)
		{
			return objType.IsValueType && !objType.IsPrimitive;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00095202 File Offset: 0x00093402
		internal Type LoadMember(object obj, MemberInfo memberInfo)
		{
			if (this.GetVariableType(obj).IsValueType)
			{
				this.LoadAddress(obj);
			}
			else
			{
				this.Load(obj);
			}
			return this.LoadMember(memberInfo);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0009522C File Offset: 0x0009342C
		private static MethodInfo GetPropertyMethodFromBaseType(PropertyInfo propertyInfo, bool isGetter)
		{
			Type type = propertyInfo.DeclaringType.BaseType;
			string name = propertyInfo.Name;
			MethodInfo methodInfo = null;
			while (type != null)
			{
				PropertyInfo property = type.GetProperty(name);
				if (property != null)
				{
					if (isGetter)
					{
						methodInfo = property.GetGetMethod(true);
					}
					else
					{
						methodInfo = property.GetSetMethod(true);
					}
					if (methodInfo != null)
					{
						break;
					}
				}
				type = type.BaseType;
			}
			return methodInfo;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00095290 File Offset: 0x00093490
		internal Type LoadMember(MemberInfo memberInfo)
		{
			Type type;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				type = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsfld, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldfld, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				type = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
				}
			}
			return type;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0009531C File Offset: 0x0009351C
		internal Type LoadMemberAddress(MemberInfo memberInfo)
		{
			Type type;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				type = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsflda, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldflda, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				type = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
					LocalBuilder tempLocal = this.GetTempLocal(type);
					this.Stloc(tempLocal);
					this.Ldloca(tempLocal);
				}
			}
			return type;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000953C4 File Offset: 0x000935C4
		internal void StoreMember(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType != MemberTypes.Field)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetSetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, false);
					}
					this.Call(methodInfo);
				}
				return;
			}
			FieldInfo fieldInfo = (FieldInfo)memberInfo;
			if (fieldInfo.IsStatic)
			{
				this.ilGen.Emit(OpCodes.Stsfld, fieldInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Stfld, fieldInfo);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00095440 File Offset: 0x00093640
		internal void Load(object obj)
		{
			if (obj == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			if (obj is ArgBuilder)
			{
				this.Ldarg((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.Ldloc((LocalBuilder)obj);
				return;
			}
			this.Ldc(obj);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00095492 File Offset: 0x00093692
		internal void LoadAddress(object obj)
		{
			if (obj is ArgBuilder)
			{
				this.LdargAddress((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.LdlocAddress((LocalBuilder)obj);
				return;
			}
			this.Load(obj);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000954C5 File Offset: 0x000936C5
		internal void ConvertAddress(Type source, Type target)
		{
			this.InternalConvert(source, target, true);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000954D0 File Offset: 0x000936D0
		internal void ConvertValue(Type source, Type target)
		{
			this.InternalConvert(source, target, false);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000954DB File Offset: 0x000936DB
		internal void Castclass(Type target)
		{
			this.ilGen.Emit(OpCodes.Castclass, target);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000954EE File Offset: 0x000936EE
		internal void Box(Type type)
		{
			this.ilGen.Emit(OpCodes.Box, type);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00095501 File Offset: 0x00093701
		internal void Unbox(Type type)
		{
			this.ilGen.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00095514 File Offset: 0x00093714
		private OpCode GetLdindOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdindOpCodes[(int)typeCode];
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00095524 File Offset: 0x00093724
		internal void Ldobj(Type type)
		{
			OpCode ldindOpCode = this.GetLdindOpCode(Type.GetTypeCode(type));
			if (!ldindOpCode.Equals(OpCodes.Nop))
			{
				this.ilGen.Emit(ldindOpCode);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldobj, type);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0009556A File Offset: 0x0009376A
		internal void Stobj(Type type)
		{
			this.ilGen.Emit(OpCodes.Stobj, type);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0009557D File Offset: 0x0009377D
		internal void Ceq()
		{
			this.ilGen.Emit(OpCodes.Ceq);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0009558F File Offset: 0x0009378F
		internal void Clt()
		{
			this.ilGen.Emit(OpCodes.Clt);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000955A1 File Offset: 0x000937A1
		internal void Cne()
		{
			this.Ceq();
			this.Ldc(0);
			this.Ceq();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000955B6 File Offset: 0x000937B6
		internal void Ble(Label label)
		{
			this.ilGen.Emit(OpCodes.Ble, label);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000955C9 File Offset: 0x000937C9
		internal void Throw()
		{
			this.ilGen.Emit(OpCodes.Throw);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000955DB File Offset: 0x000937DB
		internal void Ldtoken(Type t)
		{
			this.ilGen.Emit(OpCodes.Ldtoken, t);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000955F0 File Offset: 0x000937F0
		internal void Ldc(object o)
		{
			Type type = o.GetType();
			if (o is Type)
			{
				this.Ldtoken((Type)o);
				this.Call(typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(RuntimeTypeHandle) }, null));
				return;
			}
			if (type.IsEnum)
			{
				this.Ldc(((IConvertible)o).ToType(Enum.GetUnderlyingType(type), null));
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.Ldc((bool)o);
				return;
			case TypeCode.Char:
				throw new NotSupportedException("Char is not a valid schema primitive and should be treated as int in DataContract");
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				this.Ldc(((IConvertible)o).ToInt32(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int32:
				this.Ldc((int)o);
				return;
			case TypeCode.UInt32:
				this.Ldc((int)((uint)o));
				return;
			case TypeCode.Int64:
				this.Ldc((long)o);
				return;
			case TypeCode.UInt64:
				this.Ldc((long)((ulong)o));
				return;
			case TypeCode.Single:
				this.Ldc((float)o);
				return;
			case TypeCode.Double:
				this.Ldc((double)o);
				return;
			case TypeCode.Decimal:
			{
				ConstructorInfo constructor = typeof(decimal).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(bool),
					typeof(byte)
				}, null);
				int[] bits = decimal.GetBits((decimal)o);
				this.Ldc(bits[0]);
				this.Ldc(bits[1]);
				this.Ldc(bits[2]);
				this.Ldc(((long)bits[3] & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue));
				this.Ldc((int)((byte)((bits[3] >> 16) & 255)));
				this.New(constructor);
				return;
			}
			case TypeCode.DateTime:
			{
				ConstructorInfo constructor2 = typeof(DateTime).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(long) }, null);
				this.Ldc(((DateTime)o).Ticks);
				this.New(constructor2);
				return;
			}
			case TypeCode.String:
				this.Ldstr((string)o);
				return;
			}
			if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				ConstructorInfo constructor3 = typeof(TimeSpan).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(long) }, null);
				this.Ldc(((TimeSpan)o).Ticks);
				this.New(constructor3);
				return;
			}
			throw new NotSupportedException("UnknownConstantType");
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000958C1 File Offset: 0x00093AC1
		internal void Ldc(bool boolVar)
		{
			if (boolVar)
			{
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldc_I4_0);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000958E8 File Offset: 0x00093AE8
		internal void Ldc(int intVar)
		{
			switch (intVar)
			{
			case -1:
				this.ilGen.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				this.ilGen.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				this.ilGen.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				this.ilGen.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				this.ilGen.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				this.ilGen.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				this.ilGen.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				this.ilGen.Emit(OpCodes.Ldc_I4, intVar);
				return;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000959E5 File Offset: 0x00093BE5
		internal void Ldc(long l)
		{
			this.ilGen.Emit(OpCodes.Ldc_I8, l);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000959F8 File Offset: 0x00093BF8
		internal void Ldc(float f)
		{
			this.ilGen.Emit(OpCodes.Ldc_R4, f);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00095A0B File Offset: 0x00093C0B
		internal void Ldc(double d)
		{
			this.ilGen.Emit(OpCodes.Ldc_R8, d);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00095A1E File Offset: 0x00093C1E
		internal void Ldstr(string strVar)
		{
			if (strVar == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldstr, strVar);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00095A45 File Offset: 0x00093C45
		internal void LdlocAddress(LocalBuilder localBuilder)
		{
			if (localBuilder.LocalType.IsValueType)
			{
				this.Ldloca(localBuilder);
				return;
			}
			this.Ldloc(localBuilder);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00095A63 File Offset: 0x00093C63
		internal void Ldloc(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloc, localBuilder);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00095A78 File Offset: 0x00093C78
		internal void Ldloc(string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00095A9C File Offset: 0x00093C9C
		internal void Stloc(Type type, string name)
		{
			LocalBuilder localBuilder = null;
			if (!this.currentScope.TryGetValue(name, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, name);
			}
			this.Stloc(localBuilder);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00095ACB File Offset: 0x00093CCB
		internal void Stloc(LocalBuilder local)
		{
			this.ilGen.Emit(OpCodes.Stloc, local);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00095AE0 File Offset: 0x00093CE0
		internal void Ldloc(Type type, string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00095B01 File Offset: 0x00093D01
		internal void Ldloca(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloca, localBuilder);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00095B14 File Offset: 0x00093D14
		internal void LdargAddress(ArgBuilder argBuilder)
		{
			if (argBuilder.ArgType.IsValueType)
			{
				this.Ldarga(argBuilder);
				return;
			}
			this.Ldarg(argBuilder);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00095B32 File Offset: 0x00093D32
		internal void Ldarg(string arg)
		{
			this.Ldarg(this.GetArg(arg));
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00095B41 File Offset: 0x00093D41
		internal void Ldarg(ArgBuilder arg)
		{
			this.Ldarg(arg.Index);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00095B50 File Offset: 0x00093D50
		internal void Ldarg(int slot)
		{
			switch (slot)
			{
			case 0:
				this.ilGen.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (slot <= 255)
				{
					this.ilGen.Emit(OpCodes.Ldarg_S, slot);
					return;
				}
				this.ilGen.Emit(OpCodes.Ldarg, slot);
				return;
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00095BE4 File Offset: 0x00093DE4
		internal void Ldarga(ArgBuilder argBuilder)
		{
			this.Ldarga(argBuilder.Index);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00095BF2 File Offset: 0x00093DF2
		internal void Ldarga(int slot)
		{
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Ldarga_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldarga, slot);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00095C1F File Offset: 0x00093E1F
		internal void Ldlen()
		{
			this.ilGen.Emit(OpCodes.Ldlen);
			this.ilGen.Emit(OpCodes.Conv_I4);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00095C41 File Offset: 0x00093E41
		private OpCode GetLdelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdelemOpCodes[(int)typeCode];
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00095C50 File Offset: 0x00093E50
		internal void Ldelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Ldelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode ldelemOpCode = this.GetLdelemOpCode(Type.GetTypeCode(arrayElementType));
			if (ldelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(ldelemOpCode);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00095CA4 File Offset: 0x00093EA4
		internal void Ldelema(Type arrayElementType)
		{
			OpCode ldelema = OpCodes.Ldelema;
			this.ilGen.Emit(ldelema, arrayElementType);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00095CC4 File Offset: 0x00093EC4
		private OpCode GetStelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.StelemOpCodes[(int)typeCode];
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00095CD4 File Offset: 0x00093ED4
		internal void Stelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Stelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode stelemOpCode = this.GetStelemOpCode(Type.GetTypeCode(arrayElementType));
			if (stelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(stelemOpCode);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00095D28 File Offset: 0x00093F28
		internal Label DefineLabel()
		{
			return this.ilGen.DefineLabel();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00095D35 File Offset: 0x00093F35
		internal void MarkLabel(Label label)
		{
			this.ilGen.MarkLabel(label);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00095D43 File Offset: 0x00093F43
		internal void Nop()
		{
			this.ilGen.Emit(OpCodes.Nop);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00095D55 File Offset: 0x00093F55
		internal void Add()
		{
			this.ilGen.Emit(OpCodes.Add);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00095D67 File Offset: 0x00093F67
		internal void Ret()
		{
			this.ilGen.Emit(OpCodes.Ret);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00095D79 File Offset: 0x00093F79
		internal void Br(Label label)
		{
			this.ilGen.Emit(OpCodes.Br, label);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00095D8C File Offset: 0x00093F8C
		internal void Br_S(Label label)
		{
			this.ilGen.Emit(OpCodes.Br_S, label);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00095D9F File Offset: 0x00093F9F
		internal void Blt(Label label)
		{
			this.ilGen.Emit(OpCodes.Blt, label);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00095DB2 File Offset: 0x00093FB2
		internal void Brfalse(Label label)
		{
			this.ilGen.Emit(OpCodes.Brfalse, label);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00095DC5 File Offset: 0x00093FC5
		internal void Brtrue(Label label)
		{
			this.ilGen.Emit(OpCodes.Brtrue, label);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00095DD8 File Offset: 0x00093FD8
		internal void Pop()
		{
			this.ilGen.Emit(OpCodes.Pop);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00095DEA File Offset: 0x00093FEA
		internal void Dup()
		{
			this.ilGen.Emit(OpCodes.Dup);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00095DFC File Offset: 0x00093FFC
		internal void Ldftn(MethodInfo methodInfo)
		{
			this.ilGen.Emit(OpCodes.Ldftn, methodInfo);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00095E10 File Offset: 0x00094010
		private void InternalIf(bool negate)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			if (negate)
			{
				this.Brtrue(ifState.ElseBegin);
			}
			else
			{
				this.Brfalse(ifState.ElseBegin);
			}
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00095E64 File Offset: 0x00094064
		private OpCode GetConvOpCode(TypeCode typeCode)
		{
			return CodeGenerator.ConvOpCodes[(int)typeCode];
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00095E74 File Offset: 0x00094074
		private void InternalConvert(Type source, Type target, bool isAddress)
		{
			if (target == source)
			{
				return;
			}
			if (target.IsValueType)
			{
				if (source.IsValueType)
				{
					OpCode convOpCode = this.GetConvOpCode(Type.GetTypeCode(target));
					if (convOpCode.Equals(OpCodes.Nop))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "NoConversionPossibleTo");
					}
					this.ilGen.Emit(convOpCode);
					return;
				}
				else
				{
					if (!source.IsAssignableFrom(target))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
					}
					this.Unbox(target);
					if (!isAddress)
					{
						this.Ldobj(target);
						return;
					}
				}
			}
			else if (target.IsAssignableFrom(source))
			{
				if (source.IsValueType)
				{
					if (isAddress)
					{
						this.Ldobj(source);
					}
					this.Box(source);
					return;
				}
			}
			else
			{
				if (source.IsAssignableFrom(target))
				{
					this.Castclass(target);
					return;
				}
				if (target.IsInterface || source.IsInterface)
				{
					this.Castclass(target);
					return;
				}
				throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00095F54 File Offset: 0x00094154
		private IfState PopIfState()
		{
			return this.blockStack.Pop() as IfState;
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00095F68 File Offset: 0x00094168
		internal static AssemblyBuilder CreateAssemblyBuilder(AppDomain appDomain, string name)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = name;
			assemblyName.Version = new Version(1, 0, 0, 0);
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, CodeGenerator.TempFilesLocation);
			}
			return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00095FB4 File Offset: 0x000941B4
		// (set) Token: 0x06001AF5 RID: 6901 RVA: 0x00096008 File Offset: 0x00094208
		internal static string TempFilesLocation
		{
			get
			{
				if (CodeGenerator.tempFilesLocation == null)
				{
					object section = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath);
					string text = null;
					if (section != null)
					{
						XmlSerializerSection xmlSerializerSection = section as XmlSerializerSection;
						if (xmlSerializerSection != null)
						{
							text = xmlSerializerSection.TempFilesLocation;
						}
					}
					if (text != null)
					{
						CodeGenerator.tempFilesLocation = text.Trim();
					}
					else
					{
						CodeGenerator.tempFilesLocation = Path.GetTempPath();
					}
				}
				return CodeGenerator.tempFilesLocation;
			}
			set
			{
				CodeGenerator.tempFilesLocation = value;
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00096010 File Offset: 0x00094210
		internal static ModuleBuilder CreateModuleBuilder(AssemblyBuilder assemblyBuilder, string name)
		{
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return assemblyBuilder.DefineDynamicModule(name, name + ".dll", true);
			}
			return assemblyBuilder.DefineDynamicModule(name);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00096039 File Offset: 0x00094239
		internal static TypeBuilder CreateTypeBuilder(ModuleBuilder moduleBuilder, string name, TypeAttributes attributes, Type parent, Type[] interfaces)
		{
			return moduleBuilder.DefineType("Microsoft.Xml.Serialization.GeneratedAssembly." + name, attributes, parent, interfaces);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00096050 File Offset: 0x00094250
		internal void InitElseIf()
		{
			this.elseIfState = (IfState)this.blockStack.Pop();
			this.initElseIfStack = this.blockStack.Count;
			this.Br(this.elseIfState.EndIf);
			this.MarkLabel(this.elseIfState.ElseBegin);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000960A6 File Offset: 0x000942A6
		internal void InitIf()
		{
			this.initIfStack = this.blockStack.Count;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000960BC File Offset: 0x000942BC
		internal void AndIf(Cmp cmpOp)
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If(cmpOp);
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.ilGen.Emit(this.GetBranchCode(cmpOp), this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00096170 File Offset: 0x00094370
		internal void AndIf()
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If();
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.Brfalse(this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.Brfalse(ifState.ElseBegin);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00096209 File Offset: 0x00094409
		internal void IsInst(Type type)
		{
			this.ilGen.Emit(OpCodes.Isinst, type);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0009621C File Offset: 0x0009441C
		internal void Beq(Label label)
		{
			this.ilGen.Emit(OpCodes.Beq, label);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0009622F File Offset: 0x0009442F
		internal void Bne(Label label)
		{
			this.ilGen.Emit(OpCodes.Bne_Un, label);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00096242 File Offset: 0x00094442
		internal void GotoMethodEnd()
		{
			this.Br(this.methodEndLabel);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00096250 File Offset: 0x00094450
		internal void WhileBegin()
		{
			CodeGenerator.WhileState whileState = new CodeGenerator.WhileState(this);
			this.Br(whileState.CondLabel);
			this.MarkLabel(whileState.StartLabel);
			this.whileStack.Push(whileState);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00096288 File Offset: 0x00094488
		internal void WhileEnd()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Pop();
			this.MarkLabel(whileState.EndLabel);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000962B4 File Offset: 0x000944B4
		internal void WhileBreak()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.EndLabel);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000962E0 File Offset: 0x000944E0
		internal void WhileContinue()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.CondLabel);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0009630C File Offset: 0x0009450C
		internal void WhileBeginCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Nop();
			this.MarkLabel(whileState.CondLabel);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0009633C File Offset: 0x0009453C
		internal void WhileEndCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Brtrue(whileState.StartLabel);
		}

		// Token: 0x04001591 RID: 5521
		internal static BindingFlags InstancePublicBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x04001592 RID: 5522
		internal static BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04001593 RID: 5523
		internal static BindingFlags StaticBindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04001594 RID: 5524
		internal static MethodAttributes PublicMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig;

		// Token: 0x04001595 RID: 5525
		internal static MethodAttributes PublicOverrideMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x04001596 RID: 5526
		internal static MethodAttributes ProtectedOverrideMethodAttributes = MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x04001597 RID: 5527
		internal static MethodAttributes PrivateMethodAttributes = MethodAttributes.Private | MethodAttributes.HideBySig;

		// Token: 0x04001598 RID: 5528
		internal static Type[] EmptyTypeArray = new Type[0];

		// Token: 0x04001599 RID: 5529
		internal static string[] EmptyStringArray = new string[0];

		// Token: 0x0400159A RID: 5530
		private TypeBuilder typeBuilder;

		// Token: 0x0400159B RID: 5531
		private MethodBuilder methodBuilder;

		// Token: 0x0400159C RID: 5532
		private ILGenerator ilGen;

		// Token: 0x0400159D RID: 5533
		private Dictionary<string, ArgBuilder> argList;

		// Token: 0x0400159E RID: 5534
		private LocalScope currentScope;

		// Token: 0x0400159F RID: 5535
		private Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals;

		// Token: 0x040015A0 RID: 5536
		private Stack blockStack;

		// Token: 0x040015A1 RID: 5537
		private Label methodEndLabel;

		// Token: 0x040015A2 RID: 5538
		internal LocalBuilder retLocal;

		// Token: 0x040015A3 RID: 5539
		internal Label retLabel;

		// Token: 0x040015A4 RID: 5540
		private Dictionary<Type, LocalBuilder> TmpLocals = new Dictionary<Type, LocalBuilder>();

		// Token: 0x040015A5 RID: 5541
		private static OpCode[] BranchCodes = new OpCode[]
		{
			OpCodes.Bge,
			OpCodes.Bne_Un,
			OpCodes.Bgt,
			OpCodes.Ble,
			OpCodes.Beq,
			OpCodes.Blt
		};

		// Token: 0x040015A6 RID: 5542
		private Stack leaveLabels = new Stack();

		// Token: 0x040015A7 RID: 5543
		private static OpCode[] LdindOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_U1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_U2,
			OpCodes.Ldind_I4,
			OpCodes.Ldind_U4,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_R4,
			OpCodes.Ldind_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_Ref
		};

		// Token: 0x040015A8 RID: 5544
		private static OpCode[] LdelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_U1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_U2,
			OpCodes.Ldelem_I4,
			OpCodes.Ldelem_U4,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_R4,
			OpCodes.Ldelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldelem_Ref
		};

		// Token: 0x040015A9 RID: 5545
		private static OpCode[] StelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_R4,
			OpCodes.Stelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Stelem_Ref
		};

		// Token: 0x040015AA RID: 5546
		private static OpCode[] ConvOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Conv_I1,
			OpCodes.Conv_I2,
			OpCodes.Conv_I1,
			OpCodes.Conv_U1,
			OpCodes.Conv_I2,
			OpCodes.Conv_U2,
			OpCodes.Conv_I4,
			OpCodes.Conv_U4,
			OpCodes.Conv_I8,
			OpCodes.Conv_U8,
			OpCodes.Conv_R4,
			OpCodes.Conv_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop
		};

		// Token: 0x040015AB RID: 5547
		private static string tempFilesLocation = null;

		// Token: 0x040015AC RID: 5548
		private int initElseIfStack = -1;

		// Token: 0x040015AD RID: 5549
		private IfState elseIfState;

		// Token: 0x040015AE RID: 5550
		private int initIfStack = -1;

		// Token: 0x040015AF RID: 5551
		private Stack whileStack;

		// Token: 0x020002C9 RID: 713
		internal class WhileState
		{
			// Token: 0x06001B07 RID: 6919 RVA: 0x00096809 File Offset: 0x00094A09
			public WhileState(CodeGenerator ilg)
			{
				this.StartLabel = ilg.DefineLabel();
				this.CondLabel = ilg.DefineLabel();
				this.EndLabel = ilg.DefineLabel();
			}

			// Token: 0x040015B0 RID: 5552
			public Label StartLabel;

			// Token: 0x040015B1 RID: 5553
			public Label CondLabel;

			// Token: 0x040015B2 RID: 5554
			public Label EndLabel;
		}
	}
}
