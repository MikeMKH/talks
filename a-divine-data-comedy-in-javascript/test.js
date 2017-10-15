// npm install --save mocha lodash jshint folktale

const assert = require('assert');

const _ = require('lodash');
const { flow, flowRight, map, filter, reduce ,split, size, replace, toInteger, add, concat } = require('lodash/fp');

const Maybe = require('data.maybe');

describe('Data Flow', () =>  {
  describe('Imperative', () => {
    it('should return 6', () => {
      const s = 'Midway in our life\'s journey, I went astray';
      var count = 0;
      for(var word of _.split(s, ' ')) {
        var stripped = _.replace(word, '(\'s)|\W+', '');
        if (stripped.length > 2)
          count++;
      }
      assert.equal(count, 6);
    });
  }),
  describe('Nested',() => {
    it('should return 6', () => {
      const s = 'Midway in our life\'s journey, I went astray';
      const count = _.size(
        _.filter(
          _.map(
            _.split(s, ' '),
            word => _.replace(word, '(\'s)|\W+', '')
          ),
          stripped => _.gt(stripped.length, 2)
        )
      );
      assert.equal(count, 6);
    });
  }),
  describe('Pipe', () => {
    it('should return 6', () => {
      const count = flow([
        split(' '),
        map(
          replace('(\'s)|\W+')('')),
        filter(stripped => _.gt(stripped.length, 2)),
        size
      ])('Midway in our life\'s journey, I went astray');
      assert.equal(count, 6);
    });
  });
});

describe('Language of Data Flow', () => {
  describe('map', () => {
    it('should map function', () => {
      const numbers = _.map([3, 9, 10], x => x * 10);
      assert.deepEqual(numbers, [30, 90, 100]);
    });
  }),
  describe('flat map', () => {
    it('should be able to exploded the contents', () => {
      const list = _.flatMap([
          'Midway in our life\'s journey,',
          'I went astray'
        ],
        s => _.split(s, ' ')
      );
      assert.deepEqual(list,
        ['Midway', 'in', 'our', 'life\'s', 'journey,',
         'I', 'went', 'astray']);
    });
  }),
  describe('filter', () => {
    it('should filter even numbers', () => {
      const numbers = _.filter([3, 9, 10, 33, 100], n => n % 2 != 0);
      assert.deepEqual(numbers, [3, 9, 33]);
    });
  }),
  describe('fold', () => {
    it('should sum numbers', () => {
      const sum = _.reduce(
        [3, 9, 10, 33, 100],
        (m, x) => m + x,
        0
      );
      assert.equal(sum, 155);
    }),
    it('should sum numbers without seed', () => {
      const sum = _.reduce([3, 9, 10, 33, 100], _.add);
      assert.equal(sum, 155);
    }),
    it('should return undefined when given empty with no seed', () => {
      const result = _.reduce([], _.add);
      assert.ok(_.isUndefined(result));
    });
  }),
  describe('mutate', () => {
    it('should add numbers divisible by 3 to array', () => {
      let r = [];
      _.each(
        [3, 9, 10, 33, 100],
        n => { if (n % 3 === 0) { r.push(n); } }
      );
      assert.deepEqual(r, [3, 9, 33]);
    });
  }),
  describe('group by', () => {
    it('should return an object with the groupings as the keys', () => {
      const groups = _.groupBy([3, 9, 10, 33, 100], n => n % 2 === 0);
      assert.deepEqual(groups, {'true': [10, 100], 'false': [3, 9, 33]});
    });
  }),
  describe('order by', () => {
    it('should sort the array', () => {
      const ordered = _.orderBy([33, 9, 100, 3, 10]);
      assert.deepEqual(ordered, [3, 9, 10, 33, 100]);
    });
  });
});

describe('Composing Functions', () => {
  describe('Data Flow', () => {
    it('using flow', () => {
      const inc = flow([
        toInteger,
        add(1)
      ]);
      assert.equal(inc(5), 6);
      assert.equal(inc('5'), 6);
      assert.equal(inc('five'), 1);
    }),
    it('using compose', () => {
      const inc = flowRight([
        add(1),
        toInteger
      ]);
      assert.equal(inc(5), 6);
      assert.equal(inc('5'), 6);
      assert.equal(inc('five'), 1);
    });
  }),
  describe('Context', () => {
    describe('Identity', () => {
      class Identity {
        static of(value) { return [value]; }
      }
      it('should do nothing', () => {
        const r = Identity.of('33')
          .map(toInteger)
          .map(add(1))
          .shift();
        assert.equal(r, 34);
      }),
      it('should be chainable', () => {
        const count = Identity.of(
          'Midway in our life\'s journey, I went astray')
          .map(split(' ')) // flatMap = map -> reduce(concat)
          .reduce(concat)
          .map(replace('(\'s)|\W+')(''))
          .filter(word => _.gt(word.length, 2))
          .reduce(number => ++number, 0);
        assert.equal(count, 6);
      });
    }),
    describe('String', () => {
      it('should be chainable', () => {
        const count = 'Midway in our life\'s journey, I went astray'
          .split(' ')
          .map(replace('(\'s)|\W+')(''))
          .filter(word => _.gt(word.length, 2))
          .reduce(number => ++number, 0);
        assert.equal(count, 6);
      });
    }),
    describe('if context', () => {
      const size = s => {
        if (s) {
          return s.trim().length;
        } else {
          return 0;
        }
      };
      
      assert.equal(size('Midway'), 6);
      assert.equal(size(' '), 0);
      assert.equal(size(null), 0);
    }),
    describe('Maybe context', () => {
      const size = s =>
        Maybe.fromNullable(s)
          .map(s => s.trim())
          .cata({
            Nothing: _ => 0,
            Just: t => t.length});

      assert.equal(size('Midway'), 6);
      assert.equal(size(' '), 0);
      assert.equal(size(null), 0);
    });
  }),
  describe('Maybe context', () => {
    describe('word counter', () => {
      const counter = s =>
        Maybe.fromNullable(s)
          .map(s => s.split(' ')
                      .map(replace('(\'s)|\W+')(''))
                      .filter(word => _.gt(word.length, 2)))
          .cata({
            Nothing: _ => 0,
            Just: words => words.length
          });
      it('should support a string', () => {
        const count = counter('Midway in our life\'s journey, I went astray');
        assert.equal(count, 6);
      }),
      it('should support null', () => {
        const count = counter(null);
        assert.equal(count, 0);
      });
    });
  });
});
