﻿namespace QBForge.Interfaces
{
	public interface IAggrCall : IRenderContext { }

	public delegate IAggrCall AggrCallClauseDe(IAggrCall render, DataEntry arg);

}