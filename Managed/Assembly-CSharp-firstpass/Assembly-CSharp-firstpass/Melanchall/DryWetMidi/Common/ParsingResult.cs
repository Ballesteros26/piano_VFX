using System;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C8 RID: 456
	internal sealed class ParsingResult
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x00024BD0 File Offset: 0x00022DD0
		private ParsingResult(string error)
			: this(ParsingStatus.FormatError, error)
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00024BDA File Offset: 0x00022DDA
		private ParsingResult(ParsingStatus status)
			: this(status, null)
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00024BE4 File Offset: 0x00022DE4
		private ParsingResult(ParsingStatus status, string error)
		{
			this.Status = status;
			this._error = error;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00024BFA File Offset: 0x00022DFA
		public ParsingStatus Status { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00024C04 File Offset: 0x00022E04
		public Exception Exception
		{
			get
			{
				switch (this.Status)
				{
				case ParsingStatus.EmptyInputString:
					return new ArgumentException("Input string is null or contains white-spaces only.");
				case ParsingStatus.NotMatched:
					return new FormatException("Input string has invalid format.");
				case ParsingStatus.FormatError:
					return new FormatException(this._error);
				default:
					return null;
				}
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00024C51 File Offset: 0x00022E51
		public static ParsingResult Error(string error)
		{
			return new ParsingResult(error);
		}

		// Token: 0x04000A13 RID: 2579
		public static readonly ParsingResult Parsed = new ParsingResult(ParsingStatus.Parsed);

		// Token: 0x04000A14 RID: 2580
		public static readonly ParsingResult EmptyInputString = new ParsingResult(ParsingStatus.EmptyInputString);

		// Token: 0x04000A15 RID: 2581
		public static readonly ParsingResult NotMatched = new ParsingResult(ParsingStatus.NotMatched);

		// Token: 0x04000A16 RID: 2582
		private readonly string _error;
	}
}
