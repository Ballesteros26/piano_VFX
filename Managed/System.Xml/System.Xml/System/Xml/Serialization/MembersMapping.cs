using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F2 RID: 754
	internal class MembersMapping : TypeMapping
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0009AC2E File Offset: 0x00098E2E
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0009AC36 File Offset: 0x00098E36
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0009AC3F File Offset: 0x00098E3F
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x0009AC47 File Offset: 0x00098E47
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0009AC50 File Offset: 0x00098E50
		// (set) Token: 0x06001C49 RID: 7241 RVA: 0x0009AC58 File Offset: 0x00098E58
		internal bool HasWrapperElement
		{
			get
			{
				return this.hasWrapperElement;
			}
			set
			{
				this.hasWrapperElement = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0009AC61 File Offset: 0x00098E61
		// (set) Token: 0x06001C4B RID: 7243 RVA: 0x0009AC69 File Offset: 0x00098E69
		internal bool ValidateRpcWrapperElement
		{
			get
			{
				return this.validateRpcWrapperElement;
			}
			set
			{
				this.validateRpcWrapperElement = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0009AC72 File Offset: 0x00098E72
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x0009AC7A File Offset: 0x00098E7A
		internal bool WriteAccessors
		{
			get
			{
				return this.writeAccessors;
			}
			set
			{
				this.writeAccessors = value;
			}
		}

		// Token: 0x04001638 RID: 5688
		private MemberMapping[] members;

		// Token: 0x04001639 RID: 5689
		private bool hasWrapperElement = true;

		// Token: 0x0400163A RID: 5690
		private bool validateRpcWrapperElement;

		// Token: 0x0400163B RID: 5691
		private bool writeAccessors = true;

		// Token: 0x0400163C RID: 5692
		private MemberMapping xmlnsMember;
	}
}
