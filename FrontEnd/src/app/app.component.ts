import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { response } from 'express';
import { IProduct } from './shared/models/product/product';
import { IPagination } from './shared/models/product/pagination';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'InnoShop';
  products: IProduct []=[];

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ){}
  ngOnInit():void {
    this.refreshUser();
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
  
  private refreshUser() {
    const jwt = this.accountService.getJWT();
    if (jwt) {
     this.accountService.refreshUser(jwt).subscribe({
        next: _ => {},
        error: _ => {
         this.accountService.logout();

          //if (error.status === 401) {
           // this.sharedService.showNotification(false, 'Account blocked', error.error);
          //}
        }
      })
    } else {
     this.accountService.refreshUser(null).subscribe();
    }
  }
}

