﻿using System;
using System.Collections;
using System.Data.Common;

namespace QBForge.Interfaces
{
	public interface IQBContext
	{
		IQBContext Clone();

		public Clause Clause { get; }

		IQBProvider Provider { get; }

		int LastBuild { get; set; }
		string LastQuery { get; set; }
		DbParameterCollection LastParameters { get; }

		string MapNextTo { get; set; }
		Delegate? Map { get; set; }

		object? Tag { get; set; }

		void SetClause(ClauseEntry clause);
	}
}