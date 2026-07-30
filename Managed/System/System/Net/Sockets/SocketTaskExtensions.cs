using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x020005E6 RID: 1510
	public static class SocketTaskExtensions
	{
		// Token: 0x06002FE8 RID: 12264 RVA: 0x000BC91C File Offset: 0x000BAB1C
		public static Task<Socket> AcceptAsync(this Socket socket)
		{
			return Task<Socket>.Factory.FromAsync((AsyncCallback callback, object state) => ((Socket)state).BeginAccept(callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), socket);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000BC974 File Offset: 0x000BAB74
		public static Task<Socket> AcceptAsync(this Socket socket, Socket acceptSocket)
		{
			return Task<Socket>.Factory.FromAsync<Socket, int>((Socket socketForAccept, int receiveSize, AsyncCallback callback, object state) => ((Socket)state).BeginAccept(socketForAccept, receiveSize, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), acceptSocket, 0, socket);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000BC9CC File Offset: 0x000BABCC
		public static Task ConnectAsync(this Socket socket, EndPoint remoteEP)
		{
			return Task.Factory.FromAsync<EndPoint>((EndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, remoteEP, socket);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000BCA24 File Offset: 0x000BAC24
		public static Task ConnectAsync(this Socket socket, IPAddress address, int port)
		{
			return Task.Factory.FromAsync<IPAddress, int>((IPAddress targetAddress, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddress, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, address, port, socket);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000BCA7C File Offset: 0x000BAC7C
		public static Task ConnectAsync(this Socket socket, IPAddress[] addresses, int port)
		{
			return Task.Factory.FromAsync<IPAddress[], int>((IPAddress[] targetAddresses, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddresses, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, addresses, port, socket);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000BCAD4 File Offset: 0x000BACD4
		public static Task ConnectAsync(this Socket socket, string host, int port)
		{
			return Task.Factory.FromAsync<string, int>((string targetHost, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetHost, targetPort, callback, state), delegate(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}, host, port, socket);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000BCB2C File Offset: 0x000BAD2C
		public static Task<int> ReceiveAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffer, socketFlags, socket);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000BCB84 File Offset: 0x000BAD84
		public static Task<int> ReceiveAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<IList<ArraySegment<byte>>, SocketFlags>((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffers, socketFlags, socket);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000BCBDC File Offset: 0x000BADDC
		public static Task<SocketReceiveFromResult> ReceiveFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			object[] array = new object[] { socket, remoteEndPoint };
			return Task<SocketReceiveFromResult>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>(delegate(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
			{
				object[] array2 = (object[])state;
				Socket socket2 = (Socket)array2[0];
				EndPoint endPoint = (EndPoint)array2[1];
				IAsyncResult asyncResult2 = socket2.BeginReceiveFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, ref endPoint, callback, state);
				array2[1] = endPoint;
				return asyncResult2;
			}, delegate(IAsyncResult asyncResult)
			{
				object[] array3 = (object[])asyncResult.AsyncState;
				Socket socket3 = (Socket)array3[0];
				EndPoint endPoint2 = (EndPoint)array3[1];
				int num = socket3.EndReceiveFrom(asyncResult, ref endPoint2);
				return new SocketReceiveFromResult
				{
					ReceivedBytes = num,
					RemoteEndPoint = endPoint2
				};
			}, buffer, socketFlags, array);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000BCC44 File Offset: 0x000BAE44
		public static Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
		{
			object[] array = new object[] { socket, socketFlags, remoteEndPoint };
			return Task<SocketReceiveMessageFromResult>.Factory.FromAsync<ArraySegment<byte>>(delegate(ArraySegment<byte> targetBuffer, AsyncCallback callback, object state)
			{
				object[] array2 = (object[])state;
				Socket socket2 = (Socket)array2[0];
				SocketFlags socketFlags2 = (SocketFlags)array2[1];
				EndPoint endPoint = (EndPoint)array2[2];
				IAsyncResult asyncResult2 = socket2.BeginReceiveMessageFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, socketFlags2, ref endPoint, callback, state);
				array2[2] = endPoint;
				return asyncResult2;
			}, delegate(IAsyncResult asyncResult)
			{
				object[] array3 = (object[])asyncResult.AsyncState;
				Socket socket3 = (Socket)array3[0];
				SocketFlags socketFlags3 = (SocketFlags)array3[1];
				EndPoint endPoint2 = (EndPoint)array3[2];
				IPPacketInformation ippacketInformation;
				int num = socket3.EndReceiveMessageFrom(asyncResult, ref socketFlags3, ref endPoint2, out ippacketInformation);
				return new SocketReceiveMessageFromResult
				{
					PacketInformation = ippacketInformation,
					ReceivedBytes = num,
					RemoteEndPoint = endPoint2,
					SocketFlags = socketFlags3
				};
			}, buffer, array);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000BCCB4 File Offset: 0x000BAEB4
		public static Task<int> SendAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags>((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffer, socketFlags, socket);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000BCD0C File Offset: 0x000BAF0C
		public static Task<int> SendAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
		{
			return Task<int>.Factory.FromAsync<IList<ArraySegment<byte>>, SocketFlags>((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffers, socketFlags, socket);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000BCD64 File Offset: 0x000BAF64
		public static Task<int> SendToAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
		{
			return Task<int>.Factory.FromAsync<ArraySegment<byte>, SocketFlags, EndPoint>((ArraySegment<byte> targetBuffer, SocketFlags flags, EndPoint endPoint, AsyncCallback callback, object state) => ((Socket)state).BeginSendTo(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, endPoint, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSendTo(asyncResult), buffer, socketFlags, remoteEP, socket);
		}
	}
}
