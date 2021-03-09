'use strict';

const inboxTable = document.getElementById('inbox-table');
const errorMessage = document.getElementById('error-message');
const account = undefined;

loadInbox(account)
  .then(inbox => {
    for (const message of inbox) {
      // from | subject | received
      const row = inboxTable.insertRow(); // returns a <tr>
      row.innerHTML = `<td>${message.from}</td>
                       <td>${message.subject}</td>
                       <td>${message.date}</td>`;
      row.addEventListener('click', () => {
        // store which message was clicked on in session storage (temporary, tab-specific)
        sessionStorage.setItem('messageId', message.id);
        // navigate to details page
        location = 'details.html';
      });
    }

    inboxTable.hidden = false;
  })
  .catch(error => {
    errorMessage.textContent = error.toString();
    errorMessage.hidden = false;
  });
