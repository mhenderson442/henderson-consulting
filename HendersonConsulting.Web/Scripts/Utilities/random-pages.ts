$(document).ready(function () {

    $('#get-page-list').on('click', getPageList);
    $('#clear-page-list').on('click', clearPageList);
});

interface IReviewPage {
    pageNumber: number;
}

interface IGeneratorResult {
    manuscriptLength: number | string | string[];
    pageCount: number | string | string[];
    list: Array<IReviewPage>;
}

function getRandomInt(max) {
    return Math.floor(Math.random() * Math.floor(max));
}

function clearPageList() {
    let randomGeneratorResults: JQuery<HTMLElement> = $("#random-generator-results");
    randomGeneratorResults.empty();
}

function getPageList() {

    let list: Array<IReviewPage> = [];
    let manuscriptLength: number | string | string[] = $('#manuscript-length').val();
    let pageCount: number | string | string[] = $('#page-count').val();

    let generatorResult: IGeneratorResult = {
        manuscriptLength: manuscriptLength,
        pageCount: pageCount,
        list: list
    };

    for (var i = 0; i < pageCount; i++) {
        let pageNumber: number = getRandomInt(manuscriptLength);
        let reviewPage: IReviewPage = { pageNumber: pageNumber };
        generatorResult.list.push(reviewPage);
    }

    let source: string = $("#pages-template").html();
    let template: Handlebars.TemplateDelegate<any> = Handlebars.compile(source);
    let html: string = template(generatorResult);

    let randomGeneratorResults: JQuery<HTMLElement> = $("#random-generator-results");

    randomGeneratorResults.html(html);
}