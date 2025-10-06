export interface AddToCartRequest {
    readonly productVariantId: number;
    readonly quantity: number;
}

export interface CartItem {
    readonly cartItemId: number;
    readonly productVariantId: number;
    readonly productName: string;
    readonly variantDescription: string;
    readonly quantity: number;
    readonly unitPrice: number;
    readonly totalPrice: number;
    readonly imageUrl: string | null;
}

export interface CartResponse {
    readonly cartId: number;
    readonly items: CartItem[];
    readonly grandTotal: number;
}