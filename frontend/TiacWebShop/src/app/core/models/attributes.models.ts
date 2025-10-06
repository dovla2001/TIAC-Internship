export interface CreateAttributeRequest {
    readonly name: string;
}

export interface AttributeResponse {
    readonly attributeId: number;
    readonly name: string;
}

export interface ValueData {
    readonly value: string;
}

export interface AttributeValues {
    readonly id: number;
    readonly value: string;
}

export interface AttributeWithValues {
    readonly id: number;
    readonly name: string;
    readonly values: AttributeValues[];
}