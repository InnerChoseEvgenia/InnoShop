import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IAuthor } from '../shared/models/author';
import { IType } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(private http:HttpClient) { }

  baseUrl= 'http://localhost:6004/';

  getProductById(id:string){
    return this.http.get<IProduct>(this.baseUrl + 'Product/GetProductById/' +id );
  }

  getProducts(storeParams: StoreParams){
    let params = new HttpParams();
    if(storeParams.authorId){
      params = params.append('authorId', storeParams.authorId);
    }
    if(storeParams.typeId){
      params = params.append('typeId', storeParams.typeId);
    }
    if(storeParams.search){
      params = params.append('search', storeParams.search);
    }


    params = params.append('sort', storeParams.sort);
    params = params.append('pageIndex', storeParams.pageNumber);
    params = params.append('pageSize', storeParams.pageSize);
    
    return this.http.get<IPagination<IProduct[]>>(this.baseUrl+'Product/GetAllProducts', {params});
  }
  getAuthors(){
    return this.http.get<IAuthor[]>(this.baseUrl+'Product/GetAllAuthor');
  }
  getTypes(){
    return this.http.get<IType[]>(this.baseUrl+'Product/GetAllTypes');
  }
}
