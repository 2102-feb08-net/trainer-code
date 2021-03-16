'use strict';

function myFunction(a) {
  return this.property.length + 43;
}

// object literal syntax
let obj = {
  property: 'value',
  func: myFunction,
  behavior: function (a) {
    // debugger; // breakpoint
    return a * this.property.length;
  }
};

// obj.behavior = myFunction;
console.log(obj.behavior(3)); // 15
//           |
//          this

const theFunction = obj.behavior;
// console.log(theFunction(obj, 3));

// "this" in JS means...
//    the object to the left of the dot when the function was called. (or undefined if none)

// }

// JS lets us use any function as a constructor with the "new" operator
function myFunction2(a) {
  return 43;
}

let obj2 = new myFunction2();
console.log(obj2);

// what the new operator does is create a new object
// and set "this" for the function to that new object.

function Item(name, price) {
  this.name = name;
  this.price = price;
  this.formatPrice = function () {
    return `$${this.price}`;
  };
}

// constructor functions are just regular functions
// except we write them to be used with new
//  just because JS doesn't (didn't) have classes,
// doesn't mean we never want to create many objects with the same structure

let plate = new Item('plate', 10);
console.log(plate);
console.log(plate.formatPrice());

// objects in JS can have a "prototype"
// this is relevant because of how property access is defined to work.
// the way "." works is, if the property isn't
// found on the object to the left of it, then JS searches
// recursively through each prototype.

const obj3 = { name: 'my object' };
obj3.__proto__ = { value: 22 };
console.log(obj3);

function SaleProduct(name, price, salePercentage) {
  this.__proto__ = new Item(name, price);
  this.salePercentage = salePercentage;
  this.formatPrice = function (isOnSale) {
    return `$${this.price * (isOnSale ? 1 - salePercentage / 100 : 1)}`;
  };
}

let plate2 = new SaleProduct('plate', 10, 40);
console.log(plate2);
console.log(plate2.formatPrice(true));

// function SaleProduct(name, price, salePrice) {
//   this.__proto__ = new Item(name, price);
//     this.salePrice = salePrice;
//   }

//   formatPrice(sale) {
//     if (sale) {
//       return `$${this.salePrice}`;
//     }
//     return super.formatPrice();
//   }
// }

// let saleProduct = new SaleProduct('plate', 6, 4);

// console.log(saleProduct.formatPrice(true));
// console.log(saleProduct.formatPrice(false));

// that's how you have to do it in pre-ES6 JS
// ES6 added classes

class StoreItem {
  #id;
  constructor(name, price) {
    this.name = name;
    this.price = price;
    this.#id = 1;
  }

  // method syntax - another way to make functions
  //    (works on object literals too)
  formatPrice() {
    const id = this.#id;
    return `$${this.price}`;
  }
}

// function validateHasRequiredFunctions(obj) {
//   return typeof obj.func1 === "function";
// }

class SaleItem extends StoreItem {
  constructor(name, price, salePrice) {
    super(name, price);
    this.salePrice = salePrice;
  }

  formatPrice(sale) {
    if (sale) {
      return `$${this.salePrice}`;
    }
    return super.formatPrice();
  }
}
// in JS, classes and class-based inheritance
// are really just syntactic sugar for constructor functions and prototypes

let saleItem = new SaleItem('plate', 6, 4);

// console.log(saleItem.#id);

console.log(saleItem.formatPrice(true));
console.log(saleItem.formatPrice(false));

// // it's still not real OOP, classes
// // are just syntactic sugar for prototypal inheritance.

// // https://caniuse.com/
// // https://kangax.github.io/compat-table/

// // transpilation
// //    (compiling, when the target language is similar
// //     to the source language)
// // new JS features (+ new web APIs like fetch)
// //   are a combination of stuff that can be rewritten in old syntax
// //    (let, const, classes, template literal, arrow functions, etc)
// //   and stuff that can't (like new functions, new kinds of objects)

// // for the stuff that can't, we have "polyfills"

// // with those tools, we can write new JS and have a "build step"
// // that transpiles to old JS for more browser compat.

// // once people started doing that, they added new features of their own
// // new languages that are meant to be compiled/transpiled to JS.
// //  TypeScript
// //  Elm, CoffeeScript


// getters/setters
