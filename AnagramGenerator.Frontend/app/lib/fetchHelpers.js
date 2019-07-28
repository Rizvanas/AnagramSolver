const cache = {};

function fetchJSON(url) {
  if (cache[url]) {
    return Promise.resolve(cache[url]);
  }

  return fetch(url)
    .then(res => res.json())
    .then(json => {
      cache[url] = json;
      return json;
    });
}

function fetchPOST(url, body) {
  if (cache[url]) {
    return Promise.resolve(cache[url]);
  }

  return fetch(url, {
    method: "post",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(body)
  })
    .then(response => response.json())
    .then(json => {
      cache[url] = json;
      return json;
    });
}
