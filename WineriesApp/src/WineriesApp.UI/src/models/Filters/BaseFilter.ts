export class BaseFilter {
    public batchSize: number | null = null;

    public batchIndex: number | null = null;

    constructor(batchSize: number | null = null, batchIndex: number | null = null) {
        this.batchSize = batchSize;
        this.batchIndex = batchIndex;
    }
}