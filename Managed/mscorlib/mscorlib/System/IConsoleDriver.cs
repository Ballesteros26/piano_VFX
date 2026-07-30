using System;

namespace System
{
	// Token: 0x02000216 RID: 534
	internal interface IConsoleDriver
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001926 RID: 6438
		// (set) Token: 0x06001927 RID: 6439
		ConsoleColor BackgroundColor { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001928 RID: 6440
		// (set) Token: 0x06001929 RID: 6441
		int BufferHeight { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600192A RID: 6442
		// (set) Token: 0x0600192B RID: 6443
		int BufferWidth { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600192C RID: 6444
		bool CapsLock { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600192D RID: 6445
		// (set) Token: 0x0600192E RID: 6446
		int CursorLeft { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600192F RID: 6447
		// (set) Token: 0x06001930 RID: 6448
		int CursorSize { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001931 RID: 6449
		// (set) Token: 0x06001932 RID: 6450
		int CursorTop { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001933 RID: 6451
		// (set) Token: 0x06001934 RID: 6452
		bool CursorVisible { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001935 RID: 6453
		// (set) Token: 0x06001936 RID: 6454
		ConsoleColor ForegroundColor { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001937 RID: 6455
		bool KeyAvailable { get; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001938 RID: 6456
		bool Initialized { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001939 RID: 6457
		int LargestWindowHeight { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600193A RID: 6458
		int LargestWindowWidth { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600193B RID: 6459
		bool NumberLock { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600193C RID: 6460
		// (set) Token: 0x0600193D RID: 6461
		string Title { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600193E RID: 6462
		// (set) Token: 0x0600193F RID: 6463
		bool TreatControlCAsInput { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001940 RID: 6464
		// (set) Token: 0x06001941 RID: 6465
		int WindowHeight { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001942 RID: 6466
		// (set) Token: 0x06001943 RID: 6467
		int WindowLeft { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001944 RID: 6468
		// (set) Token: 0x06001945 RID: 6469
		int WindowTop { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001946 RID: 6470
		// (set) Token: 0x06001947 RID: 6471
		int WindowWidth { get; set; }

		// Token: 0x06001948 RID: 6472
		void Init();

		// Token: 0x06001949 RID: 6473
		void Beep(int frequency, int duration);

		// Token: 0x0600194A RID: 6474
		void Clear();

		// Token: 0x0600194B RID: 6475
		void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor);

		// Token: 0x0600194C RID: 6476
		ConsoleKeyInfo ReadKey(bool intercept);

		// Token: 0x0600194D RID: 6477
		void ResetColor();

		// Token: 0x0600194E RID: 6478
		void SetBufferSize(int width, int height);

		// Token: 0x0600194F RID: 6479
		void SetCursorPosition(int left, int top);

		// Token: 0x06001950 RID: 6480
		void SetWindowPosition(int left, int top);

		// Token: 0x06001951 RID: 6481
		void SetWindowSize(int width, int height);

		// Token: 0x06001952 RID: 6482
		string ReadLine();
	}
}
