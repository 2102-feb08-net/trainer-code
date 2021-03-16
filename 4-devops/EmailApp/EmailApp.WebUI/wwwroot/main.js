'use strict';

// send a request that will be handled by EmailController.GetInbox based on the
// route configured with attributes (/api/inbox)
function loadInbox(account, onSuccess, onError) {
  // const url = account !== undefined ? `/api/inbox?account=${account}` : '/api/inbox';
  // return fetch(url).then(response => {
  //   if (!response.ok) {
  //     throw new Error(`Network response was not ok (${response.status})`);
  //   }
  //   return response.json();
  // });

  const xhr = new XMLHttpRequest();
  // xhr has events, the most relevant one
  // is readystatechange, which fires every time its readyState property changed.
  // readystate goes from 0 to 4 as we build the request, send it, and receive the response.

  // debugger;
  xhr.onreadystatechange = function () {
    console.log(xhr.readyState);
    if (xhr.readyState === 4) {
      if (xhr.status < 200 || xhr.status >= 300) {
        onError(xhr.status);
      } else {
        // const body = xhr.responseText;
        // onSuccess(JSON.parse(body));
        // because i set responseType to json, response will have the
        // result of JSON.parse
        onSuccess(xhr.response);
      }
    }
  };

  xhr.open('GET', '/api/inbox');
  xhr.responseType = "json"; // change what xhr.response will try to be
  xhr.setRequestHeader("Accept", "application/json");
  xhr.send();
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
