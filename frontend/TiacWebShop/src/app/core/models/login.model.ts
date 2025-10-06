export interface LoginRequest{
    readonly email: string;
    readonly password: string;
}

export interface LoginResponse {
    readonly token: string;
    readonly refreshToken: string;
    readonly isAdmin: boolean;
}