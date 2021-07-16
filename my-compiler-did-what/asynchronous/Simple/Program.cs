using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/*
 * static async Task Main (string[] args)
 */
var stateMachine = default(Program);
stateMachine._builder = AsyncTaskMethodBuilder.Create();
stateMachine._state = -1;
stateMachine._builder.Start(ref stateMachine); // Program.MoveNext()
stateMachine._builder.Task.GetAwaiter().GetResult();

[StructLayout(LayoutKind.Auto)]
struct Program : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;
	private TaskAwaiter _awaiter;

	/*
	 * await PrintAndWaitWrapper(TimeSpan.FromMilliseconds(10));
	 */
	[AsyncStateMachine(typeof(PrintAndWaitAsync))]
	static Task PrintAndWaitWrapper(TimeSpan delay)
	{
		PrintAndWaitAsync stateMachine = default(PrintAndWaitAsync);
		stateMachine._builder = AsyncTaskMethodBuilder.Create();
		stateMachine.delay = delay;
		stateMachine._state = -1;
		stateMachine._builder.Start(ref stateMachine); // PrintAndWaitAsync.MoveNext()
		return stateMachine._builder.Task;
	}

	/*
	 * await PrintAndWaitWrapper(TimeSpan.FromMilliseconds(10));
	 */
	public void MoveNext()
	{
		int num = _state;
		try
		{
			TaskAwaiter awaiter;
			if (num != 0)
			{
				awaiter = PrintAndWaitWrapper(TimeSpan.FromMilliseconds(10)).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					num = (_state = 0);
					_awaiter = awaiter;
					_builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			else
			{
				awaiter = _awaiter;
				_awaiter = default(TaskAwaiter);
				num = (_state = -1);
			}
		}
		catch (Exception exception)
		{
			_state = -2;
			_builder.SetException(exception);
			return;
		}
		_state = -2;
		_builder.SetResult();
	}

	void IAsyncStateMachine.MoveNext()
	{
		this.MoveNext();
	}

	private void SetStateMachine(IAsyncStateMachine stateMachine)
	{
		_builder.SetStateMachine(stateMachine);
	}

	void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
	{
		this.SetStateMachine(stateMachine);
	}
}

[StructLayout(LayoutKind.Auto)]
struct PrintAndWaitAsync : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;

	public TimeSpan delay;

	private TaskAwaiter _awaiter;

	/*
	 static async Task PrintAndWait(TimeSpan delay)
	 {
       Console.WriteLine("before delays");
       await Task.Delay(delay);
       Console.WriteLine("between delays");
       await Task.Delay(delay);
       Console.WriteLine("after delays");
    }
	*/
	private void MoveNext()
	{
		int num = _state;
		try
		{
			TaskAwaiter awaiter;
			if (num != 0)
			{
				if (num == 1)
				{
					awaiter = _awaiter;
					_awaiter = default(TaskAwaiter);
					num = (_state = -1);
					goto done;
				}
				Console.WriteLine("before delays");
				awaiter = Task.Delay(delay).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					num = (_state = 0);
					_awaiter = awaiter;
					_builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			else
			{
				awaiter = _awaiter;
				_awaiter = default(TaskAwaiter);
				num = (_state = -1);
			}
			awaiter.GetResult();
			Console.WriteLine("between delays");
			awaiter = Task.Delay(delay).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				num = (_state = 1);
				_awaiter = awaiter;
				_builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
			}
			goto done;
		done:
			awaiter.GetResult();
			Console.WriteLine("after delays");
		}
		catch (Exception exception)
		{
			_state = -2;
			_builder.SetException(exception);
			return;
		}
		_state = -2;
		_builder.SetResult();
	}

	void IAsyncStateMachine.MoveNext()
	{
		this.MoveNext();
	}

	private void SetStateMachine(IAsyncStateMachine stateMachine)
	{
		_builder.SetStateMachine(stateMachine);
	}

	void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
	{
		this.SetStateMachine(stateMachine);
	}
}