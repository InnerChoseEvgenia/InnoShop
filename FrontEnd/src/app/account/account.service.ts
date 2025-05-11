import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../shared/models/account/register';
import { environment } from '../../environments/environment.development';
import {Login} from '../shared/models/account/login';
import { User } from '../shared/models/account/user';
import { map, of, ReplaySubject } from 'rxjs';
import { Router } from '@angular/router';
import { ConfirmEmail } from '../shared/models/account/confirmEmail';
import { ResetPassword } from '../shared/models/account/resetPassword';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSource = new ReplaySubject<User | null>(1);
  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient,
    private router: Router
  ) { }

  register(model: Register) {
    return this.http.post(`${environment.appUrl}/Account/register`, model);
  }
  confirmEmail(model: ConfirmEmail) {
    return this.http.put(`${environment.appUrl}/Account/confirm-email`, model);
  }
  resendEmailConfirmationLink(email: string) {
    return this.http.post(`${environment.appUrl}/Account/resend-email-confirmation-link/${email}`, {});
  }
  forgotUsernameOrPassword(email: string) {
    return this.http.post(`${environment.appUrl}/Account/forgot-username-or-password/${email}`, {});
  }
  resetPassword(model: ResetPassword) {
    return this.http.put(`${environment.appUrl}/Account/reset-password`, model);
  }

  refreshUser(jwt: string | null) {
    if (jwt === null) {
      this.userSource.next(null);
      return of(undefined);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http.get<User>(`${environment.appUrl}account/refresh-page`, {headers}).pipe(
      map((user: User) => {
        if (user) {
          this.setUser(user);
        }
      })
    )
  }

  login(model: Login) {
    return this.http.post<User>(`${environment.appUrl}/Account/login`, model)
    .pipe(
      map((user: User) => {
        if (user) {
          this.setUser(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem(environment.userKey);
    this.userSource.next(null);
    this.router.navigateByUrl('/');
    //this.stopRefreshTokenTimer();
  }

  getJWT() {
    const key = localStorage.getItem(environment.userKey);
    if (key) {
      const user: User = JSON.parse(key);
      return user.jwt;
    } else {
      return null;
    }
  }


  private setUser(user: User) {
    //this.stopRefreshTokenTimer();
    //this.startRefreshTokenTimer(user.jwt);
    localStorage.setItem(environment.userKey, JSON.stringify(user));
    this.userSource.next(user);

    //this.sharedService.displayingExpiringSessionModal = false;
   // this.checkUserIdleTimout();
  }

}
