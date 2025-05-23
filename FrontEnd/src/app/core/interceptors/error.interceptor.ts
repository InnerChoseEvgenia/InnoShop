import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router,
    private toastr: ToastrService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error){
          switch (error.status) {
            case 400:
              if(error.error.errors){
                const modelStateErrors = [];
                for (const key in error.error.errors){
                  if (error.error.errors[key]){
                    modelStateErrors.push(error.error.errors[key])
                  }
                }
                throw modelStateErrors.flat();
              } 
              else {
                this.toastr.error(error.error, error.status);
                //this.toastr.error('Bad Request', error.status);
              }
              break;
            case 401:
              //this.toastr.error(error.error.message, error.status);
              this.toastr.error(error.error, error.status);
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationExtras: NavigationExtras = {state: {error: error.error}};
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}

  //intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    //return next.handle(request).pipe(
    // catchError((error)=>{
      // if(error){
         //if(error.status === 404){
          //this.router.navigateByUrl('/not-found');
          //}
          //if(error.status === 401){
          // this.router.navigateByUrl('/un-authenticated');
          //}
          //if(error.status === 500){
            //this.router.navigateByUrl('/server-error');
         // }
      // }
        //return throwError(()=> new Error(error));
      //})
   //)
  //}
//}
