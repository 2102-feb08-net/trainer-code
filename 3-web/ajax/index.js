'use strict';

document.addEventListener('DOMContentLoaded', () => {
  const resultsElement = document.createElement('div');
  const errorElement = document.createElement('div');
  errorElement.style.color = 'red';

  debugger;

  const url = 'https://newton.now.sh/api/v2/simplify/2^2%2b2(2)';

  function contactApi() {
    if (resultsElement.parentNode) {
      document.body.removeChild(resultsElement);
    }
    if (errorElement.parentNode) {
      document.body.removeChild(errorElement);
    }

    fetch(url) // returns a promise of a Response (but the whole body is not downloaded)
      .then(response => {
        if (response.ok) {
          return response.json(); // returns a promise of the body downloaded &
          // deserialized as json
        }
        throw response.status;
      })
      .then(obj => {
        resultsElement.textContent = obj.result;
        document.body.appendChild(resultsElement);
      })
      .catch(error => {
        errorElement.textContent = `error ${error}`;
        document.body.appendChild(errorElement);
      });
  }

  const form = document.getElementById('calculate-form');
  form.addEventListener('submit', event => {
    event.preventDefault();

    contactApi();
  });
});
