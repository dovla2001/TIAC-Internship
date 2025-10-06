import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, map, Observable, tap } from "rxjs";
import { environment } from "../../../environments/environment";
import { RegisterRequest, RegisterResponse } from "../models/register.model";
import { LoginRequest, LoginResponse } from "../models/login.model";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})

export class AuthService {

    private AUTH_RESOURCE = 'auth';

    private accessTokenSubject$ = new BehaviorSubject<string | null>(null);
    private cuccrentUserSubject$ = new BehaviorSubject<LoginResponse | null>(null);

    public accessToken$ = this.accessTokenSubject$.asObservable();
    public isLoggedIn$: Observable<boolean>;

    public isEmployee$: Observable<boolean>;

    constructor(
        private http: HttpClient,
        private router: Router
    ) {
        const accessToken = localStorage.getItem("accessToken")
        this.accessTokenSubject$.next(accessToken)

        const userJson = localStorage.getItem('user');
        if (userJson) {
            const user: LoginResponse = JSON.parse(userJson);
            this.cuccrentUserSubject$.next(user);
        }

        this.isLoggedIn$ = this.accessToken$.pipe(
            map(token => !!token)
        );

        this.isEmployee$ = this.cuccrentUserSubject$.pipe(
            map(user => !!user && !user.isAdmin)
        );
    }

    public register(userData: RegisterRequest): Observable<RegisterResponse> {
        return this.http.post<RegisterResponse>(`${environment.apiUrl}/${this.AUTH_RESOURCE}/register`, userData);
    }

    public login(loginData: LoginRequest): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(`${environment.apiUrl}/${this.AUTH_RESOURCE}/login`, loginData).pipe(
            tap(response => {
                this.saveTokens(response);
            })
        );
    }

    public logout(): void {
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('accessToken');
        localStorage.removeItem('user');
        this.accessTokenSubject$.next(null);
        this.cuccrentUserSubject$.next(null);
        this.router.navigate(['/']);
    }

    private saveTokens(response: LoginResponse): void {
        localStorage.setItem('refreshToken', response.refreshToken);
        localStorage.setItem('accessToken', response.token)
        localStorage.setItem('user', JSON.stringify(response));
        this.accessTokenSubject$.next(response.token);
        this.cuccrentUserSubject$.next(response);
    }

    public deleteAccessToken(): void {
        localStorage.removeItem('accessToken');
        this.accessTokenSubject$.next(null);
    }

    public refreshToken(): Observable<LoginResponse> {
        const refreshToken = localStorage.getItem('refreshToken');

        if (!refreshToken) {
            this.logout();
        }

        return this.http.post<LoginResponse>(`${environment.apiUrl}/${this.AUTH_RESOURCE}/refresh`, { refreshToken })
            .pipe(
                tap(response => {
                    this.saveTokens(response);
                })
            )
    }

    public getAccessToken(): string | null {
        return this.accessTokenSubject$.getValue();
    }

    public isLoggedIn(): boolean {
        return !!this.getAccessToken();
    }

    public isUserAdmin(): boolean {
        return this.cuccrentUserSubject$.getValue()?.isAdmin ?? false;
    }

    //dodao
    public isUserEmployee(): boolean {
        return this.isLoggedIn() && !this.isUserAdmin();
    }
}