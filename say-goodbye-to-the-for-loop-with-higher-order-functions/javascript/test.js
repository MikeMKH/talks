/*jshint esversion: 6 */ 
const assert = require('assert');

// mocha --check-leaks --watch
describe('Fusion property of iterators', () => {
  const orders = [
    {zip: 53202, price: 1.89, quantity: 3},
    {zip: 60191, price: 1.99, quantity: 2},
    {zip: 60060, price: 0.99, quantity: 7},
    {zip: 53202, price: 1.29, quantity: 8},
    {zip: 60191, price: 1.89, quantity: 2},
    {zip: 53202, price: 0.99, quantity: 3}
  ];
  
  it ('must produce the value expected', () => {
    const result = orders
      .filter(order => order.zip === 53202)
      .map(order => order.price * order.quantity)
      .reduce((sub, amount) => sub + amount, 0.0);
      
    assert.equal(result, 18.96);
  });
});