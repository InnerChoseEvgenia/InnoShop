import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'InnoShop';

  constructor(
    private http: HttpClient){}
    ngOnInit():void {
 throw new Error ('Method not implemented.');
    }
    
}
