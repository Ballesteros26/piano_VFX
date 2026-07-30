using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000142 RID: 322
	internal abstract class RegexCompiler
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x0002CD62 File Offset: 0x0002AF62
		private static FieldInfo RegexRunnerField(string fieldname)
		{
			return typeof(RegexRunner).GetField(fieldname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002CD76 File Offset: 0x0002AF76
		private static MethodInfo RegexRunnerMethod(string methname)
		{
			return typeof(RegexRunner).GetMethod(methname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002CD8A File Offset: 0x0002AF8A
		internal static RegexRunnerFactory Compile(RegexCode code, RegexOptions options)
		{
			return new RegexLWCGCompiler().FactoryInstanceFromCode(code, options);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002CD98 File Offset: 0x0002AF98
		internal static void CompileToAssembly(RegexCompilationInfo[] regexes, AssemblyName an, CustomAttributeBuilder[] attribs, string resourceFile)
		{
			RegexTypeCompiler regexTypeCompiler = new RegexTypeCompiler(an, attribs, resourceFile);
			for (int i = 0; i < regexes.Length; i++)
			{
				if (regexes[i] == null)
				{
					throw new ArgumentNullException("regexes", global::SR.GetString("The array cannot contain null elements."));
				}
				string pattern = regexes[i].Pattern;
				RegexOptions options = regexes[i].Options;
				string text;
				if (regexes[i].Namespace.Length == 0)
				{
					text = regexes[i].Name;
				}
				else
				{
					text = regexes[i].Namespace + "." + regexes[i].Name;
				}
				TimeSpan matchTimeout = regexes[i].MatchTimeout;
				RegexTree regexTree = RegexParser.Parse(pattern, options);
				RegexCode regexCode = RegexWriter.Write(regexTree);
				Type type = regexTypeCompiler.FactoryTypeFromCode(regexCode, options, text);
				regexTypeCompiler.GenerateRegexType(pattern, options, text, regexes[i].IsPublic, regexCode, regexTree, type, matchTimeout);
			}
			regexTypeCompiler.Save();
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002CE70 File Offset: 0x0002B070
		internal int AddBacktrackNote(int flags, Label l, int codepos)
		{
			if (this._notes == null || this._notecount >= this._notes.Length)
			{
				RegexCompiler.BacktrackNote[] array = new RegexCompiler.BacktrackNote[(this._notes == null) ? 16 : (this._notes.Length * 2)];
				if (this._notes != null)
				{
					Array.Copy(this._notes, 0, array, 0, this._notecount);
				}
				this._notes = array;
			}
			this._notes[this._notecount] = new RegexCompiler.BacktrackNote(flags, l, codepos);
			int notecount = this._notecount;
			this._notecount = notecount + 1;
			return notecount;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002CEFA File Offset: 0x0002B0FA
		internal int AddTrack()
		{
			return this.AddTrack(128);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002CF07 File Offset: 0x0002B107
		internal int AddTrack(int flags)
		{
			return this.AddBacktrackNote(flags, this.DefineLabel(), this._codepos);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002CF1C File Offset: 0x0002B11C
		internal int AddGoto(int destpos)
		{
			if (this._goto[destpos] == -1)
			{
				this._goto[destpos] = this.AddBacktrackNote(0, this._labels[destpos], destpos);
			}
			return this._goto[destpos];
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002CF4D File Offset: 0x0002B14D
		internal int AddUniqueTrack(int i)
		{
			return this.AddUniqueTrack(i, 128);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002CF5B File Offset: 0x0002B15B
		internal int AddUniqueTrack(int i, int flags)
		{
			if (this._uniquenote[i] == -1)
			{
				this._uniquenote[i] = this.AddTrack(flags);
			}
			return this._uniquenote[i];
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002CF7F File Offset: 0x0002B17F
		internal Label DefineLabel()
		{
			return this._ilg.DefineLabel();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002CF8C File Offset: 0x0002B18C
		internal void MarkLabel(Label l)
		{
			this._ilg.MarkLabel(l);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002CF9A File Offset: 0x0002B19A
		internal int Operand(int i)
		{
			return this._codes[this._codepos + i + 1];
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002CFAD File Offset: 0x0002B1AD
		internal bool IsRtl()
		{
			return (this._regexopcode & 64) != 0;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002CFBB File Offset: 0x0002B1BB
		internal bool IsCi()
		{
			return (this._regexopcode & 512) != 0;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002CFCC File Offset: 0x0002B1CC
		internal int Code()
		{
			return this._regexopcode & 63;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002CFD7 File Offset: 0x0002B1D7
		internal void Ldstr(string str)
		{
			this._ilg.Emit(OpCodes.Ldstr, str);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002CFEA File Offset: 0x0002B1EA
		internal void Ldc(int i)
		{
			if (i <= 127 && i >= -128)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_S, (byte)i);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I4, i);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002D01A File Offset: 0x0002B21A
		internal void LdcI8(long i)
		{
			if (i <= 2147483647L && i >= -2147483648L)
			{
				this.Ldc((int)i);
				this._ilg.Emit(OpCodes.Conv_I8);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I8, i);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002D058 File Offset: 0x0002B258
		internal void Dup()
		{
			this._ilg.Emit(OpCodes.Dup);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002D06A File Offset: 0x0002B26A
		internal void Ret()
		{
			this._ilg.Emit(OpCodes.Ret);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002D07C File Offset: 0x0002B27C
		internal void Pop()
		{
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002D08E File Offset: 0x0002B28E
		internal void Add()
		{
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002D0A0 File Offset: 0x0002B2A0
		internal void Add(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Sub);
				return;
			}
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002D0C6 File Offset: 0x0002B2C6
		internal void Sub()
		{
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002D0D8 File Offset: 0x0002B2D8
		internal void Sub(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Add);
				return;
			}
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002D0FE File Offset: 0x0002B2FE
		internal void Ldloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002D111 File Offset: 0x0002B311
		internal void Stloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Stloc_S, lt);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002D124 File Offset: 0x0002B324
		internal void Ldthis()
		{
			this._ilg.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002D136 File Offset: 0x0002B336
		internal void Ldthisfld(FieldInfo ft)
		{
			this.Ldthis();
			this._ilg.Emit(OpCodes.Ldfld, ft);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002D14F File Offset: 0x0002B34F
		internal void Mvfldloc(FieldInfo ft, LocalBuilder lt)
		{
			this.Ldthisfld(ft);
			this.Stloc(lt);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002D15F File Offset: 0x0002B35F
		internal void Mvlocfld(LocalBuilder lt, FieldInfo ft)
		{
			this.Ldthis();
			this.Ldloc(lt);
			this.Stfld(ft);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002D175 File Offset: 0x0002B375
		internal void Stfld(FieldInfo ft)
		{
			this._ilg.Emit(OpCodes.Stfld, ft);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0002D188 File Offset: 0x0002B388
		internal void Callvirt(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Callvirt, mt);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0002D19B File Offset: 0x0002B39B
		internal void Call(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Call, mt);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002D1AE File Offset: 0x0002B3AE
		internal void Newobj(ConstructorInfo ct)
		{
			this._ilg.Emit(OpCodes.Newobj, ct);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002D1C1 File Offset: 0x0002B3C1
		internal void BrfalseFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse, l);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		internal void BrtrueFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brtrue, l);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002D1E7 File Offset: 0x0002B3E7
		internal void BrFar(Label l)
		{
			this._ilg.Emit(OpCodes.Br, l);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002D1FA File Offset: 0x0002B3FA
		internal void BleFar(Label l)
		{
			this._ilg.Emit(OpCodes.Ble, l);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002D20D File Offset: 0x0002B40D
		internal void BltFar(Label l)
		{
			this._ilg.Emit(OpCodes.Blt, l);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002D220 File Offset: 0x0002B420
		internal void BgeFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bge, l);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002D233 File Offset: 0x0002B433
		internal void BgtFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt, l);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002D246 File Offset: 0x0002B446
		internal void BneFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un, l);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002D259 File Offset: 0x0002B459
		internal void BeqFar(Label l)
		{
			this._ilg.Emit(OpCodes.Beq, l);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002D26C File Offset: 0x0002B46C
		internal void Brfalse(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse_S, l);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002D27F File Offset: 0x0002B47F
		internal void Br(Label l)
		{
			this._ilg.Emit(OpCodes.Br_S, l);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002D292 File Offset: 0x0002B492
		internal void Ble(Label l)
		{
			this._ilg.Emit(OpCodes.Ble_S, l);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002D2A5 File Offset: 0x0002B4A5
		internal void Blt(Label l)
		{
			this._ilg.Emit(OpCodes.Blt_S, l);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
		internal void Bge(Label l)
		{
			this._ilg.Emit(OpCodes.Bge_S, l);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002D2CB File Offset: 0x0002B4CB
		internal void Bgt(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_S, l);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002D2DE File Offset: 0x0002B4DE
		internal void Bgtun(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_Un_S, l);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0002D2F1 File Offset: 0x0002B4F1
		internal void Bne(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un_S, l);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002D304 File Offset: 0x0002B504
		internal void Beq(Label l)
		{
			this._ilg.Emit(OpCodes.Beq_S, l);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0002D317 File Offset: 0x0002B517
		internal void Ldlen()
		{
			this._ilg.Emit(OpCodes.Ldlen);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002D329 File Offset: 0x0002B529
		internal void Rightchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002D350 File Offset: 0x0002B550
		internal void Rightcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Dup();
			this.Ldc(1);
			this.Add();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002D39F File Offset: 0x0002B59F
		internal void Leftchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		internal void Leftcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Dup();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002D423 File Offset: 0x0002B623
		internal void Track()
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddTrack());
			this.DoPush();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002D43D File Offset: 0x0002B63D
		internal void Trackagain()
		{
			this.ReadyPushTrack();
			this.Ldc(this._backpos);
			this.DoPush();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002D457 File Offset: 0x0002B657
		internal void PushTrack(LocalBuilder lt)
		{
			this.ReadyPushTrack();
			this.Ldloc(lt);
			this.DoPush();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002D46C File Offset: 0x0002B66C
		internal void TrackUnique(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i));
			this.DoPush();
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002D487 File Offset: 0x0002B687
		internal void TrackUnique2(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i, 256));
			this.DoPush();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002D4A8 File Offset: 0x0002B6A8
		internal void ReadyPushTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002D528 File Offset: 0x0002B728
		internal void PopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002D5B7 File Offset: 0x0002B7B7
		internal void TopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002D5F5 File Offset: 0x0002B7F5
		internal void PushStack(LocalBuilder lt)
		{
			this.ReadyPushStack();
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
			this.DoPush();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002D614 File Offset: 0x0002B814
		internal void ReadyReplaceStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			if (i != 0)
			{
				this.Ldc(i);
				this._ilg.Emit(OpCodes.Add);
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002D668 File Offset: 0x0002B868
		internal void ReadyPushStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002D6E7 File Offset: 0x0002B8E7
		internal void TopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002D728 File Offset: 0x0002B928
		internal void PopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002D7B7 File Offset: 0x0002B9B7
		internal void PopDiscardStack()
		{
			this.PopDiscardStack(1);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002D7C0 File Offset: 0x0002B9C0
		internal void PopDiscardStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this.Ldc(i);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002D810 File Offset: 0x0002BA10
		internal void DoReplace()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002D810 File Offset: 0x0002BA10
		internal void DoPush()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002D822 File Offset: 0x0002BA22
		internal void Back()
		{
			this._ilg.Emit(OpCodes.Br, this._backtrack);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002D83C File Offset: 0x0002BA3C
		internal void Goto(int i)
		{
			if (i < this._codepos)
			{
				Label label = this.DefineLabel();
				this.Ldloc(this._trackposV);
				this.Ldc(this._trackcount * 4);
				this.Ble(label);
				this.Ldloc(this._stackposV);
				this.Ldc(this._trackcount * 3);
				this.BgtFar(this._labels[i]);
				this.MarkLabel(label);
				this.ReadyPushTrack();
				this.Ldc(this.AddGoto(i));
				this.DoPush();
				this.BrFar(this._backtrack);
				return;
			}
			this.BrFar(this._labels[i]);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002D8E8 File Offset: 0x0002BAE8
		internal int NextCodepos()
		{
			return this._codepos + RegexCode.OpcodeSize(this._codes[this._codepos]);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002D903 File Offset: 0x0002BB03
		internal Label AdvanceLabel()
		{
			return this._labels[this.NextCodepos()];
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002D916 File Offset: 0x0002BB16
		internal void Advance()
		{
			this._ilg.Emit(OpCodes.Br, this.AdvanceLabel());
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002D92E File Offset: 0x0002BB2E
		internal void CallToLower()
		{
			if ((this._options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				this.Call(RegexCompiler._getInvariantCulture);
			}
			else
			{
				this.Call(RegexCompiler._getCurrentCulture);
			}
			this.Call(RegexCompiler._chartolowerM);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002D964 File Offset: 0x0002BB64
		internal void GenerateForwardSection()
		{
			this._labels = new Label[this._codes.Length];
			this._goto = new int[this._codes.Length];
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this._goto[i] = -1;
				this._labels[i] = this._ilg.DefineLabel();
			}
			this._uniquenote = new int[10];
			for (int j = 0; j < 10; j++)
			{
				this._uniquenote[j] = -1;
			}
			this.Mvfldloc(RegexCompiler._textF, this._textV);
			this.Mvfldloc(RegexCompiler._textstartF, this._textstartV);
			this.Mvfldloc(RegexCompiler._textbegF, this._textbegV);
			this.Mvfldloc(RegexCompiler._textendF, this._textendV);
			this.Mvfldloc(RegexCompiler._textposF, this._textposV);
			this.Mvfldloc(RegexCompiler._trackF, this._trackV);
			this.Mvfldloc(RegexCompiler._trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler._stackF, this._stackV);
			this.Mvfldloc(RegexCompiler._stackposF, this._stackposV);
			this._backpos = -1;
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this.MarkLabel(this._labels[i]);
				this._codepos = i;
				this._regexopcode = this._codes[i];
				this.GenerateOneCode();
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
		internal void GenerateMiddleSection()
		{
			this.DefineLabel();
			this.MarkLabel(this._backtrack);
			this.Mvlocfld(this._trackposV, RegexCompiler._trackposF);
			this.Mvlocfld(this._stackposV, RegexCompiler._stackposF);
			this.Ldthis();
			this.Callvirt(RegexCompiler._ensurestorageM);
			this.Mvfldloc(RegexCompiler._trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler._stackposF, this._stackposV);
			this.Mvfldloc(RegexCompiler._trackF, this._trackV);
			this.Mvfldloc(RegexCompiler._stackF, this._stackV);
			this.PopTrack();
			Label[] array = new Label[this._notecount];
			for (int i = 0; i < this._notecount; i++)
			{
				array[i] = this._notes[i]._label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		internal void GenerateBacktrackSection()
		{
			for (int i = 0; i < this._notecount; i++)
			{
				RegexCompiler.BacktrackNote backtrackNote = this._notes[i];
				if (backtrackNote._flags != 0)
				{
					this._ilg.MarkLabel(backtrackNote._label);
					this._codepos = backtrackNote._codepos;
					this._backpos = i;
					this._regexopcode = this._codes[backtrackNote._codepos] | backtrackNote._flags;
					this.GenerateOneCode();
				}
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002DC3C File Offset: 0x0002BE3C
		internal void GenerateFindFirstChar()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			if ((this._anchors & 53) != 0)
			{
				if (!this._code._rightToLeft)
				{
					if ((this._anchors & 1) != 0)
					{
						Label label = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Ble(label);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label);
					}
					if ((this._anchors & 4) != 0)
					{
						Label label2 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.Ble(label2);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label2);
					}
					if ((this._anchors & 16) != 0)
					{
						Label label3 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Bge(label3);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(label3);
					}
					if ((this._anchors & 32) != 0)
					{
						Label label4 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Bge(label4);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(label4);
					}
				}
				else
				{
					if ((this._anchors & 32) != 0)
					{
						Label label5 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Bge(label5);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label5);
					}
					if ((this._anchors & 16) != 0)
					{
						Label label6 = this.DefineLabel();
						Label label7 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Blt(label6);
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Beq(label7);
						this.Ldthisfld(RegexCompiler._textF);
						this.Ldthisfld(RegexCompiler._textposF);
						this.Callvirt(RegexCompiler._getcharM);
						this.Ldc(10);
						this.Beq(label7);
						this.MarkLabel(label6);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label7);
					}
					if ((this._anchors & 4) != 0)
					{
						Label label8 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.Bge(label8);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label8);
					}
					if ((this._anchors & 1) != 0)
					{
						Label label9 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Ble(label9);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(label9);
					}
				}
				this.Ldc(1);
				this.Ret();
				return;
			}
			if (this._bmPrefix != null && this._bmPrefix._negativeUnicode == null)
			{
				LocalBuilder tempV = this._tempV;
				LocalBuilder tempV2 = this._tempV;
				LocalBuilder temp2V = this._temp2V;
				Label label10 = this.DefineLabel();
				Label label11 = this.DefineLabel();
				Label label12 = this.DefineLabel();
				Label label13 = this.DefineLabel();
				this.DefineLabel();
				Label label14 = this.DefineLabel();
				int num;
				int num2;
				if (!this._code._rightToLeft)
				{
					num = -1;
					num2 = this._bmPrefix._pattern.Length - 1;
				}
				else
				{
					num = this._bmPrefix._pattern.Length;
					num2 = 0;
				}
				int num3 = (int)this._bmPrefix._pattern[num2];
				this.Mvfldloc(RegexCompiler._textF, this._textV);
				if (!this._code._rightToLeft)
				{
					this.Ldthisfld(RegexCompiler._textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler._textbegF);
				}
				this.Stloc(temp2V);
				this.Ldthisfld(RegexCompiler._textposF);
				if (!this._code._rightToLeft)
				{
					this.Ldc(this._bmPrefix._pattern.Length - 1);
					this.Add();
				}
				else
				{
					this.Ldc(this._bmPrefix._pattern.Length);
					this.Sub();
				}
				this.Stloc(this._textposV);
				this.Br(label13);
				this.MarkLabel(label10);
				if (!this._code._rightToLeft)
				{
					this.Ldc(this._bmPrefix._pattern.Length);
				}
				else
				{
					this.Ldc(-this._bmPrefix._pattern.Length);
				}
				this.MarkLabel(label11);
				this.Ldloc(this._textposV);
				this.Add();
				this.Stloc(this._textposV);
				this.MarkLabel(label13);
				this.Ldloc(this._textposV);
				this.Ldloc(temp2V);
				if (!this._code._rightToLeft)
				{
					this.BgeFar(label12);
				}
				else
				{
					this.BltFar(label12);
				}
				this.Rightchar();
				if (this._bmPrefix._caseInsensitive)
				{
					this.CallToLower();
				}
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(num3);
				this.BeqFar(label14);
				this.Ldloc(tempV);
				this.Ldc(this._bmPrefix._lowASCII);
				this.Sub();
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(this._bmPrefix._highASCII - this._bmPrefix._lowASCII);
				this.Bgtun(label10);
				Label[] array = new Label[this._bmPrefix._highASCII - this._bmPrefix._lowASCII + 1];
				for (int i = this._bmPrefix._lowASCII; i <= this._bmPrefix._highASCII; i++)
				{
					if (this._bmPrefix._negativeASCII[i] == num)
					{
						array[i - this._bmPrefix._lowASCII] = label10;
					}
					else
					{
						array[i - this._bmPrefix._lowASCII] = this.DefineLabel();
					}
				}
				this.Ldloc(tempV);
				this._ilg.Emit(OpCodes.Switch, array);
				for (int i = this._bmPrefix._lowASCII; i <= this._bmPrefix._highASCII; i++)
				{
					if (this._bmPrefix._negativeASCII[i] != num)
					{
						this.MarkLabel(array[i - this._bmPrefix._lowASCII]);
						this.Ldc(this._bmPrefix._negativeASCII[i]);
						this.BrFar(label11);
					}
				}
				this.MarkLabel(label14);
				this.Ldloc(this._textposV);
				this.Stloc(tempV2);
				for (int i = this._bmPrefix._pattern.Length - 2; i >= 0; i--)
				{
					Label label15 = this.DefineLabel();
					int num4;
					if (!this._code._rightToLeft)
					{
						num4 = i;
					}
					else
					{
						num4 = this._bmPrefix._pattern.Length - 1 - i;
					}
					this.Ldloc(this._textV);
					this.Ldloc(tempV2);
					this.Ldc(1);
					this.Sub(this._code._rightToLeft);
					this.Dup();
					this.Stloc(tempV2);
					this.Callvirt(RegexCompiler._getcharM);
					if (this._bmPrefix._caseInsensitive)
					{
						this.CallToLower();
					}
					this.Ldc((int)this._bmPrefix._pattern[num4]);
					this.Beq(label15);
					this.Ldc(this._bmPrefix._positive[num4]);
					this.BrFar(label11);
					this.MarkLabel(label15);
				}
				this.Ldthis();
				this.Ldloc(tempV2);
				if (this._code._rightToLeft)
				{
					this.Ldc(1);
					this.Add();
				}
				this.Stfld(RegexCompiler._textposF);
				this.Ldc(1);
				this.Ret();
				this.MarkLabel(label12);
				this.Ldthis();
				if (!this._code._rightToLeft)
				{
					this.Ldthisfld(RegexCompiler._textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler._textbegF);
				}
				this.Stfld(RegexCompiler._textposF);
				this.Ldc(0);
				this.Ret();
				return;
			}
			if (this._fcPrefix == null)
			{
				this.Ldc(1);
				this.Ret();
				return;
			}
			LocalBuilder temp2V2 = this._temp2V;
			LocalBuilder tempV3 = this._tempV;
			Label label16 = this.DefineLabel();
			Label label17 = this.DefineLabel();
			Label label18 = this.DefineLabel();
			Label label19 = this.DefineLabel();
			Label label20 = this.DefineLabel();
			this.Mvfldloc(RegexCompiler._textposF, this._textposV);
			this.Mvfldloc(RegexCompiler._textF, this._textV);
			if (!this._code._rightToLeft)
			{
				this.Ldthisfld(RegexCompiler._textendF);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldthisfld(RegexCompiler._textbegF);
			}
			this.Sub();
			this.Stloc(temp2V2);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			this.BleFar(label19);
			this.MarkLabel(label16);
			this.Ldloc(temp2V2);
			this.Ldc(1);
			this.Sub();
			this.Stloc(temp2V2);
			if (this._code._rightToLeft)
			{
				this.Leftcharnext();
			}
			else
			{
				this.Rightcharnext();
			}
			if (this._fcPrefix.CaseInsensitive)
			{
				this.CallToLower();
			}
			if (!RegexCharClass.IsSingleton(this._fcPrefix.Prefix))
			{
				this.Ldstr(this._fcPrefix.Prefix);
				this.Call(RegexCompiler._charInSetM);
				this.BrtrueFar(label17);
			}
			else
			{
				this.Ldc((int)RegexCharClass.SingletonChar(this._fcPrefix.Prefix));
				this.Beq(label17);
			}
			this.MarkLabel(label20);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			if (!RegexCharClass.IsSingleton(this._fcPrefix.Prefix))
			{
				this.BgtFar(label16);
			}
			else
			{
				this.Bgt(label16);
			}
			this.Ldc(0);
			this.BrFar(label18);
			this.MarkLabel(label17);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this._code._rightToLeft);
			this.Stloc(this._textposV);
			this.Ldc(1);
			this.MarkLabel(label18);
			this.Mvlocfld(this._textposV, RegexCompiler._textposF);
			this.Ret();
			this.MarkLabel(label19);
			this.Ldc(0);
			this.Ret();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002E779 File Offset: 0x0002C979
		internal void GenerateInitTrackCount()
		{
			this.Ldthis();
			this.Ldc(this._trackcount);
			this.Stfld(RegexCompiler._trackcountF);
			this.Ret();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002E79E File Offset: 0x0002C99E
		internal LocalBuilder DeclareInt()
		{
			return this._ilg.DeclareLocal(typeof(int));
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002E7B5 File Offset: 0x0002C9B5
		internal LocalBuilder DeclareIntArray()
		{
			return this._ilg.DeclareLocal(typeof(int[]));
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		internal LocalBuilder DeclareString()
		{
			return this._ilg.DeclareLocal(typeof(string));
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		internal void GenerateGo()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._trackposV = this.DeclareInt();
			this._trackV = this.DeclareIntArray();
			this._stackposV = this.DeclareInt();
			this._stackV = this.DeclareIntArray();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			this._temp3V = this.DeclareInt();
			this._textbegV = this.DeclareInt();
			this._textendV = this.DeclareInt();
			this._textstartV = this.DeclareInt();
			this._labels = null;
			this._notes = null;
			this._notecount = 0;
			this._backtrack = this.DefineLabel();
			this.GenerateForwardSection();
			this.GenerateMiddleSection();
			this.GenerateBacktrackSection();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002E8B4 File Offset: 0x0002CAB4
		internal void GenerateOneCode()
		{
			this.Ldthis();
			this.Callvirt(RegexCompiler._checkTimeoutM);
			int regexopcode = this._regexopcode;
			if (regexopcode <= 285)
			{
				if (regexopcode <= 164)
				{
					switch (regexopcode)
					{
					case 0:
					case 1:
					case 2:
					case 64:
					case 65:
					case 66:
						goto IL_1438;
					case 3:
					case 4:
					case 5:
					case 67:
					case 68:
					case 69:
						goto IL_1604;
					case 6:
					case 7:
					case 8:
					case 70:
					case 71:
					case 72:
						goto IL_18EF;
					case 9:
					case 10:
					case 11:
					case 73:
					case 74:
					case 75:
						break;
					case 12:
						goto IL_1024;
					case 13:
					case 77:
						goto IL_11F6;
					case 14:
					{
						Label label = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ble(label);
						this.Leftchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 15:
					{
						Label label2 = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(label2);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 16:
					case 17:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler._isboundaryM);
						if (this.Code() == 16)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 18:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.BgtFar(this._backtrack);
						return;
					case 19:
						this.Ldloc(this._textposV);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.BneFar(this._backtrack);
						return;
					case 20:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Ldc(1);
						this.Sub();
						this.BltFar(this._backtrack);
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(this._labels[this.NextCodepos()]);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					case 21:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.BltFar(this._backtrack);
						return;
					case 22:
						this.Back();
						return;
					case 23:
						this.PushTrack(this._textposV);
						this.Track();
						return;
					case 24:
					{
						LocalBuilder tempV = this._tempV;
						Label label3 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV);
						this.PushTrack(tempV);
						this.Ldloc(this._textposV);
						this.Beq(label3);
						this.PushTrack(this._textposV);
						this.PushStack(this._textposV);
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(label3);
						this.TrackUnique2(5);
						return;
					}
					case 25:
					{
						LocalBuilder tempV2 = this._tempV;
						Label label4 = this.DefineLabel();
						Label label5 = this.DefineLabel();
						Label label6 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV2);
						this.Ldloc(tempV2);
						this.Ldc(-1);
						this.Beq(label5);
						this.PushTrack(tempV2);
						this.Br(label6);
						this.MarkLabel(label5);
						this.PushTrack(this._textposV);
						this.MarkLabel(label6);
						this.Ldloc(this._textposV);
						this.Beq(label4);
						this.PushTrack(this._textposV);
						this.Track();
						this.Br(this.AdvanceLabel());
						this.MarkLabel(label4);
						this.ReadyPushStack();
						this.Ldloc(tempV2);
						this.DoPush();
						this.TrackUnique2(6);
						return;
					}
					case 26:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 27:
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 28:
					{
						LocalBuilder tempV3 = this._tempV;
						LocalBuilder temp2V = this._temp2V;
						Label label7 = this.DefineLabel();
						Label label8 = this.DefineLabel();
						this.PopStack();
						this.Stloc(tempV3);
						this.PopStack();
						this.Dup();
						this.Stloc(temp2V);
						this.PushTrack(temp2V);
						this.Ldloc(this._textposV);
						this.Bne(label7);
						this.Ldloc(tempV3);
						this.Ldc(0);
						this.Bge(label8);
						this.MarkLabel(label7);
						this.Ldloc(tempV3);
						this.Ldc(this.Operand(1));
						this.Bge(label8);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV3);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(label8);
						this.PushTrack(tempV3);
						this.TrackUnique2(7);
						return;
					}
					case 29:
					{
						LocalBuilder tempV4 = this._tempV;
						LocalBuilder temp2V2 = this._temp2V;
						Label label9 = this.DefineLabel();
						this.DefineLabel();
						Label[] labels = this._labels;
						this.NextCodepos();
						this.PopStack();
						this.Stloc(tempV4);
						this.PopStack();
						this.Stloc(temp2V2);
						this.Ldloc(tempV4);
						this.Ldc(0);
						this.Bge(label9);
						this.PushTrack(temp2V2);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV4);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.TrackUnique2(8);
						this.Goto(this.Operand(0));
						this.MarkLabel(label9);
						this.PushTrack(temp2V2);
						this.PushTrack(tempV4);
						this.PushTrack(this._textposV);
						this.Track();
						return;
					}
					case 30:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.TrackUnique(0);
						return;
					case 31:
						this.PushStack(this._textposV);
						this.TrackUnique(0);
						return;
					case 32:
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(1));
							this.Callvirt(RegexCompiler._ismatchedM);
							this.BrfalseFar(this._backtrack);
						}
						this.PopStack();
						this.Stloc(this._tempV);
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldc(this.Operand(1));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler._transferM);
						}
						else
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler._captureM);
						}
						this.PushTrack(this._tempV);
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							this.TrackUnique(4);
							return;
						}
						this.TrackUnique(3);
						return;
					case 33:
						this.ReadyPushTrack();
						this.PopStack();
						this.Dup();
						this.Stloc(this._textposV);
						this.DoPush();
						this.Track();
						return;
					case 34:
						this.ReadyPushStack();
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.Ldloc(this._trackposV);
						this.Sub();
						this.DoPush();
						this.ReadyPushStack();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 35:
					{
						Label label10 = this.DefineLabel();
						Label label11 = this.DefineLabel();
						this.PopStack();
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.Beq(label11);
						this.MarkLabel(label10);
						this.Ldthis();
						this.Callvirt(RegexCompiler._uncaptureM);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.Bne(label10);
						this.MarkLabel(label11);
						this.Pop();
						this.Back();
						return;
					}
					case 36:
						this.PopStack();
						this.Stloc(this._tempV);
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.PushTrack(this._tempV);
						this.TrackUnique(9);
						return;
					case 37:
						this.Ldthis();
						this.Ldc(this.Operand(0));
						this.Callvirt(RegexCompiler._ismatchedM);
						this.BrfalseFar(this._backtrack);
						return;
					case 38:
						this.Goto(this.Operand(0));
						return;
					case 39:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
						goto IL_1AE4;
					case 40:
						this.Mvlocfld(this._textposV, RegexCompiler._textposF);
						this.Ret();
						return;
					case 41:
					case 42:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler._isECMABoundaryM);
						if (this.Code() == 41)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 76:
						goto IL_110B;
					default:
						switch (regexopcode)
						{
						case 131:
						case 132:
						case 133:
							goto IL_184F;
						case 134:
						case 135:
						case 136:
							goto IL_19D9;
						case 137:
						case 138:
						case 139:
						case 140:
						case 141:
						case 142:
						case 143:
						case 144:
						case 145:
						case 146:
						case 147:
						case 148:
						case 149:
						case 150:
						case 163:
							goto IL_1AE4;
						case 151:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.Goto(this.Operand(0));
							return;
						case 152:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopStack();
							this.Pop();
							this.TrackUnique2(5);
							this.Advance();
							return;
						case 153:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PushStack(this._textposV);
							this.TrackUnique2(6);
							this.Goto(this.Operand(0));
							return;
						case 154:
						case 155:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 156:
						{
							LocalBuilder tempV5 = this._tempV;
							Label label12 = this.DefineLabel();
							this.PopStack();
							this.Ldc(1);
							this.Sub();
							this.Dup();
							this.Stloc(tempV5);
							this.Ldc(0);
							this.Blt(label12);
							this.PopStack();
							this.Stloc(this._textposV);
							this.PushTrack(tempV5);
							this.TrackUnique2(7);
							this.Advance();
							this.MarkLabel(label12);
							this.ReadyReplaceStack(0);
							this.PopTrack();
							this.DoReplace();
							this.PushStack(tempV5);
							this.Back();
							return;
						}
						case 157:
						{
							Label label13 = this.DefineLabel();
							LocalBuilder tempV6 = this._tempV;
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopTrack();
							this.Dup();
							this.Stloc(tempV6);
							this.Ldc(this.Operand(1));
							this.Bge(label13);
							this.Ldloc(this._textposV);
							this.TopTrack();
							this.Beq(label13);
							this.PushStack(this._textposV);
							this.ReadyPushStack();
							this.Ldloc(tempV6);
							this.Ldc(1);
							this.Add();
							this.DoPush();
							this.TrackUnique2(8);
							this.Goto(this.Operand(0));
							this.MarkLabel(label13);
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.PushStack(tempV6);
							this.Back();
							return;
						}
						case 158:
						case 159:
							this.PopDiscardStack();
							this.Back();
							return;
						case 160:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Ldthis();
							this.Callvirt(RegexCompiler._uncaptureM);
							if (this.Operand(0) != -1 && this.Operand(1) != -1)
							{
								this.Ldthis();
								this.Callvirt(RegexCompiler._uncaptureM);
							}
							this.Back();
							return;
						case 161:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Back();
							return;
						case 162:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 164:
						{
							Label label14 = this.DefineLabel();
							Label label15 = this.DefineLabel();
							this.PopTrack();
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler._crawlposM);
							this.Beq(label15);
							this.MarkLabel(label14);
							this.Ldthis();
							this.Callvirt(RegexCompiler._uncaptureM);
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler._crawlposM);
							this.Bne(label14);
							this.MarkLabel(label15);
							this.Pop();
							this.Back();
							return;
						}
						default:
							goto IL_1AE4;
						}
						break;
					}
				}
				else
				{
					if (regexopcode - 195 <= 2)
					{
						goto IL_184F;
					}
					if (regexopcode - 198 <= 2)
					{
						goto IL_19D9;
					}
					switch (regexopcode)
					{
					case 280:
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.Back();
						return;
					case 281:
						this.ReadyReplaceStack(0);
						this.PopTrack();
						this.DoReplace();
						this.Back();
						return;
					case 282:
					case 283:
						goto IL_1AE4;
					case 284:
						this.PopTrack();
						this.Stloc(this._tempV);
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.PushStack(this._tempV);
						this.Back();
						return;
					case 285:
						this.ReadyReplaceStack(1);
						this.PopTrack();
						this.DoReplace();
						this.ReadyReplaceStack(0);
						this.TopStack();
						this.Ldc(1);
						this.Sub();
						this.DoReplace();
						this.Back();
						return;
					default:
						goto IL_1AE4;
					}
				}
			}
			else if (regexopcode <= 645)
			{
				switch (regexopcode)
				{
				case 512:
				case 513:
				case 514:
					goto IL_1438;
				case 515:
				case 516:
				case 517:
					goto IL_1604;
				case 518:
				case 519:
				case 520:
					goto IL_18EF;
				case 521:
				case 522:
				case 523:
					break;
				case 524:
					goto IL_1024;
				case 525:
					goto IL_11F6;
				default:
					switch (regexopcode)
					{
					case 576:
					case 577:
					case 578:
						goto IL_1438;
					case 579:
					case 580:
					case 581:
						goto IL_1604;
					case 582:
					case 583:
					case 584:
						goto IL_18EF;
					case 585:
					case 586:
					case 587:
						break;
					case 588:
						goto IL_110B;
					case 589:
						goto IL_11F6;
					default:
						if (regexopcode - 643 > 2)
						{
							goto IL_1AE4;
						}
						goto IL_184F;
					}
					break;
				}
			}
			else
			{
				if (regexopcode - 646 <= 2)
				{
					goto IL_19D9;
				}
				if (regexopcode - 707 <= 2)
				{
					goto IL_184F;
				}
				if (regexopcode - 710 > 2)
				{
					goto IL_1AE4;
				}
				goto IL_19D9;
			}
			this.Ldloc(this._textposV);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.BgeFar(this._backtrack);
				this.Rightcharnext();
			}
			else
			{
				this.Ldloc(this._textbegV);
				this.BleFar(this._backtrack);
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 11)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
				return;
			}
			this.Ldc(this.Operand(0));
			if (this.Code() == 9)
			{
				this.BneFar(this._backtrack);
				return;
			}
			this.BeqFar(this._backtrack);
			return;
			IL_1024:
			string text = this._strings[this.Operand(0)];
			this.Ldc(text.Length);
			this.Ldloc(this._textendV);
			this.Ldloc(this._textposV);
			this.Sub();
			this.BgtFar(this._backtrack);
			for (int i = 0; i < text.Length; i++)
			{
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				if (i != 0)
				{
					this.Ldc(i);
					this.Add();
				}
				this.Callvirt(RegexCompiler._getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text[i]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text.Length);
			this.Add();
			this.Stloc(this._textposV);
			return;
			IL_110B:
			string text2 = this._strings[this.Operand(0)];
			this.Ldc(text2.Length);
			this.Ldloc(this._textposV);
			this.Ldloc(this._textbegV);
			this.Sub();
			this.BgtFar(this._backtrack);
			int j = text2.Length;
			while (j > 0)
			{
				j--;
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				this.Ldc(text2.Length - j);
				this.Sub();
				this.Callvirt(RegexCompiler._getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text2[j]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text2.Length);
			this.Sub();
			this.Stloc(this._textposV);
			return;
			IL_11F6:
			LocalBuilder tempV7 = this._tempV;
			LocalBuilder temp2V3 = this._temp2V;
			Label label16 = this.DefineLabel();
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._ismatchedM);
			if ((this._options & RegexOptions.ECMAScript) != RegexOptions.None)
			{
				this.Brfalse(this.AdvanceLabel());
			}
			else
			{
				this.BrfalseFar(this._backtrack);
			}
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._matchlengthM);
			this.Dup();
			this.Stloc(tempV7);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._matchindexM);
			if (!this.IsRtl())
			{
				this.Ldloc(tempV7);
				this.Add(this.IsRtl());
			}
			this.Stloc(temp2V3);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.MarkLabel(label16);
			this.Ldloc(tempV7);
			this.Ldc(0);
			this.Ble(this.AdvanceLabel());
			this.Ldloc(this._textV);
			this.Ldloc(temp2V3);
			this.Ldloc(tempV7);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			if (!this.IsRtl())
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Beq(label16);
			this.Back();
			return;
			IL_1438:
			LocalBuilder tempV8 = this._tempV;
			Label label17 = this.DefineLabel();
			int num = this.Operand(1);
			if (num == 0)
			{
				return;
			}
			this.Ldc(num);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldloc(this._textposV);
			this.Ldc(num);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.Ldc(num);
			this.Stloc(tempV8);
			this.MarkLabel(label17);
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV8);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV8);
				this.Add();
			}
			else
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV8);
				this.Sub();
			}
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 2)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 0)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(tempV8);
			this.Ldc(0);
			if (this.Code() == 2)
			{
				this.BgtFar(label17);
				return;
			}
			this.Bgt(label17);
			return;
			IL_1604:
			LocalBuilder tempV9 = this._tempV;
			LocalBuilder temp2V4 = this._temp2V;
			Label label18 = this.DefineLabel();
			Label label19 = this.DefineLabel();
			int num2 = this.Operand(1);
			if (num2 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num2 != 2147483647)
				{
					Label label20 = this.DefineLabel();
					this.Dup();
					this.Ldc(num2);
					this.Blt(label20);
					this.Pop();
					this.Ldc(num2);
					this.MarkLabel(label20);
				}
				this.Dup();
				this.Stloc(temp2V4);
				this.Ldc(1);
				this.Add();
				this.Stloc(tempV9);
				this.MarkLabel(label18);
				this.Ldloc(tempV9);
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV9);
				this.Ldc(0);
				if (this.Code() == 5)
				{
					this.BleFar(label19);
				}
				else
				{
					this.Ble(label19);
				}
				if (this.IsRtl())
				{
					this.Leftcharnext();
				}
				else
				{
					this.Rightcharnext();
				}
				if (this.IsCi())
				{
					this.CallToLower();
				}
				if (this.Code() == 5)
				{
					this.Ldstr(this._strings[this.Operand(0)]);
					this.Call(RegexCompiler._charInSetM);
					this.BrtrueFar(label18);
				}
				else
				{
					this.Ldc(this.Operand(0));
					if (this.Code() == 3)
					{
						this.Beq(label18);
					}
					else
					{
						this.Bne(label18);
					}
				}
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.Stloc(this._textposV);
				this.MarkLabel(label19);
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Sub();
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.ReadyPushTrack();
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.DoPush();
				this.Track();
				return;
			}
			return;
			IL_184F:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._tempV);
			this.Ldloc(this._tempV);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._tempV);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.ReadyPushTrack();
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this.IsRtl());
			this.DoPush();
			this.Trackagain();
			this.Advance();
			return;
			IL_18EF:
			LocalBuilder tempV10 = this._tempV;
			int num3 = this.Operand(1);
			if (num3 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num3 != 2147483647)
				{
					Label label21 = this.DefineLabel();
					this.Dup();
					this.Ldc(num3);
					this.Blt(label21);
					this.Pop();
					this.Ldc(num3);
					this.MarkLabel(label21);
				}
				this.Dup();
				this.Stloc(tempV10);
				this.Ldc(0);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(tempV10);
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.PushTrack(this._textposV);
				this.Track();
				return;
			}
			return;
			IL_19D9:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._temp2V);
			if (!this.IsRtl())
			{
				this.Rightcharnext();
			}
			else
			{
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 8)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 6)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(this._temp2V);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._temp2V);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.PushTrack(this._textposV);
			this.Trackagain();
			this.Advance();
			return;
			IL_1AE4:
			throw new NotImplementedException(global::SR.GetString("Unimplemented state."));
		}

		// Token: 0x04000E53 RID: 3667
		internal static FieldInfo _textbegF = RegexCompiler.RegexRunnerField("runtextbeg");

		// Token: 0x04000E54 RID: 3668
		internal static FieldInfo _textendF = RegexCompiler.RegexRunnerField("runtextend");

		// Token: 0x04000E55 RID: 3669
		internal static FieldInfo _textstartF = RegexCompiler.RegexRunnerField("runtextstart");

		// Token: 0x04000E56 RID: 3670
		internal static FieldInfo _textposF = RegexCompiler.RegexRunnerField("runtextpos");

		// Token: 0x04000E57 RID: 3671
		internal static FieldInfo _textF = RegexCompiler.RegexRunnerField("runtext");

		// Token: 0x04000E58 RID: 3672
		internal static FieldInfo _trackposF = RegexCompiler.RegexRunnerField("runtrackpos");

		// Token: 0x04000E59 RID: 3673
		internal static FieldInfo _trackF = RegexCompiler.RegexRunnerField("runtrack");

		// Token: 0x04000E5A RID: 3674
		internal static FieldInfo _stackposF = RegexCompiler.RegexRunnerField("runstackpos");

		// Token: 0x04000E5B RID: 3675
		internal static FieldInfo _stackF = RegexCompiler.RegexRunnerField("runstack");

		// Token: 0x04000E5C RID: 3676
		internal static FieldInfo _trackcountF = RegexCompiler.RegexRunnerField("runtrackcount");

		// Token: 0x04000E5D RID: 3677
		internal static MethodInfo _ensurestorageM = RegexCompiler.RegexRunnerMethod("EnsureStorage");

		// Token: 0x04000E5E RID: 3678
		internal static MethodInfo _captureM = RegexCompiler.RegexRunnerMethod("Capture");

		// Token: 0x04000E5F RID: 3679
		internal static MethodInfo _transferM = RegexCompiler.RegexRunnerMethod("TransferCapture");

		// Token: 0x04000E60 RID: 3680
		internal static MethodInfo _uncaptureM = RegexCompiler.RegexRunnerMethod("Uncapture");

		// Token: 0x04000E61 RID: 3681
		internal static MethodInfo _ismatchedM = RegexCompiler.RegexRunnerMethod("IsMatched");

		// Token: 0x04000E62 RID: 3682
		internal static MethodInfo _matchlengthM = RegexCompiler.RegexRunnerMethod("MatchLength");

		// Token: 0x04000E63 RID: 3683
		internal static MethodInfo _matchindexM = RegexCompiler.RegexRunnerMethod("MatchIndex");

		// Token: 0x04000E64 RID: 3684
		internal static MethodInfo _isboundaryM = RegexCompiler.RegexRunnerMethod("IsBoundary");

		// Token: 0x04000E65 RID: 3685
		internal static MethodInfo _isECMABoundaryM = RegexCompiler.RegexRunnerMethod("IsECMABoundary");

		// Token: 0x04000E66 RID: 3686
		internal static MethodInfo _chartolowerM = typeof(char).GetMethod("ToLower", new Type[]
		{
			typeof(char),
			typeof(CultureInfo)
		});

		// Token: 0x04000E67 RID: 3687
		internal static MethodInfo _getcharM = typeof(string).GetMethod("get_Chars", new Type[] { typeof(int) });

		// Token: 0x04000E68 RID: 3688
		internal static MethodInfo _crawlposM = RegexCompiler.RegexRunnerMethod("Crawlpos");

		// Token: 0x04000E69 RID: 3689
		internal static MethodInfo _charInSetM = RegexCompiler.RegexRunnerMethod("CharInClass");

		// Token: 0x04000E6A RID: 3690
		internal static MethodInfo _getCurrentCulture = typeof(CultureInfo).GetMethod("get_CurrentCulture");

		// Token: 0x04000E6B RID: 3691
		internal static MethodInfo _getInvariantCulture = typeof(CultureInfo).GetMethod("get_InvariantCulture");

		// Token: 0x04000E6C RID: 3692
		internal static MethodInfo _checkTimeoutM = RegexCompiler.RegexRunnerMethod("CheckTimeout");

		// Token: 0x04000E6D RID: 3693
		internal ILGenerator _ilg;

		// Token: 0x04000E6E RID: 3694
		internal LocalBuilder _textstartV;

		// Token: 0x04000E6F RID: 3695
		internal LocalBuilder _textbegV;

		// Token: 0x04000E70 RID: 3696
		internal LocalBuilder _textendV;

		// Token: 0x04000E71 RID: 3697
		internal LocalBuilder _textposV;

		// Token: 0x04000E72 RID: 3698
		internal LocalBuilder _textV;

		// Token: 0x04000E73 RID: 3699
		internal LocalBuilder _trackposV;

		// Token: 0x04000E74 RID: 3700
		internal LocalBuilder _trackV;

		// Token: 0x04000E75 RID: 3701
		internal LocalBuilder _stackposV;

		// Token: 0x04000E76 RID: 3702
		internal LocalBuilder _stackV;

		// Token: 0x04000E77 RID: 3703
		internal LocalBuilder _tempV;

		// Token: 0x04000E78 RID: 3704
		internal LocalBuilder _temp2V;

		// Token: 0x04000E79 RID: 3705
		internal LocalBuilder _temp3V;

		// Token: 0x04000E7A RID: 3706
		internal RegexCode _code;

		// Token: 0x04000E7B RID: 3707
		internal int[] _codes;

		// Token: 0x04000E7C RID: 3708
		internal string[] _strings;

		// Token: 0x04000E7D RID: 3709
		internal RegexPrefix _fcPrefix;

		// Token: 0x04000E7E RID: 3710
		internal RegexBoyerMoore _bmPrefix;

		// Token: 0x04000E7F RID: 3711
		internal int _anchors;

		// Token: 0x04000E80 RID: 3712
		internal Label[] _labels;

		// Token: 0x04000E81 RID: 3713
		internal RegexCompiler.BacktrackNote[] _notes;

		// Token: 0x04000E82 RID: 3714
		internal int _notecount;

		// Token: 0x04000E83 RID: 3715
		internal int _trackcount;

		// Token: 0x04000E84 RID: 3716
		internal Label _backtrack;

		// Token: 0x04000E85 RID: 3717
		internal int _regexopcode;

		// Token: 0x04000E86 RID: 3718
		internal int _codepos;

		// Token: 0x04000E87 RID: 3719
		internal int _backpos;

		// Token: 0x04000E88 RID: 3720
		internal RegexOptions _options;

		// Token: 0x04000E89 RID: 3721
		internal int[] _uniquenote;

		// Token: 0x04000E8A RID: 3722
		internal int[] _goto;

		// Token: 0x04000E8B RID: 3723
		internal const int stackpop = 0;

		// Token: 0x04000E8C RID: 3724
		internal const int stackpop2 = 1;

		// Token: 0x04000E8D RID: 3725
		internal const int stackpop3 = 2;

		// Token: 0x04000E8E RID: 3726
		internal const int capback = 3;

		// Token: 0x04000E8F RID: 3727
		internal const int capback2 = 4;

		// Token: 0x04000E90 RID: 3728
		internal const int branchmarkback2 = 5;

		// Token: 0x04000E91 RID: 3729
		internal const int lazybranchmarkback2 = 6;

		// Token: 0x04000E92 RID: 3730
		internal const int branchcountback2 = 7;

		// Token: 0x04000E93 RID: 3731
		internal const int lazybranchcountback2 = 8;

		// Token: 0x04000E94 RID: 3732
		internal const int forejumpback = 9;

		// Token: 0x04000E95 RID: 3733
		internal const int uniquecount = 10;

		// Token: 0x02000143 RID: 323
		internal sealed class BacktrackNote
		{
			// Token: 0x06000965 RID: 2405 RVA: 0x000303B5 File Offset: 0x0002E5B5
			internal BacktrackNote(int flags, Label label, int codepos)
			{
				this._codepos = codepos;
				this._flags = flags;
				this._label = label;
			}

			// Token: 0x04000E96 RID: 3734
			internal int _codepos;

			// Token: 0x04000E97 RID: 3735
			internal int _flags;

			// Token: 0x04000E98 RID: 3736
			internal Label _label;
		}
	}
}
