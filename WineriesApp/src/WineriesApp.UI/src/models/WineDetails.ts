import { BaseWineInfo } from "./BaseWineInfo";
import { WineryPreviewInfo } from "./WineryPreviewInfo";

export class WineDetails extends BaseWineInfo {
    public description: string[] = [];

    public wineries: WineryPreviewInfo[] = [];
}