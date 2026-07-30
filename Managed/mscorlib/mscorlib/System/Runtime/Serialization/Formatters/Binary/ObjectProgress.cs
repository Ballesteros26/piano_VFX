using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200071B RID: 1819
	internal sealed class ObjectProgress
	{
		// Token: 0x06004BD5 RID: 19413 RVA: 0x0010E9F0 File Offset: 0x0010CBF0
		internal ObjectProgress()
		{
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0010EA0C File Offset: 0x0010CC0C
		[Conditional("SER_LOGGING")]
		private void Counter()
		{
			lock (this)
			{
				this.opRecordId = ObjectProgress.opRecordIdCount++;
				if (ObjectProgress.opRecordIdCount > 1000)
				{
					ObjectProgress.opRecordIdCount = 1;
				}
			}
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0010EA68 File Offset: 0x0010CC68
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0010EAF7 File Offset: 0x0010CCF7
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0010EB08 File Offset: 0x0010CD08
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x040027A4 RID: 10148
		internal static int opRecordIdCount = 1;

		// Token: 0x040027A5 RID: 10149
		internal int opRecordId;

		// Token: 0x040027A6 RID: 10150
		internal bool isInitial;

		// Token: 0x040027A7 RID: 10151
		internal int count;

		// Token: 0x040027A8 RID: 10152
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x040027A9 RID: 10153
		internal object expectedTypeInformation;

		// Token: 0x040027AA RID: 10154
		internal string name;

		// Token: 0x040027AB RID: 10155
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x040027AC RID: 10156
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x040027AD RID: 10157
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x040027AE RID: 10158
		internal Type dtType;

		// Token: 0x040027AF RID: 10159
		internal int numItems;

		// Token: 0x040027B0 RID: 10160
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x040027B1 RID: 10161
		internal object typeInformation;

		// Token: 0x040027B2 RID: 10162
		internal int nullCount;

		// Token: 0x040027B3 RID: 10163
		internal int memberLength;

		// Token: 0x040027B4 RID: 10164
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x040027B5 RID: 10165
		internal object[] typeInformationA;

		// Token: 0x040027B6 RID: 10166
		internal string[] memberNames;

		// Token: 0x040027B7 RID: 10167
		internal Type[] memberTypes;

		// Token: 0x040027B8 RID: 10168
		internal ParseRecord pr = new ParseRecord();
	}
}
