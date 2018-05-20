$(document).ready(function () {

});

interface IResultset {
    offset: number;
    count: number;
    limit: number;
}

interface IMetadata {
    resultset: IResultset;
}

interface IResult {
    uid: string;
    mindate: string;
    maxdate: string;
    name: string;
    datacoverage: number;
    id: string;
}

interface IDatasetDto {
    metadata: IMetadata;
    results: Array<IResult>;
}