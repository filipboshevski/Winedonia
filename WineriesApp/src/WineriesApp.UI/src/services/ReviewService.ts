import { ReviewEntityType } from "../enums/ReviewEntityType";
import { EnvironmentHelper } from "../helpers/EnvironmentHelper";
import { AddReview } from "../models/AddReview";
import { ReviewInfo } from "../models/ReviewInfo";

export class ReviewService {

    public async addReview(rating: number, comment: string, entityType: ReviewEntityType, entityId: string): Promise<void> {
        const model = new AddReview(rating, comment, entityType, entityId);

        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/reviews/add`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(model),
            });

            if (response.ok) {
                return;
            } else {
                console.error('Error:', response.status, response.statusText);
            }
        }
        catch (error: any) {
            console.error('Error:', error.message);
        }
    }

    public async getReviews(entityType: ReviewEntityType, entityId: string): Promise<Array<ReviewInfo> | null> {
        try {
            const response = await fetch(`${EnvironmentHelper.isDev() ? 'https://localhost:7008' : 'https://wineriesapp.azurewebsites.net'}/api/reviews/${entityType}/${entityId}`);

            if (response.ok) {
                const responseData: Array<ReviewInfo> = await response.json();
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