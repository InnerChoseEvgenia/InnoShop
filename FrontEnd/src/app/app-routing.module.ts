import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { AuthorizationGuard } from './shared/guards/authorization.guard';
//import { ServerErrorComponent } from './core/server-error/server-error.component';
//import { UnAuthenticatedComponent } from './core/un-authenticated/un-authenticated.component';
//import { NotFoundComponent } from './core/not-found/not-found.component';
//import { ValidationMessagesComponent } from './shared/components/errors/validation-messages/validation-messages.component';


const routes: Routes = [
  {path: '',  component:HomeComponent, data:{breadcrumb:'Home'}},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthorizationGuard],
    children: [
      { path: 'productList', component: ProductListComponent }
    ]
  },
  //{path: 'productList',  component:ProductListComponent},
  //{path: 'validation-messages', component: ValidationMessagesComponent},
  //{path: 'not-found', component: NotFoundComponent},
  //{path: 'un-authenticated', component: UnAuthenticatedComponent},
 // {path: 'server-error', component: ServerErrorComponent},
  {path: 'account', loadChildren: () => import('./account/account.module').then(module => module.AccountModule) },
  {path: 'store', loadChildren:()=>import('./store/store.module').then(mod=>mod.StoreModule), data:{breadcrumb:'Store'}},
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
