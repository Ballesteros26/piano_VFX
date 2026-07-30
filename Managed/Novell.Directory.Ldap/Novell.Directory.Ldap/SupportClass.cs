using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

// Token: 0x02000005 RID: 5
public class SupportClass
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
	[CLSCompliant(false)]
	public static sbyte[] ToSByteArray(byte[] byteArray)
	{
		sbyte[] array = new sbyte[byteArray.Length];
		for (int i = 0; i < byteArray.Length; i++)
		{
			array[i] = (sbyte)byteArray[i];
		}
		return array;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000209C File Offset: 0x0000029C
	[CLSCompliant(false)]
	public static byte[] ToByteArray(sbyte[] sbyteArray)
	{
		byte[] array = new byte[sbyteArray.Length];
		for (int i = 0; i < sbyteArray.Length; i++)
		{
			array[i] = (byte)sbyteArray[i];
		}
		return array;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
	public static byte[] ToByteArray(string sourceString)
	{
		byte[] array = new byte[sourceString.Length];
		for (int i = 0; i < sourceString.Length; i++)
		{
			array[i] = (byte)sourceString[i];
		}
		return array;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
	public static byte[] ToByteArray(object[] tempObjectArray)
	{
		byte[] array = new byte[tempObjectArray.Length];
		for (int i = 0; i < tempObjectArray.Length; i++)
		{
			array[i] = (byte)tempObjectArray[i];
		}
		return array;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
	[CLSCompliant(false)]
	public static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
	{
		if (target.Length == 0)
		{
			return 0;
		}
		byte[] array = new byte[target.Length];
		int num = 0;
		int num2 = start;
		int num3;
		for (int i = count; i > 0; i -= num3)
		{
			num3 = sourceStream.Read(array, num2, i);
			if (num3 == 0)
			{
				break;
			}
			num += num3;
			num2 += num3;
		}
		if (num == 0)
		{
			return -1;
		}
		for (int j = start; j < start + num; j++)
		{
			target[j] = (sbyte)array[j];
		}
		return num;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
	[CLSCompliant(false)]
	public static int ReadInput(TextReader sourceTextReader, ref sbyte[] target, int start, int count)
	{
		if (target.Length == 0)
		{
			return 0;
		}
		char[] array = new char[target.Length];
		int num = sourceTextReader.Read(array, start, count);
		if (num == 0)
		{
			return -1;
		}
		for (int i = start; i < start + num; i++)
		{
			target[i] = (sbyte)array[i];
		}
		return num;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
	public static long Identity(long literal)
	{
		return literal;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021E3 File Offset: 0x000003E3
	[CLSCompliant(false)]
	public static ulong Identity(ulong literal)
	{
		return literal;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021E6 File Offset: 0x000003E6
	public static float Identity(float literal)
	{
		return literal;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021E9 File Offset: 0x000003E9
	public static double Identity(double literal)
	{
		return literal;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
	public static string FormatDateTime(DateTimeFormatInfo format, DateTime date)
	{
		string timeFormatPattern = SupportClass.DateTimeFormatManager.manager.GetTimeFormatPattern(format);
		string dateFormatPattern = SupportClass.DateTimeFormatManager.manager.GetDateFormatPattern(format);
		return date.ToString(dateFormatPattern + " " + timeFormatPattern, format);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002225 File Offset: 0x00000425
	public static object PutElement(IDictionary collection, object key, object newValue)
	{
		object obj = collection[key];
		collection[key] = newValue;
		return obj;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002236 File Offset: 0x00000436
	public static bool VectorRemoveElement(IList arrayList, object element)
	{
		bool flag = arrayList.Contains(element);
		arrayList.Remove(element);
		return flag;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002246 File Offset: 0x00000446
	public static object HashtableRemove(Hashtable hashtable, object key)
	{
		object obj = hashtable[key];
		hashtable.Remove(key);
		return obj;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002256 File Offset: 0x00000456
	public static void SetSize(ArrayList arrayList, int newSize)
	{
		if (newSize < 0)
		{
			throw new ArgumentException();
		}
		if (newSize < arrayList.Count)
		{
			arrayList.RemoveRange(newSize, arrayList.Count - newSize);
			return;
		}
		while (newSize > arrayList.Count)
		{
			arrayList.Add(null);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000228C File Offset: 0x0000048C
	public static object StackPush(Stack stack, object element)
	{
		stack.Push(element);
		return element;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002298 File Offset: 0x00000498
	public static void GetCharsFromString(string sourceString, int sourceStart, int sourceEnd, ref char[] destinationArray, int destinationStart)
	{
		int i = sourceStart;
		int num = destinationStart;
		while (i < sourceEnd)
		{
			destinationArray[num] = sourceString[i];
			i++;
			num++;
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000022C3 File Offset: 0x000004C3
	public static FileStream GetFileStream(string FileName, bool Append)
	{
		if (Append)
		{
			return new FileStream(FileName, FileMode.Append);
		}
		return new FileStream(FileName, FileMode.Create);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000022D8 File Offset: 0x000004D8
	[CLSCompliant(false)]
	public static char[] ToCharArray(sbyte[] sByteArray)
	{
		char[] array = new char[sByteArray.Length];
		sByteArray.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000022F8 File Offset: 0x000004F8
	public static char[] ToCharArray(byte[] byteArray)
	{
		char[] array = new char[byteArray.Length];
		byteArray.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002318 File Offset: 0x00000518
	public static object CreateNewInstance(Type classType)
	{
		object obj = null;
		Type[] array = new Type[0];
		ConstructorInfo[] constructors = classType.GetConstructors();
		if (constructors.Length == 0)
		{
			throw new UnauthorizedAccessException();
		}
		for (int i = 0; i < constructors.Length; i++)
		{
			if (constructors[i].GetParameters().Length == 0)
			{
				obj = classType.GetConstructor(array).Invoke(new object[0]);
				break;
			}
			if (i == constructors.Length - 1)
			{
				throw new MethodAccessException();
			}
		}
		return obj;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000237E File Offset: 0x0000057E
	public static void WriteStackTrace(Exception throwable, TextWriter stream)
	{
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002394 File Offset: 0x00000594
	public static bool EqualsSupport(ICollection source, ICollection target)
	{
		IEnumerator enumerator = SupportClass.ReverseStack(source);
		IEnumerator enumerator2 = SupportClass.ReverseStack(target);
		if (source.Count != target.Count)
		{
			return false;
		}
		while (enumerator.MoveNext() && enumerator2.MoveNext())
		{
			if (!enumerator.Current.Equals(enumerator2.Current))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000023E5 File Offset: 0x000005E5
	public static bool EqualsSupport(ICollection source, object target)
	{
		return !(target.GetType() != typeof(ICollection)) && SupportClass.EqualsSupport(source, (ICollection)target);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000240C File Offset: 0x0000060C
	public static bool EqualsSupport(IDictionaryEnumerator source, object target)
	{
		return !(target.GetType() != typeof(IDictionaryEnumerator)) && SupportClass.EqualsSupport(source, (IDictionaryEnumerator)target);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002433 File Offset: 0x00000633
	public static bool EqualsSupport(IDictionaryEnumerator source, IDictionaryEnumerator target)
	{
		while (source.MoveNext() && target.MoveNext())
		{
			if (source.Key.Equals(target.Key) && source.Value.Equals(target.Value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002470 File Offset: 0x00000670
	public static IEnumerator ReverseStack(ICollection collection)
	{
		if (collection.GetType() == typeof(Stack))
		{
			ArrayList arrayList = new ArrayList(collection);
			arrayList.Reverse();
			return arrayList.GetEnumerator();
		}
		return collection.GetEnumerator();
	}

	// Token: 0x020000E3 RID: 227
	public class Tokenizer
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00017FF8 File Offset: 0x000161F8
		public Tokenizer(string source)
		{
			this.elements = new ArrayList();
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001804C File Offset: 0x0001624C
		public Tokenizer(string source, string delimiters)
		{
			this.elements = new ArrayList();
			this.delimiters = delimiters;
			this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			this.source = source;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000180A8 File Offset: 0x000162A8
		public Tokenizer(string source, string delimiters, bool retDel)
		{
			this.elements = new ArrayList();
			this.delimiters = delimiters;
			this.source = source;
			this.returnDelims = retDel;
			if (this.returnDelims)
			{
				this.Tokenize();
			}
			else
			{
				this.elements.AddRange(source.Split(this.delimiters.ToCharArray()));
			}
			this.RemoveEmptyStrings();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00018118 File Offset: 0x00016318
		private void Tokenize()
		{
			string text = this.source;
			if (text.IndexOfAny(this.delimiters.ToCharArray()) < 0 && text.Length > 0)
			{
				this.elements.Add(text);
			}
			else if (text.IndexOfAny(this.delimiters.ToCharArray()) < 0 && text.Length <= 0)
			{
				return;
			}
			while (text.IndexOfAny(this.delimiters.ToCharArray()) >= 0)
			{
				if (text.IndexOfAny(this.delimiters.ToCharArray()) == 0)
				{
					if (text.Length > 1)
					{
						this.elements.Add(text.Substring(0, 1));
						text = text.Substring(1);
					}
					else
					{
						text = "";
					}
				}
				else
				{
					string text2 = text.Substring(0, text.IndexOfAny(this.delimiters.ToCharArray()));
					this.elements.Add(text2);
					this.elements.Add(text.Substring(text2.Length, 1));
					if (text.Length > text2.Length + 1)
					{
						text = text.Substring(text2.Length + 1);
					}
					else
					{
						text = "";
					}
				}
			}
			if (text.Length > 0)
			{
				this.elements.Add(text);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001825A File Offset: 0x0001645A
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018267 File Offset: 0x00016467
		public bool HasMoreTokens()
		{
			return this.elements.Count > 0;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018278 File Offset: 0x00016478
		public string NextToken()
		{
			if (this.source == "")
			{
				throw new Exception();
			}
			string text;
			if (this.returnDelims)
			{
				this.RemoveEmptyStrings();
				text = (string)this.elements[0];
				this.elements.RemoveAt(0);
				return text;
			}
			this.elements = new ArrayList();
			this.elements.AddRange(this.source.Split(this.delimiters.ToCharArray()));
			this.RemoveEmptyStrings();
			text = (string)this.elements[0];
			this.elements.RemoveAt(0);
			this.source = this.source.Remove(this.source.IndexOf(text), text.Length);
			this.source = this.source.TrimStart(this.delimiters.ToCharArray());
			return text;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001835B File Offset: 0x0001655B
		public string NextToken(string delimiters)
		{
			this.delimiters = delimiters;
			return this.NextToken();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001836C File Offset: 0x0001656C
		private void RemoveEmptyStrings()
		{
			for (int i = 0; i < this.elements.Count; i++)
			{
				if ((string)this.elements[i] == "")
				{
					this.elements.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x040004C2 RID: 1218
		private ArrayList elements;

		// Token: 0x040004C3 RID: 1219
		private string source;

		// Token: 0x040004C4 RID: 1220
		private string delimiters = " \t\n\r";

		// Token: 0x040004C5 RID: 1221
		private bool returnDelims;
	}

	// Token: 0x020000E4 RID: 228
	public class DateTimeFormatManager
	{
		// Token: 0x040004C6 RID: 1222
		public static SupportClass.DateTimeFormatManager.DateTimeFormatHashTable manager = new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable();

		// Token: 0x020000FF RID: 255
		public class DateTimeFormatHashTable : Hashtable
		{
			// Token: 0x06000657 RID: 1623 RVA: 0x00019E30 File Offset: 0x00018030
			public void SetDateFormatPattern(DateTimeFormatInfo format, string newPattern)
			{
				if (this[format] != null)
				{
					((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).DateFormatPattern = newPattern;
					return;
				}
				this.Add(format, new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties
				{
					DateFormatPattern = newPattern
				});
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x00019E6E File Offset: 0x0001806E
			public string GetDateFormatPattern(DateTimeFormatInfo format)
			{
				if (this[format] == null)
				{
					return "d-MMM-yy";
				}
				return ((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).DateFormatPattern;
			}

			// Token: 0x06000659 RID: 1625 RVA: 0x00019E90 File Offset: 0x00018090
			public void SetTimeFormatPattern(DateTimeFormatInfo format, string newPattern)
			{
				if (this[format] != null)
				{
					((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).TimeFormatPattern = newPattern;
					return;
				}
				this.Add(format, new SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties
				{
					TimeFormatPattern = newPattern
				});
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x00019ECE File Offset: 0x000180CE
			public string GetTimeFormatPattern(DateTimeFormatInfo format)
			{
				if (this[format] == null)
				{
					return "h:mm:ss tt";
				}
				return ((SupportClass.DateTimeFormatManager.DateTimeFormatHashTable.DateTimeFormatProperties)this[format]).TimeFormatPattern;
			}

			// Token: 0x02000100 RID: 256
			private class DateTimeFormatProperties
			{
				// Token: 0x040004FF RID: 1279
				public string DateFormatPattern = "d-MMM-yy";

				// Token: 0x04000500 RID: 1280
				public string TimeFormatPattern = "h:mm:ss tt";
			}
		}
	}

	// Token: 0x020000E5 RID: 229
	public class ArrayListSupport
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x000183D0 File Offset: 0x000165D0
		public static object[] ToArray(ArrayList collection, object[] objects)
		{
			int num = 0;
			foreach (object obj in collection)
			{
				objects[num++] = obj;
			}
			return objects;
		}
	}

	// Token: 0x020000E6 RID: 230
	public class ThreadClass : IThreadRunnable
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00018406 File Offset: 0x00016606
		public ThreadClass()
		{
			this.threadField = new Thread(new ThreadStart(this.Run));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00018426 File Offset: 0x00016626
		public ThreadClass(string Name)
		{
			this.threadField = new Thread(new ThreadStart(this.Run));
			this.Name = Name;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001844D File Offset: 0x0001664D
		public ThreadClass(ThreadStart Start)
		{
			this.threadField = new Thread(Start);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00018461 File Offset: 0x00016661
		public ThreadClass(ThreadStart Start, string Name)
		{
			this.threadField = new Thread(Start);
			this.Name = Name;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001847C File Offset: 0x0001667C
		public virtual void Run()
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001847E File Offset: 0x0001667E
		public virtual void Start()
		{
			this.threadField.Start();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001848B File Offset: 0x0001668B
		public virtual void Interrupt()
		{
			this.threadField.Interrupt();
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00018498 File Offset: 0x00016698
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x000184A0 File Offset: 0x000166A0
		public Thread Instance
		{
			get
			{
				return this.threadField;
			}
			set
			{
				this.threadField = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000184A9 File Offset: 0x000166A9
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x000184B6 File Offset: 0x000166B6
		public string Name
		{
			get
			{
				return this.threadField.Name;
			}
			set
			{
				if (this.threadField.Name == null)
				{
					this.threadField.Name = value;
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000184D1 File Offset: 0x000166D1
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x000184DE File Offset: 0x000166DE
		public ThreadPriority Priority
		{
			get
			{
				return this.threadField.Priority;
			}
			set
			{
				this.threadField.Priority = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000184EC File Offset: 0x000166EC
		public bool IsAlive
		{
			get
			{
				return this.threadField.IsAlive;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000184F9 File Offset: 0x000166F9
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00018506 File Offset: 0x00016706
		public bool IsBackground
		{
			get
			{
				return this.threadField.IsBackground;
			}
			set
			{
				this.threadField.IsBackground = value;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018514 File Offset: 0x00016714
		public void Join()
		{
			this.threadField.Join();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018524 File Offset: 0x00016724
		public void Join(long MiliSeconds)
		{
			lock (this)
			{
				this.threadField.Join(new TimeSpan(MiliSeconds * 10000L));
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00018574 File Offset: 0x00016774
		public void Join(long MiliSeconds, int NanoSeconds)
		{
			lock (this)
			{
				this.threadField.Join(new TimeSpan(MiliSeconds * 10000L + (long)(NanoSeconds * 100)));
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000185C8 File Offset: 0x000167C8
		public void Resume()
		{
			this.threadField.Resume();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000185D5 File Offset: 0x000167D5
		public void Abort()
		{
			this.threadField.Abort();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000185E4 File Offset: 0x000167E4
		public void Abort(object stateInfo)
		{
			lock (this)
			{
				this.threadField.Abort(stateInfo);
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018628 File Offset: 0x00016828
		public void Suspend()
		{
			this.threadField.Suspend();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018638 File Offset: 0x00016838
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Thread[",
				this.Name,
				",",
				this.Priority.ToString(),
				","
			}) + "]";
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018692 File Offset: 0x00016892
		public static SupportClass.ThreadClass Current()
		{
			return new SupportClass.ThreadClass
			{
				Instance = Thread.CurrentThread
			};
		}

		// Token: 0x040004C7 RID: 1223
		private Thread threadField;
	}

	// Token: 0x020000E7 RID: 231
	public class CollectionSupport : CollectionBase
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x000186AC File Offset: 0x000168AC
		public virtual bool Add(object element)
		{
			return base.List.Add(element) != -1;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000186C0 File Offset: 0x000168C0
		public virtual bool AddAll(ICollection collection)
		{
			bool flag = false;
			if (collection != null)
			{
				IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						flag = this.Add(enumerator.Current);
					}
				}
			}
			return flag;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000186FE File Offset: 0x000168FE
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(collection);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018707 File Offset: 0x00016907
		public virtual bool Contains(object element)
		{
			return base.List.Contains(element);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00018718 File Offset: 0x00016918
		public virtual bool ContainsAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext() && (flag = this.Contains(enumerator.Current)))
			{
			}
			return flag;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001874E File Offset: 0x0001694E
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018757 File Offset: 0x00016957
		public virtual bool IsEmpty()
		{
			return base.Count == 0;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018764 File Offset: 0x00016964
		public virtual bool Remove(object element)
		{
			bool flag = false;
			if (this.Contains(element))
			{
				base.List.Remove(element);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001878C File Offset: 0x0001698C
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
				}
			}
			return flag;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000187CD File Offset: 0x000169CD
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000187D8 File Offset: 0x000169D8
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = base.GetEnumerator();
			SupportClass.CollectionSupport collectionSupport = new SupportClass.CollectionSupport();
			collectionSupport.AddAll(collection);
			while (enumerator.MoveNext())
			{
				if (!collectionSupport.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
					if (flag)
					{
						enumerator = base.GetEnumerator();
					}
				}
			}
			return flag;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001882C File Offset: 0x00016A2C
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00018838 File Offset: 0x00016A38
		public virtual object[] ToArray()
		{
			int num = 0;
			object[] array = new object[base.Count];
			foreach (object obj in this)
			{
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018874 File Offset: 0x00016A74
		public virtual object[] ToArray(object[] objects)
		{
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000188A2 File Offset: 0x00016AA2
		public static SupportClass.CollectionSupport ToCollectionSupport(object[] array)
		{
			SupportClass.CollectionSupport collectionSupport = new SupportClass.CollectionSupport();
			collectionSupport.AddAll(array);
			return collectionSupport;
		}
	}

	// Token: 0x020000E8 RID: 232
	public class ListCollectionSupport : ArrayList
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x000188B1 File Offset: 0x00016AB1
		public ListCollectionSupport()
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000188B9 File Offset: 0x00016AB9
		public ListCollectionSupport(ICollection collection)
			: base(collection)
		{
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000188C2 File Offset: 0x00016AC2
		public ListCollectionSupport(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000188CB File Offset: 0x00016ACB
		public new virtual bool Add(object valueToInsert)
		{
			base.Insert(this.Count, valueToInsert);
			return true;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000188DC File Offset: 0x00016ADC
		public virtual bool AddAll(int index, IList list)
		{
			bool flag = false;
			if (list != null)
			{
				IEnumerator enumerator = new ArrayList(list).GetEnumerator();
				int num = index;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					base.Insert(num++, obj);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001891A File Offset: 0x00016B1A
		public virtual bool AddAll(IList collection)
		{
			return this.AddAll(this.Count, collection);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00018929 File Offset: 0x00016B29
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(this.Count, collection);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00018938 File Offset: 0x00016B38
		public virtual bool AddAll(int index, SupportClass.CollectionSupport collection)
		{
			return this.AddAll(index, collection);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00018942 File Offset: 0x00016B42
		public virtual object ListCollectionClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001894A File Offset: 0x00016B4A
		public virtual IEnumerator ListIterator()
		{
			return base.GetEnumerator();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018954 File Offset: 0x00016B54
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext())
			{
				flag = true;
				if (base.Contains(enumerator.Current))
				{
					base.Remove(enumerator.Current);
				}
			}
			return flag;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00018996 File Offset: 0x00016B96
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001899F File Offset: 0x00016B9F
		public virtual object RemoveElement(int index)
		{
			object obj = this[index];
			this.RemoveAt(index);
			return obj;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000189B0 File Offset: 0x00016BB0
		public virtual bool RemoveElement(object element)
		{
			bool flag = false;
			if (this.Contains(element))
			{
				base.Remove(element);
				flag = true;
			}
			return flag;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000189D2 File Offset: 0x00016BD2
		public virtual object RemoveFirst()
		{
			object obj = this[0];
			this.RemoveAt(0);
			return obj;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000189E2 File Offset: 0x00016BE2
		public virtual object RemoveLast()
		{
			object obj = this[this.Count - 1];
			base.RemoveAt(this.Count - 1);
			return obj;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00018A00 File Offset: 0x00016C00
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = this.GetEnumerator();
			SupportClass.ListCollectionSupport listCollectionSupport = new SupportClass.ListCollectionSupport(collection);
			while (enumerator.MoveNext())
			{
				if (!listCollectionSupport.Contains(enumerator.Current))
				{
					flag = this.RemoveElement(enumerator.Current);
					if (flag)
					{
						enumerator = this.GetEnumerator();
					}
				}
			}
			return flag;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00018A4D File Offset: 0x00016C4D
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00018A58 File Offset: 0x00016C58
		public virtual bool ContainsAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
			while (enumerator.MoveNext() && (flag = this.Contains(enumerator.Current)))
			{
			}
			return flag;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00018A8E File Offset: 0x00016C8E
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00018A98 File Offset: 0x00016C98
		public virtual SupportClass.ListCollectionSupport SubList(int startIndex, int endIndex)
		{
			this.GetEnumerator();
			SupportClass.ListCollectionSupport listCollectionSupport = new SupportClass.ListCollectionSupport();
			for (int i = startIndex; i < endIndex; i++)
			{
				listCollectionSupport.Add(this[i]);
			}
			return listCollectionSupport;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018AD0 File Offset: 0x00016CD0
		public virtual object[] ToArray(object[] objects)
		{
			if (objects.Length < this.Count)
			{
				objects = new object[this.Count];
			}
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018B18 File Offset: 0x00016D18
		public virtual IEnumerator ListIterator(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IEnumerator enumerator = this.GetEnumerator();
			if (index > 0)
			{
				int num = 0;
				while (enumerator.MoveNext() && num < index - 1)
				{
					num++;
				}
			}
			return enumerator;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00018B5A File Offset: 0x00016D5A
		public virtual object GetLast()
		{
			if (this.Count == 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this[this.Count - 1];
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018B78 File Offset: 0x00016D78
		public virtual bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00018B83 File Offset: 0x00016D83
		public virtual object Set(int index, object element)
		{
			object obj = this[index];
			this[index] = element;
			return obj;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00018B94 File Offset: 0x00016D94
		public virtual object Get(int index)
		{
			return this[index];
		}
	}

	// Token: 0x020000E9 RID: 233
	public class ArraysSupport
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00018BA0 File Offset: 0x00016DA0
		public static bool IsArrayEqual(Array array1, Array array2)
		{
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1.GetValue(i).Equals(array2.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public static void FillArray(Array array, int fromindex, int toindex, object val)
		{
			object obj = val;
			Type elementType = array.GetType().GetElementType();
			if (elementType != val.GetType())
			{
				obj = Convert.ChangeType(val, elementType);
			}
			if (array.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (fromindex > toindex)
			{
				throw new ArgumentException();
			}
			if (fromindex < 0 || array.Length < toindex)
			{
				throw new IndexOutOfRangeException();
			}
			int num;
			if (fromindex <= 0)
			{
				num = fromindex;
			}
			else
			{
				fromindex = (num = fromindex) - 1;
			}
			for (int i = num; i < toindex; i++)
			{
				array.SetValue(obj, i);
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00018C64 File Offset: 0x00016E64
		public static void FillArray(Array array, object val)
		{
			SupportClass.ArraysSupport.FillArray(array, 0, array.Length, val);
		}
	}

	// Token: 0x020000EA RID: 234
	public class SetSupport : ArrayList
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00018C7C File Offset: 0x00016E7C
		public SetSupport()
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00018C84 File Offset: 0x00016E84
		public SetSupport(ICollection collection)
			: base(collection)
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00018C8D File Offset: 0x00016E8D
		public SetSupport(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00018C96 File Offset: 0x00016E96
		public new virtual bool Add(object objectToAdd)
		{
			if (this.Contains(objectToAdd))
			{
				return false;
			}
			base.Add(objectToAdd);
			return true;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00018CAC File Offset: 0x00016EAC
		public virtual bool AddAll(ICollection collection)
		{
			bool flag = false;
			if (collection != null)
			{
				IEnumerator enumerator = new ArrayList(collection).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						flag = this.Add(enumerator.Current);
					}
				}
			}
			return flag;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00018CEA File Offset: 0x00016EEA
		public virtual bool AddAll(SupportClass.CollectionSupport collection)
		{
			return this.AddAll(collection);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00018CF4 File Offset: 0x00016EF4
		public virtual bool ContainsAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext() && (flag = this.Contains(enumerator.Current)))
			{
			}
			return flag;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00018D25 File Offset: 0x00016F25
		public virtual bool ContainsAll(SupportClass.CollectionSupport collection)
		{
			return this.ContainsAll(collection);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00018D2E File Offset: 0x00016F2E
		public virtual bool IsEmpty()
		{
			return this.Count == 0;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00018D3C File Offset: 0x00016F3C
		public new virtual bool Remove(object elementToRemove)
		{
			bool flag = false;
			if (this.Contains(elementToRemove))
			{
				flag = true;
			}
			base.Remove(elementToRemove);
			return flag;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00018D60 File Offset: 0x00016F60
		public virtual bool RemoveAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!flag && this.Contains(enumerator.Current))
				{
					flag = true;
				}
				this.Remove(enumerator.Current);
			}
			return flag;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00018DA1 File Offset: 0x00016FA1
		public virtual bool RemoveAll(SupportClass.CollectionSupport collection)
		{
			return this.RemoveAll(collection);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00018DAC File Offset: 0x00016FAC
		public virtual bool RetainAll(ICollection collection)
		{
			bool flag = false;
			IEnumerator enumerator = collection.GetEnumerator();
			SupportClass.SetSupport setSupport = (SupportClass.SetSupport)collection;
			while (enumerator.MoveNext())
			{
				if (!setSupport.Contains(enumerator.Current))
				{
					flag = this.Remove(enumerator.Current);
					enumerator = this.GetEnumerator();
				}
			}
			return flag;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00018DF6 File Offset: 0x00016FF6
		public virtual bool RetainAll(SupportClass.CollectionSupport collection)
		{
			return this.RetainAll(collection);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00018E00 File Offset: 0x00017000
		public new virtual object[] ToArray()
		{
			int num = 0;
			object[] array = new object[this.Count];
			foreach (object obj in this)
			{
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00018E3C File Offset: 0x0001703C
		public virtual object[] ToArray(object[] objects)
		{
			int num = 0;
			foreach (object obj in this)
			{
				objects[num++] = obj;
			}
			return objects;
		}
	}

	// Token: 0x020000EB RID: 235
	public class AbstractSetSupport : SupportClass.SetSupport
	{
	}

	// Token: 0x020000EC RID: 236
	public class MessageDigestSupport
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00018E72 File Offset: 0x00017072
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00018E7A File Offset: 0x0001707A
		public HashAlgorithm Algorithm
		{
			get
			{
				return this.algorithm;
			}
			set
			{
				this.algorithm = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00018E83 File Offset: 0x00017083
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00018E8B File Offset: 0x0001708B
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00018E94 File Offset: 0x00017094
		public string AlgorithmName
		{
			get
			{
				return this.algorithmName;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018E9C File Offset: 0x0001709C
		public MessageDigestSupport(string algorithm)
		{
			if (algorithm.Equals("SHA-1"))
			{
				this.algorithmName = "SHA";
			}
			else
			{
				this.algorithmName = algorithm;
			}
			this.Algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(this.algorithmName);
			this.position = 0;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018EED File Offset: 0x000170ED
		[CLSCompliant(false)]
		public sbyte[] DigestData()
		{
			sbyte[] array = SupportClass.ToSByteArray(this.Algorithm.ComputeHash(this.data));
			this.Reset();
			return array;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00018F0B File Offset: 0x0001710B
		[CLSCompliant(false)]
		public sbyte[] DigestData(byte[] newData)
		{
			this.Update(newData);
			return this.DigestData();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00018F1C File Offset: 0x0001711C
		public void Update(byte[] newData)
		{
			if (this.position == 0)
			{
				this.Data = newData;
				this.position = this.Data.Length - 1;
				return;
			}
			byte[] array = this.Data;
			this.Data = new byte[newData.Length + this.position + 1];
			array.CopyTo(this.Data, 0);
			newData.CopyTo(this.Data, array.Length);
			this.position = this.Data.Length - 1;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00018F94 File Offset: 0x00017194
		public void Update(byte newData)
		{
			this.Update(new byte[] { newData });
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00018FB4 File Offset: 0x000171B4
		public void Update(byte[] newData, int offset, int count)
		{
			byte[] array = new byte[count];
			Array.Copy(newData, offset, array, 0, count);
			this.Update(array);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00018FD9 File Offset: 0x000171D9
		public void Reset()
		{
			this.data = null;
			this.position = 0;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00018FE9 File Offset: 0x000171E9
		public override string ToString()
		{
			return this.Algorithm.ToString();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00018FF6 File Offset: 0x000171F6
		public static SupportClass.MessageDigestSupport GetInstance(string algorithm)
		{
			return new SupportClass.MessageDigestSupport(algorithm);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00019000 File Offset: 0x00017200
		[CLSCompliant(false)]
		public static bool EquivalentDigest(sbyte[] firstDigest, sbyte[] secondDigest)
		{
			bool flag = false;
			if (firstDigest.Length == secondDigest.Length)
			{
				int num = 0;
				flag = true;
				while (flag && num < firstDigest.Length)
				{
					flag = firstDigest[num] == secondDigest[num];
					num++;
				}
			}
			return flag;
		}

		// Token: 0x040004C8 RID: 1224
		private HashAlgorithm algorithm;

		// Token: 0x040004C9 RID: 1225
		private byte[] data;

		// Token: 0x040004CA RID: 1226
		private int position;

		// Token: 0x040004CB RID: 1227
		private string algorithmName;
	}

	// Token: 0x020000ED RID: 237
	public class SecureRandomSupport
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00019034 File Offset: 0x00017234
		public SecureRandomSupport()
		{
			this.generator = new RNGCryptoServiceProvider();
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00019047 File Offset: 0x00017247
		public SecureRandomSupport(byte[] seed)
		{
			this.generator = new RNGCryptoServiceProvider(seed);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001905B File Offset: 0x0001725B
		[CLSCompliant(false)]
		public sbyte[] NextBytes(byte[] randomnumbersarray)
		{
			this.generator.GetBytes(randomnumbersarray);
			return SupportClass.ToSByteArray(randomnumbersarray);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00019070 File Offset: 0x00017270
		public static byte[] GetSeed(int numberOfBytes)
		{
			RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();
			byte[] array = new byte[numberOfBytes];
			randomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019090 File Offset: 0x00017290
		public void SetSeed(byte[] newSeed)
		{
			this.generator = new RNGCryptoServiceProvider(newSeed);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000190A0 File Offset: 0x000172A0
		public void SetSeed(long newSeed)
		{
			byte[] array = new byte[8];
			for (int i = 7; i > 0; i--)
			{
				array[i] = (byte)(newSeed - (newSeed >> 8 << 8));
				newSeed >>= 8;
			}
			this.SetSeed(array);
		}

		// Token: 0x040004CC RID: 1228
		private RNGCryptoServiceProvider generator;
	}

	// Token: 0x020000EE RID: 238
	public interface SingleThreadModel
	{
	}
}
