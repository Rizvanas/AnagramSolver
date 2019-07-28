(function() {
  app.addComponent({
    name: "word",
    model: {
      loading: true,
      word: {}
    },
    view,
    controller
  });

  function view() {
    if (this.loading) return "Loading...";

    const words = this.words.reduce(
      (html, word) => html + shared.wordTemplate(word),
      ""
    );
    return `
    <ul class="list-group md-3 mb-3">
      <li class="list-group-item d-flex justify-content-between lh-condensed">${word}</li>
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
