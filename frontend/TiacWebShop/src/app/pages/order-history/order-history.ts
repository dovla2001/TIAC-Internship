import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderServices } from '../../core/services/order.services';
import { Order } from '../../core/models/order.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-history',
  imports: [CommonModule, RouterLink],
  templateUrl: './order-history.html',
  styleUrl: './order-history.css'
})
export class OrderHistory {
  
  public orders$!: Observable<Order[]>;

  constructor(private orderService: OrderServices) { }

  ngOnInit(): void {
    this.orders$ = this.orderService.getOrdersHistory();
  }   
}
