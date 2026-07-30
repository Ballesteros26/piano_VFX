using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000742 RID: 1858
	internal sealed class ValueFixup
	{
		// Token: 0x06004D17 RID: 19735 RVA: 0x00116661 File Offset: 0x00114861
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x0011667E File Offset: 0x0011487E
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x001166A4 File Offset: 0x001148A4
		[SecurityCritical]
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Header reflection error: number of value members: {0}.", new object[] { member.Length }));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04002963 RID: 10595
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x04002964 RID: 10596
		internal Array arrayObj;

		// Token: 0x04002965 RID: 10597
		internal int[] indexMap;

		// Token: 0x04002966 RID: 10598
		internal object header;

		// Token: 0x04002967 RID: 10599
		internal object memberObject;

		// Token: 0x04002968 RID: 10600
		internal static volatile MemberInfo valueInfo;

		// Token: 0x04002969 RID: 10601
		internal ReadObjectInfo objectInfo;

		// Token: 0x0400296A RID: 10602
		internal string memberName;
	}
}
