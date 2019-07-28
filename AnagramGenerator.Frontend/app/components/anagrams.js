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
    <ul class="list-group md-3 mb-3">
      ${words}
    </ul>`;
  }

  function controller() {
    this.loading = true;
    api.getWords(1, 100).then(words => {
      this.words = words;
      this.loading = false;
    });
  }
})();
