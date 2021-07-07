using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

var sequence = new EnumerableFibonacci(-2);
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

public class EnumerableFibonacci : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable
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

	public EnumerableFibonacci(int state)
	{
		this._state = state;
		_initialThreadId = Environment.CurrentManagedThreadId;
	}

	void IDisposable.Dispose()
	{
	}

	private bool MoveNext()
	{
		const int ERROR = -1;
		switch (_state)
		{
			default:
				return false;
			case 0:
				// initial
				_state = ERROR;
				_current = 0;  // Fibonacci(0)
				_state = 1;
				return true;
			case 1:
				// 1st
				_state = ERROR;
				value = 1;    // Fibonacci(1)
				next = 1;     // Fibonacci(2)
				break;
			case 2:
				// rest
				_state = ERROR;
				int temp = value;
				value = next;
				next += temp;
				break;
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
		return new EnumerableFibonacci(0);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable<int>)this).GetEnumerator();
	}
}
