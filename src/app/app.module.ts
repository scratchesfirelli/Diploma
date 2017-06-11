import { HomeComponent } from './components/home/home.component';
import { OrderService } from './services/order.service';
import { CartService } from './services/cart.service';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';
import { TruncatePipe } from './pipes/truncate';
import { ProductService } from './services/product.service';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductComponent } from './components/product/product.component';
import { LoginComponent } from './components/login/login.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { RegisterComponent } from './components/register/register.component';
import { UserComponent } from './components/user/user.component';
import { AdminGuard } from "./guards/admin.guard";
import { CartComponent } from './components/cart/cart.component';
import { OrderListComponent } from './components/order-list/order-list.component';

import { Routes, RouterModule } from "@angular/router";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { ModalModule } from "ng2-modal";

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: UserComponent, canActivate: [AuthGuard] },
  { path: 'user/:email', component: UserComponent, canActivate: [AdminGuard] },
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
  {
    path: 'order', children: [
      { path: '', redirectTo: 'list/1', pathMatch: 'full' },
      { path: 'list/:page', component: OrderListComponent, canActivate: [AuthGuard] },
    ]
  },
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
    UserComponent,
    CartComponent,
    OrderListComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    RouterModule.forRoot(routes),
    ModalModule
  ],
  providers: [ProductService, AuthService, AuthGuard, AdminGuard, CartService, OrderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
