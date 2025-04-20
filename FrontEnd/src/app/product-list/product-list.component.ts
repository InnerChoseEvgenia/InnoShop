import { Component, OnInit } from '@angular/core';
import { ProductListService } from './product-list.service';

@Component({
  selector: 'app-product-list',
  standalone: false,
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit{
  message: string | undefined;

  constructor(private productListService: ProductListService) {}

  ngOnInit(): void {
    this.productListService.getProductListByUserName().subscribe({
      next: (respose: any) => this.message = respose.value.message,
      error: error => console.log(error)
    })
  }

}