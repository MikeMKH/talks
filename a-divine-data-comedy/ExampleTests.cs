using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.ValueTuple;
using Xunit;

namespace dotnet
{
  public class ExampleTests
  {
    [Fact]
    public void ImperativeStyle()
    {
      var s = "Midway in our life's journey, I went astray";
      var count = 0;
      foreach(var word in s.Split(' '))
      {
        var stripped = 
          Regex.Replace(word, "('s)|\\W+", "");
        if (stripped.Length > 2)
          count++;
      }
      Assert.Equal(6, count);
    }

    [Fact]
    public void NestedStyle()
    {
      var s = "Midway in our life's journey, I went astray";
      var count = Enumerable.Count(
        Enumerable.Where(
          Enumerable.Select(
            s.Split(' '),
            word => Regex.Replace(
              word, "('s)|\\W+", "")),
          stripped => stripped.Length > 2));
      Assert.Equal(6, count);
    }

    [Fact]
    public void DeclarativeStyle()
    {
      var count = "Midway in our life's journey, I went astray"
        .Split(' ')
        .Select(word =>
          Regex.Replace(word, "('s)|\\W+", ""))
        .Where(word => word.Length > 2)
        .Count();
      Assert.Equal(6, count);
    }

    [Fact]
    public void DataFlowManipulationsExample()
    {
      var count = new[] {
          "Midway in our life's journey, I went astray"
        }
        .SelectMany(s => s.Split(' '))
        .Select(word =>
          Regex.Replace(word, "('s)|\\W+", ""))
        .Where(word => word.Length > 2)
        .Aggregate(0, (number, _) => number + 1);
      Assert.Equal(6, count);
    }

    [Fact]
    public void MapExample()
    {
      var numbers = new [] {3, 9, 10}
        .Select(x => x * 10)
        .Select(x => x + 3);
      Assert.Equal(new [] {33, 93, 103}, numbers);
    }

    [Fact]
    public void BindExample1()
    {
      var list = new [] { 
          "Midway in our life's journey,",
          "I went astray"
        }
        .SelectMany(s => s.Split(' '))
        .Select(w => Regex.Replace(w, "('s)|\\W+", ""));
      Assert.Equal(
        new [] { "Midway", "in", "our", "life", "journey", "I", "went", "astray" },
        list);
    }

    [Fact]
    public void BindExample2()
    {
      var list =
        from sentences in
          new [] {
            "Midway in our life's journey,", 
            "I went astray"
          }
        from words in sentences.Split(' ')
        select Regex.Replace(words, "('s)|\\W+", "");
      Assert.Equal(
        new [] { "Midway", "in", "our", "life", "journey", "I", "went", "astray" },
        list);
    }

    [Fact]
    public void FilterExample()
    {
      var numbers = new [] {3, 9, 10, 33, 100}
        .Where(n => n % 2 != 0);
      Assert.Equal(new [] {3, 9, 33}, numbers);
    }

    [Fact]
    public void FoldExample()
    {
      var sum = new [] {3, 9, 10, 33, 100}
        .Aggregate(0, (m, n) => m + n);
      Assert.Equal(
        new [] {3, 9, 10, 33, 100}.Sum(), sum);
    }

    [Fact]
    public void MutateExample()
    {
      var visited = new List<int>();
      new [] {3, 9, 10, 33, 100}
        .Where(n => n % 3 == 0)
        .ToList()
        .ForEach(n => visited.Add(n));
      Assert.Equal(new [] {3, 9, 33}, visited);
    }

    [Fact]
    public void GroupByExample()
    {
      var groups = new [] {3, 9, 10, 33, 100}
        .GroupBy(n => n % 2 == 0)
        .ToDictionary(g => g.Key, g => g.ToList());
      Assert.Equal(
        new Dictionary<bool, List<int>>()
        { 
          {true, new List<int>() {10, 100}},
          {false, new List<int>() {3, 9, 33}}
        },
        groups);
    }

    [Fact]
    public void OrderByExample()
    {
      var unordered = new [] {33, 9, 100, 3, 10}
        .OrderBy(n => n);
      
      var ordered = new [] {33, 9, 100, 3, 10};
      Array.Sort(ordered);
      Assert.Equal(ordered, unordered);
    }

      string G (int x) => x.ToString();
      bool F (string y) => int.TryParse(y, out int _);

      class Identity
      {
        public static T [] Of<T>(T x) => new [] { x };
      }

      [Fact]
      public void F_Following_G_Identity()
        => Assert.True(
            Identity.Of(33)
              .Select(G)
              .Select(F)
              .First()
          );

    [Fact]
    public void IdentityExample()
    {
      var count = Identity.Of(
          "Midway in our life's journey, I went astray")
        .SelectMany(s => s.Split(' '))
        .Select(word =>
          Regex.Replace(word, "('s)|\\W+", ""))
        .Where(word => word.Length > 2)
        .Aggregate(0, (number, _) => number + 1);
      Assert.Equal(6, count);
    }

    [Fact]
    public void StringExample()
    {
      var count = "Midway in our life's journey, I went astray"
        .Split(' ')
        .Select(word => Regex.Replace(word, "('s)|\\W+", ""))
        .Where(word => word.Length > 2)
        .Count();
      Assert.Equal(6, count);
    }
  }
}
