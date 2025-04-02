import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { response } from 'express';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'InnoShop';
  products: IProduct []=[];

  constructor(private http: HttpClient){}
  ngOnInit():void {
    this.http.get<IPagination<IProduct[]>>('http://localhost:6004/Product/GetAllProducts').subscribe({
      next:response => {
        this.products=response.data,
        console.log(response)
      },
      error: error=> console.log (error),
      complete:()=>{
        console.log ('Product API call completed');
      } 
    })
  }   
}
