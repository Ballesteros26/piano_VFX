using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200035F RID: 863
	internal struct ILExceptionInfo
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x00089428 File Offset: 0x00087628
		internal int NumHandlers()
		{
			return this.handlers.Length;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00089434 File Offset: 0x00087634
		internal void AddCatch(Type extype, int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 0;
			this.handlers[num].start = offset;
			this.handlers[num].extype = extype;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x00089490 File Offset: 0x00087690
		internal void AddFinally(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 2;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000894EC File Offset: 0x000876EC
		internal void AddFault(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 4;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00089548 File Offset: 0x00087748
		internal void AddFilter(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = -1;
			this.handlers[num].extype = null;
			this.handlers[num].filter_offset = offset;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000895A4 File Offset: 0x000877A4
		internal void End(int offset)
		{
			if (this.handlers == null)
			{
				return;
			}
			int num = this.handlers.Length - 1;
			if (num >= 0)
			{
				this.handlers[num].len = offset - this.handlers[num].start;
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000895ED File Offset: 0x000877ED
		internal int LastClauseType()
		{
			if (this.handlers != null)
			{
				return this.handlers[this.handlers.Length - 1].type;
			}
			return 0;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00089614 File Offset: 0x00087814
		internal void PatchFilterClause(int start)
		{
			if (this.handlers != null && this.handlers.Length != 0)
			{
				this.handlers[this.handlers.Length - 1].start = start;
				this.handlers[this.handlers.Length - 1].type = 1;
			}
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00002194 File Offset: 0x00000394
		internal void Debug(int b)
		{
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00089668 File Offset: 0x00087868
		private void add_block(int offset)
		{
			if (this.handlers != null)
			{
				int num = this.handlers.Length;
				ILExceptionBlock[] array = new ILExceptionBlock[num + 1];
				Array.Copy(this.handlers, array, num);
				this.handlers = array;
				this.handlers[num].len = offset - this.handlers[num].start;
				return;
			}
			this.handlers = new ILExceptionBlock[1];
			this.len = offset - this.start;
		}

		// Token: 0x04001434 RID: 5172
		internal ILExceptionBlock[] handlers;

		// Token: 0x04001435 RID: 5173
		internal int start;

		// Token: 0x04001436 RID: 5174
		internal int len;

		// Token: 0x04001437 RID: 5175
		internal Label end;
	}
}
