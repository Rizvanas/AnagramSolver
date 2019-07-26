var AnagramView = function AnagramView(element) {
  this.element = element;

  this.onClickGetAnagram = null;
};

AnagramView.prototype.render = function render(viewModel) {
  this.element.innerHTML = "<h3>" + viewModel.name + "</h3>";

  this.previousIndex = viewModel.previousIndex;
  this.nextIndex = viewModel.nextIndex;

  var previousAnagram = this.element.querySelector("#previousPenguin");
  previousAnagram.addEventListener("click", this.onClickGetAnagram);

  var nextAnagram = this.element.querySelector("#nextAnagram");
  nextAnagram.addEventListener("click", this.onClickGetAnagram);
  nextAnagram.focus();
};
