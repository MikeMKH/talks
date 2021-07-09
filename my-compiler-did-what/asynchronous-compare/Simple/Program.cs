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
stateMachine._builder.Start(ref stateMachine);
stateMachine._builder.Task.GetAwaiter().GetResult();

[StructLayout(LayoutKind.Auto)]
struct Program : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder _builder;
	private TaskAwaiter<int> _awaiter;

	private int x;

	private static T Identity<T>(T x) => x;

	/*
	 * static async Task<T> IdentityAsync<T>(T x) => x;
	 */
	[AsyncStateMachine(typeof(IdentityAsync<>))]
	private static Task<T> IdentityAsyncWraper<T>(T x)
	{
		IdentityAsync<T> stateMachine = default(IdentityAsync<T>);
		stateMachine._builder = AsyncTaskMethodBuilder<T>.Create();
		stateMachine.x = x;
		stateMachine._state = -1;
		stateMachine._builder.Start(ref stateMachine);
		return stateMachine._builder.Task;
	}

	public void MoveNext()
	{
		int num = _state;
		try
		{
			TaskAwaiter<int> awaiter;
			if (num != 0)
			{
				x = Identity(6);
				awaiter = IdentityAsyncWraper(7).GetAwaiter();
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
				_awaiter = default(TaskAwaiter<int>);
				num = (_state = -1);
			}
			int y = awaiter.GetResult();
			Console.WriteLine($"{x:X4} + {y:X4} = {x * y:X4}");
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

/*
 * static async Task<T> IdentityAsync<T>(T x) => x;
 */
[StructLayout(LayoutKind.Auto)]
struct IdentityAsync<T> : IAsyncStateMachine
{
	public int _state;
	public AsyncTaskMethodBuilder<T> _builder;

	public T x;

	private void MoveNext()
	{
		T result;
		try
		{
			result = x;
		}
		catch (Exception exception)
		{
			_state = -2;
			_builder.SetException(exception);
			return;
		}
		_state = -2;
		_builder.SetResult(result);
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