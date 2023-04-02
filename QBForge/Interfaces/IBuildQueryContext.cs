﻿using System.Text;

namespace QBForge.Interfaces
{
	public interface IBuildQueryContext
	{
		IRenderContext RenderContext { get; }
		StringBuilder Output { get; }

		int ParameterCount { get; }
		object? Parameters { get; }

		string MakeParamPlaceholder();
	}
}