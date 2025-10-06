export interface OrderResponse {
    readonly orderId: number;
}

export interface AllOrdersResponse {
  readonly orderId: number;
  readonly customerName: string;
  readonly orderDate: string; 
  readonly totalPrice: number;
  readonly numberOfItems: number;
}

export interface OrderItem {
  readonly fullProductName: string; 
  readonly quantity: number;
  readonly pricePerItem: number;
  readonly totalPrice: number;
}

export interface Order {
  orderId: number;
  orderDate: string; 
  totalPrice: number;
  items: OrderItem[];
}
