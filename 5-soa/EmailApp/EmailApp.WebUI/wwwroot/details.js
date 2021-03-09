'use strict';

const messageId = sessionStorage.getItem('messageId');
const detailsList = document.getElementById('details-list');
const errorMessage = document.getElementById('error-message');

loadMessage(messageId)
  .then(message => {
    detailsList.innerHTML = `
        <dt>From</dt>
        <dd>${message.from}</dd>
        <dt>Subject</dt>
        <dd>${message.subject}</dd>
        <dt>Received</dt>
        <dd>${message.date}</dd>
        <dt>Body</dt>
        <dd>${message.body}</dd>
      `;
    detailsList.hidden = false;
  })
  .catch(error => {
    errorMessage.textContent = error.toString();
    errorMessage.hidden = false;
  });
