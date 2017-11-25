let assert = require('assert');
let jsc = require('jsverify');

describe('Subtraction', () => {
  describe('commutes', () => {
    it('0 - 0 === 0 - 0', () => {
      assert.deepEqual(0 - 0, 0 - 0);
    });
    // jsc.property(
    //   'forall a, b: int, a - b = b - a',
    //   jsc.integer, jsc.integer,
    //   (a, b) => a - b === b - a
    // );
  }),
  describe('does not commute', () => {
    jsc.property(
      'forall a, b: int, a <> b -> a - b <> b - a',
      jsc.integer, jsc.integer,
      (a, b) => (a !== b) ? a - b !== b - a : true
    );
  });
});