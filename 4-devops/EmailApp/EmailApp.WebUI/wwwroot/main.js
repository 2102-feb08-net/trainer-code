'use strict';

// send a request that will be handled by EmailController.GetInbox based on the
// route configured with attributes (/api/inbox)
function loadInbox() {
  return fetch('/api/inbox').then(response => {
    if (!response.ok) {
      throw new Error(`Network response was not ok (${response.status})`);
    }
    return response.json();
  });
}

// send a request that will be handled by EmailController.GetMessage based on the
// route configured with attributes (/api/message/[id])
function loadMessage(messageId) {
  // you can pass data to the server in the URL using either the URL path itself
  // ("route parameters", as here), or the query string after the path
  // (like "?id=1")
  return fetch(`/api/message/${messageId}`).then(response => {
    if (!response.ok) {
      throw new Error(`Network response was not ok (${response.status})`);
    }
    return response.json();
  });
}

// send a request that contains data besides the URL (JSON in the body)
function sendMessage(message) {
  return fetch('/api/send-message', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(message)
  }).then(response => {
    if (!response.ok) {
      throw new Error(`Network response was not ok (${response.status})`);
    }
  });
}
