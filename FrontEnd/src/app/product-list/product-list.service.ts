import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductListService {

  constructor(private http: HttpClient) { }

  getProductListByUserName(){
    return this.http.get(`${environment.appUrl}productList/GetProductListByUserName`);
  }
}
