'use strict';

function updateDisplay(request) {
  var display = document.querySelector('#request-display code');
  if (request === undefined) {
    var input = document.querySelector('#request');
    request = JSON.parse(input.value);
  }
  debugger;

  var result = '';

  result += `${request.method} ${request.url} HTTP/${request.httpVersion}\n`;
  for (var headerName in request.headers) {
    result += `${headerName}: ${request.headers[headerName]}\n`;
  }
  result += '\n';

  display.textContent = result;
}

function showARequest() {
  var request = {
    method: 'GET',
    httpVersion: '1.1',
    url: '/user-data',
    headers: {
      Accept: 'application/json'
    }
  };
  updateDisplay(request);
}
