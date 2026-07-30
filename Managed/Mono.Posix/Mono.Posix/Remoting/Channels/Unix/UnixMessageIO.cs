using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008E RID: 142
	internal class UnixMessageIO
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public static MessageStatus ReceiveMessageStatus(Stream networkStream, byte[] buffer)
		{
			try
			{
				UnixMessageIO.StreamRead(networkStream, buffer, 6);
			}
			catch (Exception ex)
			{
				throw new RemotingException("Unix transport error.", ex);
			}
			MessageStatus messageStatus;
			try
			{
				bool[] array = new bool[UnixMessageIO._msgHeaders.Length];
				bool flag = true;
				int num = 0;
				while (flag)
				{
					flag = false;
					byte b = buffer[num];
					for (int i = 0; i < UnixMessageIO._msgHeaders.Length; i++)
					{
						if (num <= 0 || array[i])
						{
							array[i] = b == UnixMessageIO._msgHeaders[i][num];
							if (array[i] && num == UnixMessageIO._msgHeaders[i].Length - 1)
							{
								return (MessageStatus)i;
							}
							flag = flag || array[i];
						}
					}
					num++;
				}
				messageStatus = MessageStatus.Unknown;
			}
			catch (Exception ex2)
			{
				throw new RemotingException("Unix transport error.", ex2);
			}
			return messageStatus;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000F48C File Offset: 0x0000D68C
		private static bool StreamRead(Stream networkStream, byte[] buffer, int count)
		{
			int num = 0;
			for (;;)
			{
				int num2 = networkStream.Read(buffer, num, count - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				if (num >= count)
				{
					return true;
				}
			}
			throw new RemotingException("Connection closed");
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public static void SendMessageStream(Stream networkStream, Stream data, ITransportHeaders requestHeaders, byte[] buffer)
		{
			if (buffer == null)
			{
				buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
			}
			byte[] array = UnixMessageIO._msgHeaders[0];
			networkStream.Write(array, 0, array.Length);
			if (requestHeaders["__RequestUri"] != null)
			{
				buffer[0] = 0;
			}
			else
			{
				buffer[0] = 2;
			}
			buffer[1] = 0;
			buffer[2] = 0;
			buffer[3] = 0;
			int num = (int)data.Length;
			buffer[4] = (byte)num;
			buffer[5] = (byte)(num >> 8);
			buffer[6] = (byte)(num >> 16);
			buffer[7] = (byte)(num >> 24);
			networkStream.Write(buffer, 0, 8);
			UnixMessageIO.SendHeaders(networkStream, requestHeaders, buffer);
			if (data is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)data;
				networkStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
				return;
			}
			for (int i = data.Read(buffer, 0, buffer.Length); i > 0; i = data.Read(buffer, 0, buffer.Length))
			{
				networkStream.Write(buffer, 0, i);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000F594 File Offset: 0x0000D794
		private static void SendHeaders(Stream networkStream, ITransportHeaders requestHeaders, byte[] buffer)
		{
			if (networkStream != null)
			{
				foreach (object obj in requestHeaders)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = dictionaryEntry.Key.ToString();
					if (!(text == "__RequestUri"))
					{
						if (!(text == "Content-Type"))
						{
							networkStream.Write(UnixMessageIO.msgDefaultTransportKey, 0, 3);
							UnixMessageIO.SendString(networkStream, dictionaryEntry.Key.ToString(), buffer);
							networkStream.WriteByte(1);
						}
						else
						{
							networkStream.Write(UnixMessageIO.msgContentTypeTransportKey, 0, 4);
						}
					}
					else
					{
						networkStream.Write(UnixMessageIO.msgUriTransportKey, 0, 4);
					}
					UnixMessageIO.SendString(networkStream, dictionaryEntry.Value.ToString(), buffer);
				}
			}
			networkStream.Write(UnixMessageIO.msgHeaderTerminator, 0, 2);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000F658 File Offset: 0x0000D858
		public static ITransportHeaders ReceiveHeaders(Stream networkStream, byte[] buffer)
		{
			UnixMessageIO.StreamRead(networkStream, buffer, 2);
			byte b = buffer[0];
			TransportHeaders transportHeaders = new TransportHeaders();
			while (b != 0)
			{
				UnixMessageIO.StreamRead(networkStream, buffer, 1);
				string text;
				if (b != 1)
				{
					if (b != 4)
					{
						if (b != 6)
						{
							throw new NotSupportedException("Unknown header code: " + b);
						}
						text = "Content-Type";
					}
					else
					{
						text = "__RequestUri";
					}
				}
				else
				{
					text = UnixMessageIO.ReceiveString(networkStream, buffer);
				}
				UnixMessageIO.StreamRead(networkStream, buffer, 1);
				transportHeaders[text] = UnixMessageIO.ReceiveString(networkStream, buffer);
				UnixMessageIO.StreamRead(networkStream, buffer, 2);
				b = buffer[0];
			}
			return transportHeaders;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public static Stream ReceiveMessageStream(Stream networkStream, out ITransportHeaders headers, byte[] buffer)
		{
			headers = null;
			if (buffer == null)
			{
				buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
			}
			UnixMessageIO.StreamRead(networkStream, buffer, 8);
			int num = (int)buffer[4] | ((int)buffer[5] << 8) | ((int)buffer[6] << 16) | ((int)buffer[7] << 24);
			headers = UnixMessageIO.ReceiveHeaders(networkStream, buffer);
			byte[] array = new byte[num];
			UnixMessageIO.StreamRead(networkStream, array, num);
			return new MemoryStream(array);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000F74C File Offset: 0x0000D94C
		private static void SendString(Stream networkStream, string str, byte[] buffer)
		{
			int num = Encoding.UTF8.GetMaxByteCount(str.Length) + 4;
			if (num > buffer.Length)
			{
				buffer = new byte[num];
			}
			int bytes = Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 4);
			buffer[0] = (byte)bytes;
			buffer[1] = (byte)(bytes >> 8);
			buffer[2] = (byte)(bytes >> 16);
			buffer[3] = (byte)(bytes >> 24);
			networkStream.Write(buffer, 0, bytes + 4);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
		private static string ReceiveString(Stream networkStream, byte[] buffer)
		{
			UnixMessageIO.StreamRead(networkStream, buffer, 4);
			int num = (int)buffer[0] | ((int)buffer[1] << 8) | ((int)buffer[2] << 16) | ((int)buffer[3] << 24);
			if (num == 0)
			{
				return string.Empty;
			}
			if (num > buffer.Length)
			{
				buffer = new byte[num];
			}
			UnixMessageIO.StreamRead(networkStream, buffer, num);
			return new string(Encoding.UTF8.GetChars(buffer, 0, num));
		}

		// Token: 0x040004C4 RID: 1220
		private static byte[][] _msgHeaders = new byte[][]
		{
			new byte[] { 46, 78, 69, 84, 1, 0 },
			new byte[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue }
		};

		// Token: 0x040004C5 RID: 1221
		public static int DefaultStreamBufferSize = 1000;

		// Token: 0x040004C6 RID: 1222
		private static byte[] msgUriTransportKey = new byte[] { 4, 0, 1, 1 };

		// Token: 0x040004C7 RID: 1223
		private static byte[] msgContentTypeTransportKey = new byte[] { 6, 0, 1, 1 };

		// Token: 0x040004C8 RID: 1224
		private static byte[] msgDefaultTransportKey = new byte[] { 1, 0, 1 };

		// Token: 0x040004C9 RID: 1225
		private static byte[] msgHeaderTerminator = new byte[2];
	}
}
