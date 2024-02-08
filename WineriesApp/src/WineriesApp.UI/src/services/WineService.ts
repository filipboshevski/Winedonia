import { WineType } from "../enums/WineType";
import { EnvironmentHelper } from "../helpers/EnvironmentHelper";
import { WineDetails } from "../models/WineDetails";
import { WinesFilter } from "../models/Filters/WinesFilter";
import { WinesSearchInfo } from "../models/WinesSearchInfo";

export class WineService {
    public async filterWines(searchTerm: string | null = null, ratings: number[] = [], types: WineType[] = []): Promise<WinesSearchInfo[] | undefined> {
        const model = new WinesFilter(searchTerm ?? '', ratings, types);

        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/wines/filter/search`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(model),
            });

            if (response.ok) {
                const responseData: Array<WinesSearchInfo> = await response.json();
                return responseData;
            } else {
                console.error('Error:', response.status, response.statusText);
            }
        }
        catch (error: any) {
            console.error('Error:', error.message);
        }
    }

    public async getWineryDetails(id: string): Promise<WineDetails | null> {
        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/wines/${id}/details`);

            if (response.ok) {
                const responseData: WineDetails = await response.json();
                return responseData;
            } else {
                console.error('Error:', response.status, response.statusText);
                return null;
            }
        }
        catch (error: any) {
            console.error('Error:', error.message);
            return null;
        }
    }
}