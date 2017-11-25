let assert = require('assert');

describe('Subtraction', () => {
  describe('commutes', () => {
    it('0 - 0 === 0 - 0', () => {
      assert.deepEqual(0 - 0, 0 - 0);
    });
  });
});