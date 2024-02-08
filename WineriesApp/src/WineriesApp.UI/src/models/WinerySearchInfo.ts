import { BaseWineryInfo } from "./BaseWineryInfo";

export class WinerySearchInfo extends BaseWineryInfo {
    public latitude: number | undefined;
    
    public longitude: number | undefined;

    public address: string | undefined;

    public contact: string | undefined;

    public url: string | undefined;
}