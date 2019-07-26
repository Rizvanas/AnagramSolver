var AnagramModel = function AnagramModel(XMLHttpRequest) {
  this.XMLHttpRequest;
};

AnagramModel.prototype.getAnagram = function getAnagram(index, fn) {
  var oReq = new this.XMLHttpRequest();

  oReq.onload = function onLoad(e) {
    var ajaxResponse = JSON.parse(e.currentTarget.responseText);

    var anagram = ajaxResponse[index];

    anagram.index = index;
    anagram.count = ajaxResponse.length;

    fn(anagram);
  };

  oReq.open("POST", "https://localhost:", true);
  oReq.send();
};
