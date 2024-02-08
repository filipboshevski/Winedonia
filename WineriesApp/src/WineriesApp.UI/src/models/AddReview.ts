import { ReviewEntityType } from "../enums/ReviewEntityType";

export class AddReview {
    public Rating: number;
    public Comment: string;
    public EntityType: ReviewEntityType;
    public EntityId: string;

    public constructor(rating: number, comment: string, entityType: ReviewEntityType, entityId: string) {
        this.Rating = rating;
        this.Comment = comment;
        this.EntityType = entityType;
        this.EntityId = entityId;
    }
}