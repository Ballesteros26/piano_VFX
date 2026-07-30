using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AF8 RID: 2808
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x0600650D RID: 25869 RVA: 0x0014B610 File Offset: 0x00149810
		public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
		{
			this.m_id = 0;
			this.m_version = 0;
			this.m_channel = 0;
			this.m_traceloggingId = traceloggingId;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_task = 0;
			this.m_keywords = keywords;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x0014B64C File Offset: 0x0014984C
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("Non-negative number required."));
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("The ID parameter must be in the range {0} through {1}.", new object[] { 1, ushort.MaxValue }));
			}
			this.m_traceloggingId = 0;
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("Non-negative number required."));
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("The ID parameter must be in the range {0} through {1}.", new object[] { 1, ushort.MaxValue }));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x0600650F RID: 25871 RVA: 0x0014B73D File Offset: 0x0014993D
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06006510 RID: 25872 RVA: 0x0014B745 File Offset: 0x00149945
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x0014B74D File Offset: 0x0014994D
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x0014B755 File Offset: 0x00149955
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x0014B75D File Offset: 0x0014995D
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06006514 RID: 25876 RVA: 0x0014B765 File Offset: 0x00149965
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06006515 RID: 25877 RVA: 0x0014B76D File Offset: 0x0014996D
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x0014B775 File Offset: 0x00149975
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x0014B78D File Offset: 0x0014998D
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x0014B7C0 File Offset: 0x001499C0
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x0014B832 File Offset: 0x00149A32
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x0014B83C File Offset: 0x00149A3C
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x0400322F RID: 12847
		[FieldOffset(0)]
		private int m_traceloggingId;

		// Token: 0x04003230 RID: 12848
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x04003231 RID: 12849
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x04003232 RID: 12850
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x04003233 RID: 12851
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x04003234 RID: 12852
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x04003235 RID: 12853
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x04003236 RID: 12854
		[FieldOffset(8)]
		private long m_keywords;
	}
}
