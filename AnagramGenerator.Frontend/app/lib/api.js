class API {
  constructor() {
    this.API_URL = "http://localhost:55873/api";
  }
  getWords(page, pageSize) {
    return fetchPOST(`${this.API_URL}/words`, {
      page,
      pageSize
    }).then(data => data.words);
  }
  getWord(id) {
    return fetchJSON(`${this.API_URL}/words/${id}`).then(data => data.dog);
  }
}
