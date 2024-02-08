import { WineType } from "../../enums/WineType";

export class WinesFilter {
    public SearchTerm: string;
        
    public Ratings: number[];

    public Types: WineType[];

    public constructor(searchTerm: string = '', ratings: number[] = [], types: WineType[] = []) {
        this.SearchTerm = searchTerm;
        this.Ratings = ratings;
        this.Types = types;
    }
}