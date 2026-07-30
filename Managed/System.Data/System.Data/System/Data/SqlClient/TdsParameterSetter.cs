using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000200 RID: 512
	internal class TdsParameterSetter : SmiTypedGetterSetter
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x00072378 File Offset: 0x00070578
		internal TdsParameterSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._target = new TdsRecordBufferSetter(stateObj, md);
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0000EF2B File Offset: 0x0000D12B
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0007238D File Offset: 0x0007058D
		internal override SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			return this._target;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00072395 File Offset: 0x00070595
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._target.EndElements(sink);
		}

		// Token: 0x040010B8 RID: 4280
		private TdsRecordBufferSetter _target;
	}
}
