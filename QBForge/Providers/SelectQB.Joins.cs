using QBForge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBForge.Providers
{
	internal partial class SelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.Join<TJoined>(Action<ISelectQB<TJoined>> subQuery, string label) => this;
		ISelectQB<T> ISelectQB<T>.Join<TJoined>(string tableName, string? label = null) => this;
		ISelectQB<T> ISelectQB<T>.LeftJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string label) => this;
		ISelectQB<T> ISelectQB<T>.LeftJoin<TJoined>(string tableName, string? label = null) => this;
		ISelectQB<T> ISelectQB<T>.RightJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string label) => this;
		ISelectQB<T> ISelectQB<T>.RightJoin<TJoined>(string tableName, string? label = null) => this;
		ISelectQB<T> ISelectQB<T>.FullJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string label) => this;
		ISelectQB<T> ISelectQB<T>.FullJoin<TJoined>(string tableName, string? label = null) => this;
		ISelectQB<T> ISelectQB<T>.CrossJoin<TJoined>(Action<ISelectQB<TJoined>> subQuery, string label) => this;
		ISelectQB<T> ISelectQB<T>.CrossJoin<TJoined>(string tableName, string? label = null) => this;
	}
}
