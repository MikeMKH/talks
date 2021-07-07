using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

var sequence = new Fibonacci(-2);
foreach (var n in sequence.Take(10))
{
    Console.Write($"{n}, ");
}
Console.WriteLine($"{sequence.ElementAt(10)}");

/*
 static IEnumerable<int> Fibonacci()
{
    yield return 0;

    int value = 1;
    int next = 1;
    while (true)
    {
        yield return value;

        int t = value;
        value = next;
        next += t;
    }
}
*/

public class Fibonacci : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable
{
	private int _state;
	private int _current;
	private int _initialThreadId;

	private int value;
	private int next;

	int IEnumerator<int>.Current
	{
		get
		{
			return _current;
		}
	}

	object IEnumerator.Current
	{
		get
		{
			return _current;
		}
	}

	public Fibonacci(int state)
	{
		this._state = state;
		_initialThreadId = Environment.CurrentManagedThreadId;
	}

	void IDisposable.Dispose()
	{
	}

	private bool MoveNext()
	{
		switch (_state)
		{
			default:
				return false;
			case 0:
				// seed
				_state = -1;
				_current = 0;  // Fibonacci(0)
				_state = 1;
				return true;
			case 1:
				// 1st
				_state = -1;
				value = 1;    // Fibonacci(1)
				next = 1;     // Fibonacci(2)
				break;
			case 2:
				{
					// rest
					_state = -1;
					int num = value;
					value = next;
					next += num;
					break;
				}
		}
		_current = value;
		_state = 2;
		return true;
	}

	bool IEnumerator.MoveNext()
	{
		return this.MoveNext();
	}

	void IEnumerator.Reset()
	{
		throw new NotSupportedException();
	}

	IEnumerator<int> IEnumerable<int>.GetEnumerator()
	{
		if (_state == -2 && _initialThreadId == Environment.CurrentManagedThreadId)
		{
			_state = 0;
			return this;
		}
		return new Fibonacci(0);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable<int>)this).GetEnumerator();
	}
}
