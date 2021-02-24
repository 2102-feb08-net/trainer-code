# CSS Selectors

CSS selectors are used for selecting the content/text you want to style in our site. Selectors are the part of CSS ruleset. CSS selectors select the HTML elements according to its id, class, type, attribute, etc. There are several different types of selectors in CSS, some of them are listed below:

* Element Selector
* Id Selector
* Class Selector
* Universal Selector
* Attribute selectors
* Grouping Selector
* Child and descendent selectors
* General and adjacent sibling selectors
* Pseudo-element and pseudo-class selectors

## Element Selector

The element selector selects HTML elements by their name / tag name *like a, h1, div, p etc*.

*Example:* Here, we use `<p>` as an element selector. The text inside the `<p>` will be center-aligned also red color.

```
<!DOCTYPE html>  
<html>  
<head>  
<style>  
p {  
    text-align: center;  
    color: blue;  
}   
</style>  
</head>  
<body>  
<p>This style will be applied on every paragraph.</p>  
<p> Here also</p>
</body>
</html>
```

## ID Selector

In the CSS, the ID selector is a name preceded by a hash character (“#”).  It uses the id attribute of an HTML element to match the specific HTML element. The **id** of an element should be unique within a page, so the id selector is used to select one unique element. 

*Example:* Here, we use `#para1` as an ID selector. Inside the body, we have two `<p>` elements. The CSS style rule applied to the element which has an attribute called `id`, whose value is `para1`. Therefore, `Hello World!` will be center-aligned also blue color.

```
<!DOCTYPE html>
<html>
<head>
<style>
#para1 {
  text-align: center;
  color: blue;
}
</style>
</head>
<body>

<p id="para1">Hello World!</p>
<p>This paragraph is not affected by the style.</p>

</body>
</html>
```

> *NOTE:* The id name should start with the alphabet, not with numbers. Also, the HTML element without the 'id' attribute doesn't get affected.

## Class Selector

In the CSS, the class selector is a name preceded by a period (“.”).  It uses the class attribute of an HTML element to match the specific HTML element. We can have a Class selector specific to an HTML element *like we have `p.class` in the below example*.

In the below example, we have two class selectors inside the `<style>` element. The class selector `.intro` is applied to the element which has an attribute called `class`, whose value is `intro` and the `p.intro`  class selector is applied to the `<p>` element which has an attribute called `class`, whose value is `intro`. Also, the `<p>` element without the `class` attribute doesn't get affected.

```
<!DOCTYPE html>
<html>
<head>
<style>
.intro {
  text-align: center;
  color: red;
}

p.intro {
  text-align: center;
  color: blue;
}
</style>
</head>
<body>

<h1 class="intro">Red and center-aligned heading</h1>
<p class="intro">blue and center-aligned paragraph.</p> 
<p> this will not be affected </p>
</body>
</html>
```

## Universal Selector

The universal selector denoted by an asterisk (*), matches all the elements on the page. If any other specific selector exists on the element, then the universal selector will be omitted.

In the below example, all the elements will have the style defined under `*` selector, except the element which has an attribute called `id`, whose value is `test`.
```
<!DOCTYPE html>
<html>
<head>
<style>
* {
  font-family: Arial;
  color: blue;
}
#test{
color: green;
}
</style>
</head>
<body>

<h1>Hello world!</h1>
<p>Every element on the page will be affected by the style.</p>
<p id="test">Not me!</p>
<p>And me!</p>

</body>
</html>
```
## Attribute Selector

An attribute selector selects the HTML elements that has a specific attribute or attribute with a specified value. You can create an attribute selector by having the attribute in a pair of square brackets `[attribute]`. 
The most commonly used attribute selectors are listed below:

[attribute] Selector - applies the style rule for all the element which has a specified attribute. 

[attribute="value"] Selector -  uses the `=` operator to select the element whose attribute value is exactly equal to the given value.

[attribute~="value"] Selector - uses the `~=` operator to select elements that have the specified attribute with a value containing a given word, delimited by spaces.

[attribute|="value"] Selector - uses the `|=` operator to select elements that have the specified attribute with a value either equal to a given string or starting with that string followed by a hyphen (-).

[attribute^="value"] Selector -  uses the `^=` operator to select elements that have the specified attribute with a value beginning exactly with a given string. 

[attribute$="value"] Selector uses the `$=` operator to select elements that have the specified attribute with a value ending exactly with a given string. The comparison is case sensitive.

[attribute*="value"] Selector - uses the `*=` operator to select elements that have the specified attribute with a value containing a given substring.

*Example:*
```
<!DOCTYPE html>
<html>
<head>
   <title>Example of attribute selector</title>
  <style>
     [title] {
        color:green;
    }
    p[lang|=en] {
          	background: blue;
    }
     [class*="warning"] {
         color : red;
    }
}
  </style>
</head>
<body>
  <p title="heading">It is an example for CSS [attribute] Selector</p>
  <p lang="en">It is an example for  CSS [attribute|="value"] Selector</p>
  <p class="alert_warning">It is an example for CSS [attribute*="value"] Selector</p>
</body>
</html> 
```

## Grouping Selector

The CSS grouping selector is used to apply a common style for the number of elements on the page. You can group the selector using comma (,) separator.  It allows you to specify the same properties and rules for more than one element at the same time. This reduces the code and extra effort to declare common styles for each element. 

*Example:*  Here, h1 and h2 elements have a single rule, instead of having to specify it for each of them. Therefore, the text inside the `<h1>` and `<h2>` will be center aligned and red color.

```
<!DOCTYPE html>
<html>
<head>
<style>
h1, h2 {
  text-align: center;
  color: red;
}
</style>
</head>
<body>

<h1>Hello World!</h1>
<h2>Smaller heading!</h2>
<p>This is a paragraph.</p>

</body>
</html>
```
## Child  selectors

Child Selector selects all the elements that are the children of a specified element. The Syntax of Child Selector is ` element > element { property: value;} ` which selects those elements which are the children of specific parent. The left side of > is a parent element and on the right is the children element.

In the below example, Child selectors selects those `<p>` elements which are the direct children of `<div>`. 
```
<!DOCTYPE html>
<html>
<head>
<style>
div > p {
  background-color: red;
  font-family: Arial;
}
</style>
</head>
<body>

<div>
<h3>Not me!</h3>
  <p>I'll match, </p>
  <section><p> Not me! </p></section>
  <p>I'll match, </p>
</div>
<p>I'm not in a div.</p>

</body>
</html>
```

## Descendent selectors

The descendant selector selects all the elements which are a child of the element. It allows you to limit the targeted elements to the ones who are descendants of another element. The syntax is ` element element { property: value; }` you simply write the parent(s), separate with space, and then the actual element you want to target.

In the below example, we have descendant selector that applies the style for all `<p>` elements that are descendants of a `<div>` element. 
```
<!DOCTYPE html>
<html>
<head>
<style>
div p {
  background-color: red;
  font-family: Arial;
}
</style>
</head>
<body>

<div>
  <p>I'll match, </p>
  <section><p> I'll match. </p></section>
</div>
<p>I'm not in a div.</p>

</body>
</html>
```

## General Sibling Selector

The General Sibling selector selects all the elements which are siblings of a specifed element. The syntax is ` element ~ element { property: value; }`, which selects all the sibiling elements that are in same hierarchy level

*Example:* Here, general sibling selector selects all the `<p>` elements that are sibling to the `<div>` element will have red background color.
```
<!DOCTYPE html>
<html>
<head>
<style>
div ~ p {
  background-color: red;
  
}
</style>
</head>
<body>
<p>Not me</p>

<div>
  <p>1st Child of div element</p>
  <p>2nd Child of div element.</p>
</div>

<p>1st sibling of div element </p>
<p>2nd sibling of div element</p>

</body>
</html>
```

## Adjancent Sibiling Selector

The Adjancent Sibling selector selects the element that are the adjacent sibling of a specified element. The syntax is ` element + element { property: value; }`, which selects the second one, if it immediately follows the first one in order of appearance in an HTML page.

*Example:* Here, adjancent sibling selector selects the `<p>` element which immediately follows the `<div>` element will have red background color.

```
<!DOCTYPE html>
<html>
<head>
<style>
div + p {
  background-color: red;
  
}
</style>
</head>
<body>
<p>Not me</p>

<div>
  <p>1st Child of div element</p>
  <p>2nd Child of div element.</p>
</div>

<p>1st sibling of div element </p>
<p>2nd sibling of div element</p>

</body>
</html>
```

## Pseudo Class Selector

Pseudo Class Selector is used to specify the state of an element. It let us to apply a style to an element which are related with external factors like the history of the navigator ( like `:visited`), the status of its content (like `:checked` on certain form elements), or the position of the mouse (like `:hover`, which lets you know if the mouse is over an element or not).

The syntax for Pseudo Class Selector is `selector:pseudo-class { property: value; }` . These pseudo- classes are used with the selector to style the element on a specific state. Some of the most commonly used pseudo-classes selectors are listed below:

`:link` - Used to select only `<a>` element with href attributes and applies the style for unvisted link.
`:visited` - Used to select only `<a>` element with href attributes and applies the style for visited link. 
`:active` - selects an element which is activated by user clicks
`:hover` - selects the style when the element is in its hover state (mouse cursor rolls over the element). 
`:focus` - selects the form input element that has received focus. It is generally triggered when the user clicks or taps on an element or selects it with the keyboard's "tab" key.
`:lang` - Used to specify a language to use in a element.
`:first-child` - selects the first element among a group of sibling elements.

*Example:* Whenever mouse rolls over the `<div>` element, it changes the background color to blue.
```
<style>
div:hover {
  background-color: blue;
}
</style>
</head>
<body>
<div>Mouse Over Me</div>
</body>
```

## Pseudo Element Selector

Pseudo-elements allows to style the specified parts of an element that is not available under DOM tree.
It let us to apply to style the first letter or first line of an element's content, change the font of the first line of a paragraph, etc.

The syntax for Pseudo Element Selector is `selector::pseudo-element { property:value; }`. Some of the most commonly used pseudo-element selectors are listed below:

`::first-letter` - Selects the first letter of the text contents inside an element.
`::first-line` - Selects the first line of the text contents inside an element.
`::before` - Used to insert generated content immediately before an element.
`::after` - Used to insert generated content either before or after an element on the page generate content immediately after an element.

*Example:* The first letter of the text contents inside `<p>` element will be capitalized and blue color. Also the content will be inserted before and after the `<div>` element.

```
<!DOCTYPE html>
<html>
<head>
<style>
div::before{
content: "Content inserted before.";
color: red;
} 
p::first-letter {
  color: blue;
  text-transform: uppercase;
}
div::after{
content: "Content inserted after.";
color: blue;
}

</style>
</head>
<body>
<div>
<p>this is a paragraph.</p>
</div>
</body>
</html>
```

## Specificity in CSS

When we have more than one CSS style rule for an element, the browser selects one style rule for that element based upon a specificity as a score/rank/priority. Specificity only applies when the same element is targeted by multiple CSS declarations.  Specificity is the set of the rules applied to CSS selectors to determine which style is applied to an element. More specific selector will have higher Precedence. The specificity level of a selector has 4 categories listed below:

1. Inline CSS - Example: `<h1 style="color: #ffffff;">`
2. ID Selector 
3. Class Attribute and Pseudo-Classes Selectors.
4. Element and Pseudo-Elements Selector.

Inline CSS have higher priorty and Element and Pseudo-Elements Selector have lowest priorty.  When we have 2 CSS Style rule which has same priority, then the lower priorty will be selected by the browser.

