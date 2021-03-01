'use strict';

const inboxTable = document.getElementById('inbox-table');
const errorMessage = document.getElementById('error-message');

function loadInbox() {
  return fetch('/api/inbox')
    .then(response => {
      return response.json();
    });
}

loadInbox()
  .then(inbox => {
    for (const message of inbox) {
      // from | subject | received
      const row = inboxTable.insertRow(); // returns a <tr>
      row.innerHTML = `<td>${message.from}</td>
                       <td>${message.subject}</td>
                       <td>${message.date}</td>`;
    }

    inboxTable.hidden = false;
  });
