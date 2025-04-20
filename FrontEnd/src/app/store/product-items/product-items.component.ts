import { Component, Input } from '@angular/core';
import { IProduct } from '../../shared/models/product/product';

@Component({
  selector: 'app-product-items',
  standalone: false,
  templateUrl: './product-items.component.html',
  styleUrl: './product-items.component.css'
})
export class ProductItemsComponent {
 @Input() product?:IProduct;
}
