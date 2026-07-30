using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000B0 RID: 176
	internal sealed class ExpressionParser
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00030678 File Offset: 0x0002E878
		internal ExpressionParser(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000306D4 File Offset: 0x0002E8D4
		internal void LoadExpression(string data)
		{
			int num;
			if (data == null)
			{
				num = 0;
				this._text = new char[num + 1];
			}
			else
			{
				num = data.Length;
				this._text = new char[num + 1];
				data.CopyTo(0, this._text, 0, num);
			}
			this._text[num] = '\0';
			if (this._expression != null)
			{
				this._expression = null;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00030734 File Offset: 0x0002E934
		internal void StartScan()
		{
			this._op = 0;
			this._pos = 0;
			this._start = 0;
			this._topOperator = 0;
			OperatorInfo[] ops = this._ops;
			int topOperator = this._topOperator;
			this._topOperator = topOperator + 1;
			ops[topOperator] = new OperatorInfo(Nodes.Noop, 0, 0);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00030780 File Offset: 0x0002E980
		internal ExpressionNode Parse()
		{
			this._expression = null;
			this.StartScan();
			int num = 0;
			while (this._token != Tokens.EOS)
			{
				OperatorInfo operatorInfo;
				for (;;)
				{
					this.Scan();
					int num2;
					switch (this._token)
					{
					case Tokens.Name:
					case Tokens.Numeric:
					case Tokens.Decimal:
					case Tokens.Float:
					case Tokens.StringConst:
					case Tokens.Date:
					case Tokens.Parent:
					{
						ExpressionNode expressionNode = null;
						if (this._prevOperand != 0)
						{
							goto Block_5;
						}
						if (this._topOperator > 0)
						{
							operatorInfo = this._ops[this._topOperator - 1];
							if (operatorInfo._type == Nodes.Binop && operatorInfo._op == 5 && this._token != Tokens.Parent)
							{
								goto Block_9;
							}
						}
						this._prevOperand = 1;
						Tokens token = this._token;
						switch (token)
						{
						case Tokens.Name:
							operatorInfo = this._ops[this._topOperator - 1];
							expressionNode = new NameNode(this._table, this._text, this._start, this._pos);
							break;
						case Tokens.Numeric:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Numeric, text);
							break;
						}
						case Tokens.Decimal:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Decimal, text);
							break;
						}
						case Tokens.Float:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Float, text);
							break;
						}
						case Tokens.BinaryConst:
							break;
						case Tokens.StringConst:
						{
							string text = new string(this._text, this._start + 1, this._pos - this._start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Str, text);
							break;
						}
						case Tokens.Date:
						{
							string text = new string(this._text, this._start + 1, this._pos - this._start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Date, text);
							break;
						}
						default:
							if (token == Tokens.Parent)
							{
								string text2;
								try
								{
									this.Scan();
									if (this._token == Tokens.LeftParen)
									{
										this.ScanToken(Tokens.Name);
										text2 = NameNode.ParseName(this._text, this._start, this._pos);
										this.ScanToken(Tokens.RightParen);
										this.ScanToken(Tokens.Dot);
									}
									else
									{
										text2 = null;
										this.CheckToken(Tokens.Dot);
									}
								}
								catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
								{
									throw ExprException.LookupArgument();
								}
								this.ScanToken(Tokens.Name);
								string text3 = NameNode.ParseName(this._text, this._start, this._pos);
								operatorInfo = this._ops[this._topOperator - 1];
								expressionNode = new LookupNode(this._table, text3, text2);
							}
							break;
						}
						this.NodePush(expressionNode);
						continue;
					}
					case Tokens.ListSeparator:
					{
						if (this._prevOperand == 0)
						{
							goto Block_23;
						}
						this.BuildExpression(3);
						operatorInfo = this._ops[this._topOperator - 1];
						if (operatorInfo._type != Nodes.Call)
						{
							goto Block_24;
						}
						ExpressionNode expressionNode2 = this.NodePop();
						FunctionNode functionNode = (FunctionNode)this.NodePop();
						functionNode.AddArgument(expressionNode2);
						this.NodePush(functionNode);
						this._prevOperand = 0;
						continue;
					}
					case Tokens.LeftParen:
						num++;
						if (this._prevOperand == 0)
						{
							operatorInfo = this._ops[this._topOperator - 1];
							if (operatorInfo._type == Nodes.Binop && operatorInfo._op == 5)
							{
								ExpressionNode expressionNode = new FunctionNode(this._table, "In");
								this.NodePush(expressionNode);
								OperatorInfo[] ops = this._ops;
								num2 = this._topOperator;
								this._topOperator = num2 + 1;
								ops[num2] = new OperatorInfo(Nodes.Call, 0, 2);
								continue;
							}
							OperatorInfo[] ops2 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops2[num2] = new OperatorInfo(Nodes.Paren, 0, 2);
							continue;
						}
						else
						{
							this.BuildExpression(22);
							this._prevOperand = 0;
							ExpressionNode expressionNode3 = this.NodePeek();
							if (expressionNode3 == null || expressionNode3.GetType() != typeof(NameNode))
							{
								goto IL_0420;
							}
							NameNode nameNode = (NameNode)this.NodePop();
							ExpressionNode expressionNode = new FunctionNode(this._table, nameNode._name);
							Aggregate aggregate = (Aggregate)((FunctionNode)expressionNode).Aggregate;
							if (aggregate != Aggregate.None)
							{
								expressionNode = this.ParseAggregateArgument((FunctionId)aggregate);
								this.NodePush(expressionNode);
								this._prevOperand = 2;
								continue;
							}
							this.NodePush(expressionNode);
							OperatorInfo[] ops3 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops3[num2] = new OperatorInfo(Nodes.Call, 0, 2);
							continue;
						}
						break;
					case Tokens.RightParen:
						if (this._prevOperand != 0)
						{
							this.BuildExpression(3);
						}
						if (this._topOperator <= 1)
						{
							goto Block_18;
						}
						this._topOperator--;
						operatorInfo = this._ops[this._topOperator];
						if (this._prevOperand == 0 && operatorInfo._type != Nodes.Call)
						{
							goto Block_20;
						}
						if (operatorInfo._type == Nodes.Call)
						{
							if (this._prevOperand != 0)
							{
								ExpressionNode expressionNode4 = this.NodePop();
								FunctionNode functionNode2 = (FunctionNode)this.NodePop();
								functionNode2.AddArgument(expressionNode4);
								functionNode2.Check();
								this.NodePush(functionNode2);
							}
						}
						else
						{
							ExpressionNode expressionNode = this.NodePop();
							expressionNode = new UnaryNode(this._table, 0, expressionNode);
							this.NodePush(expressionNode);
						}
						this._prevOperand = 2;
						num--;
						continue;
					case Tokens.ZeroOp:
					{
						if (this._prevOperand != 0)
						{
							goto Block_28;
						}
						OperatorInfo[] ops4 = this._ops;
						num2 = this._topOperator;
						this._topOperator = num2 + 1;
						ops4[num2] = new OperatorInfo(Nodes.Zop, this._op, 24);
						this._prevOperand = 2;
						continue;
					}
					case Tokens.UnaryOp:
						goto IL_065A;
					case Tokens.BinaryOp:
						if (this._prevOperand != 0)
						{
							this._prevOperand = 0;
							this.BuildExpression(Operators.Priority(this._op));
							OperatorInfo[] ops5 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops5[num2] = new OperatorInfo(Nodes.Binop, this._op, Operators.Priority(this._op));
							continue;
						}
						if (this._op == 15)
						{
							this._op = 2;
							goto IL_065A;
						}
						if (this._op == 16)
						{
							this._op = 1;
							goto IL_065A;
						}
						goto IL_05FA;
					case Tokens.Dot:
					{
						ExpressionNode expressionNode5 = this.NodePeek();
						if (expressionNode5 != null && expressionNode5.GetType() == typeof(NameNode))
						{
							this.Scan();
							if (this._token == Tokens.Name)
							{
								string text4 = ((NameNode)this.NodePop())._name + "." + NameNode.ParseName(this._text, this._start, this._pos);
								this.NodePush(new NameNode(this._table, text4));
								continue;
							}
						}
						break;
					}
					case Tokens.EOS:
						goto IL_0077;
					}
					goto Block_1;
					IL_065A:
					OperatorInfo[] ops6 = this._ops;
					num2 = this._topOperator;
					this._topOperator = num2 + 1;
					ops6[num2] = new OperatorInfo(Nodes.Unop, this._op, Operators.Priority(this._op));
				}
				IL_0077:
				if (this._prevOperand == 0)
				{
					if (this._topNode != 0)
					{
						operatorInfo = this._ops[this._topOperator - 1];
						throw ExprException.MissingOperand(operatorInfo);
					}
					continue;
				}
				else
				{
					this.BuildExpression(3);
					if (this._topOperator != 1)
					{
						throw ExprException.MissingRightParen();
					}
					continue;
				}
				Block_1:
				goto IL_0771;
				Block_5:
				throw ExprException.MissingOperator(new string(this._text, this._start, this._pos - this._start));
				Block_9:
				throw ExprException.InWithoutParentheses();
				IL_0420:
				throw ExprException.SyntaxError();
				Block_18:
				throw ExprException.TooManyRightParentheses();
				Block_20:
				throw ExprException.MissingOperand(operatorInfo);
				Block_23:
				throw ExprException.MissingOperandBefore(",");
				Block_24:
				throw ExprException.SyntaxError();
				IL_05FA:
				throw ExprException.MissingOperandBefore(Operators.ToString(this._op));
				Block_28:
				throw ExprException.MissingOperator(new string(this._text, this._start, this._pos - this._start));
				IL_0771:
				throw ExprException.UnknownToken(new string(this._text, this._start, this._pos - this._start), this._start + 1);
			}
			this._expression = this._nodeStack[0];
			return this._expression;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00030F5C File Offset: 0x0002F15C
		private ExpressionNode ParseAggregateArgument(FunctionId aggregate)
		{
			this.Scan();
			string text;
			bool flag;
			string text2;
			try
			{
				if (this._token != Tokens.Child)
				{
					if (this._token != Tokens.Name)
					{
						throw ExprException.AggregateArgument();
					}
					text = NameNode.ParseName(this._text, this._start, this._pos);
					this.ScanToken(Tokens.RightParen);
					return new AggregateNode(this._table, aggregate, text);
				}
				else
				{
					flag = this._token == Tokens.Child;
					this._prevOperand = 1;
					this.Scan();
					if (this._token == Tokens.LeftParen)
					{
						this.ScanToken(Tokens.Name);
						text2 = NameNode.ParseName(this._text, this._start, this._pos);
						this.ScanToken(Tokens.RightParen);
						this.ScanToken(Tokens.Dot);
					}
					else
					{
						text2 = null;
						this.CheckToken(Tokens.Dot);
					}
					this.ScanToken(Tokens.Name);
					text = NameNode.ParseName(this._text, this._start, this._pos);
					this.ScanToken(Tokens.RightParen);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExprException.AggregateArgument();
			}
			return new AggregateNode(this._table, aggregate, text, !flag, text2);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00031084 File Offset: 0x0002F284
		private ExpressionNode NodePop()
		{
			ExpressionNode[] nodeStack = this._nodeStack;
			int num = this._topNode - 1;
			this._topNode = num;
			return nodeStack[num];
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000310A9 File Offset: 0x0002F2A9
		private ExpressionNode NodePeek()
		{
			if (this._topNode <= 0)
			{
				return null;
			}
			return this._nodeStack[this._topNode - 1];
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000310C8 File Offset: 0x0002F2C8
		private void NodePush(ExpressionNode node)
		{
			if (this._topNode >= 98)
			{
				throw ExprException.ExpressionTooComplex();
			}
			ExpressionNode[] nodeStack = this._nodeStack;
			int topNode = this._topNode;
			this._topNode = topNode + 1;
			nodeStack[topNode] = node;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00031100 File Offset: 0x0002F300
		private void BuildExpression(int pri)
		{
			OperatorInfo operatorInfo;
			for (;;)
			{
				operatorInfo = this._ops[this._topOperator - 1];
				if (operatorInfo._priority < pri)
				{
					return;
				}
				this._topOperator--;
				ExpressionNode expressionNode2;
				switch (operatorInfo._type)
				{
				case Nodes.Unop:
				{
					ExpressionNode expressionNode = this.NodePop();
					int op = operatorInfo._op;
					if (op != 1 && op != 3 && op == 25)
					{
						goto Block_6;
					}
					expressionNode2 = new UnaryNode(this._table, operatorInfo._op, expressionNode);
					goto IL_0163;
				}
				case Nodes.UnopSpec:
				case Nodes.BinopSpec:
					return;
				case Nodes.Binop:
				{
					ExpressionNode expressionNode = this.NodePop();
					ExpressionNode expressionNode3 = this.NodePop();
					switch (operatorInfo._op)
					{
					case 4:
					case 6:
					case 22:
					case 23:
					case 24:
					case 25:
						goto IL_00D3;
					}
					if (operatorInfo._op == 14)
					{
						expressionNode2 = new LikeNode(this._table, operatorInfo._op, expressionNode3, expressionNode);
						goto IL_0163;
					}
					expressionNode2 = new BinaryNode(this._table, operatorInfo._op, expressionNode3, expressionNode);
					goto IL_0163;
				}
				case Nodes.Zop:
					expressionNode2 = new ZeroOpNode(operatorInfo._op);
					goto IL_0163;
				}
				break;
				IL_0163:
				this.NodePush(expressionNode2);
			}
			return;
			IL_00D3:
			throw ExprException.UnsupportedOperator(operatorInfo._op);
			Block_6:
			throw ExprException.UnsupportedOperator(operatorInfo._op);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0003127C File Offset: 0x0002F47C
		internal void CheckToken(Tokens token)
		{
			if (this._token != token)
			{
				throw ExprException.UnknownToken(token, this._token, this._pos);
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0003129C File Offset: 0x0002F49C
		internal Tokens Scan()
		{
			char[] text = this._text;
			this._token = Tokens.None;
			char c;
			for (;;)
			{
				this._start = this._pos;
				this._op = 0;
				char[] array = text;
				int pos = this._pos;
				this._pos = pos + 1;
				c = array[pos];
				if (c > '>')
				{
					goto IL_00CD;
				}
				if (c > '\r')
				{
					switch (c)
					{
					case ' ':
						goto IL_0111;
					case '!':
					case '"':
					case '$':
					case ',':
					case '.':
						goto IL_0311;
					case '#':
						goto IL_0136;
					case '%':
						goto IL_026E;
					case '&':
						goto IL_0283;
					case '\'':
						goto IL_0148;
					case '(':
						goto IL_011C;
					case ')':
						goto IL_0129;
					case '*':
						goto IL_0244;
					case '+':
						goto IL_021A;
					case '-':
						goto IL_022F;
					case '/':
						goto IL_0259;
					}
					goto Block_5;
				}
				if (c != '\0')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_0111;
					}
					break;
				}
				goto IL_0104;
				IL_0111:
				this.ScanWhite();
			}
			goto IL_0311;
			Block_5:
			switch (c)
			{
			case '<':
				this._token = Tokens.BinaryOp;
				this.ScanWhite();
				if (text[this._pos] == '=')
				{
					this._pos++;
					this._op = 11;
					goto IL_03E5;
				}
				if (text[this._pos] == '>')
				{
					this._pos++;
					this._op = 12;
					goto IL_03E5;
				}
				this._op = 9;
				goto IL_03E5;
			case '=':
				this._token = Tokens.BinaryOp;
				this._op = 7;
				goto IL_03E5;
			case '>':
				this._token = Tokens.BinaryOp;
				this.ScanWhite();
				if (text[this._pos] == '=')
				{
					this._pos++;
					this._op = 10;
					goto IL_03E5;
				}
				this._op = 8;
				goto IL_03E5;
			default:
				goto IL_0311;
			}
			IL_00CD:
			if (c <= '^')
			{
				if (c == '[')
				{
					this.ScanName(']', this._escape, "]\\");
					this.CheckToken(Tokens.Name);
					goto IL_03E5;
				}
				if (c != '^')
				{
					goto IL_0311;
				}
				this._token = Tokens.BinaryOp;
				this._op = 24;
				goto IL_03E5;
			}
			else
			{
				if (c == '`')
				{
					this.ScanName('`', '`', "`");
					this.CheckToken(Tokens.Name);
					goto IL_03E5;
				}
				if (c == '|')
				{
					this._token = Tokens.BinaryOp;
					this._op = 23;
					goto IL_03E5;
				}
				if (c != '~')
				{
					goto IL_0311;
				}
				this._token = Tokens.BinaryOp;
				this._op = 25;
				goto IL_03E5;
			}
			IL_0104:
			this._token = Tokens.EOS;
			goto IL_03E5;
			IL_011C:
			this._token = Tokens.LeftParen;
			goto IL_03E5;
			IL_0129:
			this._token = Tokens.RightParen;
			goto IL_03E5;
			IL_0136:
			this.ScanDate();
			this.CheckToken(Tokens.Date);
			goto IL_03E5;
			IL_0148:
			this.ScanString('\'');
			this.CheckToken(Tokens.StringConst);
			goto IL_03E5;
			IL_021A:
			this._token = Tokens.BinaryOp;
			this._op = 15;
			goto IL_03E5;
			IL_022F:
			this._token = Tokens.BinaryOp;
			this._op = 16;
			goto IL_03E5;
			IL_0244:
			this._token = Tokens.BinaryOp;
			this._op = 17;
			goto IL_03E5;
			IL_0259:
			this._token = Tokens.BinaryOp;
			this._op = 18;
			goto IL_03E5;
			IL_026E:
			this._token = Tokens.BinaryOp;
			this._op = 20;
			goto IL_03E5;
			IL_0283:
			this._token = Tokens.BinaryOp;
			this._op = 22;
			goto IL_03E5;
			IL_0311:
			if (c == this._listSeparator)
			{
				this._token = Tokens.ListSeparator;
			}
			else if (c == '.')
			{
				if (this._prevOperand == 0)
				{
					this.ScanNumeric();
				}
				else
				{
					this._token = Tokens.Dot;
				}
			}
			else if (c == '0' && (text[this._pos] == 'x' || text[this._pos] == 'X'))
			{
				this.ScanBinaryConstant();
				this._token = Tokens.BinaryConst;
			}
			else if (this.IsDigit(c))
			{
				this.ScanNumeric();
			}
			else
			{
				this.ScanReserved();
				if (this._token == Tokens.None)
				{
					if (this.IsAlphaNumeric(c))
					{
						this.ScanName();
						if (this._token != Tokens.None)
						{
							this.CheckToken(Tokens.Name);
							goto IL_03E5;
						}
					}
					this._token = Tokens.Unknown;
					throw ExprException.UnknownToken(new string(text, this._start, this._pos - this._start), this._start + 1);
				}
			}
			IL_03E5:
			return this._token;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00031694 File Offset: 0x0002F894
		private void ScanNumeric()
		{
			char[] text = this._text;
			bool flag = false;
			bool flag2 = false;
			while (this.IsDigit(text[this._pos]))
			{
				this._pos++;
			}
			if (text[this._pos] == this._decimalSeparator)
			{
				flag = true;
				this._pos++;
			}
			while (this.IsDigit(text[this._pos]))
			{
				this._pos++;
			}
			if (text[this._pos] == this._exponentL || text[this._pos] == this._exponentU)
			{
				flag2 = true;
				this._pos++;
				if (text[this._pos] == '-' || text[this._pos] == '+')
				{
					this._pos++;
				}
				while (this.IsDigit(text[this._pos]))
				{
					this._pos++;
				}
			}
			if (flag2)
			{
				this._token = Tokens.Float;
				return;
			}
			if (flag)
			{
				this._token = Tokens.Decimal;
				return;
			}
			this._token = Tokens.Numeric;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000317A0 File Offset: 0x0002F9A0
		private void ScanName()
		{
			char[] text = this._text;
			while (this.IsAlphaNumeric(text[this._pos]))
			{
				this._pos++;
			}
			this._token = Tokens.Name;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000317DC File Offset: 0x0002F9DC
		private void ScanName(char chEnd, char esc, string charsToEscape)
		{
			char[] text = this._text;
			do
			{
				if (text[this._pos] == esc && this._pos + 1 < text.Length && charsToEscape.IndexOf(text[this._pos + 1]) >= 0)
				{
					this._pos++;
				}
				this._pos++;
			}
			while (this._pos < text.Length && text[this._pos] != chEnd);
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidNameBracketing(new string(text, this._start, this._pos - 1 - this._start));
			}
			this._pos++;
			this._token = Tokens.Name;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00031890 File Offset: 0x0002FA90
		private void ScanDate()
		{
			char[] text = this._text;
			do
			{
				this._pos++;
			}
			while (this._pos < text.Length && text[this._pos] != '#');
			if (this._pos < text.Length && text[this._pos] == '#')
			{
				this._token = Tokens.Date;
				this._pos++;
				return;
			}
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidDate(new string(text, this._start, this._pos - 1 - this._start));
			}
			throw ExprException.InvalidDate(new string(text, this._start, this._pos - this._start));
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00031940 File Offset: 0x0002FB40
		private void ScanBinaryConstant()
		{
			char[] text = this._text;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0003194C File Offset: 0x0002FB4C
		private void ScanReserved()
		{
			char[] text = this._text;
			if (this.IsAlpha(text[this._pos]))
			{
				this.ScanName();
				string text2 = new string(text, this._start, this._pos - this._start);
				CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
				int num = 0;
				int num2 = ExpressionParser.s_reservedwords.Length - 1;
				int num3;
				for (;;)
				{
					num3 = (num + num2) / 2;
					int num4 = compareInfo.Compare(ExpressionParser.s_reservedwords[num3]._word, text2, CompareOptions.IgnoreCase);
					if (num4 == 0)
					{
						break;
					}
					if (num4 < 0)
					{
						num = num3 + 1;
					}
					else
					{
						num2 = num3 - 1;
					}
					if (num > num2)
					{
						return;
					}
				}
				this._token = ExpressionParser.s_reservedwords[num3]._token;
				this._op = ExpressionParser.s_reservedwords[num3]._op;
				return;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00031A18 File Offset: 0x0002FC18
		private void ScanString(char escape)
		{
			char[] text = this._text;
			while (this._pos < text.Length)
			{
				char[] array = text;
				int pos = this._pos;
				this._pos = pos + 1;
				char c = array[pos];
				if (c == escape && this._pos < text.Length && text[this._pos] == escape)
				{
					this._pos++;
				}
				else if (c == escape)
				{
					break;
				}
			}
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidString(new string(text, this._start, this._pos - 1 - this._start));
			}
			this._token = Tokens.StringConst;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00031AAE File Offset: 0x0002FCAE
		internal void ScanToken(Tokens token)
		{
			this.Scan();
			this.CheckToken(token);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00031AC0 File Offset: 0x0002FCC0
		private void ScanWhite()
		{
			char[] text = this._text;
			while (this._pos < text.Length && this.IsWhiteSpace(text[this._pos]))
			{
				this._pos++;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00031AFF File Offset: 0x0002FCFF
		private bool IsWhiteSpace(char ch)
		{
			return ch <= ' ' && ch > '\0';
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00031B0C File Offset: 0x0002FD0C
		private bool IsAlphaNumeric(char ch)
		{
			switch (ch)
			{
			case '$':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return ch > '\u007f';
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00031C8A File Offset: 0x0002FE8A
		private bool IsDigit(char ch)
		{
			switch (ch)
			{
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		private bool IsAlpha(char ch)
		{
			switch (ch)
			{
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return false;
		}

		// Token: 0x04000706 RID: 1798
		private const int Empty = 0;

		// Token: 0x04000707 RID: 1799
		private const int Scalar = 1;

		// Token: 0x04000708 RID: 1800
		private const int Expr = 2;

		// Token: 0x04000709 RID: 1801
		private static readonly ExpressionParser.ReservedWords[] s_reservedwords = new ExpressionParser.ReservedWords[]
		{
			new ExpressionParser.ReservedWords("And", Tokens.BinaryOp, 26),
			new ExpressionParser.ReservedWords("Between", Tokens.BinaryOp, 6),
			new ExpressionParser.ReservedWords("Child", Tokens.Child, 0),
			new ExpressionParser.ReservedWords("False", Tokens.ZeroOp, 34),
			new ExpressionParser.ReservedWords("In", Tokens.BinaryOp, 5),
			new ExpressionParser.ReservedWords("Is", Tokens.BinaryOp, 13),
			new ExpressionParser.ReservedWords("Like", Tokens.BinaryOp, 14),
			new ExpressionParser.ReservedWords("Not", Tokens.UnaryOp, 3),
			new ExpressionParser.ReservedWords("Null", Tokens.ZeroOp, 32),
			new ExpressionParser.ReservedWords("Or", Tokens.BinaryOp, 27),
			new ExpressionParser.ReservedWords("Parent", Tokens.Parent, 0),
			new ExpressionParser.ReservedWords("True", Tokens.ZeroOp, 33)
		};

		// Token: 0x0400070A RID: 1802
		private char _escape = '\\';

		// Token: 0x0400070B RID: 1803
		private char _decimalSeparator = '.';

		// Token: 0x0400070C RID: 1804
		private char _listSeparator = ',';

		// Token: 0x0400070D RID: 1805
		private char _exponentL = 'e';

		// Token: 0x0400070E RID: 1806
		private char _exponentU = 'E';

		// Token: 0x0400070F RID: 1807
		internal char[] _text;

		// Token: 0x04000710 RID: 1808
		internal int _pos;

		// Token: 0x04000711 RID: 1809
		internal int _start;

		// Token: 0x04000712 RID: 1810
		internal Tokens _token;

		// Token: 0x04000713 RID: 1811
		internal int _op;

		// Token: 0x04000714 RID: 1812
		internal OperatorInfo[] _ops = new OperatorInfo[100];

		// Token: 0x04000715 RID: 1813
		internal int _topOperator;

		// Token: 0x04000716 RID: 1814
		internal int _topNode;

		// Token: 0x04000717 RID: 1815
		private readonly DataTable _table;

		// Token: 0x04000718 RID: 1816
		private const int MaxPredicates = 100;

		// Token: 0x04000719 RID: 1817
		internal ExpressionNode[] _nodeStack = new ExpressionNode[100];

		// Token: 0x0400071A RID: 1818
		internal int _prevOperand;

		// Token: 0x0400071B RID: 1819
		internal ExpressionNode _expression;

		// Token: 0x020000B1 RID: 177
		private struct ReservedWords
		{
			// Token: 0x06000A8D RID: 2701 RVA: 0x00031EDB File Offset: 0x000300DB
			internal ReservedWords(string word, Tokens token, int op)
			{
				this._word = word;
				this._token = token;
				this._op = op;
			}

			// Token: 0x0400071C RID: 1820
			internal readonly string _word;

			// Token: 0x0400071D RID: 1821
			internal readonly Tokens _token;

			// Token: 0x0400071E RID: 1822
			internal readonly int _op;
		}
	}
}
