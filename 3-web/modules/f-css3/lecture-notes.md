# CSS3

CSS3 is the latest version of CSS. CSS3 supports responsive web design, all kinds of transitions, transformations, and animations and provides box-sizing tools that enable the user to adjust the size of any element without changing the dimensions or padding of the element.

## Responsive Web design

Responsive Web design is the approach that allows websites and pages to render (or display) on all devices and screen sizes by automatically adapting to the screen, whether it’s a desktop, laptop, tablet, or smartphone. Responsive web design works through CSS, using various settings to serve different style properties depending on the screen size, orientation, resolution, color capability, and other characteristics of the user’s device. It is a combination of flexible grids, flex boxes, flexible images, and media queries.

## Media Queries

Media queries allow you to customize the presentation of your web pages for a specific range of devices like mobile phones, tablets, desktops, etc. without any change in markups. It composed of a media type and expressions that check for the conditions of particular media features. It is a logical expression that is either true or false. 

**Syntax:**   A media query consists of an optional **media type** and any number of **media feature** expressions. Multiple queries are often combined in various ways by using **logical operators**. Media queries are case-insensitive. A media query is true if the media sort of the media query matches the media sort of the device and every one expression within the media query are true. It uses the `@media` rule to incorporate a block of CSS properties as long as a particular condition is true. Queries involving unknown media types are always false.

```
@media not|only mediatype and (mediafeature and|or|not mediafeature) {
  CSS-Code;
}
```
Media Types -  It describes the category of a device.
  * `all` - used for all media type devices 
  * `print`	- used for printers
  * `screen` - 	used primarily for screens like computer screens, tablets, smart-phones, etc.
  * `speech` - 	used for screenreaders that "reads" the page aloud

Media features - It describe specific characteristics of the user agent, output device, or environment. Some of the media features are `grid`, `height`, `width`, `hover`, `max-aspect-ratio`, `max-color`,`max-color-index`, `max-height`,etc.

Logical Operators  - It used to compose a media query . Logical Operators used in media queries are `not`, `and`, and `only`.
  
*Example:* It changes the background color of the `<body>` element to "red" and the font style to "Arial" when the browser window is 600px wide or less.
```
@media only screen and (max-width: 600px) {
  body {
    background-color: red;
    font-family: Arial;
  }
}
```
## Flex boxes

The Flexible Box Module, also known as flexbox, is a one-dimensional layout method for arranging elements in rows or columns. We can design a flexible responsive layout structure without using float or positioning easily using CSS Flex boxes.  

### Flex Container and Flex items

In any flexbox layout, the first step is to create a flex container. The flex container is an area of document laid out using flexbox.  We can define the flex container by setting the `display` property to `flex` or `inline-flex`. The parent element that has `display: flex`  property set on it is called the flex container. The items being represented as flexible boxes inside the flex container are called flex items. The direct children of the flex container called flex items. 

Example for creating a Flex Container:
```
.flex-container {
  display: flex;
}
```

### Properties of the flex container

**Flex direction property** - It used to change the direction of the flex items display. The `flex-direction` property can have the following values:
* `row` (default):  arranges the flex items from left to right (horizontally)
* `column`: arranges the flex items from top to bottom (vertically)
* `row-reverse`: arranges the flex items from  right to left.
* `column-reverse`: arranges the flex items from bottom to top.

**Flex Wrap property**- It is used to defines the flex items that should wrap or not. The `flex-wrap` property can have the following values:
* `nowrap` (default): makes flex items wrap on a single line.
* `wrap`:  makes flex items wrap to multiple lines according to flex item width.
* `wrap-reverse`:  similar to wrap property but it follows the reverse flow of the flex items.

**Flex-flow Property** - It is used as a shorthand property for setting both the flex-direction and flex-wrap properties. An example of `flex-flow` property value is `row wrap` which wraps and arranges the flex items horizontally.

**justify-content Property** - It is used to align the flex items within the container. The `justify-content property` can have the following values:
* `flex-start` -  used to align the flex items at the beginning of the container.
* `flex-end`  - used to align the flex items at the end of the container.
* `center`  -  used to align the flex items at the center of the container.
* `space-around` -  used to align the flex items in such a way each has an equal amount of space around them.

**align-items Property** - It is used to align the flex items vertically. The `align-items` property can have the following values:
* `center` - flex items are aligned at the center  of the container.
* `flex-start` - aligns the flex items at the top of the container.
* `flex-end `- aligns the flex items at the bottom of the container.
* `stretch` (default) - stretches the flex items to fill the container.
* `baseline`-  the flex items are aligned with baseline.

### Properties of the flex items

 * `order`- used to define the order of the flex items.
*  `flex-grow` - used to define the amount that a flex item can grow relative to the remaining items.
*  `flex-shrink` - used to define the amount that a flex item can shrink relative to the remaining items.
*  `flex-basis` - used to define the initial length of an item.
* `flex property` - it is a shorthand property for the flex-grow, flex-shrink, and flex-basis properties. 
* `align-self`- used to define the alignment for a specific flex item which can override the default alignment.

## Import fonts via @font-face

The @font-face CSS at-rule allows you to define and use your custom fonts, thus allowing you to extend the limited set of standard system fonts that are installed by default on a computer, and that browsers can access and use. The syntax for @font-face rule is `@font-face {   property:value }`.The property inside the @fontface describes the font face’s font-family, font-variant, font-weight, font-stretch, font-style, source (src, which indicates the source of the font face that you’re fetching into the page), and Unicode range.

*Example:* The font-family property specifies the font family name that you will be able to use throughout the document. The src property provides the source of the font URL to the browser to fetch the font. The font-style and font-weight property used to change the style and weight of the font.

```
@font-face {
  font-family: 'Graublau Web';
  font-style: normal;
  font-weight: 400;	
  src:  url('GraublauWeb.eot?') format('eot'), 
        url('GraublauWeb.woff') format('woff'), 
        url('GraublauWeb.ttf') format('truetype');
}
```

           
## CSS animations using @keyframes

The CSS animation  is used for creating animations in our site.The main component of CSS animations is @keyframes, the CSS rule where animation is created. Inside @keyframes, you can define these stages, each having a different style declaration.
            
In the below example, we set the animation stages using @keyframes properties. Here we have 2 stages 0%-100%, from (equal to 0%) and to (equal to 100%). Also, we can mention the CSS styles applied for each stage.
```
@keyframes Fadeout {
  0% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
```
The animation property is used to call `@keyframes` inside a CSS selector. Animation can have multiple properties such as `animation-name`, `animation-duration`,`animation-timing-function` ( linear | ease | ease-in | ease-out | ease-in-out | cubic-bezier ), `animation-delay`, and `animation-iteration-count`, `animation-direction`, and `animation-fill-mode`( none | forwards | backwards | both ).


## CSS tranisition

CSS transitions let you define the changes for HTML elements, the specific time intervals, the speed of the acceleration curve and much more. The transition-property specifies the CSS properties to which you want the transition effect. Only these CSS properties are animated.

Syntax: `transition: <property> <duration> <timing-function> <delay>;`

The below code defines a transition effect of the width property for a duration of five seconds. When you hover your cursor over the blue box, the blue box increases its width gradually for a time duration of five seconds. 
```
<!DOCTYPE html>
<html>
<head>
<style>
div{
width: 100px;
height: 100px;
background: blue;
transition: width 5s;
}
div:hover {
width: 600px;
}
</style>
</head>
<body>
<div></div>
<p>Move the cursor over the div element above, to see the transition effect.</p>
</body>
</html>
```







