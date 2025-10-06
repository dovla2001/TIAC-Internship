export interface ProductResponse {
    readonly productId: number;
    readonly name: string,
    readonly description: string,
    readonly basePrice: number,
    readonly imageUrl: string,
}

export interface PagedProductResponse {
    readonly productId: number;
    readonly items: ProductResponse[];
    readonly totalCount: number;
    readonly pageSize: number;
    readonly pageNumber: number;
}

export interface CreateProductResponse {
    readonly name: string,
    readonly description: string,
    readonly basePrice: number,
    readonly imageUrl: string,
}

export interface ProductForAdmin {
    readonly id: number;
    readonly name:string;
}