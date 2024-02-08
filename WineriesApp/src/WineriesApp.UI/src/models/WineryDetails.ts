import { BaseWineryInfo } from "./BaseWineryInfo";

export class WineryDetails extends BaseWineryInfo {
    public description: string[] | undefined;
        
    public latitude: number | undefined

    public longitude: number | undefined
    
    public address: string | undefined;

    public contact: string | undefined;

    public url: string | undefined;
}