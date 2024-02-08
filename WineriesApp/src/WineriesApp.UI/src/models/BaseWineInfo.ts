import { WineType } from "../enums/WineType";

export class BaseWineInfo {
    public id: string = '';

    public name: string = '';

    public type: WineType = WineType.Red;

    public rating: number = 5.0;

    public imageUrl: string = '';
}