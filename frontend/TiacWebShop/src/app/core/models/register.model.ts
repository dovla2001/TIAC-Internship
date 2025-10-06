export interface RegisterRequest {
    readonly firstName: string;
    readonly lastName: string;
    readonly email: string;
    readonly password: string;
}

export interface RegisterResponse {
    readonly id: number;
    readonly firstName: string;
    readonly lastName: string;
    readonly email: string;
}