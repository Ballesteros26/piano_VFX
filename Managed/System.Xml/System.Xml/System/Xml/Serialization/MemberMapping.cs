using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002F1 RID: 753
	internal class MemberMapping : AccessorMapping
	{
		// Token: 0x06001C2C RID: 7212 RVA: 0x0009AA3C File Offset: 0x00098C3C
		internal MemberMapping()
		{
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0009AA4C File Offset: 0x00098C4C
		private MemberMapping(MemberMapping mapping)
			: base(mapping)
		{
			this.name = mapping.name;
			this.checkShouldPersist = mapping.checkShouldPersist;
			this.checkSpecified = mapping.checkSpecified;
			this.isReturnValue = mapping.isReturnValue;
			this.readOnly = mapping.readOnly;
			this.sequenceId = mapping.sequenceId;
			this.memberInfo = mapping.memberInfo;
			this.checkSpecifiedMemberInfo = mapping.checkSpecifiedMemberInfo;
			this.checkShouldPersistMethodInfo = mapping.checkShouldPersistMethodInfo;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0009AAD3 File Offset: 0x00098CD3
		// (set) Token: 0x06001C2F RID: 7215 RVA: 0x0009AADB File Offset: 0x00098CDB
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
			set
			{
				this.checkShouldPersist = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0009AAE4 File Offset: 0x00098CE4
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0009AAEC File Offset: 0x00098CEC
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
			set
			{
				this.checkSpecified = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0009AAF5 File Offset: 0x00098CF5
		// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0009AB0B File Offset: 0x00098D0B
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0009AB14 File Offset: 0x00098D14
		// (set) Token: 0x06001C35 RID: 7221 RVA: 0x0009AB1C File Offset: 0x00098D1C
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0009AB25 File Offset: 0x00098D25
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0009AB2D File Offset: 0x00098D2D
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
			set
			{
				this.checkSpecifiedMemberInfo = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0009AB36 File Offset: 0x00098D36
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0009AB3E File Offset: 0x00098D3E
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
			set
			{
				this.checkShouldPersistMethodInfo = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0009AB47 File Offset: 0x00098D47
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x0009AB4F File Offset: 0x00098D4F
		internal bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0009AB58 File Offset: 0x00098D58
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0009AB60 File Offset: 0x00098D60
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0009AB69 File Offset: 0x00098D69
		internal bool IsSequence
		{
			get
			{
				return this.sequenceId >= 0;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0009AB77 File Offset: 0x00098D77
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0009AB7F File Offset: 0x00098D7F
		internal int SequenceId
		{
			get
			{
				return this.sequenceId;
			}
			set
			{
				this.sequenceId = value;
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0009AB88 File Offset: 0x00098D88
		private string GetNullableType(TypeDesc td)
		{
			if (td.IsMappedType || (!td.IsValueType && (base.Elements[0].IsSoap || td.ArrayElementTypeDesc == null)))
			{
				return td.FullName;
			}
			if (td.ArrayElementTypeDesc != null)
			{
				return this.GetNullableType(td.ArrayElementTypeDesc) + "[]";
			}
			return "System.Nullable`1[" + td.FullName + "]";
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0009ABF7 File Offset: 0x00098DF7
		internal MemberMapping Clone()
		{
			return new MemberMapping(this);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0009ABFF File Offset: 0x00098DFF
		internal string GetTypeName(CodeDomProvider codeProvider)
		{
			if (base.IsNeedNullable && codeProvider.Supports(GeneratorSupport.GenericTypeReference))
			{
				return this.GetNullableType(base.TypeDesc);
			}
			return base.TypeDesc.FullName;
		}

		// Token: 0x0400162F RID: 5679
		private string name;

		// Token: 0x04001630 RID: 5680
		private bool checkShouldPersist;

		// Token: 0x04001631 RID: 5681
		private SpecifiedAccessor checkSpecified;

		// Token: 0x04001632 RID: 5682
		private bool isReturnValue;

		// Token: 0x04001633 RID: 5683
		private bool readOnly;

		// Token: 0x04001634 RID: 5684
		private int sequenceId = -1;

		// Token: 0x04001635 RID: 5685
		private MemberInfo memberInfo;

		// Token: 0x04001636 RID: 5686
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x04001637 RID: 5687
		private MethodInfo checkShouldPersistMethodInfo;
	}
}
