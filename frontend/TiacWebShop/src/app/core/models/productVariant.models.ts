export interface CreateVariantRequest {
    readonly productId: number;
    readonly price: number;
    readonly attributeValueIds: number[];
}