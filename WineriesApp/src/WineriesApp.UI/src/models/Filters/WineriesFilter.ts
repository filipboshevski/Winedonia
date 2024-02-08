import { BaseFilter } from "./BaseFilter";

export class WineriesFilter extends BaseFilter {
    public searchTerm: string | null = null;
    
    public ratings: number[] = [];

    public locations: string[] = [];

    constructor(searchTerm: string | null, ratings: number[], locations: string[], batchSize: number | null = null, batchIndex: number | null = null) {
        super(batchSize, batchIndex);
        this.searchTerm = searchTerm;
        this.ratings = ratings;
        this.locations = locations;
    }
}