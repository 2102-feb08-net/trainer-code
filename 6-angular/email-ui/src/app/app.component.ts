import { Component } from '@angular/core';

@Component({
  // a directive (or component) selector defines
  // which elements on the page should be controlled by this directive (/component)
  selector: 'app-root',
  // every component needs a template, either in a separate file like this,
  templateUrl: './app.component.html',
  // template: `<div></div>`, // or inline like this

  // when components are compiled, their CSS is processed in such a way that it
  // is scoped to the component and cannot "leak out" to modify other elements on the page.
  styleUrls: ['./app.component.css'], // in separate files, or...

  // styles: [], // inline
  // animations: [] // angular animations is a thing
})
export class AppComponent {
  title = 'email-ui';
}
