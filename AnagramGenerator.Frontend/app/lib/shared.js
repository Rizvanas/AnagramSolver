const shared = {
  wordTemplate: word => `
  <li class="list-group-item d-flex justify-content-center lh-condensed">
    <a href="#/words/${word.id}/anagrams">
      <p>${word.text}</p>
    </a>
  </li>
  `
};
