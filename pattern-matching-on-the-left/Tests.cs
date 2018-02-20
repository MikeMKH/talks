using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
  public class Tests
  {
    [Fact]
    public void GivenCollectionWeCanIterateThroughIt()
    {
      IEnumerable<int> col = new HashSet<int>() { 1, 2, 3 };
      IEnumerator<int> iterator = col.GetEnumerator();

      var results = new LinkedList<int>();
      while (iterator.MoveNext())
      {
        results.AddLast(iterator.Current);
      }

      Assert.Equal(results, col);
    }
  }
}
