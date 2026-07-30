using System;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200065A RID: 1626
	internal class GenerateHelper
	{
		// Token: 0x0600413E RID: 16702 RVA: 0x0015DFC5 File Offset: 0x0015C1C5
		public GenerateHelper(XmlILModule module, bool isDebug)
		{
			this.isDebug = isDebug;
			this.module = module;
			this.staticData = new StaticDataManager();
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0015DFE8 File Offset: 0x0015C1E8
		public void MethodBegin(MethodBase methInfo, ISourceLineInfo sourceInfo, bool initWriters)
		{
			this.methInfo = methInfo;
			this.ilgen = XmlILModule.DefineMethodBody(methInfo);
			this.lastSourceInfo = null;
			if (this.isDebug)
			{
				this.DebugStartScope();
				if (sourceInfo != null)
				{
					this.MarkSequencePoint(sourceInfo);
					this.Emit(OpCodes.Nop);
				}
			}
			else if (this.module.EmitSymbols && sourceInfo != null)
			{
				this.MarkSequencePoint(sourceInfo);
				this.lastSourceInfo = null;
			}
			this.initWriters = false;
			if (initWriters)
			{
				this.EnsureWriter();
				this.LoadQueryRuntime();
				this.Call(XmlILMethods.GetOutput);
				this.Emit(OpCodes.Stloc, this.locXOut);
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x0015E084 File Offset: 0x0015C284
		public void MethodEnd()
		{
			this.Emit(OpCodes.Ret);
			if (this.isDebug)
			{
				this.DebugEndScope();
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0015E09F File Offset: 0x0015C29F
		public void CallSyncToNavigator()
		{
			if (this.methSyncToNav == null)
			{
				this.methSyncToNav = this.module.FindMethod("SyncToNavigator");
			}
			this.Call(this.methSyncToNav);
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x0015E0D1 File Offset: 0x0015C2D1
		public StaticDataManager StaticData
		{
			get
			{
				return this.staticData;
			}
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x0015E0DC File Offset: 0x0015C2DC
		public void LoadInteger(int intVal)
		{
			if (intVal >= -1 && intVal < 9)
			{
				OpCode opCode;
				switch (intVal)
				{
				case -1:
					opCode = OpCodes.Ldc_I4_M1;
					break;
				case 0:
					opCode = OpCodes.Ldc_I4_0;
					break;
				case 1:
					opCode = OpCodes.Ldc_I4_1;
					break;
				case 2:
					opCode = OpCodes.Ldc_I4_2;
					break;
				case 3:
					opCode = OpCodes.Ldc_I4_3;
					break;
				case 4:
					opCode = OpCodes.Ldc_I4_4;
					break;
				case 5:
					opCode = OpCodes.Ldc_I4_5;
					break;
				case 6:
					opCode = OpCodes.Ldc_I4_6;
					break;
				case 7:
					opCode = OpCodes.Ldc_I4_7;
					break;
				case 8:
					opCode = OpCodes.Ldc_I4_8;
					break;
				default:
					return;
				}
				this.Emit(opCode);
				return;
			}
			if (intVal >= -128 && intVal <= 127)
			{
				this.Emit(OpCodes.Ldc_I4_S, (sbyte)intVal);
				return;
			}
			this.Emit(OpCodes.Ldc_I4, intVal);
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x0015E1A3 File Offset: 0x0015C3A3
		public void LoadBoolean(bool boolVal)
		{
			this.Emit(boolVal ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x0015E1BA File Offset: 0x0015C3BA
		public void LoadType(Type clrTyp)
		{
			this.Emit(OpCodes.Ldtoken, clrTyp);
			this.Call(XmlILMethods.GetTypeFromHandle);
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0015E1D3 File Offset: 0x0015C3D3
		public LocalBuilder DeclareLocal(string name, Type type)
		{
			return this.ilgen.DeclareLocal(type);
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0015E1E1 File Offset: 0x0015C3E1
		public void LoadQueryRuntime()
		{
			this.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x0015E1EE File Offset: 0x0015C3EE
		public void LoadQueryContext()
		{
			this.Emit(OpCodes.Ldarg_0);
			this.Call(XmlILMethods.Context);
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0015E206 File Offset: 0x0015C406
		public void LoadXsltLibrary()
		{
			this.Emit(OpCodes.Ldarg_0);
			this.Call(XmlILMethods.XsltLib);
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x0015E21E File Offset: 0x0015C41E
		public void LoadQueryOutput()
		{
			this.Emit(OpCodes.Ldloc, this.locXOut);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x0015E234 File Offset: 0x0015C434
		public void LoadParameter(int paramPos)
		{
			switch (paramPos)
			{
			case 0:
				this.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (paramPos <= 255)
				{
					this.Emit(OpCodes.Ldarg_S, (byte)paramPos);
					return;
				}
				if (paramPos <= 65535)
				{
					this.Emit(OpCodes.Ldarg, paramPos);
					return;
				}
				throw new XslTransformException("Functions may not have more than 65535 parameters.");
			}
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x0015E2C0 File Offset: 0x0015C4C0
		public void SetParameter(object paramId)
		{
			int num = (int)paramId;
			if (num <= 255)
			{
				this.Emit(OpCodes.Starg_S, (byte)num);
				return;
			}
			if (num <= 65535)
			{
				this.Emit(OpCodes.Starg, num);
				return;
			}
			throw new XslTransformException("Functions may not have more than 65535 parameters.");
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x0015E309 File Offset: 0x0015C509
		public void BranchAndMark(Label lblBranch, Label lblMark)
		{
			if (!lblBranch.Equals(lblMark))
			{
				this.EmitUnconditionalBranch(OpCodes.Br, lblBranch);
			}
			this.MarkLabel(lblMark);
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x0015E328 File Offset: 0x0015C528
		public void TestAndBranch(int i4, Label lblBranch, OpCode opcodeBranch)
		{
			if (i4 == 0)
			{
				if (opcodeBranch.Value == OpCodes.Beq.Value)
				{
					opcodeBranch = OpCodes.Brfalse;
					goto IL_0086;
				}
				if (opcodeBranch.Value == OpCodes.Beq_S.Value)
				{
					opcodeBranch = OpCodes.Brfalse_S;
					goto IL_0086;
				}
				if (opcodeBranch.Value == OpCodes.Bne_Un.Value)
				{
					opcodeBranch = OpCodes.Brtrue;
					goto IL_0086;
				}
				if (opcodeBranch.Value == OpCodes.Bne_Un_S.Value)
				{
					opcodeBranch = OpCodes.Brtrue_S;
					goto IL_0086;
				}
			}
			this.LoadInteger(i4);
			IL_0086:
			this.Emit(opcodeBranch, lblBranch);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x0015E3C4 File Offset: 0x0015C5C4
		public void ConvBranchToBool(Label lblBranch, bool isTrueBranch)
		{
			Label label = this.DefineLabel();
			this.Emit(isTrueBranch ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1);
			this.EmitUnconditionalBranch(OpCodes.Br_S, label);
			this.MarkLabel(lblBranch);
			this.Emit(isTrueBranch ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			this.MarkLabel(label);
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0015E41C File Offset: 0x0015C61C
		public void TailCall(MethodInfo meth)
		{
			this.Emit(OpCodes.Tailcall);
			this.Call(meth);
			this.Emit(OpCodes.Ret);
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void TraceCall(OpCode opcode, MethodInfo meth)
		{
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x0015E43C File Offset: 0x0015C63C
		public void Call(MethodInfo meth)
		{
			OpCode opCode = ((meth.IsVirtual || meth.IsAbstract) ? OpCodes.Callvirt : OpCodes.Call);
			this.ilgen.Emit(opCode, meth);
			if (this.lastSourceInfo != null)
			{
				this.MarkSequencePoint(SourceLineInfo.NoSource);
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x0015E488 File Offset: 0x0015C688
		public void CallToken(MethodInfo meth)
		{
			MethodBuilder methodBuilder = this.methInfo as MethodBuilder;
			if (methodBuilder != null)
			{
				OpCode opCode = ((meth.IsVirtual || meth.IsAbstract) ? OpCodes.Callvirt : OpCodes.Call);
				this.ilgen.Emit(opCode, ((ModuleBuilder)methodBuilder.GetModule()).GetMethodToken(meth).Token);
				if (this.lastSourceInfo != null)
				{
					this.MarkSequencePoint(SourceLineInfo.NoSource);
					return;
				}
			}
			else
			{
				this.Call(meth);
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x0015E507 File Offset: 0x0015C707
		public void Construct(ConstructorInfo constr)
		{
			this.Emit(OpCodes.Newobj, constr);
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x0015E518 File Offset: 0x0015C718
		public void CallConcatStrings(int cStrings)
		{
			switch (cStrings)
			{
			case 0:
				this.Emit(OpCodes.Ldstr, "");
				return;
			case 1:
				break;
			case 2:
				this.Call(XmlILMethods.StrCat2);
				return;
			case 3:
				this.Call(XmlILMethods.StrCat3);
				return;
			case 4:
				this.Call(XmlILMethods.StrCat4);
				break;
			default:
				return;
			}
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x0015E574 File Offset: 0x0015C774
		public void TreatAs(Type clrTypeSrc, Type clrTypeDst)
		{
			if (clrTypeSrc == clrTypeDst)
			{
				return;
			}
			if (clrTypeSrc.IsValueType)
			{
				this.Emit(OpCodes.Box, clrTypeSrc);
				return;
			}
			if (clrTypeDst.IsValueType)
			{
				this.Emit(OpCodes.Unbox, clrTypeDst);
				this.Emit(OpCodes.Ldobj, clrTypeDst);
				return;
			}
			if (clrTypeDst != typeof(object))
			{
				this.Emit(OpCodes.Castclass, clrTypeDst);
			}
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x0015E5E0 File Offset: 0x0015C7E0
		public void ConstructLiteralDecimal(decimal dec)
		{
			if (dec >= -2147483648m && dec <= 2147483647m && decimal.Truncate(dec) == dec)
			{
				this.LoadInteger((int)dec);
				this.Construct(XmlILConstructors.DecFromInt32);
				return;
			}
			int[] bits = decimal.GetBits(dec);
			this.LoadInteger(bits[0]);
			this.LoadInteger(bits[1]);
			this.LoadInteger(bits[2]);
			this.LoadBoolean(bits[3] < 0);
			this.LoadInteger(bits[3] >> 16);
			this.Construct(XmlILConstructors.DecFromParts);
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x0015E67C File Offset: 0x0015C87C
		public void ConstructLiteralQName(string localName, string namespaceName)
		{
			this.Emit(OpCodes.Ldstr, localName);
			this.Emit(OpCodes.Ldstr, namespaceName);
			this.Construct(XmlILConstructors.QName);
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x0015E6A4 File Offset: 0x0015C8A4
		public void CallArithmeticOp(QilNodeType opType, XmlTypeCode code)
		{
			MethodInfo methodInfo = null;
			if (code <= XmlTypeCode.Double)
			{
				if (code == XmlTypeCode.Decimal)
				{
					switch (opType)
					{
					case QilNodeType.Negate:
						methodInfo = XmlILMethods.DecNeg;
						break;
					case QilNodeType.Add:
						methodInfo = XmlILMethods.DecAdd;
						break;
					case QilNodeType.Subtract:
						methodInfo = XmlILMethods.DecSub;
						break;
					case QilNodeType.Multiply:
						methodInfo = XmlILMethods.DecMul;
						break;
					case QilNodeType.Divide:
						methodInfo = XmlILMethods.DecDiv;
						break;
					case QilNodeType.Modulo:
						methodInfo = XmlILMethods.DecRem;
						break;
					}
					this.Call(methodInfo);
					return;
				}
				if (code - XmlTypeCode.Float > 1)
				{
					return;
				}
			}
			else if (code != XmlTypeCode.Integer && code != XmlTypeCode.Int)
			{
				return;
			}
			switch (opType)
			{
			case QilNodeType.Negate:
				this.Emit(OpCodes.Neg);
				return;
			case QilNodeType.Add:
				this.Emit(OpCodes.Add);
				return;
			case QilNodeType.Subtract:
				this.Emit(OpCodes.Sub);
				return;
			case QilNodeType.Multiply:
				this.Emit(OpCodes.Mul);
				return;
			case QilNodeType.Divide:
				this.Emit(OpCodes.Div);
				return;
			case QilNodeType.Modulo:
				this.Emit(OpCodes.Rem);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x0015E794 File Offset: 0x0015C994
		public void CallCompareEquals(XmlTypeCode code)
		{
			MethodInfo methodInfo = null;
			if (code != XmlTypeCode.String)
			{
				if (code != XmlTypeCode.Decimal)
				{
					if (code == XmlTypeCode.QName)
					{
						methodInfo = XmlILMethods.QNameEq;
					}
				}
				else
				{
					methodInfo = XmlILMethods.DecEq;
				}
			}
			else
			{
				methodInfo = XmlILMethods.StrEq;
			}
			this.Call(methodInfo);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0015E7D4 File Offset: 0x0015C9D4
		public void CallCompare(XmlTypeCode code)
		{
			MethodInfo methodInfo = null;
			if (code != XmlTypeCode.String)
			{
				if (code == XmlTypeCode.Decimal)
				{
					methodInfo = XmlILMethods.DecCmp;
				}
			}
			else
			{
				methodInfo = XmlILMethods.StrCmp;
			}
			this.Call(methodInfo);
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x0015E804 File Offset: 0x0015CA04
		public void CallStartRtfConstruction(string baseUri)
		{
			this.EnsureWriter();
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldstr, baseUri);
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.StartRtfConstr);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0015E83A File Offset: 0x0015CA3A
		public void CallEndRtfConstruction()
		{
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.EndRtfConstr);
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x0015E85E File Offset: 0x0015CA5E
		public void CallStartSequenceConstruction()
		{
			this.EnsureWriter();
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.StartSeqConstr);
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x0015E888 File Offset: 0x0015CA88
		public void CallEndSequenceConstruction()
		{
			this.LoadQueryRuntime();
			this.Emit(OpCodes.Ldloca, this.locXOut);
			this.Call(XmlILMethods.EndSeqConstr);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x0015E8AC File Offset: 0x0015CAAC
		public void CallGetEarlyBoundObject(int idxObj, Type clrType)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxObj);
			this.Call(XmlILMethods.GetEarly);
			this.TreatAs(typeof(object), clrType);
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x0015E8D7 File Offset: 0x0015CAD7
		public void CallGetAtomizedName(int idxName)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxName);
			this.Call(XmlILMethods.GetAtomizedName);
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x0015E8F1 File Offset: 0x0015CAF1
		public void CallGetNameFilter(int idxFilter)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxFilter);
			this.Call(XmlILMethods.GetNameFilter);
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x0015E90B File Offset: 0x0015CB0B
		public void CallGetTypeFilter(XPathNodeType nodeType)
		{
			this.LoadQueryRuntime();
			this.LoadInteger((int)nodeType);
			this.Call(XmlILMethods.GetTypeFilter);
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x0015E925 File Offset: 0x0015CB25
		public void CallParseTagName(GenerateNameType nameType)
		{
			if (nameType == GenerateNameType.TagNameAndMappings)
			{
				this.Call(XmlILMethods.TagAndMappings);
				return;
			}
			this.Call(XmlILMethods.TagAndNamespace);
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x0015E942 File Offset: 0x0015CB42
		public void CallGetGlobalValue(int idxValue, Type clrType)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxValue);
			this.Call(XmlILMethods.GetGlobalValue);
			this.TreatAs(typeof(object), clrType);
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x0015E96D File Offset: 0x0015CB6D
		public void CallSetGlobalValue(Type clrType)
		{
			this.TreatAs(clrType, typeof(object));
			this.Call(XmlILMethods.SetGlobalValue);
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x0015E98B File Offset: 0x0015CB8B
		public void CallGetCollation(int idxName)
		{
			this.LoadQueryRuntime();
			this.LoadInteger(idxName);
			this.Call(XmlILMethods.GetCollation);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x0015E9A5 File Offset: 0x0015CBA5
		private void EnsureWriter()
		{
			if (!this.initWriters)
			{
				this.locXOut = this.DeclareLocal("$$$xwrtChk", typeof(XmlQueryOutput));
				this.initWriters = true;
			}
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x0015E9D1 File Offset: 0x0015CBD1
		public void CallGetParameter(string localName, string namespaceUri)
		{
			this.LoadQueryContext();
			this.Emit(OpCodes.Ldstr, localName);
			this.Emit(OpCodes.Ldstr, namespaceUri);
			this.Call(XmlILMethods.GetParam);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x0015E9FC File Offset: 0x0015CBFC
		public void CallStartTree(XPathNodeType rootType)
		{
			this.LoadQueryOutput();
			this.LoadInteger((int)rootType);
			this.Call(XmlILMethods.StartTree);
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x0015EA16 File Offset: 0x0015CC16
		public void CallEndTree()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndTree);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x0015EA29 File Offset: 0x0015CC29
		public void CallWriteStartRoot()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.StartRoot);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x0015EA3C File Offset: 0x0015CC3C
		public void CallWriteEndRoot()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndRoot);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x0015EA50 File Offset: 0x0015CC50
		public void CallWriteStartElement(GenerateNameType nameType, bool callChk)
		{
			MethodInfo methodInfo = null;
			if (callChk)
			{
				switch (nameType)
				{
				case GenerateNameType.LiteralLocalName:
					methodInfo = XmlILMethods.StartElemLocName;
					break;
				case GenerateNameType.LiteralName:
					methodInfo = XmlILMethods.StartElemLitName;
					break;
				case GenerateNameType.CopiedName:
					methodInfo = XmlILMethods.StartElemCopyName;
					break;
				case GenerateNameType.TagNameAndMappings:
					methodInfo = XmlILMethods.StartElemMapName;
					break;
				case GenerateNameType.TagNameAndNamespace:
					methodInfo = XmlILMethods.StartElemNmspName;
					break;
				case GenerateNameType.QName:
					methodInfo = XmlILMethods.StartElemQName;
					break;
				}
			}
			else if (nameType != GenerateNameType.LiteralLocalName)
			{
				if (nameType == GenerateNameType.LiteralName)
				{
					methodInfo = XmlILMethods.StartElemLitNameUn;
				}
			}
			else
			{
				methodInfo = XmlILMethods.StartElemLocNameUn;
			}
			this.Call(methodInfo);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x0015EAD0 File Offset: 0x0015CCD0
		public void CallWriteEndElement(GenerateNameType nameType, bool callChk)
		{
			MethodInfo methodInfo = null;
			if (callChk)
			{
				methodInfo = XmlILMethods.EndElemStackName;
			}
			else if (nameType != GenerateNameType.LiteralLocalName)
			{
				if (nameType == GenerateNameType.LiteralName)
				{
					methodInfo = XmlILMethods.EndElemLitNameUn;
				}
			}
			else
			{
				methodInfo = XmlILMethods.EndElemLocNameUn;
			}
			this.Call(methodInfo);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x0015EB08 File Offset: 0x0015CD08
		public void CallStartElementContent()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.StartContentUn);
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x0015EB1C File Offset: 0x0015CD1C
		public void CallWriteStartAttribute(GenerateNameType nameType, bool callChk)
		{
			MethodInfo methodInfo = null;
			if (callChk)
			{
				switch (nameType)
				{
				case GenerateNameType.LiteralLocalName:
					methodInfo = XmlILMethods.StartAttrLocName;
					break;
				case GenerateNameType.LiteralName:
					methodInfo = XmlILMethods.StartAttrLitName;
					break;
				case GenerateNameType.CopiedName:
					methodInfo = XmlILMethods.StartAttrCopyName;
					break;
				case GenerateNameType.TagNameAndMappings:
					methodInfo = XmlILMethods.StartAttrMapName;
					break;
				case GenerateNameType.TagNameAndNamespace:
					methodInfo = XmlILMethods.StartAttrNmspName;
					break;
				case GenerateNameType.QName:
					methodInfo = XmlILMethods.StartAttrQName;
					break;
				}
			}
			else if (nameType != GenerateNameType.LiteralLocalName)
			{
				if (nameType == GenerateNameType.LiteralName)
				{
					methodInfo = XmlILMethods.StartAttrLitNameUn;
				}
			}
			else
			{
				methodInfo = XmlILMethods.StartAttrLocNameUn;
			}
			this.Call(methodInfo);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x0015EB9C File Offset: 0x0015CD9C
		public void CallWriteEndAttribute(bool callChk)
		{
			this.LoadQueryOutput();
			if (callChk)
			{
				this.Call(XmlILMethods.EndAttr);
				return;
			}
			this.Call(XmlILMethods.EndAttrUn);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x0015EBBE File Offset: 0x0015CDBE
		public void CallWriteNamespaceDecl(bool callChk)
		{
			if (callChk)
			{
				this.Call(XmlILMethods.NamespaceDecl);
				return;
			}
			this.Call(XmlILMethods.NamespaceDeclUn);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x0015EBDA File Offset: 0x0015CDDA
		public void CallWriteString(bool disableOutputEscaping, bool callChk)
		{
			if (callChk)
			{
				if (disableOutputEscaping)
				{
					this.Call(XmlILMethods.NoEntText);
					return;
				}
				this.Call(XmlILMethods.Text);
				return;
			}
			else
			{
				if (disableOutputEscaping)
				{
					this.Call(XmlILMethods.NoEntTextUn);
					return;
				}
				this.Call(XmlILMethods.TextUn);
				return;
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x0015EC14 File Offset: 0x0015CE14
		public void CallWriteStartPI()
		{
			this.Call(XmlILMethods.StartPI);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x0015EC21 File Offset: 0x0015CE21
		public void CallWriteEndPI()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndPI);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x0015EC34 File Offset: 0x0015CE34
		public void CallWriteStartComment()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.StartComment);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x0015EC47 File Offset: 0x0015CE47
		public void CallWriteEndComment()
		{
			this.LoadQueryOutput();
			this.Call(XmlILMethods.EndComment);
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x0015EC5C File Offset: 0x0015CE5C
		public void CallCacheCount(Type itemStorageType)
		{
			XmlILStorageMethods xmlILStorageMethods = XmlILMethods.StorageMethods[itemStorageType];
			this.Call(xmlILStorageMethods.IListCount);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x0015EC81 File Offset: 0x0015CE81
		public void CallCacheItem(Type itemStorageType)
		{
			this.Call(XmlILMethods.StorageMethods[itemStorageType].IListItem);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x0015EC9C File Offset: 0x0015CE9C
		public void CallValueAs(Type clrType)
		{
			MethodInfo valueAs = XmlILMethods.StorageMethods[clrType].ValueAs;
			if (valueAs == null)
			{
				this.LoadType(clrType);
				this.Emit(OpCodes.Ldnull);
				this.Call(XmlILMethods.ValueAsAny);
				this.TreatAs(typeof(object), clrType);
				return;
			}
			this.Call(valueAs);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x0015ECFC File Offset: 0x0015CEFC
		public void AddSortKey(XmlQueryType keyType)
		{
			MethodInfo methodInfo = null;
			if (keyType == null)
			{
				methodInfo = XmlILMethods.SortKeyEmpty;
			}
			else
			{
				XmlTypeCode typeCode = keyType.TypeCode;
				if (typeCode <= XmlTypeCode.DateTime)
				{
					if (typeCode != XmlTypeCode.None)
					{
						switch (typeCode)
						{
						case XmlTypeCode.AnyAtomicType:
							return;
						case XmlTypeCode.String:
							methodInfo = XmlILMethods.SortKeyString;
							break;
						case XmlTypeCode.Boolean:
							methodInfo = XmlILMethods.SortKeyInt;
							break;
						case XmlTypeCode.Decimal:
							methodInfo = XmlILMethods.SortKeyDecimal;
							break;
						case XmlTypeCode.Double:
							methodInfo = XmlILMethods.SortKeyDouble;
							break;
						case XmlTypeCode.DateTime:
							methodInfo = XmlILMethods.SortKeyDateTime;
							break;
						}
					}
					else
					{
						this.Emit(OpCodes.Pop);
						methodInfo = XmlILMethods.SortKeyEmpty;
					}
				}
				else if (typeCode != XmlTypeCode.Integer)
				{
					if (typeCode == XmlTypeCode.Int)
					{
						methodInfo = XmlILMethods.SortKeyInt;
					}
				}
				else
				{
					methodInfo = XmlILMethods.SortKeyInteger;
				}
			}
			this.Call(methodInfo);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x0015EDBC File Offset: 0x0015CFBC
		public void DebugStartScope()
		{
			this.ilgen.BeginScope();
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x0015EDC9 File Offset: 0x0015CFC9
		public void DebugEndScope()
		{
			this.ilgen.EndScope();
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x0015EDD6 File Offset: 0x0015CFD6
		public void DebugSequencePoint(ISourceLineInfo sourceInfo)
		{
			this.Emit(OpCodes.Nop);
			this.MarkSequencePoint(sourceInfo);
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0015EDEC File Offset: 0x0015CFEC
		private string GetFileName(ISourceLineInfo sourceInfo)
		{
			string uri = sourceInfo.Uri;
			if (uri == this.lastUriString)
			{
				return this.lastFileName;
			}
			this.lastUriString = uri;
			this.lastFileName = SourceLineInfo.GetFileName(uri);
			return this.lastFileName;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x0015EE2C File Offset: 0x0015D02C
		private void MarkSequencePoint(ISourceLineInfo sourceInfo)
		{
			if (sourceInfo.IsNoSource && this.lastSourceInfo != null && this.lastSourceInfo.IsNoSource)
			{
				return;
			}
			string fileName = this.GetFileName(sourceInfo);
			ISymbolDocumentWriter symbolDocumentWriter = this.module.AddSourceDocument(fileName);
			this.ilgen.MarkSequencePoint(symbolDocumentWriter, sourceInfo.Start.Line, sourceInfo.Start.Pos, sourceInfo.End.Line, sourceInfo.End.Pos);
			this.lastSourceInfo = sourceInfo;
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x0015EEB7 File Offset: 0x0015D0B7
		public Label DefineLabel()
		{
			return this.ilgen.DefineLabel();
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0015EEC4 File Offset: 0x0015D0C4
		public void MarkLabel(Label lbl)
		{
			if (this.lastSourceInfo != null && !this.lastSourceInfo.IsNoSource)
			{
				this.DebugSequencePoint(SourceLineInfo.NoSource);
			}
			this.ilgen.MarkLabel(lbl);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x0015EEF2 File Offset: 0x0015D0F2
		public void Emit(OpCode opcode)
		{
			this.ilgen.Emit(opcode);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x0015EF00 File Offset: 0x0015D100
		public void Emit(OpCode opcode, byte byteVal)
		{
			this.ilgen.Emit(opcode, byteVal);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x0015EF0F File Offset: 0x0015D10F
		public void Emit(OpCode opcode, ConstructorInfo constrInfo)
		{
			this.ilgen.Emit(opcode, constrInfo);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0015EF1E File Offset: 0x0015D11E
		public void Emit(OpCode opcode, double dblVal)
		{
			this.ilgen.Emit(opcode, dblVal);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0015EF2D File Offset: 0x0015D12D
		public void Emit(OpCode opcode, float fltVal)
		{
			this.ilgen.Emit(opcode, fltVal);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x0015EF3C File Offset: 0x0015D13C
		public void Emit(OpCode opcode, FieldInfo fldInfo)
		{
			this.ilgen.Emit(opcode, fldInfo);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x0015EF4B File Offset: 0x0015D14B
		public void Emit(OpCode opcode, short shrtVal)
		{
			this.ilgen.Emit(opcode, shrtVal);
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0015EF5A File Offset: 0x0015D15A
		public void Emit(OpCode opcode, int intVal)
		{
			this.ilgen.Emit(opcode, intVal);
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x0015EF69 File Offset: 0x0015D169
		public void Emit(OpCode opcode, long longVal)
		{
			this.ilgen.Emit(opcode, longVal);
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0015EF78 File Offset: 0x0015D178
		public void Emit(OpCode opcode, Label lblVal)
		{
			this.ilgen.Emit(opcode, lblVal);
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0015EF87 File Offset: 0x0015D187
		public void Emit(OpCode opcode, Label[] arrLabels)
		{
			this.ilgen.Emit(opcode, arrLabels);
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0015EF96 File Offset: 0x0015D196
		public void Emit(OpCode opcode, LocalBuilder locBldr)
		{
			this.ilgen.Emit(opcode, locBldr);
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x0015EFA5 File Offset: 0x0015D1A5
		public void Emit(OpCode opcode, MethodInfo methInfo)
		{
			this.ilgen.Emit(opcode, methInfo);
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x0015EFB4 File Offset: 0x0015D1B4
		public void Emit(OpCode opcode, sbyte sbyteVal)
		{
			this.ilgen.Emit(opcode, sbyteVal);
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x0015EFC3 File Offset: 0x0015D1C3
		public void Emit(OpCode opcode, string strVal)
		{
			this.ilgen.Emit(opcode, strVal);
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x0015EFD2 File Offset: 0x0015D1D2
		public void Emit(OpCode opcode, Type typVal)
		{
			this.ilgen.Emit(opcode, typVal);
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x0015EFE4 File Offset: 0x0015D1E4
		public void EmitUnconditionalBranch(OpCode opcode, Label lblTarget)
		{
			if (!opcode.Equals(OpCodes.Br) && !opcode.Equals(OpCodes.Br_S))
			{
				this.Emit(OpCodes.Ldc_I4_1);
			}
			this.ilgen.Emit(opcode, lblTarget);
			if (this.lastSourceInfo != null && (opcode.Equals(OpCodes.Br) || opcode.Equals(OpCodes.Br_S)))
			{
				this.MarkSequencePoint(SourceLineInfo.NoSource);
			}
		}

		// Token: 0x040029EE RID: 10734
		private MethodBase methInfo;

		// Token: 0x040029EF RID: 10735
		private ILGenerator ilgen;

		// Token: 0x040029F0 RID: 10736
		private LocalBuilder locXOut;

		// Token: 0x040029F1 RID: 10737
		private XmlILModule module;

		// Token: 0x040029F2 RID: 10738
		private bool isDebug;

		// Token: 0x040029F3 RID: 10739
		private bool initWriters;

		// Token: 0x040029F4 RID: 10740
		private StaticDataManager staticData;

		// Token: 0x040029F5 RID: 10741
		private ISourceLineInfo lastSourceInfo;

		// Token: 0x040029F6 RID: 10742
		private MethodInfo methSyncToNav;

		// Token: 0x040029F7 RID: 10743
		private string lastUriString;

		// Token: 0x040029F8 RID: 10744
		private string lastFileName;
	}
}
