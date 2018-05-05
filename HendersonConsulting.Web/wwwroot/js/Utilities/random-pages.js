$(document).ready(function () {
    $('#get-page-list').on('click', getPageList);
    $('#clear-page-list').on('click', clearPageList);
});
function getRandomInt(max) {
    return Math.floor(Math.random() * Math.floor(max));
}
function clearPageList() {
    var randomGeneratorResults = $("#random-generator-results");
    randomGeneratorResults.empty();
}
function getPageList() {
    var list = [];
    var manuscriptLength = $('#manuscript-length').val();
    var pageCount = $('#page-count').val();
    var generatorResult = {
        manuscriptLength: manuscriptLength,
        pageCount: pageCount,
        list: list
    };
    for (var i = 0; i < pageCount; i++) {
        var pageNumber = getRandomInt(manuscriptLength);
        var reviewPage = { pageNumber: pageNumber };
        generatorResult.list.push(reviewPage);
    }
    var source = $("#pages-template").html();
    var template = Handlebars.compile(source);
    var html = template(generatorResult);
    var randomGeneratorResults = $("#random-generator-results");
    randomGeneratorResults.html(html);
}
//# sourceMappingURL=random-pages.js.map