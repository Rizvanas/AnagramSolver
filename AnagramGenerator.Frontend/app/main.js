const api = new API();
const app = new App();

app.mount("#app");

const router = new Router(app, "/");
router.addRoute("^/$", "home");
router.addRoute("^/words$", "words");
router.addRoute("^/words/(.*)/anagrams$", "anagrams");
