using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200006E RID: 110
	public abstract class MessageBase
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x000160F8 File Offset: 0x000142F8
		protected MessageBase(int messageType)
		{
			this._type = messageType;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00016107 File Offset: 0x00014307
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0001610F File Offset: 0x0001430F
		public NtlmFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00016118 File Offset: 0x00014318
		public int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00016120 File Offset: 0x00014320
		protected byte[] PrepareMessage(int messageSize)
		{
			byte[] array = new byte[messageSize];
			Buffer.BlockCopy(MessageBase.header, 0, array, 0, 8);
			array[8] = (byte)this._type;
			array[9] = (byte)(this._type >> 8);
			array[10] = (byte)(this._type >> 16);
			array[11] = (byte)(this._type >> 24);
			return array;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00016178 File Offset: 0x00014378
		protected virtual void Decode(byte[] message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (message.Length < 12)
			{
				string text = Locale.GetText("Minimum message length is 12 bytes.");
				throw new ArgumentOutOfRangeException("message", message.Length, text);
			}
			if (!this.CheckHeader(message))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid Type{0} message."), this._type), "message");
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000161E8 File Offset: 0x000143E8
		protected bool CheckHeader(byte[] message)
		{
			for (int i = 0; i < MessageBase.header.Length; i++)
			{
				if (message[i] != MessageBase.header[i])
				{
					return false;
				}
			}
			return (ulong)BitConverterLE.ToUInt32(message, 8) == (ulong)((long)this._type);
		}

		// Token: 0x06000430 RID: 1072
		public abstract byte[] GetBytes();

		// Token: 0x040001F9 RID: 505
		private static byte[] header = new byte[] { 78, 84, 76, 77, 83, 83, 80, 0 };

		// Token: 0x040001FA RID: 506
		private int _type;

		// Token: 0x040001FB RID: 507
		private NtlmFlags _flags;
	}
}
