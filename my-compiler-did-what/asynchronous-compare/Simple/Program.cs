using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

var x = Identity(6);
int y = -1;
var awaiter = IdentityAsyncWraper(7).GetAwaiter();
if (awaiter.IsCompleted)
{
	y = awaiter.GetResult();
}

Console.WriteLine($"{x:X4} + {y:X4} = {x * y:X4}");

static T Identity<T>(T x) => x;
// static async Task<T> IdentityAsync<T>(T x) => x;

[AsyncStateMachine(typeof(IdentityAsync<>))]
static Task<T> IdentityAsyncWraper<T>(T x)
{
	IdentityAsync<T> stateMachine = default(IdentityAsync<T>);
	stateMachine._builder = AsyncTaskMethodBuilder<T>.Create();
	stateMachine.x = x;
	stateMachine._state = -1;
	stateMachine._builder.Start(ref stateMachine);
	return stateMachine._builder.Task;
}

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