'use strict';

// common in JS to pass functions as arguments
// especially in an asynchronous context.
// in that case they are called "callback functions"

// with callback functions:
function slowCalculation(a, b, successCallback, failureCallback) {
  let result = a + b;
  setTimeout(() => successCallback(result), 1000);
}
// ^ "library code"
//  library code is calling back to the calling code
// v "calling code"
slowCalculation(2, 3, result => {
  console.log(result);
});
console.log('asdf');

// // another example, more like LINQ
let obj1 = {
  list: [1, 2, 3],
  log() {
    this.list.forEach(x => console.log(this.list, x));
  }
};
obj1.log();

// // ES6 added "for..of" loop which is like C#'s foreach.
for (const x of [1, 2, 3]) {
  console.log(x);
}


// Promise
// helps us manage callbacks more effectively
// in an asynchronous context.

console.log('-----------------------');

function newCounter() {
  let count = 0;

  return function () {
    // if a function references a variable,
    // it "closes over" that variable, which keeps it alive
    // until the function is itself cleaned up
    // in CS, functions that work this way are called closures;
    // this behavior is called closure
    count++;
    return count;
  };

  // this is the end of the block,
  // so isn't that local variable gone / out of scope now?
  // shouldn't it see the global variable.

  // JS designers decided that
  // if you reference a local variable from an
  // "inner function", that inner function will
  // keep that variable alive as long as it itself remains

  // this behavior is called closure -
  // the inner function "closes over" the variables
  // it references.
}

const counter = newCounter();

console.log(counter()); // 1
console.log(counter()); // 2
console.log(counter()); // 3
console.log(counter()); // 4

const counter2 = newCounter();

console.log(counter2()); // 1
console.log(counter2()); // 2


console.log(counter()); // 5

// // --------------------



// another technique involving variable scope

// it's not good to pollute the global scope with
// your stuff
// we want more encapsulation than that.

// immediately-invoked function expression
// (IIFE)

(function () {
  // stuff
  // i can define temporary variables in here without
  // polluting the global scope
})();

let library = (function () {
  let privateData = 1;

  let privateFunction = function (value) {
    privateData += value;
  }

  return {
    publicData: 0,

    publicFunction(data) {
      this.publicData = privateFunction(data) + privateData;
      console.log(privateData);
    }
  }
})();
library.publicData = 5;
console.log(library.publicFunction(123));

// new in ES6
// http://es6-features.org/

// block scope (let, const)
// arrow functions
//     this
// method syntax for function properties
// default parameters
// string interpolation `-- ${x} --`
// classes with constructors, inheritance
// symbol type
// various useful built-in functions like string searching
// Promises
// es6 modules (import, export)
// Set and Map data structures
// for of
// internationalization features (number/date/currency format)
// spread, destructuring
