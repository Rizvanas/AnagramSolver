(function() {
  app.addComponent({
    name: "words",
    model: {
      loading: true,
      title: "Words",
      words: []
    },
    view,
    controller
  });

  function view() {
    if (this.loading)
      return `
      <div class="d-flex justify-content-center">
        <div class="spinner-grow" role="status">
          <span class="sr-only">Loading...</span>
        </div>
    </div>`;

    const words = this.words.reduce(
      (html, word) => html + shared.wordTemplate(word),
      ""
    );

    return `
    <div class="row">
    <div class="col-md-4 mb-3"></div>
    <div class="col-md-4 mb-3">
      <form class="py-5 text-left">
        <div class="form-group">
          <label for="searchInput"><h3>Žodžių paieška</h3></label>
          <input
            type="text"
            class="form-control"
            id="searchInput"
            aria-describedby="searchPhraseHelp"
            placeholder="Įveskite žodį"
            autofocus
          />
          <small id="searchPhraseHelp" class="form-text text-muted"
            >Šis laukas yra privalomas</small
          >
        </div>
        <button
          type="submit"
          class="btn btn-lg btn-primary btn-block btn-dark"
        >
          Ieškoti
        </button>
      </form>
      <h4 class="d-flex justify-content-between align-items-center mb-3">
        <p class="text-muted">Žodžiai</p>
      </h4>
      <ul class="list-group md-3 mb-3">
        ${words}
      </ul>
    </div>
    <div class="col-md-4 mb-3"></div>
  </div>
    `;
  }

  function controller() {
    this.loading = true;
    api.getWords(1, 100).then(words => {
      this.words = words;
      this.loading = false;
    });
  }
})();
