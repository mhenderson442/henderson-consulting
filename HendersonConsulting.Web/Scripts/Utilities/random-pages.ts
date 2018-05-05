$(document).ready(function () {

    $('#get-page-list').on('click', getPageList);
    $('#clear-page-list').on('click', clearPageList);
});

interface ReviewPage {
    pageNumber: number;
}

interface GeneratorResult {
    manuscriptLength: number | string | string[];
    pageCount: number | string | string[];
    list: Array<ReviewPage>;
}

function getRandomInt(max) {
    return Math.floor(Math.random() * Math.floor(max));
}

function clearPageList() {
    let randomGeneratorResults: JQuery<HTMLElement> = $("#random-generator-results");
    randomGeneratorResults.empty();
}

function getPageList() {

    let list: Array<ReviewPage> = [];
    let manuscriptLength: number | string | string[] = $('#manuscript-length').val();
    let pageCount: number | string | string[] = $('#page-count').val();

    let generatorResult: GeneratorResult = {
        manuscriptLength: manuscriptLength,
        pageCount: pageCount,
        list: list
    };

    for (var i = 0; i < pageCount; i++) {
        let pageNumber: number = getRandomInt(manuscriptLength);
        let reviewPage: ReviewPage = { pageNumber: pageNumber };
        generatorResult.list.push(reviewPage);
    }

    let source: string = $("#pages-template").html();
    let template: Handlebars.TemplateDelegate<any> = Handlebars.compile(source);
    let html: string = template(generatorResult);

    let randomGeneratorResults: JQuery<HTMLElement> = $("#random-generator-results");

    randomGeneratorResults.html(html);
}