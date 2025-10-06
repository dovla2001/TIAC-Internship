import { HttpErrorResponse, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, switchMap, throwError } from 'rxjs';

const addTokenToRequest = (req: HttpRequest<any>, token: string): HttpRequest<any> => {
  return req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });
};

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService);
  const accessToken = authService.getAccessToken();

 if (accessToken) {
    req = addTokenToRequest(req, accessToken);
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && accessToken) {
      
        console.log("Uhvaćena 401 greška. STARI token je:", accessToken);

        authService.deleteAccessToken();

        return authService.refreshToken().pipe(
          switchMap((response) => {

            console.log("Uspešno osvežavanje. NOVI token je:", response.token);

            const newReq = addTokenToRequest(req, response.token);
            return next(newReq);
          }),
          catchError((refreshError) => {
            authService.logout();
            return throwError(() => new Error('Session expired. Please log in again.'));
          })
        );
      }
      return throwError(() => error);
    })
  );

};