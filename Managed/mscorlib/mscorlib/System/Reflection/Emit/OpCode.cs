using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes a Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x0200036F RID: 879
	[ComVisible(true)]
	public struct OpCode
	{
		// Token: 0x06002842 RID: 10306 RVA: 0x0008E8AC File Offset: 0x0008CAAC
		internal OpCode(int p, int q)
		{
			this.op1 = (byte)(p & 255);
			this.op2 = (byte)((p >> 8) & 255);
			this.push = (byte)((p >> 16) & 255);
			this.pop = (byte)((p >> 24) & 255);
			this.size = (byte)(q & 255);
			this.type = (byte)((q >> 8) & 255);
			this.args = (byte)((q >> 16) & 255);
			this.flow = (byte)((q >> 24) & 255);
		}

		/// <summary>Returns the generated hash code for this Opcode.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06002843 RID: 10307 RVA: 0x0008E939 File Offset: 0x0008CB39
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Tests whether the given object is equal to this Opcode.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of Opcode and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x06002844 RID: 10308 RVA: 0x0008E948 File Offset: 0x0008CB48
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return opCode.op1 == this.op1 && opCode.op2 == this.op2;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.OpCode" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to the current instance.</param>
		// Token: 0x06002845 RID: 10309 RVA: 0x0008E987 File Offset: 0x0008CB87
		public bool Equals(OpCode obj)
		{
			return obj.op1 == this.op1 && obj.op2 == this.op2;
		}

		/// <summary>Returns this Opcode as a <see cref="T:System.String" />.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> containing the name of this Opcode.</returns>
		// Token: 0x06002846 RID: 10310 RVA: 0x0008E9A7 File Offset: 0x0008CBA7
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>The name of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The name of the MSIL instruction.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x0008E9AF File Offset: 0x0008CBAF
		public string Name
		{
			get
			{
				if (this.op1 == 255)
				{
					return OpCodeNames.names[(int)this.op2];
				}
				return OpCodeNames.names[256 + (int)this.op2];
			}
		}

		/// <summary>The size of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The size of the MSIL instruction.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x0008E9DD File Offset: 0x0008CBDD
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		/// <summary>The type of Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The type of Microsoft intermediate language (MSIL) instruction.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x0008E9E5 File Offset: 0x0008CBE5
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.type;
			}
		}

		/// <summary>The operand type of an Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The operand type of an MSIL instruction.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x0008E9ED File Offset: 0x0008CBED
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.args;
			}
		}

		/// <summary>The flow control characteristics of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The type of flow control.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x0008E9F5 File Offset: 0x0008CBF5
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pops the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pops the stack.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x0008E9FD File Offset: 0x0008CBFD
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.pop;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pushes operand onto the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pushes operand onto the stack.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0008EA05 File Offset: 0x0008CC05
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.push;
			}
		}

		/// <summary>The value of the immediate operand of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The value of the immediate operand of the MSIL instruction.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x0008EA0D File Offset: 0x0008CC0D
		public short Value
		{
			get
			{
				if (this.size == 1)
				{
					return (short)this.op2;
				}
				return (short)(((int)this.op1 << 8) | (int)this.op2);
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600284F RID: 10319 RVA: 0x0008EA2F File Offset: 0x0008CC2F
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.op1 == b.op1 && a.op2 == b.op2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002850 RID: 10320 RVA: 0x0008EA4F File Offset: 0x0008CC4F
		public static bool operator !=(OpCode a, OpCode b)
		{
			return a.op1 != b.op1 || a.op2 != b.op2;
		}

		// Token: 0x040014A3 RID: 5283
		internal byte op1;

		// Token: 0x040014A4 RID: 5284
		internal byte op2;

		// Token: 0x040014A5 RID: 5285
		private byte push;

		// Token: 0x040014A6 RID: 5286
		private byte pop;

		// Token: 0x040014A7 RID: 5287
		private byte size;

		// Token: 0x040014A8 RID: 5288
		private byte type;

		// Token: 0x040014A9 RID: 5289
		private byte args;

		// Token: 0x040014AA RID: 5290
		private byte flow;
	}
}
