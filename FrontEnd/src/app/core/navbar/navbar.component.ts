import { Component } from '@angular/core';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
  export class NavbarComponent {
    //collapsed = true;
  
    constructor(public accountService: AccountService) { }
  
    logout()
     {this.accountService.logout();
  }
  
    //toggleCollapsed() {
      //this.collapsed = !this.collapsed;
   // }
  }

