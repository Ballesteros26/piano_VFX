using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200070D RID: 1805
	internal sealed class BinaryMethodCall
	{
		// Token: 0x06004B80 RID: 19328 RVA: 0x0010D1BC File Offset: 0x0010B3BC
		internal object[] WriteArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			this.uri = uri;
			this.methodName = methodName;
			this.typeName = typeName;
			this.instArgs = instArgs;
			this.args = args;
			this.methodSignature = methodSignature;
			this.callContext = callContext;
			this.properties = properties;
			int num = 0;
			if (args == null || args.Length == 0)
			{
				this.messageEnum = MessageEnum.NoArgs;
			}
			else
			{
				this.argTypes = new Type[args.Length];
				this.bArgsPrimitive = true;
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] != null)
					{
						this.argTypes[i] = args[i].GetType();
						if ((Converter.ToCode(this.argTypes[i]) <= InternalPrimitiveTypeE.Invalid && this.argTypes[i] != Converter.typeofString) || args[i] is ISerializable)
						{
							this.bArgsPrimitive = false;
							break;
						}
					}
				}
				if (this.bArgsPrimitive)
				{
					this.messageEnum = MessageEnum.ArgsInline;
				}
				else
				{
					num++;
					this.messageEnum = MessageEnum.ArgsInArray;
				}
			}
			if (instArgs != null)
			{
				num++;
				this.messageEnum |= MessageEnum.GenericMethod;
			}
			if (methodSignature != null)
			{
				num++;
				this.messageEnum |= MessageEnum.MethodSignatureInArray;
			}
			if (callContext == null)
			{
				this.messageEnum |= MessageEnum.NoContext;
			}
			else if (callContext is string)
			{
				this.messageEnum |= MessageEnum.ContextInline;
			}
			else
			{
				num++;
				this.messageEnum |= MessageEnum.ContextInArray;
			}
			if (properties != null)
			{
				num++;
				this.messageEnum |= MessageEnum.PropertyInArray;
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray) && num == 1)
			{
				this.messageEnum ^= MessageEnum.ArgsInArray;
				this.messageEnum |= MessageEnum.ArgsIsArray;
				return args;
			}
			if (num > 0)
			{
				int num2 = 0;
				this.callA = new object[num];
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					this.callA[num2++] = args;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
				{
					this.callA[num2++] = instArgs;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
				{
					this.callA[num2++] = methodSignature;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					this.callA[num2++] = callContext;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					this.callA[num2] = properties;
				}
				return this.callA;
			}
			return null;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x0010D420 File Offset: 0x0010B620
		internal void Write(__BinaryWriter sout)
		{
			sout.WriteByte(21);
			sout.WriteInt32((int)this.messageEnum);
			IOUtil.WriteStringWithCode(this.methodName, sout);
			IOUtil.WriteStringWithCode(this.typeName, sout);
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
			{
				IOUtil.WriteStringWithCode((string)this.callContext, sout);
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
			{
				sout.WriteInt32(this.args.Length);
				for (int i = 0; i < this.args.Length; i++)
				{
					IOUtil.WriteWithCode(this.argTypes[i], this.args[i], sout);
				}
			}
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x0010D4C0 File Offset: 0x0010B6C0
		[SecurityCritical]
		internal void Read(__BinaryParser input)
		{
			this.messageEnum = (MessageEnum)input.ReadInt32();
			this.methodName = (string)IOUtil.ReadWithCode(input);
			this.typeName = (string)IOUtil.ReadWithCode(input);
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
			{
				this.scallContext = (string)IOUtil.ReadWithCode(input);
				this.callContext = new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = this.scallContext
					}
				};
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
			{
				this.args = IOUtil.ReadArgs(input);
			}
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0010D554 File Offset: 0x0010B754
		[SecurityCritical]
		internal IMethodCallMessage ReadArray(object[] callA, object handlerObject)
		{
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
			{
				this.args = callA;
			}
			else
			{
				int num = 0;
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.args = (object[])callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.instArgs = (Type[])callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.methodSignature = callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.callContext = callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.properties = callA[num++];
				}
			}
			return new MethodCall(handlerObject, new BinaryMethodCallMessage(this.uri, this.methodName, this.typeName, this.instArgs, this.args, this.methodSignature, (LogicalCallContext)this.callContext, (object[])this.properties));
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x00002194 File Offset: 0x00000394
		internal void Dump()
		{
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0010D6D0 File Offset: 0x0010B8D0
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
				{
					string text = this.callContext as string;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
				{
					for (int i = 0; i < this.args.Length; i++)
					{
					}
				}
			}
		}

		// Token: 0x0400275D RID: 10077
		private string uri;

		// Token: 0x0400275E RID: 10078
		private string methodName;

		// Token: 0x0400275F RID: 10079
		private string typeName;

		// Token: 0x04002760 RID: 10080
		private Type[] instArgs;

		// Token: 0x04002761 RID: 10081
		private object[] args;

		// Token: 0x04002762 RID: 10082
		private object methodSignature;

		// Token: 0x04002763 RID: 10083
		private object callContext;

		// Token: 0x04002764 RID: 10084
		private string scallContext;

		// Token: 0x04002765 RID: 10085
		private object properties;

		// Token: 0x04002766 RID: 10086
		private Type[] argTypes;

		// Token: 0x04002767 RID: 10087
		private bool bArgsPrimitive = true;

		// Token: 0x04002768 RID: 10088
		private MessageEnum messageEnum;

		// Token: 0x04002769 RID: 10089
		private object[] callA;
	}
}
