using System;

namespace Mono.WebBrowser.DOM
{
	// Token: 0x02000030 RID: 48
	public interface INode
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600012F RID: 303
		IAttributeCollection Attributes { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000130 RID: 304
		INodeList ChildNodes { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000131 RID: 305
		INode FirstChild { get; }

		// Token: 0x06000132 RID: 306
		INode InsertBefore(INode newChild, INode refChild);

		// Token: 0x06000133 RID: 307
		INode ReplaceChild(INode newChild, INode oldChild);

		// Token: 0x06000134 RID: 308
		INode RemoveChild(INode child);

		// Token: 0x06000135 RID: 309
		INode AppendChild(INode child);

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000136 RID: 310
		INode LastChild { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000137 RID: 311
		string LocalName { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000138 RID: 312
		INode Next { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000139 RID: 313
		IDocument Owner { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013A RID: 314
		INode Parent { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013B RID: 315
		INode Previous { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600013C RID: 316
		NodeType Type { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		string Value { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600013F RID: 319
		IntPtr AccessibleObject { get; }

		// Token: 0x06000140 RID: 320
		void FireEvent(string eventName);

		// Token: 0x06000141 RID: 321
		int GetHashCode();

		// Token: 0x06000142 RID: 322
		bool Equals(object obj);

		// Token: 0x06000143 RID: 323
		void AttachEventHandler(string eventName, EventHandler handler);

		// Token: 0x06000144 RID: 324
		void DetachEventHandler(string eventName, EventHandler handler);

		// Token: 0x06000145 RID: 325
		void AttachEventHandler(string eventName, Delegate handler);

		// Token: 0x06000146 RID: 326
		void DetachEventHandler(string eventName, Delegate handler);

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000147 RID: 327
		// (remove) Token: 0x06000148 RID: 328
		event NodeEventHandler Click;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000149 RID: 329
		// (remove) Token: 0x0600014A RID: 330
		event NodeEventHandler DoubleClick;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600014B RID: 331
		// (remove) Token: 0x0600014C RID: 332
		event NodeEventHandler KeyDown;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600014D RID: 333
		// (remove) Token: 0x0600014E RID: 334
		event NodeEventHandler KeyPress;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600014F RID: 335
		// (remove) Token: 0x06000150 RID: 336
		event NodeEventHandler KeyUp;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000151 RID: 337
		// (remove) Token: 0x06000152 RID: 338
		event NodeEventHandler MouseDown;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000153 RID: 339
		// (remove) Token: 0x06000154 RID: 340
		event NodeEventHandler MouseEnter;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000155 RID: 341
		// (remove) Token: 0x06000156 RID: 342
		event NodeEventHandler MouseLeave;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000157 RID: 343
		// (remove) Token: 0x06000158 RID: 344
		event NodeEventHandler MouseMove;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000159 RID: 345
		// (remove) Token: 0x0600015A RID: 346
		event NodeEventHandler MouseOver;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600015B RID: 347
		// (remove) Token: 0x0600015C RID: 348
		event NodeEventHandler MouseUp;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600015D RID: 349
		// (remove) Token: 0x0600015E RID: 350
		event NodeEventHandler OnFocus;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600015F RID: 351
		// (remove) Token: 0x06000160 RID: 352
		event NodeEventHandler OnBlur;
	}
}
