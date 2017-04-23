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

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'product',
    children: [
      { path: '', redirectTo: 'list/1', pathMatch: 'full' },
      { path: 'add', component: ProductFormComponent },
      { path: 'edit/:id', component: ProductFormComponent },
      { path: 'list/:page', component: ProductListComponent },
      { path: 'view/:id', component: ProductComponent }
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
    TruncatePipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    RouterModule.forRoot(routes),
    ModalModule
  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
