import { EnvironmentHelper } from "../helpers/EnvironmentHelper";
import { Municipality } from "../models/Municipality";

export class MunicipalityService {

    public async getMunicipalities(): Promise<Municipality[] | undefined> {
        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/municipalities`);

            if (response.ok) {
                const responseData: Array<Municipality> = await response.json();
                return responseData;
            } else {
                console.error('Error:', response.status, response.statusText);
            }
        }
        catch (error: any) {
            console.error('Error:', error.message);
        }
    }
}