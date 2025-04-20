import { Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import { StoreService } from './store.service';
import { IProduct } from '../shared/models/product/product';
import { IAuthor } from '../shared/models/product/author';
import { IType } from '../shared/models/product/type';
import { StoreParams } from '../shared/models/product/storeParams';

@Component({
  selector: 'app-store',
  standalone: false,
  templateUrl: './store.component.html',
  styleUrl: './store.component.css'
})
export class StoreComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  products:IProduct[]=[];
  authors:IAuthor[]=[];
  types:IType[]=[];
  storeParams = new StoreParams();
  totalCount = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Ascending', value: 'priceAsc' },
    { name: 'Price: Descending', value: 'priceDesc'}
  ];

constructor (private storeService: StoreService ){}

  ngOnInit(): void {
    this.getProducts();
    this.getAuthors();
    this.getTypes();
  }
  getProducts(){
    this.storeService.getProducts(this.storeParams).subscribe({
      next: response =>{
        this.products = response.data;
        this.storeParams.pageNumber = response.pageIndex;
        this.storeParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: error=>console.log(error)
    });
  }
  getAuthors(){
    this.storeService.getAuthors().subscribe({
      next: response =>{
        this.authors= [{id:'',name:'All'}, ...response]
      },
      error: error=>console.log(error)
    });
  }
  getTypes(){
    this.storeService.getTypes().subscribe({
      next: response =>{
        this.types= [{id:'',name:'All'}, ...response]
      },
      error: error=>console.log(error)
    });
  }
  onAuthorSelected(authorId: string){
    this.storeParams.authorId = authorId;
    this.getProducts();
  }
  onTypeSelected(typeId: string){
    this.storeParams.typeId = typeId;
    this.getProducts();
  }
  onSortSelected(sort:string){
    this.storeParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event:any){
    this.storeParams.pageNumber = event.page;
    this.getProducts();
  }
  onSearch(){
    this.storeParams.search = this.searchTerm?.nativeElement.value;
    this.storeParams.pageNumber = 1;
    this.getProducts();
  }
  onReset(){
    if(this.searchTerm){
      this.searchTerm.nativeElement.value = '';
      this.storeParams = new StoreParams();
      this.getProducts();
    }
  }
}
