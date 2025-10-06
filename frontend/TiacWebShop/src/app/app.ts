import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './core/services/auth.service';
import { CartIcon } from "./pages/cart-icon/cart-icon";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, CartIcon, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TiacWebShop');
  public isEmployee$: Observable<boolean>;
  public isLoggedIn$: Observable<boolean>;

  private authService = inject(AuthService);
  
  constructor() {
    this.isLoggedIn$ = this.authService.isLoggedIn$;
    this.isEmployee$ = this.authService.isEmployee$;
  }

  logout(): void {
    this.authService.logout();
  }
}
