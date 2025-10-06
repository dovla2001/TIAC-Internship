import { Routes } from '@angular/router';
import {Home} from './home/home';
import {Register} from './auth/register/register';
import {Login} from './auth/login/login';
import { EmployeeDashboard } from './pages/employee-dashboard/employee-dashboard';
import { AdminDashboard } from './pages/admin-dashboard/admin-dashboard';
import { AuthGuard } from './core/guards/auth-guard';
import { AdminGuard } from './core/guards/admin-guard';
import { EmployeeGuard } from './core/guards/employee-guard';
import { ProductDetails } from './pages/product-details/product-details';
import { Cart } from './pages/cart/cart';
import { OrderHistory } from './pages/order-history/order-history';

export const routes: Routes = [
    {path: '', component: Home},
    {path: 'register', component: Register},
    {path: 'login', component: Login},
    {path: 'employee-dashboard', component: EmployeeDashboard, canActivate: [AuthGuard, EmployeeGuard]},
    {path: 'admin-dashboard', component: AdminDashboard, canActivate: [AuthGuard, AdminGuard]},
    {path: 'products/:id', component: ProductDetails, canActivate: [AuthGuard, EmployeeGuard]},
    {
      path: 'admin',
      canActivate: [AuthGuard, AdminGuard],
      loadChildren: () => import('./pages/admin/admin.routes').then(r => r.ADMIN_ROUTES)
    },
    {path: 'cart', component: Cart, canActivate: [AuthGuard, EmployeeGuard]},
    {path: 'order-history', component: OrderHistory, canActivate: [AuthGuard, EmployeeGuard]}
];
