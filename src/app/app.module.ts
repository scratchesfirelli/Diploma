import { CartService } from './services/cart.service';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';
import { TruncatePipe } from './pipes/truncate';
import { ProductService } from './services/product.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import {ModalModule} from "ng2-modal";

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductComponent } from './components/product/product.component';
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from './components/login/login.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { RegisterComponent } from './components/register/register.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AdminGuard } from "app/guards/admin.guard";
import { CartComponent } from './components/cart/cart.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  {
    path: 'product',
    children: [
      { path: '', redirectTo: 'list/1', pathMatch: 'full' },
      { path: 'add', component: ProductFormComponent, canActivate: [AdminGuard] },
      { path: 'edit/:id', component: ProductFormComponent, canActivate: [AdminGuard] },
      { path: 'list/:page', component: ProductListComponent },
      { path: 'view/:id', component: ProductComponent }
    ]
  },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProductComponent,
    LoginComponent,
    ProductFormComponent,
    ProductListComponent,
    TruncatePipe,
    RegisterComponent,
    ProfileComponent,
    CartComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    RouterModule.forRoot(routes),
    ModalModule
  ],
  providers: [ProductService, AuthService, AuthGuard, AdminGuard, CartService],
  bootstrap: [AppComponent]
})
export class AppModule { }
