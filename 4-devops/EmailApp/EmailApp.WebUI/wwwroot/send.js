'use strict';

const messageForm = document.getElementById('message-form');
const errorMessage = document.getElementById('error-message');
const successMessage = document.getElementById('success-message');

messageForm.addEventListener('submit', event => {
  // don't use ordinary form submission, because the server would need
  // to both process the data and respond with HTML, and for now
  // we can only do one at a time (process data in a controller, or
  // send a static file matching the URL)
  event.preventDefault();

  successMessage.hidden = true;
  errorMessage.hidden = true;

  // you can access the form controls by their "name" attribute in the form
  // using the form's "elements" property.
  // here we build an object that we want to send to the server.
  // the sendMessage function will serialize it.

  const message = {
    from: 'me@me.com',
    date: new Date(),
    subject: messageForm.elements['subject'].value,
    body: ''
  };

  sendMessage(message)
    .then(() => {
      successMessage.textContent = 'Message sent successfully';
      successMessage.hidden = false;
    })
    .catch(error => {
      errorMessage.textContent = error.toString();
      errorMessage.hidden = false;
    });
});
