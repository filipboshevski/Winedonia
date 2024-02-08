import { EnvironmentHelper } from "../helpers/EnvironmentHelper";
import { WineriesFilter } from "../models/Filters/WineriesFilter";
import { WineryDetails } from "../models/WineryDetails";
import { WineryPreviewInfo } from "../models/WineryPreviewInfo";
import { WinerySearchInfo } from "../models/WinerySearchInfo";
import { WinerySearchResult } from "../models/WinerySearchResult";

export class WineryService {

    public async filterWineries(searchTerm: string | null = null, ratings: number[] = [], locations: string[] = [], batchIndex: number | null = null): Promise<WinerySearchResult | undefined> {
        const model = new WineriesFilter(searchTerm, ratings, locations, 20, batchIndex);

        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/wineries/filter/search`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(model),
            });

            if (response.ok) {
                const responseData: WinerySearchResult = await response.json();
                return responseData;
            } else {
                console.error('Error:', response.status, response.statusText);
            }
        }
        catch (error: any) {
            console.error('Error:', error.message);
        }
    }

    public async getWineryDetails(id: string): Promise<WineryDetails | null> {
        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/wineries/${id}/details`);

            if (response.ok) {
                const responseData: WineryDetails = await response.json();
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

    public async getTopWineries(): Promise<WineryPreviewInfo[] | null> {
        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/wineries/top-wineries`);

            if (response.ok) {
                const responseData: WineryPreviewInfo[] = await response.json();
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