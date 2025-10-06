export interface VariantAttribute {
    readonly attributeName: string;
    readonly attributeValue: string;
}

export interface Variant {
    readonly variantId: number;
    readonly price: number;
    attributes: VariantAttribute[];
}

export interface ProductDetailsResponse {
    readonly id: number;
    readonly name: string;
    readonly description: string;
    readonly imageUrl: string;
    readonly variants: Variant[];    
}
