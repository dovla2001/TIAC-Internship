import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AllOrdersResponse } from '../../../../core/models/order.model';
import { OrderServices } from '../../../../core/services/order.services';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-view-orders',
  imports: [CommonModule],
  templateUrl: './view-orders.html',
  styleUrl: './view-orders.css'
})
export class ViewOrders {
  public orders$!: Observable<AllOrdersResponse[]>;

  constructor(private orderService: OrderServices) { }

  ngOnInit(): void {
    this.orders$ = this.orderService.getAllOrders();
  } 
}
