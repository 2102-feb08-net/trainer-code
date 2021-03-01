'use strict';

const inboxTable = document.getElementById('inbox-table');
const errorMessage = document.getElementById('error-message');

function loadInbox() {
  return [
    {
      from: 'fred@fred.com',
      date: 'Mon, 01 Mar 2021 12:58:58 -0700',
      subject: 'qc'
    },
    {
      from: 'kevin@kevin.com',
      date: 'Mon, 01 Mar 2021 13:00:10 -0700',
      subject: 'RE: qc'
    }
  ];
}

const inbox = loadInbox();

for (const message of inbox) {
    // from | subject | received
    const row = inboxTable.insertRow(); // returns a <tr>
    row.innerHTML = `<td>${message.from}</td>
                     <td>${message.subject}</td>
                     <td>${message.date}</td>`;
}

inboxTable.hidden = false;
