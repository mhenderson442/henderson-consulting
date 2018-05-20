$(document).ready(function () { });

interface IBook {
    id: number;
    isbn: string;
    isbn13: string;
    ratings_count: number;
    reviews_count: number;
    text_reviews_count: number;
    work_ratings_count: number;
    work_reviews_count: number;
    work_text_reviews_count: number;
    average_rating: string;
}

interface RootObject {
    books: Array<IBook>;
}