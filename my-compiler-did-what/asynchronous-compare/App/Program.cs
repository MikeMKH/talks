using System;
using System.Threading.Tasks;

var x = Identity(6);
var y = await IdentityAsync(7);

Console.WriteLine($"{x:X4} + {y:X4} = {x * y:X4}");

static T Identity<T>(T x) => x;
static async Task<T> IdentityAsync<T>(T x) => x;