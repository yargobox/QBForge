using QBForge.Extensions.Linq.Expressions;
using QBForge.Interfaces;
using System;
using System.Linq.Expressions;

namespace QBForge.Providers
{
	internal sealed partial class SelectQB<T> : ISelectQB<T>
	{
		ISelectQB<T> ISelectQB<T>.Map(Func<T, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map<TSecond>(Func<T, TSecond, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map<TSecond, TThird>(Func<T, TSecond, TThird, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map<TSecond, TThird, TFourth>(Func<T, TSecond, TThird, TFourth, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map<TSecond, TThird, TFourth, TFifth>(Func<T, TSecond, TThird, TFourth, TFifth, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map<TSecond, TThird, TFourth, TFifth, TSixth>(Func<T, TSecond, TThird, TFourth, TFifth, TSixth, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map<TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(Func<T, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, T> map)
		{
			_context.Map = map;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.Map(Func<object[], T> map)
		{
			_context.Map = map;
			return this;
		}


		ISelectQB<T> ISelectQB<T>.MapNextTo<TMapTo>(Expression<Func<T, TMapTo>> mappedObject)
		{
			var (paramName, memberName) = mappedObject.GetMemberName(true);

			// The expression also can be used to make a setter instead of getter, or setter could be taken from the cache

			_context.MapNextTo = paramName;
			return this;
		}

		ISelectQB<T> ISelectQB<T>.MapTo<TMapTo>(Expression<Func<T, TMapTo>> mappedObject, Action<ISelectQB<T>> includes)
		{
			var (paramName, memberName) = mappedObject.GetMemberName(true);

			var prevMapNextTo = _context.MapNextTo;

			try
			{
				_context.MapNextTo = paramName;

				includes(this);
			}
			finally
			{
				_context.MapNextTo = prevMapNextTo;
			}
			return this;
		}
	}
}
