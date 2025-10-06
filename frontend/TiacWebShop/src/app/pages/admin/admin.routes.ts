import { Routes } from '@angular/router';

import { AdminDashboard } from './components/admin-dashboard/admin-dashboard';
import { CreateAttribute } from './components/create-attribute/create-attribute';
import { SetAttributeValue } from './components/set-attribute-value/set-attribute-value';
import { CreateProduct } from './components/create-product/create-product';
import { CreateProductVariant } from './components/create-product-variant/create-product-variant';
import { ViewOrders } from './components/view-orders/view-orders';

export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    component: AdminDashboard,
    children: [
      { path: 'create-attribute', component: CreateAttribute },
      { path: 'set-attribute-value', component: SetAttributeValue },
      { path: 'create-product', component: CreateProduct },
      { path: 'create-product-variant', component: CreateProductVariant },
      { path: 'view-orders', component: ViewOrders},
    ]
  }
];